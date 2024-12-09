using Xunit;
using K_popApi.Controllers;
using K_popApi.Models;
using Microsoft.AspNetCore.Mvc;


namespace K_popApi.Tests
{
    public class AlbumsControllerTests
    {
        [Fact]
        public void GetAlbums_ReturnsOkResult()
        {
            // Arrange
            var controller = new AlbumsController();

            // Act
            var result = controller.GetAlbums();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetAlbum_ReturnsNotFound_ForInvalidId() 
        {
            // Arrange
            var controller = new AlbumsController();

            // Act
            var result = controller.GetAlbum(100); 

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void PostAlbum_ReturnsCreatedAtAction()
        {
            // Arrange
            var controller = new AlbumsController();
            var newAlbum = new Album
            {
                Title = "Test Album",
                Artist = "Test Artist",
                ReleaseDate = DateTime.Now,
                TrackList = new List<string>(),
                CoverUrl = "https://example.com"
            };

            // Act
            var result = controller.PostAlbum(newAlbum);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var createdAlbum = Assert.IsAssignableFrom<Album>(createdAtActionResult.Value);
            Assert.Equal(newAlbum.Title, createdAlbum.Title);
        }

        [Fact]
        public void PutAlbum_ReturnsNotFound_ForInvalidId()
        {
            // Arrange
            var controller = new AlbumsController();
            var updatedAlbum = new Album
            {
                Id = 1,
                Title = "Updated Album Title",
                Artist = "Updated Artist",
                ReleaseDate = DateTime.Now,
                TrackList = new List<string>(),
                CoverUrl = "https://example.com"
            };

            // Act
            var result = controller.PutAlbum(999, updatedAlbum); 

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void DeleteAlbum_ReturnsNotFound_ForInvalidId()
        {
            // Arrange
            var controller = new AlbumsController();

            // Act
            var result = controller.DeleteAlbum(999); 

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}






