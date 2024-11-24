using System.Net.Http.Json;
using System.Text;
using DessertRate.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace DessertRate.Pages;
public class CounterBase : ComponentBase
{
    [Inject] HttpClient Http { get; set; }
    protected RatingModel RatingModel = new();
    protected int CurrentCount { get; set; } = 0;
    protected string Name { get; set; } = string.Empty;
    protected string Email { get; set; } = string.Empty;
    protected string Message { get; set; } = string.Empty;
    protected List<RatingRow> RatingRows = [];
    protected List<string> ImageUrls = [];

    protected override async Task OnInitializedAsync()
    {
        ImageUrls = await Http.GetFromJsonAsync<List<string>>("sample-data/image-urls.json");
        RatingRows = RatingModel.GetRatingRows(ImageUrls);
    }

    // protected override void OnInitialized()
    // {
    //     RatingRows = RatingModel.GetRatingRows();
    // }

    protected void HandleValidSubmit(EditContext editContext)
    {
        RatingModel.DoSortID();

        // var json = RatingModel.ToJSON();

        // var data = RatingModel.EncodePropertyNamesAsCSV();

        var data = new StringBuilder();
        foreach (var item in RatingModel.RatingRows)
        {
            // data += CodingExtensions.Dehydrate(item, true);
            // data += item.EncodePropertyDataAsCSV();
            data.Append(item.DessertID).Append(',').AppendLine($"{item.Ranking}");
        }

        Message = data.ToString();

        // Message = json;

        // await AzureBlobService.UploadData("november2022", "data", $"{RatingModel.name}.json", json);

        // NavManager.NavigateTo("/after-save");

        // Process the valid form
    }

    protected void ClickPlus(RatingRow row)
    {
        // if (row.ranking < RatingModel.ratingRows.Count) row.ranking += 1;
        row.Ranking += 1;
        Console.WriteLine($"click plus {row.Ranking}");
        RatingModel.Validate();
        // StateHasChanged();
    }

    protected void ClickMinus(RatingRow row)
    {
        // if (row.ranking > 1) row.ranking -= 1;
        row.Ranking -= 1;
        Console.WriteLine($"click minus {row.Ranking}");
        RatingModel.Validate();
        // StateHasChanged();
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

    protected void OnChangeName(ChangeEventArgs args)
    {
        RatingModel.Name = $"{args.Value}";
        Email = $"{args.Value}@somerandomplace222.com";
    }

    protected bool GetSubmitDisabled()
    {
        Console.WriteLine("in get Submit disabled");
        return !(Name.Trim().Length > 0);
    }


}
