namespace webBlazor.Utils;
public static class Tools
{
    public static readonly OpGroup[] OpGroups =
    [
        new("Filter / Shape",
        [
            new("σ",  "Select",  "σ "),
            new("π",  "Project", "π "),
            new("ρ",  "Rename",  "ρ "),
        ]),
        new("Joins",
        [
            new("⋈",  "Nat. join",  " ⋈ "),
            new("⋈θ", "Theta join", " ⋈θ "),
        ]),
        new("Set ops",
        [
            new("∪", "Union",      " ∪ "),
            new("∩", "Intersect",  " ∩ "),
            new("−", "Difference", " − "),
        ]),
        new("Logic",
        [
            new("∧", "And", " ∧ "),
            new("∨", "Or",  " ∨ "),
        ]),
    ];
 
    // Flat list for the Home page pills
    public static readonly Op[] Ops =
        OpGroups.SelectMany(g => g.Ops).ToArray();
 
    public static readonly Snippet[] Snippets =
    [
        new("σ [age > 18] (Student)",                             "Selection"),
        new("π [name, age] (Student)",                            "Projection"),
        new("σ [age > 18 ∧ gpa >= 3] (Student)",                  "AND condition"),
        new("σ [dept = 'CS' ∨ dept = 'Math'] (Student)",          "OR condition"),
        new("Student ⋈ Enrolled",                                 "Natural join"),
        new("Student ⋈θ [Student.id = Enrolled.sid] (Enrolled)",  "Theta join"),
        new("ρ S (Student)",                                      "Rename"),
        new("π [name] (Student) ∪ π [name] (Professor)",          "Union"),
        new("π [dept] (Student) ∩ π [dept] (Professor)",          "Intersection"),
        new("π [name] (Student) − π [name] (Graduate)",           "Difference"),
    ];

    public static readonly HashSet<string> SetOfKeywords = new(StringComparer.OrdinalIgnoreCase)
    {
        "SELECT","DISTINCT","FROM","WHERE","AND","OR","NOT",
        "JOIN","NATURAL","ON","UNION","INTERSECT","EXCEPT",
        "AS","ALL","LEFT","RIGHT","FULL","OUTER","INNER",
        "GROUP","BY","HAVING","ORDER","ASC","DESC","LIMIT"
    };

}

public record OpGroup(string Label, Op[] Ops);
public record SqlToken(string Text, bool IsKeyword);

