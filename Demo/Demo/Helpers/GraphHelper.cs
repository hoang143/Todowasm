using Azure.Identity;
using Microsoft.Graph;
using Microsoft.Graph.Models;

namespace Demo.Helpers
{
    public class GraphHelper
    {
        private readonly IConfiguration _configuration;

        public GraphHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public GraphServiceClient GetGraphClient()
        {
            var scopes = new[] { "https://graph.microsoft.com/.default" };

            var tenantId = _configuration["Hoang:TenantID"];
            var clientId = _configuration["Hoang:ClientID"];
            var clientSecret = _configuration["Hoang:ClientSecret"];

            var options = new TokenCredentialOptions
            {
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
            };

            var clientSecretCredential = new ClientSecretCredential(tenantId, clientId, clientSecret, options);

            var graphClient = new GraphServiceClient(clientSecretCredential, scopes);
            return graphClient;
        }
        public async Task CreateListAsync()
        {
            var graphClient = GetGraphClient();

            var list = new List
            {
                DisplayName = "Books",
                Columns = new ListColumnsCollectionPage()
    {
        new ColumnDefinition
        {
            Name = "Author",
            Text = new TextColumn
            {
            }
        },
        new ColumnDefinition
        {
            Name = "PageCount",
            Number = new NumberColumn
            {
            }
        }
    },
                ListInfo = new ListInfo
                {
                    Template = "genericList"
                }
            };

            await graphClient.Sites["{site-id}"].Lists
                .Request()
                .AddAsync(list);
        }

    }
}