using Microsoft.Extensions.FileProviders;

public interface IFileWriter : IFileProvider
{
    void Write(string name, string content);

    void Write(string name, Stream content);
}
