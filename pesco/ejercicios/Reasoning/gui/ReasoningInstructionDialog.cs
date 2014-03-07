using System;
using Gtk;
namespace pesco
{
	public partial class ReasoningInstructionDialog : Gtk.Dialog
	{
		int contador = 0;
		
		public void SetTitle(string s)
		{
			this.labelPauseText.UseMarkup = true;
			this.labelPauseText.Markup = "<span color='blue'><big><big><big><big>" + s + "</big></big></big></big></span>";
			labelPauseText.Show();
		}
		
		public ReasoningInstructionDialog ()
		{
			this.Build ();
			
			foreach(Widget v in this.vbox2)
				v.Hide();
			
			vbox3.ShowAll();
			button6.Sensitive = false;
			button26.Sensitive = false;
			
			GtkUtil.SetStyle(this.button25, Configuration.Current.ButtonFont);
			GtkUtil.SetStyle(this.button26, Configuration.Current.ButtonFont);
			GtkUtil.SetStyle(this.button6,  Configuration.Current.ButtonFont);
			
			button25.Clicked += new EventHandler(OnClickForward);
			button6.Clicked  += new EventHandler(OnClickBack);
			
			label2.ModifyFont(Pango.FontDescription.FromString("Ahafoni CLM 9"));
			this.labelPauseText.ModifyFont(Pango.FontDescription.FromString("Ahafoni CLM Bold 14"));
			this.labelPauseText2.ModifyFont(Pango.FontDescription.FromString("Ahafoni CLM Bold 14"));
			
			
		}
		
		public void SetText(string s){
			label2.UseMarkup = true;	
			label2.Markup = "<span color='blue'>" + s + "</span>";
		}
		
		public void SetImage(string s){
			image11.Pixbuf = Gdk.Pixbuf.LoadFromResource(s);
		}
		
		protected virtual void OnClickBack (object sender, System.EventArgs e)
		{
			//.WriteLine("LLLLL" + vbox2.Children.Length);
			
			button25.Sensitive = true;
			if (this.contador > 0){
				vbox2.Children[this.contador].Hide();
				this.contador--;
			}
			
			if (this.contador == 0)
			{
				button25.Sensitive = true;
				button6.Sensitive = false;
				button26.Sensitive = true;
			}
			
			vbox2.Children[contador].ShowAll();
		}
		
		
		protected virtual void OnClickForward (object sender, System.EventArgs e)
		{
			button6.Sensitive = true;
			
			//Console.WriteLine("LLLLL" + vbox2.Children.Length);
			
			if (contador < vbox2.Children.Length-1){
				vbox2.Children[contador].Hide();
				contador++;
			}
			
			if (contador >= vbox2.Children.Length-1){
				button25.Sensitive = false;
				button6.Sensitive = true;
				button26.Sensitive = true;
			}
			
			vbox2.Children[contador].ShowAll();
		}
		
		
	}
	
	
}

