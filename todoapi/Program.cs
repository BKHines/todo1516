var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var _cors = builder.Configuration.GetSection("TODOCORS").Get<string[]>();

if (_cors != null && _cors.Length > 0)
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("TODOCORS", builder => builder.WithOrigins("http://localhost:4015", "http://localhost:4016").AllowAnyHeader().AllowAnyMethod());
    });
}

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("TODOCORS");
app.MapControllers();

app.Run();
