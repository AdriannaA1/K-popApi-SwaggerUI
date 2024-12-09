
using Newtonsoft.Json;

namespace K_popApi.Services
{
    public class AlbumService
    {
        private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "albums.json");

        public AlbumService()
        {
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "[]");
            }
        }

        public List<Album> GetAlbums()
        {
            var albums = JsonConvert.DeserializeObject<List<Album>>(File.ReadAllText(_filePath));
            return albums;
        }

        public Album GetAlbum(int id)
        {
            var albums = GetAlbums();
            return albums.FirstOrDefault(a => a.Id == id);
        }

        public void AddAlbum(Album album)
        {
            var albums = GetAlbums();
            album.Id = albums.Count > 0 ? albums.Max(a => a.Id) + 1 : 1;
            albums.Add(album);
            SaveAlbums(albums);
        }

        public void UpdateAlbum(int id, Album updatedAlbum)
        {
            var albums = GetAlbums();
            var album = albums.FirstOrDefault(a => a.Id == id);

            if (album != null)
            {
                album.Title = updatedAlbum.Title;
                album.Artist = updatedAlbum.Artist;
                album.ReleaseDate = updatedAlbum.ReleaseDate;
                album.TrackList = updatedAlbum.TrackList;
                album.CoverUrl = updatedAlbum.CoverUrl;
                SaveAlbums(albums);
            }
        }

        public void DeleteAlbum(int id)
        {
            var albums = GetAlbums();
            var album = albums.FirstOrDefault(a => a.Id == id);

            if (album != null)
            {
                albums.Remove(album);
                SaveAlbums(albums);
            }
        }

        public List<Album> SearchAlbums(string searchTerm)
        {
            var albums = GetAlbums();
            return albums
                .Where(a => a.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                            a.Artist.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        private void SaveAlbums(List<Album> albums)
        {
            File.WriteAllText(_filePath, JsonConvert.SerializeObject(albums, Formatting.Indented));
        }
    }
}
