namespace DoubltTravel.Data.AssistenceInfos
{
    using System.Threading.Tasks;

    using Models;

    public interface IAssistenceInfoRepository
    {
        Task<AssistenceInfo> GetByIdAsync(int id);

        Task<int> InsertAsync(AssistenceInfo assistenceInfo);

        Task<int> UpdateAsync(int id, AssistenceInfo assistenceInfo);
    }
}