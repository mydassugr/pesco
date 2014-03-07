using System;
using System.Collections.Generic;


namespace pesco
{
	/// <summary>
	/// Clase encargada de mostrar la lista de sesiones al usuario
	/// </summary>
	
	
	[System.ComponentModel.ToolboxItem(true)]
	public partial class SessionsTree : Gtk.Bin
	{
		
		// TODO cambiar cadena por icono para indicar si la actividad a sido realizada ya o está pendiente
		
		/// <summary>
		/// Columna para la etiqueta de la sesión
		/// </summary>
		protected Gtk.TreeViewColumn SessionColumn;
		
		/// <summary>
		/// Columna para la acción asociada a la acción
		/// </summary>
		protected Gtk.TreeViewColumn ActionColumn;
		
		/// <summary>
		/// Modelo de la lista
		/// </summary>
		protected SessionsTreeModel model;
		
		/// <summary>
		/// Camino hacia la sesión activa (i.e., la sesión a realizar o reanudar)
		/// </summary>
		protected Gtk.TreePath pathToActiveSesion;
		
		/// <summary>
		/// Constructor de oficio. Le asigna el modelo a la lista
		/// </summary>
		public SessionsTree ()
		{
			this.Build ();
			model = new SessionsTreeModel();	
			arbol.HeadersVisible = false;
			
			arbol.Model = model;
			
			// Creamos las columnas
			SessionColumn = new Gtk.TreeViewColumn ();
			SessionColumn.Title = "Sesión";
				
			ActionColumn = new Gtk.TreeViewColumn ();
			ActionColumn.Title = "Acción";
			
			arbol.AppendColumn(SessionColumn);
			arbol.AppendColumn(ActionColumn);
			
			this.CreateRenders();
			
			pathToActiveSesion = new Gtk.TreePath("0");
			
			arbol.ExpandRow(pathToActiveSesion,true);
			
			arbol.RowActivated += OnRowActivated;
		}
		
		/// <summary>
		/// Función auxiliar para crear los renderers de 
		/// </summary>
		private void CreateRenders(){
			Gtk.CellRendererText sesionRender = new Gtk.CellRendererText();
			Gtk.CellRendererText respuestaRender = new Gtk.CellRendererText();
			
			SessionColumn.PackStart(sesionRender, true);
			ActionColumn.PackStart(respuestaRender, true);
						
			SessionColumn.AddAttribute(sesionRender, "text", 0);
			ActionColumn.AddAttribute(respuestaRender, "text", 1);
		}
		
		/// <summary>
		/// Metodo a ejecutar cuando se active una fila de la tabla mediante doble click
		/// </summary>
		/// <param name="o">
		/// A <see cref="System.Object"/>
		/// </param>
		/// <param name="args">
		/// A <see cref="Gtk.RowActivatedArgs"/>
		/// </param>
		public void OnRowActivated(object o, Gtk.RowActivatedArgs args){
			
			Gtk.TreeIter iter;
			model.GetIter(out iter, args.Path);
			
			// 1 recuperar el valor en la posición 2
			int idEjercicio = Convert.ToInt32(model.GetValue(iter, 2));
			
			// 2 si el valor es -1 es q es una sesión, expandimos la sesión y salimos
			
			if (idEjercicio <= 0)
			{
				arbol.ExpandToPath(args.Path);
				return;
			}
			
			Exercise e = Exercise.GetEjercicio(idEjercicio);
			
			if ( e != null ) {
				
				e.inicializar();
			
				if ( idEjercicio < 4 || idEjercicio == 8 || idEjercicio == 9 || idEjercicio >= 11 )
					e.iniciar();
			}
		}
	}
}

