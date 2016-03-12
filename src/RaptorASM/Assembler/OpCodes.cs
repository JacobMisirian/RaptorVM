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
        public const byte Jmp = 0x09;
        public const byte Shift_Left = 0x0A;
        public const byte Shift_Right = 0x0B;
        public const byte And = 0x0C;
        public const byte Or = 0x0D;
        public const byte Xor = 0x0E;

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
                case "Jmp":
                    return Jmp;
                case "Shift_Left":
                    return Shift_Left;
                case "Shift_Right":
                    return Shift_Right;
                case "And":
                    return And;
                case "Or":
                    return Or;
                case "Xor":
                    return Xor;
                default:
                    throw new Exception("Unknown identifier: " + instructionString);
            }
        }
    }
}

