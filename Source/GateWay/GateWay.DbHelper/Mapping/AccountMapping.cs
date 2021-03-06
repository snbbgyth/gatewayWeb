﻿using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GateWay.DbHelper.Model;

namespace GateWay.DbHelper.Mapping
{
   public  class AccountMapping:EntityTypeConfiguration<Account>
    {
       public AccountMapping()
       {
           ToTable("Account");
           HasKey(t => t.Id);
           Property(t => t.CreateDateTime);
           Property(t => t.BirthDay);
           Property(t => t.IsDelete);
           Property(t => t.Name);
           Property(t => t.NickName);
           Property(t => t.Password);
           Property(t => t.Phone);
           Property(t => t.Sex);
           Property(t => t.Email);
           Property(t => t.Salt);
       }
    }
}
