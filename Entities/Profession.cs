using System;
using System.Collections.Generic;

#nullable disable

namespace AvtoSalon.Entities
{
    public partial class Profession
    {
        public Profession()
        {
            Workers = new HashSet<Worker>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Worker> Workers { get; set; }
    }
}
