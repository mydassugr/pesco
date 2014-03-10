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
	public partial class GS_PanelMyShopping : Gtk.Bin
	{

		public GS_PanelMyShopping ()
		{
			this.Build ();
			GtkUtil.SetStyle( buttonGoToShops, Configuration.Current.HugeFont );
		}
		
		public void UpdatePanel () {
						
			
			GS_Situation auxSituation = ExerciseGiftsShopping.getInstance().CurrentSituation;
			List <GS_Item> items = auxSituation.Shoppingcart;
			
			Gtk.Table table = new Gtk.Table(1,1	,true);
						
			for (int i = 0; i < items.Count; i++ ) {
				
				Gtk.Button auxButton = new Gtk.Button();
				auxButton.Add( new Gtk.VBox(false,0) );
				
				auxButton.Name = i.ToString();
				
				
				string auxSeparator = System.IO.Path.DirectorySeparatorChar.ToString();
				Gtk.Image auxImageItem = new Gtk.Image (Configuration.CommandExercisesDirectory + auxSeparator + "resources" 
				                                        + auxSeparator + "img" + auxSeparator + "items" 
				                                        + auxSeparator + items[i].Img);
				
				auxImageItem.Pixbuf = auxImageItem.Pixbuf.ScaleSimple (75, 75, Gdk.InterpType.Bilinear);
				( (Gtk.VBox) auxButton.Children[0]).PackStart( auxImageItem );
				
				Gtk.Label auxLabel = new Gtk.Label( items[i].Name +" ( "+items[i].FinalPrice+"€ )" );
				GtkUtil.SetStyle( auxLabel, Configuration.Current.SmallFont );
				
				( (Gtk.VBox) auxButton.Children[0]).PackStart( auxLabel  );
				
				auxButton.Clicked += DropItem;
				table.Attach( auxButton, (uint) i % 3, (uint) (i % 3)+1, (uint) i / 3, (uint) (i/3)+1, Gtk.AttachOptions.Fill, Gtk.AttachOptions.Fill, (uint) 0, (uint) 0 );
			}
					
			if ( items.Count == 0 ) {
				Gtk.Label auxLabel = new Gtk.Label("");
				auxLabel.UseMarkup = true;
				auxLabel.Markup = "<span color=\"blue\">Aun no has comprado ningún regalo</span>";
				auxLabel.Justify = Gtk.Justification.Center;
				GtkUtil.SetStyle ( auxLabel, Configuration.Current.MediumFont );
				table.Attach( auxLabel,0, 3, 0, 1 );
			}
			
			if ( frameAlignment.Children.Length > 0 ) {
				frameAlignment.Remove( frameAlignment.Children[0] );	
			}
			
			frameAlignment.Add( table );
			
			table.ShowAll();
		}
		protected virtual void GoToShopsCallback (object sender, System.EventArgs e)
		{
			ExerciseGiftsShopping.getInstance().ShowShops();			
		}
		

		protected virtual void DropItem (object sender, EventArgs e) {	
			Gtk.MessageDialog dialog = new Gtk.MessageDialog (	null,
				Gtk.DialogFlags.Modal,
				Gtk.MessageType.Question,
				Gtk.ButtonsType.YesNo,
				"<span size=\"xx-large\">¿Deseas devolver este objeto?</span>",
				"Devolver compra"
			);
			GtkUtil.SetStyleRecursive( dialog, Configuration.Current.LargeFont );
			int result = dialog.Run ();
			if ( result == (int) Gtk.ResponseType.Yes )
			{											
				ExerciseGiftsShopping.getInstance().DropItemFromCart( Convert.ToInt32( ((Gtk.Button)sender).Name ) );
				UpdatePanel();
			}
			dialog.Destroy();
		}
	}
}

