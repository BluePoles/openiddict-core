/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/openiddict/openiddict-core for more information concerning
 * the license and the contributors participating to this project.
 */

using System;
using LinqToDB;
using LinqToDB.DataProvider;

namespace OpenIddict.LinqToDB
{
    /// <summary>
    /// Provides various settings needed to configure
    /// the OpenIddict Linq2Db integration.
    /// </summary>
    public class OpenIddictLinqToDBOptions
    {
        //TODO: possible options if needed
        public Type? DataContextType { get; set; }
    }
}
