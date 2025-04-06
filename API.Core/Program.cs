using API.Domain.IDALs;
using API.Domain.IRepositories;
using API.Infrastructure.DALs;
using API.Infrastructure.Repositories;
using API.Shared.Helpers;
using NLog;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Load configuration files
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                      .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

// 🔹 Get root directory dynamically
string currentDirectory = Directory.GetCurrentDirectory();
string? rootDirectory = currentDirectory;

while (rootDirectory != null && !rootDirectory.EndsWith("BookHiveAPI"))
{
    rootDirectory = Directory.GetParent(rootDirectory)?.FullName;
}

// 🔹 Resolve file paths for configuration files
string nlogConfigPath = builder.Configuration["NLogConfigPath"]!;
string dbQueryJsonPath = builder.Configuration["APIDbQueryJsonPath"]!;

string nlogFullPath = Path.Combine(rootDirectory ?? throw new InvalidOperationException("Root directory not found"), nlogConfigPath);
string dbQueryFullPath = Path.Combine(rootDirectory ?? throw new InvalidOperationException("Root directory not found"), dbQueryJsonPath);

// 🔹 Setup Logger
var logger = LogManager.Setup().LoadConfigurationFromFile(nlogFullPath).GetCurrentClassLogger();
logger.Info("Application Initiated");

// 🔹 Register services
builder.Services.AddControllers();  // ✅ Ensures API controllers are registered
builder.Services.AddSingleton(new QueryHelper(dbQueryFullPath));
builder.Services.AddScoped<DbConnectionHelper>();
builder.Services.AddScoped<IInventoryRepo, InventoryRepo>();
builder.Services.AddScoped<IInventoryDAL, InventoryDAL>();

builder.Services.AddScoped<ICartRepo, CartRepo>();
builder.Services.AddScoped<ICartDAL, CartDAL>();
builder.Services.AddSingleton<ApiResponseHelper>();

// 🔹 Enable Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 🔹 Configure Kestrel
builder.WebHost.ConfigureKestrel(options =>
{
    options.Configure(builder.Configuration.GetSection("Kestrel"));
});

var app = builder.Build();

Console.WriteLine($"Running in environment: {app.Environment.EnvironmentName}");

// 🔹 Enable Swagger UI if configured
if (builder.Configuration.GetValue<bool>("EnableSwagger"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();  // ✅ Ensures that controllers (including InventoryController) are mapped

app.Run();
