using System;
using System.Collections.Generic;

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
        public string Name { get; }

        /// <summary>
        /// What element does the character master
        /// </summary>
        public Element Element { get; }

        /// <summary>
        /// What kind of weapons the character can wield
        /// </summary>
        public WeaponClass AcceptedWeapons { get; }

        /// <summary>
        /// User's weapon
        /// </summary>
        private Item? weapon;
        public Item? Weapon
        {
            get { return weapon; }
            private set { weapon = value; }
        }

        /// <summary>
        /// The items the character has equipped
        /// </summary>
        private List<Item> inventory;
        public IReadOnlyList<Item> Inventory
        {
            get { return inventory.AsReadOnly(); }
        }

        /// <summary>
        /// Percentage of health left
        /// </summary>
        private uint health;
        public uint Health
        {
            get { return health; }
            private set { health = value; }
        }

        /// <summary>
        /// Creates a new character
        /// </summary>
        public Character(string name, Element element, WeaponClass class_)
        {
            this.Name = name;
            this.Element = element;
            this.AcceptedWeapons = class_;
            this.health = 20;
            this.inventory = new List<Item>();
        }

        /// <summary>
        /// Damages the character by a given amount
        /// </summary>
        public void Damage(uint amount)
        {

            if (amount >= health)
            {
                health = 0;
            }
            else
            {
                health -= amount;
            }
        }

        /// <summary>
        /// Heals the character by a given amount
        /// </summary>
        public void Heal(uint amount)
        {
            health += amount;
            if (health > 20)
            {
                health = 20;
            }
        }

        /// <summary>
        /// Adds an item to the user's inventory
        /// </summary>
        public void Equip(Item item)
        {
            if (inventory.Contains(item))
            {
                throw new InvalidOperationException("Item is already equipped.");
            }

            if (item.Type == ItemType.Weapon && AcceptedWeapons == item.Range)
            {
                throw new InvalidOperationException("This character cannot wield this weapon.");
            }

            inventory.Add(item);

            if (item.Type == ItemType.Weapon)
            {
                weapon = item;
            }
        }

        /// <summary>
        /// Removes an item from a character's inventory
        /// </summary>
        public void UnEquip(Item item)
        {
            if (!inventory.Contains(item))
            {
                throw new InvalidOperationException("Item is not equipped.");
            }

            inventory.Remove(item);

            if (item == weapon)
            {
                weapon = null;
            }
        }

        /// <summary>
        /// Deals damage to a selected monster
        /// </summary>
        public void Attack(Monster monster)
        {
            if (this.weapon == null) return;

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
            multiplier *= this.weapon.Value;

            // Reduce monster's health
            monster.Health = (uint)(monster.Health * multiplier);
        }

    }
}
