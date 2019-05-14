using System;
using System.Collections.Generic;
using System.Text;

namespace VM.Net
{
    public class Stack
    {
        public IList<string> Push_D_OntoStack()
        {
            var assemblyInstruction = new List<string>
            {
                //SP == 0
                $"@SP",
                $"A=M",
                $"M=D",
                $"@SP",
                $"M=M+1"
            };

            return assemblyInstruction;
        }

        public IList<string> PopTo_D()
        {
            var assemblyInstruction = new List<string>
            {
                //SP == 0
                $"@SP",
                $"A=M-1",
                $"D=M",
                $"@SP",
                $"M=M-1"
            };

            return assemblyInstruction;
        }
    }
}
