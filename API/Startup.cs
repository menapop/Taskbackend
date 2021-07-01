using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using API.Filters;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repo;
using Service;
using Service.Helpers;
using Service.Initializer;
using FluentValidation.AspNetCore;
using Service.Validators;

using NLog;


namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            LogManager.LoadConfiguration(System.String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
            _env = env;
            
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment _env { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(
                options=>
                {
                  //  options.Filters.Add(new SecurityFilter());
                    options.Filters.Add(new ValidationFilter());

                }).AddFluentValidation( options =>
                {
                    options.RegisterValidatorsFromAssembly(typeof(CreateUserDtoValidatior).Assembly);
                }
                ).AddNewtonsoftJson();
          
            
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin()
                 .AllowAnyMethod().AllowAnyHeader().AllowAnyMethod());
            });
            // appsetting 
            var appSettingSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingSection);
            var appSettings = appSettingSection.Get<AppSettings>();


            // repo and Service 
            new RepositoryInitializer(services);
            new ServiceInitializer(services);

            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddDbContext<ApplicationContext>(options =>

              options.UseSqlServer(appSettings.ConnectionString), ServiceLifetime.Scoped);

            // services for UserManager , signin Manager , Role manager 
           

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfile());
            });

            IMapper Mapper = mappingConfig.CreateMapper();
            services.AddSingleton(Mapper);
            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

                c.DescribeAllParametersInCamelCase();
                //c.DescribeAllEnumsAsStrings();
                //c.DescribeStringEnumsInCamelCase();
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
               app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();  
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseExceptionHandler(options =>
            {
                options.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "text/html";
                    var exceptionObject = context.Features.Get<IExceptionHandlerFeature>();
                    if (exceptionObject != null)
                    {

                        string fileName = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "_Logs.txt";
                        string path = _env.WebRootPath + "/Logs/";
                        try
                        {
                            StreamWriter file = new StreamWriter(path + fileName, true);
                            file.WriteLine(DateTime.Now.ToString() + ":" + exceptionObject.Error.Message);

                            file.WriteLine(DateTime.Now.ToString() + ":" + exceptionObject.Error.StackTrace);


                        }
                        catch (Exception ex)
                        {

                        }
                        await context.Response.WriteAsync(exceptionObject.Error.Message).ConfigureAwait(false);
                    }

                });
            });
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "My Api V1");

            });

            app.UseCors("CorsPolicy");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

            });
        }
    }
    }
