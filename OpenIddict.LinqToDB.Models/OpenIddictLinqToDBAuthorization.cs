/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/openiddict/openiddict-core for more information concerning
 * the license and the contributors participating to this project.
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using LinqToDB.Mapping;

namespace OpenIddict.LinqToDB.Models
{
    /// <summary>
    /// Represents an OpenIddict authorization.
    /// </summary>
    [Table(Name = "OpenIddictAuthorizations")]
    public class OpenIddictLinqToDBAuthorization : OpenIddictLinqToDBAuthorization<string, OpenIddictLinqToDBApplication, OpenIddictLinqToDBToken>
    {
        public OpenIddictLinqToDBAuthorization()
        {
            // Generate a new string identifier.
            Id = Guid.NewGuid().ToString();
        }
    }

    /// <summary>
    /// Represents an OpenIddict authorization.
    /// </summary>
    public class OpenIddictLinqToDBAuthorization<TKey> : OpenIddictLinqToDBAuthorization<TKey, OpenIddictLinqToDBApplication<TKey>, OpenIddictLinqToDBToken<TKey>>
        where TKey : IEquatable<TKey>
    {
    }

    /// <summary>
    /// Represents an OpenIddict authorization.
    /// </summary>
    [DebuggerDisplay("Id = {Id.ToString(),nq} ; Subject = {Subject,nq} ; Type = {Type,nq} ; Status = {Status,nq}")]
    public class OpenIddictLinqToDBAuthorization<TKey, TApplication, TToken>
        where TKey : IEquatable<TKey>
        where TApplication : class
        where TToken : class
    {
        /// <summary>
        /// Gets or sets the application associated with the current authorization.
        /// </summary>
        public virtual TApplication? Application { get; set; }

        /// <summary>
        /// Gets or sets the concurrency token.
        /// </summary>
        [Column(Name = "ConcurrencyToken")]
        public virtual string? ConcurrencyToken { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Gets or sets the UTC creation date of the current authorization.
        /// </summary>
        [Column(Name = "CreationDate")]
        public virtual DateTime? CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier associated with the current authorization.
        /// </summary>
        [PrimaryKey, Identity, Column(Name = "Id", SkipOnInsert = false, SkipOnUpdate = true)]
        public virtual TKey? Id { get; set; }

        /// <summary>
        /// Gets or sets the additional properties serialized as a JSON object,
        /// or <c>null</c> if no bag was associated with the current authorization.
        /// </summary>
        [Column(Name = "Properties")]
        public virtual string? Properties { get; set; }

        /// <summary>
        /// Gets or sets the scopes associated with the current
        /// authorization, serialized as a JSON array.
        /// </summary>
        [Column(Name = "Scopes")]
        public virtual string? Scopes { get; set; }

        /// <summary>
        /// Gets or sets the status of the current authorization.
        /// </summary>
        [Column(Name = "Status")]
        public virtual string? Status { get; set; }

        /// <summary>
        /// Gets or sets the subject associated with the current authorization.
        /// </summary>
        [Column(Name = "Subject")]
        public virtual string? Subject { get; set; }

        /// <summary>
        /// Gets the list of tokens associated with the current authorization.
        /// </summary>
        public virtual ICollection<TToken> Tokens { get; } = new HashSet<TToken>();

        /// <summary>
        /// Gets or sets the type of the current authorization.
        /// </summary>
        [Column(Name = "Type")]
        public virtual string? Type { get; set; }
    }
}
