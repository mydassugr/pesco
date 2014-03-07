using System;
namespace pesco
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class PESCOGoodByeDialog : Gtk.Bin
	{
		protected virtual void OnButtonQuitClicked (object sender, System.EventArgs e)
		{
			SessionManager.GetInstance().FinishApplication();
		}
		
		public PESCOGoodByeDialog ()
		{
			this.Build ();
			GtkUtil.SetStyle(buttonQuit, Configuration.Current.MediumFont);
		}
		
		public void SetText(string message) {
		
			dialogPanel.SetText(message);
			
		}
		
		public DialogPanel Dialog {			
			get {
				return dialogPanel;
			}
		}
	}
}

