using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Logic
{
    public class LogicApi : LogicAbstractApi
    {
        private readonly DataAbstractApi _data;
        public LogicApi(DataAbstractApi data)
        {
            _data = data;
        }
    }
}
