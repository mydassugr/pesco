using System;
using Gtk;

namespace pesco
{
	public class TaskListToggleButton: Gtk.ToggleButton
	{
		public TaskListToggleButton (string k ): base()
		{
			this.Label= k;
            ModifyBg (StateType.Active, new Gdk.Color (0xff, 0xff,0x77));
			this.Entered += delegate(object sender, EventArgs e) {
				
				(sender as ToggleButton).ModifyBg(StateType.Prelight);
				if ((sender as ToggleButton).Active){
					(sender as ToggleButton).ModifyBg (StateType.Prelight, new Gdk.Color (0xff, 0xff,0x77));
				}
			};
			
			this.Clicked += delegate(object sender, EventArgs e) {
								
				(sender as ToggleButton).ModifyBg(StateType.Prelight);
				if ((sender as ToggleButton).Active){
					(sender as ToggleButton).ModifyBg (StateType.Prelight, new Gdk.Color (0xff, 0xff,0x77));
				}
			};
		}
	}
}

