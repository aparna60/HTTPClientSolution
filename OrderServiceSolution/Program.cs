using Polly;
using Polly.Extensions.Http;

var retryPolicy= HttpPolicyExtensions
    .HandleTransientHttpError()
    .WaitAndRetryAsync(3, retryAttempt=> TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));

var circuitBreakerPolicy = HttpPolicyExtensions
    .HandleTransientHttpError()
    .CircuitBreakerAsync(3, TimeSpan.FromSeconds(30));

var timeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(5);

var bulkHeadPolicy = Policy.BulkheadAsync<HttpResponseMessage>(10, 20);

var fallbackPolicy = Policy<HttpResponseMessage>.Handle<HttpRequestException>()
    .FallbackAsync(new HttpResponseMessage(System.Net.HttpStatusCode.OK)
    {
        Content = new StringContent("[{\"ProductId\":0,\"Name\":\"Default Product\",\"Description\":\"Service Unavailable\"}]")
    });

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddHttpClient("ProductService",
    client =>
    {
        client.BaseAddress = new Uri("http://localhost:5125/api/product");
    }).AddPolicyHandler(fallbackPolicy)                     // Provide a default response when the API is down
    .AddPolicyHandler(retryPolicy)                          // Retry on failures
    .AddPolicyHandler(circuitBreakerPolicy)                 // Stop calling failing services temporarily
    .AddPolicyHandler(timeoutPolicy)                        // Cancel long-running requests
    .AddPolicyHandler(bulkHeadPolicy);                      // Limit concurrent requests

var app = builder.Build();

// Configure the HTTP request pipeline.


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
