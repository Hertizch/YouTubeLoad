using System;
using System.Collections.Generic;
using YouTubeLoader.Extensions;

namespace YouTubeLoader.Models
{
    public class YouTubeObject : Bindable
    {
        private string _jsonFilename;
        public string JsonFilename
        {
            get { return _jsonFilename; }
            set { SetField(ref _jsonFilename, value); }
        }

        private VideoInfo.VideoInfo _infoJson;
        public VideoInfo.VideoInfo InfoJson
        {
            get { return _infoJson; }
            set { SetField(ref _infoJson, value); }
        }

        private int _processId;
        public int ProcessId
        {
            get { return _processId; }
            set { SetField(ref _processId, value); }
        }

        private TimeSpan _processActiveTimeSpan;
        public TimeSpan ProcessActiveTimeSpan
        {
            get { return _processActiveTimeSpan; }
            set { SetField(ref _processActiveTimeSpan, value); }
        }

        private DateTime _processStartDateTime;
        public DateTime ProcessStartDateTime
        {
            get { return _processStartDateTime; }
            set { SetField(ref _processStartDateTime, value); }
        }

        private DateTime _processEndDateTime;
        public DateTime ProcessEndDateTime
        {
            get { return _processEndDateTime; }
            set { SetField(ref _processEndDateTime, value); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetField(ref _name, value); }
        }

        private string _url;
        public string Url
        {
            get { return _url; }
            set { SetField(ref _url, value); }
        }

        private string _thumbnailUrl;
        public string ThumbnailUrl
        {
            get { return _thumbnailUrl; }
            set { SetField(ref _thumbnailUrl, value); }
        }

        private string _id;
        public string Id
        {
            get { return _id; }
            set { SetField(ref _id, value); }
        }

        private double _progressPercentage;
        public double ProgressPercentage
        {
            get { return _progressPercentage; }
            set { SetField(ref _progressPercentage, value); }
        }

        private double _size;
        public double Size
        {
            get { return _size; }
            set { SetField(ref _size, value); }
        }

        private string _sizeSuffix;
        public string SizeSuffix
        {
            get { return _sizeSuffix; }
            set { SetField(ref _sizeSuffix, value); }
        }

        private double _speed;
        public double Speed
        {
            get { return _speed; }
            set { SetField(ref _speed, value); }
        }

        private string _speedSuffix;
        public string SpeedSuffix
        {
            get { return _speedSuffix; }
            set { SetField(ref _speedSuffix, value); }
        }

        private string _eta;
        public string Eta
        {
            get { return _eta; }
            set { SetField(ref _eta, value); }
        }

        private List<string> _output;
        public List<string> Output
        {
            get { return _output; }
            set { SetField(ref _output, value); }
        }

        private bool _isDownloading;
        public bool IsDownloading
        {
            get { return _isDownloading; }
            set { SetField(ref _isDownloading, value); }
        }

        private bool _isDownloadingAudio;
        public bool IsDownloadingAudio
        {
            get { return _isDownloadingAudio; }
            set { SetField(ref _isDownloadingAudio, value); }
        }

        private bool _isInitializing;
        public bool IsInitializing
        {
            get { return _isInitializing; }
            set { SetField(ref _isInitializing, value); }
        }

        private bool _isGatheringInfo;
        public bool IsGatheringInfo
        {
            get { return _isGatheringInfo; }
            set { SetField(ref _isGatheringInfo, value); }
        }

        private string _status;
        public string Status
        {
            get { return _status; }
            set { SetField(ref _status, value); }
        }
    }
}
