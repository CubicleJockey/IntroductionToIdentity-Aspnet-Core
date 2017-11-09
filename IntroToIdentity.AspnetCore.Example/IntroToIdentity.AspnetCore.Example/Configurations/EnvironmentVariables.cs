using System;
using System.Security;

namespace IntroToIdentity.AspnetCore.Example.Configurations
{
    public static class EnvironmentVariables
    {
        /// <exception cref="SecurityException">The caller does not have the required permission to perform this operation.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="variable">variable</paramref> is null.</exception>
        public static string GetDatabase => Environment.GetEnvironmentVariable("Database");
    }
}
