using System;

/// <summary>
/// An object with a specific usage
/// </summary>
namespace Dungeon_Master_Manager.model {
	public class Item
	{
		/// <summary>
		/// Name of the item
		/// </summary>
		private string name;
		
		public string Name
		{
			get { return name; }
		}

		/// <summary>
		/// Description for the item
		/// </summary>
		private string description;

		public string Description
		{
			get { return description; }
		}

		/// <summary>
		/// What kind of item it is, something that can be consumed or something that can damage monsters
		/// </summary>
		private ItemType type;

		public ItemType Type
		{
			get { return type; }
		}

		/// <summary>
		/// If the item is a weapon, stores the type of the weapon (range or melee) otherwise stores null
		/// </summary>
		private WeaponClass? range;

		public WeaponClass? Range
		{
			get { return range; }
		}

		/// <summary>
		/// The value associated with the item type, may be health regained or weapon damage or something else
		/// </summary>
		private int value;

		public int Value
		{
			get { return value; }
		}
		
		public Item(string name, string description, ItemType type, WeaponClass? range, int value)
		{
			this.name = name;
			this.description = description;
			this.type = type;
			this.range = range;
			this.value = value;
		}
	}
}
