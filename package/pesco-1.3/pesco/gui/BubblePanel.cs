using System;
namespace pesco
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class BubblePanel : Gtk.Bin
	{
		public void SetText(string text)
		{
			label1.Markup = "<span color='blue'>" + text + "</span>";	
		}
		
		public BubblePanel ()
		{
			this.Build ();
		}
		
		public BubblePanel (string text)
		{
			this.Build ();
			label1.UseMarkup = true;
			label1.ModifyFont(Pango.FontDescription.FromString("Ahafoni CLM Bold 14"));
			this.SetText(text);
		}
	}
}

