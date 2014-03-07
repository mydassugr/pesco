using System;
using Gtk;
namespace pesco
{
	public class QuestionaryToggleBtn: ToggleButton
	{
		public QuestionaryToggleBtn (string mtext):base()
		{
			this.Label=mtext;
			this.HeightRequest =100;
			
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
					this.ExclusiveToggleButton(((VBox)(sender as ToggleButton).Parent),(sender as QuestionaryToggleBtn));
			};
		}
		public QuestionaryToggleBtn (string mtext, int btnHeigth):base()
		{
			this.Label=mtext;
			this.HeightRequest =btnHeigth;
			
			GtkUtil.SetStyle(this,Configuration.Current.MediumFont);
			
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
					this.ExclusiveToggleButton(((VBox)(sender as ToggleButton).Parent),(sender as QuestionaryToggleBtn));
			};
		}
		
		public void ExclusiveToggleButton(VBox optionButton, QuestionaryToggleBtn activeButton){
			
			foreach( QuestionaryToggleBtn btn in optionButton.Children){
				if(btn != activeButton)
					btn.Active=false;
			}
		}
	}
}

