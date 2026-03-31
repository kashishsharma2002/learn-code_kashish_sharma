using FinanceTracker.Api.Adapters;
using FinanceTracker.Api.Exceptions;
using FinanceTracker.Api.Repositories;
using FinanceTracker.Api.Services;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging(logging => logging.AddConsole());

builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<ITransactionRepository, TransactionRepository>();
builder.Services.AddSingleton<IBudgetRepository, BudgetRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IBudgetService, BudgetService>();
builder.Services.AddScoped<IReportService, ReportService>();

builder.Services.AddSingleton<INotificationService, ConsoleNotificationAdapter>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async context =>
    {
        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        var exception = exceptionHandlerPathFeature?.Error;

        context.Response.ContentType = "application/json";

        if (exception is NotFoundException)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
        }
        else if (exception is ValidationException || exception is BudgetExceededException)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
        else
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        }

        await context.Response.WriteAsJsonAsync(new
        {
            Error = exception?.Message ?? "An unexpected error occurred."
        });
    });
});

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
