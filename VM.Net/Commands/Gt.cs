using System;
using System.Collections.Generic;
using System.Text;

namespace VM.Net.Commands
{
    public class Gt : ComparedToZero
    {
        protected override string GetJumpField()
        {
            return "JGT";
        }
    }
}
