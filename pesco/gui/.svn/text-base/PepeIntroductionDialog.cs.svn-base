using System;
using Gdk;
using System.Collections.Generic;
namespace pesco
{
	public partial class PepeIntroductionDialog : Gtk.Bin
	{
		
		public DialogPanel Dialog {
			get{
				return dialogPanel;	
			}
		}
		
		UsersExercice userEx=null;
		protected virtual void OnButtonCancelClicked (object sender, System.EventArgs e)
		{
			userEx.iniciar();
		}
		
		
		public PepeIntroductionDialog (UsersExercice userExArg)
		{
			this.Build ();
			userEx = userExArg;
			List <string> auxTexts = new List<string>();
			auxTexts.Add( "A continuación te voy realizar una serie de preguntas sobre tí y tu entorno, <span color=\"black\">¡que son muy importantes!</span>");
			dialogPanel.SetText( auxTexts );
			dialogPanel.InitPanel();
			
			GtkUtil.SetStyle(this.buttonCancel, Configuration.Current.ButtonFont);
		}
	}
}

