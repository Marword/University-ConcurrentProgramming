namespace Model.API
{
    public interface IChecker<T>
    {
        bool Check(T value);
        bool CheckNotCorrect(T value);
    }
}
