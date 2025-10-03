using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMicroservice.Bus.Events
{
    public record OrderCreatedEvent(Guid OrderId, Guid UserId);

}
