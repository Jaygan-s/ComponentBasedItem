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
        public virtual void OnAttackReleased()
        {
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
        Dictionary<Type, ItemAbilityComponentBase> abilityComponents = new Dictionary<Type, ItemAbilityComponentBase>();
        public T? GetAbilityComponent<T>()
        {
            abilityComponents.TryGetValue(typeof(T), out var component);
            if(component != null)
                return (T)Convert.ChangeType(component, typeof(T));
            else
                return default(T);
        }
        protected T AddAbilityComponent<T>(T component)
        {
            if(component != null)
                abilityComponents.Add(component.GetType(), component as ItemAbilityComponentBase);
            return component;
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

    public class KatanaType : WeaponBase
    {
        AbilityDamage? damage;

        public override void OnAttackPressed()
        {
            base.OnAttackPressed();
            var Damage = GetAbilityComponent<AbilityDamage>();
            if(Damage != null)
                Console.WriteLine($" > Sugoiiii Sashimi! {name}-Kun Ganbahtteh YOUR DAMAGE IS {Damage.value}!");
        }
        public override void InitComponents()
        {
            base.InitComponents();
            damage = AddAbilityComponent(new AbilityDamage() { value = 2 });
        }
    }
    public class GunKataType : WeaponBase
    {
        protected AbilityDamage? damage;
        protected AbilityFireAmmoCount? fireAmmoCount;
        protected AbilityMaxAmmo? maxAmmo;
        protected BehaviourAmmo? ammoSystem;

        public override void OnAttackPressed()
        {
            base.OnAttackPressed();
            if (ammoSystem != null && fireAmmoCount != null)
            {
                if(ammoSystem.TryUseAmmo(fireAmmoCount.value))
                {
                    Console.WriteLine($" > Bang! Shots fired from {name}. {ammoSystem.GetRemainingAmmo()} bullets left.");
                }
                else
                {
                    Console.WriteLine($" > Tick! Tick! It needs reload.");
                }
            }
        }

        public override void OnReload()
        {
            base.OnReload();
            if (ammoSystem != null && maxAmmo != null)
            {
                if(ammoSystem.IsAmmoEnough(maxAmmo.value))
                {
                    Console.WriteLine($" > It's already reloaded. no need to waste effort.");
                }
                else
                {
                    int reloadCount = ammoSystem.RefilAmmo();
                    Console.WriteLine($" > Reeeeee-loading! Loaded {reloadCount} more bullets. {ammoSystem.GetRemainingAmmo()} bullets left.");
                }
            }
        }

        public override void InitComponents()
        {
            base.InitComponents();
            damage = AddAbilityComponent(new AbilityDamage() { value = 2 });
            fireAmmoCount = AddAbilityComponent(new AbilityFireAmmoCount() { value = 1 });
            maxAmmo = AddAbilityComponent(new AbilityMaxAmmo() { value = 12 });
            ammoSystem = AddBehaviourComponent(new BehaviourAmmo(this));
        }
    }

    public class EE_Sehki_Yernbal_Chong_SSunda_Type : GunKataType
    {
        AbilityAutoRepeatCount? autoRepeatCount;
        AbilityCooldown? cooldown;
        public override void OnAttackPressed()
        {
            if (autoRepeatCount != null &&
                cooldown != null &&
                ammoSystem != null &
                fireAmmoCount != null)
            {
                for(int i = 0; i < autoRepeatCount.value; i++)
                {
                    base.OnAttackPressed();
                    if (!ammoSystem.IsAmmoEnough(fireAmmoCount.value))
                    {
                        break;
                    }
                    else
                    {
                        Thread.Sleep(cooldown.value);
                    }
                }
            }
        }

        public override void InitComponents()
        {
            base.InitComponents();
            Dictionary<Type, string> dummyTableKeyValuePair = new Dictionary<Type, string> { 
                { typeof(AbilityAutoRepeatCount), "2" }, 
                { typeof(AbilityCooldown), "200" } 
            };

            autoRepeatCount = AddAbilityComponent(new AbilityAutoRepeatCount());
            cooldown = AddAbilityComponent(new AbilityCooldown());

            foreach(var key in dummyTableKeyValuePair.Keys)
            {
                key.ToString();
                var aa = key.GetMethod("GetAbilityComponent").Invoke(this, null);
                ;

                bool a = true;
            }

            autoRepeatCount.SetFromTable("3");
            cooldown.SetFromTable("200");
        }

    }
}
