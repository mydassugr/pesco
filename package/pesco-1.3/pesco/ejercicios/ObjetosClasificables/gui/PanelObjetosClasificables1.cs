using System;
using Gtk;
namespace pesco
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class PanelObjetosClasificables1 : Gtk.Bin
    {
            Gdk.GC auxGC;
       private string DirExer =Configuration.DirExerImage+"classifyobjects"+System.IO.Path.DirectorySeparatorChar;
        
        public PanelObjetosClasificables1 ()
        {
            this.Build ();
            GtkUtil.SetStyle(this.buttonExercise, Configuration.Current.ButtonFont);
          
        //  label1.ModifyFont(Pango.FontDescription.FromString("Ahafoni CLM Bold 10"));
            // Init pixmap of image
            this.image167.Pixmap = new Gdk.Pixmap(this.GdkWindow, 1000, 600, 24);
            
            // Init graphics context
            auxGC = new Gdk.GC(this.image167.Pixmap);       
            
            // Generate cartel
            PepeUtils.GenerateCartel( this.image167.CreatePangoContext(), auxGC, this.image167.Pixmap, "Terminada la Prueba \n \n¡Comienza el Ejercicio!" );
            
            // Draw background
            Gdk.Pixbuf auxBackground = new Gdk.Pixbuf(DirExer+"backgroundc3.png");
            this.image167.Pixmap.DrawPixbuf( auxGC, auxBackground, 0, 0, 0, 0, auxBackground.Width, auxBackground.Height, 0, 0, 0 );

            // Draw cartel
            PepeUtils.DrawCartel();
        }
        
        public Button ButtonExercise {
            get { return this.buttonExercise; }
        }
    }
}
