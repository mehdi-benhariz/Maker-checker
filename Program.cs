using maker_checker_v1.data;
using maker_checker_v1.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/makerchecker.txt", rollingInterval: RollingInterval.Day).CreateLogger();
var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(
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
builder.Services.AddScoped<RoleRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapGet("/", (context) => context.Response.WriteAsync("Hello Mehdi!"));

app.Run();
