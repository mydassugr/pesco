/**

Copyright 2011 Grupo de Investigación GEDES
Lenguajes y sistemas informáticos
Universidad de Granada

Licensed under the EUPL, Version 1.1 or – as soon they 
will be approved by the European Commission - subsequent  
versions of the EUPL (the "Licence"); 
You may not use this work except in compliance with the 
Licence. 
You may obtain a copy of the Licence at: 

http://ec.europa.eu/idabc/eupl  

Unless required by applicable law or agreed to in 
writing, software distributed under the Licence is 
distributed on an "AS IS" basis, 
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either 
express or implied. 
See the Licence for the specific language governing 
permissions and limitations under the Licence. 



*/
using Gtk;
using System;

namespace pesco
{

	public partial class VentanaEjercicios : Gtk.Window
	{
		
		public Exercise auxEj;
		private Gdk.Pixbuf fondoPixbuf;
		private Gdk.Pixmap auxPixmap;
		
		protected virtual void OnDeleteEvent (object o, Gtk.DeleteEventArgs args)
		{
			auxEj.pausa();
			
			MessageDialog md = new MessageDialog ( this, 
										DialogFlags.DestroyWithParent,
										MessageType.Question, 
										ButtonsType.YesNo, "<span size=\"xx-large\">¿Desea realmente abandonar el ejercicio?</span>");
      		GtkUtil.SetStyleRecursive( md, Configuration.Current.MediumFont );
			
			ResponseType result = (ResponseType)md.Run ();

			if (result == ResponseType.Yes) {
				auxEj.finalizar();
				SessionManager.GetInstance().FinishApplication();
				args.RetVal = false; // <-- Destroy window
			} else {
				md.Destroy();
				auxEj.pausa();
				args.RetVal = true; // <-- Don't destroy window
			}

		}	

		public VentanaEjercicios () : base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
			this.Maximize();
			// this.Fullscreen();
		}
		
		public void agregarPanelEjercicio ( Gtk.Widget panel ) {
			//if (contenedorEjercicio !=  null)
			// contenedorEjercicio.agregarPanelEjercicio( panel );
			if ( vboxContainer.Children.Length > 0 ) {
				vboxContainer.Remove( vboxContainer.Children[0] );	
			}
			vboxContainer.PackStart( panel );
			this.Title = auxEj.NombreEjercicio();
		}
		
	}
}

