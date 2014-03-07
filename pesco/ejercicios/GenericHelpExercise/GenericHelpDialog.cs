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
using System.Collections.Generic;

namespace pesco
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class GenericHelpDialog : Gtk.Bin
	{
		
		public DialogPanel Dialog {
			get{
				return dialogPanel;	
			}
		}

		protected virtual void buttonClicked (object sender, System.EventArgs e)
		{
			
			if ( Dialog.CurrentStep == Dialog.TextsToShow.Count - 1 ) {
				SessionManager.GetInstance().ExerciseFinished(-1);
				SessionManager.GetInstance().TakeControl();
				this.Destroy();
			} else {
				Dialog.NextStep();	
			}
			
		}
		
		public GenericHelpDialog ()
		{
			this.Build ();
		}

		public GenericHelpDialog (List <string> texts)
		{
			this.Build ();
			dialogPanel.SetText( texts );
			dialogPanel.InitPanel();
			
			GtkUtil.SetStyle(this.buttonForward, Configuration.Current.ButtonFont);			
		}
	}
}


