using System.Text.Json.Serialization;

namespace Dungeon_Master_Manager.model {
	/// <summary>
	/// An object with a specific usage
	/// </summary>
	public class Item
	{
		/// <summary>
		/// Name of the item
		/// </summary>
		[JsonPropertyName("name")]
		public string Name { get; set; }
		

		/// <summary>
		/// Description for the item
		/// </summary>
		[JsonPropertyName("description")]
		public string Description { get; set; }
		

		/// <summary>
		/// What kind of item it is, something that can be consumed or something that can damage monsters
		/// </summary>
		[JsonPropertyName("type")]
		public ItemType Type { get; set; }

		/// <summary>
		/// If the item is a weapon, stores the type of the weapon (range or melee) otherwise stores null
		/// </summary>
		[JsonPropertyName("range")]
		public WeaponClass? Range { get; set; }
		

		/// <summary>
		/// The value associated with the item type, may be health regained or weapon damage or something else
		/// </summary>
		[JsonPropertyName("value")]
		public int Value { get; set; }
		
		public Item() { }
		public Item(string name, string description, ItemType type, WeaponClass? range, int value)
		{
			Name = name;
			Description = description;
			Type = type;
			Range = range;
			Value = value;
		}
		
		public override string ToString()
		{
			string valueTypeDescription = Type == ItemType.Weapon ? "Dégâts" : "Valeure";
			string valueEmoji = Type == ItemType.Weapon ? "⚔️" : "❤️";
			string rangeText = Range.HasValue ?  $"🏹 Portée: {Range.ToString()}\n" : "";
            
			return $"🛠️ Objet: {Name}\n" +
			       $"📜 Description: {Description}\n" +
			       $"🔖 Type: {Type}\n" +
			       rangeText +
			       $"{valueEmoji} {valueTypeDescription}: {Value}";
		}
	}
}
