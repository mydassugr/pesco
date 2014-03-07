using System;
using Gtk;
namespace ecng
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class ExercisePanelTaskList2 : Gtk.Bin
    {
       
        public ExercisePanelTaskList2  ()
        {
            this.Build ();
            GtkUtil.SetStyle(this.botonempezarsecuencia, Configuration.Current.ButtonFont);
            GtkUtil.SetStyle(this.labelPrevius, Configuration.Current.LargeFont);
            GtkUtil.SetStyle(this.labelLevel, Configuration.Current.LargeFont);
         
        }
        public Button BotonEmpezarSecuencia {
            get { return this.botonempezarsecuencia; }
        }
        public void MuestraResultado(int mtotal, int maciertos, int iLevel){
            if (iLevel!=0)
                labelLevel.Text="¡Ya estas en el Nivel "+iLevel+ " !";
            else labelLevel.Text ="";
            labelLevel.ModifyFg(StateType.Normal, new Gdk.Color(0xb6, 0x00, 0x00));  
             string iex = maciertos+ " de un total de "+mtotal+" caracteres.";
            //explanation.Markup="<span size='xx-large'>Lo has hecho bien, porque has pulsado primero el número <b>" + CadOne+ "</b> y luego la vocal <b>" + CadTwo+ "</b>.</span>";
             labelPrevius.Markup = "<b>Has acertado</b> " +iex;
             labelPrevius.ModifyBg(StateType.Normal, new Gdk.Color(0xb6, 0x00, 0x00));  
             labelPrevius.Show();
        }
        
        public void resultado(int itotal, int iaciertos){
            
            
        }
        
    }
}

