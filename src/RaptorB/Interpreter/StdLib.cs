using System;
using System.Collections.Generic;

namespace RaptorB.Interpreter
{
    public class StdLib: ILibrary
    {
        public Dictionary<string, InternalFunction> GetFunctions()
        {
            var result = new Dictionary<string, InternalFunction>();

            result.Add("putchar", new InternalFunction(putchar));
            result.Add("putint", new InternalFunction(putint));

            return result;
        }

        private static object putchar(object[] args)
        {
            Console.Write((char)args[0]);
            return null;
        }

        private static object putint(object[] args)
        {
            Console.Write((Int16)args[1]);
            return null;
        }
    }
}

