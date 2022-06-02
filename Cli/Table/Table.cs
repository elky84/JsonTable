using Cli.MasterData;
using Cli.Types;
using JsonTable;

namespace Cli.Table
{
    [Table(@"json")]
    public partial class TableItem : BaseDict<string, Item>
    {
    }

    [Table(@"json", "Type")]
    public partial class TableItemByType : BaseMultiDict<ItemType, Item>
    {
    }

    [Table(@"json")]
    public partial class TableItemDropGrade : BaseList<ItemDropGrade>
    {
        protected override List<ItemDropGrade> OnLoad(List<ItemDropGrade> list)
        {
            return list.OrderByDescending(x => (int)x!.Grade).ToList();
        }
    }


    [Table(@"json")]
    public partial class TableItemOption : BaseDict<string, ItemOption>
    {
    }

    [Table(@"json", "Grade")]
    public partial class TableItemOptionByGrade : BaseMultiDict<GradeType, ItemOption>
    {
        protected override Dictionary<GradeType, List<ItemOption>> OnLoad(List<ItemOption> list)
        {
            foreach (var itemOption in list)
            {
                foreach (var grade in Enumerable.Range((int)GradeType.Normal, (int)itemOption!.Grade + 1))
                {
                    Add((GradeType)grade, itemOption);
                }
            }

            return _dictionary;
        }
    }
}
