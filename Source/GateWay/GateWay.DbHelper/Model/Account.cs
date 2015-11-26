using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GateWay.DbHelper.Model
{
    public class Account
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public DateTime CreateDateTime { get; set; }

        public bool IsDelete { get; set; }

        public string Sex { get; set; }

        public DateTime? BirthDay { get; set; }

        public string Phone { get; set; }

        public string NickName { get; set; }
    }
}
