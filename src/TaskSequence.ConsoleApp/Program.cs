using TaskSequence.Common;

public class Program
{
    private enum CacheType
    {
        Undefined,
        Local,
        Memory
    }

    private record Cache(string Value);
    private record LocalCache(string MockValue) : Cache(MockValue);
    private record MemoryCache(string MockValue) : Cache(MockValue);

    static void Main()
    {
        var program = new Program();
        program.Start().Wait();
    }

    private async Task Start()
    {
        var sequenceBuilder = new TaskSequenceBuilder<Cache>();
        var sequence = sequenceBuilder
            .Add(GetNull) // Add task with predefined null value
            .Add(GetCacheAsync("Local cache", CacheType.Local)) // Add task with predefined "Local cache" value
            .Add(() => GetCacheAsync("Memory cache", CacheType.Memory)) // Add task with predefined "Memory cache" value
            .ToSequence();

        // Try to get the first not null value
        var cache = await sequence
            .FirstOrDefaultAsync(
                selector: x => x != null,
                cancellationToken: default);

        Console.WriteLine(cache?.Value); // prints "Local cache"
    }

    private Task<Cache> GetNull() => null;

    private async Task<Cache> GetCacheAsync(string mockValue, CacheType cacheType)
    {
        await Task.Delay(1000);
        switch (cacheType)
        {
            case CacheType.Local: return new LocalCache(mockValue);
            case CacheType.Memory: return new MemoryCache(mockValue);
            default: throw new ArgumentOutOfRangeException(nameof(cacheType));
        };
    }
}
