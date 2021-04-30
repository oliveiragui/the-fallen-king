namespace _Game.Scripts.GameContent.Abilities
{
    public class AbilityCombo
    {
        readonly AbilityComboData _data;

        public AbilityCombo(AbilityComboData data, Ability ability)
        {
            _data = data;
        }

        public RawComboAttributes Attributes => _data.attributes;
        public float Factor3 => _data.factor3;
        public float Factor2 => _data.factor2;
        public float Factor1 => _data.factor1;
        public bool Castable => _data.castable;

        public static implicit operator bool(AbilityCombo hit) =>
            // assuming, that 1 is true;
            // somehow this method should deal with value == null case
            hit != null;
    }
}