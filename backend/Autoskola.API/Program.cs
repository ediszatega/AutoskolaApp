global using Autoskola.Repository.Data;
global using Autoskola.Core.Models.ExceptionHandling;
using Autoskola.Repository;
using Autoskola.Repository.Interfaces;
using Autoskola.Repository.Repositories;
using Autoskola.Service.Interfaces;
using Autoskola.Service.Services;
using Microsoft.EntityFrameworkCore;
using Autoskola.Core.Profiles;
using AutoMapper;
using Autoskola.API.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});
var services = builder.Services;
// Add services to the container.

services.AddControllers();
services.AddLogging();
services.AddCors(options =>
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
services.AddDbContext<AutoskolaContext>(
        options => options
        .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    );

services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
services.AddScoped<ICityRepository, CityRepository>();
services.AddScoped<IUserRepository, UserRepository>();
services.AddScoped<ICategoryRepository, CategoryRepository>();
services.AddScoped<ITestRepository, TestRepository>();
services.AddScoped<IQuestionRepository, QuestionRepository>();
services.AddScoped<IAnswerRepository, AnswerRepository>();
services.AddScoped<IVehicleRepository, VehiclesRepository>();
services.AddScoped<IMotTestRepository, MotTestRepository>();
services.AddScoped<ICustomerRepository, CustomerRepository>();
services.AddScoped<IEmployeeRepository, EmployeeRepository>();
services.AddScoped<IInstructorRepository, InstructorRepository>();
services.AddScoped<ILecturerRepository, LecturerRepository>();
services.AddScoped<IPaymentRepository, PaymentRepository>();
services.AddScoped<INewsRepository, NewsRepository>();
services.AddScoped<IReviewRepository, ReviewRepository>();

services.AddScoped<IUnitOfWork, UnitOfWork>();
services.AddScoped<ICityService, CityService>();
services.AddScoped<IUserService, UserService>();
services.AddScoped<ICategoryService, CategoryService>();
services.AddScoped<ITestService, TestService>();
services.AddScoped<IQuestionService, QuestionService>();
services.AddScoped<IAnswerService, AnswerService>();
services.AddScoped<IVehicleService, VehicleService>();
services.AddScoped<IMotTestService, MotTestService>();
services.AddScoped<ICustomerService, CustomerService>();
services.AddScoped<IEmployeeService, EmployeeService>();
services.AddScoped<IInstructorService, InstructorService>();
services.AddScoped<ILecturerService, LecturerService>();
services.AddScoped<IPaymentService, PaymentService>();
services.AddScoped<INewsService, NewsService>();
services.AddScoped<IReviewService, ReviewService>();


var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new CityProfile());
    mc.AddProfile(new UserProfile());
    mc.AddProfile(new AnswerProfile());
    mc.AddProfile(new QuestionProfile());
    mc.AddProfile(new TestProfile());
    mc.AddProfile(new CustomerProfile());
    mc.AddProfile(new EmployeeProfile());
    mc.AddProfile(new InstructorProfile());
    mc.AddProfile(new LecturerProfile());
    mc.AddProfile(new PaymentProfile());
    mc.AddProfile(new NewsProfile());
    mc.AddProfile(new ReviewProfile());
});
IMapper mapper = mappingConfig.CreateMapper();
services.AddSingleton(mapper);

services.AddTransient<GlobalExceptionHandlingMiddleware>();

services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("jqoABlaO91oU3Bahxv")),
        ValidateAudience = false,
        ValidateIssuer = false
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
