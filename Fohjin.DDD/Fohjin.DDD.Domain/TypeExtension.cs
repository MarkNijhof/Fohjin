using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Fohjin.DDD.Domain
{
    public static class TypeExtension
    {
        public static MethodInfo GetMethod<TType>(this Type baseType, Expression<Action<TType>> method)
        {
            return baseType.GetMethod(((MethodCallExpression)method.Body).Method.Name);
        }
    }
}