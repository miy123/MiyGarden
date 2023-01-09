using System.ComponentModel;

namespace MiyGarden.Models.Enums
{
    public enum MediaType
    {
        [Description("application/json")]
        ApplicationJson = 0,
        [Description("application/x-www-form-urlencoded")]
        ApplicationUrlencoded = 1
    }
}
