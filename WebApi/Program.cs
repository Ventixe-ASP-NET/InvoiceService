using Microsoft.EntityFrameworkCore;
using WebApi.Data.Contexts;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<DataContext>(x =>
    x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//ConnectionString is not yet defined.

var app = builder.Build();
app.MapOpenApi();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseCors("AllowAll"); // Should be added roles to this?

app.Run();
