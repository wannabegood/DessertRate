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
        var client = new GitHubClient(new ProductHeaderValue("DessertRate"));
        var tokenAuth = new Credentials(Token); // NOTE: not real token
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
                "firstfile.txt", "data");

        await client.Repository.Content.CreateFile(
             owner, repoName, filePath,
             new CreateFileRequest($"First commit for {filePath}", sb.ToString(), branch));

    }
}
