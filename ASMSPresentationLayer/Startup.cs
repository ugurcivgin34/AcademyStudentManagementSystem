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
using ASMSDataAccessLayer.�mplementationsDAL;

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
            //Aspnet Core'un ConnectionString ba�lant�s� yapabilmesi i�in yap�land�rma servislerine dbcontext nesnesini eklemesi gerekir.
            services.AddDbContext<MyContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("SqlConnection")));//Mycontext a�a��da unitofwork da da yapm��t�k burda da �ak��ma durumu oldu�u i�in bunu �nlemek ama�l� ServiceLifeTime komutunu kulland�k
            services.AddControllersWithViews();

            services.AddRazorPages(); //Razo sayfalar� i�in
            services.AddMvc();
            services.AddSession(options => options.IdleTimeout = TimeSpan.FromSeconds(20)); //oturum zaman�

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
            app.UseStaticFiles(); // wwwroot klas�r�n�n eri�imi i�indir.
            app.UseRouting(); // Controller/Action/Id
            app.UseSession(); // Oturum mekanizmas�n�n kullan�lmas� i�in
            app.UseAuthentication(); // Login Logout i�lemlerinin gerektirti�i oturum i�leyi�lerini kullanabilmek i�in.
            app.UseAuthorization(); // [Authorize] attribute i�in (yetki)
            //MVC ile ayn� kod blo�u endpoint'in mekanizmas�n�n nas�l olaca�� belirleniyor

            //rolleri olu�turacak static metot �a�r�ld�
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
