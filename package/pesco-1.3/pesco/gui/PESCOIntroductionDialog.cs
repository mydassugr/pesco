using System;
using System.Collections.Generic;

namespace pesco
{
	public partial class PESCOIntroductionDialog : Gtk.Bin
	{
		UsersExercice userEx = null;
		public PESCOIntroductionDialog ()
		{
			this.Build();						
			SetStyles();
		}
		
		public PESCOIntroductionDialog (UsersExercice userExArg)
		{
			this.Build ();
			this.userEx = userExArg;

			SetStyles();
		}
		
		public void SetStyles() {
			GtkUtil.SetStyle(this.buttonCancel, Configuration.Current.MediumFont);
			GtkUtil.SetStyle(this.buttonBack, Configuration.Current.MediumFont);

			buttonBack.Hide();	
		}
		
		public void ShowBackButton()
		{
			buttonBack.Show();
		}
		
		public Gtk.Button BackButton{
			get{
				return buttonBack;	
			}
		}
		
		public void SetText(string s){			
			dialogPanel.SetText( s );
		}
		
		public void SetButtonText( string s ) {
			this.DialogButton.Label = Gtk.Stock.Quit;
			// GtkUtil.SetStyle(this.DialogButton, Configuration.Current.MediumFont);
		}
				
		protected virtual void OnButtonCancelClicked (object sender, System.EventArgs e)
		{
			if ( userEx != null ) {
				userEx.iniciar();
			}
		}
						
		public Gtk.Button DialogButton{
			get{
				return this.buttonCancel;	
			}
		}	
		
		public DialogPanel Dialog {
			get {
				return this.dialogPanel;	
			}
		}
	}
}

