using System;
using System.Activities;
using System.ComponentModel;

namespace OptiSol.Directory {
    [DisplayName(AttributeString.DisplayName_GetDirectoryFiles)]
    [Description(AttributeString.Description_GetDirectoryFiles)]
    public class GetDirectoryFiles : CodeActivity {
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

        [Category(AttributeString.Category_Output)]
        public OutArgument<string[]> DirectoryFiles { get; set; }

        [Category(AttributeString.Category_Common)]
        public EnumYesOrNo ContinueOnError { get; set; }

        private void GetFilesInDirectory(CodeActivityContext context) {
            try {
                string _directoryPath = DirectoryPath.Get(context);
                string _fileExtension = FileExtension.Get(context);
                int _dayCountFrom = Convert.ToInt16(DayCountFrom.Get(context));
                int _dayCountTo = Convert.ToInt16(DayCountTo.Get(context));
                bool _timestampCategory = Convert.ToBoolean((int)TimestampCategory);
                string[] fileList = Helper.DirectoryHelper.GetDirectoryFiles(directoryPath: _directoryPath, fileExtension: _fileExtension, timestampCategory: _timestampCategory, dayCountFrom: _dayCountFrom, dayCountTo: _dayCountTo);

                DirectoryFiles.Set(context, fileList);
            } catch (Exception ex) {
                if (Convert.ToBoolean((int)ContinueOnError)) {
                } else {
                    throw ex;
                }
            }
        }

        protected override void Execute(CodeActivityContext context) {
            GetFilesInDirectory(context);
        }
    }
}