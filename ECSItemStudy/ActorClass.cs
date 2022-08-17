using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSItemStudy
{
    public class ActorClass
    {
        // 그냥 왠지 넣어보고 싶었음
        public string ActorName = "";

        // Loadout (Weapons)
        WeaponBase? PrimaryWeapon; // Selected weapon
        GunKataType WindowMaker = new GunKataType() { name="윈도우메이커" };
        KatanaType Karnata = new KatanaType() { name = "카나타" };
        SoStronkSemiAutoRifle DdabalBan = new SoStronkSemiAutoRifle() { name = "세미오토라이훌" };

        // Inventory



        public ActorClass()
        {
            // 컴포넌트 초기화
            WindowMaker.InitComponents();
            Karnata.InitComponents();
            DdabalBan.InitComponents();

            // 실제로는 테이블로부터 값을 받아와서 능력치를 업데이트하는 부분
            Dictionary<string, string> statArray_WindowMaker = new Dictionary<string, string> {
                { "damage", "10" },
                { "fire_ammo", "1" },
                { "max_ammo", "6" },
            };
            WindowMaker.UpdateAbilityFromKeyValuePair(statArray_WindowMaker);

            // 실제로는 테이블로부터 값을 받아와서 능력치를 업데이트하는 부분
            Dictionary<string, string> statArray_Karnata = new Dictionary<string, string> {
                { "damage", "20" },
            };
            Karnata.UpdateAbilityFromKeyValuePair(statArray_Karnata);

            // 실제로는 테이블로부터 값을 받아와서 능력치를 업데이트하는 부분
            Dictionary<string, string> statArray_ddabal = new Dictionary<string, string> {
                { "damage", "1" },
                { "max_ammo", "21" },
                { "fire_ammo", "1" },
                { "repeat_count", "3" },
                { "repeat_delay", "100" },
            };
            DdabalBan.UpdateAbilityFromKeyValuePair(statArray_ddabal);
        }

        /// <summary>
        /// 액션 메뉴를 출력하고 사용자의 입력을 기다림
        /// </summary>
        /// <param name="newPrint">화면을 비우고 새로 메뉴를 출력하는지의 여(true)/부(false)</param>
        /// <returns></returns>
        public bool ActionSelectEvent(bool newPrint)
        {
            if(newPrint)
            {
                Console.Clear();
                Console.WriteLine("::: PLAYER STATUS ::::::::::::::");
                Console.WriteLine($" - 주 무기: {(PrimaryWeapon != null ? PrimaryWeapon.name : "None")}");
                Console.WriteLine("::: SELECT ACTION ::::::::::::::");
                Console.WriteLine(" - I : 인벤토리 아이템 사용");
                Console.WriteLine(" - W : 무기 선택");
                if (PrimaryWeapon != null)
                {
                    Console.WriteLine($" - E : 공격:{PrimaryWeapon.name} (주 무기)");
                    if (PrimaryWeapon.IsAmmoType())
                        Console.WriteLine($" - R : 장전:{PrimaryWeapon.name} (주 무기)");
                    Console.WriteLine($" - F : 정보:{PrimaryWeapon.name} (주 무기)");
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
                case ConsoleKey.F:
                    if (PrimaryWeapon != null)
                    {
                        Console.WriteLine($" > {PrimaryWeapon.name}의 정보를 출력 중");
                        PrimaryWeapon.PrintWeaponInformation();
                    }
                    return false;
            }
            return false;
        }

        /// <summary>
        /// 무기 선택 메뉴. 사용자의 입력(1~3)을 받아 해당하는 무기를 장착함
        /// </summary>
        public void WeaponSwitchEvent()
        {
            Console.Clear();
            Console.WriteLine("::: 무기 선택 :::::");
            Console.WriteLine($" - 1 : {WindowMaker.name}");
            Console.WriteLine($" - 2 : {Karnata.name}");
            Console.WriteLine($" - 3 : {DdabalBan.name}");
            Console.WriteLine(" - 0 : Exit");

            var key = Console.ReadKey();
            Console.WriteLine("");
            switch (key.Key)
            {
                case ConsoleKey.D0:
                    return;
                case ConsoleKey.D1:
                    PrimaryWeapon = WindowMaker;
                    Console.WriteLine($" > {WindowMaker.name}를 장착합니다");
                    break;
                case ConsoleKey.D2:
                    PrimaryWeapon = Karnata;
                    Console.WriteLine($" > {Karnata.name}를 장착합니다");
                    break;
                case ConsoleKey.D3:
                    PrimaryWeapon = DdabalBan;
                    Console.WriteLine($" > {DdabalBan.name}를 장착합니다");
                    break;
            }
        }

        /// <summary>
        /// (아직 메뉴만 구현) 아이템 사용 메뉴
        /// </summary>
        public void ItemSelectEvent()
        {
            Console.Clear();
            Console.WriteLine("::: SELECT ITEM ::::::::::::::");
            Console.WriteLine(" - 1 : Inventory:Health of Potion (Consumable)");
            Console.WriteLine(" - 2 : Inventory:Fury of Fist (Skillbook)");
            Console.WriteLine(" - 3 : Inventory:Case of Study (Quest Item)");
            Console.WriteLine(" - 0 : Exit");

            var key = Console.ReadKey();
            switch (key.Key)
            {
                case ConsoleKey.D0:
                    return;
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

        /// <summary>
        /// 액션 메뉴에서 무기 발사 키를 눌렀을 때
        /// </summary>
        private void OnFireWeapon()
        {
            if(PrimaryWeapon != null)
                PrimaryWeapon.OnAttackPressed();
        }

        /// <summary>
        /// 액션 메뉴에서 무기 장전 키를 눌렀을 때
        /// </summary>
        private void OnReloadWeapon()
        {
            if (PrimaryWeapon != null && PrimaryWeapon.IsAmmoType())
                PrimaryWeapon.OnReload();
        }

        /// <summary>
        /// 아이템 선택 메뉴에서 아이템을 사용했을 때
        /// </summary>
        /// <param name="item"></param>
        private void OnUse(UsableBase item)
        {
            item.OnUse(this);
        }
    }
}
