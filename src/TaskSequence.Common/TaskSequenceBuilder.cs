namespace TaskSequence.Common;

public class TaskSequenceBuilder<T>
{
    public TaskSequenceBuilder()
    {
        tasks = new List<Task<T>>();
    }
    private IList<Task<T>> tasks;

    public TaskSequenceBuilder<T> Add(Task<T> task)
    {
        tasks.Add(task);
        return this;
    }

    public TaskSequenceBuilder<T> Add(Func<Task<T>> taskFunc)
    {
        if (taskFunc?.Invoke() is Task<T> task)
            tasks.Add(task);
        return this;
    }

    public TaskSequence<T> ToSequence() =>
        TaskSequenceFactory.CreateTaskSequence(tasks);
}
