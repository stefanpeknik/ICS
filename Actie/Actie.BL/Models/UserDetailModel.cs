﻿// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Actie.DAL.Entities;

namespace Actie.BL.Models;
public record UserDetailModel : ModelBase
{
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public Uri? Photo { get; set; }
    public int? Age { get; set; }
    public string? Gender { get; set; }
    public float? Weight { get; set; }
    public int? Height { get; set; }
    public ObservableCollection<ActivityEntity>? Activities { get; init; } = new();
    public ObservableCollection<UserProjectEntity>? Projects { get; init; } = new();

    public static UserDetailModel Empty => new()
    {
        Id = Guid.NewGuid(),
        Name = string.Empty,
        Surname = string.Empty,
        Photo = null,
        Gender = string.Empty,
    };
}
