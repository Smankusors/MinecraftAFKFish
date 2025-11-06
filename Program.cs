using CoreAudioApi;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace MinecraftAFKFish {
    class Program {

        [Flags]
        public enum MouseEventFlags {
            RightDown = 0x00000008,
            RightUp = 0x00000010
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetCursorPos(out MousePoint lpMousePoint);

        [DllImport("user32.dll")]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        static MousePoint currentMousePoint;
        static int gotchaCount = 0;

        static void doMancing() {
            doRightClick();
            Thread.Sleep(300);
            doRightClick();
            Thread.Sleep(2000);
        }

        static void doRightClick() {
            mouse(MouseEventFlags.RightDown);
            Thread.Sleep(100);
            mouse(MouseEventFlags.RightUp);
        }


        static void Main(string[] args) {
            MMDeviceEnumerator devEnum = new MMDeviceEnumerator();
            MMDevice device = devEnum.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia);
            AudioMeterInformation audioMeterInfo = null;
            for (int i = 0; i < device.AudioSessionManager.Sessions.Count; i++) {
                var session = device.AudioSessionManager.Sessions[i];
                uint procID = session.ProcessID;
                string procName = Process.GetProcessById((int)procID).ProcessName;
                if (procName == "javaw") {
                    Console.WriteLine("Got Minecraft @ " + procID.ToString());
                    audioMeterInfo = session.AudioMeterInformation;
                }
                //Console.WriteLine(session.DisplayName);
            }
            if (audioMeterInfo == null) {
                throw new Exception("eh gak detek??");
            }
            while (true) {
                Console.Write("How many? ");
                int many = int.Parse(Console.ReadLine());
                Console.Write("Starting in 3 seconds.");
                Thread.Sleep(1000);
                Console.Write(".");
                Thread.Sleep(1000);
                Console.Write(".");
                Thread.Sleep(1000);
                //Console.CancelKeyPress += new ConsoleCancelEventHandler(interrupt_handler);
                Console.WriteLine(" Started!");
                var gotPoint = GetCursorPos(out currentMousePoint);
                float currentVolume = 0;
                DateTime lastMancing = DateTime.Now;
                while (many != 0) {
                    currentVolume = audioMeterInfo.MasterPeakValue;

                    if (currentVolume > 0.1) {
                        gotchaCount++;
                        Console.WriteLine("Gotcha! " + gotchaCount.ToString());
                        doMancing();
                        lastMancing = DateTime.Now;
                        many--;
                    }
                    DateTime sekarang = DateTime.Now;
                    if ((sekarang - lastMancing).TotalSeconds > 30) {
                        Console.WriteLine("So quiet?");
                        doMancing();
                        Thread.Sleep(200);
                        lastMancing = sekarang;
                    }
                    Thread.Sleep(100);
                }
                Console.WriteLine("\b=== DONE! ===");
            }
        }

        protected static void interrupt_handler(object sender, ConsoleCancelEventArgs args) {
            Console.WriteLine("App interrupted!");
            Console.WriteLine("Gotcha count : " + gotchaCount.ToString());
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MousePoint {
            public int X;
            public int Y;

            public MousePoint(int x, int y) {
                X = x;
                Y = y;
            }
        }

        public static void mouse(MouseEventFlags value) {
            mouse_event((int)value, currentMousePoint.X, currentMousePoint.Y, 0, 0);
        }
    }
}
