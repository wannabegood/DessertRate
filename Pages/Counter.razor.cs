using System.Text;
using DessertRate.Models;
using FoundryRulesAndUnits.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace DessertRate.Pages;
public class CounterBase : ComponentBase
{
    protected RatingModel RatingModel = new();
    protected int CurrentCount { get; set; } = 0;
    protected string Name { get; set; } = string.Empty;
    protected string Email { get; set; } = string.Empty;
    protected string Message { get; set; } = string.Empty;


    // protected override async Task OnInitializedAsync()
    // {
    // }    

    protected void HandleValidSubmit(EditContext editContext)
    {
        RatingModel.DoSortID();

        // var json = RatingModel.ToJSON();

        // var data = RatingModel.EncodePropertyNamesAsCSV();

        var data = new StringBuilder();
        foreach (var item in RatingModel.ratingRows)
        {
            // data += CodingExtensions.Dehydrate(item, true);
            // data += item.EncodePropertyDataAsCSV();
            data.Append(item.dessertID).Append(',').AppendLine($"{item.ranking}");
        }

        Message = data.ToString();

        // Message = json;

        // await AzureBlobService.UploadData("november2022", "data", $"{RatingModel.name}.json", json);

        // NavManager.NavigateTo("/after-save");

        // Process the valid form
    }

    protected void ClickPlus(RatingRow row)
    {
        if (row.ranking < RatingModel.ratingRows.Count) row.ranking += 1;
        RatingModel.Validate();
    }

    protected void ClickMinus(RatingRow row)
    {
        if (row.ranking > 1) row.ranking -= 1;
        RatingModel.Validate();
    }

    protected void ClickSortID()
    {
        RatingModel.DoSortID();
    }

    protected void ClickSortRank()
    {
        RatingModel.DoSortRank();
    }
    protected void IncrementCount()
    {
        CurrentCount++;
        Message = $"{CurrentCount}";
    }

    protected void OnChangeName(ChangeEventArgs args)
    {
        RatingModel.name = $"{args.Value}";
        Email = $"{args.Value}@somerandomplace222.com";
    }

    protected bool GetSubmitDisabled()
    {
        Console.WriteLine("in get Submit disabled");
        return !(Name.Trim().Length > 0);
    }


}
