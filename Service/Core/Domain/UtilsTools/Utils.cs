using System.Net.Mail;

namespace Domain.UtilsTools
{
    public class Utils
    {
        public static bool IsValidEmail(string emailAddres)
        {
            try
            {
                var address = new MailAddress(emailAddres);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
