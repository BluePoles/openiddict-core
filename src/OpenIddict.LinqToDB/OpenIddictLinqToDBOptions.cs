/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/openiddict/openiddict-core for more information concerning
 * the license and the contributors participating to this project.
 */

using System;
using LinqToDB;

namespace OpenIddict.LinqToDB
{
    /// <summary>
    /// Provides various settings needed to configure
    /// the OpenIddict Linq2Db integration.
    /// </summary>
    public class OpenIddictLinqToDBOptions
    {
        /// <summary>
        /// Gets or sets the concrete type of the <see cref="DataContext"/> used by the
        /// OpenIddict Linq2Db stores. If this property is not populated,
        /// an exception is thrown at runtime when trying to use the stores.
        /// </summary>
        public Type? DataContextType { get; set; }
    }
}
