using System.Collections.Generic;

namespace YouTubeLoader.Models.VideoInfo
{
    public class RootObject
    {
        public object Annotations { get; set; }
        public double AverageRating { get; set; }
        public object AltTitle { get; set; }
        public string UploadDate { get; set; }
        public string Protocol { get; set; }
        public int ViewCount { get; set; }
        public string UploaderId { get; set; }
        public string Vcodec { get; set; }
        public object Playlist { get; set; }
        public int AgeLimit { get; set; }
        public string WebpageUrlBasename { get; set; }
        public string WebpageUrl { get; set; }
        public Subtitles Subtitles { get; set; }
        public int DislikeCount { get; set; }
        public object PlaylistIndex { get; set; }
        public string Title { get; set; }
        public object StartTime { get; set; }
        public object Creator { get; set; }
        public object EndTime { get; set; }
        public string Fulltitle { get; set; }
        public int Height { get; set; }
        public string Description { get; set; }
        public string Acodec { get; set; }
        public string FormatId { get; set; }
        public int Duration { get; set; }
        public string UploaderUrl { get; set; }
        public string Extractor { get; set; }
        public int Abr { get; set; }
        public string Uploader { get; set; }
        public object IsLive { get; set; }
        public HttpHeaders HttpHeaders { get; set; }
        public string FormatNote { get; set; }
        public string Url { get; set; }
        public string Format { get; set; }
        public string License { get; set; }
        public AutomaticCaptions AutomaticCaptions { get; set; }
        public List<string> Tags { get; set; }
        public string Thumbnail { get; set; }
        public List<Format> Formats { get; set; }
        public List<string> Categories { get; set; }
        public int LikeCount { get; set; }
        public int Width { get; set; }
        public List<Thumbnail> Thumbnails { get; set; }
        public string Filename { get; set; }
        public object PlayerUrl { get; set; }
        public string Id { get; set; }
        public string DisplayId { get; set; }
        public string Resolution { get; set; }
        public string Ext { get; set; }
        public string ExtractorKey { get; set; }
    }
}
