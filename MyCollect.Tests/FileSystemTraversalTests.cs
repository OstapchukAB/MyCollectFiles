using System.IO.Abstractions.TestingHelpers;
using FluentAssertions;

namespace MyCollect.Tests;
public class FileSystemTraversalTests
{
    [Fact]
    public void GetFiles_ReturnsAllFiles()
    {
        // Arrange

        var mockFileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
        {
            { @"C:\Users\ExampleUser\Documents\file1.txt", new MockFileData("Content of file1") },
            { @"C:\Users\ExampleUser\Documents\subdir1\file2.txt", new MockFileData("Content of file2") },
            { @"C:\Users\ExampleUser\Documents\subdir2\file3.txt", new MockFileData("Content of file3") }
        });

        string myDocumentsPath = @"C:\Users\ExampleUser\Documents";
        mockFileSystem.AddDirectory(myDocumentsPath); // Ensure the directory exists in the mock file system

        // Act
        var files = Program.GetFiles(myDocumentsPath, mockFileSystem);

        // Assert
        var expectedFiles = new List<string>
        {
            @"C:\Users\ExampleUser\Documents\file1.txt",
            @"C:\Users\ExampleUser\Documents\subdir1\file2.txt",
            @"C:\Users\ExampleUser\Documents\subdir2\file3.txt"
        };

        files.Should().BeEquivalentTo(expectedFiles);
    }
}
