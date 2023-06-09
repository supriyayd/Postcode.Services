using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Postcode.Services;

namespace LambdaAPI;

public class Startup
{

    private const string ApiTitle = "Postcode.API";
    private const string ApiVersion = "v1";
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container
    public void ConfigureServices(IServiceCollection services)
    {
        ConfigureSwagger(services);
        services.RegisterPostcodeService(Configuration);
        var corsConfiguration = Configuration.GetSection("Cors").Get<CorsConfigurations>();
        services.AddCors(options =>
        {
            options.AddPolicy("SiteCorsPolicy", corsBuilder =>
            {
                corsBuilder.AllowAnyHeader();
                corsBuilder.AllowAnyMethod();
                corsBuilder.SetIsOriginAllowedToAllowWildcardSubdomains();
                corsBuilder.WithOrigins(corsConfiguration.AllowedOrigins.ToArray());

            });
        });
        services.AddControllers();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseSwagger();
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });
        }

        app.UseAuthorization();
        app.UseCors("SiteCorsPolicy");

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync("Welcome to running api for postcode");
            });

        });
    }

    private void ConfigureSwagger(IServiceCollection services)
    {
        services.AddSwaggerGen(o =>
        {
            o.SwaggerDoc(ApiVersion, new OpenApiInfo
            {
                Title = ApiTitle,
                Version = ApiVersion,
                Description = "This is the post code API."
            });

        });
    }
}