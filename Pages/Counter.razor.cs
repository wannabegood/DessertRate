
using System.Net.Http.Json;
using DessertRate.Models;
using Microsoft.AspNetCore.Components;

namespace DessertRate.Pages;

public class CounterBase : ComponentBase
{
    private const string ApiBaseUrl =
        "https://dessert-scores.gtfullton.workers.dev/";

    [Inject]
    protected HttpClient Http { get; set; } = default!;

    protected RatingModel RatingModel = new();

    protected int ContestId { get; set; }

    protected string Name { get; set; } = string.Empty;

    protected List<RatingRow> RatingRows = [];

    protected bool IsSubmitting { get; set; }

    protected string SubmissionMessage { get; set; } = "";

    protected string LoadError { get; set; } = "";

    protected bool VotingOpen { get; set; }
    protected override async Task OnInitializedAsync()
    {
        try
        {
            var response =
                await Http.GetFromJsonAsync<ContestApiResponse>(
                    $"{ApiBaseUrl}/api/contest");

            if (response?.Contest == null ||
                response.Desserts == null ||
                response.Desserts.Count == 0)
            {
                LoadError = "No contest or desserts are available.";
                return;
            }

            ContestId = response.Contest.ContestId;

            VotingOpen = response.Contest.VotingOpen == 1;

            RatingRows = response.Desserts
                .Select((dessert, index) => new RatingRow
                {
                    DessertID = dessert.DessertId,
                    Name = dessert.DisplayName,
                    ImageURL = dessert.ImageUrl,
                    Ranking = index + 1
                })
                .ToList();

            RatingModel.RatingRows = RatingRows;
            RatingModel.Validate();
        }
        catch (Exception ex)
        {
            LoadError = $"Unable to load contest: {ex.Message}";
        }
    }

    protected void ClickPlus(RatingRow row)
    {
        if (row.Ranking < RatingRows.Count)
            row.Ranking++;

        RatingModel.Validate();
    }

    protected void ClickMinus(RatingRow row)
    {
        if (row.Ranking > 1)
            row.Ranking--;

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

    protected void OnChangeRating(
        ChangeEventArgs args,
        RatingRow row)
    {
        if (int.TryParse(args.Value?.ToString(), out var rank))
        {
            row.Ranking = rank;
            RatingModel.Validate();
        }
    }

    protected void OnChangeName(ChangeEventArgs args)
    {
        Name = args.Value?.ToString() ?? "";
        RatingModel.Name = Name;
    }

    protected bool IsVoteDisabled()
    {
        return !VotingOpen ||
               IsSubmitting ||
               Name.Trim().Length < 3 ||
               !RatingModel.Valid;
    }
    protected async Task ClickSave()
    {
        if (IsVoteDisabled())
            return;

        IsSubmitting = true;
        SubmissionMessage = "";

        try
        {
            RatingModel.DoSortID();
            RatingRows = RatingModel.RatingRows;

            var request = new SubmissionRequest
            {
                ContestId = ContestId,
                ParticipantName = Name.Trim(),
                Rankings = RatingRows.Select(row =>
                    new RankingRequest
                    {
                        DessertId = row.DessertID,
                        Rank = row.Ranking
                    }).ToList()
            };

            var response = await Http.PostAsJsonAsync(
                $"{ApiBaseUrl}/api/submissions",
                request);

            if (response.IsSuccessStatusCode)
            {
                SubmissionMessage =
                    "Your ballot has been saved! " +
                    "You can submit again anytime to change your rankings.";
            }
            else
            {
                var error = await response.Content
                    .ReadFromJsonAsync<ApiError>();

                SubmissionMessage =
                    error?.Error ??
                    $"Submission failed: {response.StatusCode}";
            }
        }
        catch (Exception ex)
        {
            SubmissionMessage =
                $"Unable to submit your ballot: {ex.Message}";
        }
        finally
        {
            IsSubmitting = false;
        }
    }

    // API DTOs

    protected class ContestApiResponse
    {
        public ContestDto Contest { get; set; } = new();

        public List<DessertDto> Desserts { get; set; } = [];
    }

    protected class ContestDto
    {
        public int ContestId { get; set; }

        public string Name { get; set; } = "";

        public int VotingOpen { get; set; }
    }

    protected class DessertDto
    {
        public string DessertId { get; set; } = "";

        public string DisplayName { get; set; } = "";

        public string ImageUrl { get; set; } = "";
    }

    protected class SubmissionRequest
    {
        public int ContestId { get; set; }

        public string ParticipantName { get; set; } = "";

        public List<RankingRequest> Rankings { get; set; } = [];
    }

    protected class RankingRequest
    {
        public string DessertId { get; set; } = "";

        public int Rank { get; set; }
    }

    protected class ApiError
    {
        public string Error { get; set; } = "";
    }
}
