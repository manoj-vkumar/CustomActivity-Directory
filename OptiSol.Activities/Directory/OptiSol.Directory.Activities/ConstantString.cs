namespace OptiSol.Directory {
    public static class ConstantString {
    }

    public static class AttributeString {
        public const string Category_Input = "Input";
        public const string Category_Output = "Output";
        public const string Category_Common = "Common";

        public const string DisplayName_GetDirectoryFiles = "Get Directory Files";
        public const string Description_GetDirectoryFiles = "Files will be listed from the given direcotry";
        public const string DisplayName_DeleteDirectoryFiles = "Delete Directory Files";
        public const string Description_DeleteDirectoryFiles = "Files will be deleted from the given direcotry";
        public const string DisplayName_CopyMoveDirectoryFiles = "Copy Move Directory Files";
        public const string Description_CopyMoveDirectoryFiles = "Files will be copied/moved from the given direcotry into another";
    }

    public static class Messages {
        public const string Success = "Success";
        public const string Failiure = "Failiure";
        public const string SomethingWentWrong = "Something went wrong";
    }

    public enum EnumYesOrNo {
        Yes = 1,
        No = 0
    }

    public enum EnumDirectoryAction {
        Copy = 1,
        Move = 0
    }

    public enum EnumTimestampCategory {
        CreationTime = 1,
        LastWriteTime = 0
    }
}
