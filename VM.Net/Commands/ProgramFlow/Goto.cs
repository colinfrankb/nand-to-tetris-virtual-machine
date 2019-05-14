using System;
using System.Collections.Generic;
using System.Text;

namespace VM.Net.Commands.ProgramFlow
{
    public class Goto : Command
    {
        private readonly VMCommandsContext _context;
        private readonly string _labelName;

        public Goto(VMCommandsContext context, string labelName)
        {
            _context = context;
            _labelName = labelName;
        }

        public override IList<string> Execute(Stack stack)
        {
            var assemblyInstructions = new List<string>();

            assemblyInstructions.Add($"@{_context.FunctionNames.Peek()}${_labelName}");
            assemblyInstructions.Add("0;JMP");

            return assemblyInstructions;
        }
    }
}
