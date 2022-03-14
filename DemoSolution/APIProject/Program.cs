using APIProject.Model;
using APIProject.Repository;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// This method gets called by the runtime. Use this method to add services to the container.
//public void ConfigureServices(IServiceCollection services)
//{
//    var server = Configuration["DbServer"] ?? "localhost";
//    var port = Configuration["DbPort"] ?? "1433"; // Default SQL Server port
//    var user = Configuration["DbUser"] ?? "SA"; // Warning do not use the SA account
//    var password = Configuration["Password"] ?? "Youtube2021";
//    var database = Configuration["Database"] ?? "bookDb";

//    // Add Db context as a service to our application
//    services.AddDbContext<ApplicationDbContext>(options =>
//        options.UseSqlServer($"Server={server}, {port};Initial Catalog={database};User ID={user};Password={password}"));

//    services.AddControllersWithViews();
//}

// Add services to the container.
builder.Services.AddDbContext<EmployeeDBContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.PropertyNamingPolicy = null;
}).AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
).AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver
                = new DefaultContractResolver()).AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

APIProject.Services.DatabaseManagementService.MigrationInitialisation(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
