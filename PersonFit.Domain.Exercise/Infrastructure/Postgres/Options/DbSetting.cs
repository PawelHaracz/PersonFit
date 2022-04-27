namespace PersonFit.Domain.Exercise.Infrastructure.Postgres.Options;

public class DbSetting
{
    public string Host { get; set; }
    public string Database { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }

    public override string ToString() => $"Host={Host};Database={Database};Username={Username};Password={Password}";
}