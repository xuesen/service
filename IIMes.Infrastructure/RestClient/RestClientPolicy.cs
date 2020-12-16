using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Polly;
using Polly.CircuitBreaker;
using Polly.Extensions.Http;

namespace IIMes.Infrastructure.RestClient
{
    public static class RestClientPolicy
    {
        public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }

        public static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(
                    2,
                    TimeSpan.FromSeconds(60),
                    async (x, y) =>
                    {
                        await Task.CompletedTask;
                        Console.WriteLine("circuit breaked");
                    },
                    async () =>
                    {
                        await Task.CompletedTask;
                        Console.WriteLine("circuit breaked");
                    });
        }

        public static IAsyncPolicy<HttpResponseMessage> GetFallbackPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .Or<BrokenCircuitException>()
                .FallbackAsync(
                async (ctx, token) =>
                {
                    await Task.CompletedTask;
                    return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("fallback ok.") };
                },
                async (result, ctx) =>
                {
                    await Task.CompletedTask;
                    Console.WriteLine("to call fallback.");
                });
        }
    }
}
