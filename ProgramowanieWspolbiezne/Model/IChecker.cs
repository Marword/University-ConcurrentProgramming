
namespace Presentation.Model
{
    public interface IChecker<T>
    {
        bool Check(T value);
        bool CheckNotCorrect(T value);
    }
}
