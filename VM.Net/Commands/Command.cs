using System;
using System.Collections.Generic;
using System.Text;

namespace VM.Net.Commands
{
    public abstract class Command
    {
        public abstract IList<string> Execute(Stack stack);
    }
}
