using System;
using WebApplication1;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Регистрация ICounter с использованием Counter
builder.Services.AddSingleton<ICounter, Count2>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapGet("/", (ICounter counter) =>
{


    lock (counter)
    {

        counter.IncrementCount();

        return counter.GetCount(); 
        
    }


});


app.MapPost("/reset", (ICounter counter) =>
{
    counter.SetDefaultCount();
    return Results.Ok("Счетчик сброшен.");
});


app.Run();