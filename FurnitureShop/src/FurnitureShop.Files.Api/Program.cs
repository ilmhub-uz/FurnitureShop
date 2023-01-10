using FurnitureShop.Common.Extensions;
using FurnitureShop.Files.Api.Services;
using JFA.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddServicesFromAttribute();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAppDbContext(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
