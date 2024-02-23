using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using WorkSharp.FileSystem;
using WorkSharp.Storage;
using WorkShop.SQL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(TodoProfile));

// Add services to the container
builder.Services.AddSingleton<ITodoService, TodoService>();

// Add repositories to the container
// builder.Services.AddSingleton<ITodoRepository, TodoFileRepository>();
builder.Services.AddSingleton<ITodoRepository, TodoSQLRepository>();

// Add config to the container
builder.Services.AddSingleton<IConfig, Config>();

// Add authentication to the container
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var JwtIssuer = "https://accounts.google.com";
        var JwtAudience = "289832142171-4pgrkds4cptbdrslrppgoontkss38ejt.apps.googleusercontent.com";

        options.Authority = JwtIssuer;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = JwtIssuer,
            ValidAudience = JwtAudience,
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
