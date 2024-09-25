using BeaniesUtilities.Models;

namespace Gay.TCazier.DatabaseParser.Services.Interfaces;

public interface IBaseModelService
{
    public int GetNextCommonID(IEnumerable<BaseModel> models);
}
