using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSItemStudy
{
    public class ActorClass
    {
        public string ActorName = "";


        // Loadout
        WeaponBase? PrimaryWeapon; // Selected weapon
        GunKataType WindowMaker = new GunKataType() { name="Window Maker" };
        KatanaType Karnata = new KatanaType() { name = "Karnata" };
        EE_Sehki_Yernbal_Chong_SSunda_Type DdabalBan = new EE_Sehki_Yernbal_Chong_SSunda_Type() { name = "Ddabal Chong Ban plz" };

        // Inventory



        public ActorClass()
        {
            WindowMaker.InitComponents();
            Karnata.InitComponents();
            DdabalBan.InitComponents();
        }


        public bool ActionSelectEvent(bool newPrint)
        {
            if(newPrint)
            {
                Console.Clear();
                Console.WriteLine("::: SELECT ACTION ::::::::::::::");
                Console.WriteLine(" - I : Use Inventory Item");
                Console.WriteLine(" - W : Switch Weapon");
                if (PrimaryWeapon != null)
                {
                    Console.WriteLine($" - E : Attack:{PrimaryWeapon.name} (Primary Weapon)");
                    if (PrimaryWeapon.IsAmmoType())
                        Console.WriteLine($" - R : Reload:{PrimaryWeapon.name} (Primary Weapon)");
                }
            }

            var key = Console.ReadKey();
            Console.WriteLine("");
            switch (key.Key)
            {
                case ConsoleKey.I:
                    ItemSelectEvent();
                    return true;
                case ConsoleKey.W:
                    WeaponSwitchEvent();
                    return true;
                case ConsoleKey.E:
                    OnFireWeapon();
                    return false;
                case ConsoleKey.R:
                    if (PrimaryWeapon != null && PrimaryWeapon.IsAmmoType())
                    {
                        OnReloadWeapon();
                        return false;
                    }
                    break;
            }
            return false;
        }
        public void WeaponSwitchEvent()
        {
            Console.Clear();
            Console.WriteLine("::: SELECT WEAPON TO USE :::::");
            Console.WriteLine($" - 1 : {WindowMaker.name}");
            Console.WriteLine($" - 2 : {Karnata.name}");
            Console.WriteLine($" - 3 : {DdabalBan.name}");
            var key = Console.ReadKey();
            Console.WriteLine("");
            switch (key.Key)
            {
                case ConsoleKey.D1:
                    PrimaryWeapon = WindowMaker;
                    Console.WriteLine($" > Selected {WindowMaker.name}");
                    break;
                case ConsoleKey.D2:
                    PrimaryWeapon = Karnata;
                    Console.WriteLine($" > Selected {Karnata.name}");
                    break;
                case ConsoleKey.D3:
                    PrimaryWeapon = DdabalBan;
                    Console.WriteLine($" > Selected {DdabalBan.name}");
                    break;
            }
        }
        public void ItemSelectEvent()
        {
            Console.Clear();
            Console.WriteLine("::: SELECT ITEM ::::::::::::::");
            Console.WriteLine(" - 1 : Inventory:Health of Potion (Consumable)");
            Console.WriteLine(" - 2 : Inventory:Fury of Fist (Skillbook)");
            Console.WriteLine(" - 3 : Inventory:Case of Study (Quest Item)");

            var key = Console.ReadKey();
            switch (key.Key)
            {
                case ConsoleKey.D1:
                    Console.WriteLine(" - Using Item:Inventory:Health of Potion");
                    break;
                case ConsoleKey.D2:
                    Console.WriteLine(" - Using Item:Inventory:Fury of Fist");
                    break;
                case ConsoleKey.D3:
                    Console.WriteLine(" - Using Item:Inventory:Case of Study");
                    break;
            }
        }

        private void OnFireWeapon()
        {
            PrimaryWeapon.OnAttackPressed();
        }

        private void OnReloadWeapon()
        {
            if (PrimaryWeapon.IsAmmoType())
                PrimaryWeapon.OnReload();
        }

        private void OnUse(UsableBase item)
        {
            item.OnUse(this);
        }
    }
}
