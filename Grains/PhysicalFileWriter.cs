using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;

public class PhysicalFileWriter : IFileWriter
{
    private readonly IFileInfo directoryInfo;
    private IFileProvider fileProvider;

    public PhysicalFileWriter(IFileProvider fileProvider)
    {
        this.directoryInfo = fileProvider?.GetFileInfo("./");
        if (fileProvider == null || !directoryInfo.PhysicalPath.EndsWith("\\"))
        {
            throw new ArgumentOutOfRangeException($"Invalid Directory Provided: {directoryInfo?.PhysicalPath ?? "[null]"}" );
        }

        this.fileProvider = fileProvider;
    }

    public void Delete(string filename)
    {
        var path = Path.Combine(this.directoryInfo.PhysicalPath, filename);
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

    public IDirectoryContents GetDirectoryContents(string subpath)
    {
        return this.fileProvider.GetDirectoryContents(subpath);
    }

    public IFileInfo GetFileInfo(string subpath)
    {
        return this.fileProvider.GetFileInfo(subpath);
    }

    public IChangeToken Watch(string filter)
    {
        return this.fileProvider.Watch(filter);
    }

    public void Write(string name, string content)
    {
        this.Write(name, new MemoryStream(System.Text.Encoding.UTF8.GetBytes(content)));
    }

    public void Write(string name, Stream content)
    {
        using var file = File.OpenWrite(Path.Combine(this.directoryInfo.PhysicalPath, name));
        content.CopyTo(file);
    }
}