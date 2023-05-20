using MakeupWebShop.Repositories;
using MakeupWebShop.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.Extensions.Options;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
 {
     options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
     {
         In = ParameterLocation.Header,
         Name = "Authorization",
         Type = SecuritySchemeType.ApiKey
     });
     options.OperationFilter<SecurityRequirementsOperationFilter>();
 });



builder.Services.AddDbContext<MakeUpDbContext>();

builder.Services.AddScoped<IAdresaRepository, AdresaRepository>();
builder.Services.AddScoped<IBrendRepository, BrendRepository>();
builder.Services.AddScoped<IKolekcijaRepository, KolekcijaRepository>();
builder.Services.AddScoped<INamenaRepository, NamenaRepository>();
builder.Services.AddScoped<ITipRepository, TipRepository>();
builder.Services.AddScoped<IUlogaRepository, UlogaRepository>();
builder.Services.AddScoped<IProizvodRepository, ProizvodRepository>();
builder.Services.AddScoped<IKorisnikRepository, KorisnikRepository>();
builder.Services.AddScoped<IKorpaRepository, KorpaRepository>();
builder.Services.AddScoped<IProizvodUKorpiRepository, ProizvodUKorpiRepository>();
builder.Services.AddScoped<IRacunRepository, RacunRepository>();

 

builder.Services.AddControllers().AddJsonOptions(x =>
                 x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddAutoMapper(typeof(Program).Assembly);


builder.Services.AddCors(options =>
{
    options.AddPolicy("angularApplication", builder =>
    {
        builder.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
            /*.WithMethods("GET", "POST", "PUT", "DELETE")
            .WithExposedHeaders("*");*/
    });
});


var app = builder.Build();


app.UseCors("angularApplication");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
