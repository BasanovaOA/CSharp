namespace LoadTestWebApi;

public static class EnumerableExtension
{
    public static async Task<IEnumerable<TResult>> ForEachAsync<T, TResult>(this IEnumerable<T> source, int degreeOfParallelism, Func<T, Task<TResult>> body)
    {
        if (degreeOfParallelism == 1)
        {
            var results = new List<TResult>();
            foreach (var item in source)
            {
                results.Add(await body(item));
            }
            return results;
        }
        else
        {
            var tasks = new Task<TResult>[degreeOfParallelism];
            var index = 0;
            var results = new List<TResult>();
            foreach (var item in source)
            {
                var itemCopy = item;  // Захват переменной замыкания
                tasks[index++] = body(itemCopy);
                if (index == degreeOfParallelism)  // Бача полна
                {
                    var batchResults = await Task.WhenAll(tasks);
                    results.AddRange(batchResults);
                    index = 0;  // Сброс индекса для заполнения следующей партии
                }
            }
            if (index > 0)  // Ждать любые остатки
            {
                var batchResults = await Task.WhenAll(tasks);
                results.AddRange(batchResults);
            }
            return results;
        }

    }
}