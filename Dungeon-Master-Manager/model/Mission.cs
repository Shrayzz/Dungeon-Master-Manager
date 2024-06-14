using System;
using System.Windows;

namespace Dungeon_Master_Manager.model
{
    public class Mission
    {
        /// <summary>
        /// The monsters the mission features.
        /// We store copies of the monsters to make the mission replayable, so no pointers
        /// </summary>
        private Monster[] monsters;

        public Monster[] Monsters
        {
            get { return (Monster[])monsters.Clone(); }
        }

        /// <summary>
        /// How much gold to reward the player with if the mission is successful, may be 0
        /// </summary>
        private uint reward_gold;
        

        /// <summary>
        /// Items to reward the player with if the mission is successful
        /// </summary>
        private Item[] reward_items;
        

        /// <summary>
        /// Characters to reward the player with if the mission is successful
        /// </summary>
        private Character[] reward_characters;


        /// <summary>
        /// Constructor to initialize the Mission
        /// </summary>
        public Mission(Monster[] monsters, uint rewardGold, Item[] rewardItems, Character[] rewardCharacters)
        {
            this.monsters = (Monster[])monsters.Clone();
            this.reward_gold = rewardGold;
            this.reward_items = (Item[])rewardItems.Clone();
            this.reward_characters = (Character[])rewardCharacters.Clone();
        }

        /// <summary>
        /// Method to check if the mission is completed by defeating all monsters
        /// </summary>
        public bool IsCompleted()
        {
            foreach (var monster in monsters)
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
            ((App)Application.Current).Bank = reward_gold;

            foreach (Item item in reward_items)
            {
                ((App)Application.Current).StoreItem(item);
            }

            foreach (Character character in reward_characters)
            {
                ((App)Application.Current).AddCharacter(character);
            }
        }
    }
}
