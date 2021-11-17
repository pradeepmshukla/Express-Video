using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Roles:Base
    {
        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public string RoleDescription { get; set; }
    }
}
