namespace lexer.Models;
public enum TokenType
{
    // Relational Algebra Operators
    SELECT,      
    PROJECT,     
    JOIN,      
    RENAME,     

    // Logical Operators
    AND,         
    OR,          
    NOT,        

    // Structure
    LPAREN, RPAREN, COMMA,

    // Comparisons
    EQ, NEQ, LT, GT, LTE, GTE,

    // Data
    IDENTIFIER,
    NUMBER,
    STRING,

    EOF
}
