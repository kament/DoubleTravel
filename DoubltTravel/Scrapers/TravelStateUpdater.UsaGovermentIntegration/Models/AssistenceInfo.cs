namespace TravelStateUpdater.UsaGovermentIntegration.Models
{
    public class AssistenceInfo
    {
        public AssistenceInfo(string name, string phone, string fax, string email, string globe)
        {
            Name = name;
            Phone = phone;
            Fax = fax;
            Email = email;
            Globe = globe;
        }

        public string Email { get; private set; }

        public string Fax { get; private set; }

        public string Globe { get; private set; }

        public string Name { get; private set; }

        public string Phone { get; private set; }
    }
}