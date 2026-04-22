using Microsoft.EntityFrameworkCore;
using Cooperative.Data;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddOpenApi();


builder.Services.AddDbContext<CooperativeDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

//Next steps
//Use data annotation for input validation ✅
//Convert to async programming ✅
//JWT and authorization with identity and microsoft entra
//Assign Roles and perform operations based on those roles
//Figure out the logic for cooperators
//Use StaffNumber as primary key
