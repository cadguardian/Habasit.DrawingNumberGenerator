namespace CADCleanser
{
    public interface ISolidWorksService
    {
        void CloseFile();

        void OpenFile(string filePath);

        void SaveBlockToLibrary(string blockName, string destinationFolder);
    }
}