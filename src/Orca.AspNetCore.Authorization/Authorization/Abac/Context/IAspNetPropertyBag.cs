﻿using Microsoft.AspNetCore.Authorization;

namespace Orca.Authorization.Abac.Context
{
    /// <inheritdoc />
    public interface IAspNetPropertyBag : IPropertyBag
    	{
        /// <summary>
        /// Initialize the property bag.
        /// </summary>
        /// <param name="authorizationHandlerContext">The authorization handler context to use.</param>
        /// <returns>A Task that complete when service finished.</returns>
        Task Initialize(AuthorizationHandlerContext authorizationHandlerContext);
    }
}
