namespace core.Nodes;

public class ComparisonNode : ConditionNode
{
    public string Left { get; }
    public string Operator { get; }
    public string Right { get; }

    public ComparisonNode(string left, string op, string right)
    {
        Left = left;
        Operator = op;
        Right = right;
    }
}