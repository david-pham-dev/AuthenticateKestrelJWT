using FrontEnd.Service;
using FrontEnd.Views;
using Microsoft.Extensions.Logging;

namespace FrontEnd
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            //builder.Services.AddHttpClient("AppHttpClient", (client) =>
            //{
            //    client.BaseAddress = new Uri(AppConfig.ApiBaseUrl);
            //});
            //{
            //    return new HttpClient { BaseAddress = new Uri(AppConfig.ApiBaseUrl) };
            //});
          
            builder.Services.AddSingleton<ApiService>();
            //builder.Services.AddSingleton<UserService>();
            builder.Services.AddTransient<RegisterPage>();
            builder.Services.AddTransient<SignInPage>();
            builder.Services.AddTransient<UserDetail>();


#if DEBUG
            builder.Logging.AddDebug();
#endif  

            return builder.Build();
        }
    }
}
