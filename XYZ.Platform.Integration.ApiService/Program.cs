
using XYZ.Platform.Core.Application.Service;
using XYZ.Platform.Infrastructure.Repository;

namespace XYZ.Platform.Integration.ApiService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            var loggerFactory = LoggerFactory.Create(loggingBuilder => loggingBuilder
                .SetMinimumLevel(LogLevel.Trace)
                .AddConsole());

            ILogger logger = loggerFactory.CreateLogger<TransactionService>();

            builder.Services.AddSingleton<ITransactionService>(new TransactionService(logger)
            {
                CsvImportedTransactionsValidator = new CsvImportedTransactionsValidator(),
                CsvTransactionsImporter = new CsvTransactionsImporter(),
                XmlImportedTransactionsValidator = new XmlImportedTransactionsValidator(),
                XmlTransactionsImporter = new XmlTransactionsImporter(),
                TransactionsRepository = new InMemoryTransactionsRepository()
            });

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
