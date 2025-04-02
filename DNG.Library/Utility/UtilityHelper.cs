using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace DNG.Library.Utility
{
    public static class UtilityHelper
    {
        /// <summary>
        /// Checks if a folder path is reachable.
        /// Returns true if the folder exists and is accessible; otherwise, false.
        /// </summary>
        public static bool IsFolderPathReachable(string folderPath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(folderPath)) return false;
                return Directory.Exists(folderPath);
            }
            catch (Exception)
            {
                return false; // Any error means it's unreachable.
            }
        }

        /// <summary>
        /// Checks if an active internet connection is available.
        /// Returns true if online, false otherwise.
        /// </summary>
        public static bool IsInternetAvailable()
        {
            try
            {
                using (Ping ping = new Ping())
                {
                    PingReply reply = ping.Send("8.8.8.8", 1000); // Google's Public DNS
                    return reply != null && reply.Status == IPStatus.Success;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}