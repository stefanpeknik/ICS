// Copyright (c) .NET Foundation and contributors. All rights reserved.
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
public record ProjectDetailModel : ModelBase
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public ObservableCollection<ActivityEntity>? Activities { get; init; } = new ();
    public ObservableCollection<UserProjectEntity>? Users { get; init; } = new();

    public static ProjectDetailModel Empty => new()
    {
        Id = Guid.NewGuid(),
        Name = string.Empty,
        Description = string.Empty,

    };
}
