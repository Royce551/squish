//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Squish.Interop.Mock
//{
//    public class MockWindow : IWindow
//    {
//        public string Title { get; }

//        public byte[]? Icon { get; }

//        private bool isFocused = false;
//        public bool IsFocused
//        {
//            get => isFocused;
//            set
//            {
//                bool wasWindowFound = false;
//                foreach (var window in windowManager.RunningWindows)
//                {
//                    if (window.Id == id)
//                    {
//                        window.IsActiveWindow = true;
//                        wasWindowFound = true;
//                    }
//                    else
//                    {
//                        window.IsActiveWindow = false;
//                    }
//                }
//                windowManager.WindowsUpdated?.Invoke(null, EventArgs.Empty);
//                if (!wasWindowFound) throw new InvalidOperationException("can't focus a window that doesn't exist");
//            }
//        }

//        private IWindowManager windowManager;

//        public MockWindow(IWindowManager windowManager, string title, byte[] icon, bool isFocused)
//        {
//            this.windowManager = windowManager;
//            Title = title;
//            Icon = icon;
//            IsFocused = isFocused;
//        }
//    }
//}
