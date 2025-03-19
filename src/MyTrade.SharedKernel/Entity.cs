using Ardalis.Result;
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

        private List<IDomainEvent> _events = new List<IDomainEvent>();

        public IReadOnlyCollection<IDomainEvent> Events => _events.AsReadOnly();

        public void ClearDomainEvents()
        {
            _events.Clear();
        }

        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _events.Add(domainEvent);
        }
    }
}
