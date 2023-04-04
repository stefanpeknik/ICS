﻿// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actie.BL.Models;
public record ActivityDetailModel : ModelBase
{
    public required string Name { get; set; }
    public required DateTime Start { get; set; }
    public required DateTime End { get; set; }
    public int? Rating { get; set; }
    public string? Type { get; set; }
    public string? Description { get; set; }
    public Guid? ProjectId { get; set; }

    public static ActivityDetailModel Empty => new()
    {
        Id = Guid.NewGuid(),
        Name = string.Empty,
        Start = DateTime.MinValue,
        End = DateTime.MinValue,
        Type = string.Empty,
        Description = string.Empty,
        ProjectId = Guid.Empty,
    };
}
