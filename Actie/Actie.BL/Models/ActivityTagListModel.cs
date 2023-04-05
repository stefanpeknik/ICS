// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actie.BL.Models;
public record ActivityTagListModel : ModelBase
{
    public required Guid TagId { get; set; }
    public required Guid ActivityId { get; set; }

    public static ActivityTagListModel Empty => new()
    {
        ActivityId= Guid.Empty,
        TagId = Guid.Empty
    };
}
