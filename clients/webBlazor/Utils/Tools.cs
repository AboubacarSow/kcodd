namespace webBlazor.Utils;
public class Tools
{
    public  static readonly Op[] Ops =
    [
        new("σ", "Select", "σ "),
        new("π", "Project", "π "),
        new("ρ", "Rename", "ρ "),
        new("⨝", "Nat. join", " ⨝ "),
        new("⨝θ", "Theta join", " ⨝θ "),
        new("∧", "And", " ∧ "),
        new("∨", "Or", " ∨ "),
        new("(", "", "("),
        new(")", "", ")"),
    ];

    public  static readonly Snippet[] Snippets =
    [
        new("σ age > 18 (Student)", "Simple selection"),
        new("π name, age (Student)", "Projection"),
        new("ρ S / Student", "Rename relation"),
        new("Student ⨝ Enrolled", "Natural join"),
        new("Student ⨝θ age > 18 (Enrolled)", "Theta join"),
        new("σ age > 18 ∧ gpa > 3 (Student)","AND condition"),
    ];
    
}