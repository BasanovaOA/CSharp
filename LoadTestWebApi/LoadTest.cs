namespace LoadTestWebApi;

public class LoadTest
{
    public async Task<ICollection<LoadTestInfo>> Test(string baseUrl, string location, int urlCount)
    {
        var urls = Enumerable.Range(0, urlCount)
            .Select(i => new LoadTestInfo($"{baseUrl}/{location}", i))
            .ToList();

        var tasks = Parallel.ForEachAsync(
            source: urls,
            body: (info, cancellationToken) => info.GetAsync(cancellationToken));
        await tasks;
        return urls;
    }
}