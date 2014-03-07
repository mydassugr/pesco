using System;
using	Gdk;
using Gtk;
using System.Threading;

namespace pesco
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class PanelVocalesYNumerosCen : Gtk.Bin
	{
		
		public PanelVocalesYNumerosCen ()
		{
			this.Build ();
			GtkUtil.SetStyle(this.labelCaracter, Configuration.Current.ExtraHugeFont);	
			labelCaracter.ModifyFg(StateType.Normal, new Gdk.Color(255, 255, 255));
            labelCaracter.ModifyFont(Pango.FontDescription.FromString( "Times Italic 150"));
		   	}
		
		public char SiguienteCaracter
		{
			set {
				this.labelCaracter.Text = "" + value;
				labelCaracter.ShowAll();
			}
		}
		
		public void SetNextExerciseMessage()
		{
			
		}
		
	}
}

