

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actie.BL.Models;
public record UserProjectDetailModel : ModelBase
{
    public required Guid UserId { get; set; }

    public required Guid ProjectId { get; set; }

    public static UserProjectDetailModel Empty => new()
    {
        UserId = Guid.Empty,
        ProjectId = Guid.Empty,
    };
}
