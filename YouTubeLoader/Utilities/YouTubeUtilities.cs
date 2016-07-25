using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using YouTubeLoader.Models;

namespace YouTubeLoader.Utilities
{
    public class YouTubeUtilities
    {
        public static void ProcessOutput(YouTubeObject youTubeObject, bool addVideoToQueue)
        {
            var lastOutput = youTubeObject.Output.Last();

            if (addVideoToQueue)
            {
                // Gets the title of the video -- and returns
                if (!lastOutput.StartsWith("https://"))
                    youTubeObject.Name = lastOutput;

                // Get thumbnail url
                if (lastOutput.StartsWith("https://"))
                    youTubeObject.ThumbnailUrl = lastOutput;

                // todo if nothing returnes

                return;
            }

            // Get status
            var regexStatus = Regex.Match(lastOutput, "\\[youtube\\]\\s*(.*):\\s*(.*)");
            if (regexStatus.Success)
                youTubeObject.Status = regexStatus.Groups[2].Value;

            // Get merge status
            var regexMerge = Regex.Match(lastOutput, "\\[ffmpeg\\]\\s*(.*)\\s*into.*");
            if (regexMerge.Success)
                youTubeObject.Status = regexStatus.Groups[1].Value;

            // Get Download info
            var regexDownload = Regex.Match(lastOutput, "\\[download\\]\\s*([0-9][0-9.]*[0-9])%\\s*of\\s*([0-9][0-9.]*[0-9])(.*)\\s*at\\s*([0-9][0-9.]*[0-9])(.*)\\s*ETA\\s*([0-9][0-9:]*[0-9])");
            if (regexDownload.Success)
            {
                youTubeObject.IsDownloading = true;
                youTubeObject.Status = "Downloading";
                youTubeObject.IsInitializing = false;

                youTubeObject.ProgressPercentage = double.Parse(regexDownload.Groups[1].Value);
                youTubeObject.Size = double.Parse(regexDownload.Groups[2].Value);
                youTubeObject.SizeSuffix = regexDownload.Groups[3].Value;
                youTubeObject.Speed = double.Parse(regexDownload.Groups[4].Value);
                youTubeObject.SpeedSuffix = regexDownload.Groups[5].Value;
                youTubeObject.Eta = regexDownload.Groups[6].Value;
            }

            // Get download complete
            var regexComplete = Regex.Match(lastOutput, "\\[download\\]\\s*100%\\s*of\\s*(.*)\\s*in\\s*(.*)");
            if (regexComplete.Success)
                youTubeObject.Status = "Complete";

            // Get if duplicated download
            if (lastOutput.Contains("has already been downloaded"))
            {
                youTubeObject.Status = "Complete";
                youTubeObject.ProgressPercentage = 100;
                Debug.WriteLine("Video already downloaded");
            }
        }

        public static bool GetValidYouTubeUrlAndId(string url, out string id)
        {
            id = null;

            var regex = Regex.Match(url, "(?:https|http):\\/\\/(?:www\\.|)(?:youtube|youtu)\\.(?:com|be)\\/(?:watch\\?v=|_)(\\S+)");

            if (regex.Success)
                id = regex.Groups[1].Value;

            return regex.Success && !string.IsNullOrEmpty(id) && id.Length >= 11;
        }
    }
}
