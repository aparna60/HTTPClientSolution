using Polly;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddHttpClient("ProductService",
    client =>
    {
        client.BaseAddress = new Uri("http://localhost:5125/api/product");
    }).AddTransientHttpErrorPolicy(policy=> policy.WaitAndRetryAsync(3, retryattempt => TimeSpan.FromSeconds(retryattempt)));
   

var app = builder.Build();

// Configure the HTTP request pipeline.


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
