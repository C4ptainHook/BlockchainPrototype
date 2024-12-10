using System.Globalization;
using Blockchain.Business.Interfaces.Utils;

namespace Blockchain.Business.Services;

public class CultureService : ICultureService
{
    public CultureInfo CurrentCulture => CultureInfo.InvariantCulture;
}
