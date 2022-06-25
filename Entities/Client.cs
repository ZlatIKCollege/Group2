using System;
using System.Collections.Generic;

#nullable disable

namespace AvtoSalon.Entities
{
    public partial class Client
    {
        public Client()
        {
            Contracts = new HashSet<Contract>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Contract> Contracts { get; set; }
    }
}
