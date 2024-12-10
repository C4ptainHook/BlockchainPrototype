using System.Globalization;

namespace Blockchain.Business.Interfaces.Utils;

public interface ICultureService
{
    CultureInfo CurrentCulture { get; }
}
