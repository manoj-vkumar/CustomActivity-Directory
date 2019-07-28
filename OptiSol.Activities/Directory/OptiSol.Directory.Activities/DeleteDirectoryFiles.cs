using System;
using System.Activities;
using System.ComponentModel;
using System.IO;

namespace OptiSol.Directory {
    [DisplayName(AttributeString.DisplayName_DeleteDirectoryFiles)]
    [Description(AttributeString.Description_DeleteDirectoryFiles)]
    public class DeleteDirectoryFiles : CodeActivity {
        [Category(AttributeString.Category_Input)]
        [RequiredArgument]
        public InArgument<string> DirectoryPath { get; set; }

        [Category(AttributeString.Category_Input)]
        public InArgument<string> FileExtension { get; set; }

        [Category(AttributeString.Category_Input)]
        public EnumTimestampCategory TimestampCategory { get; set; }

        [Category(AttributeString.Category_Input)]
        public InArgument<long> DayCountFrom { get; set; }

        [Category(AttributeString.Category_Input)]
        public InArgument<long> DayCountTo { get; set; }

        [Category(AttributeString.Category_Common)]
        public EnumYesOrNo ContinueOnError { get; set; }

        private void DeleteFilesInDirectory(CodeActivityContext context) {
            try {
                string _directoryPath = DirectoryPath.Get(context);
                string _fileExtension = FileExtension.Get(context);
                int _dayCountFrom = Convert.ToInt16(DayCountFrom.Get(context));
                int _dayCountTo = Convert.ToInt16(DayCountTo.Get(context));
                bool _timestampCategory = Convert.ToBoolean((int)TimestampCategory);
                string[] fileList = Helper.DirectoryHelper.GetDirectoryFiles(directoryPath: _directoryPath, fileExtension: _fileExtension, timestampCategory: _timestampCategory, dayCountFrom: _dayCountFrom, dayCountTo: _dayCountTo);

                foreach (string file in fileList) {
                    try {
                        File.Delete(file);
                    } catch (Exception ex) {
                        throw ex;
                    }
                }
            } catch (Exception ex) {
                if (Convert.ToBoolean((int)ContinueOnError)) {
                } else {
                    throw ex;
                }
            }
        }

        protected override void Execute(CodeActivityContext context) {
            DeleteFilesInDirectory(context);
        }
    }
}