namespace PersonFit.Domain.Exercise.Api.Dtos.Commands;

public record UploadImageCommandDto(Guid Id, byte[] Image);