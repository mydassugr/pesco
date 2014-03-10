using System;
using Gtk;
namespace pesco
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class ExercisePanelTaskList1 : Gtk.Bin
    {
       private string DirExer =Configuration.DirExerImage+"tasklist"+System.IO.Path.DirectorySeparatorChar;
        
        public ExercisePanelTaskList1 ()
        {
            this.Build ();
            imagPepe.Pixbuf = new Gdk.Pixbuf(DirExer+"pepetl1.png");
            imagBack.Pixbuf= new Gdk.Pixbuf(DirExer+"backgroundtl4.png");
            GtkUtil.SetStyle(this.botonempezarsecuencia, Configuration.Current.ButtonFont);
            GtkUtil.SetStyle(this.labelPrevius, Configuration.Current.LargeFont);
            GtkUtil.SetStyle(this.labelLevel, Configuration.Current.LargeFont);
         
        }
        public Button BotonEmpezarSecuencia {
            get { return this.botonempezarsecuencia; }
        }
        public void MuestraResultado(int mtotal, int maciertos, int ierrors, int iLevel){
            if (iLevel!=0){
                labelLevel.Text="¡Ya estás en el nivel "+iLevel+ " !";
                string iex = "los "+ maciertos+" recados.";
                labelPrevius.Markup = "<b>¡Muy bien!, has acertado</b> " +iex;
                label52.Visible=true;
                label51.Visible=false;
            }
            else {
                string iex = maciertos+ " de un total de "+mtotal+" recados.";
                string iex2 = "";
                 if (ierrors!=0)
                    iex2 = "Has fallado "+ierrors+".";                 
                
                labelPrevius.Markup = "<b>Has acertado</b> " +iex+"\n"+iex2; 
                label51.Visible=true;
                label52.Visible=false;
                labelLevel.Text ="";}
            labelLevel.ModifyFg(StateType.Normal, new Gdk.Color(0xb6, 0x00, 0x00));  
            labelPrevius.ModifyBg(StateType.Normal, new Gdk.Color(0xb6, 0x00, 0x00));  
            labelPrevius.Show();
        }
        
        public void resultado(int itotal, int iaciertos){
            
            
        }
        
    }
}

