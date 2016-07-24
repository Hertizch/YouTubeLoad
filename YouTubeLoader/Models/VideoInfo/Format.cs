namespace YouTubeLoader.Models.VideoInfo
{
    public class Format
    {
        public double Tbr { get; set; }
        public int Abr { get; set; }
        public string FormatId { get; set; }
        public int Preference { get; set; }
        public string Protocol { get; set; }
        public string FormatNote { get; set; }
        public string Url { get; set; }
        public string Vcodec { get; set; }
        public HttpHeaders2 HttpHeaders { get; set; }
        public int? Fps { get; set; }
        public int? Asr { get; set; }
        public int? Width { get; set; }
        public int Filesize { get; set; }
        public int? Height { get; set; }
        public string Acodec { get; set; }
        public object Language { get; set; }
        public string Ext { get; set; }
        public string Container { get; set; }
        public object PlayerUrl { get; set; }
        public string Resolution { get; set; }
    }
}
