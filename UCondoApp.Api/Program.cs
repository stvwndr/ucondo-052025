using UCondoApp.Application.Extensions;
using UCondoApp.Infra.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfraData(builder.Configuration);
builder.Services.AddApplication();


builder.Services.AddControllers();
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

app.Run();
