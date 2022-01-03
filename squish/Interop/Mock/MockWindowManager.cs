using Squish.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Squish.Interop.Mock
{
    public class MockWindowManager : IWindowManager
    {
        public List<TaskbarWindow> RunningWindows => workingRunningWindows;

        private List<TaskbarWindow> workingRunningWindows = new List<TaskbarWindow>
        {
            new()
            {
                Id = "1",
                Title = "i wanna",
                //Icon = File.ReadAllBytes(@"jacket.png")
            },
            new()
            {
                Id = "2",
                Title = "hug a",
                IsActiveWindow = true,
                //Icon = File.ReadAllBytes(@"jacket.png")
            },
            new()
            {
                Id = "3",
                Title = "catgirl",
                //Icon = File.ReadAllBytes(@"jacket.png")
            }
        };

        public event EventHandler? WindowsUpdated;

        public void FocusWindow(string id)
        {
            bool wasWindowFound = false;
            foreach (var window in workingRunningWindows)
            {
                if (window.Id == id)
                {
                    window.IsActiveWindow = true;
                    wasWindowFound = true;
                }
                else
                {
                    window.IsActiveWindow = false;
                }
            }
            WindowsUpdated?.Invoke(null, EventArgs.Empty);
            if (!wasWindowFound) throw new InvalidOperationException("can't focus a window that doesn't exist");
        }
    }
}
