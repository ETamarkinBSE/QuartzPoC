using Quartz;
using Quartz.Impl;
using QuartzPoC2.Entry;
using QuartzPoC2.Entry.Jobs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();
var settings = new QuartzSettings();
builder.Configuration.GetSection("Quartz").Bind(settings);
builder.Services.AddSingleton<IQuartzSettings>(settings);
builder.Services.AddInfrastructure(settings);

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
