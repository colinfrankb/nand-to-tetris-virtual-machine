using System;
using System.Collections.Generic;
using System.Text;
using VM.Net.Commands.ProgramFlow;

namespace VM.Net.Commands.SubroutineCalling
{
    public class Call : Command
    {
        private readonly VMCommandsContext _context;
        private readonly string _functionName;
        private readonly int _n_arguments;

        public Call(VMCommandsContext context, string functionName, string n_arguments)
        {
            _context = context;
            _functionName = functionName;
            _n_arguments = Convert.ToInt32(n_arguments);
        }

        public override IList<string> Execute(Stack stack)
        {
            var assemblyInstructions = new List<string>();

            var returnAddressLabelSymbol = Guid.NewGuid().ToString();

            //push return-address
            assemblyInstructions.Add($"@{returnAddressLabelSymbol}");
            assemblyInstructions.Add("D=A");
            assemblyInstructions.AddRange(stack.Push_D_OntoStack());

            //push LCL
            Push_M_OfSymbolOntoStack(assemblyInstructions, MemorySegments.LCL, stack);

            //push ARG
            Push_M_OfSymbolOntoStack(assemblyInstructions, MemorySegments.ARG, stack);

            //push THIS
            Push_M_OfSymbolOntoStack(assemblyInstructions, MemorySegments.THIS, stack);

            //push THAT
            Push_M_OfSymbolOntoStack(assemblyInstructions, MemorySegments.THAT, stack);

            //ARG = SP - n - 5
            assemblyInstructions.Add($"@{MemorySegments.SP}");
            assemblyInstructions.Add("D=M");
            assemblyInstructions.Add($"@{_n_arguments}");
            assemblyInstructions.Add("D=D-A");
            assemblyInstructions.Add("@5");
            assemblyInstructions.Add("D=D-A");
            assemblyInstructions.Add($"@{MemorySegments.ARG}");
            assemblyInstructions.Add("M=D");

            //LCL = SP
            assemblyInstructions.Add($"@{MemorySegments.SP}");
            assemblyInstructions.Add("D=M");
            assemblyInstructions.Add($"@{MemorySegments.LCL}");
            assemblyInstructions.Add("M=D");

            //goto f
            assemblyInstructions.Add($"@{_functionName}");
            assemblyInstructions.Add("0;JMP");

            //(return-address)
            assemblyInstructions.Add($"({returnAddressLabelSymbol})");

            return assemblyInstructions;
        }

        private void Push_M_OfSymbolOntoStack(List<string> assemblyInstructions, string symbol, Stack stack)
        {
            assemblyInstructions.Add($"@{symbol}");
            assemblyInstructions.Add("D=M");
            assemblyInstructions.AddRange(stack.Push_D_OntoStack());
        }
    }
}
