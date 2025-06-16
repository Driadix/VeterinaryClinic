using System.Net.Http;

namespace Client.Services.Handlers;

public class StrategyHandler : DelegatingHandler
{
    public static ApiClientService? ApiClientServiceInstance { get; set; }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (ApiClientServiceInstance != null)
        {
            request.Headers.Remove("X-Data-Access-Strategy");

            var strategyValue = ApiClientServiceInstance.UseEntityFramework ? "EFCore" : "RawSQL";

            request.Headers.Add("X-Data-Access-Strategy", strategyValue);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}