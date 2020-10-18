using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


namespace MediaGadget.Model
{
    class MediaModel : INotifyPropertyChanged {   
        protected IKeyboard keyboard;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName) {

        }

        public MediaModel(IKeyboard keyboard)
        {
            this.keyboard = keyboard;
        }

        public MediaModel() {
            this.keyboard = IKeyboard.GetDefaultDevice();
        }


        /*
         * Keyboard Media Buttons
         * Have the VM call these onbuttonpress
         * TODO have these return bindings
         * ViewModel should see these, call onbuttonpress
         */
        public void MediaPressPlayPause() {
            keyboard.Send(VInput.ScanCodeShort.MEDIA_PLAY_PAUSE);
        }

        public void MediaPressNext() {
            keyboard.Send(VInput.ScanCodeShort.MEDIA_NEXT_TRACK);
        }

        public void MediaPressPrev() {
            keyboard.Send(VInput.ScanCodeShort.MEDIA_PREV_TRACK);
        }

        public void MediaPressStop() {
            keyboard.Send(VInput.ScanCodeShort.MEDIA_STOP);
        }
    }
}
