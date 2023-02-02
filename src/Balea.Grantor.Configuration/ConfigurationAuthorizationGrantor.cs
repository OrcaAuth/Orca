﻿using Balea.Abstractions;
using Balea.Grantor.Configuration.Model;
using Balea.Model;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Balea.Grantor.Configuration
{
    public class ConfigurationAuthorizationGrantor : IAuthorizationGrantor
    {
        private readonly BaleaConfiguration _configuration;
        private readonly BaleaOptions _options;

        public ConfigurationAuthorizationGrantor(BaleaConfiguration configuration, BaleaOptions options)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public Task<AuthorizationContext> FindAuthorizationAsync(ClaimsPrincipal user, CancellationToken cancellationToken = default)
        {
            var sourceRoleClaims = user.GetClaimValues(_options.ClaimTypeMap.RoleClaimType);
            var application = _configuration.Applications.GetByName(_options.ApplicationName);

            if (application is null)
            {
                return Task.FromResult(new AuthorizationContext());
            }

            var delegation = application.Delegations.GetCurrentDelegation(user.GetSubjectId(_options));
            var subject = GetSubject(user, delegation);
            var roles = application.Roles
                    .Where(role =>
                        role.Enabled &&
                        role.Subjects.Contains(subject, StringComparer.InvariantCultureIgnoreCase) ||
                        role.Mappings.Any(m => sourceRoleClaims.Contains(m, StringComparer.InvariantCultureIgnoreCase)))
                    .Select(role => role.To());

            var authorization = new AuthorizationContext
            {
                Roles = roles,
                Delegation = delegation.To(),
            };

            return Task.FromResult(authorization);
        }

        public Task<Policy> GetPolicyAsync(string name, CancellationToken cancellationToken = default)
        {
            var application = _configuration.Applications.GetByName(_options.ApplicationName);

            if (application is null)
            {
                return Task.FromResult<Policy>(null);
            }

            var policy = application.Policies.FirstOrDefault(p => p.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));

            if (policy is null)
            {
                return Task.FromResult<Policy>(null);
            }

            return Task.FromResult(new Policy
            {
                Name = policy.Name,
                Content = policy.Content
            });
        }

        private string GetSubject(ClaimsPrincipal user, DelegationConfiguration delegation)
        {
            return delegation?.Who ?? user.GetSubjectId(_options);
        }
    }
}

