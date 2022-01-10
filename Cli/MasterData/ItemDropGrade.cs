using System;
using System.Collections.Generic;
using System.Drawing;
using Newtonsoft.Json;
using EnumExtend;
using Cli.Types;

namespace Cli.MasterData
{
	public partial class ItemDropGrade
	{
        public string? Id { get; set; }

        [JsonConverter(typeof(JsonEnumConverter<GradeType>))]
        public GradeType Grade { get; set; }

        public double Probability { get; set; }

        public int OptionMin { get; set; }

        public int OptionMax { get; set; }

        [JsonConverter(typeof(JsonEnumConverter<GradeType>))]
        public GradeType MinOptionGrade { get; set; }

	}
}
