namespace PersonFit.Domain.Exercise.Infrastructure.Postgres.Documents;
using Core.Enums;
using Core.ValueObjects;

internal static class Extensions
{
    public static Application.Dtos.ExerciseDto AsDto(this Core.Entities.Exercise document)
        => new(document.Id,
            document.Name,
            document.Tags);
    
    public static Core.Entities.Exercise AsEntity(this ExerciseDocument document)
        => new(document.Id,
            document.Name,
            document.Description,
            document.Tags,
            document.Media.Select(m => new MediaContent(m.Url, Enum.Parse<MediaContentType>(m.Type))), 
            document.Version);//todo change to event store
    
    public static ExerciseDocument AsDocument(this Core.Entities.Exercise entity)
        => new ()
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            Tags = entity.Tags,
            Version = entity.Version,
            Media = entity.Contents.Select(c => new Media()
            {
                Type = $"{c.Type}",
                Url = c.Url
            })
        };
}