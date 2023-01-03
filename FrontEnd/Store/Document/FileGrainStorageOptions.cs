namespace FrontEnd.Store.DocumentData
{

    public class FileGrainStorageOptions
    {
        public IFileWriter RootDirectory { get; set; }
        public IFileNamer FileNamer { get; set; } = new JsonFileName();
    }
}
