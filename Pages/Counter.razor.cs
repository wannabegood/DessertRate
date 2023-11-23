using System.Text;
using Dropbox.Api;
using Dropbox.Api.Files;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Components;
using MimeKit;
using Octokit;

namespace DessertRate.Pages;
public class CounterBase : ComponentBase
{

    protected int CurrentCount { get; set; } = 0;
    private string Token { get; } = "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";
    private string DropboxToken { get; } = "sl.BqanfA6kowG8dRWPsgk-Ym2mjDUpMnkqam31AQc0NMHGtXwM4aSvGijiYQhJNSTCXKmteMidW0LTic4Io7UMQHNuu1G_FuOTf3Fxn91eIPZU_LM347qCIBYKXOAjWZ3I1HxAL8kT7ejvm6U";
    private string RefreshToken { get; set; } = string.Empty;

    protected void IncrementCount()
    {
        CurrentCount++;
    }

    protected async Task Save()
    {
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
                "data/thirdfile.txt", "data");

        await client.Repository.Content.CreateFile(
             owner, repoName, filePath,
             new CreateFileRequest($"First commit for {filePath}", sb.ToString(), branch));

    }

    protected async void DropBoxSave()
    {
        using var client = new DropboxClient(DropboxToken);
        var filePath = "/test1.txt";

        var text = "abcdefghijklmnopqrstuvwxyz";
        using var stream = new MemoryStream(Encoding.ASCII.GetBytes(text));
        await client.Files.UploadAsync(filePath, WriteMode.Overwrite.Instance, body: stream);
    }

    protected void Email()
    {

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
                "secondfile.txt", "data-2");


        var email = new MimeMessage();

        email.From.Add(new MailboxAddress("DessertRate", "k2persona04@gmail.com"));
        email.To.Add(new MailboxAddress("Greg Fulton", "gtfullton@gmail.com"));

        email.Subject = "Testing out email sending";
        email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = "<b>Hello all the way from the land of C#</b>"
        };

        using var smtp = new SmtpClient();
        smtp.Connect("smtp-relay.gmail.com", 587, false);

        // Note: only needed if the SMTP server requires authentication
        smtp.Authenticate("k2persona04@gmail.com", "k2Team#1inSAIC");

        smtp.Send(email);
        smtp.Disconnect(true);

    }
}
