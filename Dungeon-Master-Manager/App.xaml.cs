using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json; // for the Append method
using System.Windows;
using System.Windows.Controls;
using Dungeon_Master_Manager.view;
using Dungeon_Master_Manager.model;

namespace Dungeon_Master_Manager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// All the map's missions
        /// </summary>
        private List<Mission> missions = [];

        /// <summary>
        /// What is the player trying to do ?
        /// </summary>
        public Intent Intent { get; set; } = Intent.View;

        /// <summary>
        /// The player's money
        /// </summary>
        public uint Bank { get; set; } = 500;

        /// <summary>
        /// The selected mission
        /// </summary>
        public uint SelectedMission { get; set; } = 0;

        /// <summary>
        /// The list of all available characters the player recruited
        /// </summary>
        private Character[] characters = [];

        public Character[] Characters
        {
            get { return characters; }
        }

        /// <summary>
        /// Player's inventory
        /// </summary>
        public Item[] Inventory { get; private set; } = [];

        /// <summary>
        /// All monsters that exist in the game
        /// </summary>
        private Monster[] monsters = [];

        /// <summary>
        /// The characters the player has selected for a mission
        /// </summary>
        public Character?[] Team { get; private set; } = [null, null, null, null];

        public List<Mission> Missions
        {
            get { return missions; }
            set { missions = value; }
        }

        public Monster[] Monsters
        {
            get { return monsters; }
        }

        public Mission SelectMission(uint mission_index)
        {
            SelectedMission = mission_index;
            return Missions[(int)SelectedMission];
        }


        /// <summary>
        /// Simulates the selected mission
        /// </summary>
        public void StartMission(Mission mission)
        {
            // Implement the logic to start the mission
            foreach (var member in Team)
            {
                foreach (var monster in mission.Monsters)
                {
                    member.Attack(monster);
                }
            }

            if (mission.IsCompleted())
            {
                mission.GrantRewards();
            }
        }

        /// <summary>
        /// Add a character to the player's team
        /// </summary>
        public void AddTeamMember(uint characterIndex)
        {
            var member = Characters[characterIndex];

            if (Team.Contains(member))
            {
                MessageBox.Show("Le personnage est déjà dans l'équipe", "Information sur l'équipe", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            } 
            
            for (var i = 0; i < Team.Length; i++)
            {
                if (Team[i] != null) continue;
                Team[i] = member;
                return;
            }
        }

        /// <summary>
        /// Removes a character from the team
        /// </summary>
        public void RemoveTeamMember(uint slot)
        {
            Team[slot] = null;
            Trace.WriteLine(Team);
        }

        /// <summary>
        /// Add an item to the player's inventory
        /// </summary>
        public void StoreItem(Item item)
        {
            Inventory = Inventory.Append(item).ToArray();
        }

        /// <summary>
        /// Add a character to the list of selectable characters
        /// </summary>
        public void AddCharacter(Character character)
        {
            characters = characters.Append(character).ToArray();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Load missions
            var jsonString = File.ReadAllText("../../../assets/missions.json");
            Missions = JsonSerializer.Deserialize<List<model.Mission>>(jsonString) ?? [];

            var w = new MainWindow();

            // Add a default character
            var defaultCharacter = new Character("Daniel", Element.Electrik, WeaponClass.Melee);
            defaultCharacter.Equip(new Item()
            {
                Name = "Le bâton de Daniel",
                Description = "Une simple branch d'arbre que Daniel à trouvé au sol en forêt",
                Type = ItemType.Weapon,
                Range = WeaponClass.Melee,
                Value = 3
            });
            AddCharacter(defaultCharacter);
            
            AddTeamMember(0);

            w.Show();
        }
    }
}