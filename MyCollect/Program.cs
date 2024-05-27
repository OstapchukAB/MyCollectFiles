using System.IO.Abstractions;

public class Program
{
    public static void Main(string[] args)
    {
        var fileSystem = new FileSystem();
        string rootPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}"; // Укажите начальный каталог
        IEnumerable<string> files = GetFiles(rootPath, fileSystem);

        foreach (var file in files)
        {
            Console.WriteLine(file);
        }
    }

    public static IEnumerable<string> GetFiles(string rootPath, IFileSystem fileSystem)
    {
        Stack<string> dirs = new Stack<string>();

        if (!fileSystem.Directory.Exists(rootPath))
        {
            throw new ArgumentException();
        }

        dirs.Push(rootPath);

        while (dirs.Count > 0)
        {
            string currentDir = dirs.Pop();
            IEnumerable<string> subDirs;
            IEnumerable<string> files;

            try
            {
                subDirs = fileSystem.Directory.EnumerateDirectories(currentDir);
                files = fileSystem.Directory.EnumerateFiles(currentDir);
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message);
                continue;
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
                continue;
            }

            foreach (string file in files)
            {
                yield return file;
            }

            foreach (string str in subDirs)
                dirs.Push(str);
        }
    }
}
