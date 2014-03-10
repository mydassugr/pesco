using System;
using Gtk;
namespace pesco
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class ExercisePanelWordList1 : Gtk.Bin
    {
       private string DirExer =Configuration.DirExerImage+"wordlist"+System.IO.Path.DirectorySeparatorChar;
        
        public ExercisePanelWordList1 ()
        {
            this.Build ();
            imagPepe.Pixbuf = new Gdk.Pixbuf(DirExer+"pepewl1.png");
            imagBack.Pixbuf= new Gdk.Pixbuf(DirExer+"backgroundwl4.png");
            GtkUtil.SetStyle(this.botonempezarsecuencia, Configuration.Current.ButtonFont);
            GtkUtil.SetStyle(this.label2, Configuration.Current.LargeFont);
            GtkUtil.SetStyle(this.label1, Configuration.Current.LargeFont);
         
        }
        public Button BotonEmpezarSecuencia {
            get { return this.botonempezarsecuencia; }
        }
        
        public void ShowLabel(string itext1, string itext2){
            label1.Text=itext1;
            label1.ModifyFg(StateType.Normal, new Gdk.Color(0xb6, 0x00, 0x00));  
            label2.UseMarkup=true;
            label2.Markup=itext2;
            label1.Show();
            label2.Show();
        }
        public void SetLabel1Blue(){
            label1.ModifyFg(StateType.Normal, new Gdk.Color(0x2d, 0x13, 0xea));  
            label1.Show();
        }
            
        
    }
}

