using Microsoft.Extensions.Configuration;
using MISA.AMIS.KeToan.BL;
using MISA.AMIS.KeToan.DL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Thay đổi format tên trường của json trả về từ camelCase sang PascalCase
builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency injection
builder.Services.AddScoped(typeof(IBaseBL<>), typeof(BaseBL<>)); 
builder.Services.AddScoped(typeof(IBaseDL<>), typeof(BaseDL<>)); 
builder.Services.AddSingleton<IEmployeeBL, EmployeeBL>();
builder.Services.AddSingleton<IEmployeeDL, EmployeeDL>();

//Thêm file json chứa cấu hình từ bên ngoài project
builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    // Lấy các thuộc tính của thư mục cha của project
    var parentDir = Directory.GetParent(hostingContext.HostingEnvironment.ContentRootPath);
    // Lấy đường dẫn đến file json
    var path = string.Concat(parentDir?.Parent?.FullName, "\\connectionStrings.json");
    // Thêm file json vào configuration
    config.AddJsonFile(path, optional: true, reloadOnChange: true);
});

// Lấy dữ liệu connection string từ file appsettings.Development.json
DatabaseContext.ConnectionString = builder.Configuration.GetConnectionString("Mysql");

// Allow Origins
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("*");
                      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
