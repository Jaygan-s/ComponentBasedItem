using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSItemStudy
{
    public class WeaponBase
    {
        #region Basic Information and Base Functions
        public string name { get; set; }
        public string description { get; set; }

        public bool IsAmmoType()
        {
            return GetBehaviourComponent<BehaviourAmmo>() is not null;
        }

        public virtual void InitComponents()
        {
            abilityComponents.Clear();
        }
        #endregion

        #region Weapon Events
        public virtual void OnAttackPressed()
        {
            //Console.WriteLine($" > Attacking with: {name}");
        }
        public virtual void OnTick()
        {
        }
        public virtual void OnReload()
        {
            //Console.WriteLine($" > Reloading: {name}");
        }
        #endregion

        #region Component Base Structure
        // Ability Component Implementations
        Dictionary<string, AbilityComponentBase> abilityComponents = new Dictionary<string, AbilityComponentBase>();
        public T? GetAbilityComponent<T>(string AbilityName)
        {
            abilityComponents.TryGetValue(AbilityName, out var component);
            if(component != null)
                return (T)Convert.ChangeType(component, typeof(T));
            else
            {
#if DEBUG
                throw new Exception($"Failed to seek ability component '{AbilityName}' in {this.GetType()}");
#else
                return default(T);
#endif
            }
        }
        protected T AddAbilityComponent<T>(T component)
        {
            if (component != null)
            {
                var ComponentAsBase = component as AbilityComponentBase;
                if (ComponentAsBase != null)
                    abilityComponents.Add(ComponentAsBase.GetAbilityName(), ComponentAsBase);
                else
                    throw new Exception("Failed to cast component to AbilityComponentBase");
            }
            return component;
        }
        public void UpdateAbilityFromKeyValuePair(Dictionary<string, string> keyValuePairs)
        {
            foreach (var entry in keyValuePairs)
            {
                IntAbility? component = GetAbilityComponent<IntAbility>(entry.Key);
                if (component != null)
                {
                    component.SetFromTable(entry.Value);
                }
            }
        }
        public void PrintWeaponInformation()
        {
            Console.WriteLine($"[{name}]");
            Console.WriteLine($" Behaviour components:");
            foreach (var entry in behaviourComponents)
            {
                Console.WriteLine($" - {entry.Value.GetType()}");
            }
            Console.WriteLine($" Ability components:");
            foreach (var entry in abilityComponents)
            {
                entry.Value.PrintValue();
            }
        }

        // Behaviour Component Implementations
        Dictionary<Type, ItemBehaviourComponent> behaviourComponents = new Dictionary<Type, ItemBehaviourComponent>();
        public T? GetBehaviourComponent<T>()
        {
            behaviourComponents.TryGetValue(typeof(T), out var component);
            if (component != null)
                return (T)Convert.ChangeType(component, typeof(T));
            else
                return default(T);
        }
        protected T AddBehaviourComponent<T>(T component)
        {
            if (component != null)
                behaviourComponents.Add(component.GetType(), component as ItemBehaviourComponent);
            return component;
        }

#endregion

    }

    /// <summary>
    /// 근접 무기
    /// </summary>
    public class KatanaType : WeaponBase
    {
        protected IntAbility? damage;

        public override void OnAttackPressed()
        {
            base.OnAttackPressed();
            if(damage != null)
                Console.WriteLine($" > 육회사시미! {name}로 적을 베었습니다!");
        }
        public override void InitComponents()
        {
            base.InitComponents();
            damage = AddAbilityComponent(new IntAbility() { Name = "damage" });
        }
    }

    /// <summary>
    /// 건카타 타입: 한 번의 입력에 한 발만 발사하는 총기류 타입
    /// </summary>
    public class GunKataType : WeaponBase
    {
        protected IntAbility? damage;
        protected IntAbility? fireAmmoCount;
        protected IntAbility? maxAmmo;
        protected BehaviourAmmo? ammoSystem;

        public override void OnAttackPressed()
        {
            base.OnAttackPressed();
            if (ammoSystem != null && fireAmmoCount != null)
            {
                if(ammoSystem.TryUseAmmo(fireAmmoCount.Value))
                {
                    Console.WriteLine($" > 탕! {name}이 불을 뿜었습니다. {ammoSystem.GetRemainingAmmo()} 발 남았습니다.");
                }
                else
                {
                    Console.WriteLine($" > 틱! 틱! 재장전이 필요합니다.");
                }
            }
        }

        public override void OnReload()
        {
            base.OnReload();
            if (ammoSystem != null && maxAmmo != null)
            {
                if(ammoSystem.IsAmmoEnough(maxAmmo.Value))
                {
                    Console.WriteLine($" > 이미 장전되어 있습니다.");
                }
                else
                {
                    int reloadCount = ammoSystem.RefilAmmo();
                    Console.WriteLine($" > 리로오오오딩! {reloadCount} 개의 탄약을 사용하여 {ammoSystem.GetRemainingAmmo()} 발까지 장전했습니다.");
                }
            }
        }

        public override void InitComponents()
        {
            base.InitComponents();
            damage          = AddAbilityComponent(new IntAbility() { Name = "damage" });
            fireAmmoCount   = AddAbilityComponent(new IntAbility() { Name = "fire_ammo" });
            maxAmmo         = AddAbilityComponent(new IntAbility() { Name = "max_ammo" });

            ammoSystem = AddBehaviourComponent(new BehaviourAmmo(this));
        }
    }

    /// <summary>
    /// 쏘스트롱크세미오토라이플 : 한 번 입력에 N발을 발사하는 타입
    /// </summary>
    public class SoStronkSemiAutoRifle : GunKataType
    {
        /// <summary>
        /// 발사버튼 한번에 발사할 총알의 수
        /// </summary>
        IntAbility? repeatCount;
        /// <summary>
        /// 연사 딜레이
        /// </summary>
        IntAbility? repeatDelay;
        public override void OnAttackPressed()
        {
            if (repeatCount != null &&
                repeatDelay != null &&
                ammoSystem != null &&
                fireAmmoCount != null)
            {
                for(int i = 0; i < repeatCount.Value; i++)
                {
                    base.OnAttackPressed();
                    if (!ammoSystem.IsAmmoEnough(fireAmmoCount.Value))
                    {
                        break;
                    }
                    else
                    {
                        Thread.Sleep(repeatDelay.Value);
                    }
                }
            }
        }

        public override void InitComponents()
        {
            base.InitComponents();
            repeatCount = AddAbilityComponent(new IntAbility() { Name = "repeat_count" });
            repeatDelay = AddAbilityComponent(new IntAbility() { Name = "repeat_delay" });
        }

    }
}
