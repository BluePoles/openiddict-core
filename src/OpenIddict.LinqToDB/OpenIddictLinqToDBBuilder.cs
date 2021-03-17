/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/openiddict/openiddict-core for more information concerning
 * the license and the contributors participating to this project.
 */

using System;
using System.ComponentModel;
using LinqToDB;
using OpenIddict.Core;
using OpenIddict.LinqToDB;
using OpenIddict.LinqToDB.Models;
using SR = OpenIddict.Abstractions.OpenIddictResources;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Exposes the necessary methods required to configure the OpenIddict Linq2Db services.
    /// </summary>
    public class OpenIddictLinqToDBBuilder
    {
        /// <summary>
        /// Initializes a new instance of <see cref="OpenIddictLinqToDBBuilder"/>.
        /// </summary>
        /// <param name="services">The services collection.</param>
        public OpenIddictLinqToDBBuilder(IServiceCollection services)
            => Services = services ?? throw new ArgumentNullException(nameof(services));

        /// <summary>
        /// Gets the services collection.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public IServiceCollection Services { get; }

        /// <summary>
        /// Amends the default OpenIddict Linq2Db configuration.
        /// </summary>
        /// <param name="configuration">The delegate used to configure the OpenIddict options.</param>
        /// <remarks>This extension can be safely called multiple times.</remarks>
        /// <returns>The <see cref="OpenIddictLinqToDBBuilder"/>.</returns>
        public OpenIddictLinqToDBBuilder Configure(Action<OpenIddictLinqToDBOptions> configuration)
        {
            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            Services.Configure(configuration);

            return this;
        }

        /// <summary>
        /// Configures OpenIddict to use the default OpenIddict
        /// Linq2Db entities, with the specified key type.
        /// </summary>
        /// <returns>The <see cref="OpenIddictLinqToDBBuilder"/>.</returns>
        public OpenIddictLinqToDBBuilder ReplaceDefaultEntities<TKey>()
            where TKey : IEquatable<TKey>
            => ReplaceDefaultEntities<OpenIddictLinqToDBApplication<TKey>,
                                      OpenIddictLinqToDBAuthorization<TKey>,
                                      OpenIddictLinqToDBScope<TKey>,
                                      OpenIddictLinqToDBToken<TKey>, TKey>();

        /// <summary>
        /// Configures OpenIddict to use the specified entities, derived
        /// from the default OpenIddict Linq2Db entities.
        /// </summary>
        /// <returns>The <see cref="OpenIddictLinqToDBBuilder"/>.</returns>
        public OpenIddictLinqToDBBuilder ReplaceDefaultEntities<TApplication, TAuthorization, TScope, TToken, TKey>()
            where TApplication : OpenIddictLinqToDBApplication<TKey, TAuthorization, TToken>
            where TAuthorization : OpenIddictLinqToDBAuthorization<TKey, TApplication, TToken>
            where TScope : OpenIddictLinqToDBScope<TKey>
            where TToken : OpenIddictLinqToDBToken<TKey, TApplication, TAuthorization>
            where TKey : IEquatable<TKey>
        {
            Services.Configure<OpenIddictCoreOptions>(options =>
            {
                options.DefaultApplicationType = typeof(TApplication);
                options.DefaultAuthorizationType = typeof(TAuthorization);
                options.DefaultScopeType = typeof(TScope);
                options.DefaultTokenType = typeof(TToken);
            });

            return this;
        }

        /// <summary>
        /// Configures the OpenIddict Linq2Db stores to use the specified database context type.
        /// </summary>
        /// <typeparam name="TContext">The type of the <see cref="DataContext"/> used by OpenIddict.</typeparam>
        /// <returns>The <see cref="OpenIddictLinqToDBBuilder"/>.</returns>
        public OpenIddictLinqToDBBuilder UseDataContext<TContext>()
            where TContext : DataContext
            => UseDataContext(typeof(TContext));

        /// <summary>
        /// Configures the OpenIddict Linq2Db stores to use the specified database context type.
        /// </summary>
        /// <param name="type">The type of the <see cref="DataContext"/> used by OpenIddict.</param>
        /// <returns>The <see cref="OpenIddictLinqToDBBuilder"/>.</returns>
        public OpenIddictLinqToDBBuilder UseDataContext(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (!typeof(DataContext).IsAssignableFrom(type))
            {
                throw new ArgumentException(SR.GetResourceString(SR.ID0232), nameof(type));
            }

            return Configure(options => options.DataContextType = type);
        }

        /// <inheritdoc/>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object? obj) => base.Equals(obj);

        /// <inheritdoc/>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode() => base.GetHashCode();

        /// <inheritdoc/>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string? ToString() => base.ToString();
    }
}
