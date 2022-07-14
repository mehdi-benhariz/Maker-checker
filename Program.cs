using maker_checker_v1;
using maker_checker_v1.data;
using maker_checker_v1.Middleware;
using maker_checker_v1.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/makerchecker.txt", rollingInterval: RollingInterval.Day).CreateLogger();
var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();
// builder.WebHost.ConfigureKestrel(options =>
// {
//     // Setup a HTTP/2 endpoint without TLS.
//     options.ListenLocalhost(5000, o => o.Protocols = HttpProtocols.Http2);
// });
// Add services to the container.

builder.Services.AddControllers(
    options =>
{
    options.Filters.Add<HttpResponseExceptionFilter>();
}
).AddNewtonsoftJson(
    options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
   {
       c.OrderActionsBy(c => c.HttpMethod);
   });
builder.Services.AddDbContext<RequestContext>(options =>
{
    options.UseSqlite(builder.Configuration["ConnectionStrings:MakerCheckerDBConnectionString"]);
});

builder.Services.AddScoped<ServiceTypeRepository>();
builder.Services.AddScoped<RequestRepository>();
builder.Services.AddScoped<ValidationRepository>();
builder.Services.AddScoped<RuleRepository>();
builder.Services.AddScoped<RoleRepository>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddTransient<UnitOfWork>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = "/login";

})
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.SlidingExpiration = true;
    }).AddCertificate();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddCors(options =>
{
    //allow any header

    options.AddPolicy("AllowAll",
        policy => policy.WithOrigins("http://localhost:3000")
        .WithHeaders(
            HeaderNames.ContentType,
            HeaderNames.Authorization,
            HeaderNames.Accept,
            HeaderNames.Authorization
        )
        .AllowCredentials()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .WithExposedHeaders("X-Paggination"));

});

builder.Services.AddAuthorization(options =>
{
    PolicyConfiguration.setPolicies(options);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Maker Checker API V1");

    });

}
app.UseExceptionHandler(new ExceptionHandlerOptions()
{
    AllowStatusCode404Response = true, // important!
    ExceptionHandlingPath = "/error"
});


app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowAll");
app.UseCookiePolicy();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGet("/", (context) => context.Response.WriteAsync("Hello Mehdi!"));

app.Run();
