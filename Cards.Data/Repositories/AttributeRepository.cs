using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Data.Repositories
{
    public class AttributeRepository : GenericRepository<Models.Attribute, Guid>, Abstractions.IAttributeRepository
    {
        public AttributeRepository(Abstractions.IDataContext dataContext)
            : base(dataContext)
        {
        }
    }
}