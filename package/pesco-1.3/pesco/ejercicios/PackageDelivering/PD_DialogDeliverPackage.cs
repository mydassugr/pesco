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


	public partial class PD_DialogDeliverPackage : Gtk.Dialog
	{

		private PanelPackageDelivering panelInstance = null;
		
		public PD_DialogDeliverPackage ()
		{
			this.Build ();
		}
		
		public PD_DialogDeliverPackage( PanelPackageDelivering panel ) {
			
			this.Build();
			GtkUtil.SetStyle( labelQuestion, Configuration.Current.MediumFont );
			this.panelInstance = panel;
			for ( int i = 0; i < panel.Car.Items.Count; i++ ) {
				Gtk.Button auxButton = new Gtk.Button( 
					new Gtk.Image( panelInstance.CurrentScene.BoxesStatus[panel.Car.Items[i]].PixbufBoxBig) );
				auxButton.Name = panel.Car.Items[i].ToString();
				auxButton.Clicked += DeliverCallback;
				hboxBoxes.Add( auxButton );
				auxButton.ShowAll();
			}			
		}
		
		protected virtual void DeliverCallback (object sender, System.EventArgs e) {
			panelInstance.DeliverPackage( Int32.Parse( ((Gtk.Button)sender).Name ) );
			this.Destroy();
		}
		
		protected virtual void cancelCallback (object sender, System.EventArgs e)
		{
			this.Destroy();
		}
		
		
	}
}

