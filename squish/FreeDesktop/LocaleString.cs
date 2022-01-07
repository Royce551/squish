using System.Collections.Generic;
using System.Globalization;

namespace Squish.FreeDesktop;

public class LocaleString : Dictionary<CultureInfo, string>
{
    public string Default { get; }
    public LocaleString(string @default, IDictionary<CultureInfo, string> dict) : base(dict)
    {
        Default = @default;
    }
}