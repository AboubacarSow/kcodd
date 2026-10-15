using Core.Nodes;

namespace SQLGenerator;

public class SqlGenerator
{
    public string GenerateSql(ExpressionNode node)
    {
        return node switch
        {
            RelationNode relation => GenerateRelationSql(relation),

            ProjectionNode projection => GenerateProjectionSql(projection),

            SelectionNode selection => GenerateSelectionSql(selection),

            NaturalJoinNode join => GenerateJoinSql(join),

            ThetaJoinNode thetaJoin => GenerateThetaJoinSql(thetaJoin),

            RenameNode rename => GenerateRenameSql(rename),

            IntersectionNode intersection => GenerateIntersectionSql(intersection),

            DifferenceNode difference => GenerateDifferenceSql(difference),

            UnionNode union => GenerateUnionSql(union),

            CartesienProductNode cartesien => GenerateProductSql(cartesien),

            DivisionNode division => GenerateDivisionSql(division),
            LeftOuterJoinNode leftOuterJoin => GenerateLeftOuterJoin(leftOuterJoin),
            RightOuterJoinNode rightOuterJoin => GenerateRightOuterJoin(rightOuterJoin),
            FullOuterJoinNode fullOuterJoin => GenerateFullOuterJoin(fullOuterJoin),

            _ => throw new Exception($"Unsupported node type: {node.GetType().Name}")
        };
    }


    private string GenerateLeftOuterJoin(LeftOuterJoinNode leftOuterJoin)
    {
        var left = GenerateSql(leftOuterJoin.Left);
        var leftTableName = ExtractTableName(leftOuterJoin.Left);
        var right = ExtractTableName(leftOuterJoin.Right);

        return $"{left} LEFT JOIN {right} ON {leftTableName}.{right}_id={right}.id";
    }

    private string GenerateRightOuterJoin(RightOuterJoinNode rightOuterJoin)
    {
        var left = GenerateSql(rightOuterJoin.Left);
        var leftTableName = ExtractTableName(rightOuterJoin.Left);
        var right = ExtractTableName(rightOuterJoin.Right);

        return $"{left} RIGHT JOIN {right} ON {leftTableName}.{right}_id={right}.id";
    }

    private string GenerateFullOuterJoin(FullOuterJoinNode leftOuterJoin)
    {
        var left = GenerateSql(leftOuterJoin.Left);
        var leftTableName = ExtractTableName(leftOuterJoin.Left);
        var right = ExtractTableName(leftOuterJoin.Right);

        return $"{left} FULL OUTER JOIN {right} ON {leftTableName}.{right}_id={right}.id";
    }
    private string GenerateDivisionSql(DivisionNode division)
    {
        var sourceSql = GenerateSql(division.Left.Source);

        var innerQuery = GenerateSql(division.Right);
        foreach (var attribute in division.Right.Attributes)
        {
            if (!division.Left.Attributes.Contains(attribute))
            {
                throw new Exception($"Attribute :{attribute} does not exist in the first relation");
            }

        }
        List<string> candidate_attributes = [.. division.Left.Attributes.Except(division.Right.Attributes)];
        if(candidate_attributes.Count==0){
            throw new Exception($"Candidate Attribute not found");
        }
        var key_attributes = string.Join(",", candidate_attributes);
        sourceSql = sourceSql.Replace("*", $"DISTINCT {key_attributes}");
        var left_alias = ExtractTableName(division.Left.Source).ToLower()[0];

        var innerAttributes = string.Join(", ", division.Right.Attributes);
        var sourceSqlwith_righAttributes = GenerateSql(division.Left.Source)
        .Replace("*", $"DISTINCT {innerAttributes}");
        string inputComparaison="";

        for(var index=0;index<candidate_attributes.Count; index++)
        {
            inputComparaison =string.Concat(inputComparaison,
             $"{left_alias}2.{candidate_attributes[index]}={left_alias}.{candidate_attributes[index]}");
            if (!(index + 1 == candidate_attributes.Count))
            {
                inputComparaison = string.Concat(inputComparaison," AND ");
            }
        }


        return @$"{sourceSql} AS {left_alias}
                WHERE NOT EXISTS (
                {innerQuery}
                EXCEPT
                {sourceSqlwith_righAttributes} AS {left_alias}2
                WHERE 
                {inputComparaison})";
    }

    private string GenerateProductSql(CartesienProductNode cartesien)
    {
        var left = GenerateSql(cartesien.Left);
        var right = ExtractTableName(cartesien.Right);
        return $"{left}, {right}";
    }

    private string GenerateProjectionSql(ProjectionNode node)
    {
        var sourceSql = GenerateSql(node.Source);
        var attributes = string.Join(", ", node.Attributes);
        return sourceSql.Replace("*", $"DISTINCT {attributes}");
    }

    private string GenerateSelectionSql(SelectionNode node)
    {
        var sourceSql = GenerateSql(node.Source);
        var conditionSql = GenerationConditionSql(node.Condition);
        return $"{sourceSql} WHERE {conditionSql}";
    }

    private static string GenerationConditionSql(ConditionNode condition)
    {
        return condition switch
        {
            ComparisonNode comparison => $"({GenerateComparisonSql(comparison)})",
            AndNode and => $"{GenerationConditionSql(and.Left)} AND {GenerationConditionSql(and.Right)}",
            OrNode or => $"{GenerationConditionSql(or.Left)} OR {GenerationConditionSql(or.Right)}",
            NotNode not => $"NOT {GenerationConditionSql(not.Inner)}",
            _ => throw new Exception($"Unsupported condition type: {condition.GetType().Name}")
        };
    }

    private static string GenerateComparisonSql(ComparisonNode node)
    {
        return $"{node.Left} {node.Operator} {FormatLiteral(node.Right)}";
    }

    private static string FormatLiteral(string right)
    {
        if (int.TryParse(right, out _) || decimal.TryParse(right, out _) || right.Contains('.'))
            return right;
        return $"'{right}'";
    }

    private string GenerateJoinSql(NaturalJoinNode node)
    {
        var leftsql = GenerateSql(node.Left);
        var rightSql = ExtractTableName(node.Right);

        return $"{leftsql} NATURAL JOIN {rightSql}";
    }
    private string GenerateThetaJoinSql(ThetaJoinNode node)
    {
        var leftsql = GenerateSql(node.Left);
        var rightSql = ExtractTableName(node.Right);
        var conditionSql = GenerationConditionSql(node.Condition);

        return $"{leftsql} JOIN {rightSql} ON {conditionSql}";
    }
    private static string ExtractTableName(ExpressionNode node)
    {
        if (node is RelationNode relation)
            return relation.Name;

        throw new Exception($"Unsupported expression type: {node.GetType().Name}");
    }

    private string GenerateRenameSql(RenameNode node)
    {
        var sourceSql = GenerateSql(node.Source);
        return $"({sourceSql}) AS {node.Alias}";
    }
    private static string GenerateRelationSql(RelationNode node)
    {
        return $"SELECT * FROM {node.Name}";
    }

    private string GenerateIntersectionSql(IntersectionNode node)
    {
        var leftSql = GenerateSql(node.Left);
        var rightSql = GenerateSql(node.Right);
        return $"{leftSql} INTERSECT {rightSql}";
    }
    private string GenerateUnionSql(UnionNode node)
    {
        var leftSql = GenerateSql(node.Left);
        var rightSql = GenerateSql(node.Right);
        return $"{leftSql} UNION {rightSql}";
    }
    private string GenerateDifferenceSql(DifferenceNode node)
    {
        var leftSql = GenerateSql(node.Left);
        var rightSql = GenerateSql(node.Right);
        return $"{leftSql} EXCEPT {rightSql}";
    }
}