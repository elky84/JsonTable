using EnumExtend;

namespace Cli.Types
{
	
	public enum ItemSlotType
	{
        [Description("불가")]
        Impossible,

        [Description("양손")]
        BothHands,

        [Description("왼손")]
        LeftHand,

        [Description("오른손")]
        RightHand,

        [Description("머리")]
        Head,

        [Description("갑옷")]
        Armor,

        [Description("장갑")]
        Glove,

        [Description("신발")]
        Boots,

        [Description("왼손가락")]
        LeftFinger,

        [Description("오른손가락")]
        RightFinger,

        [Description("손가락")]
        Finger,

        [Description("목")]
        Neck,

        [Description("벨트")]
        Belt,

	}
}
