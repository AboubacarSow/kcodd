namespace core.Nodes;

public class RelationNode : ExpressionNode
{
    public string Name { get; }

    public RelationNode(string name)
    {
        Name = name;
    }
}


