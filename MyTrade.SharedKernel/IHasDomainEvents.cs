using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTrade.SharedKernel
{
    public interface IHasDomainEvents
    {
        List<DomainEvent> DomainEvents { get; }
        void ClearDomainEvents();
    }
}
