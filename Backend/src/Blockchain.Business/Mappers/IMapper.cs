namespace Blockchain.Business.Mappers;

public interface IMapper<From, To>
{
    To Map(From from);
    From Map(To to);
    IEnumerable<To> Map(IEnumerable<From> from);
    IEnumerable<From> Map(IEnumerable<To> to);
}
