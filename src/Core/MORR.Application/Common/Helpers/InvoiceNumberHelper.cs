using System.Text;

namespace MORR.Application.Common.Helpers
{
    public class InvoiceNumberHelper
    {
        public static string GenerateInvoiceNumber()
        {
            var length = 8;
            var random = new Random();

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            var code = new StringBuilder(length);

            for (int index = 0; index < length; index++)
            {
                code.Append(chars[random.Next(chars.Length)]);
            }

            return code.ToString();
        }
    }
}
