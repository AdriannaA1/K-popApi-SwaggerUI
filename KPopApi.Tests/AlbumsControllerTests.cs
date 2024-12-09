using Xunit;
using Microsoft.AspNetCore.Mvc;
using K_popApi.Controllers;
using K_popApi.Models;
using K_popApi.Services;
using System.Collections.Generic;

namespace K_popApi.Tests
{
    public class AlbumsControllerTests
    {
        private readonly AlbumsController _controller;
        private readonly AlbumService _service;

        public AlbumsControllerTests()
        {
            _service = new AlbumService();
            _controller = new AlbumsController();
        }

        [Fact]
        public void GetAlbums_ReturnsOkResult_WithListOfAlbums()
        {
            // Act
            var result = _controller.GetAlbums();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var albums = Assert.IsType<List<Album>>(okResult.Value);
            Assert.Equal(3, albums.Count); // Zakładając, że w pliku są 3 albumy
        }

        [Fact]
        public void GetAlbum_ReturnsOkResult_WithAlbum()
        {
            // Arrange
            var albumId = 1;

            // Act
            var result = _controller.GetAlbum(albumId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var album = Assert.IsType<Album>(okResult.Value);
            Assert.Equal(albumId, album.Id);
        }

        [Fact]
        public void PostAlbum_CreatesAlbum()
        {
            // Arrange
            var newAlbum = new Album
            {
                Title = "Test Album",
                Artist = "Test Artist",
                ReleaseDate = System.DateTime.Now,
                TrackList = new List<string> { "Track 1", "Track 2" },
                CoverUrl = "http://example.com/cover.jpg"
            };

            // Act
            var result = _controller.PostAlbum(newAlbum);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var album = Assert.IsType<Album>(createdAtActionResult.Value);
            Assert.Equal("Test Album", album.Title);
        }

        [Fact]
        public void PutAlbum_UpdatesExistingAlbum()
        {
            // Arrange
            var albumId = 1;
            var updatedAlbum = new Album
            {
                Title = "Updated Album",
                Artist = "Updated Artist",
                ReleaseDate = System.DateTime.Now,
                TrackList = new List<string> { "Track 3", "Track 4" },
                CoverUrl = "http://example.com/updated_cover.jpg"
            };

            // Act
            var result = _controller.PutAlbum(albumId, updatedAlbum);

            // Assert
            Assert.IsType<NoContentResult>(result);

            var album = _service.GetAlbum(albumId);
            Assert.Equal("Updated Album", album.Title);
            Assert.Equal("Updated Artist", album.Artist);
            Assert.Equal("http://example.com/updated_cover.jpg", album.CoverUrl);
        }

        [Fact]
        public void DeleteAlbum_DeletesExistingAlbum()
        {
            // Arrange
            var albumId = 1;

            // Act
            var result = _controller.DeleteAlbum(albumId);

            // Assert
            Assert.IsType<NoContentResult>(result);

            var album = _service.GetAlbum(albumId);
            Assert.Null(album);
        }
    }
}
