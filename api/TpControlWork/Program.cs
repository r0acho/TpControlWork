using TpControlWork.DataAccess;
using Microsoft.EntityFrameworkCore;
using TpControlWork.DataAccess.Interfaces;
using TpControlWork.DataAccess.Implementations;
using TpControlWork.Services.Interfaces;
using TpControlWork.Services.Implementations;

namespace TpControlWork;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        builder.Configuration.AddJsonFile("config.json");

        var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: MyAllowSpecificOrigins,
                              builder =>
                              {
                                  builder.WithOrigins("*").AllowAnyMethod()
                                                                              .AllowAnyHeader()
                                                                              .WithExposedHeaders("Location");
                              });
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(builder.Configuration["PostgreSQLConnection"], b => b.MigrationsAssembly("TpControlWork")));

        builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        builder.Services.AddScoped<IEmployeeService, EmployeeService>();
        builder.Services.AddScoped<IEmployeeDomainToDataAccessAdapterService, EmployeeDomainToDataAccessAdapterService>();
        builder.Services.AddScoped<IEmployeeDataAccessToDomainAdapterService, EmployeeDataAccessToDomainAdapterService>();
        builder.Services.AddScoped<IStatisticsCalculatorService, StatisticsCalculatorService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseCors(MyAllowSpecificOrigins);

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}