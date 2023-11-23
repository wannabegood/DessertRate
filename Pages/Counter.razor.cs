using System.Text;
using FoundryRulesAndUnits.Extensions;
using Microsoft.AspNetCore.Components;
using Octokit;

namespace DessertRate.Pages;
public class CounterBase : ComponentBase
{

    protected int CurrentCount { get; set; } = 0;
    protected string Name { get; set; } = string.Empty;
    protected string Email { get; set; } = string.Empty;
    protected string Message { get; set; } = string.Empty;


    protected void IncrementCount()
    {
        CurrentCount++;
        Message = $"{CurrentCount}";
    }

    protected void OnChangeName(ChangeEventArgs args)
    {
        Email = $"{args.Value}@somerandomplace222.com";
    }

    protected bool GetSubmitDisabled()
    {
        Console.WriteLine("in get Submit disabled");
        return !(Name.Trim().Length > 0);
    }


}
