using System;
using Gtk;
namespace pesco
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class UsersToggleButton :  Gtk.ToggleButton
	{
		public UsersToggleButton (string mtext):base()
		{
			this.Build ();
			this.Label=mtext;
			
			this.Entered += delegate(object sender, EventArgs e) {
				
				(sender as ToggleButton).ModifyBg(StateType.Prelight);
				if ((sender as ToggleButton).Active){
					(sender as ToggleButton).ModifyBg (StateType.Prelight, new Gdk.Color (0xff, 0xff,0x77));
				}
			};
			
			this.Clicked += delegate(object sender, EventArgs e) {
								
				(sender as ToggleButton).ModifyBg(StateType.Prelight);
				if ((sender as ToggleButton).Active ){
					(sender as ToggleButton).ModifyBg (StateType.Prelight, new Gdk.Color (0xff, 0xff,0x77));
					(sender as ToggleButton).ModifyBg (StateType.Active, new Gdk.Color (0xff, 0xff,0x77));
				}
				
				if((sender as ToggleButton).Active == true)
					this.ExclusiveToggleButton(((VBox)(sender as ToggleButton).Parent),(sender as UsersToggleButton));
			};
		}
		
		public void ExclusiveToggleButton(VBox optionButton, UsersToggleButton activeButton){
			
			foreach( UsersToggleButton btn in optionButton.Children){
				if(btn != activeButton)
					btn.Active=false;
			}
		}
	}
}

