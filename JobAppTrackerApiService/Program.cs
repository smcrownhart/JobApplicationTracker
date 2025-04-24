using JobApplicationTracker.DataAccess.Data;
using JobApplicationTracker.DataAccess.Models;
using JobApplicationTracker.DataAccess.Repositories;
using JobApplicationTracker.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace JobAppTrackerApiService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<JobAppDbContext>(
                options => options.UseInMemoryDatabase("JobAppDbContext"));

            builder.Services.AddScoped(typeof(IJobAppTracker<>), typeof(JobAppTrackerRepository<>));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI();

            

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
