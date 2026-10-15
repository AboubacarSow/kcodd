namespace Core.Nodes;

public class ProjectionNode : ExpressionNode
{
    public List<string> Attributes { get; }
    public ExpressionNode Source { get; }

    public ProjectionNode(List<string> attributes, ExpressionNode source)
    {
        Attributes = attributes;
        Source = source;
    }
}
