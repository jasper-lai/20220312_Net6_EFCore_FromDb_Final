using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyConsoleApp.Models
{
    internal class MyNullableModel
    {
    }

    internal class AppInfo
    {
        public string Name { get; set; }     // 會被提示 Nullable
    }
    internal class NullFix1_AppInfo
    {
        public string? Name { get; set; }    // 明確標示可能是 Null
    }
    internal class NullFix2_AppInfo
    {
        public string Name { get; set; } = default!;   // 使用 default!, 避開 Name 下方的毛毛蟲
    }
    internal class NullFix3_AppInfo
    {
        public string Name { get; set; } = null!;   // 使用 null, 避開 Name 下方的毛毛蟲
    }
    internal class NullFix4_AppInfo
    {
        public string Name { get; set; } = string.Empty;   // 建議這樣寫, 避開 Name 下方的毛毛蟲
    }

}
