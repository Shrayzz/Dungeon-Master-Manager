using System.Text.Json.Serialization;
using System.Windows;

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
        public Element Element { get; }

        /// <summary>
        /// What kind of weapons the character can wield
        /// </summary>
        [JsonPropertyName("acceptedWeapons")]
        public WeaponClass AcceptedWeapons { get; }

        /// <summary>
        /// User's weapon
        /// </summary>
        [JsonPropertyName("weapon")]
        public Item? Weapon { get; set; }

        /// <summary>
        /// The items the character has equipped
        /// </summary>
        [JsonPropertyName("inventory")]
        public List<Item?> Inventory { get; } = [null, null, null, null];

        /// <summary>
        /// Percentage of health left
        /// </summary>
        [JsonPropertyName("health")]
        public uint Health { get; private set; }

        /// <summary>
        /// Creates a new character
        /// </summary>
        public Character(string name, Element element, WeaponClass class_)
        {
            Name = name;
            Element = element;
            AcceptedWeapons = class_;
            Health = 20;
        }

        public Character()
        {
        }

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
        public void Equip(int itemIndex)
        {
            var item = ((App)Application.Current).Inventory[itemIndex];
            if (Inventory.Contains(item))
            {
                MessageBox.Show("Cet object est déjà équipé !", $"Inventaire de {Name}", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                ((App)Application.Current).Intent = Intent.View;
                return;
            }

            if (item.Type == ItemType.Weapon && AcceptedWeapons != item.Range)
            {
                MessageBox.Show(
                    $"{Name} ne peux pas équiper cet arme ! Celle ci est de type {item.Range} et {Name} n'utilise que des armes de type {AcceptedWeapons}",
                    $"Inventaire de {Name}", MessageBoxButton.OK, MessageBoxImage.Error);
                ((App)Application.Current).Intent = Intent.View;
                return;
            }

            var availablePosition = Inventory.IndexOf(null);
            if (availablePosition == -1)
            {
                MessageBox.Show(
                    $"L'inventaire de {Name} est plein ! Enlève un objet d'abord !",
                    $"Inventaire de {Name}", MessageBoxButton.OK, MessageBoxImage.Error);
                ((App)Application.Current).Intent = Intent.View;
                return;
            }

            Inventory[availablePosition] = item;

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
            if (Weapon == null)
            {
                return;
            }

            double damage = Weapon.Value;

            if (monster.Weakness == Element)
            {
                damage *= (1 + monster.WeaknessPercent);
            }
            else if (monster.Resistance == Element)
            {
                damage *= (1 - monster.ResistancePercent);
            }


            monster.Health = (int)(monster.Health - damage);
        }


        public override string ToString()
        {
            var str = $"👤 Prénom: {Name}\n" +
                      $"🌟 Element: {Element}\n" +
                      $"⚔️ Type d'arme acceptée: {AcceptedWeapons}\n" +
                      $"❤️ Santé: {Health}\n";

            if (Weapon != null)
            {
                str += $"---- Inventaire de {Name} ----\n";
                str = Inventory.Aggregate(str, (current, it) => current + (it + "\n"));
            }

            return str;
        }
    }
}