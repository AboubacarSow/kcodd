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
    public Result(string Ra,String Sql, bool IsError, DateTime At,bool Copied=false, string Elapsed="")
    {
        this.Ra = Ra;
        this.Sql = Sql;
        this.IsError = IsError;
        this.At = At;
        this.Copied = Copied;
        this.Elapsed = Elapsed;
    }
   

    public Result(string ra, string sql, bool isError, DateTime at, string timeInfo=null!)
    {
        Ra = ra;
        Sql = sql;
        IsError = isError;
        At = at;
        Elapsed = timeInfo;
    }

    
}