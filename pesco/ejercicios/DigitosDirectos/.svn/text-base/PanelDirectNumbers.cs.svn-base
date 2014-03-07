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
using Gtk;
using Gdk;
using System;

namespace pesco
{
	
	[System.ComponentModel.ToolboxItem(true)]
	public partial class PanelDirectNumbers : Gtk.Bin
	{
		
		public Gtk.Label LabelNumero {
			get {
				return this.labelNumero;	
			}
			set {
				this.labelNumero = value;
			}
		}		
		
		public PanelDirectNumbers ()
		{
			this.Build ();
			for ( int i = 0; i < tableButtons.Children.Length; i++ ) {				
				GtkUtil.SetStyle(tableButtons.Children[i], Configuration.Current.ExtraLargeFont);
			}
			GtkUtil.SetStyle( buttonReady, Configuration.Current.HugeFont );			
		}
	
		public void setLabelNumero ( string texto ) {
			this.labelNumero.Markup = "<span color=\"white\" font_desc=\"Ahafoni CLM Bold 120\">"+texto+"</span>";
		}
		
		protected virtual void numberButtonClicked (object sender, System.EventArgs e)
		{			
			EnableButtons();
			ExerciseDirectNumbers edd = ExerciseDirectNumbers.getInstance();			
			Gtk.Button aux = (Gtk.Button) sender;
			// setLabelNumero( aux.Label[0].ToString() );
			// Label for delete is "Borrar", so char B has to be considered
			edd.NumberPressed( aux.Label[0] );
			if ( aux.Label[0] != 'B' )
				aux.Sensitive = false;
		}
		
		public void HideButtonsShowNumbers( string texto ) {
			
			vboxReady.HideAll();
			Gdk.Pixmap inv = new Gdk.Pixmap (null, 1, 1, 1);
			Gdk.Cursor invisibleCursor = new Cursor (inv, inv, Gdk.Color.Zero,	Gdk.Color.Zero, 0, 0);
			this.GdkWindow.Cursor = invisibleCursor;
			this.GdkWindow.Display.Sync();			
			setLabelNumero( texto );
			
		}
		
		public void ShowButtonStartSequence(bool firstTime) {
			
			tableButtons.HideAll();
			
			vboxReady.ShowAll();
			// If it is first time, we'll hide "Nueva secuencia" label
			if ( firstTime ) {
				labelNewSequence.HideAll();	
			}
			HideBlackboard();
						
		}

		public void HideBlackboard() {
			fixedBlackboard.HideAll();
		}
		
		public void ShowBlackboard() {
			fixedBlackboard.ShowAll();
		}
		
		public void UpdateLabelWithSequence() {
			
			ExerciseDirectNumbers edd = ExerciseDirectNumbers.getInstance();
			string stringToShow = "";
			
			for ( int i = 0; i < edd.CurrentSequence.Length; i++ ) {
				if ( edd.CurrentPosition > i ) {
					stringToShow += "☑";
				} else {
					stringToShow += "☐";	
				}
			}
			
			if ( edd.CurrentSequence.Length < 3 ) {
				LabelNumero.Markup = "<span color=\"white\" font_desc=\"Ahafoni CLM Bold 120\">"+stringToShow+"</span>";
			} else if ( edd.CurrentSequence.Length < 5 ) {
				LabelNumero.Markup = "<span color=\"white\" font_desc=\"Ahafoni CLM Bold 70\">"+stringToShow+"</span>";
			} else if ( edd.CurrentSequence.Length < 7 ){
				LabelNumero.Markup = "<span color=\"white\" font_desc=\"Ahafoni CLM Bold 55\">"+stringToShow+"</span>";	
			} else {
				LabelNumero.Markup = "<span color=\"white\" font_desc=\"Ahafoni CLM Bold 36\">"+stringToShow+"</span>";
			}
			
		}
		public bool HideNumbersShowButtons() {
			this.GdkWindow.Cursor = null;
			this.GdkWindow.Display.Sync();
			
			UpdateLabelWithSequence();
			
			tableButtons.ShowAll();
			return false;
			
		}
		
		public void DisableButtons() {
			
			this.tableButtons.Sensitive = false;
		
		}
		
		public void EnableButtons() {
			this.tableButtons.Sensitive = true;
			for ( int i = 0; i < this.tableButtons.Children.Length; i++ ) {			
				this.tableButtons.Children[i].Sensitive = true;
			}
		}
		
		protected virtual void buttonReadyClicked (object sender, System.EventArgs e)
		{
			ExerciseDirectNumbers.getInstance().NextSequence();
		}
		
	}
}
