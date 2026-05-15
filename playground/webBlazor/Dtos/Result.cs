namespace webBlazor.Dtos;

public class Result
{
    public string Ra { get;set; }
    public string Sql { get; set; }
    public bool IsError { get; set; }
    public DateTime At { get; set; }
    public bool Copied { get; set; }

    public string Elapsed { get; set; }


    public Result() { }
    public Result(string Ra,string Sql, bool IsError, DateTime At, string Elapsed,bool Copied=false)
    {
        this.Ra = Ra;
        this.Sql = Sql;
        this.IsError = IsError;
        this.At = At;
        this.Copied = Copied;
        this.Elapsed = Elapsed;
    }
   

    public Result(string ra, string sql, bool isError, DateTime at)
    {
        Ra = ra;
        Sql = sql;
        IsError = isError;
        At = at;
        Elapsed = null!;
    }

    
}