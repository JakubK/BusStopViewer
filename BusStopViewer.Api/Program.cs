using BusStopViewer.Api.Data;
using BusStopViewer.Api.Filters;
using BusStopViewer.Api.Options;
using BusStopViewer.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => options.AddPolicy("CorsPolicy",
    builder =>
    {
        builder.AllowAnyHeader()
            .AllowAnyMethod()
            .SetIsOriginAllowed((host) => true)
            .AllowCredentials();
    }));

// Add services to the container.
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("jwt"));

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IStopService, StopService>();

builder.Services.AddDbContext<AppDbContext>(options => {
    options.UseMySql(builder.Configuration.GetConnectionString("DB_CONNECTION_STRING"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DB_CONNECTION_STRING")));
});
builder.Services.AddSingleton<IJwtService, JwtService>();

builder.Services.AddHttpClient<ITristarClient, TristarClient>(options =>
{
    options.BaseAddress = new Uri("https://ckan.multimediagdansk.pl");
    options.Timeout = new TimeSpan(0, 0, 30);
    options.DefaultRequestHeaders.Clear();
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddMemoryCache();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
}

app.UseCors("CorsPolicy");

app.UseMiddleware<JwtMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
