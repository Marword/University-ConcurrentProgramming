using System;

namespace Data
{
    public abstract class DataAbstractApi
    {

        public static DataAbstractApi CreateDataApi()
        {
            return new DataApi();
        }
    }
}