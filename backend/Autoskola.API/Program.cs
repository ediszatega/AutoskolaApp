global using Autoskola.Repository.Data;
using Autoskola.Repository;
using Autoskola.Repository.Interfaces;
using Autoskola.Repository.Repositories;
using Autoskola.Service.Interfaces;
using Autoskola.Service.Services;
using Microsoft.EntityFrameworkCore;
using Autoskola.Core.Profiles;
using AutoMapper;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
// Add services to the container.

services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200",
                                              "https://localhost:4200",
                                              "localhost:4200").WithHeaders("*").WithMethods("*");
                      });
});

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

var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new CityProfile());
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

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
