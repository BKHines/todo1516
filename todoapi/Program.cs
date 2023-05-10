using todoapi.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("TODOCORS", builder => builder.WithOrigins("http://localhost:4215", "http://localhost:4216").AllowAnyHeader().AllowAnyMethod());
});

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

var logger = new LoggerFactory().CreateLogger<TodoCore>();
TodoContext.ApplicationTodoCore = new TodoCore(logger);

app.Run();
