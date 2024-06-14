using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Dungeon_Master_Manager.model
{
    /// <summary>
    /// An entity that can live, die and attack
    /// </summary>
    public class Character
    {
        /// <summary>
        /// A cute name for the character
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// What element does the character master
        /// </summary>
        [JsonPropertyName("element")]
        public Element Element { get; set; }

        /// <summary>
        /// What kind of weapons the character can wield
        /// </summary>
        [JsonPropertyName("acceptedWeapons")]
        public WeaponClass AcceptedWeapons { get; set; }

        /// <summary>
        /// User's weapon
        /// </summary>
        [JsonPropertyName("weapon")]
        public Item? Weapon { get; set; }

        /// <summary>
        /// The items the character has equipped
        /// </summary>
        [JsonPropertyName("inventory")]
        public List<Item> Inventory { get; set; }

        /// <summary>
        /// Percentage of health left
        /// </summary>
        [JsonPropertyName("health")]
        public uint Health { get; set; }

        /// <summary>
        /// Creates a new character
        /// </summary>
        public Character(string name, Element element, WeaponClass class_)
        {
            this.Name = name;
            this.Element = element;
            this.AcceptedWeapons = class_;
            this.Health = 20;
            this.Inventory = new List<Item>();
        }

        public Character() { }
        /// <summary>
        /// Damages the character by a given amount
        /// </summary>
        public void Damage(uint amount)
        {

            if (amount >= Health)
            {
                Health = 0;
            }
            else
            {
                Health -= amount;
            }
        }

        /// <summary>
        /// Heals the character by a given amount
        /// </summary>
        public void Heal(uint amount)
        {
            Health += amount;
            if (Health > 20)
            {
                Health = 20;
            }
        }

        /// <summary>
        /// Adds an item to the user's inventory
        /// </summary>
        public void Equip(Item item)
        {
            if (Inventory.Contains(item))
            {
                throw new InvalidOperationException("Item is already equipped.");
            }

            if (item.Type == ItemType.Weapon && AcceptedWeapons == item.Range)
            {
                throw new InvalidOperationException("This character cannot wield this weapon.");
            }

            Inventory.Add(item);

            if (item.Type == ItemType.Weapon)
            {
                Weapon = item;
            }
        }

        /// <summary>
        /// Removes an item from a character's inventory
        /// </summary>
        public void UnEquip(Item item)
        {
            if (!Inventory.Contains(item))
            {
                throw new InvalidOperationException("Item is not equipped.");
            }

            Inventory.Remove(item);

            if (item == Weapon)
            {
                Weapon = null;
            }
        }

        /// <summary>
        /// Deals damage to a selected monster
        /// </summary>
        public void Attack(Monster monster)
        {
            if (this.Weapon == null) return;

            double multiplier = 1.0;

            if (monster.Weakness == this.Element)
            {
                multiplier += monster.WeaknessPercent;
            }

            if (monster.Resistance == this.Element)
            {
                multiplier -= monster.ResistancePercent;
            }

            // Apply weapon value to the multiplier
            multiplier *= this.Weapon.Value;

            // Reduce monster's health
            monster.Health = (uint)(monster.Health * multiplier);
        }

    }
}
