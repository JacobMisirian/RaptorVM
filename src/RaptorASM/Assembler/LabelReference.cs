using System;

namespace RaptorASM
{
    public class LabelReference
    {
        public string Name { get; private set; }
        public long Position { get; private set; }

        public LabelReference(string name, long position)
        {
            Name = name;
            Position = position;
        }
    }
}

