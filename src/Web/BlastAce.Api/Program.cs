
namespace BlastAceApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        var config = GetConfiguration();
        builder.Services.AddControllers().AddDapr();

        builder.Services.AddRazorPages();

        builder.Services.AddSingleton(new Settings()
        {
            ApiGatewayUrlExternal = config.GetValue<string>(nameof(Settings.ApiGatewayUrlExternal))
        });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();


            //for razor
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseCloudEvents();
        app.MapControllers();
        app.MapSubscribeHandler();

        //for razor
        app.UseStaticFiles();
        app.UseRouting();
        app.MapRazorPages();

        app.Run();
    }
    static IConfiguration GetConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();

        return builder.Build();
    }
}
