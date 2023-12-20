namespace TaskSequence.Common;

public record TaskSequence<T>(IAsyncEnumerable<T> Sequence);
