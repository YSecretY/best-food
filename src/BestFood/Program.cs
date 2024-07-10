using BestFood.Extensions;
using BestFood.Global.Exceptions;
using BestFood.Modules.Shared.Application;
using BestFood.Modules.Shared.Infrastructure;
using BestFood.Modules.Shared.Presentation;
using BestFood.Modules.Users.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

#region Exceptions

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

#endregion

#region Shared

builder.Services.AddSharedInfrastructure([UsersModule.ConfigureConsumers]);
builder.Services.AddSharedApplication([BestFood.Modules.Users.Application.AssemblyReference.Assembly]);
builder.Services.AddSharedPresentation();

#endregion

#region Modules

builder.Services.AddUsersModule(builder.Configuration);

#endregion

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.ApplyMigrations();
}

app.UseHttpsRedirection();

app.UseExceptionHandler();

app.MapControllers();

app.Run();