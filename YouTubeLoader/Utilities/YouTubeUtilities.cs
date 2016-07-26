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
            Match match;

            // Get info.json filename
            if (ExistsInOutput(lastOutput, "\\[info\\]\\s*(Writing video description metadata as JSON) to:\\s*(.*)", out match))
            {
                youTubeObject.JsonFilename = match.Groups[2].Value;
                youTubeObject.Status = match.Groups[1].Value;
            }

            // Get status
            if (ExistsInOutput(lastOutput, "\\[youtube\\]\\s*.*:\\s*(.*)", out match))
                youTubeObject.Status = match.Groups[1].Value;

            // Get ffmpeg status (merg)
            if (ExistsInOutput(lastOutput, "\\[ffmpeg\\]\\s*(.*)\\s*into.*", out match))
            {
                youTubeObject.Status = match.Groups[1].Value;
                youTubeObject.IsDownloadingAudio = false;
            }

            // Get if status is 100% (download complete)
            if (ExistsInOutput(lastOutput, "\\[download\\]\\s*100%\\s*of\\s*(.*)\\s*in\\s*(.*)", out match))
                youTubeObject.Status = "Complete";

            if (lastOutput.Contains("[NA@NA]"))
                youTubeObject.IsDownloadingAudio = true;

            // Get download progress status
            if (ExistsInOutput(lastOutput, "\\[download\\]\\s*([0-9][0-9.]*[0-9])%\\s*of\\s*([0-9][0-9.]*[0-9])(.*)\\s*at\\s*([0-9][0-9.]*[0-9])(.*)\\s*ETA\\s*([0-9][0-9:]*[0-9])", out match))
            {
                youTubeObject.IsDownloading = true;
                youTubeObject.Status = "Downloading";
                youTubeObject.IsInitializing = false;

                youTubeObject.ProgressPercentage = double.Parse(match.Groups[1].Value);
                youTubeObject.Size = double.Parse(match.Groups[2].Value);
                youTubeObject.SizeSuffix = match.Groups[3].Value;
                youTubeObject.Speed = double.Parse(match.Groups[4].Value);
                youTubeObject.SpeedSuffix = match.Groups[5].Value;
                youTubeObject.Eta = match.Groups[6].Value;
            }

            // Get if duplicated download
            if (lastOutput.Contains("has already been downloaded"))
            {
                youTubeObject.Status = "Complete";
                youTubeObject.ProgressPercentage = 100;
                Debug.WriteLine("Video already downloaded");
            }
        }

        private static bool ExistsInOutput(string input, string pattern, out Match match)
        {
            match = null;
            var regex = Regex.Match(input, pattern);

            if (regex.Success)
                match = regex;

            return regex.Success;
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
