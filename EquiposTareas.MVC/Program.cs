using EquiposProyectosApi.Consumer;
using ProyectoEquiposs;

namespace EquiposTareas.MVC
{
    public class Program
    {
        public static void Main(string[] args)

        {
            Crud<Usuario>.EndPoint = "https://localhost:7042/api/Usuarios";
            Crud<Proyecto>.EndPoint = "https://localhost:7042/api/Proyectos";
            Crud<Tarea>.EndPoint = "https://localhost:7042/api/Tareas";
            var builder = WebApplication.CreateBuilder(args);

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
