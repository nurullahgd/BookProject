using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Ocelot.ApiGateway
{
    public class Startup
    {
        private readonly IConfiguration _cfg;
        public Startup(IConfiguration configuration)
        {
            _cfg = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            //var authenticationProviderKey = "HGgypZtVCeL2LJ0wpGhzmOJe/ljMkIb+vtBtJSlgiEs=";
            //services.AddAuthentication()
            //   .AddJwtBearer(authenticationProviderKey, x =>
            //   {
                   
            //       x.RequireHttpsMetadata = false;
                   
            //   });

            services.AddControllers();
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ocelot.ApiGateway", Version = "v1" });
            //});
            services.AddOcelot(_cfg);
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseSwagger();
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ocelot.ApiGateway v1"));
            }
            app.UseOcelot();
            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //app.UseOcelot().Wait();
        }
    }
}
