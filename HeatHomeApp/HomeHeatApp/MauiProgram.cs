using CommunityToolkit.Maui;
using HeatHomeApp.Services;
using HeatHomeApp.Veiw;
using HeatHomeApp.VeiwModel;
using HeatHomeApp.Models;
using Syncfusion.Maui.Core.Hosting;

namespace HeatHomeApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureSyncfusionCore()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
		builder.Services.AddSingleton<HeatingUnitVeiwModel>();
		builder.Services.AddSingleton<SingleHeatingUnitVeiwModel>();
		builder.Services.AddSingleton<HeatingUnitService>();
		builder.Services.AddSingleton<HeatingUnitPage>();
		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddSingleton<SignalRService>();
		return builder.Build();
	}
}
