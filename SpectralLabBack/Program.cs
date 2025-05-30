using Microsoft.EntityFrameworkCore;
using SpectralLabBack.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<SpectralLabDbContext>(options =>
	options.UseSqlServer("Server=User-PC\\SQLEXPRESS; Database=SpectralLab; Integrated Security = True;" +
		" Encrypt = False; Trusted_Connection=True;"));

builder.Services.RegistrateServices();
//builder.Services.AddTransient<DbSparesRepository>();
//builder.Services.AddTransient<DbProposalsRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder => builder.WithOrigins("http://localhost:5173").AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
