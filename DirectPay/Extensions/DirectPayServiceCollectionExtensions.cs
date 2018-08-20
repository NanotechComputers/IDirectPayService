using System;
using Microsoft.Extensions.DependencyInjection;

namespace DirectPay.Extensions
{
    // ReSharper disable once UnusedMember.Global
    public static class DirectPayServiceCollectionExtensions
    {
        // ReSharper disable once UnusedMember.Global
        public static IServiceCollection AddDirectPay(this IServiceCollection collection, Action<DirectPayServiceOptions> setupAction)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (setupAction == null) throw new ArgumentNullException(nameof(setupAction));

            collection.Configure(setupAction);

            return collection.AddScoped<IDirectPayService, DirectPayService>();
        }
    }
}