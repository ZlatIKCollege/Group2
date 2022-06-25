using System;
using System.Collections.Generic;

#nullable disable

namespace AvtoSalon.Entities
{
    public partial class Contract
    {
        public int Id { get; set; }
        public int Client { get; set; }
        public int Worker { get; set; }
        public int Car { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }

        public virtual Car CarNavigation { get; set; }
        public virtual Client ClientNavigation { get; set; }
        public virtual Worker WorkerNavigation { get; set; }
    }
}
