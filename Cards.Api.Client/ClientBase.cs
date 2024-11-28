using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Api.Client
{
    public abstract class ClientBase
    {
        private readonly Enums.SchemaType _schemaType;
        private readonly IOptions<Options.ApiClientSettings> _options;

        protected ClientBase(Enums.SchemaType schemaType, 
            IOptions<Options.ApiClientSettings> options)
        {
            _schemaType = schemaType;
            _options = options;
        }

        public abstract string Name { get; }

        protected IFlurlRequest BuildUrlWithAuth()
        {
            return _options.Value.ApiBaseUrl
                .AppendPathSegment(this.AppendSchema(_schemaType))
                .AppendPathSegment(this.Name)
                .WithOAuthBearerToken(String.Empty);
        }

        protected Url BuildUrlWithoutAuth()
        {
            return _options.Value.ApiBaseUrl
                .AppendPathSegment(this.AppendSchema(_schemaType))
                .AppendPathSegment(this.Name);
        }

        private string AppendSchema(Enums.SchemaType schemaType)
        {
            switch (schemaType)
            {
                case Enums.SchemaType.Dbo:
                    return String.Empty;
                case Enums.SchemaType.Yugioh:
                    return "Yugioh";
                default:
                    return String.Empty;
            }
        }
    }
}