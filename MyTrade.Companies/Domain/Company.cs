using MyTrade.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTrade.Companies.Domain
{
    internal class Company : Entity
    {
        public string ComanyNumber { get; private set; } = string.Empty;
        public Company(Guid EntityId) : base(EntityId)
        {
        }


    }
}
