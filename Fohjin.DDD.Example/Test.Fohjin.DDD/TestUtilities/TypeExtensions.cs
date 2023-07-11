using Fohjin.DDD.EventStore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using System;
using System.Reflection;

namespace Test.Fohjin.DDD.TestUtilities
{
    public static class TypeExtensions
    {
        public static object BuildObject(this Type type, IServiceProvider? serviceProvider = null)
        {
            var defaultConstructor = type.GetDefaultConstructorInfo() ?? throw new NotSupportedException($"{type}");

            var obj = defaultConstructor.Invoke(Array.Empty<object?>());

            var properties = type.GetSetterProperties();
            obj.FillObject(properties ,serviceProvider);
            return obj;
        }

        public static ConstructorInfo? GetDefaultConstructorInfo(this Type type) =>
             type.GetConstructors().FirstOrDefault(c => c.GetParameters().Length == 0);

        public static PropertyInfo[] GetSetterProperties(this Type type) =>
            type.GetProperties(BindingFlags.SetProperty | BindingFlags.Public | BindingFlags.Instance);

        public static PropertyInfo[] GetGetterProperties(this Type type) =>
            type.GetProperties(BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance);

        public static object FillObject(this object obj, IServiceProvider? serviceProvider) =>
            obj switch
            {
                Type type => type.BuildObject(serviceProvider),
                _ => obj.FillObject(obj.GetType().GetSetterProperties(), serviceProvider)
            };

        public static object FillObject(this object obj, PropertyInfo[] properties, IServiceProvider? serviceProvider)
        {
            foreach (var property in properties)
            {
                try
                {
                    property.SetValue(obj, property.PropertyType.GetNonDefaultValue(serviceProvider));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{obj.GetType()}.{property.Name}:> {ex.Message}");
                    throw;
                }
            }
            return obj;
        }

        public static object? GetNonDefaultValue(this Type type, IServiceProvider? serviceProvider)
        {
            if (type == typeof(int))
                return 1;
            else if (type == typeof(double))
                return 1.0;
            else if (type == typeof(decimal))
                return 1.0M;
            else if (type == typeof(string))
                return "1";
            else if (type == typeof(Guid))
                return Guid.NewGuid();
            else if (type == typeof(bool))
                return true;
            else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
            {
                var list = type?.GetDefaultConstructorInfo()?.Invoke(Array.Empty<object?>());
                var item = type?.GetGenericArguments()[0].GetNonDefaultValue(serviceProvider);
                type?.GetMethod("Add")?.Invoke(list, new object?[] { item });
                return list;
            }
            else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(KeyValuePair<,>))
            {
                var args =
                    type.GetGenericArguments().Select(t => t.GetNonDefaultValue(serviceProvider)).ToArray()
                    ;
                var ctor = type.GetConstructors()[0];
                return ctor.Invoke(args);

            }
            else if (type.IsInterface)
            {
                return type.GetInstanceTypes()?.FirstOrDefault()?.GetNonDefaultValue(serviceProvider);
            }
            else
            {
                var ctor = type.GetDefaultConstructorInfo();
                if (ctor == null && serviceProvider != null)
                {
                    try
                    {
                        return ActivatorUtilities.CreateInstance(serviceProvider, type);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"{type}:> {ex.Message}");
                        throw;
                    }
                }

                return type.GetDefaultConstructorInfo()?
                    .Invoke(Array.Empty<object?>())
                    .FillObject(serviceProvider);
            }
        }

        public static object? GetDefaultValue(this Type type) =>
            typeof(TypeExtensions).GetMethod(nameof(GetDefaultValue), 1, Type.EmptyTypes)
                ?.MakeGenericMethod(type)
                ?.Invoke(null, Array.Empty<object?>());

        public static T? GetDefaultValue<T>() => default;

        public static Type EnsureNotDefault(this Type type, object instance)
        {
            var properties = type.GetGetterProperties();
            foreach (var property in properties)
            {
                if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                {
                    var list = property.GetValue(instance, Array.Empty<object?>());
                    var value = list.GetType().GetProperty("Item").GetValue(list, new object[] { 0 });
                    value.GetType().EnsureNotDefault(value);
                }
                else
                {
                    var value = property.GetValue(instance, Array.Empty<object?>());


                    var defValue = GetDefaultValue(property.PropertyType);
                    if (value == null || object.Equals(value, defValue))
                        throw new NotSupportedException();
                    if (!value.GetType().IsValueType && value.GetType() != typeof(string))
                    {
                        try
                        {
                            value.GetType().EnsureNotDefault(value);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"{value.GetType()}.{property.Name}:> {ex.Message}");
                            throw;
                        }
                    }
                }
            }

            return type;
        }

        //TODO: need to be able to load external assemblies
        public static IEnumerable<Type> GetInstanceTypes(this Type type) =>
            from asm in AppDomain.CurrentDomain.GetAssemblies()
            from t in asm.GetTypes()
            where t.IsAssignableTo(type)
            where !t.IsAbstract
            select t;
    }
}
