using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);


//add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.WithOrigins("http://localhost:3000")
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
builder.Services.AddRateLimiter(rlopt =>
{
    rlopt.AddFixedWindowLimiter("fixed", options =>
    {
        options.Window = TimeSpan.FromSeconds(10);
        options.PermitLimit = 5;
    });

});
var app = builder.Build();

app.UseCors("AllowAll");
app.UseRateLimiter();
app.MapReverseProxy();
app.Run();
