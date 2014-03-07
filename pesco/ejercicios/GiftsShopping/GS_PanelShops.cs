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
	public partial class GS_PanelShops : Gtk.Bin
	{

		public GS_PanelShops ()
		{
			this.Build ();
		}
		
		public void AddSituation(GS_Situation auxSituation) {
			
			for ( int i = 0; i < table.Children.Length; i++ ) {
				table.Children[i].Destroy();
			}

			for (int i = 0; i < auxSituation.Shops.Count; i++ ) {
							
				Gtk.Button auxButton = new Gtk.Button();
				auxButton.Name = i.ToString();
				
				auxButton.Add( new Gtk.VBox(false,0) );
				
				Gtk.Image auxImageShop = new Gtk.Image(Gdk.Pixbuf.LoadFromResource(auxSituation.Shops[i].Img).ScaleSimple(75,75,Gdk.InterpType.Bilinear) );
				( (Gtk.VBox) auxButton.Children[0]).PackStart( auxImageShop );
				
				Gtk.Label auxLabel = new Gtk.Label( auxSituation.Shops[i].Name );
				GtkUtil.SetStyle( auxLabel, Configuration.Current.MediumFont );
				
				( (Gtk.VBox) auxButton.Children[0]).PackStart( auxLabel  );
				
				auxButton.Clicked += delegate(object sender, EventArgs e) {
					ExerciseGiftsShopping.getInstance().ShowItemsShop( Convert.ToInt32(((Gtk.Button) sender).Name) );
				};
				
				table.Attach( auxButton, (uint) i % 4, (uint) (i % 4)+1, (uint) i / 4, (uint) (i/4)+1, Gtk.AttachOptions.Fill, Gtk.AttachOptions.Fill, (uint) 0, (uint) 0 );
				auxButton.Show();			
			}
		}
	}
}

