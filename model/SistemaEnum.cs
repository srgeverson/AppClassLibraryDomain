using System.ComponentModel;

namespace AppClassLibraryDomain.model
{
    public enum SistemaEnum : long
    {
        [Description("API C#")]
        ApiCSharp = 1,
        [Description("Aplicação Windows Forms")]
        WindowsForms = 2
    }
}
