using System;
using System.Collections;
using System.Text;

namespace LibCalcEngine
{
    public interface IParser
    {
        ArrayList Parse(string parseString);
    }
}
