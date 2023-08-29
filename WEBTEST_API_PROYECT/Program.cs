
using Microsoft.EntityFrameworkCore;
using WEBTEST_API_PROYECT.Data;
using WEBTEST_API_PROYECT.Repository;
using WEBTEST_API_PROYECT.Repository.IRepository;

namespace WEBTEST_API_PROYECT
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddNewtonsoftJson();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddDbContext<ApplicationDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DBstring"));
            });


            builder.Services.AddAutoMapper(typeof(MappingConfig));

            builder.Services.AddScoped<ITestWebRepository, TestWebRepository>();
            builder.Services.AddScoped<INumberTestWebRepository, NumberTestWebRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}