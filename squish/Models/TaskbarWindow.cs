using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Squish.Models
{
    public class TaskbarWindow
    {
        public string Id { get; init; } = "lol";

        public string Title { get; init; } = "Window";

        public byte[]? Icon { get; init; }

        public bool IsActiveWindow { get; set; } = false;
    }
}
