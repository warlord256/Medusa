namespace Medusa
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
  
    public static class StorageConfigHandler
    {
        private static readonly string cfgStorageConnectionString = "StorageConnectionString";
        private static readonly string cfgDestContainer = "destCont";
        private static readonly string cfgQueue = "queue";

        /// <summary>
        /// Returns the storage connection for <>
        /// </summary>
        /// <returns>Returns the StorageConnectionString</returns>
        public static string StorageConnectionString()
        {
            return ConfigurationManager.ConnectionStrings[cfgStorageConnectionString].ConnectionString;
        }

        /// <summary>
        /// Returns the storage connection for <>
        /// </summary>
        /// <returns>Returns the StorageConnectionString</returns>
        public static string DestContainer()
        {
            return ConfigurationManager.AppSettings[cfgDestContainer];
        }

        /// <summary>
        /// Returns the storage connection for <>
        /// </summary>
        /// <returns>Returns the StorageConnectionString</returns>
        public static string Queue()
        {
            return StorageConfigHandler.cfgQueue;
        }
    }
}
