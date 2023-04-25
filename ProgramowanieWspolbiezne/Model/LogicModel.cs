using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic;

namespace Model
{
    public class LogicModel
    {
        private readonly LogicAbstractApi _logic;
        public LogicModel(LogicAbstractApi logic = null)
        {
            _logic = logic;
            LogicAbstractApi.CreateApi();
        }
    }
}