using System.Collections.Generic;
using System.IO;

namespace MagicFilesLib;

// Interface wraps static Directory.GetFiles — makes it mockable
public interface IDirectoryExplorer
{
    ICollection<string> GetFiles(string path);
}

// Real implementation — uses actual file system
// In unit tests, IDirectoryExplorer is mocked so no real disk access needed
public class DirectoryExplorer : IDirectoryExplorer
{
    public ICollection<string> GetFiles(string path)
    {
        string[] files = Directory.GetFiles(path);
        return files;
    }
}
