namespace core.Nodes;

public class NotNode : ConditionNode
{
    public ConditionNode Inner { get; }

    public NotNode(ConditionNode inner)
    {
        Inner = inner;
    }
}