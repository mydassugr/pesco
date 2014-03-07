/**

Copyright 2011 Grupo de Investigación GEDES
Lenguajes y sistemas informáticos
Universidad de Granada

Licensed under the EUPL, Version 1.1 or – as soon they 
will be approved by the European Commission - subsequent  
versions of the EUPL (the "Licence"); 
You may not use this work except in compliance with the 
Licence. 
You may obtain a copy of the Licence at: 

http://ec.europa.eu/idabc/eupl  

Unless required by applicable law or agreed to in 
writing, software distributed under the Licence is 
distributed on an "AS IS" basis, 
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either 
express or implied. 
See the Licence for the specific language governing 
permissions and limitations under the Licence. 



*/
using System;
namespace pesco
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class LI_ScenePanel : Gtk.Bin
	{
		
		private LI_RoomPanel currentRoomPanel = null;
		private LI_HallPanel currentHallPanel = null;
		private LIExercise exerciseInstance = null;
		
		/* public Gtk.HBox HboxDebug {
			get {
				return this.hboxDebug;
			}
		}*/
		
		public Gtk.HBox HboxDemo {
			get {
				return this.hboxDemo;
			}
		}
		
		public Gtk.HBox HboxGameButtons {
			get {
				return this.hboxGameButtons;
			}
		}
		
		public Gtk.Button ButtonGoBack {
			get {
				return this.buttonGoBack;
			}
		}

		public Gtk.Button ButtonGoLast {
			get {
				return this.buttonGoLast;
			}
		}
		
		public Gtk.Button ButtonGoForward {
			get {
				return this.buttonGoForward;
			}
		}
		
		public Gtk.Button ButtonStartExercise {
			get {
				return this.buttonStartExercise;
			}
		}
		
		public Gtk.Button ButtonFinishExercise {
			get {
				return this.buttonFinishExercise;
			}
		}
		
		public Gtk.Image ImageBackground {
		
			get {
				return this.imageBackground;	
			}
			
		}
		
		
		/* public Gtk.Label LabelInfo {
			get {
				return this.labelInfo;	
			}		
		}*/		
		
		public Gtk.EventBox EventBoxBackground {
			get {
				return eventboxImage;
			}
		}		
		
		public LI_ScenePanel ()
		{
			this.Build ();
			GtkUtil.SetStyle( buttonGoBack, Configuration.Current.MediumFont );
			GtkUtil.SetStyle( buttonGoForward, Configuration.Current.MediumFont );
			GtkUtil.SetStyle( buttonGoLast, Configuration.Current.MediumFont );
			GtkUtil.SetStyle( buttonStartExercise, Configuration.Current.MediumFont );
			// hboxDebug.HideAll();
			hboxGameButtons.HideAll();
			GtkUtil.SetStyle( buttonFinishExercise, Configuration.Current.MediumFont );
		}
		
		public LI_ScenePanel ( LIExercise instance )
		{
			this.Build ();
			GtkUtil.SetStyle( buttonGoBack, Configuration.Current.MediumFont );
			GtkUtil.SetStyle( buttonGoForward, Configuration.Current.MediumFont );
			GtkUtil.SetStyle( buttonGoLast, Configuration.Current.MediumFont );
			GtkUtil.SetStyle( buttonStartExercise, Configuration.Current.MediumFont );			
			GtkUtil.SetStyle( buttonFinishExercise, Configuration.Current.MediumFont );
			exerciseInstance = instance;
			// hboxDebug.HideAll();
			hboxGameButtons.HideAll();
		}
		
/*		public void ReplaceRoom ( LI_RoomPanel replace ) {
		
			if ( currentRoomPanel != null ) {
				fixedRoom.Remove(currentRoomPanel);								
			}
			
			if ( currentHallPanel != null ) {
				fixedRoom.Remove(currentHallPanel);	
				currentHallPanel = null;
			}
			
			fixedRoom.Put( replace, 0, 0 );
			currentRoomPanel = replace;
		}
		
		public void ShowHall ( LI_HallPanel hallPanel ) {
			
			if ( currentRoomPanel != null ) {
				fixedRoom.Remove(currentRoomPanel);								
			}
			
			fixedRoom.Put( hallPanel, 0, 0 );
			currentHallPanel = hallPanel;
			
		}
		*/
		
		protected virtual void OnSpinbutton1ValueChanged (object sender, System.EventArgs e)
		{
			exerciseInstance.CurrentRoom = (int) ((Gtk.SpinButton) sender).Value;
		}
		
		protected virtual void OnTogglebutton1Clicked (object sender, System.EventArgs e)
		{
			if ( ( (Gtk.ToggleButton) sender).Active )
				exerciseInstance.DebugMode = true;
			else
				exerciseInstance.DebugMode = false;
		}
		
		protected virtual void buttonFinishExerciseCLicked (object sender, System.EventArgs e)
		{
			// Confirm the finish action
			Gtk.MessageDialog dialog = new Gtk.MessageDialog (	null,
				Gtk.DialogFlags.Modal,
				Gtk.MessageType.Question,
				Gtk.ButtonsType.YesNo,
				"<span size=\"xx-large\">¿Ya has colocado todos los objetos y encontrado todas las monedas?</span>",
				"Ejercicio terminado"
			);
			GtkUtil.SetStyleRecursive( dialog, Configuration.Current.LargeFont );
			int result = dialog.Run ();
			
			if ( result == (int) Gtk.ResponseType.Yes )
			{	
				exerciseInstance.FinishExercise();
			}
			dialog.Destroy();
		}
		
		protected virtual void GoForward (object sender, System.EventArgs e)
		{			
			exerciseInstance.NextStep();
		}
		
		protected virtual void OnButtonBackwardClicked (object sender, System.EventArgs e)
		{
			exerciseInstance.BackStep();
		}
		
		protected virtual void OnButtonStartExerciseClicked (object sender, System.EventArgs e)
		{			
			exerciseInstance.InitGame();
		}
		
	}
}


