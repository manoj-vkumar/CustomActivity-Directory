using System;
using System.Activities;
using System.ComponentModel;
using System.IO;

namespace OptiSol.Directory {
    public class DeleteDirectoryFiles : CodeActivity {
        //File(s) will be delete from this directory path.
        [Category("Input")]
        [RequiredArgument]
        public InArgument<string> DirectoryPath { get; set; }

        //File(s) will be considered if extension is matched, but default all.
        [Category("Input")]
        public InArgument<string> FileExtension { get; set; }

        //If it is a true, file(s) created date will be considered else or default last write date will be considered.
        [Category("Input")]
        public InArgument<bool> CreatedBy { get; set; }

        //How many days from today for the file(s) which is(are) should be getting from.
        [Category("Input")]
        public InArgument<long> DayCountFrom { get; set; }

        //How many days from today for the file(s) which is(are) should be getting to from DayCountFrom.
        [Category("Input")]
        public InArgument<long> DayCountTo { get; set; }

        //If it is a true, even continues with exception.
        [Category("Common")]
        public InArgument<bool> ContinueOnError { get; set; }

        private void DeleteFilesInDirectory(CodeActivityContext context) {
            try {
                //Looping a files - Get directory files as array
                foreach (string file in HelpperClass.GetDirectoryFiles(DirectoryPath: DirectoryPath.Get(context), FileExtension: FileExtension.Get(context), CreatedBy: CreatedBy.Get(context), DayCountFrom: DayCountFrom.Get(context), DayCountTo: DayCountTo.Get(context))) {
                    // If throws an exception continues into a next
                    try {
                        // Deleting a files one by one
                        File.Delete(file.ToString());
                    } catch (Exception ex) {
                        throw ex;
                    }
                }
            } catch (Exception ex) {
                if (ContinueOnError.Get(context)) {
                    //Exception not throws
                } else {
                    //Exception throws
                    throw ex;
                }
            }
        }

        protected override void Execute(CodeActivityContext context) {
            //Delete files from directory
            DeleteFilesInDirectory(context);
        }
    }
}