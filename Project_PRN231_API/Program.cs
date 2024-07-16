using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Project_PRN231_API.AutoMapper;
using Project_PRN231_API.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(
    ).AddOData(options => options
       .Select()
       .Filter()
       .OrderBy()
       .Count()
       );

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(ApplicationMapper));

builder.Services.AddDbContext<FarmManagement_PRN231Context>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DB"));
});
var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
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