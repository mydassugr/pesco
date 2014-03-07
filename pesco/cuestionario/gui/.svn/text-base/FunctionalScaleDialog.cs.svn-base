
using System;
using Gtk;

namespace pesco
{
	public partial class FunctionalScaleDialog : Gtk.Bin
	{
		protected AnswersList lp = null;
		protected ResponseType response;
		protected Questionary quest = null;
		
		public Questionary Quest {
			get {
				return this.quest;
			}
		}
			
		protected string ficheroCuestionario;
		
		public new ResponseType Response {
			get {
				return response;
			}
			set {
				response = value;	
			}
		}
		
		public static FunctionalScaleDialog GetDailyLifeQuestionaryDialog()
		{
			Questionary q = Questionary.GetDailyLifeQuestionary();
			FunctionalScaleDialog fsd = new FunctionalScaleDialog(q);
			
			fsd.Hide(); 
			
			return fsd;
		}
		
		
		public int RunDialog()
		{
			// actualizamos la lista de preguntas
			UpdateList (botonSiguiente);
			//return this.Run();	
			return 1;
		}
		
		public static FunctionalScaleDialog GetInstrumentalActivitiesQuestionaryDialog()
		{
			Questionary q = Questionary.GetInstrumentalActivitiesQuestionary();
			FunctionalScaleDialog fsd = new FunctionalScaleDialog(q);
			fsd.Hide();
			return fsd;
		}
		
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="nFichero">
		/// A <see cref="System.String"/>
		/// </param>
		private FunctionalScaleDialog (Questionary q)
		{
			
			
			quest = q;
			CheckNextQuestion();
			this.Build ();		
			
			
			// oculatamos por defecto los botones de guardar y anterior
			botonGuardar.Hide ();
			botonAtras.Hide ();
			
			bool QuestionaryAlreadyDone = false;
			// If we have finished the questionary previously, finish exercise
			if ( quest.NextQuestion == quest.Questions.Count ) {
				SessionManager.GetInstance().ExerciseFinished(-1);
				SessionManager.GetInstance().TakeControl();
				QuestionaryAlreadyDone = true;
			}
			
			// controlamos que si es la última pregunta se ponga le mensaje de guardar en vez de siguiente
			if (quest.NextQuestion == quest.Questions.Count - 1) {
				botonSiguiente.Hide ();
				botonGuardar.Show ();
			}
			
			// si es la primera pregunta ocultamos el boton de pregunta previa
			if (quest.NextQuestion > 0) {
				botonAtras.Show ();
			}
     
			
			GtkUtil.SetStyle(vbox4, Configuration.Current.ButtonFont);
			GtkUtil.SetStyle(this.botonAtras, Configuration.Current.ButtonFont);
			GtkUtil.SetStyle(this.botonGuardar, Configuration.Current.ButtonFont);
			GtkUtil.SetStyle(this.buttonCancel, Configuration.Current.ButtonFont);
			GtkUtil.SetStyle(this.botonSiguiente, Configuration.Current.ButtonFont);
			
			this.buttonCancel.Clicked += this.OnCancelScale;
			
			if ( !QuestionaryAlreadyDone ) 
				UpdateList (botonSiguiente);
		}
		
		public void CheckNextQuestion(){
			
			int questionNumber = 0;
			//int maxScore =0;
			
			foreach( Question question in Quest.Questions){
				QuestionResult qr= null;
				foreach(Answer answer in question.Answers){
					
					if(answer.Selected== true){
						questionNumber ++;
						
						//qr= new QuestionResult(question.Question_, answer.Text, answer.Score,0);
					}
					//if(maxScore < answer.Score)
					//	maxScore= answer.Score;
					
				}
				/*if(qr !=null){
					qr.puntuacionMax=maxScore;
					Quest.QuestionsResults.QuestionsResults.Add(qr);
				}*/
			}
			
			
			
			Quest.NextQuestion = questionNumber;
		}
		
		/// <summary>
		/// Actualiza la pregunta mostrada en la ventana, de acuerdo con la pregunta siguiente
		/// </summary>
		private void UpdateList (Button b)
		{
			if (lp != null)
				vbox4.Remove (lp);
			
			//b.Sensitive = false;
			
			lp = new AnswersList (quest, b);
			
			
			vbox4.PackStart(lp,true,true,0);
			//GtkUtil.Put(vbox4, lp, 0);
			//this.Maximize();
			lp.ShowAll ();
		}
		
		#region event handlers
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender">
		/// A <see cref="System.Object"/>
		/// </param>
		/// <param name="e">
		/// A <see cref="System.EventArgs"/>
		/// </param>
		protected virtual void OnCancelScale (object sender, System.EventArgs e)
		{
			//this.CloseDialog();
		}
		
		private void CloseDialog()
		{
			//response = ResponseType.Cancel;
			//Quest.Serialize();
			//this.Respond(response);
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender">
		/// A <see cref="System.Object"/>
		/// </param>
		/// <param name="e">
		/// A <see cref="System.EventArgs"/>
		/// </param>
		protected virtual void OnPreviousQuestion (object sender, System.EventArgs e)
		{
			quest.NextQuestion--;
			
			if (quest.NextQuestion < quest.Questions.Count) {
				
				UpdateList (botonSiguiente);
				
				if (quest.NextQuestion == 0) {
					botonAtras.Hide ();
				}
				
				botonGuardar.Hide ();
				botonSiguiente.Show ();
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender">
		/// A <see cref="System.Object"/>
		/// </param>
		/// <param name="e">
		/// A <see cref="System.EventArgs"/>
		/// </param>
		protected virtual void OnNextQuestion (object sender, System.EventArgs e)
		{
			// almacenamos la elección del Usuario
			
			if (!StoreAnswer ())
				return;
			
			quest.Serialize ();
			quest.NextQuestion++;
			
			if (quest.NextQuestion < quest.Questions.Count) {
				
				if (quest.NextQuestion == quest.Questions.Count - 1) {
					botonSiguiente.Hide ();
					botonGuardar.Show ();
					UpdateList (botonGuardar);
				} else {
					botonGuardar.Hide ();
					botonSiguiente.Show ();
					UpdateList (botonSiguiente);
				}
				
				if (quest.NextQuestion > 0) {
					botonAtras.Show ();
				}
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender">
		/// A <see cref="System.Object"/>
		/// </param>
		/// <param name="e">
		/// A <see cref="System.EventArgs"/>
		/// </param>
		protected virtual void OnSaveEscala (object sender, System.EventArgs e)
		{ 
			
			//TODO arreglar problema con la pulsación en el botón guardar que hace que se emita la 
			// respuesta del diálogo sin haber seleccionado la última pregunta
			if (!StoreAnswer ())
			{
				Console.WriteLine("Falla al guardar!!!!");
				return;
			}
			quest.NextQuestion++;
			
			quest.Serialize ();
			
			SessionManager.GetInstance().ExerciseFinished(-1);
			SessionManager.GetInstance().TakeControl();

		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender">
		/// A <see cref="System.Object"/>
		/// </param>
		/// <param name="e">
		/// A <see cref="System.EventArgs"/>
		/// </param>
		protected virtual void OnCloseDialog2 (object sender, System.EventArgs e)
		{
			quest.Serialize();
			response = ResponseType.Cancel;
			
		}
		#endregion
		
		/// <summary>
		/// Almacena la respuesta escogida para una pregunta.
		/// </summary>
		/// <returns>
		/// A <see cref="System.Boolean"/> indicando si se ha almacenado correctamente la respuesta elegida
		/// </returns>
		private bool StoreAnswer ()
		{
			// si la lista existe
			if (lp != null) {
				
				// buscamos la respuesta seleccionada en la lista de respuestas
				int selec = lp.GetSelectedAnswer ();
				
				// si no hay ninguna seleccionada devolvemos false
				if (selec == -1){
					MessageDialog md = new MessageDialog ( null, 
										DialogFlags.DestroyWithParent,
										MessageType.Error, 
										ButtonsType.Ok, "<span size=\"xx-large\">Debes selecionar una opción para continuar. Si tienes dudas consulta al monitor de la sala.</span>");
					
					GtkUtil.SetStyleRecursive( md, Configuration.Current.MediumFont );
					this.Sensitive=false;
					md.Show();
					md.GdkWindow.Cursor= GtkUtil.ChangeCursor();
					ResponseType result = (ResponseType)md.Run ();	
														
					if(result == ResponseType.Ok){
						this.Sensitive=true;
					}
					md.Destroy ();
					return false;
				}
				quest.SetAnswer (quest.NextQuestion, selec, true);
			}
			else 
				return false;
			
			// todo ha ido ok => devolvemos true
			return true;
		}
		protected virtual void OnDeleteDialog (object o, Gtk.DeleteEventArgs args)
		{
			Quest.Serialize();
		}
	}
}
