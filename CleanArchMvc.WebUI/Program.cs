using CleanArchMvc.Domain.Account;
using CleanArchMvc.Infra.Data.Context;
using CleanArchMvc.Infra.Ioc;
using Microsoft.EntityFrameworkCore;

namespace CleanArchMvc.WebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configura��o da string de conex�o
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            // Registro do DbContext no cont�iner de servi�os
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Adiciona o servicos configurados em Dependecy Injection
            builder.Services.AddInfrastructure(builder.Configuration);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //using (var scope = app.Services.CreateScope())
            //{
            //    var services = scope.ServiceProvider;
            //    var seedUserRoleInitial = services.GetRequiredService<ISeedUserRoleInitial>();
            //    seedUserRoleInitial.SeedRoles();
            //    seedUserRoleInitial.SeedUsers();
            //}

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
