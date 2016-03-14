using System;

namespace RaptorB.Parser
{
    public class CodeBlockNode: AstNode
    {
        public static AstNode Parse(Parser parser)
        {
            CodeBlockNode block = new CodeBlockNode();

            while (!parser.EndOfStream)
                block.Children.Add(StatementNode.Parse(parser));

            return block;
        }
    }
}

