// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

namespace Actie.BL.Models;
public record TagListModel : ModelBase
{
    public required string Name { get; set; }
    public string? Description { get; set; }

    public static TagListModel Empty => new()
    {
        Id = Guid.NewGuid(),
        Name = string.Empty,
        Description = string.Empty,
    };
}
