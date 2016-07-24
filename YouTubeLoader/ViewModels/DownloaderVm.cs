using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using YouTubeLoader.Extensions;
using YouTubeLoader.Interfaces;
using YouTubeLoader.Models;
using YouTubeLoader.Properties;
using YouTubeLoader.Utilities;

namespace YouTubeLoader.ViewModels
{
    public class DownloaderVm : Bindable, IPageViewModel
    {
        public DownloaderVm()
        {
            YouTubeObjects = new ObservableCollection<YouTubeObject>();

            /*
            YouTubeObjects.Add(new YouTubeObject
            {
                Name = "Factorio New Alpha 15",
                Url = "https://www.youtube.com/watch?v=2CWNJIQ_TcQ",
                ThumbnailUrl = "https://i.ytimg.com/vi/77iXnAWhYXw/maxresdefault.jpg"
            });
            */
        }

        #region Properties

        public string Name => nameof(DownloaderVm);

        private ObservableCollection<YouTubeObject> _youTubeObjects; 
        public ObservableCollection<YouTubeObject> YouTubeObjects
        {
            get { return _youTubeObjects; }
            set { SetField(ref _youTubeObjects, value); }
        }

        #endregion

        #region Command -- Execute YouTube-Dl

        private RelayCommand _commandExecuteYouTubeDl;
        public RelayCommand CommandExecuteYouTubeDl
        {
            get
            {
                return _commandExecuteYouTubeDl ??
                       (_commandExecuteYouTubeDl = new RelayCommand(p => Execute_ExecuteYouTubeDl(p as YouTubeObject), p => true));
            }
        }

        private static async void Execute_ExecuteYouTubeDl(YouTubeObject youTubeObject, bool addVideoToQueue = false, bool autoStart = false)
        {
            var sb = new StringBuilder();

            sb.Append($"-f \"bestvideo+bestaudio/best\" -o \"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\%(title)s-%(id)s.%(ext)s\" https://www.youtube.com/watch?v={youTubeObject.Id}");

            // If to add new video to list, get title
            string specialArgs = null;

            if (addVideoToQueue)
                specialArgs = $"--skip-download --get-thumbnail --get-title https://www.youtube.com/watch?v={youTubeObject.Id}";

            // Execute youtube-dl.exe
            using (var process = new Process
            {
                StartInfo =
                {
                    FileName = "youtube-dl.exe",
                    Arguments = addVideoToQueue ? specialArgs : sb.ToString(),
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true
                }
            })
            {
                // Output data
                process.OutputDataReceived += (o, args) =>
                {
                    if (args.Data == null) return;

                    if (youTubeObject.Output == null)
                        youTubeObject.Output = new List<string>();

                    youTubeObject.Output?.Add(args.Data);

                    YouTubeUtilities.ProcessOutput(youTubeObject, addVideoToQueue);

                    Debug.WriteLine(args.Data);
                };

                // Exited
                process.Exited += (sender, args) =>
                {
                    Logger.Write($"Terminated process: {youTubeObject.ProcessId}", true);

                    // Reset flags
                    youTubeObject.IsGatheringInfo = false;
                    youTubeObject.IsDownloading = false;
                    youTubeObject.IsInitializing = false;

                    if (youTubeObject.Status != "Complete")
                        youTubeObject.Status = "Stopped";

                    // If to download immidiately
                    if (autoStart)
                        Execute_ExecuteYouTubeDl(youTubeObject);

                    Debug.WriteLine(@"Process youtube-dl.exe Exited");
                };

                // Start process
                process.Start();

                Logger.Write($"Started process: {process.Id} -- Arguments passed: {process.StartInfo.Arguments}", true);

                process.BeginOutputReadLine();

                // Set flags
                if (addVideoToQueue)
                    youTubeObject.IsGatheringInfo = true;

                youTubeObject.IsInitializing = true;
                youTubeObject.Status = "Working...";

                // Store process ID
                youTubeObject.ProcessId = process.Id;

                // Wait untill exited
                await process.WaitForExitAsync();
            }
        }

        #endregion

        #region Command -- Kill YouTube-Dl

        private RelayCommand _commandKillYouTubeDl;
        public RelayCommand CommandKillYouTubeDl
        {
            get
            {
                return _commandKillYouTubeDl ??
                       (_commandKillYouTubeDl = new RelayCommand(p => Execute_KillYouTubeDl(p as YouTubeObject), p => true));
            }
        }

        private static void Execute_KillYouTubeDl(YouTubeObject youTubeObject)
        {
            foreach (var process in Process.GetProcesses().Where(process => youTubeObject != null && process.Id == youTubeObject.ProcessId))
                process.Kill();
        }

        #endregion

        #region Command -- Add Video To Queue

        private RelayCommand _commandAddVideoToQueue;
        public RelayCommand CommandAddVideoToQueue
        {
            get
            {
                return _commandAddVideoToQueue ??
                       (_commandAddVideoToQueue = new RelayCommand(p => Execute_AddVideoToQueue(p as string), p => !string.IsNullOrWhiteSpace((string)p) && p.ToString().Length > 3));
            }
        }

        private async void Execute_AddVideoToQueue(string url)
        {
            var id = YouTubeUtilities.GetYouTubeIdFromUrl(url);

            if (YouTubeObjects.Any(x => x.Id == id))
            {
                Logger.Write($"Attempt to add video object with id: {id} failed. It already exists in the collection.", true);
                return;
            }

            YouTubeObjects.Add(new YouTubeObject
            {
                Url = url,
                Id = id
            });

            YouTubeObjects[YouTubeObjects.IndexOf(YouTubeObjects.First(x => x.Id == id))].Status = "Verifying...";
            YouTubeObjects[YouTubeObjects.IndexOf(YouTubeObjects.First(x => x.Id == id))].IsGatheringInfo = true;

            // Verify if the url actually can be reached
            HttpStatusCode httpStatusCode = 0;

            try
            {
                httpStatusCode = await WebUtilities.GetResponseStatusCode(url);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                YouTubeObjects[YouTubeObjects.IndexOf(YouTubeObjects.First(x => x.Id == id))].Status = "Invalid";
                YouTubeObjects[YouTubeObjects.IndexOf(YouTubeObjects.First(x => x.Id == id))].IsGatheringInfo = false;
                Logger.Write($"GetResponseStatusCode failed on url: {url} -- Status code returned: {httpStatusCode}", true);
                return;
            }

            if (httpStatusCode != HttpStatusCode.OK)
            {
                YouTubeObjects[YouTubeObjects.IndexOf(YouTubeObjects.First(x => x.Id == id))].Status = "Invalid";
                YouTubeObjects[YouTubeObjects.IndexOf(YouTubeObjects.First(x => x.Id == id))].IsGatheringInfo = false;
                Logger.Write($"GetResponseStatusCode failed on url: {url} -- Status code returned: {httpStatusCode}", true);
                return;
            }

            YouTubeObjects[YouTubeObjects.IndexOf(YouTubeObjects.First(x => x.Id == id))].Status = null;
            YouTubeObjects[YouTubeObjects.IndexOf(YouTubeObjects.First(x => x.Id == id))].IsGatheringInfo = false;

            // Execute and get info
            Execute_ExecuteYouTubeDl(YouTubeObjects.First(x => x.Id == id), true, Settings.Default.AutoDownloadOnAdd);
        }

        #endregion

        #region Command - Remove Video From Queue

        private RelayCommand _commandRemoveVideoFromQueue;
        public RelayCommand CommandRemoveVideoFromQueue
        {
            get
            {
                return _commandRemoveVideoFromQueue ??
                       (_commandRemoveVideoFromQueue = new RelayCommand(p => Execute_RemoveVideoFromQueue(p as YouTubeObject), p => true));
            }
        }

        private void Execute_RemoveVideoFromQueue(YouTubeObject youTubeObject)
        {
            if (youTubeObject.IsDownloading || youTubeObject.IsGatheringInfo)
            {
                Debug.WriteLine("Object is busy");
                return;
            }

            YouTubeObjects.RemoveAt(YouTubeObjects.IndexOf(youTubeObject));
        }

        #endregion

        #region Command -- Move Video Object Up

        private RelayCommand _commandMoveVideoObjectUp;
        public RelayCommand CommandMoveVideoObjectUp
        {
            get
            {
                return _commandMoveVideoObjectUp ??
                       (_commandMoveVideoObjectUp = new RelayCommand(p => Execute_MoveVideoObjectUp(p as YouTubeObject), p => true));
            }
        }

        private void Execute_MoveVideoObjectUp(YouTubeObject youTubeObject)
        {
            var oldIndex = YouTubeObjects.IndexOf(youTubeObject);
            var newIndex = oldIndex - 1;

            if (oldIndex <= 0)
                return;

            YouTubeObjects.Move(oldIndex, newIndex);
        }

        #endregion

        #region Command -- Move Video Object Down

        private RelayCommand _commandMoveVideoObjectDown;
        public RelayCommand CommandMoveVideoObjectDown
        {
            get
            {
                return _commandMoveVideoObjectDown ??
                       (_commandMoveVideoObjectDown = new RelayCommand(p => Execute_MoveVideoObjectDown(p as YouTubeObject), p => true));
            }
        }

        private void Execute_MoveVideoObjectDown(YouTubeObject youTubeObject)
        {
            var oldIndex = YouTubeObjects.IndexOf(youTubeObject);
            var newIndex = oldIndex + 1;

            if (oldIndex >= YouTubeObjects.Count - 1 || newIndex > YouTubeObjects.Count - 1)
                return;

            YouTubeObjects.Move(oldIndex, newIndex);
        }

        #endregion
    }
}
