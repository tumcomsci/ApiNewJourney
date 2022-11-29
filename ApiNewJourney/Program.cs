using ApiNewJourney.BusinessLayer.IService;
using ApiNewJourney.BusinessLayer.Service;
using ApiNewJourney.DataLayer.IDL;
using ApiNewJourney.DataLayer.DL;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowDefaultOrigins",
        builder =>
        {
            builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
IServiceCollection services = builder.Services;
// Services
services.AddTransient<IAccountService, AccountService>();
services.AddTransient<IAccountBalanceService,AccountBalanceService>();
services.AddTransient<IPackageService, PackageService>();   
services.AddTransient<ITransactionService, TransactionService>();

// Data Layers
services.AddTransient<IAccountDL,AccountDL>();
services.AddTransient<IAccountBalanceDL,AccountBalanceDL>();
services.AddTransient<IPackageDL,PackageDL>();  
services.AddTransient<ITransactionDL,TransactionDL>();

services.AddTransient<IDapperDL, DapperDL>();

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();


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
