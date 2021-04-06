using CoreWCF;
using CoreWCF.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace NetCoreWithDIServer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddServiceModelServices();
            services.AddSingleton<IMessageQueue, MessageQueue>();
            services.AddSingleton<Contract.IEchoService, EchoService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseServiceModel(builder =>
            {
                builder
                    .AddService<EchoService>()
                    .AddServiceEndpoint<EchoService, Contract.IEchoService>(new BasicHttpBinding(), "/basichttp")
                    .AddServiceEndpoint<EchoService, Contract.IEchoService>(new NetTcpBinding(), "/nettcp")
                    .ConfigureServiceHostBase<EchoService>(serviceHost =>
                    {
                        serviceHost.Description.Behaviors.Add(new InstanceProviderBehavior<Contract.IEchoService>(app.ApplicationServices));
                    });
            });
        }
    }
}
