namespace core.Nodes;

public class OrNode : ConditionNode
{
    public ConditionNode Left { get; }
    public ConditionNode Right { get; }

    public OrNode(ConditionNode left, ConditionNode right)
    {
        Left = left;
        Right = right;
    }
}
