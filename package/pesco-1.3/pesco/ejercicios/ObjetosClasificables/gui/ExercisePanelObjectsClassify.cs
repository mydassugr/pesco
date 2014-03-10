using System;
using Gtk;
namespace pesco
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class ExercisePanelObjectsClassify : Gtk.Bin
    {
         private string DirExer =Configuration.DirExerImage+"classifyobjects"+System.IO.Path.DirectorySeparatorChar;
        
        public ExercisePanelObjectsClassify  ()
        {
            this.Build ();
            imagPepe.Pixbuf = new Gdk.Pixbuf(DirExer+"pepec1.png");
            imagBack.Pixbuf= new Gdk.Pixbuf(DirExer+"backgroundc4.png");
            GtkUtil.SetStyle(this.botonempezarsecuencia, Configuration.Current.ButtonFont);
            GtkUtil.SetStyle(this.label2, Configuration.Current.LargeFont);
            GtkUtil.SetStyle(this.label1, Configuration.Current.LargeFont);
         
        }
        public Button BotonEmpezarSecuencia {
            get { return this.botonempezarsecuencia; }
        }
        
        public void ShowLabel(int ilevel){
            label1.Text="¡Muy bien, has pasado de nivel!";
            label1.ModifyFg(StateType.Normal, new Gdk.Color(0xb6, 0x00, 0x00));  
            label2.Text="Ya estás en el nivel "+ilevel+", ahora te pondré más \nimágenes, seguro que lo haces muy bien.";
            label1.Show();
            label2.Show();
        }
        public void SetLabel1Blue(){
            label1.ModifyFg(StateType.Normal, new Gdk.Color(0x2d, 0x13, 0xea));  
            label1.Show();
        }
            
        
    }
}

