using System;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;

namespace Fohjin.EventStore
{
    public interface IAggregateRootFactory
    {
        TAggregateRoot Create<TAggregateRoot>();
        object Create(Type type);
    }

    public class AggregateRootFactory : IAggregateRootFactory
    {
        private readonly ProxyGenerator _proxyGenerator;

        public AggregateRootFactory()
        {
            _proxyGenerator = new ProxyGenerator();
        }

        public TAggregateRoot Create<TAggregateRoot>()
        {
            return (TAggregateRoot)Create(typeof(TAggregateRoot));
        }

        public object Create(Type type)
        {
            TypeHasPrivateApplyMethodDeclared(type);

            var instance = type.GetConstructors().Last().Invoke(new object[] {});
            var eventProvider = new EventProvider(instance);
            var proxyGenerationOptions = new ProxyGenerationOptions();
            proxyGenerationOptions.AddMixinInstance(instance);
            proxyGenerationOptions.AddMixinInstance(eventProvider);
            proxyGenerationOptions.AddMixinInstance(new Orginator());

            return _proxyGenerator.CreateClassProxy(
                type, 
                proxyGenerationOptions,
                eventProvider
            );
        }

        private static void TypeHasPrivateApplyMethodDeclared(Type type)
        {
            var applyMethod = type.GetMethod("Apply", BindingFlags.Instance | BindingFlags.Public);

            if (applyMethod == null)
                throw new PublicVirtualApplyMethodException(string.Format("Object '{0}' does not have a private 'void Apply(object @event);' method", type.FullName));

            if (applyMethod.GetParameters().Count() != 1 || applyMethod.GetParameters().First().ParameterType != typeof(object))
                throw new PublicVirtualApplyMethodException(string.Format("The Apply method in Object '{0}' does not have the correct signature 'Apply(object @event)'", type.FullName));

            if (applyMethod.ReturnType.Name != "Void")
                throw new PublicVirtualApplyMethodException(string.Format("The Apply method in Object '{0}' does not return 'void'", type.FullName));
        }
    }
}