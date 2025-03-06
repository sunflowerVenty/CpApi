namespace CpApi.Requests
{
    public class FilmInfo
    {
        public int id_Film { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public string NameGenre { get; set; }
        public DateTime DateCreate { get; set; }
        public double Rating { get; set; }
    }

    public class FilmData
    {
        public string Status { get; set; }
        public FilmDataContainer Data { get; set; }
    }

    public class FilmDataContainer
    {
        public List<FilmInfo> movies { get; set; }
    }
}
