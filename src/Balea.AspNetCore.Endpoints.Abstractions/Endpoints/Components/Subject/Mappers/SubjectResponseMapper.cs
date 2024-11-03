namespace Balea.AspNetCore.Endpoints;

internal class SubjectResponseMapper : IEntityMapper<Subject, SubjectResponse>
{
    public void FromEntity(Subject source, SubjectResponse destination)
    {
        destination.Id = source.Id;
        destination.Sub = source.Sub;
        destination.Email = source.Email;
        destination.Name = source.Name;
    }

    public void ToEntity(SubjectResponse source, Subject destination)
    {
        destination.Id = source.Id;
        destination.Sub = source.Sub;
        destination.Email = source.Email;
        destination.Name = source.Name;
    }
}
