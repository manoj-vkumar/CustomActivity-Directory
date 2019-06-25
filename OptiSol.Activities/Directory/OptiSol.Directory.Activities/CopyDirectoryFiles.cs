using System;
using System.Activities;
using System.ComponentModel;

namespace OptiSol.Directory {
    public class CopyDirectoryFiles : CodeActivity {
        //File(s) will be copy from this directory path.
        [Category("Input")]
        [RequiredArgument]
        public InArgument<string> DirectoryPathFrom { get; set; }

        //File(s) will be past into this directory path.
        [Category("Input")]
        [RequiredArgument]
        public InArgument<string> DirectoryPathTo { get; set; }

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

        private void CopyFilesToDirectory(CodeActivityContext context) {
            try {
                //If directory isn't exists, it'll be created
                if (!System.IO.Directory.Exists(DirectoryPathTo.Get(context))) {
                    System.IO.Directory.CreateDirectory(DirectoryPathTo.Get(context));
                }
                //Looping a files - Get directory files as array
                foreach (string file in HelpperClass.GetDirectoryFiles(DirectoryPath: DirectoryPathFrom.Get(context), FileExtension: FileExtension.Get(context), CreatedBy: CreatedBy.Get(context), DayCountFrom: DayCountFrom.Get(context), DayCountTo: DayCountTo.Get(context))) {
                    // If throws an exception continues into a next
                    try {
                        string pathTo = System.IO.Path.Combine(DirectoryPathTo.Get(context), System.IO.Path.GetFileName(file));
                        if (System.IO.File.Exists(pathTo)) {
                            System.IO.File.Delete(pathTo);
                        }
                        //Copy a files one by one from one directory into another
                        System.IO.File.Copy(file, pathTo);
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
            //Copy files from one directory into another
            CopyFilesToDirectory(context);
        }
    }
}