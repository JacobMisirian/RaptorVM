using System;
using System.Collections.Generic;

namespace RaptorASM
{
    public class OpCodes
    {
        public static List<string> instructions = new List<string>()
        {
            "Add", "Sub", "Mul", "Div", "Mod", "Mov", "Load_Immediate", "Print", "Jmp", "Shift_Left", "Shift_Right",
            "And",  "Or", "Xor", "Not", "Cmp", "Je", "Jne", "Jg", "Jge", "Jl", "Jle", "Print_Char", "Load_Byte", "Load_Word",
            "Store_Byte", "Store_Word", "Inc", "Dec", "Push", "Pop", "Call", "Ret", "Add_Immediate", "Sub_Immediate",
            "Mul_Immediate", "Div_Immediate", "Mod_Immediate", "Mov_Immediate", "Print_Immediate", "Print_Char_Immediate",
            "Shift_Left_Immediate", "Shift_Right_Immediate", "And_Immedate", "Or_Immediate", "Xor_Immediate", "Cmp_Immediate",
            "Push_Immediate"
        };

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
        public const byte Push = 0x1E;
        public const byte Pop = 0x1F;
        public const byte Call = 0x20;
        public const byte Ret = 0x21;
        public const byte Add_Immediate = 0x22;
        public const byte Sub_Immediate = 0x23;
        public const byte Mul_Immediate = 0x24;
        public const byte Div_Immediate = 0x25;
        public const byte Mod_Immediate = 0x26;
        public const byte Mov_Immediate = 0x27;
        public const byte Print_Immediate = 0x28;
        public const byte Print_Char_Immediate = 0x29;
        public const byte Shift_Left_Immediate = 0x2A;
        public const byte Shift_Right_Immediate = 0x2B;
        public const byte And_Immediate = 0x2C;
        public const byte Or_Immediate = 0x2D;
        public const byte Xor_Immediate = 0x2E;
        public const byte Cmp_Immediate = 0x2F;
        public const byte Push_Immediate = 0x30;

        public static byte ToByte(string instructionString)
        {
            return (byte)(instructions.IndexOf(instructionString) + 1);
        }
    }
}