using System;
using System.Collections.Generic;
using System.Linq;

namespace VirtualScene.Common
{
    public abstract class ProxyManagerBase<T>
        where T: class, IProxyHolder
    {
        private IEnumerable<T> _proxyHolders;
        private T _nullProxyHolder;

        protected T GetProxyHolderFor<TEntity>(TEntity entity)
        {
            if (entity == null)
                return NullProxyHolder;
            return ProxyHolders.FirstOrDefault(p => p.TypeKey.IsInstanceOfType(entity)) ?? NullProxyHolder;
        }

        private IEnumerable<T> ProxyHolders
        {
            get { return _proxyHolders?? (_proxyHolders = RegisterProxyHolders()); }
        }

        protected abstract IEnumerable<T> RegisterProxyHolders();

        protected T GetProxyHolderBy(Type type)
        {
            if (type == null)
                return NullProxyHolder;
            return _proxyHolders.FirstOrDefault(p => p.TypeKey.IsAssignableFrom(type)) ?? NullProxyHolder;
        }

        public T NullProxyHolder
        {
            get { return _nullProxyHolder ?? (_nullProxyHolder = CreateNullProxyHolder()); }
        }

        protected abstract T CreateNullProxyHolder();
    }
}