using CoreWCF;
using CoreWCF.Channels;
using CoreWCF.Description;
using CoreWCF.Dispatcher;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace NetCoreWithDIServer
{
    public class InstanceProviderBehavior<TContract> : IServiceBehavior
    {
        private readonly IServiceProvider _serviceProvider;

        public InstanceProviderBehavior(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            var provider = new InstanceProvider<TContract>(_serviceProvider);
            foreach (var channelDispatcher in serviceHostBase.ChannelDispatchers.OfType<ChannelDispatcher>())
            {
                foreach (var ed in channelDispatcher.Endpoints.Where(p => !p.IsSystemEndpoint))
                {
                    ed.DispatchRuntime.InstanceProvider = provider;
                }
            }
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }
    }
}