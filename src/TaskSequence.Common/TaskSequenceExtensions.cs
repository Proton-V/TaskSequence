namespace TaskSequence.Common;

public static class TaskSequenceExtensions
{
    public static async Task<T?> FirstOrDefaultAsync<T>(this TaskSequence<T> taskSequence,
        Func<T, bool> selector = default,
        CancellationToken cancellationToken = default)
    {
        if (taskSequence is null)
            throw new ArgumentNullException(nameof(taskSequence));

        await foreach (var data in taskSequence.Sequence.WithCancellation(cancellationToken))
        {
            if (selector?.Invoke(data) ?? true) return data;
        }

        return default;
    }
}
