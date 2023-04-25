
namespace Presentation.Model
{
    public interface IChecker<T>
    {
        bool Check(T value);
    }
}
