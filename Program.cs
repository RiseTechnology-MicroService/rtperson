using rtperson.DatabaseModels;
using rtperson.DatabaseModels.Settings;
using rtperson.Repositories;
using rtperson.Services;
using rtperson.Services.ValidationServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("Database"));

//Person
builder.Services.AddScoped<IValidationService<Person>, PersonValidationService>();
builder.Services.AddScoped<IGenericRepository<Person>, PersonRepository>();
builder.Services.AddScoped<PersonsService>();

//Log
builder.Services.AddScoped<ILogService, LogService>();
builder.Services.AddScoped<LogRequestService>();

builder.Services.AddSwaggerGen();


builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
