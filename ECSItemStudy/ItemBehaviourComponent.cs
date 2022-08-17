using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSItemStudy
{
    public class ItemBehaviourComponent
    {
        protected WeaponBase weaponBase { get; set; }
        public ItemBehaviourComponent(WeaponBase weaponBase)
        {
            this.weaponBase = weaponBase;
        }
    }

    public class BehaviourAmmo : ItemBehaviourComponent
    {
        IntAbility? maxAmmo;
        int currentAmmo = 0;

        public BehaviourAmmo(WeaponBase baseObject) : base(baseObject)
        {
            maxAmmo = weaponBase.GetAbilityComponent<IntAbility>("max_ammo");
        }

        public int GetRemainingAmmo()
        {
            return currentAmmo;
        }

        public bool IsAmmoEnough(int amount)
        {
            return currentAmmo >= amount;
        }

        public bool TryUseAmmo(int amount)
        {
            if(IsAmmoEnough(amount))
            {
                currentAmmo -= amount;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// value: 장전할 탄약의 수
        /// </summary>
        /// <param name="value">장전할 탄약의 수; 지정하지 않을 경우 MaxAmmo만큼 장전함.</param>
        /// <returns>새롭게 장전된 탄약의 수</returns>
        public int RefilAmmo(int? value = null)
        {
            int prevAmmo = currentAmmo;
            // 임의의 값을 지정했다면
            if (value != null)
            {
                currentAmmo = value.Value;
                return currentAmmo - prevAmmo;
            }
            // 따로 값을 지정하지 않았다면
            else if(maxAmmo != null)
            {
                currentAmmo = maxAmmo.Value;
                return currentAmmo - prevAmmo;
            }
            else
            {
                return 0;
            }
        }
    }
}
