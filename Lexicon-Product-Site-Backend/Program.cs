using Lexicon_Product_Site_Backend;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:5173",
                              "https://localhost:5173",
                              "http://localhost:7123",
                              "https://localhost:7123")
                              .AllowAnyHeader()
                              .AllowAnyMethod()
                              .AllowCredentials();
                      });
});

builder.Services.AddDbContext<PSiteDB>(options =>
    options.UseSqlite("DataSource=PSite.db"));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();



app.Run();
