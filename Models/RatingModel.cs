using System.ComponentModel.DataAnnotations;
using System.Text.Json;
namespace DessertRate.Models;

public class RatingRow
{
    public string name { get; set; } = "NO_NAME";
    public string dessertID { get; set; } = "";
    public string imageURL { get; set; } = "";
    public int ranking { get; set; } = 0;

}

public class RatingModel
{
    [Required]
    [StringLength(30, MinimumLength = 3, ErrorMessage = "Name must be at least 3 characters.")]
    public string name { get; set; } = "";
    public bool valid { get; set; }
    public List<RatingRow> ratingRows { get; set; } = new();
    private List<string> ImageLinks { get; set; } = ["https://dl.dropboxusercontent.com/scl/fi/l867c105n0epv972h16vv/dessert5.jpg?rlkey=d6yiese7ddy2awpk9p14ihztb&dl=0"];

    private EnvConfig _config;

    public RatingModel()
    {
        _config = new EnvConfig();
        valid = Validate();
    }
    // private string GetImgSrc(string name)
    // {
    //     var src = $"{_config.ImgBaseURL}/{name}";
    //     return src;
    // }

    // private RatingRow BuildRow(BlobItem blobItem, int index)
    // {
    //     var format = "00";
    //     var idx = (index + 1).ToString(format);
    //     var item = new RatingRow()
    //     {
    //         dessertID = $"Dessert-{idx}",
    //         imageURL = GetImgSrc(blobItem.Name),
    //         ranking = index + 1
    //     };
    //     return item;
    // }

    private List<RatingRow> AssignNameToRows()
    {
        foreach (var row in ratingRows)
        {
            row.name = name;
        }
        return ratingRows;

    }

    public List<RatingRow> DoSortID()
    {
        ratingRows = ratingRows.OrderBy((x) => x.dessertID).ToList();
        return ratingRows;
    }

    public List<RatingRow> DoSortRank()
    {
        ratingRows = ratingRows.OrderBy((x) => x.ranking).ToList();
        return ratingRows;
    }

    public bool Validate()
    {
        var imageCount = ratingRows.Count;
        var expectedRankTotal = imageCount * (imageCount + 1) / 2;
        var actualTotal = 0;
        ratingRows.ForEach((item) =>
        {
            actualTotal += item.ranking;
        });

        valid = expectedRankTotal == actualTotal;
        return valid;
    }

    public List<RatingRow> GetRatingRows()
    {
        ratingRows = [];
        var cnt = 1;
        var index = 0;
        foreach (var imageLink in ImageLinks)
        {
            var ratingRow = new RatingRow()
            {
                dessertID = $"Dessert-{cnt++}",
                imageURL = imageLink,
                ranking = index + 1
            };
            ratingRows.Add(ratingRow);
        }

        return ratingRows;

    }



    // public async Task<List<RatingRow>> setRatingRows(IAzureBlobService service)
    // {
    //     var imgs = await service.ListBlobsStartingWith("november2022", "images", "");
    //     var sortedImgs = imgs.OrderBy((x) => x.Name).ToList();

    //     var i = 0;
    //     foreach (var img in sortedImgs)
    //     {
    //         Console.WriteLine($"img.Name={img.Name}");
    //         var row = BuildRow(img, i);
    //         ratingRows.Add(row);
    //         ++i;
    //     }
    //     Console.WriteLine($"ratingRows.Count={ratingRows.Count}");

    //     return this.ratingRows;
    // }

    public string ToJSON()
    {
        // AssignNameToRows();
        var opt = new JsonSerializerOptions() { WriteIndented = true };
        var strJson = JsonSerializer.Serialize<List<RatingRow>>(ratingRows, opt);
        Console.WriteLine(strJson);
        return strJson;
    }

}
