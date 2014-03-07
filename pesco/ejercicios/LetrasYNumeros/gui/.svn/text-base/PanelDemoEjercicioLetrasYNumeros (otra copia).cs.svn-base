
using System;
using Gtk;

namespace ecng
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
		
		public Label LabelJustificacion{
			get {
				LabelJustificacion.Wrap = true;
				return this.labelJustificacion;
			}	
		}
		
		public PanelDemoEjercicioLetrasYNumeros ()
		{
			this.Build ();
			
			vbox5.ShowAll();
			vbox1.Hide();
			
			this.PanelEjercicio.MuestraPanelIzquierdo();
			
			button1.Clicked += delegate(object sender, EventArgs e) {
				vbox5.Hide();
				vbox1.Show();			
			};
			
			GtkUtil.SetStyle(button1, Configuration.Current.ButtonFont);
			GtkUtil.SetStyle(botonEjecutaDemo, Configuration.Current.ButtonFont);
			GtkUtil.SetStyle(this.BotonAEnsayo, Configuration.Current.ButtonFont);
			
			frame1.Hide();
		}
		
		public void HideResultImages(){
			this.image26.Hide();	
			this.image27.Hide();
			frame1.Hide();
		}
		
		public void ShowOkResultImage(){
			frame1.Show();
			this.image26.Show();	
			this.image27.Hide();
			frame1.ModifyBg(StateType.Normal, new Gdk.Color(128, 255, 128));
			this.GtkLabel18.ModifyFg(StateType.Normal, new Gdk.Color(128, 255, 128));
		}
		
		public void ShowErrorResultImage(){
			frame1.Show();
			this.image26.Hide();	
			this.image27.Show();
			frame1.ModifyBg(StateType.Normal, new Gdk.Color(255, 128, 128));
			this.GtkLabel18.ModifyFg(StateType.Normal, new Gdk.Color(255, 128, 128));
		}
		
		public Button BotonAEnsayo{
			get{
				return this.botonAEnsayo;	
			}
		}
		
		public Button BotonEjecutaDemo {
			get	{
				return 	this.botonEjecutaDemo;
			}
		}
		
	}
}
