using System;
using System.Collections.Generic;
using System.Text;

namespace VM.Net.Commands
{
    public class Sub : Command
    {
        public override IList<string> Execute(Stack stack)
        {
            var assemblyInstructions = new List<string>();

            //Get the top value on the stack
            assemblyInstructions.AddRange(stack.PopTo_D());
            //Save it temporarily in order to get the
            //second value on the stack
            assemblyInstructions.Add($"@{MemorySegments.R14}");
            assemblyInstructions.Add("M=D");

            //Get the new top value on the stack
            assemblyInstructions.AddRange(stack.PopTo_D());

            assemblyInstructions.Add($"@{MemorySegments.R14}");
            assemblyInstructions.Add("D=D-M");

            assemblyInstructions.AddRange(stack.Push_D_OntoStack());

            return assemblyInstructions;
        }
    }
}
