using Employee.Data.Data;
using Microsoft.EntityFrameworkCore;
//using Repository.Design


//1using Serilog;


var builder = WebApplication.CreateBuilder(args);




//2 Serilog configuration
//builder.Host.UseSerilog((context, configuration) =>
//{
//    configuration
//        .ReadFrom.Configuration(context.Configuration)
//        .Enrich.FromLogContext()
//        .WriteTo.Console()
//        .WriteTo.File(
//            "Logs/app-.log",
//            rollingInterval: RollingInterval.Day);
//});



// MVC
//builder.Services.AddControllersWithViews();
// Blazor
//builder.Services.AddRazorComponents()
//    .AddInteractiveServerComponents();


builder.Services.AddControllersWithViews();

//builder.Services.AddControllers();

builder.Services.AddDbContext<EmployeeDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(
            builder.Configuration.GetConnectionString("DefaultConnection")
        )
    ));
//builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//3 Serilog HTTP request logging
//app.UseSerilogRequestLogging();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//app.MapRazorComponents<App>()
//    .AddInteractiveServerRenderMode();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Employee}/{action=Index}/{id?}");

app.Run();