using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terminal
{
    class Wesh : Shell
    {
        public override string Message(string p_script)
        {
            return p_script;
        }
    }
}
