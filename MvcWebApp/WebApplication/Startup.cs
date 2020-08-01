using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WebApplication.Dal;

namespace WebApplication
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
            //var optionsBuilder = new DbContextOptionsBuilder<PatientDal>();
            //optionsBuilder.UseSqlServer(Configuration["ConnStr"].ToString());
            // PatientDal dal = new PatientDal(Configuration["ConnStr"].ToString());

            //Object created foe efCore tool
            services.AddDbContext<PatientDal>(
                    options => options.UseSqlServer(Configuration["ConnStr"].ToString()));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
             {
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = "mytoken",
                     ValidAudience = "mytoken",
                     IssuerSigningKey = new
                 SymmetricSecurityKey(Encoding.UTF8.GetBytes("245317896557853298012654"))
                 };

             });

            services.AddSession(options =>
      {
          options.Cookie.Name = ".MyCookie";
          options.IdleTimeout = TimeSpan.FromSeconds(10);
          options.Cookie.IsEssential = true;
      });

            services.AddControllers().AddNewtonsoftJson(Options => {
                Options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });
            services.AddControllersWithViews();
            services.AddCors(o => o.AddPolicy("AllowOriginRule", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();

            }));
            // services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            //services.AddMvc(Option =>.EnableEndpointRouting = false).AddNewtonsoftJson

            services.AddControllersWithViews().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseAuthentication();
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
            app.UseCookiePolicy();

            app.UseRouting();
            app.UseCors("AllowOriginRule");
            app.UseAuthorization();
            app.UseSession();
          
            app.UseEndpoints(endpoints =>
             {
                 endpoints.MapControllerRoute(
                         name: "default",
                         pattern: "{controller=Home}/{action=Add}");
             });
           // app.UseMvc(routes =>
           // { 
           // routes.MapRoute(
           //     name: "default",
           //     template: "{controller= Patient}/{action = Add}");
           //  });
        }
    }
}
