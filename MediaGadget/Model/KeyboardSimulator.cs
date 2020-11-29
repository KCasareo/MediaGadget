using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;

namespace MediaGadget.Model {
	class KeyboardSimulator {

		public KeyboardSimulator() {
			
		}

		public KeyboardSimulator(DependencyObject dependency) {
		}

		public void SendKey(Key key) {
			/**
			var key = Key.Insert;
			var target = Keyboard.FocusedElement;
			var routedEvent = Keyboard.KeyDownEvent;
			uses https://stackoverflow.com/questions/10820990/how-to-create-a-keyeventargs-object-in-wpf-related-to-a-so-answer
			Solution does not work since PresentationForm is evaluating to null
			Thought that the media inputs were being sent to the app instead of to the system, but ProcessInput never returns true.
			If this is to work, this object probably needs a handle to the main window.

			*/

			var target = Keyboard.FocusedElement;
			if (InputManager.Current.ProcessInput(
				new KeyEventArgs(
					Keyboard.PrimaryDevice,
					new HwndSource(0, 0, 0, 0, 0, "", IntPtr.Zero),
					0,
					key
				) { RoutedEvent = Keyboard.KeyDownEvent }
			)) {
				MessageBox.Show("Key Processed");
			}
			
				
		}
	}
}
