﻿namespace Actie.BL.Models;

public record ActivityListModel : ModelBase
{
    public required string Name { get; set; }
    public required DateTime Start { get; set; }
    public required DateTime End { get; set; }
    public required string Type { get; set; }

    public static ActivityListModel Empty => new()
    {
        Id = Guid.Empty,
        Name = string.Empty,
        Start = DateTime.MinValue,
        End = DateTime.MinValue,
        Type = string.Empty
    };
}
