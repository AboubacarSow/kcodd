using System.Reflection;
using core.Nodes;

namespace transformer;

public class Transformer
{
    public static SelectionNode Transform(SelectionNode node)
    {
        var source = node.Source;
        var condition = node.Condition;
        var typeofSource = source.GetType();
        Console.WriteLine(typeofSource.ToString());
        if(typeofSource.Equals(typeof(SelectionNode).Assembly))
        {
            ConditionNode previous=null!;
            ExpressionNode newsource=null!;
            while (typeofSource.Equals(typeof(SelectionNode)))
            {
                var newcondition = (source as SelectionNode)!.Condition;
                newsource = (source as SelectionNode)!.Source;
                typeofSource = newsource.GetType();
                if(previous==null)
                    previous = newcondition;
                else
                {
                    previous = new AndNode(previous,newcondition);
                }
                
            }
            return new SelectionNode(new AndNode(previous,condition),newsource);
        }

        return node;
    }
}