// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actie.BL.Models;
public record UserListModel : ModelBase
{
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public Uri? Photo { get; set; }

    public static UserListModel Empty => new()
    {
        Id = Guid.NewGuid(),
        Name = string.Empty,
        Surname = string.Empty,
        Photo = null,
    };
}
