using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using log4net;
using System.Windows;
using System.Windows.Input;

namespace MediaGadget.Model
{
    class MediaModel : INotifyPropertyChanged {
        ILog log = LogManager.GetLogger("MediaModel|");
        //protected IKeyboard keyboard;
        protected KeyboardSimulator simulator;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        /**
        public MediaModel(IKeyboard keyboard)
        {
            this.keyboard = keyboard;
        }

        public MediaModel() {
            this.keyboard = IKeyboard.GetDefaultDevice();
        }*/
        public MediaModel() {
            simulator = new KeyboardSimulator();
		}

        /*
         * Keyboard Media Buttons
         * Have the VM call these onbuttonpress
         * TODO have these return bindings
         * ViewModel should see these, call onbuttonpress
         */
        public void MediaPressPlayPause() {
            log.Info("Play/Pause sent.");
            OnPropertyChanged("PlayPauseButton");
            KeyboardEvent.keybd_event(KeyboardEvent.VK_MEDIA_PLAY_PAUSE, 0, KeyboardEvent.KEYEVENTF_EXTENDEDKEY, IntPtr.Zero);
        }

        public void MediaPressNext() {
            log.Info("Next sent.");
            OnPropertyChanged("NextButton");
            KeyboardEvent.keybd_event(KeyboardEvent.VK_MEDIA_NEXT_TRACK, 0, KeyboardEvent.KEYEVENTF_EXTENDEDKEY, IntPtr.Zero);
        }

        public void MediaPressPrev() {
            log.Info("Prev sent.");
            OnPropertyChanged("PrevButton");
            KeyboardEvent.keybd_event(KeyboardEvent.VK_MEDIA_PREV_TRACK, 0, KeyboardEvent.KEYEVENTF_EXTENDEDKEY, IntPtr.Zero);
        }

        public void MediaPressStop() {
            log.Info("Stop Sent");
            OnPropertyChanged("StopButton");
            KeyboardEvent.keybd_event(KeyboardEvent.VK_MEDIA_STOP, 0, KeyboardEvent.KEYEVENTF_EXTENDEDKEY, IntPtr.Zero);
        }
    }
}
