﻿namespace Balea.Store.Configuration;

public class PolicyStore : IPolicyStore
{
    private readonly MemoryStoreOptions _options;

	public PolicyStore(MemoryStoreOptions options)
	{
		_options = options ?? throw new ArgumentNullException(nameof(options));
	}

    public Task<Policy> FindByIdAsync(string policyId, CancellationToken cancellationToken)
    {
        var policy = _options.Policies.FirstOrDefault(x => x.Id == policyId);

        return Task.FromResult(policy);
    }

    public Task<Policy> FindByNameAsync(string policyName, CancellationToken cancellationToken)
	{
		var policy = _options.Policies.FirstOrDefault(x => x.Name == policyName);

        return Task.FromResult(policy);
    }

    public Task<AccessControlResult> CreateAsync(Policy policy, CancellationToken cancellationToken)
	{
        policy.Id = Guid.NewGuid().ToString();
        _options.Policies.Add(policy);

        return Task.FromResult(AccessControlResult.Success);
	}

	public Task<AccessControlResult> UpdateAsync(Policy policy, CancellationToken cancellationToken)
	{
        return Task.FromResult(AccessControlResult.Success);
    }

	public Task<AccessControlResult> DeleteAsync(Policy policy, CancellationToken cancellationToken)
	{
        policy.Id = Guid.NewGuid().ToString();
        _options.Policies.Remove(policy);

        return Task.FromResult(AccessControlResult.Success);
    }

    public Task<IList<Policy>> ListAsync(CancellationToken cancellationToken)
    {
        var result = _options.Policies.ToList();

        return Task.FromResult<IList<Policy>>(result);
    }

    public Task<IList<Policy>> SearchAsync(PolicyFilter filter, CancellationToken cancellationToken = default)
    {
        var source = _options.Policies.AsQueryable();

        if (!string.IsNullOrEmpty(filter.Name))
        {
            var words = filter.Name.Split().Where(word => word != string.Empty);
            source = source.Where(policy => words.All(word => policy.Name.Contains(word)));
        }

        if (!string.IsNullOrEmpty(filter.Description))
        {
            var words = filter.Description.Split().Where(word => word != string.Empty);
            source = source.Where(policy => words.All(word => policy.Description.Contains(word)));
        }

        var result = source.ToList();

        return Task.FromResult<IList<Policy>>(result);
    }
}
