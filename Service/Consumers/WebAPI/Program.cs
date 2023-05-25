using Application.ArticulatorApplication.Commands.Handlers;
using Application.ManagerApplication.Commands.Handlers;
using Application.UserApplication.Commands.Handlers;
using Application.Utils.ResponseBase;
using Data;
using Data.ArticulatorData;
using Data.CellData;
using Data.ManagerData;
using Data.UserData;
using Domain.ArticulatorDomain.Ports;
using Domain.CellDomain.Ports;
using Domain.ManagerDomain.Ports;
using Domain.UserDomain.Ports;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Add MediatR
#region
builder.Services.AddMediatR(typeof(CreateArticulatorCommandHandler));
builder.Services.AddMediatR(typeof(CreateManagerCommandHandler));
builder.Services.AddMediatR(typeof(UserAuthenticationCommandHandler));
builder.Services.AddMediatR(typeof(Response));
#endregion

// Add Connection Database
#region
var connectionString = builder.Configuration["ConnectionStrings:MySQLConnectionStringDocker"];
var optionsBuilder = new DbContextOptionsBuilder<PACCEConnectDbContext>();
optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 5)));
builder.Services.AddDbContext<PACCEConnectDbContext>(
    options => options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 5))));
#endregion

// Add Dependecy Injection
#region
builder.Services.AddScoped<IArticulatorRepository, ArticulatorRepository>();
builder.Services.AddScoped<IManagerRepository, ManagerRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICellRepository, CellRepository>();
#endregion

// Authentication Configuration
#region
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
    };
});
#endregion

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "PACCEConnect", Version = "v1" });

    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Token JWT using schema Bearer",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };

    options.AddSecurityDefinition("Bearer", securityScheme);
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            securityScheme, new string[] { }
        }
    });
});

builder.Services.AddControllersWithViews()
                .AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors(x => x
    .AllowAnyOrigin()
       .AllowAnyMethod()
          .AllowAnyHeader());

app.MapControllers();

app.Run();
