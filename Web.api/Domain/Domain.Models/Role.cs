using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain.Models
{
    public partial class Role
    {
        public int RoleId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
    }
}
