using System;
using System.Collections.Generic;
using System.Text;

namespace VM.Net
{
    public class VMCommandsContext
    {
        public string FileName { get; set; }
        public Stack<string> FunctionNames = new Stack<string>();
    }
}
