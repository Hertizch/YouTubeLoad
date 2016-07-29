using System;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using YouTubeLoader.Extensions;
using YouTubeLoader.Interfaces;
using YouTubeLoader.Properties;
using YouTubeLoader.Utilities;

namespace YouTubeLoader.ViewModels
{
    public class SettingsVm : Bindable, IPageViewModel
    {
        public SettingsVm()
        {
            /*
            if (CommandGetExeVersion.CanExecute("youtube-dl.exe"))
                CommandGetExeVersion.Execute("youtube-dl.exe");

            if (CommandGetExeVersion.CanExecute("ffmpeg.exe"))
                CommandGetExeVersion.Execute("ffmpeg.exe");
                */
        }

        public string Name => nameof(SettingsVm);

        private RelayCommand _commandGetExeVersion;
        public RelayCommand CommandGetExeVersion
        {
            get
            {
                return _commandGetExeVersion ??
                       (_commandGetExeVersion = new RelayCommand(p => Execute_GetExeVersion(p as string), p => true));
            }
        }

        private static async void Execute_GetExeVersion(string application)
        {
            var sb = new StringBuilder();

            if (application == "youtube-dl.exe")
                sb.Append("--version");

            if (application == "ffmpeg.exe")
                sb.Append("-version");

            var process = new Process
            {
                StartInfo =
                {
                    FileName = application,
                    Arguments = sb.ToString(),
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true
                }
            };

            // Output data
            process.OutputDataReceived += (o, args) =>
            {
                if (args.Data == null) return;
                ProcessOutput(args.Data, application);
            };

            // Exited
            process.Exited += (sender, args) =>
            {
                Logger.Write($"Terminated process", true);
            };

            process.Start();

            Logger.Write($"Started process: {process.StartInfo.FileName} ({process.Id}) -- Arguments passed: {process.StartInfo.Arguments}", true);

            process.BeginOutputReadLine();

            // Wait untill exited
            await process.WaitForExitAsync();
        }

        private static void ProcessOutput(string output, string application)
        {
            if (application == "ffmpeg.exe")
            {
                var regex = Regex.Match(output, "ffmpeg version\\s*(.*)\\s*Copyright \\(c\\) 2000-2016 the FFmpeg developers");
                if (regex.Success)
                {
                    Settings.Default.FfmpegInstalledVersion = regex.Groups[1].Value;

                }
            }

            if (application == "youtube-dl.exe")
            {
                Settings.Default.YouTubeDlInstalledVersion = output;
            }
        }
    }
}
