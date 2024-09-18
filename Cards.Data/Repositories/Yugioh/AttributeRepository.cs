using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Data.Repositories.Yugioh
{
    public class AttributeRepository : GenericRepository<Models.Yugioh.Attribute, Guid>, Abstractions.Repositories.Yugioh.IAttributeRepository
    {
        public AttributeRepository(Abstractions.IDataContext dataContext,
            Abstractions.IUserContext userContext)
            : base(dataContext, userContext)
        {
        }
    }
}