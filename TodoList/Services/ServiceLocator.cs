using System;
using System.Collections.Generic;

namespace TodoList.Services
{
    public class ServiceLocator
    {
        protected static Dictionary<Type, object> _instances = new Dictionary<Type, object>();

        public static void Register<TInterface, TConcrete>(TConcrete instance)
        {
            _instances.Add(typeof(TInterface), instance);
        }

        public static TInterface GetInstance<TInterface>()
        {
            if(_instances.ContainsKey(typeof(TInterface)))
            {
                return (TInterface)_instances[typeof(TInterface)];
            }

            return default(TInterface);
        }

    }
}
