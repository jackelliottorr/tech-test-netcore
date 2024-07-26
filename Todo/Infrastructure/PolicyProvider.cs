using Polly;
using Polly.Extensions.Http;
using System;
using System.Net.Http;

namespace Todo.Infrastructure;

public class PolicyProvider
{
    public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(3, // Retry up to 3 times
                retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)) // Exponential backoff
            );
    }

    public static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .CircuitBreakerAsync(5, // Break the circuit after 5 consecutive failed requests
                TimeSpan.FromMinutes(5) // Wait for 5 minutes before resetting the circuit
            );
    }

    public static IAsyncPolicy<HttpResponseMessage> GetFallbackPolicy()
    {
        return Policy<HttpResponseMessage>
            .Handle<HttpRequestException>()
            .FallbackAsync(new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent("{\"display_name\": \"Fallback User\"}")
            });
    }
}