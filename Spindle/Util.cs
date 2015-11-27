using System.Management;

namespace Spindle {
    public static class Util {
        /// <summary>
        ///     Utility function to query ManagementObjects
        /// </summary>
        /// <param name="table"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public static string GetObject(string table, string property) {
            ManagementObjectSearcher search = new ManagementObjectSearcher("SELECT " + property + " FROM " + table);
            var enu = search.Get().GetEnumerator();
            if (enu.MoveNext()) {
                return (string)enu.Current[property];
            }

            return null;
        }
    }
}
