using System;

namespace Registry.Data
{
    public static class Check
    {
        public static void CheckStringParameters(this string parameter)
        {
            if (parameter == null)
                throw new ArgumentNullException();
            else if (parameter.Trim().Length == 0)
                throw new ArgumentException();
        }
    }
}
