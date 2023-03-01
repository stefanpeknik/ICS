﻿using System.Xml.Serialization;

namespace Actie.DAT.Entities;

public class ProjectEntity : IEntity
{
    public Guid Id { get; set; }
        
    public required string Name { get; set; }
    public ICollection<ActivityEntity>? Users { get; set; }
    public ICollection<ActivityEntity>? Activities { get; set; }

}