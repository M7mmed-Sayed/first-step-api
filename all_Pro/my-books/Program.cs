using Microsoft.EntityFrameworkCore;
using my_books.Data;
using my_books.Data.Services;
using my_books.Exceptions;
using Serilog;
public class Program
{
    public static void Main(string[] args)
    {
        // log
        try
        {
            var configration = new ConfigurationBuilder().
                AddJsonFile("appsettings.json",optional:false,reloadOnChange:true).Build();

            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configration)
              .CreateLogger();

            //Log.Logger = new LoggerConfiguration()
            //    .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
            //    .CreateLogger();

            var builder = WebApplication.CreateBuilder(args);
            // Serilog to Builder
            builder.Host.UseSerilog((ctx,cfg)=>cfg.ReadFrom.Configuration(ctx.Configuration));


            // Add services to the container.

            builder.Services.AddControllers();

            string conectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
            builder.Services.AddDbContext<AppDbContext>(opttions => opttions.UseSqlServer(conectionString));

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddTransient<BooksService>();
            builder.Services.AddTransient<AuthorsService>();
            builder.Services.AddTransient<PublishersService>();
            builder.Services.AddTransient<LogsService>();
            //Add Api Versioning
            // builder.Services.AddApiVersioning();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();

            app.UseAuthorization();
            var loggerFactory = LoggerFactory.Create(logging =>
            {
                logging.Configure(options =>
                {
                    options.ActivityTrackingOptions = ActivityTrackingOptions.SpanId
                                                        | ActivityTrackingOptions.TraceId
                                                        | ActivityTrackingOptions.ParentId
                                                        | ActivityTrackingOptions.Baggage
                                                        | ActivityTrackingOptions.Tags;
                }).AddSimpleConsole(options =>
                {
                    options.IncludeScopes = true;
                });
            });
            var env = app.Environment;
            //middleWare Exception Handling
            app.ConfigureBuildInExceptionHandeler(loggerFactory);
            // app.ConfigureCustomExceptionHandeler();

            app.MapControllers();

            //AppDbinitializer.Seed(app);
            app.Run();
        }
        finally
        {

            Log.CloseAndFlush();
        }

    }
}