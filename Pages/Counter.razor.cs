using System.Net.Http.Json;
using System.Text;
using DessertRate.Models;
using DessertRate.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace DessertRate.Pages;

public class CounterBase : ComponentBase
{
    [Inject] HttpClient Http { get; set; }
    [Inject] IConfigService ConfigService { get; set; }
    [Inject] protected IJSRuntime JS { get; set; }
    protected RatingModel RatingModel;
    protected int CurrentCount { get; set; } = 0;
    protected string Name { get; set; } = string.Empty;
    protected string Email { get; set; } = string.Empty;
    protected string Message { get; set; } = string.Empty;
    protected List<RatingRow> RatingRows = [];
    protected List<string> ImageUrls = [];
    protected ElementReference ExternalForm;


    protected override async Task OnInitializedAsync()
    {
        // Initialize RatingModel with injected ConfigService
        RatingModel = new RatingModel(ConfigService);

        // Load image URLs from the configuration service
        RatingRows = await RatingModel.LoadImageURLsAsync();
    }

    protected void ClickPlus(RatingRow row)
    {
        if (row.Ranking < RatingModel.RatingRows.Count) row.Ranking += 1;
        Console.WriteLine($"click plus {row.Ranking}");
        RatingModel.Validate();
    }

    protected void ClickMinus(RatingRow row)
    {
        if (row.Ranking > 1) row.Ranking -= 1;
        Console.WriteLine($"click minus {row.Ranking}");
        RatingModel.Validate();
    }

    protected void ClickSortID()
    {
        RatingRows = RatingModel.DoSortID();
    }

    protected void ClickSortRank()
    {
        RatingRows = RatingModel.DoSortRank();
    }
    protected void IncrementCount()
    {
        CurrentCount++;
        Message = $"{CurrentCount}";
    }

    protected void OnChangeRating(ChangeEventArgs args, RatingRow row)
    {
        // Console.WriteLine($"OnChangeRating {row.DessertID}={row.Ranking}");
        Console.WriteLine($"OnChangeRating Value={args.Value.ToString()}");
        row.Ranking = int.Parse(args.Value.ToString());
        RatingModel.Validate();


    }
    // protected void OnChangeRating(RatingRow row)
    // {
    //     Console.WriteLine($"OnChangeRating {row.DessertID}={row.Ranking}");
    // }

    protected void OnChangeName(ChangeEventArgs args)
    {
        RatingModel.Name = $"{args.Value}";
        Email = $"{args.Value}@somerandomplace.com";
    }

    protected bool IsVoteDisabled()
    {
        Console.WriteLine("in is vote disabled");
        return !RatingModel.Name.Trim().Any() || !RatingModel.Valid;
    }

    protected bool GetSubmitDisabled()
    {
        Console.WriteLine("in get Submit disabled");
        return !(Name.Trim().Length > 0);
    }

    // Called when you want to finalize/preprocess then submit the form
    protected async Task SubmitFormWithPreprocessingAsync()
    {
        await JS.InvokeVoidAsync("submitForm", ExternalForm);
    }

    // Example: call from your existing ClickSave
    protected async Task ClickSave()
    {
        RatingModel.DoSortID();

        var data = new StringBuilder();
        foreach (var item in RatingRows)
        {
            data.Append(item.DessertID).Append(',').AppendLine($"{item.Ranking}");
        }

        Message = data.ToString();
        await Task.Delay(250); // Small delay to ensure Message is updated
        await SubmitFormWithPreprocessingAsync();
    }

}
