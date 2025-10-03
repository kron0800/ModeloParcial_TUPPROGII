
using Microsoft.EntityFrameworkCore;
using TwitterCloneApi.Models;
using TwitterCloneApi.Repositories;
using TwitterCloneApi.Services;

namespace TwitterCloneApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Inyect Context
            builder.Services.AddDbContext<TwitterCloneContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            );

            // Inyect services
            builder.Services.AddScoped<ITweetRepository, TweetRepository>();
            builder.Services.AddScoped<ITweetService, TweetService>();

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
