

namespace Actie.App.Options;
public record DALOptions
{
#nullable enable
    public LocalDbOptions? LocalDb { get; init; }
    public SqliteOptions? Sqlite { get; init; }
#nullable disable
}

public record LocalDbOptions
{
    public bool Enabled { get; init; }
    public string ConnectionString { get; init; } = null!;
}

public record SqliteOptions
{
    public bool Enabled { get; init; }

    public string DatabaseName { get; init; } = null!;
    /// <summary>
    /// Deletes database before application startup
    /// </summary>
    public bool RecreateDatabaseEachTime { get; init; } = false;

    /// <summary>
    /// Seeds DemoData from DbContext on database creation.
    /// </summary>
    public bool SeedDemoData { get; init; } = false;
}

