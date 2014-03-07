using System;
namespace pesco
{
	/// <summary>
	/// 
	/// </summary>
	public class SessionsTreeModel : Gtk.TreeStore
	{
		
		//TODO enganchar con XML con la descripción de las sesiones
		//TODO cambiar cadena por icono para indicar si la actividad a sido realizada ya o está pendiente
		
		/// <summary>
		/// 
		/// </summary>
		public SessionsTreeModel () : base(typeof(string), typeof(string), typeof(int))
		{
			LoadSessions ();
		}

		/// <summary>
		/// 
		/// </summary>
		private void LoadSessions ()
		{
				
			Gtk.TreeIter iter = this.AppendValues ("Todos los ejercicios", "", -1);
			
			this.AppendValues (iter, "Vocales y números", "", 1 );
			this.AppendValues (iter, "Lista de tareas", "", 2 );
			this.AppendValues (iter, "Objetos clasificables", "", 3 );
			this.AppendValues (iter, "Globos", "", 4 );
			this.AppendValues (iter, "Bolsa de objetos", "", 5 );
			this.AppendValues (iter, "Pirámides", "", 6 );
			this.AppendValues (iter, "Números directos", "", 7 );
			this.AppendValues (iter, "Lista de palabras", "", 8 );
			this.AppendValues (iter, "Objetos perdidos", "", 9 );
			this.AppendValues (iter, "Series semánticas", "", 11 );
			this.AppendValues (iter, "Series lógicas", "", 12 );
			this.AppendValues (iter, "Razonamiento espacial", "", 13 );
			this.AppendValues (iter, "Ejercicio de clasificación", "", 14 );
			this.AppendValues (iter, "Analogías semánticas", "", 15 );
			this.AppendValues (iter, "Compra de regalos", "", 16 );
			this.AppendValues (iter, "Reparto de paquetes", "", 17 );
			this.AppendValues (iter, "Recuerdo a largo plazo", "", 18 );
            this.AppendValues (iter, "Lista de Palabras largo plazo", "", 19 );
			this.AppendValues (iter, "Test panel de medallas", "", 40 );
			this.AppendValues (iter, "Test panel de medallas en sesiones", "", 41 );
			this.AppendValues (iter, "Test final de sesion", "", 42 );
			this.AppendValues (iter, "Ayuda sesión 1 - Antes razonamiento", "", 101 );
			this.AppendValues (iter, "Ayuda sesión 1 - Después razonamiento", "", 102 );
				
/*				
			iter = this.AppendValues ("Sesión 1: Registro y Escreening Funcional", "Ver Resultados");
			
			iter = this.AppendValues ("Sesión 2: Escreening Funcional", "Ver Resultados", -1);
			this.AppendValues (iter,"Atención: Pirámides", "", 6);
			this.AppendValues (iter,"Memoria: Lista de Palabras","",  8);
			this.AppendValues (iter,"Memoria: Dígitos directos","",  7);
			this.AppendValues(iter, "Memoria: Vocales y Números", "", 1);
			this.AppendValues(iter, "Razonamiento: Series de Figuras", "", 12);
			this.AppendValues(iter, "Razonamiento: Series Semánticas", "", 11);
			this.AppendValues(iter, "Planificación: Repartidor de Paquetes", "", 17);
			this.AppendValues(iter, "Planificación: Recuerdo a largo plazo", "", 19);
			
			iter = this.AppendValues ("Sesión 3", "Ver Resultados", -1);
			this.AppendValues (iter, "Memoria: Lista de Recados", "", 2);
			this.AppendValues (iter, "Planificación: Compra de Regalos","",  16);
			this.AppendValues (iter, "Memoria: Recuerdo a largo plazo","",  18);
			
			
			iter = this.AppendValues ("Sesión 4", "Ver Resultados", -1);			
			this.AppendValues (iter, "Atención: Globos","",  4);
			this.AppendValues (iter, "Razonamiento: Series","",  13);
			
			
			iter = this.AppendValues ("Sesión 5", "Ver Resultados", -1);
			this.AppendValues (iter, "Atención: Búsqueda de Objetos", "", 9);
			this.AppendValues (iter, "Memoria: Objetos clasificables", "", 3);
			
			iter = this.AppendValues ("Sesión 6", "Ver Resultados", -1);
			this.AppendValues (iter, "Memoria: Bolsa", "", 5);
			this.AppendValues (iter, "Planificación: Compra de Regalos", 16);
			
			iter = this.AppendValues ("Sesión 7", "Ver Resultados", -1);
			this.AppendValues (iter, "Memoria: Lista de Recados","", "",  2);
			this.AppendValues (iter, "Atención: Búsqueda de Objetos","",  9);
			this.AppendValues (iter, "Memoria: Recuerdo a largo plazo","",  18);
			
			iter = this.AppendValues ("Sesión 8", "Ver Resultados", -1);
			this.AppendValues (iter, "Razonamiento: Analogías Semánticas","",  15);
			this.AppendValues (iter, "Atención: Globos", "", 4);
			
			iter = this.AppendValues ("Sesión 9", "Ver Resultados", -1);
			this.AppendValues (iter, "Memoria: Objetos clasificables","",  3);
			this.AppendValues (iter, "Memoria: Bolsa", "", 5);
			
			iter = this.AppendValues ("Sesión 10", "Ver Resultados", -1);
			this.AppendValues (iter, "Memoria: Lista de Recados","",  2);
			this.AppendValues (iter, "Atención: Búsqueda de Objetos","",  20);
			this.AppendValues (iter, "Memoria: Recuerdo a largo plazo","",  18);
			
			iter = this.AppendValues ("Sesión 11", "Ver Resultados", -1);
			this.AppendValues (iter, "Planificación: Compra de Regalos","",  16);
			this.AppendValues (iter, "Memoria: Bolsa","",  5);
			this.AppendValues (iter, "Razonamiento: Classify", "", 14);
			
			iter = this.AppendValues ("Sesión 12: Escreening Funcional", "Ver Resultados", -1);
			this.AppendValues (iter, "Atención: Pirámides", "", 6);
			this.AppendValues (iter, "Memoria: Lista de Palabras","",  8);
			this.AppendValues (iter, "Memoria: Dígitos directos","",  7);
			this.AppendValues(iter, "Memoria: Vocales y Números", "", 1);
			this.AppendValues(iter, "Razonamiento: Series lógicas", "", 12);
			this.AppendValues(iter, "Razonamiento: Series Semánticas", "", 11);
			this.AppendValues(iter, "Planificación: Repartidor de Paquetes", "", -1);
*/
		}
	}
}

