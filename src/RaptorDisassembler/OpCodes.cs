using System;

namespace RaptorDisassembler
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
       
        public static string ToString(byte b)
        {
            switch (b)
            {
                case 0x01:
                    return "Add";
                case 0x02:
                    return "Sub";
                case 0x03:
                    return "Mul";
                case 0x04:
                    return "Div";
                case 0x05:
                    return "Mod";
                case 0x06:
                    return "Mov";
                case 0x07:
                    return "Load_Immediate";
                case 0x08:
                    return "Print";
                default:
                    throw new Exception("Unknown byte: " + b);
            }
        }
    }
}

