using System;
using System.Linq;
using System.Security.Cryptography;

namespace PodeKestrel
{
    public class PodeHelpers
    {

        public static void WriteException(Exception ex, PodeListener listener = default(PodeListener), PodeLoggingLevel level = PodeLoggingLevel.Error)
        {
            if (ex == default(Exception))
            {
                return;
            }

            if (listener != default(PodeListener) && (!listener.ErrorLoggingEnabled || !listener.ErrorLoggingLevels.Contains(level.ToString(), StringComparer.InvariantCultureIgnoreCase)))
            {
                return;
            }

            Console.WriteLine($"[{level}] {ex.GetType().Name}: {ex.Message}");
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