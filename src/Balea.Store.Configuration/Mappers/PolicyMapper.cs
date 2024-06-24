﻿namespace Balea.Store.Configuration;

internal class PolicyMapper : IEntityMapper<PolicyConfiguration, Policy>
{
    public void FromEntity(PolicyConfiguration source, Policy destination)
    {
        destination.Id = source.Id;
        destination.Name = source.Name;
        destination.Description = source.Description;
        destination.Content = source.Content;
    }

    public void ToEntity(Policy source, PolicyConfiguration destination)
    {
        destination.Id = source.Id;
        destination.Name = source.Name;
        destination.Description = source.Description;
        destination.Content = source.Content;
    }
}
