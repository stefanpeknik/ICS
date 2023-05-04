// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actie.App.Options;
public record DALOptions
{
    public LocalDbOptions? LocalDb { get; init; }
    public SqliteOptions? Sqlite { get; init; }
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

