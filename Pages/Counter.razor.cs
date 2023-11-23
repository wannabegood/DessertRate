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
    private string DropboxToken { get; } = "sl.BqanfA6kowG8dRWPsgk-Ym2mjDUpMnkqam31AQc0NMHGtXwM4aSvGijiYQhJNSTCXKmteMidW0LTic4Io7UMQHNuu1G_FuOTf3Fxn91eIPZU_LM347qCIBYKXOAjWZ3I1HxAL8kT7ejvm6U";

    protected void IncrementCount()
    {
        CurrentCount++;
    }

    protected async void DropBoxSave()
    {
        using var client = new DropboxClient(DropboxToken);
        var filePath = "/test1.txt";

        var text = "abcdefghijklmnopqrstuvwxyz";
        using var stream = new MemoryStream(Encoding.ASCII.GetBytes(text));
        await client.Files.UploadAsync(filePath, WriteMode.Overwrite.Instance, body: stream);
    }

}
