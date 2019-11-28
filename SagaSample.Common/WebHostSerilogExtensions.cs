using Microsoft.AspNetCore.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace SagaSample.Common
{
    public static class WebHostSerilogExtensions
    {
        public static IWebHostBuilder UseSerilogFromConfig(this IWebHostBuilder builder) =>
            (builder ?? throw new ArgumentNullException(nameof(builder)))
                .UseSerilog((ctx, cfg) => cfg.ReadFrom.Configuration(ctx.Configuration));
    }
}
