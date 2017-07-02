using DoubltTravel.Data.Models;
using TravelStateUpdater.UsaGovermentIntegration.Models;

namespace TravelStateUpdater.Core.Factories
{
    public static class AssistanceInfoFactory
    {
        public static AssistenceInfo Create(UsaAssistenceInfo sourceInfo)
        {
            var info = new AssistenceInfo
            {
                Email = sourceInfo.Email,
                Fax = sourceInfo.Fax,
                Globe = sourceInfo.Globe,
                Phone = sourceInfo.Phone,
                Title = sourceInfo.Name
            };

            return info;
        }

        public static void Update(AssistenceInfo infoToUpdate, UsaAssistenceInfo sourceInfo)
        {
            infoToUpdate.Email = sourceInfo.Email;
            infoToUpdate.Fax = sourceInfo.Fax;
            infoToUpdate.Globe = sourceInfo.Globe;
            infoToUpdate.Phone = sourceInfo.Phone;
            infoToUpdate.Title = sourceInfo.Name;
        }
    }
}
