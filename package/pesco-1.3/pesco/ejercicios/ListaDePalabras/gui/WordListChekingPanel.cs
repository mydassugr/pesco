using System;
using System.Collections.Generic;
using Gtk;

namespace pesco
{
    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.ToolboxItem(true)]
    public partial class WordListCheckingPanel : Gtk.Bin
    {

        List<VBox> cajas = new List<VBox> ();
        int current = 0;
        
        int numPags = 1;
 private string DirExer = Configuration.DirExerImage+"wordlist"+System.IO.Path.DirectorySeparatorChar;
        
        /// <summary>
        /// 
        /// </summary>
        public Button BotonListo {
            get { return this.botonListo; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listaPalabras">
        /// A <see cref="List<System.String>"/>
        /// </param>
        public WordListCheckingPanel (List<string> listaPalabras)
        {
            this.Build ();
            imagepepe.Pixbuf = new Gdk.Pixbuf(DirExer+"pepehablatl.png");
            // Disable button ready at beginning. It will be enabled when user enter last page.
            botonListo.Sensitive = false;
            
            ToggleButton b;
            
            int numFila = 0;
            int numColumna = 0;
            int tope;
            
            
            if (listaPalabras.Count >= 18)
                tope = (listaPalabras.Count / 3) - 1;
            else
                tope = (listaPalabras.Count / 2) - 1;
            
            VBox actual;
            
            VBox aux = new VBox();
            cajas.Add(aux);
            hbox6.Add(aux);
			
            
            for (int i = 0; i < listaPalabras.Count; ++i) {
                actual = cajas[numColumna];
                //b = new ToggleButton (listaPalabras[i]);
                b= new TaskListToggleButton(listaPalabras[i]);
                
                
                actual.Add (b);
                
                GtkUtil.SetStyle (b, Configuration.Current.HandwrittingStyle);
            /*  b.ModifyFg(StateType.Normal, new Gdk.Color(0,0,0));
                b.ModifyFg(StateType.Active, new Gdk.Color(0,0,0));
                b.ModifyFg(StateType.Prelight, new Gdk.Color(0,0,0));
                b.ModifyBg (StateType.Active, new Gdk.Color (0xff, 0xff, 0x77));
                b.ModifyBg (StateType.Prelight, new Gdk.Color (0xff, 0xff, 0x77));*/
                //b.Clicked += this.OnClickTarea;
                
                numFila++;
                if (numFila > tope && i < listaPalabras.Count-1 ) {
                    numColumna++;
                    numFila = 0;
                    aux = new VBox();
                    cajas.Add(aux);
                    hbox6.Add(aux);
					//hbox6.ReorderChild(aux,1);
					//hbox6.ReorderChild(vbox1,2);
					
                    numPags++;
                }
            }
            
			
            if (numColumna == 0)
                
            cajas[0].Hide();
            vbox2.Show();
            this.hbox6.ShowAll();
            
            for (int j=1; j < cajas.Count; ++j) {
                cajas[j].Hide();
            }
            
            
            GtkUtil.SetStyle (botonListo, Configuration.Current.ButtonFont);
            GtkUtil.SetStyle (buttonNext, Configuration.Current.ButtonFont);
            GtkUtil.SetStyle (buttonPrevious, Configuration.Current.ButtonFont);
            
            buttonNext.ImagePosition = PositionType.Right;
            
            buttonNext.Clicked += delegate {
                
                buttonPrevious.Sensitive = true;
                
                if (this.current < cajas.Count){
                    cajas[current].Hide();
                    current++;
                    cajas[current].Show();
                    //this.hbox5.ShowAll();
                    //this.hbox4.Show();
                    
                    
                    if (current ==  cajas.Count-1) { 
                        buttonNext.Sensitive = false;
                        botonListo.Sensitive = true;    
                    }
                    
                    this.label1.Markup = "<big><big>Página <b>" + (current+1) + "</b> de <b>" + numPags + "</b></big></big>";
                }
            };
            buttonPrevious.Sensitive = false;
            buttonPrevious.Clicked += delegate {
                
                buttonNext.Sensitive = true;
                
                if (this.current > 0){
                    cajas[current].Hide();
                    current--;

                    
                    cajas[current].Show();
                    //this.hbox4.Show();
                    
                    
                    if (current ==  0)
                        buttonPrevious.Sensitive = false;
                    
                    this.label1.Markup = "<big><big>Página <b>" + (current+1) + "</b> de <b>" + numPags  + "</b></big></big>";
                }
            };
            
            this.label1.UseMarkup = true;
            this.label1.Markup = "<big><big>Página <b>1</b> de <b>" +  numPags + "</b></big></big>";
            //alignment1.BorderWidth = 12;
            this.alignment3.Xscale = 0.5f;
            
			hbox6.ReorderChild(vbox1, hbox1.Children.GetLength(0));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tarea">
        /// A <see cref="System.String"/>
        /// </param>
        /// <returns>
        /// A <see cref="System.Boolean"/>
        /// </returns>
        public bool SeleccionarTarea (string tarea)
        {
            bool res = false;
            
            foreach (VBox v in cajas)
                foreach (ToggleButton tb in v.Children) {
                    
                    if (tb.Label == tarea)
                        if (tb.Active)
                            return false;
                        else {
                            tb.IsFocus = true;
                            tb.ModifyBg (StateType.Normal, new Gdk.Color (0xbb, 0x99, 0x44));
                            return true;
                        }
                }
            
            return res;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">
        /// A <see cref="System.Object"/>
        /// </param>
        /// <param name="e">
        /// A <see cref="System.EventArgs"/>
        /// </param>
        protected virtual void OnClickTarea (object sender, System.EventArgs e)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// 
        /// </returns>
        public List<string> GetSelectedItems ()
        {
            
            List<string> seleccionadas = new List<string> ();
            
            foreach(VBox v in cajas)
                foreach (ToggleButton tb in v.Children) {
                    if (tb.Active)
                        seleccionadas.Add (tb.Label);
                }
            
            return seleccionadas;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">
        /// A <see cref="System.Object"/>
        /// </param>
        /// <param name="e">
        /// A <see cref="System.EventArgs"/>
        /// </param>
        protected virtual void OnVerSeleccionadosClicked (object sender, System.EventArgs e)
        {
            List<String> seleccionadas = this.GetSelectedItems ();
            
            foreach (string s in seleccionadas)
                Console.WriteLine (s);
        }
    }
}
