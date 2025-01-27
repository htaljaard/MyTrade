using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTrade.SharedKernel
{
    public class Entity(Guid EntityId)
    {
        public Guid Id { get; protected set; } = EntityId;


    }
}
