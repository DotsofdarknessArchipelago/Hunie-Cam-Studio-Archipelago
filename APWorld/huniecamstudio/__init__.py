from BaseClasses import ItemClassification, Region, LocationProgressType
from worlds.AutoWorld import World
from worlds.huniecamstudio.Items import item_table, HCSItem, girls_start, upgrade_item_start, items_start, girls, upgrade_item, upgrade_item_count, item_list
from worlds.huniecamstudio.Locations import location_table, Jessie_loc_start, Nikki_loc_start, Lillian_loc_start, Nora_loc_start, Lola_loc_start, Kyanna_loc_start, Candace_loc_start, Renee_loc_start, Tiffany_loc_start, Brooke_loc_start, Zoey_loc_start, Beli_loc_start, Audrey_loc_start, Sarah_loc_start, Lailani_loc_start, Aiko_loc_start, Nadia_loc_start, Marlena_loc_start, trophy_loc, HCSLocation, Jessie_loc, Nikki_loc, Lillian_loc, Nora_loc, Lola_loc, Kyanna_loc, Candace_loc, Renee_loc, Tiffany_loc, Brooke_loc, Zoey_loc, Beli_loc, Audrey_loc, Sarah_loc, Lailani_loc, Aiko_loc, Nadia_loc, Marlena_loc, shop_loc, shop_loc_start, trophy_loc_start, arch_loc
from worlds.huniecamstudio.Options import HCSOptions


class HuniePop(World):
    game = "Hunie Cam Studio"
    worldversion = {
        "major": 0,
        "minor": 3,
        "build": 0
    }

    item_name_to_id = item_table
    location_name_to_id = location_table

    options_dataclass = HCSOptions
    options: HCSOptions

    startinggirl = ""
    girldata = {}

    totalloc = 0
    totalite = 0
    filleritems = 0

    def generate_early(self):
        self.startinggirl = ""
        self.totalloc = 0
        self.totalite = 0
        self.filleritems = 0

        self.girldata = {}
        for i in ["Jessie","Nikki","Lillian","Nora","Lola","Kyanna","Candace","Renee","Tiffany","Brooke","Zoey","Beli","Audrey","Sarah","Lailani","Aiko","Nadia","Marlena",]:
            smoke = self.random.choice(range(4))
            drink = self.random.choice(range(4))
            fr = [1,2,3,4,5,6,7,8,9,10,11,12,14,15,17,21,23,24, 25]
            f1 = self.random.choice(fr)
            fr.remove(f1)
            f2 = self.random.choice(fr)
            self.girldata[i] = [smoke,drink,f1,f2]



        self.startinggirl = self.random.choice(list(self.options.enabled_girls.value))

        if self.options.clean_start.value:
            self.girldata[self.startinggirl][0] = 0
            self.girldata[self.startinggirl][1] = 0

        self.totalloc += len(self.options.enabled_girls.value) * 8
        self.totalloc += self.options.shop_items.value
        if self.options.goal.value:
            self.totalloc += self.options.min_trophy.value
        else:
            self.totalloc += 5


        self.totalite += len(self.options.enabled_girls.value) - 1
        self.totalite += (71 - (self.options.start_slots.value-1) - self.options.item_slots.value)


        if self.totalloc > self.totalite:
            self.filleritems = self.totalloc - self.totalite
        else:
            self.options.shop_items.value = self.totalite - len(self.options.enabled_girls.value) * 8
        hi = 0


    def create_regions(self):
        hub_region = Region("Menu", self.player, self.multiworld)
        self.multiworld.regions.append(hub_region)

        trophy = Region("Trophy", self.player, self.multiworld)
        trophy.add_locations(trophy_loc, HCSLocation)
        if not self.options.goal.value:
            trophy.add_locations(arch_loc, HCSLocation)
        hub_region.connect(trophy)

        if self.options.shop_items > 0:
            shop_region = Region("shop", self.player, self.multiworld)
            for i in range(self.options.shop_items.value):
                shop_region.add_locations({f"shop_location: {i+1}" : shop_loc_start+i+1}, HCSLocation)
            hub_region.connect(shop_region, "hub-shop")

        if "Jessie" in self.options.enabled_girls.value:
            Jessie = Region("Jessie", self.player, self.multiworld)
            Jessie.add_locations(Jessie_loc, HCSLocation)
            hub_region.connect(Jessie, "hub->Jessie", lambda state: state.has("Unlock Girl (Jessie)", self.player))

        if "Nikki" in self.options.enabled_girls.value:
            Nikki = Region("Nikki", self.player, self.multiworld)
            Nikki.add_locations(Nikki_loc, HCSLocation)
            hub_region.connect(Nikki, "hub->Nikki", lambda state: state.has("Unlock Girl (Nikki)", self.player))

        if "Lillian" in self.options.enabled_girls.value:
            Lillian = Region("Lillian", self.player, self.multiworld)
            Lillian.add_locations(Lillian_loc, HCSLocation)
            hub_region.connect(Lillian, "hub->Lillian", lambda state: state.has("Unlock Girl (Lillian)", self.player))

        if "Nora" in self.options.enabled_girls.value:
            Nora = Region("Nora", self.player, self.multiworld)
            Nora.add_locations(Nora_loc, HCSLocation)
            hub_region.connect(Nora, "hub->Nora", lambda state: state.has("Unlock Girl (Nora)", self.player))

        if "Lola" in self.options.enabled_girls.value:
            Lola = Region("Lola", self.player, self.multiworld)
            Lola.add_locations(Lola_loc, HCSLocation)
            hub_region.connect(Lola, "hub->Lola", lambda state: state.has("Unlock Girl (Lola)", self.player))

        if "Kyanna" in self.options.enabled_girls.value:
            Kyanna = Region("Kyanna", self.player, self.multiworld)
            Kyanna.add_locations(Kyanna_loc, HCSLocation)
            hub_region.connect(Kyanna, "hub->Kyanna", lambda state: state.has("Unlock Girl (Kyanna)", self.player))

        if "Candace" in self.options.enabled_girls.value:
            Candace = Region("Candace", self.player, self.multiworld)
            Candace.add_locations(Candace_loc, HCSLocation)
            hub_region.connect(Candace, "hub->Candace", lambda state: state.has("Unlock Girl (Candace)", self.player))

        if "Renee" in self.options.enabled_girls.value:
            Renee = Region("Renee", self.player, self.multiworld)
            Renee.add_locations(Renee_loc, HCSLocation)
            hub_region.connect(Renee, "hub->Renee", lambda state: state.has("Unlock Girl (Renee)", self.player))

        if "Tiffany" in self.options.enabled_girls.value:
            Tiffany = Region("Tiffany", self.player, self.multiworld)
            Tiffany.add_locations(Tiffany_loc, HCSLocation)
            hub_region.connect(Tiffany, "hub->Tiffany", lambda state: state.has("Unlock Girl (Tiffany)", self.player))

        if "Brooke" in self.options.enabled_girls.value:
            Brooke = Region("Brooke", self.player, self.multiworld)
            Brooke.add_locations(Brooke_loc, HCSLocation)
            hub_region.connect(Brooke, "hub->Brooke", lambda state: state.has("Unlock Girl (Brooke)", self.player))

        if "Zoey" in self.options.enabled_girls.value:
            Zoey = Region("Zoey", self.player, self.multiworld)
            Zoey.add_locations(Zoey_loc, HCSLocation)
            hub_region.connect(Zoey, "hub->Zoey", lambda state: state.has("Unlock Girl (Zoey)", self.player))

        if "Beli" in self.options.enabled_girls.value:
            Beli = Region("Beli", self.player, self.multiworld)
            Beli.add_locations(Beli_loc, HCSLocation)
            hub_region.connect(Beli, "hub->Beli", lambda state: state.has("Unlock Girl (Beli)", self.player))

        if "Audrey" in self.options.enabled_girls.value:
            Audrey = Region("Audrey", self.player, self.multiworld)
            Audrey.add_locations(Audrey_loc, HCSLocation)
            hub_region.connect(Audrey, "hub->Audrey", lambda state: state.has("Unlock Girl (Audrey)", self.player))

        if "Sarah" in self.options.enabled_girls.value:
            Sarah = Region("Sarah", self.player, self.multiworld)
            Sarah.add_locations(Sarah_loc, HCSLocation)
            hub_region.connect(Sarah, "hub->Sarah", lambda state: state.has("Unlock Girl (Sarah)", self.player))

        if "Lailani" in self.options.enabled_girls.value:
            Lailani = Region("Lailani", self.player, self.multiworld)
            Lailani.add_locations(Lailani_loc, HCSLocation)
            hub_region.connect(Lailani, "hub->Lailani", lambda state: state.has("Unlock Girl (Lailani)", self.player))

        if "Aiko" in self.options.enabled_girls.value:
            Aiko = Region("Aiko", self.player, self.multiworld)
            Aiko.add_locations(Aiko_loc, HCSLocation)
            hub_region.connect(Aiko, "hub->Aiko", lambda state: state.has("Unlock Girl (Aiko)", self.player))

        if "Nadia" in self.options.enabled_girls.value:
            Nadia = Region("Nadia", self.player, self.multiworld)
            Nadia.add_locations(Nadia_loc, HCSLocation)
            hub_region.connect(Nadia, "hub->Nadia", lambda state: state.has("Unlock Girl (Nadia)", self.player))

        if "Marlena" in self.options.enabled_girls.value:
            Marlena = Region("Marlena", self.player, self.multiworld)
            Marlena.add_locations(Marlena_loc, HCSLocation)
            hub_region.connect(Marlena, "hub->Marlena", lambda state: state.has("Unlock Girl (Marlena)", self.player))

    def create_item(self, name: str) -> HCSItem:
        if name in girls or name == "victory":
            return HCSItem(name, ItemClassification.progression, self.item_name_to_id[name], self.player)
        if name in upgrade_item:
            return HCSItem(name, ItemClassification.useful, self.item_name_to_id[name], self.player)

        return HCSItem(name, ItemClassification.filler, self.item_name_to_id[name], self.player)

    def create_items(self):

        for x in range(self.options.start_slots.value-1):
            self.multiworld.push_precollected(self.create_item("Progressive Staffing Upgrade"))
        for x in range(self.options.item_slots.value):
            self.multiworld.push_precollected(self.create_item("Progressive Inventory Upgrade"))

        for k in upgrade_item_count:
            if k=="Progressive Staffing Upgrade":
                for i in range(upgrade_item_count["Progressive Staffing Upgrade"]-(self.options.start_slots.value-1)):
                    self.multiworld.itempool.append(self.create_item(k))
            elif k=="Progressive Inventory Upgrade":
                for i in range(upgrade_item_count["Progressive Inventory Upgrade"]-self.options.item_slots.value):
                    self.multiworld.itempool.append(self.create_item(k))
            else:
                for i in range(upgrade_item_count[k]):
                    self.multiworld.itempool.append(self.create_item(k))

        if self.filleritems > 0:
            for i in range(self.filleritems):
                if self.options.filler_item.value == 1:
                    self.multiworld.itempool.append(self.create_item("nothing"))
                else:
                    self.multiworld.itempool.append(self.create_item(self.random.choice(item_list)))

        if "Jessie" in self.options.enabled_girls.value:
            if self.startinggirl == "Jessie":
                self.multiworld.push_precollected((self.create_item("Unlock Girl (Jessie)")))
            else:
                self.multiworld.itempool.append((self.create_item("Unlock Girl (Jessie)")))
        if "Nikki" in self.options.enabled_girls.value:
            if self.startinggirl == "Nikki":
                self.multiworld.push_precollected((self.create_item("Unlock Girl (Nikki)")))
            else:
                self.multiworld.itempool.append((self.create_item("Unlock Girl (Nikki)")))
        if "Lillian" in self.options.enabled_girls.value:
            if self.startinggirl == "Lillian":
                self.multiworld.push_precollected((self.create_item("Unlock Girl (Lillian)")))
            else:
                self.multiworld.itempool.append((self.create_item("Unlock Girl (Lillian)")))
        if "Nora" in self.options.enabled_girls.value:
            if self.startinggirl == "Nora":
                self.multiworld.push_precollected((self.create_item("Unlock Girl (Nora)")))
            else:
                self.multiworld.itempool.append((self.create_item("Unlock Girl (Nora)")))
        if "Lola" in self.options.enabled_girls.value:
            if self.startinggirl == "Lola":
                self.multiworld.push_precollected((self.create_item("Unlock Girl (Lola)")))
            else:
                self.multiworld.itempool.append((self.create_item("Unlock Girl (Lola)")))
        if "Kyanna" in self.options.enabled_girls.value:
            if self.startinggirl == "Kyanna":
                self.multiworld.push_precollected((self.create_item("Unlock Girl (Kyanna)")))
            else:
                self.multiworld.itempool.append((self.create_item("Unlock Girl (Kyanna)")))
        if "Candace" in self.options.enabled_girls.value:
            if self.startinggirl == "Candace":
                self.multiworld.push_precollected((self.create_item("Unlock Girl (Candace)")))
            else:
                self.multiworld.itempool.append((self.create_item("Unlock Girl (Candace)")))
        if "Renee" in self.options.enabled_girls.value:
            if self.startinggirl == "Renee":
                self.multiworld.push_precollected((self.create_item("Unlock Girl (Renee)")))
            else:
                self.multiworld.itempool.append((self.create_item("Unlock Girl (Renee)")))
        if "Tiffany" in self.options.enabled_girls.value:
            if self.startinggirl == "Tiffany":
                self.multiworld.push_precollected((self.create_item("Unlock Girl (Tiffany)")))
            else:
                self.multiworld.itempool.append((self.create_item("Unlock Girl (Tiffany)")))
        if "Brooke" in self.options.enabled_girls.value:
            if self.startinggirl == "Brooke":
                self.multiworld.push_precollected((self.create_item("Unlock Girl (Brooke)")))
            else:
                self.multiworld.itempool.append((self.create_item("Unlock Girl (Brooke)")))
        if "Zoey" in self.options.enabled_girls.value:
            if self.startinggirl == "Zoey":
                self.multiworld.push_precollected((self.create_item("Unlock Girl (Zoey)")))
            else:
                self.multiworld.itempool.append((self.create_item("Unlock Girl (Zoey)")))
        if "Beli" in self.options.enabled_girls.value:
            if self.startinggirl == "Beli":
                self.multiworld.push_precollected((self.create_item("Unlock Girl (Beli)")))
            else:
                self.multiworld.itempool.append((self.create_item("Unlock Girl (Beli)")))
        if "Audrey" in self.options.enabled_girls.value:
            if self.startinggirl == "Audrey":
                self.multiworld.push_precollected((self.create_item("Unlock Girl (Audrey)")))
            else:
                self.multiworld.itempool.append((self.create_item("Unlock Girl (Audrey)")))
        if "Sarah" in self.options.enabled_girls.value:
            if self.startinggirl == "Sarah":
                self.multiworld.push_precollected((self.create_item("Unlock Girl (Sarah)")))
            else:
                self.multiworld.itempool.append((self.create_item("Unlock Girl (Sarah)")))
        if "Lailani" in self.options.enabled_girls.value:
            if self.startinggirl == "Lailani":
                self.multiworld.push_precollected((self.create_item("Unlock Girl (Lailani)")))
            else:
                self.multiworld.itempool.append((self.create_item("Unlock Girl (Lailani)")))
        if "Aiko" in self.options.enabled_girls.value:
            if self.startinggirl == "Aiko":
                self.multiworld.push_precollected((self.create_item("Unlock Girl (Aiko)")))
            else:
                self.multiworld.itempool.append((self.create_item("Unlock Girl (Aiko)")))
        if "Nadia" in self.options.enabled_girls.value:
            if self.startinggirl == "Nadia":
                self.multiworld.push_precollected((self.create_item("Unlock Girl (Nadia)")))
            else:
                self.multiworld.itempool.append((self.create_item("Unlock Girl (Nadia)")))
        if "Marlena" in self.options.enabled_girls.value:
            if self.startinggirl == "Marlena":
                self.multiworld.push_precollected((self.create_item("Unlock Girl (Marlena)")))
            else:
                self.multiworld.itempool.append((self.create_item("Unlock Girl (Marlena)")))

    def set_rules(self):
        if self.options.goal.value:
            if self.options.min_trophy.value <= 0: self.multiworld.get_location("Obtain Bronze Trophy", self.player).place_locked_item(self.create_item("victory"))
            if self.options.min_trophy.value <= 1: self.multiworld.get_location("Obtain Silver Trophy", self.player).place_locked_item(self.create_item("victory"))
            if self.options.min_trophy.value <= 2: self.multiworld.get_location("Obtain Gold Trophy", self.player).place_locked_item(self.create_item("victory"))
            if self.options.min_trophy.value <= 3: self.multiworld.get_location("Obtain Platinum Trophy", self.player).place_locked_item(self.create_item("victory"))
            if self.options.min_trophy.value <= 4: self.multiworld.get_location("Obtain Diamond Trophy", self.player).place_locked_item(self.create_item("victory"))
        else:
            self.multiworld.get_location("Fully Upgrade All Girls", self.player).place_locked_item(self.create_item("victory"))

        if self.options.filler_game.value >= 1:
            if self.options.shop_items.value > 0:
                for i in range(self.options.shop_items.value):
                    self.multiworld.get_location(f"shop_location: {i + 1}", self.player).progress_type = LocationProgressType.EXCLUDED

        if self.options.filler_game.value >= 2:
            if self.options.goal.value:
                if self.options.min_trophy.value >= 1: self.multiworld.get_location("Obtain Bronze Trophy", self.player).progress_type = LocationProgressType.EXCLUDED
                if self.options.min_trophy.value >= 2: self.multiworld.get_location("Obtain Silver Trophy", self.player).progress_type = LocationProgressType.EXCLUDED
                if self.options.min_trophy.value >= 3: self.multiworld.get_location("Obtain Gold Trophy", self.player).progress_type = LocationProgressType.EXCLUDED
                if self.options.min_trophy.value >= 4: self.multiworld.get_location("Obtain Platinum Trophy", self.player).progress_type = LocationProgressType.EXCLUDED
                if self.options.min_trophy.value >= 5: self.multiworld.get_location("Obtain Diamond Trophy", self.player).progress_type = LocationProgressType.EXCLUDED
            else:
                self.multiworld.get_location("Obtain Bronze Trophy", self.player).progress_type = LocationProgressType.EXCLUDED
                self.multiworld.get_location("Obtain Silver Trophy", self.player).progress_type = LocationProgressType.EXCLUDED
                self.multiworld.get_location("Obtain Gold Trophy", self.player).progress_type = LocationProgressType.EXCLUDED
                self.multiworld.get_location("Obtain Platinum Trophy", self.player).progress_type = LocationProgressType.EXCLUDED
                self.multiworld.get_location("Obtain Diamond Trophy", self.player).progress_type = LocationProgressType.EXCLUDED

            if "Jessie" in self.options.enabled_girls.value:
                for l in Jessie_loc:
                    self.multiworld.get_location(l, self.player).progress_type = LocationProgressType.EXCLUDED

            if "Nikki" in self.options.enabled_girls.value:
                for l in Nikki_loc:
                    self.multiworld.get_location(l, self.player).progress_type = LocationProgressType.EXCLUDED

            if "Lillian" in self.options.enabled_girls.value:
                for l in Lillian_loc:
                    self.multiworld.get_location(l, self.player).progress_type = LocationProgressType.EXCLUDED

            if "Nora" in self.options.enabled_girls.value:
                for l in Nora_loc:
                    self.multiworld.get_location(l, self.player).progress_type = LocationProgressType.EXCLUDED

            if "Lola" in self.options.enabled_girls.value:
                for l in Lola_loc:
                    self.multiworld.get_location(l, self.player).progress_type = LocationProgressType.EXCLUDED

            if "Kyanna" in self.options.enabled_girls.value:
                for l in Kyanna_loc:
                    self.multiworld.get_location(l, self.player).progress_type = LocationProgressType.EXCLUDED

            if "Candace" in self.options.enabled_girls.value:
                for l in Candace_loc:
                    self.multiworld.get_location(l, self.player).progress_type = LocationProgressType.EXCLUDED

            if "Renee" in self.options.enabled_girls.value:
                for l in Renee_loc:
                    self.multiworld.get_location(l, self.player).progress_type = LocationProgressType.EXCLUDED

            if "Tiffany" in self.options.enabled_girls.value:
                for l in Tiffany_loc:
                    self.multiworld.get_location(l, self.player).progress_type = LocationProgressType.EXCLUDED

            if "Brooke" in self.options.enabled_girls.value:
                for l in Brooke_loc:
                    self.multiworld.get_location(l, self.player).progress_type = LocationProgressType.EXCLUDED

            if "Zoey" in self.options.enabled_girls.value:
                for l in Zoey_loc:
                    self.multiworld.get_location(l, self.player).progress_type = LocationProgressType.EXCLUDED

            if "Beli" in self.options.enabled_girls.value:
                for l in Beli_loc:
                    self.multiworld.get_location(l, self.player).progress_type = LocationProgressType.EXCLUDED

            if "Audrey" in self.options.enabled_girls.value:
                for l in Audrey_loc:
                    self.multiworld.get_location(l, self.player).progress_type = LocationProgressType.EXCLUDED

            if "Sarah" in self.options.enabled_girls.value:
                for l in Sarah_loc:
                    self.multiworld.get_location(l, self.player).progress_type = LocationProgressType.EXCLUDED

            if "Lailani" in self.options.enabled_girls.value:
                for l in Lailani_loc:
                    self.multiworld.get_location(l, self.player).progress_type = LocationProgressType.EXCLUDED

            if "Aiko" in self.options.enabled_girls.value:
                for l in Aiko_loc:
                    self.multiworld.get_location(l, self.player).progress_type = LocationProgressType.EXCLUDED

            if "Nadia" in self.options.enabled_girls.value:
                for l in Nadia_loc:
                    self.multiworld.get_location(l, self.player).progress_type = LocationProgressType.EXCLUDED

            if "Marlena" in self.options.enabled_girls.value:
                for l in Marlena_loc:
                    self.multiworld.get_location(l, self.player).progress_type = LocationProgressType.EXCLUDED

        self.multiworld.completion_condition[self.player] = lambda state: state.has("victory", self.player)

    def fill_slot_data(self) -> dict:
        returndict = {
            "start_girl": self.startinggirl,
            "enabled_girls": self.options.enabled_girls.value,

            "Jessie_loc_start": Jessie_loc_start,
            "Nikki_loc_start": Nikki_loc_start,
            "Lillian_loc_start": Lillian_loc_start,
            "Nora_loc_start": Nora_loc_start,
            "Lola_loc_start": Lola_loc_start,
            "Kyanna_loc_start": Kyanna_loc_start,
            "Candace_loc_start": Candace_loc_start,
            "Renee_loc_start": Renee_loc_start,
            "Tiffany_loc_start": Tiffany_loc_start,
            "Brooke_loc_start": Brooke_loc_start,
            "Zoey_loc_start": Zoey_loc_start,
            "Beli_loc_start": Beli_loc_start,
            "Audrey_loc_start": Audrey_loc_start,
            "Sarah_loc_start": Sarah_loc_start,
            "Lailani_loc_start": Lailani_loc_start,
            "Aiko_loc_start": Aiko_loc_start,
            "Nadia_loc_start": Nadia_loc_start,
            "Marlena_loc_start": Marlena_loc_start,
            "trophy_loc_start":trophy_loc_start,
            "shop_loc_start":shop_loc_start,

            "girls_start": girls_start,
            "upgrade_item_start": upgrade_item_start,
            "items_start": items_start,

            "girldata": self.girldata,
            "shop_items": self.options.shop_items.value,
            "goal": self.options.goal.value,
            "force_goal": self.options.force_goal.value,
            "min_trophy": self.options.min_trophy.value,

            "world_version": self.worldversion,
        }

        return returndict
