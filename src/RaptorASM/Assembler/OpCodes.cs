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
        public const byte Not = 0x0F;
        public const byte Cmp = 0x10;
        public const byte Je = 0x11;
        public const byte Jne = 0x12;
        public const byte Jg = 0x13;
        public const byte Jge = 0x14;
        public const byte Jl = 0x15;
        public const byte Jle = 0x16;
        public const byte Print_Char = 0x17;
        public const byte Load_Byte = 0x18;
        public const byte Load_Word = 0x19;
        public const byte Store_Byte = 0x1A;
        public const byte Store_Word = 0x1B;
        public const byte Inc = 0x1C;
        public const byte Dec = 0x1D;

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
                case "Not":
                    return Not;
                case "Cmp":
                    return Cmp;
                case "Je":
                    return Je;
                case "Jne":
                    return Jne;
                case "Jg":
                    return Jg;
                case "Jge":
                    return Jge;
                case "Jl":
                    return Jl;
                case "Jle":
                    return Jle;
                case "Print_Char":
                    return Print_Char;
                case "Load_Byte":
                    return Load_Byte;
                case "Load_Word":
                    return Load_Word;
                case "Store_Byte":
                    return Store_Byte;
                case "Store_Word":
                    return Store_Word;
                case "Inc":
                    return Inc;
                case "Dec":
                    return Dec;
                default:
                    throw new Exception("Unknown identifier: " + instructionString);
            }
        }
    }
}

