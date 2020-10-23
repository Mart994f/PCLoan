using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using PCLoan.Data.Library.Repositorys;
using PCLoan.Logic.Library.Controllers;
using PCLoan.Logic.Library.Services;
using System.Text;
using System.Threading.Tasks;

namespace PCLoan.Presentation.Web
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
            services.AddControllersWithViews();
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<ILoginController, LdapLoginController>();
            services.AddScoped<IAdminController, AdminController>();
            services.AddScoped<IComputerController, ComputerController>();
            services.AddScoped<ILogController, LogController>();

            services.AddScoped<IAuthenticationService, LdapAuthenticationService>();
            services.AddScoped<IAuthorizationService, LdapAuthorizationService>();
            services.AddScoped<IJsonWebTokenService, JsonWebTokenService>();

            services.AddScoped<IComputerRepository, ComputerRepository>();
            services.AddScoped<ILoanRepository, LoanRepository>();
            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<IStateRepository, StateRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ILoggingService, DbLoggingService>();

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

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["Auth"];
                            return Task.CompletedTask;
                        },
                    };
                });
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=login}/{action=login}/{id?}");
            });
        }
    }
}
