// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System.Collections.ObjectModel;
using Actie.DAL.Entities;

namespace Actie.BL.Models;
public record TagDetailModel : ModelBase
{
    public required string Name { get; set; }
    public string? Description { get; set; }

    public ObservableCollection<ActivityTagEntity> Activities { get; init; } = new();

    public static TagDetailModel Empty => new()
    {
        Id = Guid.NewGuid(),
        Name = string.Empty,
        Description = string.Empty,
    };
}
