namespace Balea.AspNetCore.Endpoints;

internal class PolicyResponseMapper : IEntityMapper<Policy, PolicyResponse>
{
    public void FromEntity(Policy source, PolicyResponse destination)
    {
        destination.Id = source.Id;
        destination.Name = source.Name;
        destination.Description = source.Description;
        destination.Content = source.Content;
    }

    public void ToEntity(PolicyResponse source, Policy destination)
    {
        destination.Id = source.Id;
        destination.Name = source.Name;
        destination.Description = source.Description;
        destination.Content = source.Content;
    }
}
