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
	public partial class GS_PanelSituation : Gtk.Bin
	{

		private int seconds = 0; // Seconds.
		private int minutes = 4; // Minutes.
	
		private int totalBudget = 0;
		
		public GS_PanelSituation ()
		{
			this.Build ();
			GtkUtil.SetStyle( labelBudget, Configuration.Current.MediumFont );
			GtkUtil.SetStyle( labelTime, Configuration.Current.MediumFont );
			GtkUtil.SetStyle( buttonViewMyShopping, Configuration.Current.MediumFont );
			GtkUtil.SetStyle( buttonViewShoppingList, Configuration.Current.MediumFont );
			GtkUtil.SetStyle( buttonFinishShopping, Configuration.Current.MediumFont );
			
		}

		public void ReplaceView( Gtk.Widget replaceWidget ) {
					
			if ( vboxContent.Children.Length > 0 ) {
				vboxContent.Remove( vboxContent.Children[0] );
			}
			vboxContent.PackStart(replaceWidget,true,true,0);
			
		}
		
		public void AddSituation( GS_Situation auxSituation) {
		
			totalBudget = auxSituation.Budget;
			
			labelBudget.Text = auxSituation.Budgetused + "€ / " + auxSituation.Budget+"€";
			labelTime.Text = "4:00";
			
		}
		
		public void UpdateTimer() {
		
			 if ( (minutes == 0) && (seconds == 0) )
				{
					labelTime.ModifyFg( Gtk.StateType.Normal, new Gdk.Color( 255, 0, 0 ) );
				  	labelTime.Text = "0:00";
				}
            else
            {
                if (seconds < 1)
                {
                    seconds = 59;
                    minutes -= 1;
                }
                else
                    seconds -= 1;
				
				string auxSeconds = "";
				if ( seconds < 10 ) {
					auxSeconds = "0"+seconds.ToString();
				} else {
					auxSeconds = seconds.ToString();
				}
                // the corresponding fields.
                labelTime.Text = minutes.ToString()+":"+auxSeconds;
            }

			
		}
		
		protected virtual void ViewShoppingListCallback (object sender, System.EventArgs e)
		{
			ExerciseGiftsShopping.getInstance().ShowCriterias();
		}
		
		protected virtual void ShowMyShoppingCallback (object sender, System.EventArgs e)
		{
			ExerciseGiftsShopping.getInstance().ShowMyShopping();
		}
		
		public void SetBudget( int budgetUsed ) {

			labelBudget.Text = budgetUsed + "€ / " + this.totalBudget+"€";
			if ( budgetUsed > totalBudget ) {
				labelBudget.ModifyFg( Gtk.StateType.Normal, new Gdk.Color(255,0,0) );
			} else {
				labelBudget.ModifyFg( Gtk.StateType.Normal );
			}
		}				
		
		protected virtual void FinishShoppingCallback (object sender, System.EventArgs e)
		{
			Gtk.MessageDialog dialog = new Gtk.MessageDialog (	null,
						Gtk.DialogFlags.Modal,
						Gtk.MessageType.Question,
						Gtk.ButtonsType.YesNo,
						"<span size=\"xx-large\">¿Has terminado tu compra?</span>",
						"Terminar compra"
			);
			GtkUtil.SetStyleRecursive( dialog, Configuration.Current.LargeFont );
			int result = dialog.Run ();
			if ( result == (int) Gtk.ResponseType.Yes )
			{											
				ExerciseGiftsShopping.getInstance().FinishShopping();
			}
			dialog.Destroy();
			
		}
		
		
	}
}

