namespace WebApplication1
{
    public class Counter : ICounter
    {
        // Статическое поле для хранения счетчика
        private static int _count = 0;

        // Сбросить счетчик
        public void SetDefaultCount()
        {
            Interlocked.Exchange(ref _count, 0); // Атомарный сброс
        }

        public void IncrementCount()
        {
            throw new NotImplementedException();
        }

        // Увеличить счетчик


        // Получить текущее значение счетчика
        public int GetCount()
        {
            // Получаем значение атомарно с помощью CompareExchange
            int temp = _count;
            return _count;
        }
    }
}