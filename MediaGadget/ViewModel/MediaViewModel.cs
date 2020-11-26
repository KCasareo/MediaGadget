using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MediaGadget.ViewModel {
	class MediaViewModel {
		// Based on https://scottlilly.com/c-design-patterns-mvvm-model-view-viewmodel/
		// Don't want model object to be modified by view at least. Set to private if get is undesired.
		// There will only ever be one media model writing to winapi for the mediaviewmodel
		private readonly Model.MediaModel _mediaModel;

		private bool canExecute = true;
		private bool _playing;
		public bool playing {
			get { return _playing; }
			// Only the viewmodel should modify this.
			private set { _playing = value; }
		}

		// If the view is wholly decoupled, treat the viewmodel as an entrypoint and have it determine dependencies.
		public MediaViewModel() {
			_mediaModel = new Model.MediaModel();

			// Last worked on 19/10/2020
			// Figure out how to link view to viewmodel
			// Commands
			// TODO: make each extend ICommand instead of relaycommand
			nextCommand = new ButtonCommand(Next, param => canExecute);
			prevCommand = new ButtonCommand(Prev);
			stopCommand = new ButtonCommand(Stop);
			playPauseCommand = new ToggleButtonCommand(PlayPause);

		}
		// Media state
		// Populate audio streams < audio stream collection
		//		getset
		//			modify volume controls individually
		// Commands to use
		private void Next(object obj) {
			_mediaModel.MediaPressNext();
			// call obj code behind to modify the button
		}

		private void Prev(object obj) {
			_mediaModel.MediaPressPrev();
		}

		private void Stop(object obj) {
			_mediaModel.MediaPressStop();
			playing = false;
		}

		private void PlayPause(object obj) {
			_mediaModel.MediaPressPlayPause();
			playing = !playing;
			
		}
		// 
		private ButtonCommand nextCommand;
		private ButtonCommand prevCommand;
		private ButtonCommand stopCommand;
		private ButtonCommand playPauseCommand;
		// Commands, get called by ICommand . execute()
		public ButtonCommand NextCommand {
			get {
				return nextCommand;
			}
			set {
				nextCommand = value;
			}
		}
		public ButtonCommand PrevCommand {
			get {
				return prevCommand;
			}
			set {
				prevCommand = value;
			}
		}
		public ButtonCommand StopCommand {
			get {
				return stopCommand;
			}
			set {
				stopCommand = value;
			}
		}
		public ButtonCommand PlayPauseCommand {
			get {
				return playPauseCommand;
			}
			set {
				playPauseCommand = value;
			}
		}
	}

	}

	class ToggleButtonCommand : ButtonCommand {

		protected Predicate<object> canToggle;
		public ToggleButtonCommand(Action<object> execute) : this(execute, DefaultCanExecute, DefaultCanToggle) {

		}

		public ToggleButtonCommand(Action<object> execute, Predicate<object> canExecute) : this(execute, canExecute, DefaultCanToggle) {

		}

		public ToggleButtonCommand(Action<object> execute, Predicate<object> canExecute, Predicate<object> canToggle) : base(execute, canExecute) {
			this.canToggle = canToggle ?? throw new ArgumentNullException("canToggle");
		}

		private static bool DefaultCanToggle(object parameter) {
			return false;
		}

		// Overload
		public new void Execute(object parameter) {
			base.execute(parameter);
			
		}
	}

	class ButtonCommand : ICommand {
		#region

		#endregion
		// OnMouseDown

		protected Action<object> execute;
		// Eg typeof button

		protected Predicate<object> canExecute;

		//

		private event EventHandler CanExecuteChangedInternal;

		public ButtonCommand(Action<object> execute) : this(execute, DefaultCanExecute) { }

		public ButtonCommand(Action<object> execute, 
							Predicate<object> canExecute) {
			this.execute = execute ?? throw new ArgumentNullException("execute");
			this.canExecute = canExecute ?? throw new ArgumentNullException("canExecute");
		}


		// use for stop button activation.
		public event EventHandler CanExecuteChanged {
			add {
				CommandManager.RequerySuggested += value;
				this.CanExecuteChangedInternal += value;
			}
			remove {
				CommandManager.RequerySuggested -= value;
				this.CanExecuteChangedInternal -= value;
			}
		}

		public bool CanExecute(object parameter) {
			return this.canExecute != null && this.canExecute(parameter);
		}

		public void Execute(object parameter) {
			this.execute(parameter);
		}

		public void Destroy() {
			this.canExecute = _ => false;
			this.execute = _ => { return; };
		}

		protected static bool DefaultCanExecute(object parameter) {
			return true;
		}


	}
}
