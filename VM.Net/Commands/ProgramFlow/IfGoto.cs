using System;
using System.Collections.Generic;
using System.Text;

namespace VM.Net.Commands.ProgramFlow
{
    public class IfGoto : Command
    {
        private readonly VMCommandsContext _context;
        private readonly string _labelName;

        public IfGoto(VMCommandsContext context, string labelName)
        {
            _context = context;
            _labelName = labelName;
        }

        public override IList<string> Execute(Stack stack)
        {
            var assemblyInstructions = new List<string>();

            assemblyInstructions.AddRange(stack.PopTo_D());

            assemblyInstructions.Add($"@{_context.FunctionNames.Peek()}${_labelName}");
            assemblyInstructions.Add("D;JNE");

            return assemblyInstructions;
        }
    }
}
