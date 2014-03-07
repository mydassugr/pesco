using System;
namespace pesco
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class VerbalMerorizationPanel : Gtk.Bin
	{
		 private string DirExer = Configuration.DirExerImage+"wordlist"+System.IO.Path.DirectorySeparatorChar;
        
		public Gtk.Button BotonListo{
			get {
				return this.botonListo;	
			}
		}
		
		public Gtk.Button RecordButton{
			get {
				return this.recordButton;	
			}
		}
		
		public VerbalMerorizationPanel ()
		{
			this.Build ();
			imagebtn1.Pixbuf = new Gdk.Pixbuf(DirExer+"btnRec.png");
            imagebtn2.Pixbuf = new Gdk.Pixbuf(DirExer+"btnStop.png");
            imagepepe.Pixbuf = new Gdk.Pixbuf(DirExer+"pepehabla2.png");
			GtkUtil.SetStyle(this.BotonListo, Configuration.Current.ButtonFont);
			GtkUtil.SetStyle(this.RecordButton, Configuration.Current.ButtonFont);
		//	GtkUtil.SetStyle(this.label2, Configuration.Current.LargeFont);
			
			botonListo.Sensitive = false;
		}
        public void activateP1(){
            labelP1.Sensitive=false;
            labelP11.Sensitive=false;
            labelP2.Sensitive=true;
            labelP22.Sensitive=true;
            recordButton.Sensitive=false;
            botonListo.Sensitive=true;
        }
	}
}

