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
	public partial class GS_PanelItemsShop : Gtk.Bin
	{

		private int idShop;
		private GS_Shop shop;
		private Gtk.Label boughtLabel = new Gtk.Label ("COMPRADO");

		public GS_PanelItemsShop ()
		{
			this.Build ();
			boughtLabel.UseMarkup = true;
			boughtLabel.Markup = "<span color=\"blue\">COMPRADO</span>";
		}

		public void AddShop (int idShop, GS_Shop auxShop)
		{
			
			this.idShop = idShop;
			this.shop = auxShop;
			
			for (int i = 0; i < auxShop.Items.Count; i++) {
				Gtk.ToggleButton auxButton = new Gtk.ToggleButton ();
				auxButton.Add (new Gtk.VBox (false, 0));
				
				auxButton.Name = i.ToString ();
				// Images are not resources anymore, we'll load then from folder
				// Gtk.Image auxImageItem = new Gtk.Image (Gdk.Pixbuf.LoadFromResource (auxShop.Items[i].Img).ScaleSimple (75, 75, Gdk.InterpType.Bilinear));
				string auxSeparator = System.IO.Path.DirectorySeparatorChar.ToString();
				Gtk.Image auxImageItem = new Gtk.Image (Configuration.CommandExercisesDirectory + auxSeparator + "resources" 
				                                        + auxSeparator + "img" + auxSeparator + "items" 
				                                        + auxSeparator + auxShop.Items[i].Img);
				auxImageItem.Pixbuf = auxImageItem.Pixbuf.ScaleSimple (75, 75, Gdk.InterpType.Bilinear);
				
				Gtk.Label auxLabel = new Gtk.Label (auxShop.Items[i].Name + " ( " + auxShop.Items[i].FinalPrice + "€ )");
				GtkUtil.SetStyle (auxLabel, Configuration.Current.SmallFont);
				
				Gtk.Label auxLabelBought = new Gtk.Label ("");
				GtkUtil.SetStyle (auxLabelBought, Configuration.Current.SmallFont);
				
				((Gtk.VBox)auxButton.Children[0]).PackStart (auxImageItem, false, false, 0);
				((Gtk.VBox)auxButton.Children[0]).PackStart (auxLabel, false, false, 0);
				((Gtk.VBox)auxButton.Children[0]).PackStart (auxLabelBought, false, false, 0);
				
				auxButton.Clicked += BuyItemButton;
				
				table.Attach (auxButton, (uint)i % 3, (uint)(i % 3) + 1, (uint)i / 3, (uint)(i / 3) + 1, Gtk.AttachOptions.Fill, Gtk.AttachOptions.Fill, (uint)0, (uint)0);
				auxButton.Show ();
			}
			
			Gtk.Button auxButtonComeToShops = new Gtk.Button ();
			auxButtonComeToShops.Label = "Volver a tiendas";
			GtkUtil.SetStyle (auxButtonComeToShops, Configuration.Current.HugeFont);
			auxButtonComeToShops.Clicked += delegate { ExerciseGiftsShopping.getInstance ().ShowShops (); };
			this.vbox7.PackEnd (auxButtonComeToShops, false, true, 0);
			
		}

		public void RemoveBought (GS_Item item)
		{
			
			int auxPosition = -1;
			for (int i = 0; i < shop.Items.Count; i++) {
				if (shop.Items[i].Id == item.Id && shop.Items[i].FinalPrice == item.FinalPrice) {
					auxPosition = i;
					break;
				}
			}
			
			// IMPORTANT! Order in table is reverse than order in array! 8 = 0, 7 = 1, 6 = 2...
			// So fixing it...
			auxPosition = ((table.Children.Length - 1) - auxPosition);
			
			// Removing bougth label from panel
			if (auxPosition != -1) {
				((Gtk.Label)((Gtk.VBox)(((Gtk.ToggleButton)table.Children[auxPosition])).Children[0]).Children[2]).Text = " ";
				(((Gtk.ToggleButton)table.Children[auxPosition])).Clicked -= BuyItemButton;
				(((Gtk.ToggleButton)table.Children[auxPosition])).Active = false;
				(((Gtk.ToggleButton)table.Children[auxPosition])).Clicked += BuyItemButton;
			}
		}

		protected virtual void BuyItemButton (object sender, EventArgs e)
		{
			
			if (((Gtk.ToggleButton)sender).Active == true) {
				Gtk.MessageDialog dialog = new Gtk.MessageDialog (null, Gtk.DialogFlags.Modal, Gtk.MessageType.Question, Gtk.ButtonsType.YesNo, "<span size=\"xx-large\">¿Deseas comprar este artículo?</span>", "Confirmar compra");
				GtkUtil.SetStyleRecursive( dialog, Configuration.Current.LargeFont );
				Gtk.ResponseType result = (Gtk.ResponseType)dialog.Run ();
				if (result == Gtk.ResponseType.Yes) {
					ExerciseGiftsShopping.getInstance ().BuyItem (idShop, Convert.ToInt32 (((Gtk.ToggleButton)sender).Name));
					((Gtk.Label)((Gtk.VBox)((Gtk.ToggleButton)sender).Children[0]).Children[2]).UseMarkup = true;
					((Gtk.Label)((Gtk.VBox)((Gtk.ToggleButton)sender).Children[0]).Children[2]).Markup = "<span color=\"blue\">COMPRADO</span>";
					((Gtk.ToggleButton)sender).ShowAll ();
				} else {
					((Gtk.ToggleButton)sender).Clicked -= BuyItemButton;
					((Gtk.ToggleButton)sender).Active = false;
					((Gtk.ToggleButton)sender).Clicked += BuyItemButton;
				}
				dialog.Destroy ();
			} else {
				Gtk.MessageDialog dialog = new Gtk.MessageDialog (null, Gtk.DialogFlags.Modal, Gtk.MessageType.Question, Gtk.ButtonsType.YesNo, "<span size=\"xx-large\">¿Deseas devolver este objeto?</span>", "Devolver compra");
				GtkUtil.SetStyleRecursive( dialog, Configuration.Current.LargeFont );
				int result = dialog.Run ();
				if (result == (int)Gtk.ResponseType.Yes) {
					ExerciseGiftsShopping.getInstance ().DropItem (idShop, Convert.ToInt32 (((Gtk.ToggleButton)sender).Name));
					((Gtk.Label)((Gtk.VBox)((Gtk.ToggleButton)sender).Children[0]).Children[2]).Text = "";
					((Gtk.ToggleButton)sender).ShowAll ();
				} else {
					((Gtk.ToggleButton)sender).Clicked -= BuyItemButton;
					((Gtk.ToggleButton)sender).Active = true;
					((Gtk.ToggleButton)sender).Clicked += BuyItemButton;					
				}
				dialog.Destroy ();
			}
		}
	}
}

