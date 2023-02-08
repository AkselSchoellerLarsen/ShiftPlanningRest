using ShiftPlanningRest;
using ShiftPlanningRest.Controllers;
using ShiftPlanningRest.Managers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IShiftManager, ShiftManager>();
builder.Services.AddSingleton<IUserManager, UserManager>();
builder.Services.AddScoped<IUserController, UserController>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

DatabaseHelper.ShiftPlanningDatabase = app.Configuration.GetConnectionString("ShiftPlanningDatabase");

app.Run();
