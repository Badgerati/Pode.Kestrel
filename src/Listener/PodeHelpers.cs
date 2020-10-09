using System;
using System.Security.Cryptography;

namespace PodeKestrel
{
    public class PodeHelpers
    {

        public static void WriteException(Exception ex, PodeListener listener = default(PodeListener))
        {
            if (ex == default(Exception))
            {
                return;
            }

            if (listener != default(PodeListener) && !listener.ErrorLoggingEnabled)
            {
                return;
            }

            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
        }

        public static string NewGuid(int length = 16)
        {
            using (var rnd = RandomNumberGenerator.Create())
            {
                var bytes = new byte[length];
                rnd.GetBytes(bytes);
                return (new Guid(bytes)).ToString();
            }
        }
    }
}