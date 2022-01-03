using Squish.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Squish.Interop.Linux
{
    public class LinuxWindowManager : IWindowManager
    {
        public List<TaskbarWindow> RunningWindows => throw new NotImplementedException();
    }
}
