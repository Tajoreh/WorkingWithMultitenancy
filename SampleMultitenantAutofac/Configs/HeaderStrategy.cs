using Autofac.Multitenant;

namespace SampleMultitenantAutofac.Configs;

public class HeaderStrategy : ITenantIdentificationStrategy
{
    private readonly IHttpContextAccessor _accessor;

    public HeaderStrategy(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }

    public bool TryIdentifyTenant(out object tenantId)
    {

        var db = _accessor.HttpContext?.Request?.Headers["environment"];


        var key = "All";

        if (!string.IsNullOrEmpty(db))
        {
            key = db.Value;


        }
        tenantId = key;

        return tenantId != null;
    }
}