using System;
using System.Text.Json.Serialization;

/// <summary>
/// An entity that can live, die, attack, and is strong against some Elements
/// </summary>
namespace Dungeon_Master_Manager.model
{
    public class Monster
    {
        /// <summary>
        /// A monster's health
        /// </summary>
        [JsonPropertyName("health")]
        public uint Health { get; set; }


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
        public Monster(uint health, Element weakness, double weaknessPercent, Element resistance, double resistancePercent)
        {
            this.Health = health;
            this.Weakness = weakness;
            this.WeaknessPercent = weaknessPercent;
            this.Resistance = resistance;
            this.ResistancePercent = resistancePercent;
        }
        public Monster() { }
    }
}
