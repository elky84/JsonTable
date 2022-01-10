using System;
using System.Collections.Generic;
using System.Drawing;
using Newtonsoft.Json;
using EnumExtend;
using Cli.Types;

namespace Cli.MasterData
{
	public partial class ItemOption
	{
        public string? Id { get; set; }

        [JsonConverter(typeof(JsonEnumConverter<GradeType>))]
        public GradeType Grade { get; set; }

        [JsonConverter(typeof(JsonEnumConverter<ItemOptionType>))]
        public ItemOptionType Option { get; set; }

        public float ValueMin { get; set; }

        public float ValueMax { get; set; }

        public string? Level { get; set; }

        [JsonConverter(typeof(JsonEnumsConverter<ItemSlotType>))]
        public List<ItemSlotType>? SlotLimit { get; set; }

        [JsonConverter(typeof(JsonEnumsConverter<ClassType>))]
        public List<ClassType>? ClassLimit { get; set; }

	}
}
