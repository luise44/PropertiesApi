using Microsoft.OpenApi.Models;
using Properties.Services.Authentication.Extensions;
using Properties.Services.Configuration.Models;
using Properties.Data.Extensions;
using Properties.Services.Application.Extensions;
using Properties.Client.Api.Automapper;
using Properties.Services.Application.Automapper;

var builder = WebApplication.CreateBuilder(args);
var settings = builder.Configuration.GetSection("ApiSettings").Get<ApiSettings>();

// Add services to the container.
builder.Services.AddAutoMapper(typeof(ApiProfile), typeof(ServicesProfile));
builder.Services.AddControllers();
builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddJwtAuthentication(settings);
builder.Services.AddSwaggerGen(opt =>
{
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
builder.Services.AddContext(settings);
builder.Services.AddServicesAndDependencies();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
