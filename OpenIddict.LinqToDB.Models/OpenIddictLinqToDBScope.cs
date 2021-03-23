/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/openiddict/openiddict-core for more information concerning
 * the license and the contributors participating to this project.
 */

using LinqToDB.Mapping;
using System;
using System.Diagnostics;

namespace OpenIddict.LinqToDB.Models
{
    /// <summary>
    /// Represents an OpenIddict scope.
    /// </summary>
    [Table(Name = "OpenIddictScopes")]
    public class OpenIddictLinqToDBScope : OpenIddictLinqToDBScope<string>
    {
        public OpenIddictLinqToDBScope()
        {
            // Generate a new string identifier.
            Id = Guid.NewGuid().ToString();
        }
    }

    /// <summary>
    /// Represents an OpenIddict scope.
    /// </summary>
    [DebuggerDisplay("Id = {Id.ToString(),nq} ; Name = {Name,nq}")]
    public class OpenIddictLinqToDBScope<TKey> where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Gets or sets the concurrency token.
        /// </summary>
        /// 
        [Column(Name = "ConcurrencyToken")]
        public virtual string? ConcurrencyToken { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Gets or sets the public description associated with the current scope.
        /// </summary>
        [Column(Name = "Description")]
        public virtual string? Description { get; set; }

        /// <summary>
        /// Gets or sets the localized public descriptions associated
        /// with the current scope, serialized as a JSON object.
        /// </summary>
        /// 
        [Column(Name = "Descriptions")]
        public virtual string? Descriptions { get; set; }

        /// <summary>
        /// Gets or sets the display name associated with the current scope.
        /// </summary>
        /// 
        [Column(Name = "DisplayName")]
        public virtual string? DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the localized display names
        /// associated with the current application,
        /// serialized as a JSON object.
        /// </summary>
        /// 
        [Column(Name = "DisplayNames")]
        public virtual string? DisplayNames { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier associated with the current scope.
        /// </summary>
        [PrimaryKey, Identity, Column(Name = "Id", SkipOnInsert = false, SkipOnUpdate = true)]
        public virtual TKey? Id { get; set; }

        /// <summary>
        /// Gets or sets the unique name associated with the current scope.
        /// </summary>
        [Column(Name = "Name")]
        public virtual string? Name { get; set; }

        /// <summary>
        /// Gets or sets the additional properties serialized as a JSON object,
        /// or <c>null</c> if no bag was associated with the current scope.
        /// </summary>
        [Column(Name = "Properties")]
        public virtual string? Properties { get; set; }

        /// <summary>
        /// Gets or sets the resources associated with the
        /// current scope, serialized as a JSON array.
        /// </summary>
        [Column(Name = "Resources")]
        public virtual string? Resources { get; set; }
    }
}
