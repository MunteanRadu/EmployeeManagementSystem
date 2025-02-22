using EmployeeManagementSystem.Configurations;
using EmployeeManagementSystem.Services.Employees;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Components.Web;
using System.ComponentModel;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAllOrigins", builder =>
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
    });

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Employee Management System API",
            Version = "v1",
            Description = "API for managing employees",
        });
    });

    var mongoDbConfig = builder.Configuration.GetSection("MongoDatabase");
    var connectionString = mongoDbConfig.GetValue<string>("ConnectionString");
    var databaseName = mongoDbConfig.GetValue<string>("DatabaseName");

    builder.Services.AddScoped<IEmployeeService>(sp => 
        new EmployeeService(connectionString, databaseName));
}

var app = builder.Build();
{
    app.UseCors("AllowAllOrigins");
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Employee Management System API v1");
        c.RoutePrefix = string.Empty;
    });
    // app.UseExceptionHandler("/error");
    // app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}
