using BeaniesUtilities.Models;
using Gay.TCazier.DatabaseParser.Services.Interfaces;

namespace Gay.TCazier.DatabaseParser.Services;

public abstract class BaseModelService: IBaseModelService
{
    public int GetNextCommonID(IEnumerable<BaseModel> models)
    {
        int id = -1;
        try
        {
            id = models.Max(x => x.CommonIdentity);
        }
        catch (Exception ex)
        {
        }
        return id + 1;
    }
}
