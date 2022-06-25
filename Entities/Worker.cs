using System;
using System.Collections.Generic;

#nullable disable

namespace AvtoSalon.Entities
{
    public partial class Worker
    {
        public Worker()
        {
            Contracts = new HashSet<Contract>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Profession { get; set; }
        public double Salary { get; set; }

        public virtual Profession ProfessionNavigation { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }
    }
}
