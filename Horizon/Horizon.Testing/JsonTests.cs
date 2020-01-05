using FluentAssertions;
using Horizon.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace Horizon.Testing
{
    public class JsonTests
    {
        [Environment]
        public void Environment()
        {
        }

        [Experiment]
        [Trait("Type", "Unit Tests")]
        [Trait("Concern", "Json")]
        public void ItemFile_should_be_savable_and_loadable_without_error(ItemFile itemFile)
        {
            "Assume we have a file"
                .Hypothesize(x =>
                {
                    itemFile = new ItemFile { FilePath = Path.Combine(Testing.AssemblyDirectory, "testfiles"), FileName = "testitem.item" };
                    itemFile.Category = "TestCategory";
                    itemFile.Description = "Test Description";
                    itemFile.ID = "testitem";
                    itemFile.ImageName = "\testimage.png";
                    itemFile.Name = "Test Item";
                    itemFile.Price = 30;
                    itemFile.Rarity = "Rare";
                    itemFile.Tags = new List<string>() { "reagent" };
                    itemFile.UnlocksBlueprint = new List<string> { "testitem2", "testitem3" };
                });
            "If we save the file"
                .Observe(x =>
                {
                    itemFile.Save();
                });
            "Then we should be able to load it with the information intact"
                .Conclude(x =>
                {
                    ItemFile loadedFile = JFile.Load<ItemFile>(Path.Combine(Testing.AssemblyDirectory, "testfiles"), "testitem.item");
                    itemFile.Category.Should().Be(loadedFile.Category);
                    itemFile.Description.Should().Be(loadedFile.Description);
                    itemFile.ID.Should().Be(loadedFile.ID);
                    itemFile.ImageName.Should().Be(loadedFile.ImageName);
                    itemFile.Name.Should().Be(loadedFile.Name);
                    itemFile.Price.Should().Be(loadedFile.Price);
                    itemFile.Rarity.Should().Be(loadedFile.Rarity);
                    itemFile.Tags.Should().Contain(loadedFile.Tags);
                    itemFile.UnlocksBlueprint.Should().Contain(loadedFile.UnlocksBlueprint);
                });
        }
    }
}