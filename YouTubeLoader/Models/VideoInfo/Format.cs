using System.Net.Http.Headers;

namespace YouTubeLoader.Models.VideoInfo
{
    public class Format
    {
        public string Protocol { get; set; }
        public string Acodec { get; set; }
        public int? Width { get; set; }
        public int? Asr { get; set; }
        public string FormatNote { get; set; }
        public double Tbr { get; set; }
        public string format { get; set; }
        public string Vcodec { get; set; }
        public int Abr { get; set; }
        public string Url { get; set; }
        public object Language { get; set; }
        public int Filesize { get; set; }
        public HttpHeaders HttpHeaders { get; set; }
        public string FormatId { get; set; }
        public int? Fps { get; set; }
        public int Preference { get; set; }
        public int? Height { get; set; }
        public string Ext { get; set; }
        public string Container { get; set; }
        public object PlayerUrl { get; set; }
        public string Resolution { get; set; }
    }
}
