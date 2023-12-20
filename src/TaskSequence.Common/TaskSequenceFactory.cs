namespace TaskSequence.Common;

internal static class TaskSequenceFactory
{
    public static TaskSequence<T> CreateTaskSequence<T>(IList<Task<T>> tasks) =>
        CreateTaskSequence(tasks.ToArray());
    public static TaskSequence<T> CreateTaskSequence<T>(params Task<T>[] tasks) =>
        new TaskSequence<T>(CreateSequence(tasks));

    private static async IAsyncEnumerable<T> CreateSequence<T>(params Task<T>[] tasks)
    {
        if (tasks is null)
            throw new ArgumentNullException(nameof(tasks));

        foreach (var task in tasks)
        {
            if (task is null) continue;
            yield return await task;
        }
    }
}