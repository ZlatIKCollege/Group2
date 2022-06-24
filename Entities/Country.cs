using System;
using System.Collections.Generic;

#nullable disable

namespace AvtoSalon.Entities
{
    public partial class Country
    {
        public Country()
        {
            Cars = new HashSet<Car>();
        }

        public int Id { get; set; }
        public string Countries { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
