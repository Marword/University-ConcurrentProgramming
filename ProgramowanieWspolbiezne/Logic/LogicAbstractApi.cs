using Data;
namespace Logic
{
    public abstract class LogicAbstractApi
    {
        protected LogicAbstractApi CreateLogicApi(DataAbstractApi data = default)
        {
            return new LogicApi(data ?? DataAbstractApi.CreateDataApi());
        }
    }
}