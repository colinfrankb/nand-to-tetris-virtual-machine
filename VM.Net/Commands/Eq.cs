using System;
using System.Collections.Generic;
using System.Text;

namespace VM.Net.Commands
{
    public class Eq : ComparedToZero
    {
        protected override string GetJumpField()
        {
            return "JEQ";
        }
    }
}
