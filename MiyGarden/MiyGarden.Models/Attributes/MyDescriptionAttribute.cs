using System;

namespace MiyGarden.Models.Attributes
{
    public class MyDescriptionAttribute : Attribute
    {
        public string Description { get; set; }

        public MyDescriptionAttribute(string Description)
        {
            this.Description = Description;
        }

        public override string ToString()
        {
            return this.Description.ToString();
        }
    }
}
