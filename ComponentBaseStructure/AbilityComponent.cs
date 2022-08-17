using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentBaseStructure
{
    public abstract class AbilityComponentBase
    {
        public string Name;
        public void SetAbilityName(string NewName)
        {
            Name = NewName;
        }
        public string GetAbilityName()
        {
            return Name;
        }
        public enum AbilityDataType
        {
            None,
            Int,
            String,
            Float,
        }
        public virtual void SetFromTable(string valueString)
        {
            return;
        }
        public virtual void PrintValue()
        {
            return;
        }
        public virtual AbilityDataType GetAbilityDataType()
        {
            return AbilityDataType.None;
        }
    }

    public class IntAbility : AbilityComponentBase
    {
        public int Value;
        public override void SetFromTable(string valueString)
        {
            Value = int.Parse(valueString);
        }
        public override void PrintValue()
        {
            base.PrintValue();
            Console.WriteLine($" - {Name}: {Value}");
        }
    }

    public class StringAbility : AbilityComponentBase
    {
        public string Value;
        public override void SetFromTable(string valueString)
        {
            Value = valueString;
        }
        public override void PrintValue()
        {
            base.PrintValue();
            Console.WriteLine($" - {Name}: {Value}");
        }
    }

}
