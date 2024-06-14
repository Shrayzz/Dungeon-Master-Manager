using System;

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
        private uint health;

        public uint Health
        {
            get { return health; }
            set { health = value; }
        }

        /// <summary>
        /// Which element hurts the monster the most
        /// </summary>
        private Element weakness;

        public Element Weakness
        {
            get { return weakness; }
        }

        /// <summary>
        /// Which element the monster can resist
        /// </summary>
        private Element resistance;

        public Element Resistance
        {
            get { return resistance; }
        }

        /// <summary>
        /// How much to increase the damage by when attacked by someone of the right Element
        ///
        /// Ranges from 0 to 1
        /// </summary>
        private double weaknessPercent;

        public double WeaknessPercent
        {
            get { return weaknessPercent; }
        }

        /// <summary>
        /// How much to decrease the damage by when attacked by someone of the right Element
        ///
        /// Ranges from 0 to 1
        ///
        /// A value of 0.90 would apply only 0.10 of the received damage
        /// </summary>
        private double resistancePercent;

        public double ResistancePercent
        {
            get { return resistancePercent; }
        }

        /// <summary>
        /// Constructor for Monster
        /// </summary>
        public Monster(uint health, Element weakness, double weaknessPercent, Element resistance, double resistancePercent)
        {
            this.health = health;
            this.weakness = weakness;
            this.weaknessPercent = weaknessPercent;
            this.resistance = resistance;
            this.resistancePercent = resistancePercent;
        }
    }
}
