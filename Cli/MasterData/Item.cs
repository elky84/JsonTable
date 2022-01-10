using System;
using System.Collections.Generic;
using System.Drawing;
using Newtonsoft.Json;
using EnumExtend;
using Cli.Types;

namespace Cli.MasterData
{
	public partial class Item
	{
        public string? Id { get; set; }

        [JsonConverter(typeof(JsonEnumConverter<ItemType>))]
        public ItemType Type { get; set; }

        [JsonConverter(typeof(JsonEnumConverter<ItemSlotType>))]
        public ItemSlotType SlotType { get; set; }

        public string? Name { get; set; }

        public float Weight { get; set; }

        [JsonConverter(typeof(JsonEnumsConverter<ClassType>))]
        public List<ClassType>? ClassLimit { get; set; }

        public string? Icon { get; set; }

        public string? Description { get; set; }

        public int Lv { get; set; }

        public int LvLimit { get; set; }

        public int Mgt { get; set; }

        public int Dex { get; set; }

        public int Con { get; set; }

        public int Int { get; set; }

        public int Per { get; set; }

        public int Res { get; set; }

	}
}
