using ASMSDataAccessLayer;
using ASMSEntityLayer.IdentityModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ASMSEntityLayer.Mappings;
using ASMSBusinessLayer.EmailService;
using ASMSBusinessLayer.ContractBLL;
using ASMSBusinessLayer.ImplementationsBLL;
using ASMSDataAccessLayer.ContractsDAL;
using ASMSDataAccessLayer.ÝmplementationsDAL;

namespace ASMSPresentationLayer
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
            //Aspnet Core'un ConnectionString baðlantýsý yapabilmesi için yapýlandýrma servislerine dbcontext nesnesini eklemesi gerekir.
            services.AddDbContext<MyContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("SqlConnection")));//Mycontext aþaðýda unitofwork da da yapmýþtýk burda da çakýþma durumu olduðu için bunu önlemek amaçlý ServiceLifeTime komutunu kullandýk
            services.AddControllersWithViews();

            services.AddRazorPages(); //Razo sayfalarý için
            services.AddMvc();
            services.AddSession(options => options.IdleTimeout = TimeSpan.FromSeconds(20)); //oturum zamaný

            //****************************************//
            services.AddIdentity<AppUser, AppRole>(options =>
             {
                 options.User.RequireUniqueEmail = true;
                 options.Password.RequiredLength = 3;
                 options.Password.RequireLowercase = false;
                 options.Password.RequireUppercase = false;
                 options.Password.RequireNonAlphanumeric = false;
                 options.Password.RequireDigit = false;
                 options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_@.";
             }).AddDefaultTokenProviders().AddEntityFrameworkStores<MyContext>();

            //Mapleme eklendi
            services.AddAutoMapper(typeof(Maps));


            services.AddSingleton<IEmailSender, EmailSender>();
            services.AddScoped<IStudentBusinessEngine, StudentBusinessEngine>();
            services.AddScoped<IUsersAddressBusinessEngine, UsersAddressBusinessEngine>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, RoleManager<AppRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles(); // wwwroot klasörünün eriþimi içindir.
            app.UseRouting(); // Controller/Action/Id
            app.UseSession(); // Oturum mekanizmasýnýn kullanýlmasý için
            app.UseAuthentication(); // Login Logout iþlemlerinin gerektirtiði oturum iþleyiþlerini kullanabilmek için.
            app.UseAuthorization(); // [Authorize] attribute için (yetki)
            //MVC ile ayný kod bloðu endpoint'in mekanizmasýnýn nasýl olacaðý belirleniyor

            //rolleri oluþturacak static metot çaðrýldý
            CreateDefaultData.CreateData.Create(roleManager);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
