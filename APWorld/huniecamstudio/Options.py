from dataclasses import dataclass

from Options import PerGameCommonOptions, Range, OptionSet, Toggle, Choice


class enabled_girls(OptionSet):
    """girls enabled to be accessed"""
    display_name = "enabled girls"
    valid_keys = [
        "Jessie",
        "Nikki",
        "Lillian",
        "Nora",
        "Lola",
        "Kyanna",
        "Candace",
        "Renee",
        "Tiffany",
        "Brooke",
        "Zoey",
        "Beli",
        "Audrey",
        "Sarah",
        "Lailani",
        "Aiko",
        "Nadia",
        "Marlena",
    ]
    default = valid_keys.copy()

class shop_items(Range):
    """
    Number of archipelago items in the shop.
    
    *If there is not enough locations for items, it will add shop locations to satisfy the locations needed.*
    """
    display_name = "Shop items"
    range_start = 0
    range_end = 200
    default = 0

class start_slots(Range):
    """
    Start the game with X girl slots open.
    """
    display_name = "Starting slots"
    range_start = 1
    range_end = 8
    default = 2

class item_slots(Range):
    """
    Start the game with X girl item slots open.
    """
    display_name = "Starting item slots"
    range_start = 0
    range_end = 3
    default = 1

class clean_start(Toggle):
    """
    Makes the starting girl not smoking or drinking.
    """
    display_name = "Clean start"
    default = True

class filler_item(Range):
    """
    How the filler items are handled
    
    1 - **Nothing items** *Fillers wont do anything*
    2 - **Non progression items** *Fillers will be non-progression items*
    """
    display_name = "Filler items"
    range_start = 1
    range_end = 2
    default = 2

class filler_game(Choice):
    """
    Limits the amount of locations that can have a progression item in.
    
    - **None** *No limit on what locations can have useful/progression.*
    - **Shop** *Excludes shop locations from having useful/progression items.*
    - **Full** *No location in this slot will have useful/progression items in it.*
        *(It might cause generation to fail depending on other game slots.)*
    """
    display_name = "Filler game"
    option_none = 0
    option_shop = 1
    option_full = 2
    default = 0

class goal(Toggle):
    """
    - **True** *Goal is to obtain a minimum trophy.*
    - **False** *Goal is to max out every girls style and talent.*
    """
    display_name = "Goal"
    default = True

class min_trophy(Choice):
    """
    Sets the minimum trophy that will complete the trophy goal.
    """
    display_name = "Min trophy"
    option_bronze= 0
    option_silver= 1
    option_gold= 2
    option_platinum= 3
    option_diamond= 4
    default = 0

class force_goal(Toggle):
    """
    Forces the goal to be achieved in 21 ingame days from starting a new save.
    *This still means you need to wait till the trophy check on the 22 day.*"""
    display_name = "Force goal"
    default = False

@dataclass
class HCSOptions(PerGameCommonOptions):
    enabled_girls:enabled_girls
    shop_items:shop_items
    filler_item: filler_item
    start_slots: start_slots
    item_slots: item_slots
    clean_start: clean_start
    filler_game:filler_game
    goal:goal
    min_trophy:min_trophy
    force_goal:force_goal
