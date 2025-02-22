﻿using Orca.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace Orca.Authorization.Abac.Context
{
    /// <summary>
    /// A property bag that collect information about user claims and allow to use this on 
    /// Abac authorizacion policy with the Subject accessor. This property bag map standard
    /// claim names to more simple name identifiers using the <see cref="ClaimMapping"/> dictionary.
    /// </summary>
    public class UserPropertyBag
        : IAspNetPropertyBag
    {
        /// <summary>
        /// The Claim mapping dictionary used to translate some standard claims like ClaimTypes.Role, ClaimTypesName to
        /// more simple name identifiers ( Subject.Role and Subject.Name). You can add or modify existing entries to 
        /// include new simplified name identifiers or modify existing mapping.
        /// </summary>
        public static Dictionary<string, IEnumerable<string>> ClaimMapping = new Dictionary<string, IEnumerable<string>>()
        {
            { "Role", new string[] { ClaimTypes.Role, "role" } },
            { "Name", new string[] { ClaimTypes.Name, "name" } },
            { "Sub", new string[] { ClaimTypes.NameIdentifier, "sub" } },
            { "Email", new string[] { ClaimTypes.Email, "email" } },
            { "GivenName", new string[] { ClaimTypes.GivenName, "given_name" } },
            { "FamilyName", new string[] { ClaimTypes.Surname, "family_name" } }
        };

        private IEnumerable<Claim> _claims;
        private readonly ILogger<UserPropertyBag> _logger;

        ///<inheritdoc/>
        public string Name { get; } = "Subject";

       ///<inheritdoc/>
        public object this[string propertyName]
        {
            get
            {
                // You use equal expression for this property bag but the item can 
                // contain multiple elements ( multiple role values for example ) , probably in this case CONTAINS will be 
                // a more apropiate operator but we need to solve this scenario, for that
                // we use the first element on the collection.

                IEnumerable<string> claimTypes = ClaimMapping.ContainsKey(propertyName) 
                    ? ClaimMapping[propertyName] : new string[] { propertyName };

                var value = _claims
                    .Where(c => claimTypes.Contains(c.Type))
                    .FirstOrDefault();

                if (value is object)
                {
                    return value.Value;
                }

                throw new ArgumentException($"The property name {propertyName} does not exist on the {Name}  property bag.");
            }
        }

        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <param name="logger">The logger to be used.</param>
        public UserPropertyBag(ILogger<UserPropertyBag> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _claims = new List<Claim>();
        }

        ///<inheritdoc/>
        public bool Contains(string propertyName, object value)
        {
            // This method is used when the grammar use CONTAINS operator, in this we don't need
            // to perfrom conversion type operations and check only if the Claims of the selected user 
            // exist and the value is on this collection.

            IEnumerable<string> claimTypes = ClaimMapping.ContainsKey(propertyName)
                   ? ClaimMapping[propertyName] : new string[] { propertyName };

            return _claims
                .Where(c => claimTypes.Contains(c.Type) && c.Value.Equals(value.ToString(), StringComparison.InvariantCultureIgnoreCase))
                .Any();
        }

        ///<inheritdoc/>
        public Task Initialize(AuthorizationHandlerContext authorizationHandlerContext)
        {
            if (authorizationHandlerContext != null && authorizationHandlerContext.User is ClaimsPrincipal claimsPrincipal)
            {
                _logger.PopulatePropertyBag(Name);
                _claims = claimsPrincipal.Claims;
            }
            else
            {
                _logger.PropertyBagCantBePopulated(Name);
            }

            return Task.CompletedTask;
        }
    }
}
