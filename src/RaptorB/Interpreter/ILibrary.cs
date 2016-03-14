using System;
using System.Collections.Generic;

namespace RaptorB.Interpreter
{
    interface IFunction
    {
        object Invoke(object[] args);
    }
    public interface ILibrary
    {
        Dictionary<string, InternalFunction> GetFunctions();
    }
    public delegate object FunctionDelegate(object[] arguments);

    public class InternalFunction : IFunction
    {
        private FunctionDelegate target;

        public InternalFunction(FunctionDelegate target)
        {
            this.target = target;
        }

        public object Invoke(object[] args)
        {
            return this.target(args);
        }
    }
}