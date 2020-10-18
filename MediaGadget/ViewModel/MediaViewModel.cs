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
		private Model.MediaModel _mediaModel;

		private bool canExecute = true;
		private bool playing = false;

		// If the view is wholly decoupled, treat the viewmodel as an entrypoint and have it determine dependencies.
		public MediaViewModel() {
			_mediaModel = new Model.MediaModel();

			// Last worked on 19/10/2020
			// Figure out how to link view to viewmodel
			// Commands
			NextCommand = new RelayCommand(Next, param => this.playing && this.canExecute);
			PrevCommand = new RelayCommand(Prev, param => this.playing && this.canExecute);
			StopCommand = new RelayCommand(Stop, param => this.playing && this.canExecute);
			
		}
		// Media state
		// Populate audio streams < audio stream collection
		//		getset
		//			modify volume controls individually
		// Bindings
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
			playing = true;
		}
		// 
		private RelayCommand nextCommand;
		private RelayCommand prevCommand;
		private RelayCommand stopCommand;
		private RelayCommand playCommand;
		// Commands
		public RelayCommand NextCommand {
			get {
				return nextCommand;
			}
			set {
				nextCommand = value;
			}
		}
		public RelayCommand PrevCommand {
			get;
			private set;
		}
		public RelayCommand StopCommand {
			get;
			private set;
		}
		public RelayCommand PlayPauseCommand {
			get;
			private set;
		}

	}

	class RelayCommand : ICommand {
		#region

		#endregion
		// OnMouseDown
		private Action<object> execute;
		// Eg typeof button
		private Predicate<object> canExecute;

		private event EventHandler CanExecuteChangedInternal;

		public RelayCommand(Action<object> execute) : this(execute, DefaultCanExecute) {

		}

		public RelayCommand(Action<object> execute, Predicate<object> canExecute) {
			if (execute == null) {
				throw new ArgumentNullException("execute");
			}
			if (canExecute == null) {
				throw new ArgumentNullException("canExecute");
			}
			this.execute = execute;
			this.canExecute = canExecute;
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

		private static bool DefaultCanExecute(object parameter) {
			return true;
		}
	}
}
