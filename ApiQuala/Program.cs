using ApiQuala.Business.Contract;
using ApiQuala.Business.Implement;
using ApiQuala.DataAccess.Contract;
using ApiQuala.DataAccess.Implement;
using ApiQuala.Entities.Class.Dto;
using ApiQuala.Entities.Class.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApiDBContext>(option => option.UseSqlServer(connectionString));
ApiDBContext.ConfiguracionApiDDB.cadenaConexion = connectionString;

builder.Services.AddSingleton<IBUsers, BUsers>();
builder.Services.AddSingleton<IBMonedas, BMonedas>();
builder.Services.AddSingleton<IBSucursal, BSucursal>();
builder.Services.AddSingleton<ISucursales, Sucursales>();
builder.Services.AddSingleton<IUsers, Users>();
builder.Services.AddSingleton<IMonedas, Monedas>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("_corsApp",
                      policy =>
                      {
                          policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("_corsApp");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
