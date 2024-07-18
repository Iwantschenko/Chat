using BLL.Hubs;
using BLL.Services;
using DAL.DB;
using DAL.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            #region Services 

            builder.Services.AddDbContext<ChatDbContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(ChatDbContext)))
                );
            builder.Services.AddScoped<IRepository<User>, BaseRepository<User>>();
            builder.Services.AddScoped<IRepository<Chat>, BaseRepository<Chat>>();
            builder.Services.AddScoped<ChatRepository>();
            builder.Services.AddScoped<UserRepository>();

            builder.Services.AddScoped<UsersService>();
            builder.Services.AddScoped<ChatsService>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSignalR();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

            #endregion


            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseCors("AllowAllOrigins");
            app.MapHub<ChatHub>("/chat");
            app.MapControllers();

            app.Run();
        }
    }
}
