using SmartTask.Core.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// 🔥 ADD THIS LINE (IMPORTANT)
builder.Services.AddSingleton<UserTaskManager>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();