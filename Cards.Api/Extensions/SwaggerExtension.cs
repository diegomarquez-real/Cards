namespace Cards.Api
{
    public static class SwaggerExtension
    {
        public static void AddSwaggerWithAuth(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                // Grouped Swagger Documentation for Dbo.
                options.SwaggerDoc("Dbo", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Dbo API",
                    Version = "v1"
                });

                // Grouped Swagger Documentation for Yugioh.
                options.SwaggerDoc("Yugioh", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Yugioh API",
                    Version = "v1"
                });

                // Define Swagger groups.
                options.DocInclusionPredicate((docName, apiDesc) =>
                {
                    if (String.IsNullOrEmpty(apiDesc.GroupName))
                        return true;  // Default grouping for controllers without GroupName.

                    return docName.Equals(apiDesc.GroupName);
                });

                options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
                {
                    Scheme = "Bearer",
                    Description = @"JWT Authorization header using the Bearer scheme. Example: ""Authorization: Bearer {token}""",
                    BearerFormat = "JWT",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
                });

                options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
                {
                    {
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference()
                            {
                                Id = "Bearer",
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>()
                    }
                });
            });
        }
    }
}