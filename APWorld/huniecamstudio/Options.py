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
    """number of archipelago items in the shop Note if there is not enough locations for items it will add shop locations to satisfy the locations needed"""
    display_name = "shop items"
    range_start = 0
    range_end = 200
    default = 0

class start_slots(Range):
    """start the game wth X girl slots open"""
    display_name = "starting slots"
    range_start = 1
    range_end = 8
    default = 2

class item_slots(Range):
    """start the game wth X girl item slots open"""
    display_name = "starting item slots"
    range_start = 0
    range_end = 3
    default = 1

class clean_start(Toggle):
    """makes the starting girl not smoke or drink"""
    display_name = "clean start"
    default = True

class filler_item(Range):
    """how the filler item is handled by making them all either:
    1:nothing items,
    2:random non progression items"""
    display_name = "filler item"
    range_start = 1
    range_end = 2
    default = 2

class filler_game(Choice):
    """limits the amount of locations that can have a progression item in them
    none: no limit on what locations can have useful/progression
    shop: excludes shop locations from having useful/progression items
    full: no location in this slot will have useful/progression items in it(NOTE may cause generation to fail depending on other game slots)
    """
    display_name = "filler game"
    option_none = 0
    option_shop = 1
    option_full = 2
    default = 0

class goal(Toggle):
    """true will set the goal to be getting trophy's
    false will set the goal to max out every girls style/talent"""
    display_name = "force goal"
    default = True

class min_trophy(Choice):
    """sets the minimum trophy that will complete goal"""
    display_name = "min trophy"
    option_bronze= 0
    option_silver= 1
    option_gold= 2
    option_platinum= 3
    option_diamond= 4
    default = 0

class force_goal(Toggle):
    """forces the goal to be achieved in 21 ingame days from starting a new save (NOTE: this still means you need to wait till the trophy check on the 22 day)"""
    display_name = "force goal"
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
