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
	public partial class GS_PanelCriterias : Gtk.Bin
	{		
		
		public GS_PanelCriterias ()
		{
			this.Build ();
		}
	
		public void AddSituation(GS_Situation auxSituation) {
	
			int width = 0;
			int height = 0;

			// Label criterias
			if ( auxSituation.Criterias.Count <= 4 ) {
				GtkUtil.SetStyle( labelInfo, Configuration.Current.MediumFont );	
			} else if ( auxSituation.Criterias.Count <= 6 ) {
				GtkUtil.SetStyle( labelInfo, Configuration.Current.MediumFont );	
			} else {
				GtkUtil.SetStyle( labelInfo, Configuration.Current.SmallFont );	
			}
			
			labelInfo.SizeAllocated += delegate(object o, Gtk.SizeAllocatedArgs args) {
				((Gtk.Label) o).SetSizeRequest ( Allocation.Size.Width - 20, -1 );
			};
			
			string text = "";

			for ( int i = 0; i < auxSituation.Criterias.Count; i++ ) {				
				text += " - "+auxSituation.Criterias[i].Text;
				if ( i != auxSituation.Criterias.Count - 1 )
					text += "\n\n";
			}
			
			Gtk.Label auxLabel = new Gtk.Label();
			auxLabel.UseMarkup = true;
			auxLabel.Markup = "<span color=\"blue\">"+text+"</span>";
			this.vbox5.PackStart( auxLabel, true, true, 0 );												
			auxLabel.Justify = Gtk.Justification.Fill;
			if ( auxSituation.Criterias.Count <= 4 ) {
				GtkUtil.SetStyle( auxLabel, Configuration.Current.MediumFont );
			} else if ( auxSituation.Criterias.Count <= 6 ) {
				GtkUtil.SetStyle( auxLabel, Configuration.Current.SmallFont );
			} else {
				GtkUtil.SetStyle( auxLabel, Configuration.Current.SmallFont );
			}
			auxLabel.Wrap = true;
			auxLabel.SizeAllocated += delegate(object o, Gtk.SizeAllocatedArgs args) {
				((Gtk.Label) o).SetSizeRequest ( Allocation.Size.Width - 20, -1 );
			};
			
			// Budge
			labelBudget.Markup = "<span color=\"red\">Tu presupuesto es de: "+auxSituation.Budget+"€</span>";
			if ( auxSituation.Criterias.Count <= 4 ) {
				GtkUtil.SetStyle( labelBudget, Configuration.Current.LargeFont );
			} else if ( auxSituation.Criterias.Count <= 6 ) {
				GtkUtil.SetStyle( labelBudget, Configuration.Current.MediumFont );
			} else {
				GtkUtil.SetStyle( labelBudget, Configuration.Current.SmallFont );
			}
			
			// Go to shops button
			if ( auxSituation.Criterias.Count <= 4 ) {
				GtkUtil.SetStyle( buttonGoToShops, Configuration.Current.LargeFont );
			} else if ( auxSituation.Criterias.Count <= 6 ) {
				GtkUtil.SetStyle( buttonGoToShops, Configuration.Current.MediumFont );					
			} else {
				GtkUtil.SetStyle( buttonGoToShops, Configuration.Current.MediumFont );
			}
		
		}
		
		protected virtual void GoToShopsCallback (object sender, System.EventArgs e)
		{
			ExerciseGiftsShopping.getInstance().GoToShops();			
		}
		
		
		
	}
}

