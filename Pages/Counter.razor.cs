using System.Text;
using Microsoft.AspNetCore.Components;
using Octokit;

namespace DessertRate.Pages;
public class CounterBase : ComponentBase
{

    protected int CurrentCount { get; set; } = 0;
    private string Token { get; } = "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";
    protected void IncrementCount()
    {
        CurrentCount++;
    }

    protected async Task Save()
    {
        IncrementCount();
        var client = new GitHubClient(new ProductHeaderValue("DessertRate"));
        var tokenAuth = new Credentials(Token);
        client.Credentials = tokenAuth;

        var sb = new StringBuilder("---");
        sb.AppendLine();
        sb.AppendLine($"date: \"2023-11-23\"");
        sb.AppendLine($"title: \"My new fancy updated post\"");
        sb.AppendLine("tags: [csharp, azure, dotnet]");
        sb.AppendLine("---");
        sb.AppendLine();

        sb.AppendLine("The heading for my first post");
        sb.AppendLine();

        var (owner, repoName, filePath, branch) = ("wannabegood", "DessertRate",
                $"data/file.txt-{CurrentCount}", "data");

        await client.Repository.Content.CreateFile(
             owner, repoName, filePath,
             new CreateFileRequest($"First commit for {filePath}", sb.ToString(), branch));

    }

    // protected async void DropBoxSave()
    // {
    //     using var client = new DropboxClient(DropboxToken);
    //     var filePath = "/test1.txt";

    //     var text = "abcdefghijklmnopqrstuvwxyz";
    //     using var stream = new MemoryStream(Encoding.ASCII.GetBytes(text));
    //     await client.Files.UploadAsync(filePath, WriteMode.Overwrite.Instance, body: stream);
    // }
}
