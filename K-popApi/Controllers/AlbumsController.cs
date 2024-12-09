
using K_popApi.Services;


namespace K_popApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private readonly AlbumService _albumService;

        public AlbumsController()
        {
            _albumService = new AlbumService();
        }

        // GET: api/albums
        [HttpGet]
        public ActionResult<IEnumerable<Album>> GetAlbums()
        {
            return Ok(_albumService.GetAlbums());
        }

        // GET: api/albums/5
        [HttpGet("{id}")]
        public ActionResult<Album> GetAlbum(int id)
        {
            var album = _albumService.GetAlbum(id);
            if (album == null)
            {
                return NotFound();
            }
            return Ok(album);
        }

        // POST: api/albums
        [HttpPost]
        public ActionResult<Album> PostAlbum([FromBody] Album album)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _albumService.AddAlbum(album);
            return CreatedAtAction(nameof(GetAlbum), new { id = album.Id }, album);
        }

        // PUT: api/albums/5
        [HttpPut("{id}")]
        public IActionResult PutAlbum(int id, [FromBody] Album updatedAlbum)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var album = _albumService.GetAlbum(id);
            if (album == null)
            {
                return NotFound();
            }

            _albumService.UpdateAlbum(id, updatedAlbum);
            return NoContent();
        }

        // DELETE: api/albums/5
        [HttpDelete("{id}")]
        public IActionResult DeleteAlbum(int id)
        {
            var album = _albumService.GetAlbum(id);
            if (album == null)
            {
                return NotFound();
            }

            _albumService.DeleteAlbum(id);
            return NoContent();
        }

        // GET: api/albums/search?query=term
        [HttpGet("search")]
        public ActionResult<IEnumerable<Album>> SearchAlbums(string query)
        {
            var result = _albumService.SearchAlbums(query);
            if (!result.Any())
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
