using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Data.Repositories.Yugioh
{
    public class AttributeRepository : GenericRepository<Models.Attribute, Guid>, Abstractions.Repositories.Yugioh.IAttributeRepository
    {
        public AttributeRepository(Abstractions.IDataContext dataContext)
            : base(dataContext)
        {
        }
    }
}