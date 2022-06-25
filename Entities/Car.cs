using System;
using System.Collections.Generic;

#nullable disable

namespace AvtoSalon.Entities
{
    public partial class Car
    {
        public Car()
        {
            Contracts = new HashSet<Contract>();
        }

        public int Id { get; set; }
        public string CarBrand { get; set; }
        public int Country { get; set; }
        public DateTime Year { get; set; }

        public virtual Country CountryNavigation { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }
    }
}
