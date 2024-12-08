using LoadTestWebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger(); 
app.UseSwaggerUI();
app.MapGet("/load-test/{count:int}", async (int count) =>
    {
        var test = new LoadTest();
        var result = await test.Test("http://localhost:5095", "",count);
        return result;
    })
    .WithName("LoadTest")
    .WithOpenApi();

app.Run();

