﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace CartonWPFHost {
    internal class GameHost : HwndHost {
        private const int WS_CHILD = 0x40000000;
        private const int WS_VISIBLE = 0x10000000;
        private const int HOST_ID = 0x00000002;

        private IntPtr hwndHost;
        private int hostHeight, hostWidth;

        private Process process;
        private IntPtr unityHWND = IntPtr.Zero;

        public GameHost(double width, double height) {
            hostHeight = (int)height;
            hostWidth = (int)width;
        }


        protected override HandleRef BuildWindowCore(HandleRef hwndParent) {
            hwndHost = IntPtr.Zero;

            hwndHost = CreateWindowEx(0, "static", "",
                                      WS_CHILD | WS_VISIBLE,
                                      0, 0,
                                      hostWidth, hostHeight,
                                      hwndParent.Handle,
                                      (IntPtr)HOST_ID,
                                      IntPtr.Zero,
                                      0);

            {
                try {
                    process = new Process();
                    process.StartInfo.FileName = "C:/Projects/CartonVisualizer/Builds/Game.exe";
                    process.StartInfo.Arguments = "-parentHWND " + hwndHost.ToInt32() + " " + Environment.CommandLine;
                    process.StartInfo.UseShellExecute = true;
                    process.StartInfo.CreateNoWindow = true;

                    process.Start();

                    process.WaitForInputIdle();
                    // Doesn't work for some reason ?!
                    //unityHWND = process.MainWindowHandle;
                    EnumChildWindows(hwndHost, WindowEnum, IntPtr.Zero);
                }
                catch (Exception ex) {
                    MessageBox.Show(ex.Message + "GameHost: Could not find game executable.");
                }
            }

            return new HandleRef(this, hwndHost);
        }

        protected override void DestroyWindowCore(HandleRef hwnd) {
            try {
                process.CloseMainWindow();

                System.Threading.Thread.Sleep(1000);
                while (process.HasExited == false)
                    process.Kill();
            }
            catch (Exception) {

            }

            DestroyWindow(hwnd.Handle);
        }

        protected override IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled) {
            handled = false;
            return IntPtr.Zero;
        }

        private int WindowEnum(IntPtr hwnd, IntPtr lparam) {
            unityHWND = hwnd;
            return 0;
        }

        //PInvoke declarations
        [DllImport("user32.dll", EntryPoint = "CreateWindowEx", CharSet = CharSet.Unicode)]
        internal static extern IntPtr CreateWindowEx(int dwExStyle,
                                                      string lpszClassName,
                                                      string lpszWindowName,
                                                      int style,
                                                      int x, int y,
                                                      int width, int height,
                                                      IntPtr hwndParent,
                                                      IntPtr hMenu,
                                                      IntPtr hInst,
                                                      [MarshalAs(UnmanagedType.AsAny)] object pvParam);

        [DllImport("user32.dll", EntryPoint = "DestroyWindow", CharSet = CharSet.Unicode)]
        internal static extern bool DestroyWindow(IntPtr hwnd);

        [DllImport("User32.dll")]
        static extern bool MoveWindow(IntPtr handle, int x, int y, int width, int height, bool redraw);

        internal delegate int WindowEnumProc(IntPtr hwnd, IntPtr lparam);
        [DllImport("user32.dll")]
        internal static extern bool EnumChildWindows(IntPtr hwnd, WindowEnumProc func, IntPtr lParam);
    }
}