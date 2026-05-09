using core.Nodes;

namespace sqlgenerator.Services;

public class SqlGenerator
{
    public string GenerateSql(ExpressionNode node)
    {
        return node switch
        {
            RelationNode relation => GenerateRelationSql(relation),

            ProjectionNode projection => GenerateProjectionSql(projection),

            SelectionNode selection => GenerateSelectionSql(selection),

            JoinNode join => GenerateJoinSql(join),

            RenameNode rename => GenerateRenameSql(rename),

            _ => throw new Exception($"Unsupported node type: {node.GetType().Name}")
        };
    }

    private string GenerateProjectionSql(ProjectionNode node)
    {
        var sourceSql = GenerateSql(node.Source);
        var attributes = string.Join(", ", node.Attributes);
        return sourceSql.Replace("*", $"{attributes}");
    }

    private string GenerateSelectionSql(SelectionNode node)
    {
        var sourceSql = GenerateSql(node.Source);
        var conditionSql = GenerationConditionSql(node.Condition);
        return $"{sourceSql} WHERE {conditionSql}";
    }

    private string GenerationConditionSql(ConditionNode condition)
    {
        return condition switch
        {
            ComparisonNode comparison => $"({GenerateComparisonSql(comparison)})",
            AndNode and => $"{GenerationConditionSql(and.Left)} AND {GenerationConditionSql(and.Right)}",
            OrNode or => $"({GenerationConditionSql(or.Left)}) OR ({GenerationConditionSql(or.Right)})",
            NotNode not => $"(NOT {GenerationConditionSql(not.Inner)})",
            _ => throw new Exception($"Unsupported condition type: {condition.GetType().Name}")
        };
    }

    private string GenerateComparisonSql(ComparisonNode node)
    {
        return $"{node.Left} {node.Operator} {FormatLiteral(node.Right)}";
    }

    private object FormatLiteral(string right)
    {
        if(int.TryParse(right, out _))
            return right;    // Return as-is for numbers
        return $"'{right}'";   // Wrap in quotes for strings
    }

    private string GenerateJoinSql(JoinNode node)
    {
        var leftsql = GenerateSql(node.Left);
        var rightSql = ExtractTableName(node.Right);

        return $"{leftsql} NATURAL JOIN {rightSql}";
    }

    private string ExtractTableName(ExpressionNode node)
    {
        if (node is RelationNode relation)
            return relation.Name;
  
        throw new Exception($"Unsupported expression type for join: {node.GetType().Name}");
    }

    private string GenerateRenameSql(RenameNode node)
    {
        var sourceSql = GenerateSql(node.Source);
        return $"({sourceSql}) AS {node.Alias}";
    }

    private string GenerateRelationSql(RelationNode node)
    {
        return $"SELECT * FROM {node.Name}";
    }
}