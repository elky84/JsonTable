using EnumExtend;

namespace Cli.Types
{
	
	public enum ItemOptionType
	{
        [Description("화염저항")]
        FireResistance,

        [Description("화염증폭")]
        FireAmplify,

        [Description("냉기저항")]
        ColdResistance,

        [Description("냉기증폭")]
        ColdAmplify,

        [Description("번개저항")]
        LightningResistance,

        [Description("번개증폭")]
        LightningAmplify,

        [Description("독저항")]
        PoisonResistance,

        [Description("독증폭")]
        PoisonAmplify,

        [Description("이동속도")]
        MoveSpeed,

        [Description("시전시간")]
        CastRate,

        [Description("피격지연")]
        HitDelay,

        [Description("힘")]
        Might,

        [Description("민첩")]
        Dexterity,

        [Description("체질")]
        Constitution,

        [Description("지능")]
        Intellect,

        [Description("통찰")]
        Perception,

        [Description("결의")]
        Resolve,

        [Description("공격속도")]
        AttackSpeed,

        [Description("데미지")]
        Damage,

        [Description("방어력")]
        Defense,

        [Description("추가데미지")]
        AdditionalDamage,

        [Description("고장방어력")]
        FixedDefense,

        [Description("공격등급")]
        AttackRate,

        [Description("체력")]
        Hp,

        [Description("마나")]
        Mana,

	}
}
