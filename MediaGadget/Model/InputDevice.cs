
using MediaGadget.Model;
using MediaGadget.VInput;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

/*
* Virtual Input based on LuvForAirplanes answer in: https://stackoverflow.com/questions/20482338/simulate-keyboard-input-in-c-sharp
*     
*     
*/

namespace MediaGadget.Model
{    
    /* Probably no longer needed. */
    public interface IDevice
    {

    }

    public abstract class IKeyboard : IDevice
    {
        protected IKeyboard() { }

        public abstract void Send(ScanCodeShort scanCodeShort);

        public static IKeyboard GetDefaultDevice()
        {
            return new Keyboard();
        }

        private class Keyboard : IKeyboard
        {
            [DllImport("user32.dll")]
            internal static extern uint SendInput(
                uint nInputs,
                [MarshalAs(UnmanagedType.LPArray), In] INPUT[] pInputs,
                int cbSize);

            List<INPUT> InputBuffer = new List<INPUT>();

            public override void Send(ScanCodeShort scanCodeShort)
            {
                InputBuffer.Add(generateInputFrame(scanCodeShort));
                SendInput(1, InputBuffer.ToArray(), INPUT.Size);
            }

            private INPUT generateInputFrame(ScanCodeShort scanCodeShort)
            {
                INPUT frame = new INPUT();
                frame.type = 1;
                frame.U.ki.wScan = scanCodeShort;
                frame.U.ki.dwFlags = KEYEVENTF.SCANCODE;
                return frame;
            }

        }
    }
    public abstract class IMouse : IDevice
    {
        
        protected IMouse() { }


        public static IMouse GetDefaultDevice()
        {
            return new Mouse();
        }


        private class Mouse : IMouse
        {
            [System.Runtime.InteropServices.DllImport("user32.dll")]
            public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
            [System.Runtime.InteropServices.DllImport("user32.dll")]
            public static extern bool ReleaseCapture();


        }

    }

        

    
}