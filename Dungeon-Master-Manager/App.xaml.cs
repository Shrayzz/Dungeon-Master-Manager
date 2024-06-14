using System.Diagnostics;
using System.Linq; // for the Append method
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
        /// The character the player has selected for a mission
        /// </summary>
        private Character[] team = [];

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

        public Character[] Team
        {
            get { return team; }
        }

        public List<Mission> Missions
        {
            get { return missions; }
            set { missions = value; }
        }

        public Monster[] Monsters
        {
            get { return monsters; }
        }

        public void SelectMission(uint mission_index)
        {
            SelectedMission = mission_index;
            Trace.WriteLine(Missions[(int)SelectedMission].Name);
        }
        
        public void SelectMissionEvent(Object? caller, EventArgs e)
        {
            uint mission_id = Convert.ToUInt32(((Button)caller).Tag);
            SelectMission(mission_id);
        }
        
        /// <summary>
        /// Simulates the selected mission
        /// </summary>
        public void StartMission(Mission mission)
        {
            // Implement the logic to start the mission
            foreach (var member in team)
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
        public void AddTeamMember(Character member)
        {
            team = team.Append(member).ToArray();
        }

        /// <summary>
        /// Removes a character from the team
        /// </summary>
        public void RemoveTeamMember(Character member)
        {
            team = team.Where(c => c != member).ToArray();
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

            MainWindow w = new MainWindow();

            Character defaultCharacter = new Character("Daniel", Element.Electrik, WeaponClass.Range);
            AddCharacter(defaultCharacter);
            AddTeamMember(Characters[0]);

            w.Show();
        }
    }
}
