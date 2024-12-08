using MORR.Domain.Repositories.Command.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MORR.Domain.Repositories.Command
{
    public interface IOrderItemCommandRepository : ICommandRepository<OrderItem>
    {
    }
}
