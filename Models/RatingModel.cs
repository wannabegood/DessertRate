using System.ComponentModel.DataAnnotations;
using System.Text.Json;
namespace DessertRate.Models;

public class RatingRow
{
    public string Name { get; set; } = "NO_NAME";
    public string DessertID { get; set; } = "";
    public string ImageURL { get; set; } = "";
    public int Ranking { get; set; } = 0;

}

public class RatingModel
{
    [Required]
    [StringLength(30, MinimumLength = 3, ErrorMessage = "Name must be at least 3 characters.")]
    public string Name { get; set; } = "";
    public bool Valid { get; set; }
    public List<RatingRow> RatingRows { get; set; } = new();
    private List<string> ImageLinks { get; set; } = ["https://dl.dropboxusercontent.com/scl/fi/dpbd5sx0c5i8ygfkgxeko/Dessert-01.jpg?rlkey=6o9mj8bxticx37pwfhrumx459&dl=0",
    "https://dl.dropboxusercontent.com/scl/fi/8tc8cw4rq0208ukh2nxdh/Dessert-02.jpg?rlkey=b57qpoisx1hbq2kho99k9lqa1&dl=0",
    "https://dl.dropboxusercontent.com/scl/fi/nvfb9yngovpjuqyxm8okb/Dessert-03.jpg?rlkey=7qk3zc2q74jbibo027w79q4pv&dl=0",
    "https://dl.dropboxusercontent.com/scl/fi/n36pasfa593gqat0n5ir0/Dessert-04.jpg?rlkey=yb2i547fosekqbfs8v1l873sp&dl=0",
    "https://dl.dropboxusercontent.com/scl/fi/teo4230teokn2dbpmdqj0/Dessert-05.jpg?rlkey=7j56jpml4xzw4f3m48nmr5ls1&dl=0",
    "https://dl.dropboxusercontent.com/scl/fi/47vgbbx3036v1veeepg17/Dessert-06.jpg?rlkey=hkqy75hci81ugi0sxjasqelob&dl=0",
    "https://dl.dropboxusercontent.com/scl/fi/uuwpuwwr0bs7av85myno1/Dessert-08.jpg?rlkey=m8m2ogpcaiztyhiyvrjycvzwk&dl=0"
    ];


    private EnvConfig _config;

    public RatingModel()
    {
        _config = new EnvConfig();
        Valid = Validate();
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
        foreach (var row in RatingRows)
        {
            row.Name = Name;
        }
        return RatingRows;

    }

    public List<RatingRow> DoSortID()
    {
        RatingRows = [.. RatingRows.OrderBy((x) => x.DessertID)];
        return RatingRows;
    }

    public List<RatingRow> DoSortRank()
    {
        RatingRows = [.. RatingRows.OrderBy((x) => x.Ranking)];
        return RatingRows;
    }

    public bool Validate()
    {
        var imageCount = RatingRows.Count;
        var expectedRankTotal = imageCount * (imageCount + 1) / 2;
        var actualTotal = 0;
        RatingRows.ForEach((item) =>
        {
            actualTotal += item.Ranking;
        });

        Valid = expectedRankTotal == actualTotal;
        return Valid;
    }

    public List<RatingRow> GetRatingRows()
    {
        Console.WriteLine("In GetRatingRows");
        RatingRows = [];
        var cnt = 1;
        var index = 1;
        foreach (var imageLink in ImageLinks)
        {
            var ratingRow = new RatingRow()
            {
                DessertID = $"Dessert-{cnt++}",
                ImageURL = imageLink,
                Ranking = index++
            };
            RatingRows.Add(ratingRow);
        }

        return RatingRows;

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
        var strJson = JsonSerializer.Serialize<List<RatingRow>>(RatingRows, opt);
        Console.WriteLine(strJson);
        return strJson;
    }

}
