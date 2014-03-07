
using System;
using System.Collections.Generic;

using Gtk;

namespace ecng
{


	[System.ComponentModel.ToolboxItem(true)]
	public partial class PanelObjetosClasificables : Gtk.Bin
	{
		
		Timer t;
		
		uint numColumnas = 3;
		uint numFilas = 3;
		
		uint numColumnasI = 3;
		uint numFilasI = 3;
		
		uint col = 0;
		uint fil = 0;
		
		uint colI = 0;
		uint filI = 0;
		
		public void PararTemporizador()
		{
			t.Stop = true;
			t.StopTimer();
			this.ShowSecondMessagePanel();
		}
		
		
		public Button BotonComprobar{
			get{return botonComprobar;}	
		}

		
		public void IniciaTemporizador()
		{
			this.ShowFirstMessagePanel();			
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
		
		public PanelObjetosClasificables ()
		{
			this.Build ();
			
			
			
			
			//GtkUtil.SetStyle(this, Configuration.Current.ButtonFont);
			
			button1.Clicked += delegate{
				vbox3.Remove(t);
				t = new Timer(EjercicioObjetosClasificables.MuestraPanelDerecha, 30.00);
				this.vbox3.Add(t);
				this.ShowMemorizationPanel();
				
				t.StartClock();	
			};
			
			button2.Clicked += delegate {
				this.ShowSelectionPanel();	
			};
			
			GtkUtil.SetStyle(this.button1, Configuration.Current.ButtonFont);
			GtkUtil.SetStyle(this.button2, Configuration.Current.ButtonFont);
			GtkUtil.SetStyle(this.botonComprobar, Configuration.Current.ButtonFont);
			
			this.labelPauseText.ModifyFont(Pango.FontDescription.FromString("Ahafoni CLM Bold 10"));
			this.labelPauseText1.ModifyFont(Pango.FontDescription.FromString("Ahafoni CLM Bold 10"));
			//this.labelPauseText2.ModifyFont(Pango.FontDescription.FromString("Ahafoni CLM Bold 10"));
			//labelPauseText3.ModifyFont(Pango.FontDescription.FromString("Ahafoni CLM Bold 10"));
		}
		
		public void ResetPaneles()
		{
			foreach(Widget w in table1.AllChildren)
				table1.Remove(w);
			
			foreach(Widget w in table2.AllChildren)
				table2.Remove(w);
		}
		
		public void AddImageDerecha(string resourceName)
		{
			
			BotonObjetoClasificable boton = new BotonObjetoClasificable(resourceName);
			table1.Attach(boton, col, col+1, fil, fil+1);
			
		
			col++;
			col = col % numColumnas;
			
			if (col == 0)
			{
				fil++;
				fil = fil % numFilas;
			}
		}
		
		public void AddImageIzquierda(string resourceName)
		{
			
			//BotonObjetoClasificable boton = new BotonObjetoClasificable(resourceName);
			//boton.ModifyBg(StateType.Prelight, new Gdk.Color(255,255,255));
			
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
		
		public void ShowSelectionPanel()
		{
			vbox1.Hide();
			vbox2.ShowAll();
			vbox3.Hide();
			vbox4.Hide();
		}
		
		public void ShowMemorizationPanel()
		{
			vbox1.Hide();
			vbox2.Hide();
			vbox3.ShowAll();
			vbox4.Hide();
		}
		
		public void ShowFirstMessagePanel()
		{
			vbox1.ShowAll();
			vbox2.Hide();
			vbox3.Hide();
			vbox4.Hide();
		}
		
		public void ShowSecondMessagePanel()
		{
			vbox1.Hide();
			vbox2.Hide();
			vbox3.Hide();
			vbox4.ShowAll();
		}
		
/*SIN PONER AUN*/
		protected virtual void OnClickObjeto(object sender, System.EventArgs e)
		{
			if (sender.GetType().Equals(typeof(ToggleButton)) )
			{
				
				ToggleButton tb = (ToggleButton) sender;
				/*
				if (tb.Active)
					this.MarcaBoton(tb);
				else 
					this.DesmarcaBoton(tb);*/
			}
		
		}

		
			
	}
}
