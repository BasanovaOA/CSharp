namespace WebApplication1;

public interface ICounter
{

        void SetDefaultCount();
        void IncrementCount();
        
        int GetCount();
}