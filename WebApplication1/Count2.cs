namespace WebApplication1
{
    public class Count2 : ICounter
    {
        // Статическое поле для хранения счетчика
        public static int count = 0;

        private static readonly object lockObject = new object(); // Объект для блокировки

        
        // Сбросить счетчик
        public void SetDefaultCount()
        {
            // Блокируем доступ к критической секции

            count = 0; // Сброс

            
        }

        // Увеличить счетчик
        public void IncrementCount()
        {
            lock (lockObject)
            {

                count++; // Инкремент
                //return count; // Возвращаем уникальный порядковый номер для каждого потока
            }

        }

        // Получить текущее значение счетчика
        public int GetCount( )
        {
            lock (lockObject)
            {

                return count; // Чтение значения
            }


        }
    }
}