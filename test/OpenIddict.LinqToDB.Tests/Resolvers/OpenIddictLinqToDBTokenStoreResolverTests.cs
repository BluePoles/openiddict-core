/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/openiddict/openiddict-core for more information concerning
 * the license and the contributors participating to this project.
 */

using LinqToDB;
using LinqToDB.Data;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moq;
using OpenIddict.Abstractions;
using OpenIddict.LinqToDB.Models;
using System;
using Xunit;
using static OpenIddict.LinqToDB.OpenIddictLinqToDBTokenStoreResolver;
using SR = OpenIddict.Abstractions.OpenIddictResources;

namespace OpenIddict.LinqToDB.Tests
{
    public class OpenIddictLinqToDBTokenStoreResolverTests
    {
        [Fact]
        public void Get_ReturnsCustomStoreCorrespondingToTheSpecifiedTypeWhenAvailable()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddSingleton(Mock.Of<IOpenIddictTokenStore<CustomToken>>());

            var provider = services.BuildServiceProvider();
            var options = Mock.Of<IOptionsMonitor<OpenIddictLinqToDBOptions>>();
            var resolver = new OpenIddictLinqToDBTokenStoreResolver(new TypeResolutionCache(), options, provider);

            // Act and assert
            Assert.NotNull(resolver.Get<CustomToken>());
        }

        [Fact]
        public void Get_ThrowsAnExceptionForInvalidEntityType()
        {
            // Arrange
            var services = new ServiceCollection();

            var provider = services.BuildServiceProvider();
            var options = Mock.Of<IOptionsMonitor<OpenIddictLinqToDBOptions>>();
            var resolver = new OpenIddictLinqToDBTokenStoreResolver(new TypeResolutionCache(), options, provider);

            // Act and assert
            var exception = Assert.Throws<InvalidOperationException>(() => resolver.Get<CustomToken>());

            Assert.Equal(SR.GetResourceString(SR.ID0260), exception.Message);
        }

        private static OpenIddictLinqToDBTokenStore<MyToken, MyApplication, MyAuthorization, DataConnection, long> CreateStore()
            => new Mock<OpenIddictLinqToDBTokenStore<MyToken, MyApplication, MyAuthorization, DataConnection, long>>(
                Mock.Of<IMemoryCache>(),
                Mock.Of<DataConnection>()).Object;

        public class CustomToken { }

        public class MyApplication : OpenIddictLinqToDBApplication<long, MyAuthorization, MyToken> { }
        public class MyAuthorization : OpenIddictLinqToDBAuthorization<long, MyApplication, MyToken> { }
        public class MyScope : OpenIddictLinqToDBScope<long> { }
        public class MyToken : OpenIddictLinqToDBToken<long, MyApplication, MyAuthorization> { }
    }
}
