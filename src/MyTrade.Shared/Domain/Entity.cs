using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTrade.Shared.Domain;

public class Entity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    private List<IDomainEvent> _events = new List<IDomainEvent>();

    public IReadOnlyCollection<IDomainEvent> Events => _events.AsReadOnly();

    
}

