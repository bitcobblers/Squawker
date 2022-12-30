namespace Grains.DocumentData
{

    public class FileGrainStorageOptions
    {
        public IFileWriter RootDirectory { get; set; }
        public IFileNamer FileNamer { get; set; } = new JsonFileName();
    }
}
