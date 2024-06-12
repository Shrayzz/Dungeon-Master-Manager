const ItemType = {
  Consumable: 0,
  Weapon: 1,
};

const Intent = {
  View: 0,
  Select: 1,
};

const Element = {
  Electrik: 0,
  Ice: 1,
  Earth: 2,
};

const WeaponClass = {
  Range: 0,
  Melee: 1,
  Magic: 2,
};

class Item {
  #name;
  #desc;
  #type;
  #range;
  #value;

  constructor(name, description, type, range, value) {
    this.name = name;
    this.desc = description;
    this.type = type;
    this.range = range;
    this.value = value;
  }
}

class Monster {
  #weekness;
  #resistance;
  #weekness_percent;
  #resistance_percent;
  health;

  constructor(health, weekness, resistance, weekness_percent, resistance_percent) {
    this.health = health;
    this.#weekness = weekness;
    this.#resistance = resistance;
    this.#weekness_percent = weekness_percent;
    this.#resistance_percent = resistance_percent;
  }

  get weekness() {
    return this.#weekness;
  }

  get weekness_percent() {
    return this.#weekness_percent;
  }

  get resistance() {
    return this.#resistance;
  }

  get resitance_percent() {
    return this.#resistance_percent;
  }
}

class Mission {
  #monsters;
  #reward_gold;
  #reward_items;
  #reward_characters;

  constructor(monsters, reward_gold, reward_items, reward_characters) {
    this.#monsters = monsters;
    this.#reward_gold = reward_gold;
    this.#reward_items = reward_items;
    this.#reward_characters = reward_characters;
  }
}

class Character {
  #element;
  #acceptedWeapon;
  #name;
  #inventory = [];
  #health;
  #weapon = null;

  constructor(name, element, weaponclass, health) {
    this.#name = name;
    this.#element = element;
    this.#acceptedWeapon = weaponclass;
    this.#health = health;
  }

  damage(amount) {
    this.#health -= amount;
  }

  heal(amount) {
    this.#health += amount;
  }

  equip(item) {
    if (item.type == ItemType.Weapon) {
      if (this.#weapon != null) {
        return;
      } else {
        this.#weapon = item;
      }
    }

    this.#inventory.push(item);
  }

  unequip(item) {
    index = this.#inventory.find(item);
    if (index == -1) return;
    this.#inventory = this.#inventory.splice(index, index);
  }

  attack(monster) {
    if (this.#weapon == null) return;

    monster.health *=
      (monster.weekness == this.#element) * monster.weekness_percent +
      1 +
      (monster.resistance == this.#element) * monster.resitance_percent * this.#weapon.value;
  }
}

class GameLogic {
  static #instance = null;

  #intent = Intent.View;
  #bank = 500;
  #selected_mission = 0;
  #characters = [];
  #inventory = [];
  #shops = [];
  #team = [];
  #missions = [];
  #monsters = [];

  constructor() {
    // Default character and weapon
    let default_character = new Character("The Konquerror", Element.Electrik, WeaponClass.Melee, 20);
    default_character.equip(
      new Item("Wooden sword", "A swoord made out of Sakura wood", ItemType.Weapon, WeaponClass.Melee, 2),
    );
    this.#characters.push(default_character);
  }

  static get_instance() {
    if (this.#instance == null) {
      this.#instance = new GameLogic();
    }

    return this.#instance;
  }

  StartMission(mission) {
    // TODO: Mission logic
  }

  AddTeamMember(character) {
    if (this.#team.length < 4) {
      this.#team.push(character);
    }
  }

  RemoveTeamMember(character) {
    let cIndex = this.#team.findIndex(character);

    if (cIndex == -1) return;

    this.#team.splice(cIndex, 1);
  }

  StoreItem(item) {
    this.#inventory.push(item);
  }
  AddCharacter(character) {
    this.#characters.push(character);
  }
}

module.exports = { GameLogic, Mission, ItemType, Intent, Element, WeaponClass, Item, Monster, Mission, Character };
