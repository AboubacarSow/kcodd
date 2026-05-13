namespace webBlazor.Utils;
public class Tools
{
   public static readonly Op[] Ops =
    [
        // Core filter / shape
        new("σ",  "Select",     "σ "),
        new("π",  "Project",    "π "),
        new("ρ",  "Rename",     "ρ "),
 
        // Joins
        new("⨝",  "Nat. join",  " ⋈ "),
        new("⨝θ", "Theta join", " ⋈θ "),
 
        // Set operations
        new("∪",  "Union",      " ∪ "),
        new("∩",  "Intersect",  " ∩ "),
        new("−",  "Difference", " − "),
 
        // Logic
        new("∧",  "And",        " ∧ "),
        new("∨",  "Or",         " ∨ "),
 
        // Parens
        new("(",  "",           "("),
        new(")",  "",           ")"),
    ];
 
    public static readonly Snippet[] Snippets =
    [
        new("σ [age > 18] (Student)",                             "Selection"),
        new("π [name, age] (Student)",                            "Projection"),
        new("σ [age > 18 ∧ gpa >= 3] (Student)",                  "AND condition"),
        new("σ [dept = 'CS' ∨ dept = 'Math'] (Student)",          "OR condition"),
        new("Student ⨝ Enrolled",                                 "Natural join"),
        new("Student ⨝θ [Student.id = Enrolled.sid] (Enrolled)",  "Theta join"),
        new("ρ S (Student)",                                      "Rename"),
        new("π [name] (Student) ∪ π [name] (Professor)",          "Union"),
        new("π [dept] (Student) ∩ π [dept] (Professor)",          "Intersection"),
        new("π [name] (Student) − π [name] (Graduate)",           "Difference"),
    ];
    
}