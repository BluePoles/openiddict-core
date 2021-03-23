/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/openiddict/openiddict-core for more information concerning
 * the license and the contributors participating to this project.
 */

using LinqToDB;
using LinqToDB.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OpenIddict.Core;
using OpenIddict.LinqToDB.Models;
using System;
using Xunit;
using SR = OpenIddict.Abstractions.OpenIddictResources;

namespace OpenIddict.LinqToDB.Tests
{
    public class OpenIddictLinqToDBBuilderTests
    {
        [Fact]
        public void Constructor_ThrowsAnExceptionForNullServices()
        {
            // Arrange
            var services = (IServiceCollection)null!;

            // Act and assert
            var exception = Assert.Throws<ArgumentNullException>(() => new OpenIddictLinqToDBBuilder(services));

            Assert.Equal("services", exception.ParamName);
        }

        [Fact]
        public void ReplaceDefaultEntities_EntitiesAreCorrectlyReplaced()
        {
            // Arrange
            var services = CreateServices();
            var builder = CreateBuilder(services);

            // Act
            builder.ReplaceDefaultEntities<CustomApplication, CustomAuthorization, CustomScope, CustomToken, long>();

            // Assert
            var provider = services.BuildServiceProvider();
            var options = provider.GetRequiredService<IOptionsMonitor<OpenIddictCoreOptions>>().CurrentValue;

            Assert.Equal(typeof(CustomApplication), options.DefaultApplicationType);
            Assert.Equal(typeof(CustomAuthorization), options.DefaultAuthorizationType);
            Assert.Equal(typeof(CustomScope), options.DefaultScopeType);
            Assert.Equal(typeof(CustomToken), options.DefaultTokenType);
        }

        [Fact]
        public void ReplaceDefaultEntities_AllowsSpecifyingCustomKeyType()
        {
            // Arrange
            var services = CreateServices();
            var builder = CreateBuilder(services);

            // Act
            builder.ReplaceDefaultEntities<long>();

            // Assert
            var provider = services.BuildServiceProvider();
            var options = provider.GetRequiredService<IOptionsMonitor<OpenIddictCoreOptions>>().CurrentValue;

            Assert.Equal(typeof(OpenIddictLinqToDBApplication<long>), options.DefaultApplicationType);
            Assert.Equal(typeof(OpenIddictLinqToDBAuthorization<long>), options.DefaultAuthorizationType);
            Assert.Equal(typeof(OpenIddictLinqToDBScope<long>), options.DefaultScopeType);
            Assert.Equal(typeof(OpenIddictLinqToDBToken<long>), options.DefaultTokenType);
        }

        private static OpenIddictLinqToDBBuilder CreateBuilder(IServiceCollection services)
            => services.AddOpenIddict().AddCore().UseLinqToDB();

        private static IServiceCollection CreateServices()
        {
            var services = new ServiceCollection();
            services.AddOptions();

            return services;
        }

        public class CustomApplication : OpenIddictLinqToDBApplication<long, CustomAuthorization, CustomToken> { }
        public class CustomAuthorization : OpenIddictLinqToDBAuthorization<long, CustomApplication, CustomToken> { }
        public class CustomScope : OpenIddictLinqToDBScope<long> { }
        public class CustomToken : OpenIddictLinqToDBToken<long, CustomApplication, CustomAuthorization> { }

        public class CustomDbContext : DataConnection
        {
            public CustomDbContext(string configurationString)
                : base(configurationString)
            {
            }
        }
    }
}
