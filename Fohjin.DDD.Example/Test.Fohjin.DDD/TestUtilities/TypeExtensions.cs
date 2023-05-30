using Fohjin.DDD.EventStore;
using System.Reflection;

namespace Test.Fohjin.DDD.TestUtilities
{
    public static class TypeExtensions
    {
        public static object BuildObject(this Type type)
        {
            var defaultConstructor = type.GetDefaultConstructorInfo();
            if (defaultConstructor == null)
                throw new NotSupportedException();

            var obj = defaultConstructor.Invoke(Array.Empty<object?>());

            var properties = type.GetSetterProperties();
            FillObject(obj, properties);
            return obj;
        }

        public static ConstructorInfo? GetDefaultConstructorInfo(this Type type) =>
             type.GetConstructors().FirstOrDefault(c => c.GetParameters().Length == 0);

        public static PropertyInfo[] GetSetterProperties(this Type type) =>
            type.GetProperties(BindingFlags.SetProperty | BindingFlags.Public | BindingFlags.Instance);

        public static PropertyInfo[] GetGetterProperties(this Type type) =>
            type.GetProperties(BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance);

        public static void FillObject(object obj, PropertyInfo[] properties)
        {
            foreach (var property in properties)
                property.SetValue(obj, property.PropertyType.GetNonDefaultValue());
        }

        public static object? GetNonDefaultValue(this Type type)
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
            else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
                return type.GetDefaultConstructorInfo().Invoke(Array.Empty<object?>());
            else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(KeyValuePair<,>))
                return type.GetConstructors()[0].Invoke(
                    Array.Empty<object?>(),
                    type.GetGenericArguments().Select(t=>t.GetNonDefaultValue()).ToArray()
                    );
            else
                throw new NotSupportedException($"{type}");
        }

        public static object GetDefaultValue(this Type type) =>
            typeof(TypeExtensions).GetMethod(nameof(GetDefaultValue), 1, Type.EmptyTypes)
                .MakeGenericMethod(type)
                .Invoke(null, Array.Empty<object?>());

        public static T GetDefaultValue<T>() => default;

        public static Type EnsureNotDefault(this Type type, object instance)
        {
            var properties = type.GetGetterProperties();
            foreach (var property in properties)
            {
                var defValue = GetDefaultValue(property.PropertyType);
                var value = property.GetValue(instance, Array.Empty<object?>());
                if (value == null || object.Equals(value, defValue))
                    throw new NotSupportedException();
            }

            return type;
        }

        public static IEnumerable<Type> GetInstanceTypes(this Type type) =>
            type.Assembly.GetTypes()
                .Where(t => t.IsAssignableTo(type))
                .Where(t => !t.IsAbstract)
            ;
    }
}
