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

    protected void HandleValidSubmit(EditContext editContext)
    {
        Console.WriteLine($"HandleValidSubmit");

        RatingModel.DoSortID();

        var data = new StringBuilder();
        foreach (var item in RatingRows)
        {
            data.Append(item.DessertID).Append(',').AppendLine($"{item.Ranking}");
        }

        Message = data.ToString();
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

    protected bool GetSubmitDisabled()
    {
        Console.WriteLine("in get Submit disabled");
        return !(Name.Trim().Length > 0);
    }


}
