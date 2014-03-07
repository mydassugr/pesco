
using System;
using Gtk;

namespace pesco
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class PanelDemoEjercicioLetrasYNumeros : Gtk.Bin
	{
		
		public PanelVocalesYNumeros PanelEjercicio
		{
			get {
				return panelEjercicio;	
			}
		}
		
		/*public Label LabelJustificacion{
			get {
				labelJustificacion.Wrap = true;
				return this.labelJustificacion;
			}	
		}
		*/
		public PanelDemoEjercicioLetrasYNumeros ()
		{
			this.Build ();
			GtkUtil.SetStyle(botonEjecutaDemo, Configuration.Current.ButtonFont);
			GtkUtil.SetStyle(this.BotonAEnsayo, Configuration.Current.ButtonFont);
			BotonAEnsayo.Sensitive = false;
		//	frame1.Hide();
			//this.LabelJustificacion.WidthRequest = 1000;
		}
		
	/*	public void HideResultImages(){
			//this.image26.Hide();	
			//this.image27.Hide();
			frame1.Hide();
		}
	*/	
	/*	public void ShowOkResultImage(){
			frame1.Show();
			//this.image26.Show();	
			//this.image27.Hide();
			frame1.ModifyBg(StateType.Normal, new Gdk.Color(128, 255, 128));
			this.GtkLabel18.ModifyFg(StateType.Normal, new Gdk.Color(128, 255, 128));
		}
	*/	
		/*public void ShowErrorResultImage(){
			frame1.Show();
			//this.image26.Hide();	
			//this.image27.Show();
			frame1.ModifyBg(StateType.Normal, new Gdk.Color(255, 128, 128));
			this.GtkLabel18.ModifyFg(StateType.Normal, new Gdk.Color(255, 128, 128));
		}
		*/
		public Button BotonAEnsayo{
			get{
				return this.botonAEnsayo;	
			}
		}
		//Primer boton q se ejecuta en ensayo
		public Button BotonEjecutaDemo {
			get	{
				return 	this.botonEjecutaDemo;
			}
		}
		
	}
}
