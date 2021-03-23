/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/openiddict/openiddict-core for more information concerning
 * the license and the contributors participating to this project.
 */

using System;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OpenIddict.LinqToDB;
using OpenIddict.LinqToDB.Models;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Exposes extensions allowing to register the OpenIddict Linq2Db services.
    /// </summary>
    public static class OpenIddictLinqToDBExtensions
    {
        /// <summary>
        /// Registers the Linq2Db stores services in the DI container and
        /// configures OpenIddict to use the Linq2Db entities by default.
        /// </summary>
        /// <param name="builder">The services builder used by OpenIddict to register new services.</param>
        /// <remarks>This extension can be safely called multiple times.</remarks>
        /// <returns>The <see cref="OpenIddictLinqToDBBuilder"/>.</returns>
        public static OpenIddictLinqToDBBuilder UseLinqToDB(this OpenIddictCoreBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            // Since Entity Framework Core may be used with databases performing case-insensitive
            // or culture-sensitive comparisons, ensure the additional filtering logic is enforced
            // in case case-sensitive stores were registered before this extension was called.
            builder.Configure(options => options.DisableAdditionalFiltering = false);

            builder.SetDefaultApplicationEntity<OpenIddictLinqToDBApplication>()
                   .SetDefaultAuthorizationEntity<OpenIddictLinqToDBAuthorization>()
                   .SetDefaultScopeEntity<OpenIddictLinqToDBScope>()
                   .SetDefaultTokenEntity<OpenIddictLinqToDBToken>();

            builder.ReplaceApplicationStoreResolver<OpenIddictLinqToDBApplicationStoreResolver>()
                   .ReplaceAuthorizationStoreResolver<OpenIddictLinqToDBAuthorizationStoreResolver>()
                   .ReplaceScopeStoreResolver<OpenIddictLinqToDBScopeStoreResolver>()
                   .ReplaceTokenStoreResolver<OpenIddictLinqToDBTokenStoreResolver>();

            builder.Services.TryAddSingleton<OpenIddictLinqToDBApplicationStoreResolver.TypeResolutionCache>();
            builder.Services.TryAddSingleton<OpenIddictLinqToDBAuthorizationStoreResolver.TypeResolutionCache>();
            builder.Services.TryAddSingleton<OpenIddictLinqToDBScopeStoreResolver.TypeResolutionCache>();
            builder.Services.TryAddSingleton<OpenIddictLinqToDBTokenStoreResolver.TypeResolutionCache>();

            builder.Services.TryAddScoped(typeof(OpenIddictLinqToDBApplicationStore<,,,,>));
            builder.Services.TryAddScoped(typeof(OpenIddictLinqToDBAuthorizationStore<,,,,>));
            builder.Services.TryAddScoped(typeof(OpenIddictLinqToDBScopeStore<,,>));
            builder.Services.TryAddScoped(typeof(OpenIddictLinqToDBTokenStore<,,,,>));

            return new OpenIddictLinqToDBBuilder(builder.Services);
        }

        /// <summary>
        /// Registers the Linq2Db stores services in the DI container and
        /// configures OpenIddict to use the Linq2Db entities by default.
        /// </summary>
        /// <param name="builder">The services builder used by OpenIddict to register new services.</param>
        /// <param name="configuration">The configuration delegate used to configure the Linq2Db services.</param>
        /// <remarks>This extension can be safely called multiple times.</remarks>
        /// <returns>The <see cref="OpenIddictCoreBuilder"/>.</returns>
        public static OpenIddictCoreBuilder UseLinqToDB(
            this OpenIddictCoreBuilder builder, Action<OpenIddictLinqToDBBuilder> configuration)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            configuration(builder.UseLinqToDB());

            return builder;
        }
    }
}