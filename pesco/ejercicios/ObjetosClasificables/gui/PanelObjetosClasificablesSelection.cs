using System;
using System.Collections.Generic;
using Gtk;

namespace pesco
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class PanelObjetosClasificablesSelection : Gtk.Bin
	{
		uint numColumnas = 3;
		uint numFilas = 3;
		
		uint numColumnasI = 3;
		uint numFilasI = 3;
		
		uint col = 0;
		uint fil = 0;
			
		public Button BotonComprobar{
			get{return botonComprobar;}	
		}
		
		public uint NumFilas {
			get {
				return numFilas;
			}
			set {
				numFilas = value;
			}
		}
		
		
		public uint NumColumnas {
			get {
				return numColumnas;
			}
			set {
				numColumnas = value;
			}
		}	
		SortedDictionary<ToggleButton, string> asociacion = new SortedDictionary<ToggleButton, string>();
		
		public PanelObjetosClasificablesSelection ()
		{
			this.Build ();
			GtkUtil.SetStyle(this.botonComprobar, Configuration.Current.ButtonFont);
		}
		
		public void AddImageDerecha( List<string> LResourceName)
		{
			foreach (string resourceName in LResourceName){
				
			BotonObjetoClasificable boton = new BotonObjetoClasificable(resourceName);
            //boton.ButtonPressEvent+=new ButtonPressEventHandler(OnButtonImagenClicked);
            boton.Clicked+=OnButtonImagenClicked;    
			table1.Attach(boton, col, col+1, fil, fil+1);
           	col++;
			col = col % numColumnas;
			
			if (col == 0)
			{
				fil++;
				fil = fil % numFilas;
			}
			}
			ShowAll();
		}
        
		public List<string> getListaSeleccionadas()
		{
			
			List<string> seleccionadas = new List<string>();
			
			foreach (Widget w in table1.Children)
			{
				if (w.GetType().Equals(typeof(BotonObjetoClasificable)) ){
					BotonObjetoClasificable tb = (BotonObjetoClasificable) w;
					
					if (tb.Active)
						seleccionadas.Add(tb.ResourceName);
				}
			}
			
			return seleccionadas;
		}
        public void ResetPaneles()
        {
            foreach(Widget w in table1.AllChildren)
                table1.Remove(w);
            
        }
	
        protected virtual void OnButtonImagenClicked (object sender, System.EventArgs e)
        {
         if ((sender as BotonObjetoClasificable).Active)
              (sender as BotonObjetoClasificable).ModifyBg(StateType.Prelight, new Gdk.Color(0x1c, 0x0c, 0xf9));
         else (sender as BotonObjetoClasificable).ModifyBg(StateType.Prelight, new Gdk.Color(0xd0, 0xcf, 0xed));     
                
        }
        
        
	}
}

