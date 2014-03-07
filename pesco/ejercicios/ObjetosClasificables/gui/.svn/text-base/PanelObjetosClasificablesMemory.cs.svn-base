using System;
using System.Collections.Generic;
using Gtk;
namespace pesco
{
    
	[System.ComponentModel.ToolboxItem(true)]
	public partial class PanelObjetosClasificablesMemory : Gtk.Bin
	{
		
		Timer t;
	    protected EjercicioObjetosClasificables padre;
		uint numColumnas = 3;
		uint numFilas = 3;
		
		uint numColumnasI = 3;
		uint numFilasI = 3;
		
		uint col = 0;
		uint fil = 0;
		
		uint colI = 0;
		uint filI = 0;
        
        int nivelMemory=0;
        
		List<string> LResourceName;
		
     	public EjercicioObjetosClasificables Padre {
			get {return this.padre;}
			set {padre = value;}
		}
		
		public void PararTemporizador()
		{
			t.Stop = true;
			t.StopTimer();
			//this.ShowSecondMessagePanel();
		}
		
		
		public void IniciaTemporizador()
		{
			t.StartClock();
		//	this.ShowFirstMessagePanel();			
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
		
		public uint NumFilasI {
			get {
				return numFilasI;
			}
			set {
				numFilasI = value;
			}
		}
		
		
		public uint NumColumnasI {
			get {
				return numColumnasI;
			}
			set {
				numColumnasI = value;
			}
		}
		
		
		SortedDictionary<ToggleButton, string> asociacion = new SortedDictionary<ToggleButton, string>();
		
		
	   
		public PanelObjetosClasificablesMemory (int inivel)
		{
           
			this.Build ();
            
		//	t = new Timer(EjercicioObjetosClasificables.MuestraPanelDerecha, 30.00);
			t = new Timer(FinalizeTime, (inivel<3) ? 30 : 60);
           	this.vbox3.Add(t);
	        IniciaTemporizador();
			
		}
		public void FinalizeTime(){
	    	PararTemporizador();
			Padre.CreaPanelDerecha();
	  	}
		
		
		public void AddImageIzquierda(List<string> LResourceName)
		{
			
			//BotonObjetoClasificable boton = new BotonObjetoClasificable(resourceName);
			//boton.ModifyBg(StateType.Prelight, new Gdk.Color(255,255,255));
			foreach (string resourceName in LResourceName){
				Console.WriteLine("memorizar:  "+ resourceName+numColumnasI+" "+numFilasI);
					Frame f = new Frame();
					f.ShadowType = ShadowType.In;
					f.Add(new Image(Gdk.Pixbuf.LoadFromResource(resourceName)));
					
					table2.Attach(f, colI, colI+1, filI, filI+1);
					colI++;
					colI = colI % numColumnasI;
					
					if (colI == 0)
					{
						filI++;
						filI = filI % numFilasI;
					}
			}
			this.ShowAll();
		}
        public void ResetPaneles()
        {
            foreach(Widget w in table2.AllChildren)
                table2.Remove(w);
        }
		
	}
}

