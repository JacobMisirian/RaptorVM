using System;

namespace RaptorASM
{
    public class OpCodes
    {
        public const byte Add = 0x01;
        public const byte Sub = 0x02;
        public const byte Mul = 0x03;
        public const byte Div = 0x04;
        public const byte Mod = 0x05;
        public const byte Mov = 0x06;
        public const byte Load_Immediate = 0x07;
        public const byte Print = 0x08;

        public static byte ToByte(string instructionString)
        {
            switch (instructionString)
            {
                case "Add":
                    return Add;
                case "Sub":
                    return Sub;
                case "Mul":
                    return Mul;
                case "Div":
                    return Div;
                case "Mod":
                    return Mod;
                case "Mov":
                    return Mov;
                case "Load_Immediate":
                    return Load_Immediate;
                case "Print":
                    return Print;
                default:
                    throw new Exception("Unknown identifier: " + instructionString);
            }
        }
    }
}

