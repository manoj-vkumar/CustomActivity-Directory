using System;
using System.IO;
using System.Linq;

namespace OptiSol.Directory.Helper {
    public class DirectoryHelper {
        /**
         * Check the day count and FromCount should be >= ToCount.
         * Filters - Timestamp (Created Date / Last Write Date), Extension
         */
        public static string[] GetDirectoryFiles(string directoryPath, string fileExtension, bool timestampCategory, long dayCountFrom, long dayCountTo) {
            if (dayCountFrom < dayCountTo) {
                dayCountTo = dayCountFrom = 0;
            }

            if (timestampCategory) {
                return new DirectoryInfo(directoryPath).GetFiles().Where(x => (dayCountTo <= 0 || x.CreationTime.Date <= DateTime.Now.AddDays(-dayCountTo).Date) && (dayCountFrom <= 0 || x.CreationTime.Date >= DateTime.Now.AddDays(-dayCountFrom).Date) && (string.IsNullOrEmpty(fileExtension) || x.Extension.ToLower() == fileExtension.ToLower())).Select(x => x.FullName).ToArray();
            } else {
                return new DirectoryInfo(directoryPath).GetFiles().Where(x => (dayCountTo <= 0 || x.LastWriteTime.Date <= DateTime.Now.AddDays(-dayCountTo).Date) && (dayCountFrom <= 0 || x.LastWriteTime.Date >= DateTime.Now.AddDays(-dayCountFrom).Date) && (string.IsNullOrEmpty(fileExtension) || x.Extension.ToLower() == fileExtension.ToLower())).Select(x => x.FullName).ToArray();
            }
        }
    }
}
