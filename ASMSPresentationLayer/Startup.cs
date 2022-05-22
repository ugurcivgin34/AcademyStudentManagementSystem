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
using ASMSDataAccessLayer.İmplementationsDAL;

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
            //Aspnet Core'un ConnectionString bağlantısı yapabilmesi için yapılandırma servislerine dbcontext nesnesini eklemesi gerekir.
            services.AddDbContext<MyContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("SqlConnection")));//Mycontext aşağıda unitofwork da da yapmıştık burda da çakışma durumu olduğu için bunu önlemek amaçlı ServiceLifeTime komutunu kullandık
            services.AddControllersWithViews()
             .AddRazorRuntimeCompilation();//Proje çalışırken razor sayfalarında yapılan değişiklikler anında sayfaya yansıması için eklendi

            services.AddRazorPages(); //Razo sayfaları için
            services.AddMvc();
            services.AddSession(options => options.IdleTimeout = TimeSpan.FromSeconds(20)); //oturum zamanı

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
            services.AddScoped<ICityBusinessEngine, CityBusinessEngine>();
            services.AddScoped<IDistrictBusinessEngine, DistrictBusinessEngine>();
            services.AddScoped<INeighbourhoodBusinessEngine, NeighbourhoodBusinessEngine>();
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
            app.UseStaticFiles(); // wwwroot klasörünün erişimi içindir.
            app.UseRouting(); // Controller/Action/Id
            app.UseSession(); // Oturum mekanizmasının kullanılması için
            app.UseAuthentication(); // Login Logout işlemlerinin gerektirtiği oturum işleyişlerini kullanabilmek için.
            app.UseAuthorization(); // [Authorize] attribute için (yetki)
            //MVC ile aynı kod bloğu endpoint'in mekanizmasının nasıl olacağı belirleniyor

            //rolleri oluşturacak static metot çağrıldı
            CreateDefaultData.CreateData.Create(roleManager);

            //MVC ile ayný kod bloðu endpoint'in mekanizmasýnýn nasýl olacaðý belirleniyor
            app.UseEndpoints(endpoints =>
            {

                endpoints.MapAreaControllerRoute(
                 "management",
                 "management",
                 "management/{controller=Admin}/{action=Login}/{id?}"
                 );
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            });
        }
    }
}   

