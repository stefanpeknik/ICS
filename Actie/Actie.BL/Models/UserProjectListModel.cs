// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actie.BL.Models;
public record UserProjectListModel : ModelBase
{
    public required Guid UserId { get; set; }

    public required Guid ProjectId { get; set; }

    public static UserProjectListModel Empty => new()
    {
        UserId = Guid.Empty,
        ProjectId = Guid.Empty,
    };
}
