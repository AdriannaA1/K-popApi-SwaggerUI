using System.ComponentModel.DataAnnotations;

namespace K_popApi.Models
{
    public class Album
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Artist is required.")]
        public string Artist { get; set; }

        [Required(ErrorMessage = "Release Date is required.")]
        public DateTime ReleaseDate { get; set; }

        [Required(ErrorMessage = "Track List is required.")]
        public List<string> TrackList { get; set; }

        [Url(ErrorMessage = "Invalid URL format.")]
        public string CoverUrl { get; set; }
    }
}
