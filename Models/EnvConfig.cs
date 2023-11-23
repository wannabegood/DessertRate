using System;
using System.IO;
using System.Reflection;
// using DTARServer.Helpers;
// using DTARServer.Models;
// using IoBTMessage.Models;

namespace DessertRate.Models;
public interface IEnvConfig
{
}

public class EnvConfig : IEnvConfig
{
    public string ImgBaseURL { get; set; } = "https://dessertratings.blob.core.windows.net/november2022";
}
