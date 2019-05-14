using System;
using System.Collections.Generic;
using System.Text;

namespace VM.Net.Commands
{
    public class Neg : Command
    {
        public override IList<string> Execute(Stack stack)
        {
            var assemblyInstructions = new List<string>();

            //Get the top value on the stack
            assemblyInstructions.AddRange(stack.PopTo_D());
            
            assemblyInstructions.Add("D=-D");

            assemblyInstructions.AddRange(stack.Push_D_OntoStack());

            return assemblyInstructions;
        }
    }
}
