from BaseClasses import Item


class HCSItem(Item):
    game = "Hunie Cam Studio"

architems = {
    "nothing": 1,
    "victory": 2,
}

girls_start = 2
girls = {
    "Unlock Girl (Jessie)": girls_start + 1,
    "Unlock Girl (Nikki)": girls_start + 2,
    "Unlock Girl (Lillian)": girls_start + 3,
    "Unlock Girl (Nora)": girls_start + 4,
    "Unlock Girl (Lola)": girls_start + 5,
    "Unlock Girl (Kyanna)": girls_start + 6,
    "Unlock Girl (Candace)": girls_start + 7,
    "Unlock Girl (Renee)": girls_start + 8,
    "Unlock Girl (Tiffany)": girls_start + 9,
    "Unlock Girl (Brooke)": girls_start + 10,
    "Unlock Girl (Zoey)": girls_start + 11,
    "Unlock Girl (Beli)": girls_start + 12,
    "Unlock Girl (Audrey)": girls_start + 13,
    "Unlock Girl (Sarah)": girls_start + 14,
    "Unlock Girl (Lailani)": girls_start + 15,
    "Unlock Girl (Aiko)": girls_start + 16,
    "Unlock Girl (Nadia)": girls_start + 17,
    "Unlock Girl (Marlena)": girls_start + 18,
}

upgrade_item_start = girls_start + 18
upgrade_item = {
    "Progressive Staffing Upgrade": upgrade_item_start + 1,
    "Progressive Accounting Upgrade": upgrade_item_start + 2,
    "Progressive Capacity Upgrade": upgrade_item_start + 3,
    "Progressive Inventory Upgrade": upgrade_item_start + 4,
    "Progressive Community Upgrade": upgrade_item_start + 5,
    "Progressive Automation Upgrade": upgrade_item_start + 6,
    "Progressive Training Upgrade": upgrade_item_start + 7,
    "Progressive Productivity Upgrade": upgrade_item_start + 8,
    "Progressive Aesthetics Upgrade": upgrade_item_start + 9,
    "Progressive Web Servers Upgrade": upgrade_item_start + 10,
    "Progressive Hardware Upgrade": upgrade_item_start + 11,
    "Progressive Advertising Upgrade": upgrade_item_start + 12,
}
upgrade_item_count = {
    "Progressive Staffing Upgrade": 7,
    "Progressive Accounting Upgrade": 15,
    "Progressive Capacity Upgrade": 6,
    "Progressive Inventory Upgrade": 3,
    "Progressive Community Upgrade": 8,
    "Progressive Automation Upgrade": 7,
    "Progressive Training Upgrade": 4,
    "Progressive Productivity Upgrade": 4,
    "Progressive Aesthetics Upgrade": 4,
    "Progressive Web Servers Upgrade": 4,
    "Progressive Hardware Upgrade": 4,
    "Progressive Advertising Upgrade": 5,
    #71
}

items_start = upgrade_item_start + 12
items = {
    "Vibrator": items_start + 1,
    "Butt Plug": items_start + 2,
    "Ball Gag": items_start + 3,
    "Cat Ears": items_start + 4,
    "Water Bottle": items_start + 5,
    "Chocolate Cake": items_start + 6,
    "Condom": items_start + 7,
    "Antibiotics": items_start + 8,
    "Steroids": items_start + 9,
    "Nicotine Patch": items_start + 10,
    "Wine Box": items_start + 11,
    "Shopping Basket": items_start + 12,
    "Subscribe Pillow": items_start + 13,
    "Weed": items_start + 14,
    "Coke": items_start + 15,
    "Stripper Heels": items_start + 16,
    "Fashion Magazine": items_start + 17,
    "Piggy Bank": items_start + 18,
}
item_list = [
"Vibrator",
"Butt Plug",
"Ball Gag",
"Cat Ears",
"Water Bottle",
"Chocolate Cake",
"Condom",
"Antibiotics",
"Steroids",
"Nicotine Patch",
"Wine Box",
"Shopping Basket",
"Subscribe Pillow",
"Weed",
"Coke",
"Stripper Heels",
"Fashion Magazine",
"Piggy Bank",
]

item_table = {
    **architems,
    **girls,
    **upgrade_item,
    **items,
}
