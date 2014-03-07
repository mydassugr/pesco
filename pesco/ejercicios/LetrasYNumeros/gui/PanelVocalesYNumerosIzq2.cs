using System;
using Gtk;
namespace pesco
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class PanelVocalesYNumerosIzq2 : Gtk.Bin
	{

		Gdk.GC auxGC;
			
		public PanelVocalesYNumerosIzq2 ()
		{
			this.Build ();
          	GtkUtil.SetStyle(this.botonempezarsecuencia, Configuration.Current.ButtonFont);
          
		//	label1.ModifyFont(Pango.FontDescription.FromString("Ahafoni CLM Bold 10"));
			
			
			// Init pixmap of image
			this.image167.Pixmap = new Gdk.Pixmap(this.GdkWindow, 1000, 600, 24);
			
			// Init graphics context
			auxGC = new Gdk.GC(this.image167.Pixmap);		
			
			// Generate cartel
			PepeUtils.GenerateCartel( this.image167.CreatePangoContext(), auxGC, this.image167.Pixmap, "¡Comienza el Ejercicio!" );
			
			// Draw background
			Gdk.Pixbuf auxBackground = new Gdk.Pixbuf( Configuration.CommandExercisesDirectory+"/resources/img/numersvowels/escenario3.jpg");
			this.image167.Pixmap.DrawPixbuf( auxGC, auxBackground, 0, 0, 0, 0, auxBackground.Width, auxBackground.Height, 0, 0, 0 );

			// Draw cartel
			PepeUtils.DrawCartel();
		}
        
        public Button BotonEmpezarSecuencia {
            get { return this.botonempezarsecuencia; }
        }
    }
}

