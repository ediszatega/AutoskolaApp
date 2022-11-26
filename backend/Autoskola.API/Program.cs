global using Autoskola.Repository.Data;
using Autoskola.Repository;
using Autoskola.Repository.Interfaces;
using Autoskola.Repository.Repositories;
using Autoskola.Service.Interfaces;
using Autoskola.Service.Services;
using Microsoft.EntityFrameworkCore;
using Autoskola.Core.Profiles;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
// Add services to the container.

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddAutoMapper(typeof(Program));
services.AddDbContext<ApplicationContext>(
        options => options.UseLazyLoadingProxies()
        .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    );

services.AddScoped(typeof(IRepository<,>), typeof(BaseRepository<,>));
services.AddScoped<ICityRepository, CityRepository>();
services.AddScoped<ICityService, CityService>();
services.AddScoped<IUserRepository, UserRepository>();
services.AddScoped<IUserService, UserService>();

var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new CityProfile());
    mc.AddProfile(new UserProfile());
});
IMapper mapper = mappingConfig.CreateMapper();
services.AddSingleton(mapper);
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
