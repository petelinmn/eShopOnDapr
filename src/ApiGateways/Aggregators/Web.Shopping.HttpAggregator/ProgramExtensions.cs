// Only use in this file to avoid conflicts with Microsoft.Extensions.Logging
using Serilog;

namespace Microsoft.eShopOnDapr.Web.Shopping.HttpAggregator;

public static class ProgramExtensions
{
    private const string AppName = "Shopping Aggregator API";

    public static void AddCustomSerilog(this WebApplicationBuilder builder)
    {
        var seqServerUrl = builder.Configuration["SeqServerUrl"];

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .WriteTo.Console()
            .WriteTo.Seq(seqServerUrl)
            .Enrich.WithProperty("ApplicationName", AppName)
            .CreateLogger();

        builder.Host.UseSerilog();
    }

    public static void AddCustomSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = $"eShopOnDapr - {AppName}", Version = "v1" });

            var identityUrlExternal = builder.Configuration.GetValue<string>("IdentityUrlExternal");

            c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows()
                {
                    Implicit = new OpenApiOAuthFlow()
                    {
                        AuthorizationUrl = new Uri($"{identityUrlExternal}/connect/authorize"),
                        TokenUrl = new Uri($"{identityUrlExternal}/connect/token"),
                        Scopes = new Dictionary<string, string>()
                            {
                                { "shoppingaggr-api", AppName }
                            }
                    }
                }
            });

            c.OperationFilter<AuthorizeCheckOperationFilter>();
        });
    }

    public static void UseCustomSwagger(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{AppName} V1");
            c.OAuthClientId("shoppingaggrswaggerui");
            c.OAuthAppName("Shopping Aggregator Swagger UI");
        });
    }

    public static void AddCustomAuthentication(this WebApplicationBuilder builder)
    {
        // Prevent mapping "sub" claim to nameidentifier.
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

        builder.Services.AddAuthentication("Bearer")
            .AddJwtBearer(options =>
            {
                options.Audience = "shoppingaggr-api";
                options.Authority = builder.Configuration.GetValue<string>("IdentityUrl");
                options.RequireHttpsMetadata = false;
            });
    }

    public static void AddCustomAuthorization(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("ApiScope", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("scope", "shoppingaggr");
            });
        });
    }

    public static void AddCustomHealthChecks(this WebApplicationBuilder builder) =>
        builder.Services.AddHealthChecks()
            .AddCheck("self", () => HealthCheckResult.Healthy())
            .AddDapr()
            .AddUrlGroup(new Uri(builder.Configuration["CatalogUrlHC"]), name: "catalogapi-check", tags: new string[] { "catalogapi" })
            .AddUrlGroup(new Uri(builder.Configuration["IdentityUrlHC"]), name: "identityapi-check", tags: new string[] { "identityapi" })
            .AddUrlGroup(new Uri(builder.Configuration["BasketUrlHC"]), name: "basketapi-check", tags: new string[] { "basketapi" })
            .AddUrlGroup(new Uri(builder.Configuration["FlowUrlHC"]), name: "flowapi-check", tags: new string[] { "flowapi" });

    public static void AddCustomApplicationServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IBasketService, BasketService>(
            _ => new BasketService(DaprClient.CreateInvokeHttpClient("basket-api")));

        builder.Services.AddSingleton<IFlowService, FlowService>(
            _ => new FlowService(DaprClient.CreateInvokeHttpClient("flow-api")));

        builder.Services.AddSingleton<ICatalogService, CatalogService>(
            _ => new CatalogService(DaprClient.CreateInvokeHttpClient("catalog-api")));
    }
}
