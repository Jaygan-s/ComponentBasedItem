using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSItemStudy
{

    public class ItemAbilityComponentBase
    {
        public virtual void SetFromTable(string valueString)
        {
            return;
        }
        public virtual void PrintValue()
        {
            return;
        }
    }

    public class AbilityMaxAmmo : ItemAbilityComponentBase
    {
        public int value;
        public override void SetFromTable(string valueString)
        {
            value = int.Parse(valueString);
        }
        public override void PrintValue()
        {
            base.PrintValue();
            Console.WriteLine($"{GetType()}: {value}");
        }
    }

    public class AbilityFireAmmoCount : ItemAbilityComponentBase
    {
        public int value;
        public override void SetFromTable(string valueString)
        {
            value = int.Parse(valueString);
        }
        public override void PrintValue()
        {
            base.PrintValue();
            Console.WriteLine($"{GetType()}: {value}");
        }
    }
    public class AbilityAutoRepeatCount : ItemAbilityComponentBase
    {
        public int value;
        public override void SetFromTable(string valueString)
        {
            value = int.Parse(valueString);
        }
        public override void PrintValue()
        {
            base.PrintValue();
            Console.WriteLine($"{GetType()}: {value}");
        }
    }
    public class AbilityCooldown : ItemAbilityComponentBase
    {
        public int value;
        public override void SetFromTable(string valueString)
        {
            value = int.Parse(valueString);
        }
        public override void PrintValue()
        {
            base.PrintValue();
            Console.WriteLine($"{GetType()}: {value}");
        }
    }
    public class AbilityDamage : ItemAbilityComponentBase
    {
        public int value;
        public override void SetFromTable(string valueString)
        {
            value = int.Parse(valueString);
        }
        public override void PrintValue()
        {
            base.PrintValue();
            Console.WriteLine($"{GetType()}: {value}");
        }
    }
}
