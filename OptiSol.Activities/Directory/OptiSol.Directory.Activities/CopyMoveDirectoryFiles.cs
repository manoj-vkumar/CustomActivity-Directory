using System;
using System.Activities;
using System.ComponentModel;
using System.IO;

namespace OptiSol.Directory {
    [DisplayName(AttributeString.DisplayName_CopyMoveDirectoryFiles)]
    [Description(AttributeString.Description_CopyMoveDirectoryFiles)]
    public class CopyMoveDirectoryFiles : CodeActivity {
        [Category(AttributeString.Category_Input)]
        [RequiredArgument]
        public InArgument<string> DirectoryPathFrom { get; set; }

        [Category(AttributeString.Category_Input)]
        [RequiredArgument]
        public InArgument<string> DirectoryPathTo { get; set; }

        [Category(AttributeString.Category_Input)]
        public InArgument<string> FileExtension { get; set; }

        [Category(AttributeString.Category_Input)]
        public EnumDirectoryAction DirectoryAction { get; set; }

        [Category(AttributeString.Category_Input)]
        public EnumYesOrNo IsNeedToReplace { get; set; }

        [Category(AttributeString.Category_Input)]
        public EnumTimestampCategory TimestampCategory { get; set; }

        [Category(AttributeString.Category_Input)]
        public InArgument<long> DayCountFrom { get; set; }

        [Category(AttributeString.Category_Input)]
        public InArgument<long> DayCountTo { get; set; }

        [Category(AttributeString.Category_Common)]
        public EnumYesOrNo ContinueOnError { get; set; }

        private void CopyFilesToDirectory(CodeActivityContext context) {
            try {
                string _directoryPathFrom = DirectoryPathFrom.Get(context);
                string _directoryPathTo = DirectoryPathTo.Get(context);
                string _fileExtension = FileExtension.Get(context);
                int _dayCountFrom = Convert.ToInt16(DayCountFrom.Get(context));
                int _dayCountTo = Convert.ToInt16(DayCountTo.Get(context));
                bool _timestampCategory = Convert.ToBoolean((int)TimestampCategory);
                bool _directoryAction = Convert.ToBoolean((int)DirectoryAction);
                bool _isNeedToReplace = Convert.ToBoolean((int)IsNeedToReplace);
                string[] fileList = Helper.DirectoryHelper.GetDirectoryFiles(directoryPath: _directoryPathFrom, fileExtension: _fileExtension, timestampCategory: _timestampCategory, dayCountFrom: _dayCountFrom, dayCountTo: _dayCountTo);

                //If directory isn't exists, it'll be created.
                if (!System.IO.Directory.Exists(_directoryPathTo)) {
                    System.IO.Directory.CreateDirectory(_directoryPathTo);
                }

                foreach (string file in fileList) {
                    try {
                        string pathTo = Path.Combine(_directoryPathTo, Path.GetFileName(file));
                        if (_isNeedToReplace) {
                            // Delete the exsisteing file
                            if (File.Exists(pathTo)) {
                                File.Delete(pathTo);
                            }
                        }

                        if (_directoryAction) {
                            File.Copy(file, pathTo);
                        } else {
                            File.Move(file, pathTo);
                        }
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
            CopyFilesToDirectory(context);
        }
    }
}