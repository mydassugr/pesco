using System;
using Gtk;

namespace pesco
{
	/// <summary>
	/// Esta clase se corresponde con el modelo (dentro del patrón MVC) de la clase
	/// <see cref="ListaPregunta" />.
	/// </summary>
	public class AnswesListModel : ListStore
	{
		protected String selectedPath = null;
		Questionary questionary;
		Question p;
		
		/// <summary>
		/// 
		/// </summary>
		public AnswesListModel () : base(typeof(string), typeof(bool))
		{
			LoadQuestions ();
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="c">
		/// A <see cref="Cuestionario"/>
		/// </param>
		public AnswesListModel (Questionary c) : base(typeof(string), typeof(bool))
		{
			questionary = c;
			
			p = questionary.Questions[questionary.NextQuestion];
			int i = 1;
				
				
			foreach (Answer r in p.Answers){
				this.AppendValues(i+". "+r.Text+"\n", r.Selected);
				i++;
			}
		}
		
		/// <summary>
		/// Método para cargar las preguntas. De momento es un STUB.
		/// </summary>
		private void LoadQuestions ()
		{
			this.AppendValues ("Respuesta 1", false);
			this.AppendValues ("Respuesta 2", false);
			this.AppendValues ("Respuesta 3", false);
			this.AppendValues ("Respuesta 4", false);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="o">
		/// A <see cref="System.Object"/>
		/// </param>
		/// <param name="args">
		/// A <see cref="ToggledArgs"/>
		/// </param>
		public void SelectAnswer (object o, ToggledArgs args)
		{
			// iterador sobre el árbol
			Gtk.TreeIter iter;
			this.GetIterFirst(out iter);
		
			do {
  				SetValue(iter, 1, false); // los demás elementos
			}while(IterNext(ref iter));
			
			
			if (GetIter (out iter, new TreePath (args.Path))) {
				SetValue (iter, 1, true);
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <returns>
		/// A <see cref="System.Int32"/>
		/// </returns>
		public int GetSelectedAnswer(){
			// iterador sobre el árbol
			Gtk.TreeIter iter;
			this.GetIterFirst(out iter);
			int posicion = 0, seleccionada=-1;
			bool valor;
		
			do {
  				valor = (bool)GetValue(iter, 1); // los demás elementos
				
				if (valor) seleccionada = posicion;
				posicion++;
			}while(IterNext(ref iter));
			
			return seleccionada;
		}
		
	}
}
