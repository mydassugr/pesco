using System;
using Gtk;


namespace pesco
{
	/// <summary>
	/// Pinta una lista con las respuestas para una pregunta. El modelo de esta lista
	/// siguiendo el patron MVC es implementado en la clase <see cref="ModeloRespuestas" />
	/// </summary>
	[System.ComponentModel.ToolboxItem(true)]
	public partial class AnswersList : Gtk.Bin
	{
		/// <summary>
		/// 
		/// </summary>
		Gtk.TreeViewColumn AnswerRow;

		/// <summary>
		/// 
		/// </summary>
		Gtk.TreeViewColumn ChoiceRow;

		/// <summary>
		/// 
		/// </summary>
		AnswesListModel model;

		/// <summary>
		/// 
		/// </summary>
		Questionary questionary;

		//
		Button button;


		/// <summary>
		/// 
		/// </summary>
		/// <param name="c">
		/// A <see cref="Cuestionario"/>
		/// </param>
		public AnswersList (Questionary c, Button b)
		{
			
			this.Build ();
			button = b;
			
			image5.Pixbuf = Gdk.Pixbuf.LoadFromResource(c.Questions[c.NextQuestion].Imagen);
			
			titulo.Markup = "<b>" + c.Questions[c.NextQuestion].Question_ + "</b>";
			GtkUtil.SetStyle(titulo, Configuration.Current.LargeFont);
			GtkUtil.SetStyle(subtitulo, Configuration.Current.SmallFont);
			questionary = c;
			
			foreach(Answer ans in c.Questions[c.NextQuestion].Answers){
				
				QuestionaryToggleBtn qToggleBtn=null;
				if(c.Questions[c.NextQuestion].Answers.Count >=5)
					qToggleBtn = new QuestionaryToggleBtn(ans.Text,80);
				else{
					qToggleBtn = new QuestionaryToggleBtn(ans.Text);
					GtkUtil.SetStyle(qToggleBtn, Configuration.Current.ButtonFont);
				}
				
				optionButtons.PackStart(qToggleBtn,true,false,0);
				
				if(ans.Selected==true)
					qToggleBtn.Active=true;
			}
			
		}

	
		
		/// <summary>
		/// 
		/// </summary>
		/// <returns>
		/// A <see cref="System.Int32"/>
		/// </returns>
		public int GetSelectedAnswer ()
		{
			int selectedAnswer =0;
			foreach( QuestionaryToggleBtn qToggleBtn in optionButtons.Children){
				if(qToggleBtn.Active==true)
					return selectedAnswer;
				else selectedAnswer ++;
			}
			// Console.WriteLine( "Respuesta seleccionada: "+selectedAnswer );
			//return selectedAnswer;
			return -1;
		}
		
	}
}

