using FurnitureShop.EmailSender.API;
using FurnitureShop.EmailSender.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

//The code block below gets email configuration from appsettings.json file. So you need to go to that file and replace my configurations with yours.
var emailConfig = builder.Configuration
    .GetSection("EmailConfiguration")
    .Get<EmailConfiguration>();

builder.Services.AddSingleton(emailConfig);

builder.Services.AddScoped<IEmailSender, FurnitureShop.EmailSender.API.Services.EmailSender>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin();
    });
});

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