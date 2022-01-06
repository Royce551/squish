//using Squish.Models;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Squish.Interop.Mock
//{
//    public class MockWindowManager : IWindowManager
//    {
//        public List<TaskbarWindow> RunningWindows => workingRunningWindows;

//        private List<TaskbarWindow> workingRunningWindows = new List<TaskbarWindow>
//        {
//            new()
//            {
//                Id = "1",
//                Title = "i wanna",
//                //Icon = File.ReadAllBytes(@"jacket.png")
//            },
//            new()
//            {
//                Id = "2",
//                Title = "hug a",
//                IsActiveWindow = true,
//                //Icon = File.ReadAllBytes(@"jacket.png")
//            },
//            new()
//            {
//                Id = "3",
//                Title = "catgirl",
//                //Icon = File.ReadAllBytes(@"jacket.png")
//            }
//        };

//        public event EventHandler? WindowsUpdated;
//    }
//}
