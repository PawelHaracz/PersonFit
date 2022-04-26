﻿
using PersonFit.Domain.Exercise.ValueObjects;

namespace PersonFit.Domain.Exercise;
using Core;
using Events;

internal sealed class Exercise : AggregateRoot, IAggregateRoot
{
    private ISet<MediaContent> _contents = new HashSet<MediaContent>();
    private ISet<string> _tags = new HashSet<string>();

    public IEnumerable<string> Tags
    {
        get => _tags;
        private set => _tags = new HashSet<string>(value);
    }
    
    public IEnumerable<MediaContent> Contents
    {
        get => _contents;
        private set => _contents = new HashSet<MediaContent>(value);
    }
    public string Name { get; private set; }
    public string Description { get; private set; }

    private Exercise(Guid id, string name, string description, IEnumerable<string> tags, IEnumerable<MediaContent> mediaContents,  int version = 0)
    {
        Id = id;
        Name = name;
        Description = description;
        Version = version;
        Contents = mediaContents;
        Tags = tags;
    }
    
    public static Exercise Create(Guid id, string name, string description, string[] tags)
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
}