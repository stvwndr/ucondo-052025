using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using System.Text.Json.Serialization;
using UCondoApp.Api.Extensions;
using UCondoApp.Application.Extensions;
using UCondoApp.Infra.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfraData(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddControllers(options =>
    {
        options.ModelMetadataDetailsProviders.Add(new SystemTextJsonValidationMetadataProvider());
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.MigrateDatabase();

app.Run();
