using CarPool.Interface;
using CarPool.Models;
using CarPool.Models.DBModels;
using CarPool.Services;
using Microsoft.EntityFrameworkCore;

var policyName = "_myAllowSpecificOrigin";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: policyName, builder =>
    {
        builder.AllowAnyOrigin().WithMethods("GET", "POST").AllowAnyHeader();

    });
});

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
/*builder.Services.AddSwaggerGen();*/

builder.Services.AddScoped<ISignUpSupport, SignUpSupport>();
builder.Services.AddScoped<IDataBaseService, DataBaseService>();
builder.Services.AddScoped<IValidator, Validator>();
builder.Services.AddScoped<ILogInSupport, LogInSupport>();
builder.Services.AddScoped<ICarpoolOfferService,CarpoolOfferService>();
builder.Services.AddScoped<IBookARideService, BookARideService>();
builder.Services.AddScoped<IMyRideSupport, MyRidesSupport>();

/*
builder.Services.AddDbContext<CarPoolDBContext>(opt =>
                                               opt.UseInMemoryDatabase("CarPoolDB"));
*/

/*Console.WriteLine("...");*/

builder.Services.AddDbContext<CarPoolDBContext>(op => op.UseSqlServer(builder.Configuration.GetValue<string>("connectionString")));



var app = builder.Build();

// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}*/

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(policyName);

app.Run();
