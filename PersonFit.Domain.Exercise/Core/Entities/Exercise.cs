using PersonFit.Core;
using PersonFit.Core.Aggregations;
using PersonFit.Domain.Exercise.Core.Events;
using PersonFit.Domain.Exercise.Core.ValueObjects;

namespace PersonFit.Domain.Exercise.Core.Entities;

internal sealed class Exercise : AggregateRoot, IAggregateRoot
{
    private readonly ISet<MediaContent> _contents = new HashSet<MediaContent>();
    private readonly ISet<string> _tags = new HashSet<string>();

    public IEnumerable<string> Tags
    {
        get => _tags;
        private init => _tags = new HashSet<string>(value);
    }
    
    public IEnumerable<MediaContent> Contents
    {
        get => _contents;
        private init => _contents = new HashSet<MediaContent>(value);
    }
    public string Name { get; private set; }
    public string Description { get; private set; }

    public Exercise(Guid id, string name, string description, IEnumerable<string> tags, IEnumerable<MediaContent> mediaContents,  int version = 0)
    {
        Id = id;
        Name = name;
        Description = description;
        Version = version;
        Contents = mediaContents;
        Tags = tags;
    }
    
    public static Exercise Create(AggregateId id, string name, string description, string[] tags)
    {
        var tagsArray = tags.ToArray();
        var exercise = new Exercise(id, name, description, Enumerable.Empty<string>(),Enumerable.Empty<MediaContent>());
        exercise.AddEvent(new ExerciseCreatedEvent(id, name, description));
        if (tagsArray.Any())
        {
            exercise.AssignTags(tagsArray);
        }
        return exercise;
    }

    public void AssignTags(string[] tags)
    {
        bool raiseEvent = false;
        foreach (var tag in tags)
        {
            if (_tags.Contains(tag))
            {
                continue;
            }

            _tags.Add(tag);
            raiseEvent = true;
        }

        if (raiseEvent)
        {
            AddEvent(new AssignedTagsEvent(tags));
        }
    }

    public void UnassignTags(string[] tags)
    {
        bool raiseEvent = false;
        foreach (var tag in tags)
        {
            if (!_tags.Contains(tag))
            {
                continue;
            }

            _tags.Remove(tag);
            raiseEvent = true;
        }

        if (raiseEvent)
        {
            AddEvent(new UnassignedTagsEvent(tags));
        }
    }

    public void AddContent(MediaContent content)
    {
        if (_contents.Contains(content))
        {
            return;
        }

        _contents.Add(content);
        AddEvent(new MediaContentAddedEvent(Id, $"{content.Type}", content.Url));
    }
}