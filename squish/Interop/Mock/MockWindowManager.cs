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
        public List<TaskbarWindow> RunningWindows => new List<TaskbarWindow>
        {
            new()
            {
                Title = "i wanna",
                Icon = File.ReadAllBytes(@"C:\Users\poohw\OneDrive\Music\3rd Strike\[DCKS-0049] V.A. - Σ3-2017 3rd STRIKE\jacket.png")
            },
            new()
            {
                Title = "hug a",
                IsActiveWindow = true,
                Icon = File.ReadAllBytes(@"C:\Users\poohw\OneDrive\Music\3rd Strike\[DCKS-0049] V.A. - Σ3-2017 3rd STRIKE\jacket.png")
            },
            new()
            {
                Title = "catgirl",
                Icon = File.ReadAllBytes(@"C:\Users\poohw\OneDrive\Music\3rd Strike\[DCKS-0049] V.A. - Σ3-2017 3rd STRIKE\jacket.png")
            }
        };
    }
}
