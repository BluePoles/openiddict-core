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
using static OpenIddict.LinqToDB.OpenIddictLinqToDBApplicationStoreResolver;
using SR = OpenIddict.Abstractions.OpenIddictResources;

namespace OpenIddict.LinqToDB.Tests
{
    public class OpenIddictLinqToDBApplicationStoreResolverTests
    {
        [Fact]
        public void Get_ReturnsCustomStoreCorrespondingToTheSpecifiedTypeWhenAvailable()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddSingleton(Mock.Of<IOpenIddictApplicationStore<CustomApplication>>());

            var options = Mock.Of<IOptionsMonitor<OpenIddictLinqToDBOptions>>();
            var provider = services.BuildServiceProvider();
            var resolver = new OpenIddictLinqToDBApplicationStoreResolver(new TypeResolutionCache(), options, provider);

            // Act and assert
            Assert.NotNull(resolver.Get<CustomApplication>());
        }

        [Fact]
        public void Get_ThrowsAnExceptionForInvalidEntityType()
        {
            // Arrange
            var services = new ServiceCollection();

            var options = Mock.Of<IOptionsMonitor<OpenIddictLinqToDBOptions>>();
            var provider = services.BuildServiceProvider();
            var resolver = new OpenIddictLinqToDBApplicationStoreResolver(new TypeResolutionCache(), options, provider);

            // Act and assert
            var exception = Assert.Throws<InvalidOperationException>(() => resolver.Get<CustomApplication>());

            Assert.Equal(SR.GetResourceString(SR.ID0252), exception.Message);
        }

        private static OpenIddictLinqToDBApplicationStore<MyApplication, MyAuthorization, MyToken, DataConnection, long> CreateStore()
            => new Mock<OpenIddictLinqToDBApplicationStore<MyApplication, MyAuthorization, MyToken, DataConnection, long>>(
                Mock.Of<IMemoryCache>(),
                Mock.Of<DataConnection>()).Object;

        public class CustomApplication { }

        public class MyApplication : OpenIddictLinqToDBApplication<long, MyAuthorization, MyToken> { }
        public class MyAuthorization : OpenIddictLinqToDBAuthorization<long, MyApplication, MyToken> { }
        public class MyScope : OpenIddictLinqToDBScope<long> { }
        public class MyToken : OpenIddictLinqToDBToken<long, MyApplication, MyAuthorization> { }
    }
}