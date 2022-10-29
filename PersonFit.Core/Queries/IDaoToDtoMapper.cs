namespace PersonFit.Core.Queries;

public interface IDaoToDtoMapper<in TDao, out TDto>
{
    TDto Map(TDao dao);
}