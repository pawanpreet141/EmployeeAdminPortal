using Employee.Data.Data;
using Employee.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
//using Repository.Design


var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<EmployeeDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(
            builder.Configuration.GetConnectionString("DefaultConnection")
        )
    ));

// password hasher
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Employee}/{action=Index}/{id?}");

app.Run();


//using Employee.Data.Data;
//using Microsoft.EntityFrameworkCore;

//var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddControllers();

//var connectionString =
//    builder.Configuration.GetConnectionString("DefaultConnection");

//builder.Services.AddDbContext<EmployeeDbContext>(options =>
//    options.UseMySql(
//        connectionString,
//        ServerVersion.AutoDetect(connectionString)
//    ));

//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowBlazorUI", policy =>
//    {
//        policy
//            .AllowAnyOrigin()
//            .AllowAnyHeader()
//            .AllowAnyMethod();
//    });
//});

//var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseCors("AllowBlazorUI");

//app.UseAuthorization();

//app.MapControllers();

//app.Run();
