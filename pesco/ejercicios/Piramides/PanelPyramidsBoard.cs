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
using Gtk;

namespace pesco
{
	
	[System.ComponentModel.ToolboxItem(true)]
	public partial class PanelPyramidsBoard : Gtk.Bin
	{
		
		#region Parent window size
		int PARENT_WIDTH;
		int PARENT_HEIGHT;
		int FIXEDLAYOUT_WIDTH;
		int BOARD_POS_X;
		const int BOARD_WIDTH = 936;
		#endregion
		
		#region Constantes del ejercicio
		// private int secondsPerPanel = 20;
		#endregion
		
		#region Elementos del panel
		private int timeLeft;
		private int pyramidsSelected;
		private Gtk.Table currentTable = null;
		#endregion
		
		#region Variables temporizadores
		private int auxAnimationTime;
		private uint auxTimer;
		private GLib.TimeoutHandler	currentHandler;
		private uint currentInterval;
		private bool pause;
		#endregion
						
		public PanelPyramidsBoard ()
		{
			this.Build ();
			labelTimeLeft.Hide();
			timeLeft = 3;
			pyramidsSelected = 0;
			labelTimeLeft.ModifyFont( Pango.FontDescription.FromString( "Sans 30" ));
			labelPyramidsSelected.ModifyFont( Pango.FontDescription.FromString( "Sans 30" ));
		}
		
		public void addPiramides ( Table table ) {
			
			// first run
			if ( currentTable == null ) {
				currentTable = table;
				FirstRun();				
				fixedLayout.Put( this.currentTable, (PARENT_WIDTH-BOARD_WIDTH)/2, 0 );
				ShowAll();
				StartTimer();		
			} 
			// rest
			else {				
				fixedLayout.Remove( currentTable );
				currentTable = table;
				currentTable.Sensitive = false;
				auxAnimationTime = 0;
				fixedLayout.Put( currentTable, PARENT_WIDTH, 0 );											
				currentHandler = new GLib.TimeoutHandler (ScrollInAnimation);
				currentInterval = 25;
				auxTimer = GLib.Timeout.Add (currentInterval, currentHandler);
				ShowAll();
				HideTimer();				
			}
		}

		public void FirstRun() {
			SetSelectedPyramids(0);
			vbox2.ParentWindow.GetSize( out PARENT_WIDTH, out PARENT_HEIGHT );
			this.SetSizeRequest( PARENT_WIDTH, PARENT_HEIGHT);
			BOARD_POS_X = (PARENT_WIDTH - BOARD_WIDTH) / 2;
			Gdk.Pixbuf auxPixbuf = 	Gdk.Pixbuf.LoadFromResource( "pesco.ejercicios.Piramides.img.background.png" );
			auxPixbuf = auxPixbuf.ScaleSimple( PARENT_WIDTH, PARENT_HEIGHT, Gdk.InterpType.Bilinear );
			fixedLayout.Put( new Gtk.Image(auxPixbuf),0,0 );
		}
		
		public void SetSelectedPyramids ( int value ) {
			this.pyramidsSelected = value;
			// Commented to don't show number of selected pyramids
			// if ( value != 0 ) {
			//	this.labelPyramidsSelected.Text = value + " pirámides seleccionadas";
			// } else {
				this.labelPyramidsSelected.Markup = "<span color=\"red\">¡Rápido!</span>¡Pulsa las postales correctas!";
			// }
		}
		
		public int GetSelectedPyramids() {
			return this.pyramidsSelected;	
		}
		
		public void HideTimer() {
			this.labelTimeLeft.Hide();
		}
		
		public void ShowTimer() {
			this.labelTimeLeft.Show();	
		}
		
		public void SetTimeLeft ( int value ) {
			this.timeLeft = value;
			if ( value == 1 )
				this.labelTimeLeft.Text = value + " segundo restante";
			else
				this.labelTimeLeft.Text = value + " segundos restantes";
		}
		
		public int GetTimeLeft () {
			return this.timeLeft;	
		}
		
		public void StartTimer () {			
			currentHandler = new GLib.TimeoutHandler (UpdateTimeLeft);
			currentInterval = 1000;
			auxTimer = GLib.Timeout.Add (1000, new GLib.TimeoutHandler (UpdateTimeLeft));
		}
		
		private bool UpdateTimeLeft() {
			SetTimeLeft( timeLeft - 1 );
			if ( timeLeft == 0 ) {
				this.auxAnimationTime = 0;
				currentHandler = new GLib.TimeoutHandler (ScrollOutAnimation);
				currentInterval = 25;
				auxTimer = GLib.Timeout.Add (25, new GLib.TimeoutHandler (ScrollOutAnimation));
				this.currentTable.Sensitive = false;
				return false;
			}
			return true;
		}
				
		public bool ScrollOutAnimation() {
						
			this.auxAnimationTime += 25;
			// Panel is not scrolled anymore, now it is hide at the end of time
			// this.fixedLayout.Move( this.currentTable, (int) (BOARD_POS_X - (BOARD_POS_X+BOARD_WIDTH) * (this.auxAnimationTime / 1000.0)), 0 );
			if ( auxAnimationTime == 1000 ) {
				this.Visible = false;
				HideTimer();
				this.auxTimer = 0;
				FinishBoard();
				return false;
			}
			
			
			return true;
		}
		
		public bool ScrollInAnimation() {

			this.auxAnimationTime += 50;
			// this.fixedLayout.Move( this.currentTable, PARENT_WIDTH - (int) ((BOARD_POS_X+BOARD_WIDTH)*(this.auxAnimationTime/1000.0)), 0 );
			if ( auxAnimationTime == 1000 ) {
				this.fixedLayout.Move( this.currentTable, PARENT_WIDTH - (int) ((BOARD_POS_X+BOARD_WIDTH)*(this.auxAnimationTime/1000.0)), 0 );
				this.auxTimer = 0;
				SetTimeLeft(EjercicioPiramides.getInstance().SecondsPerPanel);
				ShowTimer();
				SetSelectedPyramids(0);
				StartTimer();
				this.currentTable.Sensitive = true;
				return false;
			}
			return true;
		}
		
		public void FinishBoard() {
			
			EjercicioPiramides.getInstance().RepetionFinished();
				
		}

		public bool ExercisePaused() {
		
			return pause;
			
		}
		
		public void PauseExercise() {
		
			if ( !pause ) {
				pause = true;
				GLib.Source.Remove( auxTimer );
			} else {
				pause = false;
				auxTimer = GLib.Timeout.Add( currentInterval, currentHandler );	
			}
			
			
		}
		
	}
}
	
