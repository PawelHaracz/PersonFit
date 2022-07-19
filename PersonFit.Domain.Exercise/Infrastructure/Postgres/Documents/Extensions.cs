using PersonFit.Domain.Exercise.Core.Enums;
using PersonFit.Domain.Exercise.Core.ValueObjects;

namespace PersonFit.Domain.Exercise.Infrastructure.Postgres.Documents;

internal static class Extensions
{
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