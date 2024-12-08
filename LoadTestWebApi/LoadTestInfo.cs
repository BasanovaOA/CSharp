using System.Diagnostics;
using System.Net;

namespace LoadTestWebApi;

public record LoadTestInfo(string Url, int Iteration)
{
    public TimeSpan EclipsedTime { get; set; }

    public HttpStatusCode Code { get; set; }
    public string? Content { get; set; }
    public DateTime StartTime { get; set; }
    
    public async ValueTask GetAsync(CancellationToken cancellationToken)
    {
        using var client = new HttpClient();
        var stopWatch = new Stopwatch();
        StartTime = DateTime.Now;
        stopWatch.Start();
        using var response = await client.GetAsync(Url, cancellationToken).ConfigureAwait(false);
        stopWatch.Stop();
        Code = response.StatusCode;
        EclipsedTime = stopWatch.Elapsed;
        Content = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
    }
}