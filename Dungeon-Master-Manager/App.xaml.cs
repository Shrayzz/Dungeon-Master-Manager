using System.Collections;
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
    public partial class App
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
        public uint SelectedMission { get; private set; } = 0;

        /// <summary>
        /// The index of something from an inventory like storage that goes with the selection Intent
        /// </summary>
        public int SelectedThing { get; set; } = 0;

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
            get => missions;
            private set => missions = value;
        }


        public Mission SelectMission(uint missionIndex)
        {
            SelectedMission = missionIndex;
            return Missions[(int)SelectedMission];
        }


        /// <summary>
        /// Simulates the selected mission
        /// </summary>
        public void StartMission()
        {
            Mission mission = Missions[(int)SelectedMission];

            if (!(mission.Monsters.Any(monster => monster.Health > 0)))
            {
                MessageBox.Show("Vous avez déjà fini cette mission !",
                    "Simulation",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            Character[] team = Team.Where(member => member != null).ToArray();
            var missionLog = "";

            if (team.Length == 0)
            {
                MessageBox.Show("Votre équipe est vide ! Vous ne pouvez pas partir à l'aventure tout seul !",
                    "Simulation",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Simulate the mission
            missionLog += "----- Début de la mission\n";
            while (mission.Monsters.Any(monster => monster.Health > 0) && team.Any(character => character.Health > 0))
            {
                missionLog += "---- Votre équipe attaque\n";
                foreach (Character character in team)
                {
                    if (character.Health == 0)
                    {
                        continue;
                    }

                    missionLog += $"--- Au tour de {character.Name}\n";
                    Item? healingItem = character.Inventory.FirstOrDefault(item => item.Type == ItemType.Consumable);
                    if (character.Health < 10 && healingItem != null)
                    {
                        missionLog +=
                            $"--- {character.Name}' se soigne de {healingItem.Value} HP! Il à maintenant {character.Health} PV !\n";
                        character.Heal((uint)healingItem.Value);
                        character.Inventory.Remove(healingItem);
                        continue; // Next character
                    }

                    missionLog += $"--- {character.Name} attaque !\n";
                    Monster target = mission.Monsters.FirstOrDefault(monster => monster.Health > 0);
                    if (target != null)
                    {
                        character.Attack(target);
                        missionLog += $"--- Le monstre est à {target.Health} PV!\n";
                    }
                }

                missionLog += "---- Les monstres attaquent!\n";
                foreach (Monster monster in mission.Monsters)
                {
                    if (monster.Health <= 0) continue;


                    Character target = team.FirstOrDefault(character => character.Health > 0);
                    if (target != null)
                    {
                        // Todo: Add damage to monsters
                        missionLog += $"---- {target.Name} est maintenant à {target.Health} PV!\n";
                        target.Damage(5);
                    }
                }
            }


            bool missionSuccessful = mission.Monsters.All(monster => monster.Health <= 0);
            if (missionSuccessful)
            {
                missionLog += $"---- Tout les monstres sont mort !\n";
                MessageBox.Show("Tout les monstres sont morts ! Mission réussie !");
                mission.MissionDone = true;
                mission.GrantRewards();
            }
            else
            {
                MessageBox.Show("Tout vos membres sont morts, vous avez perdu la mission !", "Mission échouée",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }

            MessageBox.Show(missionLog);
        }


        /// <summary>
        /// Add a character to the player's team
        /// </summary>
        public void AddTeamMember(uint characterIndex)
        {
            var member = Characters[characterIndex];

            if (Team.Contains(member))
            {
                MessageBox.Show("Le personnage est déjà dans l'équipe", "Information sur l'équipe", MessageBoxButton.OK,
                    MessageBoxImage.Information);
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
            var defaultWeapon = new Item()
            {
                Name = "Le bâton de Daniel",
                Description = "Une simple branch d'arbre que Daniel à trouvé au sol en forêt",
                Type = ItemType.Weapon,
                Range = WeaponClass.Melee,
                Value = 7
            };
            StoreItem(defaultWeapon);
            StoreItem(new Item()
            {
                Name = "Health potion",
                Description = "A simple health potion",
                Type = ItemType.Consumable,
                Value = 20
            });


            defaultCharacter.Equip(Inventory[0]);

            AddCharacter(defaultCharacter);

            AddTeamMember(0);

            w.Show();
        }
    }
}