using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using VidyaVahini.Core.Extensions;
using VidyaVahini.DataAccess.Contracts;
using VidyaVahini.DataAccess.Models;
using VidyaVahini.DataAccess.Repository;
using VidyaVahini.Repository.Contracts;
using VidyaVahini.Repository;
using VidyaVahini.Service.Contracts;
using VidyaVahini.Service;
using VidyaVahini.WebApi.Aspects;
using VidyaVahini.WebApi.Middleware;

namespace VidyaVahini.WebApi
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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });

            services.AddCors();

            services.AddControllers()
                .AddMvcOptions(options =>
            {
                options.Filters.Add<ControllerActionProfilingFilter>();
                options.Filters.Add<ExceptionFilter>();
            });

            services.AddMemoryCache();

            // Add API Versioning to as service to your project 
            services.AddApiVersioning(config =>
            {
                // Specify the default API Version
                config.DefaultApiVersion = new ApiVersion(1, 0);
                // If the client hasn't specified the API version in the request, use the default API version number 
                config.AssumeDefaultVersionWhenUnspecified = true;
                // Advertise the API versions supported for the particular endpoint
                config.ReportApiVersions = true;
            });

            services.AddSingleton<Infrastructure.Contracts.ILogger, Infrastructure.Logger.FileLogger>();
            services.AddSingleton<Infrastructure.Contracts.ICache, Infrastructure.Cache.MemoryCache>();

            services.AddScoped<IDemoService, DemoService>();
            services.DecorateWithDispatchProxy<IDemoService, ServicesDecorator<IDemoService>>();

            services.AddScoped<ICacheService, CacheService>();
            services.DecorateWithDispatchProxy<ICacheService, ServicesDecorator<ICacheService>>();

            services.AddScoped<IStateService, StateService>();
            services.AddScoped<IStateRepository, StateRepository>();
            services.DecorateWithDispatchProxy<IStateService, ServicesDecorator<IStateService>>();


            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.DecorateWithDispatchProxy<ICountryService, ServicesDecorator<ICountryService>>();

            services.AddScoped<IGenderService, GenderService>();
            services.AddScoped<IGenderRepository, GenderRepository>();
            services.DecorateWithDispatchProxy<IGenderService, ServicesDecorator<IGenderService>>();

            services.AddScoped<ILanguageService, LanguageService>();
            services.AddScoped<ILanguageRepository, LanguageRepository>();
            services.DecorateWithDispatchProxy<ILanguageService, ServicesDecorator<ILanguageService>>();

            services.AddScoped<IClassService, ClassService>();
            services.AddScoped<IClassRepository, ClassRepository>();
            services.DecorateWithDispatchProxy<IClassService, ServicesDecorator<IClassService>>();

            services.AddScoped<IQualificationService, QualificationService>();
            services.AddScoped<IQualificationRepository, QualificationRepository>();
            services.DecorateWithDispatchProxy<IQualificationService, ServicesDecorator<IQualificationService>>();

            services.AddScoped<ISubjectService, SubjectService>();
            services.AddScoped<ISubjectRepository, SubjectRepository>();
            services.DecorateWithDispatchProxy<ISubjectService, ServicesDecorator<ISubjectService>>();

            services.AddScoped<ILanguageService, LanguageService>();
            services.AddScoped<ILanguageRepository, LanguageRepository>();
            services.DecorateWithDispatchProxy<ILanguageService, ServicesDecorator<ILanguageService>>();

            services.AddScoped<ITeacherService, TeacherService>();
            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.DecorateWithDispatchProxy<ITeacherService, ServicesDecorator<ITeacherService>>();

            services.AddScoped<IUserAccountService, UserAccountService>();
            services.AddScoped<IUserAccountRepository, UserAccountRepository>();
            services.DecorateWithDispatchProxy<IUserAccountService, ServicesDecorator<IUserAccountService>>();

            services.AddScoped<IMentorService, MentorService>();
            services.AddScoped<IMentorRepository, MentorRespository>();
            services.DecorateWithDispatchProxy<IMentorService, ServicesDecorator<IMentorService>>();

            services.AddScoped<ISchoolService, SchoolService>();
            services.AddScoped<ISchoolRepository, SchoolRepository>();
            services.DecorateWithDispatchProxy<ISchoolService, ServicesDecorator<ISchoolService>>();

            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<IDashboardRepository, DashboardRepository>();
            services.DecorateWithDispatchProxy<IDashboardService, ServicesDecorator<IDashboardService>>();

            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IErrorRepository, ErrorRepository>();
            services.AddScoped<ISchoolRepository, SchoolRepository>();
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<IUserLanguageRepository, UserLanguageRepository>();
            services.AddScoped<ITeacherClassRepository, TeacherClassRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<ITeacherSubjectRepository, TeacherSubjectRepository>();

            services.AddScoped<IQueryService, QueryService>();
            services.AddScoped<IQueryRepository, QueryRepository>();
            services.DecorateWithDispatchProxy<IQueryService, ServicesDecorator<IQueryService>>();


            services.AddDbContext<vidyavahiniContext>(options =>
            ///options.UseMySql(Configuration.GetConnectionString("VidyaVahiniDb"), x => x.ServerVersion("8.0.13-mysql")));

            options.UseMySql(Configuration.GetConnectionString("VidyaVahiniDb"),
                mySqlOptionsAction: sqlOptions => 
                {
                    sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 25,
                maxRetryDelay: System.TimeSpan.FromSeconds(60),
                errorNumbersToAdd: null);

                    sqlOptions.CommandTimeout((int)System.TimeSpan.FromMinutes(10).TotalSeconds);
                }
                ));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IDataAccessRepository<>), typeof(DataAccessRepository<>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            //app.UseOptions();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()); // allow any origin); // allow credentials  

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();    

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            loggerFactory.AddLog4Net();

        }
    }
}
