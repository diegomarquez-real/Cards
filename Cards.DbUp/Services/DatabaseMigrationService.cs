using DbUp;
using DbUp.Engine;
using DbUp.Support;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cards.DbUp.Services
{
    public class DatabaseMigrationService : Abstractions.IDatabaseMigrationService
    {
        private readonly ILogger<DatabaseMigrationService> _logger;
        private readonly IOptions<Options.ConnectionStringsOptions> _connectionStringsOptions;

        public DatabaseMigrationService(ILogger<DatabaseMigrationService> logger,
            IOptions<Options.ConnectionStringsOptions> connectionStringsOptions)
        {
            _logger = logger;
            _connectionStringsOptions = connectionStringsOptions;
        }

        public void ApplyMSSQLDatabaseMigrations()
        {
            try
            {
                _logger.LogInformation("Ensuring MSSQL database exists...");
                EnsureDatabase.For.SqlDatabase(_connectionStringsOptions.Value.MssqlConnection);
                _logger.LogInformation("Applying MSSQL database migrations...");
                var upgrader = DeployChanges.To.SqlDatabase(_connectionStringsOptions.Value.MssqlConnection)
                                               .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly(), script => script.StartsWith("Cards.DbUp.Scripts."), new SqlScriptOptions { ScriptType = ScriptType.RunOnce, RunGroupOrder = 1 })
                                               .LogToConsole()
                                               .Build();
                var result = upgrader.PerformUpgrade();
                if (!result.Successful)
                {
                    _logger.LogError(result.Error, "An error occurred while applying MSSQL database migrations.");
                }
                else
                {
                    _logger.LogInformation("MSSQL database migrations applied successfully.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while applying MSSQL database migrations.");
            }
        }
    }
}
