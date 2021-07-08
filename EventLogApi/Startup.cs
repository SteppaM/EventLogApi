using System.Text.Json.Serialization;
using Application.Dtos;
using Application.Exceptions;
using Application.Services;
using EventLogApi.CustomProblemDetails;
using EventLogApi.Validation;
using FluentValidation;
using FluentValidation.AspNetCore;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Persistence;

namespace EventLogApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddProblemDetails(setup =>
            {
                setup.Map<QueryParameterException>(exception => new ParametersProblemDetails
                {
                    Title = "Query Parameter Exception",
                    Detail = exception.Description,
                    Status = StatusCodes.Status400BadRequest,
                    Type = "Type",
                    Instance = "Instance",
                    AdditionalInfo = "AdditionalInfo"
                });
            });
            
            services.AddDbContext<EventLogContext>(options => { options.UseInMemoryDatabase("EventLog"); });
            services.AddScoped<IEventLogService, EventLogService>();
            services.AddTransient<IValidator<LogEntryDto>, LogEntryValidator>();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "My API", Version = "v1"}); });
            
            services.AddControllers()
                .AddFluentValidation()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(
                        new JsonStringEnumConverter());
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseProblemDetails();
                            
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseSwagger();

            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });
        }
    }
}