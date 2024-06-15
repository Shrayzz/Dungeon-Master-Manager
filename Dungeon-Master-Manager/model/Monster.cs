using System.Text.Json.Serialization;

namespace Dungeon_Master_Manager.model
{
    /// <summary>
    /// An entity that can live, die, attack, and is strong against some Elements
    /// </summary>
    public class Monster
    {
        /// <summary>
        /// A monster's health
        /// </summary>
        [JsonPropertyName("health")]
        public int Health { get; set; }


        /// <summary>
        /// Which element hurts the monster the most
        /// </summary>
        [JsonPropertyName("weakness")]
        public Element Weakness { get; set; }
        

        /// <summary>
        /// Which element the monster can resist
        /// </summary>
        [JsonPropertyName("resistance")]
        public Element Resistance { get; set; }
        

        /// <summary>
        /// How much to increase the damage by when attacked by someone of the right Element
        ///
        /// Ranges from 0 to 1
        /// </summary>
        [JsonPropertyName("weaknessPercent")]
        public double WeaknessPercent { get; set; }
        

        /// <summary>
        /// How much to decrease the damage by when attacked by someone of the right Element
        ///
        /// Ranges from 0 to 1
        ///
        /// A value of 0.90 would apply only 0.10 of the received damage
        /// </summary>
        [JsonPropertyName("resistancePercent")]
        public double ResistancePercent { get; set; }
        

        /// <summary>
        /// Constructor for Monster
        /// </summary>
        public Monster(int health, Element weakness, double weaknessPercent, Element resistance, double resistancePercent)
        {
            Health = health;
            Weakness = weakness;
            WeaknessPercent = weaknessPercent;
            Resistance = resistance;
            ResistancePercent = resistancePercent;
        }
        public Monster() { }
    }
}
