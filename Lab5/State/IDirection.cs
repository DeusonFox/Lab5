using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5.State
{
    interface IDirection
    {
        void Step(ref int X, ref int Y);
        IDirection ChangeTrend(string command);
        direction Trend { get; }
    }
}
