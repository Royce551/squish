using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Squish.Interop
{
    public interface IWindow
    {
        public string Title { get; }

        public byte[]? Icon { get; }

        public bool IsFocused { get; set; }
    }
}
