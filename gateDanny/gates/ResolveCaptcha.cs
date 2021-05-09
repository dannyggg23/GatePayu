using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AntiCaptchaAPI;

internal class ResolveCaptcha
{
    

    private AntiCaptcha AntiCaptcha
    {
        get;
        set;
    }

    public decimal Balance
    {
        get;
        private set;
    }

    public ResolveCaptcha(string apikey)
    {
        AntiCaptcha = new AntiCaptcha(apikey);
    }

    private async void SetBalance()
    {
        AntiCaptchaResult antiCaptchaResult = await AntiCaptcha.GetBalance();
        if (antiCaptchaResult.Success)
        {
            Balance = Convert.ToDecimal(antiCaptchaResult.Response);
        }
        else
        {
            Balance = 0m;
        }
    }

    public string Image(string imageCaptchaSrc)
    {
        string task = "";
        try
        {
            string imageBase64 = ConvertImageURLToBase64(imageCaptchaSrc);
            task = Task.Run(async () => await SolveImage(imageBase64)).GetAwaiter().GetResult();
        }
        catch (HttpRequestException) { }

        return task;
    }

    public string ReCaptcha(string googleSiteKey, string pageUrl, bool isInvisible = false)
    {
        return Task.Run(async () => await SolveRecaptcha(googleSiteKey, pageUrl, isInvisible)).GetAwaiter().GetResult();
    }

    private async Task<string> SolveRecaptcha(string googleSiteKey, string pageUrl, bool isInvisible)
    {
        AntiCaptchaResult antiCaptchaResult = await AntiCaptcha.SolveReCaptchaV2(googleSiteKey, pageUrl, isInvisible);
        if (antiCaptchaResult.Success)
        {
            return antiCaptchaResult.Response;
        }
        return null;
    }

    private async Task<string> SolveImage(string imageBase64)
    {
        AntiCaptchaResult antiCaptchaResult = await AntiCaptcha.SolveImage(imageBase64);
        if (antiCaptchaResult.Success)
        {
            return antiCaptchaResult.Response;
        }
        return null;
    }

    private string ConvertImageURLToBase64(string url)
    {
        StringBuilder stringBuilder = new StringBuilder();
        byte[] image = GetImage(url);
        stringBuilder.Append(Convert.ToBase64String(image, 0, image.Length));
        return stringBuilder.ToString();
    }

    private byte[] GetImage(string url)
    {
        Stream stream = null;
        try
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            stream = httpWebResponse.GetResponseStream();
            byte[] result;
            using (BinaryReader binaryReader = new BinaryReader(stream))
            {
                int count = (int)httpWebResponse.ContentLength;
                result = binaryReader.ReadBytes(count);
                binaryReader.Close();
            }
            stream.Close();
            httpWebResponse.Close();
            return result;
        }
        catch (Exception)
        {
            return null;
        }
    }
}