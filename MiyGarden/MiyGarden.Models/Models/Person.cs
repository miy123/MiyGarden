using MiyGarden.Models.Attributes;
using MiyGarden.Service.MiyExtensions;

namespace MiyGarden.Models.Models
{
    public class Person
    {
        public int Id { set; get; }

        [MyDescription("姓名")]
        public string Name { set; get; }

        [MyDescription("活躍朝代")]
        public string Dynasty { set; get; }

        public string Deeds => string.Join(",", DescriptionExtension.GetDescription<Person>());
    }
}
