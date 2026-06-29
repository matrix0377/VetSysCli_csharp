using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Moq;
using VetSysCli.Application.DTOs;
using VetSysCli.Application.Interfaces;
using VetSysCli.Application.Services;
using VetSysCli.Core.Entities;

namespace VetSysCli.Tests;

public class AnimalServiceTests
{
    [Fact]
    public async Task CreateAsync_ShouldReturnMappedAnimal()
    {
        var repo = new Mock<IAnimalRepository>();
        var createdAnimal = new Animal { Id = Guid.NewGuid(), Name = "Luna", Type = "Cachorro", Breed = "Vira-lata" };
        repo.Setup(r => r.AddAsync(It.IsAny<Animal>())).ReturnsAsync(createdAnimal);

        var service = new AnimalService(repo.Object);
        var dto = new CreateAnimalDto { Name = "Luna", Type = "Cachorro", Breed = "Vira-lata", OwnerId = Guid.NewGuid() };

        var result = await service.CreateAsync(dto);

        Assert.NotNull(result);
        Assert.Equal(createdAnimal.Name, result.Name);
        Assert.Equal(createdAnimal.Type, result.Type);
        Assert.Equal(createdAnimal.Breed, result.Breed);
        repo.Verify(r => r.AddAsync(It.Is<Animal>(a => a.Name == dto.Name)), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_WhenAnimalExists_ShouldReturnMappedAnimal()
    {
        var repo = new Mock<IAnimalRepository>();
        var animal = new Animal { Id = Guid.NewGuid(), Name = "Rex", Type = "Cachorro", Breed = "Poodle" };
        repo.Setup(r => r.GetByIdAsync(animal.Id)).ReturnsAsync(animal);

        var service = new AnimalService(repo.Object);

        var result = await service.GetByIdAsync(animal.Id);

        Assert.NotNull(result);
        Assert.Equal(animal.Name, result.Name);
        Assert.Equal(animal.Type, result.Type);
        Assert.Equal(animal.Breed, result.Breed);
    }

    [Fact]
    public async Task UploadPhotoAsync_WithInvalidExtension_ShouldThrow()
    {
        var repo = new Mock<IAnimalRepository>();
        repo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Animal { Id = Guid.NewGuid() });

        var service = new AnimalService(repo.Object);
        using var stream = new MemoryStream(new byte[] { 1, 2, 3 });
        var file = new Mock<IFormFile>();
        file.Setup(f => f.FileName).Returns("test.txt");
        file.Setup(f => f.CopyToAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
            .Returns((Stream target, CancellationToken _) =>
            {
                stream.CopyTo(target);
                return Task.CompletedTask;
            });

        await Assert.ThrowsAsync<ArgumentException>(() => service.UploadPhotoAsync(Guid.NewGuid(), file.Object));
    }
}
