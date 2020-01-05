using Horizon.ObjectModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Horizon.Json
{
    public class ItemFile : JFile, IFileToModel<Item>
    {
        /// <summary>
        /// The category of the item. Used for organizational and filtering purposes.
        /// </summary>
        [JsonProperty("category")]
        public string Category { get; set; }

        /// <summary>
        /// The description of the item.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// The string ID of the item. This is how the item is referenced behind the scenes.
        /// </summary>
        [JsonProperty("itemName")]
        public string ID { get; set; }

        /// <summary>
        /// The item's image.
        /// </summary>
        [JsonProperty("inventoryIcon")]
        public string ImageName { get; set; }

        /// <summary>
        /// The name of the item. This is what is displayed in game.
        /// </summary>
        [JsonProperty("shortdescription")]
        public string Name { get; set; }

        /// <summary>
        /// The value of the item in pixels.
        /// </summary>
        [JsonProperty("price")]
        public int Price { get; set; }

        /// <summary>
        /// A list of radio messages that are played the first time the item is picked up.
        /// </summary>
        [JsonProperty("radioMessagesOnPickup")]
        public List<string> RadioMessages { get; set; }

        /// <summary>
        /// The rarity of the item. Can be Common, Uncommon, Rare, Legendary, or Essential.
        /// </summary>
        [JsonProperty("rarity")]
        public string Rarity { get; set; }

        /// <summary>
        /// A list of tags that the item carries. Mostly determines which bag or tab the item
        /// appears in.
        /// </summary>
        [JsonProperty("itemTags")]
        public List<string> Tags { get; set; }

        /// <summary>
        /// A list of blueprints that the item unlocks when it is picked up by the player.
        /// </summary>
        [JsonProperty("learnBlueprintsOnPickup")]
        public List<string> UnlocksBlueprint { get; set; }

        public Item CreateModel()
        {
            Item item = new Item()
            {
                ID = this.ID,
                Name = this.Name,
                Price = this.Price,
                ImageName = this.ImageName,
                FilePath = this.FilePath,
                FileName = this.FileName
            };
            item.IsSaved = true;
            return item;
        }

        public void PopulateFile(Item model)
        {
            this.ID = model.ID;
            this.Name = model.Name;
            this.Price = model.Price;
            this.ImageName = model.ImageName;
            this.FilePath = model.FilePath;
            this.FileName = model.FileName;
        }
    }
}