using System;
using System.Collections.Generic;
using System.Text;

namespace VM.Net.Commands
{
    public class Lt : ComparedToZero
    {
        protected override string GetJumpField()
        {
            return "JLT";
        }
    }
}
