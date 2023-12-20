Simple ordered sequence of tasks with selection rules (c#)

This may help as a starting point for avoiding multiple conditional checks.

Example of multiple conditional checks:

```cs
    private async Task Start()
    {
        var cache = await GetCacheAsync(null, CacheType.Local);
        if (cache is LocalCache && cache.HasKey("UserData"))
        {
            ///...
        }

        cache = await GetCacheAsync(null, CacheType.Memory);
        if (cache is MemoryCache && cache.HasKey("UserData"))
        {
            ///...
        }

        ///...
        ///...
        ///...
    }
```

Example of using TaskSequence:

```cs
    private async Task Start()
    {
        var sequenceBuilder = new TaskSequenceBuilder<Cache>();
        var sequence = sequenceBuilder
            .Add(GetLocalCacheAsync) // Add task with predefined "Local cache" value
            .Add(GetMemoryCacheAsync) // Add task with predefined "Memory cache" value
            .ToSequence();

        // Try to get the first not null value
        var cache = await sequence
            .FirstOrDefaultAsync(
                selector: x => x != null, // optional: selector to skip or accept results
                cancellationToken: default); // optional: CancellationToken

        Console.WriteLine(cache?.Value); // prints "Local cache"
    }
```
