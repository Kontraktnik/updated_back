namespace Application.Resource;
using Microsoft.Extensions.Localization;

public interface ILocalize
{
}
public class Localize : ILocalize
{
    private readonly IStringLocalizer _localizer;
    public Localize(IStringLocalizer<Localize> localizer)
    {
        _localizer = localizer;
    }

    public string this[string index]
    {
        get
        {
            return _localizer[index];
        }
    }
}