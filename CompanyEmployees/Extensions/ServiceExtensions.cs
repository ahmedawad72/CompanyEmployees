using Contracts;
using LoggerService;
using Microsoft.EntityFrameworkCore;
using Repository;
using Service;
using Service.Contracts;

namespace CompanyEmployees.Extensions
{
    public static class ServiceExtensions
    {   
        // extension method is a static method of a static class and the first parameter is 'this'
        // and this represents the data type of the object which will be using that extension method
        // here we are extending IServiceCollection to add CORS configuration
        // this method can be called in Program.cs to configure CORS for the application
        // allowing any origin, method, and header for simplicity
        // in a production application, you might want to restrict these settings for security reasons
        // this method adds a CORS policy named "CorsPolicy" to the service collection


        // CORS: Cross-Origin Resource Sharing
        // CORS is a mechanism to give or restrict access rights to applications from different domains.

        // We are using basic CORS policy settings because allowing any origin,method, and header is okay for now.
        // But we should be more restrictive with those settings in the production environment.More precisely, as restrictive as possible.
        public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            /*
                Instead of the AllowAnyOrigin() method which allows requests from any 
                source, we can use th       e WithOrigins("https://example.com") which will 
                allow requests only from that concrete source. Also, instead of 
                8 
                AllowAnyMethod() that allows all HTTP methods, we can use 
                WithMethods("POST", "GET") that will allow only specific HTTP methods. 
                Furthermore, you can make the same changes for the AllowAnyHeader() 
                method by using, for example, the WithHeaders("accept", "content
                type") method to allow only specific headers.
            */
            options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .WithExposedHeaders("X-Pagination")
                       );
        });


        /*  IISIntegration method is used to configure the application to work with IIS.
            This method sets up the IISOptions for the application.
            Currently, it does not modify any default settings but provides a place
            to add custom configurations if needed in the future.

            1- AutomaticAuthentication:
                if true, the authentication middleware sets the HttpContext.User and responds to generic challenges.
                if false, the authentication middleware only provides an identity (HttpContext.User) and responds to challenges when explicitly requested
                by the 'AuthenticationScheme'

            2- authenticationDisplayName: if null,  sets the display name shown to users on login pages.
            3- forwardClientCertificate: if true, and the ms-aspnetcore-ClientCert request header is present,
                the httpContext.Connection.ClientCertificate property is populated with the client certificate.
        */

        public static void ConfigureIISIntegration(this IServiceCollection services) =>
            services.Configure<IISOptions>(options =>
            {
                // IIS configuration options can be set here if needed
            });


        public static void ConfigureLoggerService (this IServiceCollection services)=>
            services.AddSingleton<ILoggerManager,LoggerManager>();

        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        public static void ConfigureServiceManager(this IServiceCollection services) =>
           services.AddScoped<IServiceManager, ServiceManager>();


        public static void ConfigureSqlContext(this IServiceCollection services,IConfiguration configuration) =>
            services.AddDbContext<RepositoryContext>(opts =>
            opts.UseSqlServer(configuration.GetConnectionString("sqlConnection")));

    }
}
