/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/openiddict/openiddict-core for more information concerning
 * the license and the contributors participating to this project.
 */

using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace OpenIddict.LinqToDB.Models
{
    /// <summary>
    /// Represents an OpenIddict application.
    /// </summary>
    [Table(Name = "OpenIddictApplications")]
    public class OpenIddictLinqToDBApplication : OpenIddictLinqToDBApplication<string, OpenIddictLinqToDBAuthorization, OpenIddictLinqToDBToken>
    {
        public OpenIddictLinqToDBApplication()
        {
            // Generate a new string identifier.
            Id = Guid.NewGuid().ToString();
        }
    }

    /// <summary>
    /// Represents an OpenIddict application.
    /// </summary>
    public class OpenIddictLinqToDBApplication<TKey> : OpenIddictLinqToDBApplication<TKey, OpenIddictLinqToDBAuthorization<TKey>, OpenIddictLinqToDBToken<TKey>>
        where TKey : IEquatable<TKey>
    {
    }

    /// <summary>
    /// Represents an OpenIddict application.
    /// </summary>
    [DebuggerDisplay("Id = {Id.ToString(),nq} ; ClientId = {ClientId,nq} ; Type = {Type,nq}")]
    public class OpenIddictLinqToDBApplication<TKey, TAuthorization, TToken>
        where TKey : IEquatable<TKey>
        where TAuthorization : class
        where TToken : class
    {
        /// <summary>
        /// Gets the list of the authorizations associated with this application.
        /// </summary>
        public virtual ICollection<TAuthorization> Authorizations { get; } = new HashSet<TAuthorization>();

        /// <summary>
        /// Gets or sets the client identifier associated with the current application.
        /// </summary>
        [Column(Name = "ClientId")]
        public virtual string? ClientId { get; set; }

        /// <summary>
        /// Gets or sets the client secret associated with the current application.
        /// Note: depending on the application manager used to create this instance,
        /// this property may be hashed or encrypted for security reasons.
        /// </summary>
        [Column(Name = "ClientSecret")]
        public virtual string? ClientSecret { get; set; }

        /// <summary>
        /// Gets or sets the concurrency token.
        /// </summary>
        [Column(Name = "ConcurrencyToken")]
        public virtual string? ConcurrencyToken { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Gets or sets the consent type associated with the current application.
        /// </summary>
        [Column(Name = "ConsentType")]
        public virtual string? ConsentType { get; set; }

        /// <summary>
        /// Gets or sets the display name associated with the current application.
        /// </summary>
        [Column(Name = "DisplayName")]
        public virtual string? DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the localized display names
        /// associated with the current application,
        /// serialized as a JSON object.
        /// </summary>
        [Column(Name = "DisplayNames")]
        public virtual string? DisplayNames { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier associated with the current application.
        /// </summary>
        /// 
        [PrimaryKey, Identity, Column(Name = "Id", SkipOnInsert = false, SkipOnUpdate = true)]
        public virtual TKey? Id { get; set; }

        /// <summary>
        /// Gets or sets the permissions associated with the
        /// current application, serialized as a JSON array.
        /// </summary>
        [Column(Name = "Permissions")]
        public virtual string? Permissions { get; set; }

        /// <summary>
        /// Gets or sets the logout callback URLs associated with
        /// the current application, serialized as a JSON array.
        /// </summary>
        [Column(Name = "PostLogoutRedirectUris")]
        public virtual string? PostLogoutRedirectUris { get; set; }

        /// <summary>
        /// Gets or sets the additional properties serialized as a JSON object,
        /// or <c>null</c> if no bag was associated with the current application.
        /// </summary>
        [Column(Name = "Properties")]
        public virtual string? Properties { get; set; }

        /// <summary>
        /// Gets or sets the callback URLs associated with the
        /// current application, serialized as a JSON array.
        /// </summary>
        [Column(Name = "RedirectUris")]
        public virtual string? RedirectUris { get; set; }

        /// <summary>
        /// Gets or sets the requirements associated with the
        /// current application, serialized as a JSON array.
        /// </summary>
        [Column(Name = "Requirements")]
        public virtual string? Requirements { get; set; }

        /// <summary>
        /// Gets the list of the tokens associated with this application.
        /// </summary>
        public virtual ICollection<TToken> Tokens { get; } = new HashSet<TToken>();

        /// <summary>
        /// Gets or sets the application type associated with the current application.
        /// </summary>
        [Column(Name = "Type")]
        public virtual string? Type { get; set; }
    }
}