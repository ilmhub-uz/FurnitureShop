using FurnitureShop.Common.Extensions;
using JFA.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.GlobalAppSettings();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddServicesFromAttribute();
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