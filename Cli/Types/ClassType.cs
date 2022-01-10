using EnumExtend;

namespace Cli.Types
{
	
	public enum ClassType
	{
        [Description("기사")]
        Knight,

        [Description("레인저")]
        Ranger,

        [Description("마법사")]
        Wizard,

        [Description("전사")]
        Warrior,

        [Description("성직자")]
        Priest,

        [Description("도적")]
        Thief,

        [Description("학자")]
        Mage,

        [Description("바바리안")]
        Babarian,

        [Description("드루이드")]
        Druid,

	}
}
