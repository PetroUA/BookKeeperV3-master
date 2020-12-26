using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bookKeeper
{
    public static class IoC
    {
        private static readonly IDictionary<Type, Type> Registered = new Dictionary<Type, Type>();

        public static void Register<TType, TConcrete>() where TConcrete : class, TType
        {
            Registered[typeof(TType)] = typeof(TConcrete);
        }

        public static TTypeToResolve Resolve<TTypeToResolve>()
        {
            return (TTypeToResolve)Resolve(typeof(TTypeToResolve)); //создать экземпляр зарегестрированного типа
        }

        public static object Resolve(Type type)
        {
            return GetInstance(Registered[type]);
        }

        private static object GetInstance(Type type)
        {
            var parameters = ResolveConstructorParameters(type);
            return Activator.CreateInstance(type, parameters.ToArray()); // создаем экземпляр класса без слова new, не зная о кнонструкторе
        }

        private static IEnumerable<object> ResolveConstructorParameters(Type type) //получает из списка конструкторов класса первый и для каждого его параметра (если они есть) пытается создать экземпляр
        {
            var constructorInfo = type.GetConstructors().First();
            return constructorInfo.GetParameters().Select(parameter => Resolve(parameter.ParameterType));
        }
    }
}