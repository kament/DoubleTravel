using DoubltTravel.Data.Models;

namespace DoubltTravel.Data.AssistenceInfos
{
    public interface IAssistenceInfoRepository
    {
        AssistenceInfo GetById(int id);
    }
}