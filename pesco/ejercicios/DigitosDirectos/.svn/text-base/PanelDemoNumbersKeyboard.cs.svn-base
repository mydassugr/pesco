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
	public partial class PanelDemoNumbersKeyboard : Gtk.Bin
	{

		private PanelDemoNumbers parentPanel;
		
		public PanelDemoNumbersKeyboard ( PanelDemoNumbers parent )
		{
			this.Build ();
			
			this.parentPanel = parent;
			
			Gtk.Button auxButton;
			Gtk.Label auxLabel;
				
			for ( uint i = 0; i < 9; i++ ) {			
				auxLabel = new Gtk.Label();
				auxLabel.UseMarkup = true;
				auxLabel.Markup = "<span font_desc=\"Ahafoni CLM Bold 50\">"+(i+1)+"</span>";
				auxButton = new Gtk.Button( auxLabel );				
				auxButton.Name = (i+1).ToString();	
				auxButton.Clicked += delegate(object sender, EventArgs e) {
					parentPanel.buttonClicked( ( ( Gtk.Button) sender).Name );
				};
				tableNumbers.Attach( auxButton,  i % 3, (i % 3) + 1, i / 3, (i / 3) + 1 );
			}
			auxLabel = new Gtk.Label();
			auxLabel.UseMarkup = true;
			auxLabel.Markup = "<span font_desc=\"Ahafoni CLM Bold 50\">0</span>";
			auxButton = new Gtk.Button( auxLabel );
			auxButton.Name = ( (int) 0 ).ToString();
			auxButton.Clicked += delegate(object sender, EventArgs e) {
					parentPanel.buttonClicked( ( ( Gtk.Button) sender).Name );
				};
			tableNumbers.Attach( auxButton,  1, 2, 3, 4 );
			
		}
	}
}

