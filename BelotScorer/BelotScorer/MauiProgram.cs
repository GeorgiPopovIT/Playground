﻿using BelotScorer.Data;
using BelotScorer.Pages;
using Microsoft.Extensions.Logging;

namespace BelotScorer;

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
        builder.Services.AddTransient<CreateGamePage>();


        builder.Services.AddSingleton<GameRepository>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
