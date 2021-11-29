using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Net.WebSockets;
using System.Net;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace backend
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

            services.AddControllers();
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            
          
           services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)    
                    .AddJwtBearer(options =>    
                    {    
                        options.TokenValidationParameters = new TokenValidationParameters    
                        {    
                            ValidateIssuer = true,    
                            ValidateAudience = true,    
                            ValidateLifetime = false,    
                            ValidateIssuerSigningKey = true,    
                            ValidIssuer = Configuration["Jwt:Issuer"],    
                            ValidAudience = Configuration["Jwt:Issuer"],    
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))    
                        };    
                    });  
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "backend", Version = "v1" });
            });
            services.AddCors(options =>options.AddPolicy("*",
                builder=>builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
                             
            ));
            IServiceCollection serviceCollections = services.AddTransient<DAL.Helper.IDatabaseHelper,DAL.Helper.DatabaseHelper>();
            services.AddTransient< DAL.Interface.iUserDAl , DAL.userDAl>();
            services.AddTransient<BUS.Interface.IuserBUS, BUS.UserBUS>();
            services.AddTransient< DAL.Interface.iCategoryDAL , DAL.categoryDAL>();
            services.AddTransient<BUS.Interface.iCategoryBUS, BUS.CategoryBUS>();
            services.AddTransient< DAL.Interface.iSystemDAL , DAL.systemDAL>();
            services.AddTransient<BUS.Interface.Isystem, BUS.systemBUS>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "backend v1"));
            }

            app.UseHttpsRedirection();
            // app.UseStaticFiles();// For the wwwroot folder

            app.UseStaticFiles(new StaticFileOptions
                    {
                        FileProvider = new PhysicalFileProvider(
                            Path.Combine(Directory.GetCurrentDirectory(), "Resources")),
                        RequestPath = "/Resources"
                     });
            // app.UseDirectoryBrowser(new DirectoryBrowserOptions
            //         {
            //             FileProvider = new PhysicalFileProvider(
            //                 Path.Combine(Directory.GetCurrentDirectory(), "Resources")),
            //             RequestPath = "/Resources"
            //         });
           
            app.UseCors("*");
         
           
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
