using System;
using VM.Net.Commands;
using VM.Net.Commands.ProgramFlow;
using VM.Net.Commands.SubroutineCalling;

namespace VM.Net
{
    public class CommandFactory
    {
        public Command Create(VMCommandsContext context, string line)
        {
            var commandFields = line.Split(' ');
            var commandName = commandFields[0];
            var segment = commandFields.Length > 1 ? commandFields[1] : null;
            var index = commandFields.Length > 2 ? commandFields[2] : null;

            switch (commandName)
            {
                case "push":
                    return new Push(context, segment, index);
                case "pop":
                    return new Pop(context, segment, index);
                case "add":
                    return new Add();
                case "sub":
                    return new Sub();
                case "eq":
                    return new Eq();
                case "lt":
                    return new Lt();
                case "gt":
                    return new Gt();
                case "neg":
                    return new Neg();
                case "and":
                    return new And();
                case "or":
                    return new Or();
                case "not":
                    return new Not();
                case "label":
                    return new Label(context, segment);
                case "goto":
                    return new Goto(context, segment);
                case "if-goto":
                    return new IfGoto(context, segment);
                case "function":
                    return new Function(context, segment, index);
                case "return":
                    return new Return(context);
                case "call":
                    return new Call(context, segment, index);

                default:
                    throw new NotSupportedException();
            }
        }
    }
}
