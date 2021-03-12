using CoreWCF;
using CoreWCF.Channels;
using CoreWCF.Dispatcher;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace NetCoreWithDIServer
{
    public class InstanceProvider<TContract> : IInstanceProvider
    {
        private readonly IServiceProvider _serviceProvider;

        public InstanceProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public object GetInstance(InstanceContext instanceContext, Message message) => _serviceProvider.GetService<TContract>();
        public object GetInstance(InstanceContext instanceContext) => _serviceProvider.GetService<TContract>();
        public void ReleaseInstance(InstanceContext instanceContext, object instance) { }
    }
}