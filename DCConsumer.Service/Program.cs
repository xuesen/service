using Consumer.Interface;
using Consumer.TestLog.Consumer;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Infrastructure.Hibernate.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using NHibernate;
using Serilog;



namespace DCConsumer.Service
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            System.IO.Directory.SetCurrentDirectory(System.AppDomain.CurrentDomain.BaseDirectory);
            NHibernateLogger.SetLoggersFactory(new NHibernate.Logging.Serilog.SerilogLoggerFactory());
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
                            .ReadFrom.Configuration(hostingContext.Configuration)
                            .Enrich.FromLogContext())
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddOptions();
                    services.Configure<KafkaConsumerOptions>(hostContext.Configuration.GetSection("ConsumerSetting"));
                    services.AddHostedService<Worker>();
                    services.AddNHibernate();
                    services.AddSingleton(sp => sp);
                    services.AddSingleton<IConsumer, TestLogConsumer>();
                    services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
                });

    }
    public static class NHibernateExtensions
    {
        public static IServiceCollection AddNHibernate(this IServiceCollection services)
        {
            var dbContextManager = new DBContextManager();
            services.AddSingleton<IDBContextManager>(dbContextManager);
            services.AddScoped<IDBContext>(factory => dbContextManager.OpenDBContext());
            return services;
        }
    }
}
