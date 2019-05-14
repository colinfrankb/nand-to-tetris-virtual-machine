using System;
using System.Collections.Generic;
using System.Text;

namespace VM.Net
{
    public class MemorySegments
    {
        public static IDictionary<string, string> PredefinedSymbols = new Dictionary<string, string>
        {
            { "stackpointer", "SP" },
            { "local", "LCL" },
            { "argument", "ARG" },
            { "this", "THIS" },
            { "that", "THAT" },
            { "temp", "R5" }
        };

        // Segments mapped to memory
        // argument => Stack (256-2047)
        // local => Stack (256-2047)
        // static => Static RAM[16..255], which is the RAM address space used for 
        //           symbols (Variable Symbols and Label Symbols) found in an asm file.
        //           For the file Xxx.vm, push static 3, should be translated to 
        //           @Xxx.3
        //           D=M
        //           The Hack assembler will alocate RAM space, therefore no need to worry 
        //           about it in VM implementation
        // constant => not mapped, read arg2 as value
        // this => Heap (2048-16383)
        // that => Heap (2048-16383)
        // pointer[0..1] => Register RAM[3..4]
        // temp[0..7] => Register RAM[5..12]

        public static string SP = "SP";
        public static string LCL = "LCL";
        public static string ARG = "ARG";
        public static string THIS = "THIS";
        public static string THAT = "THAT";
        public static string R14 = "R14";
        public static string R15 = "R15";

        public static string GetTempSymbol(string index)
        {
            return $"R{5 + Convert.ToInt32(index)}";
        }
    }
}
