namespace webBlazor.Dtos;

public class Result(string ra, string sql, bool isError, DateTime at,string timeInfo = null)
{
    public string Ra { get; } = ra;
    public string Sql { get; } = sql;
    public bool IsError { get; } = isError;
    public DateTime At { get; } = at;
    public bool Copied { get; set; }

    public string Elapsed { get; set; } = timeInfo;
}