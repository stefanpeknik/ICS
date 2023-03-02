using Actie.DAT.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.

namespace Actie.DAL.Entities
{
    public class TagEntity : IEntity
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required int? Color { get; set; }

    }
}
