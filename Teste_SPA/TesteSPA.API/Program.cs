using System.Text.Json.Serialization;
using TesteSPA.API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(x =>
   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("AppDb") ?? "DataSource=teste.db;Cache=Shared"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowWebApp",
            builder=> {
                builder.WithOrigins("*")
                .AllowAnyHeader()
                .AllowAnyMethod();
            }
        );
    }
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowWebApp");
app.UseAuthorization();
app.MapControllers();
app.Run();
