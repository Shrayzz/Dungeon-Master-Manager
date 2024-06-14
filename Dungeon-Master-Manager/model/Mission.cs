using System;
using System.Windows;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Dungeon_Master_Manager.model
{
    public class Mission
    {
        /// <summary>
        /// The mission's name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        /// <summary>
        /// A simple summary of the mission
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// Coordinates on the map
        /// </summary>
        [JsonPropertyName("marginTop")]
        public int MarginTop { get; set; }
        
        [JsonPropertyName("marginLeft")]
        public int MarginLeft { get; set; }
        
        [JsonPropertyName("marginRight")]
        public int MarginRight { get; set; }
        
        [JsonPropertyName("marginBottom")]
        public int MarginBottom { get; set; }
        
        /// <summary>
        /// The monsters the mission features.
        /// We store copies of the monsters to make the mission replayable, so no pointers
        /// </summary>
        [JsonPropertyName("monsters")]
        public Monster[] Monsters { get; set; }
        

        /// <summary>
        /// How much gold to reward the player with if the mission is successful, may be 0
        /// </summary>
        [JsonPropertyName("reward_gold")]
        public uint RewardGold { get; set; }
        

        /// <summary>
        /// Items to reward the player with if the mission is successful
        /// </summary>
        [JsonPropertyName("reward_items")]
        public Item[] RewardItems { get; set; }
        

        /// <summary>
        /// Characters to reward the player with if the mission is successful
        /// </summary>
        [JsonPropertyName("reward_characters")]
        public Character[] RewardCharacters { get; set; }


        /// <summary>
        /// Constructor to initialize the Mission
        /// </summary>
        public Mission(string name, string description, Monster[] monsters, uint rewardGold, Item[] rewardItems, Character[] rewardCharacters, int marginTop, int marginBottom,
            int marginLeft, int marginRight)
        {
            Name = name;
            Description = description;
            MarginTop = marginTop;
            MarginLeft = marginLeft;
            MarginRight = marginRight;
            MarginBottom = marginBottom;
            Monsters = (Monster[])monsters.Clone();
            RewardGold = rewardGold;
            RewardItems = (Item[])rewardItems.Clone();
            RewardCharacters = (Character[])rewardCharacters.Clone();

        }

        /// <summary>
        /// Method to check if the mission is completed by defeating all monsters
        /// </summary>
        public bool IsCompleted()
        {
            foreach (var monster in Monsters)
            {
                if (monster.Health > 0)
                {
                    return false;
                }
            }
            return true;
        }


        /// <summary>
        /// Method to grant rewards to the player
        /// </summary>
        public void GrantRewards()
        {
            ((App)Application.Current).Bank = RewardGold;

            foreach (Item item in RewardItems)
            {
                ((App)Application.Current).StoreItem(item);
            }

            foreach (Character character in RewardCharacters)
            {
                ((App)Application.Current).AddCharacter(character);
            }
        }
    }
    
}
