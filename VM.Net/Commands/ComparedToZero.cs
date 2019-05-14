using System;
using System.Collections.Generic;
using System.Text;

namespace VM.Net.Commands
{
    public class ComparedToZero : Command
    {
        public override IList<string> Execute(Stack stack)
        {
            var assemblyInstructions = new List<string>();

            //Get the top value on the stack
            assemblyInstructions.AddRange(stack.PopTo_D());
            //Save it temporarily in order to get the
            //second value on the stack
            assemblyInstructions.Add($"@{MemorySegments.R15}");
            assemblyInstructions.Add("M=D");

            //Get the new top value on the stack
            assemblyInstructions.AddRange(stack.PopTo_D());

            assemblyInstructions.Add($"@{MemorySegments.R15}");
            assemblyInstructions.Add("D=D-M");

            var setDTOTrue = Guid.NewGuid();
            var endOfEq = Guid.NewGuid();

            assemblyInstructions.Add($"@{setDTOTrue}");
            assemblyInstructions.Add($"D;{GetJumpField()}");
            assemblyInstructions.Add("D=0");
            assemblyInstructions.Add($"@{endOfEq}");
            assemblyInstructions.Add("0;JMP");
            assemblyInstructions.Add($"({setDTOTrue})");
            assemblyInstructions.Add("D=-1");
            assemblyInstructions.Add($"({endOfEq})");

            assemblyInstructions.AddRange(stack.Push_D_OntoStack());

            return assemblyInstructions;
        }

        protected virtual string GetJumpField()
        {
            throw new NotImplementedException();
        }
    }
}
