using System.Linq;

namespace OptiSol.Directory {
    public class HelpperClass {
        public static string[] GetDirectoryFiles(string DirectoryPath, string FileExtension, bool CreatedBy, long DayCountFrom, long DayCountTo) {
            // If To count is lesser than a From count, both will be reset into zero
            if (DayCountTo - DayCountFrom < 0) {
                DayCountFrom = DayCountTo = 0;
            }

            // Get file list by TimeStamp and Extension in directory
            if (CreatedBy) {
                // Created date
                return new System.IO.DirectoryInfo(DirectoryPath).GetFiles().Where(x => (DayCountFrom <= 0 || x.CreationTime.Date <= System.DateTime.Now.AddDays(-DayCountFrom).Date) && (DayCountTo <= 0 || x.CreationTime.Date >= System.DateTime.Now.AddDays(-DayCountTo).Date) && (string.IsNullOrEmpty(FileExtension) || x.Extension.ToLower() == FileExtension.ToLower())).Select(x => x.FullName).ToArray();
            } else {
                // Last Write date
                return new System.IO.DirectoryInfo(DirectoryPath).GetFiles().Where(x => (DayCountFrom <= 0 || x.LastWriteTime.Date <= System.DateTime.Now.AddDays(-DayCountFrom).Date) && (DayCountTo <= 0 || x.LastWriteTime.Date >= System.DateTime.Now.AddDays(-DayCountTo).Date) && (string.IsNullOrEmpty(FileExtension) || x.Extension.ToLower() == FileExtension.ToLower())).Select(x => x.FullName).ToArray();
            }
        }
    }
}
