using System;
using System.Collections.Generic;

using System.Xml;

using System.Xml.Serialization;
using System.IO;

using Gdk;
using Gtk;

namespace pesco
{
	[XmlRoot("logic-series-exercise")]
	public class LogicalSeriesExercise : CompleteSeriesExercise<LogicalSeriesElement, LogicalSeriesPanel>
	{
		
		public static string xmlUserFile = "logical-series.xml";
		
		[XmlIgnoreAttribute]
		protected ReasoningExerciseSesionResults finalRes = XmlUtil.DeserializeForUser<ReasoningExerciseSesionResults>(Configuration.Current.GetExerciseConfigurationFolderPath () + "/LogicalSeries.xml");
		
		[XmlIgnoreAttribute]
		int prePos=0;
		[XmlIgnoreAttribute]
		int postPos=0;
		[XmlIgnoreAttribute]
		protected List <int> preList = new List<int>(new int[] {1,2,17,18,26,15});
		[XmlIgnoreAttribute]
		protected List <int> postList = new List<int>(new int[]{17,11,8,19,28,30});	
		#region Exercise Interface

		public override void ayuda ()
		{
			panel.ExplanationText = this.CurrentSeries.Explanation;
			panel.ShowExplanation();
		}

		public override int idEjercicio ()
		{
			return 12;
		}

		public override void iniciar(){
			
			// inicializamos los resultados 
			if(finalRes.ReasoningExerciceExecutionResults == null){
				finalRes = new ReasoningExerciseSesionResults(false);
				
			}
			currentSesionResults = new ReasoningExerciseSesionResults(false);
			finalRes.CategoryId=Convert.ToInt16(ExerciseCategory.Reasoning);
			finalRes.CurrentLevel=Convert.ToInt16( this.CurrentLevel);
			finalRes.ExerciseId= this.idEjercicio();
			
			ExerciceExecutionResult <SingleResultReasoningExercice> exerciceExecution =  new ExerciceExecutionResult<SingleResultReasoningExercice>(SessionManager.GetInstance().CurSession.IdSession, SessionManager.GetInstance().CurExecInd);
			finalRes.ReasoningExerciceExecutionResults.Add(exerciceExecution);
			currentSesionResults.ReasoningExerciceExecutionResults.Add(exerciceExecution);
			
			// Changing mode to "demo" mode
			SessionManager.GetInstance().ChangeExerciseStatus("demo");
			
			// Creating panels
			ExerciseDemoLogicalSeries demols= new ExerciseDemoLogicalSeries(this);
			SessionManager.GetInstance().ReplacePanel( demols );
            demols.InitPanel();
		}
		
		public override bool inicializar ()
		{
            
			this.panel = new LogicalSeriesPanel ();
			/*this.window = new Gtk.Window ("Razonamiento >> Complete la serie");*/
			this.instructions = 
				"Durante el ejercicio, en la parte superior de la pantalla te mostraré una serie de figuras " +
				"donde falta el último elemento. Y en la parte inferior de la pantalla debes seleccionar la figura que completaría la serie. " +
				 "Abajo, tienes un ejemplo donde se ha seleccionado la figura que completa la serie.\n\nSelecciona la figura que " +
				 "creas correcta pulsando sobre ella (se marcará en amarillo). Pulsa el botón " +
				 "<span color='black'><b>¡Listo!</b></span> para ver si tu respuesta es correcta. Tu respuesta se marcará en verde si es correcta y en rojo en caso contrario. Pulsa botón <span color='black'><b>Ayuda</b></span> para obtener una pista.";
			
			this.instructionsImageResourceName = "pesco.ejercicios.Reasoning.LogicalSeries.figures.Pantallazo.png";
			
			panel.ExplanationText = currentSeries.Explanation;
			panel.HelpButton.Clicked += this.OnClickHelp;
			bool activo=true;
			
			// the event checks the answer of the user, store it a generate the next 
			panel.ReadyButton.Clicked += delegate(object sender, EventArgs e) {
				//Show the exercice answer
				
				if (!errorMostrado  || screeningMode)
				{		
					activo=panel.SolutionSelected();
					if(activo){
						end = DateTime.Now;
						TimeSpan timespan = end-start ;
						
						//1. Check the answer and append it to the results list
						
						//currentSesionResults.Add(this.CheckAnswer(panel.GetSelectedItems()));
						SingleResultReasoningExercice singleReasoningExercice=this.getReasoningResult( panel.GetSelectedItems());
						singleReasoningExercice.TimeElapsed = Convert.ToInt16( timespan.TotalSeconds);
					
						
						finalRes.ReasoningExerciceExecutionResults[finalRes.ReasoningExerciceExecutionResults.Count -1].SingleResults.Add(singleReasoningExercice);
						//currentSesionResults.ReasoningExerciceExecutionResults[currentSesionResults.ReasoningExerciceExecutionResults.Count -1].SingleResults.Add(singleReasoningExercice);
						
						SessionManager.GetInstance().RepetitionFinished();
						
						
						
						panel.ShowLogicalSeriesCorrectAnswers<LogicalSeriesElement>(currentSeries.CorrectElements, panel.GetSelectedItems());
						panel.ExplanationText = currentSeries.Explanation;
						
						
						//If the answer is right
						//ReasoningExerciseResult r = this.currentSesionResults.Results[this.currentSesionResults.Results.Count-1];
						if( singleReasoningExercice.Result.Equals("Fail"))
							panel.ShowCorrectExplanation(false);
						else 
							panel.ShowCorrectExplanation(true);
						
						panel.HideHelpButton();
						
						GtkUtil.PimpButtonFromStock(panel.ReadyButton, "Siguiente", "gtk-go-forward");
						GtkUtil.SetStyle(panel.ReadyButton, Configuration.Current.ButtonFont);
						
					}
					else{
						Gtk.MessageDialog md = new Gtk.MessageDialog (null, Gtk.DialogFlags.DestroyWithParent, 
					                                              Gtk.MessageType.Error, Gtk.ButtonsType.Ok, 
					                                              "<big><big><big>Debes elegir alguna solución, para poder continuar.</big></big></big>");					
						GtkUtil.SetStyleRecursive( md, Configuration.Current.MediumFont );
						Gtk.ResponseType result = (Gtk.ResponseType)md.Run ();
						md.Destroy ();
					}
					//Disable buttons
					//panel.DisableOptionsContainer();
					
				}
				//Show the exercice 
				if (errorMostrado || screeningMode) {
				
					//2. Generates the next series & adds the new series to the panel
					if (!Next(this.currentLevel))
						
						this.finishExercice();
					else {
						PopulatePanel();
						panel.ShowAll();
                        panel.HideHelpButton();
						panel.HideExplanation();				
						GtkUtil.PimpButtonFromStock(panel.ReadyButton, "¡Listo!", "gtk-apply");
						GtkUtil.SetStyle(panel.ReadyButton, Configuration.Current.ButtonFont);
						start = DateTime.Now;
					}
					
				}
				if(activo)
					errorMostrado = !errorMostrado;
				
			};
			return true;
		}
		
		public override string NombreEjercicio ()
		{
			return "Series Lógicas";
		}
		
		/// <summary>
		/// Selects a random series with a certain difficulty
		/// </summary>
		/// <param name="d">
		/// A <see cref="ReasoningExerciseDifficulty"/> 
		/// </param>
		protected override bool Next (ReasoningExerciseDifficulty d)
		{
			if(SessionManager.GetInstance().CurSession.IdSession ==1){
				
				if (SessionManager.GetInstance ().HaveToFinishCurrentExercise ()) {
						Console.WriteLine ("OVER Max number of executions!!!!");
						return false;
				}
				//SomeObject desiredObject =     myObjects.Find(delegate(SomeObject o) { return o.Id == desiredId; });
				
				Series <LogicalSeriesElement> se = seriesPool.Find ( delegate (Series <LogicalSeriesElement> o) {return o.ID== preList[prePos]; });
				this.currentSeries = se;
				prePos ++;
				
				return true;
			}
			else{
				if(SessionManager.GetInstance().CurSession.IdSession ==11){
					if (SessionManager.GetInstance ().HaveToFinishCurrentExercise ()) {
						Console.WriteLine ("OVER Max number of executions!!!!");
						return false;
					}
					//SomeObject desiredObject =     myObjects.Find(delegate(SomeObject o) { return o.Id == desiredId; });
					
					Series <LogicalSeriesElement> se = seriesPool.Find ( delegate (Series <LogicalSeriesElement> o) {return o.ID== postList[postPos]; });
					this.currentSeries = se;
					postPos ++;
					
					return true;
				}
				else{
					base.Next (d);
					
					if (SessionManager.GetInstance ().HaveToFinishCurrentExercise ()) {
						Console.WriteLine ("OVER Max number of executions!!!!");
						return false;
					}
					
					// if there are series to choose
					if (seriesPool.Count > 0) {
						// select all the series with difficulty = d
						List<Series<LogicalSeriesElement>> pool = new List<Series<LogicalSeriesElement>> ();
						
						foreach (Series<LogicalSeriesElement> s in seriesPool)
							if (s.Difficulty == d && !preList.Contains(s.ID) && !postList.Contains(s.ID))
								pool.Add (s);
						
						// shuffle the list of available exercises
						TaskListExercise.Shuffle<Series<LogicalSeriesElement>>(pool);
						
						// select random series
						if (pool.Count > 0) {
							int contador = 0;
							
							// tries to select a yet-not-showed series
							do {
								this.currentSeries = pool[contador];
								contador++;
							} while (this.showedExercises.Contains (currentSeries.ID) && contador < pool.Count);
							
							// if all the series with level d have been shown already => finish exercise
							if (contador >= pool.Count) {
								/*Gtk.MessageDialog md = new Gtk.MessageDialog (window, Gtk.DialogFlags.DestroyWithParent, Gtk.MessageType.Error, Gtk.ButtonsType.Ok, "Actualmente no existen ejercicios de tu nivel actual");						
								md.Run ();
								md.Destroy ();*/
								return false;
							} else { 
		
								this.showedExercises.Add (currentSeries.ID);						
								TaskListExercise.Shuffle<LogicalSeriesElement> (currentSeries.Options);
								repetitions++;
								
								Console.WriteLine(this.showedExercises);
								Console.WriteLine(currentSeries.ID);
								
								return true;
							}
							
						// there are no series with level d :( 
						} else {
							/*Gtk.MessageDialog md = new Gtk.MessageDialog (window, Gtk.DialogFlags.DestroyWithParent, Gtk.MessageType.Error, Gtk.ButtonsType.Ok, "Actualmente no existen ejercicios de tu nivel actual");
							
							md.Run ();
							md.Destroy ();*/
							return false;
						}
					// there are no series to choose :/
					} else {
						/*Gtk.MessageDialog md = new Gtk.MessageDialog (window, Gtk.DialogFlags.DestroyWithParent, Gtk.MessageType.Error, Gtk.ButtonsType.Ok, "Actualmente no existen ejercicios de este tipo");
						
						md.Run ();
						md.Destroy ();*/
						return false;
					}
				}
			}
						
		
			
			return true;
		}	
		public override void pausa ()
		{
			throw new NotImplementedException ();
		}

		/// <summary>
		/// 
		/// </summary>
		#endregion
		
		
		public SingleResultReasoningExercice getReasoningResult (List<LogicalSeriesElement> selected){
			
			
			SingleResultReasoningExercice sr = new SingleResultReasoningExercice();
			if ( selected.Count== 0 || selected.Count != currentSeries.CorrectElements.Count)
			{
				sr.Result = "Fail";
			}
			else if (selected[0].PositionId ==currentSeries.CorrectElements[0].PositionId)
			{
				sr.Result = "Valid";
			}
			else 
			{
				sr.Result = "Fail";
			}
			
			sr.HelpUsed=withHelp;
			sr.Level = this.CurrentLevel.ToString();
			sr.QuestionId = CurrentSeries.ID;
			sr.AnswerIdSelected = selected[0].PositionId;
			
			return sr;
		}
		
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="selected">
		/// A <see cref="List<LogicalSeriesElement>"/>
		/// </param>
		/// <returns>
		/// A <see cref="ReasoningExerciseResult"/>
		/// </returns>
		public override ReasoningExerciseResult CheckAnswer (List<LogicalSeriesElement> selected)
		{
			//Console.WriteLine("-seleccionado " +  selected[0].ValueOfElement + "\n-Correcto " + correctElements[0].ValueOfElement);
			
			
			ReasoningExerciseResult r = new ReasoningExerciseResult ();
			if (selected.Count== 0 || selected.Count != currentSeries.CorrectElements.Count) {
				r.Errores = 1;
				r.Aciertos = 0;
				r.Omisiones = currentSeries.CorrectElements.Count - r.Aciertos;				
			} else if (selected.Contains (currentSeries.CorrectElements[0])) {
				r.Errores = 0;
				r.Aciertos = 1;
				r.Omisiones = currentSeries.CorrectElements.Count - r.Aciertos;
			} else {
				r.Errores = 1;
				r.Aciertos = 0;
				r.Omisiones = currentSeries.CorrectElements.Count - r.Aciertos;
			}
			
			r.Tiempo = end - start;
			r.Id = this.currentSeries.ID;
			r.Ayuda= withHelp;
			//Console.WriteLine (r);
			
			return r;
		}
		
		protected void SetFinalResult(int scoreArg){
			
			finalRes.ReasoningExerciceExecutionResults[finalRes.ReasoningExerciceExecutionResults.Count -1].TotalCorrects =0;
			finalRes.ReasoningExerciceExecutionResults[finalRes.ReasoningExerciceExecutionResults.Count -1].TotalFails =0;
			finalRes.ReasoningExerciceExecutionResults[finalRes.ReasoningExerciceExecutionResults.Count -1].TotalTimeElapsed =0;
			finalRes.ReasoningExerciceExecutionResults[finalRes.ReasoningExerciceExecutionResults.Count -1].TotalHelpCounter =0;
			finalRes.ReasoningExerciceExecutionResults[finalRes.ReasoningExerciceExecutionResults.Count -1].Score =scoreArg;
			foreach(SingleResultReasoningExercice sr in finalRes.ReasoningExerciceExecutionResults[finalRes.ReasoningExerciceExecutionResults.Count -1].SingleResults){
				if( sr.Result.Equals("Valid"))
					finalRes.ReasoningExerciceExecutionResults[finalRes.ReasoningExerciceExecutionResults.Count -1].TotalCorrects ++;
				
				if( sr.Result.Equals("Fail"))
					finalRes.ReasoningExerciceExecutionResults[finalRes.ReasoningExerciceExecutionResults.Count -1].TotalFails ++;
			
				if(sr.HelpUsed)
					finalRes.ReasoningExerciceExecutionResults[finalRes.ReasoningExerciceExecutionResults.Count -1].TotalHelpCounter ++;
				//finalRes.VowelsNumberExecutionResults[finalRes.VowelsNumberExecutionResults.Count -1].TotalTimeElapsed += sr.TimeElapsed;
				
			}
			
			finalRes.CurrentLevel=Convert.ToInt16( this.CurrentLevel);
		}
		
		#region XML serialization
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="filename">
		/// A <see cref="System.String"/>
		/// </param>
		public override void Serialize(int scoreArg){
			
			if( finalRes.ReasoningExerciceExecutionResults !=null){
				SetFinalResult(scoreArg);
				XmlUtil.SerializeForUser<ReasoningExerciseSesionResults>(finalRes,Configuration.Current.GetExerciseConfigurationFolderPath () + "/LogicalSeries.xml");
			}
			
			/*string fullPath = Configuration.Current.GetExerciseConfigurationFolderPath() + "/" + xmlUserFile;
			
			XmlTextWriter escritor = new XmlTextWriter(fullPath, null);
		
			try
			{
				escritor.Formatting = Formatting.Indented;
				
				escritor.WriteStartDocument();
				
				escritor.WriteDocType("logical-series-exercise", null, null, null);
				
				// hoja de estilo para pode ver en un navegador el xml
				//escritor.WriteProcessingInstruction("xml-stylesheet", "type='text/xsl' href='cuestionario.xsl'");
				
				XmlSerializer serializer = new XmlSerializer(typeof(LogicalSeriesExercise));
				
				XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
				
				namespaces.Add("","");
				
				serializer.Serialize(escritor, this, namespaces);
				
				escritor.WriteEndDocument();
				escritor.Close();
			}
			catch(Exception e)
			{
				escritor.Close();
				//Console.WriteLine("Error al serializar" +  e.Message);
			}*/
		}	
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="filename">
		/// A <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// A <see cref="LogicalSeriesExercise"/>
		/// </returns>
		public static  LogicalSeriesExercise Deserialize()
		{
			string fullPath = Configuration.Current.GetExerciseConfigurationFolderPath() + "/" + xmlUserFile;
			
			if (!File.Exists(fullPath))
			{
				string s = Environment.CommandLine;			
				fullPath = Configuration.CommandDirectory + "/ejercicios/Reasoning/LogicalSeries/xml-templates/" + xmlUserFile;
			}
			
			XmlTextReader lector = new XmlTextReader(fullPath);
			try
			{
				exercise = new LogicalSeriesExercise();
				
				XmlSerializer serializer = new XmlSerializer(typeof(LogicalSeriesExercise));				
				exercise = (LogicalSeriesExercise) serializer.Deserialize(lector);
				
				lector.Close();				
				
			}
			catch( Exception e)
			{
				lector.Close();
				return null;
			}
			
			LogicalSeriesExercise lseAux = Deserialize(Configuration.CommandDirectory + "/ejercicios/Reasoning/LogicalSeries/xml-templates/" + xmlUserFile);
			((LogicalSeriesExercise) exercise).SeriesPool = lseAux.SeriesPool;
				
			return (LogicalSeriesExercise) exercise;
		}
		
		public static  LogicalSeriesExercise Deserialize(string path)
		{
		
			XmlTextReader lector = new XmlTextReader(path);
				try
				{
					LogicalSeriesExercise ex = new LogicalSeriesExercise();
					
					XmlSerializer serializer = new XmlSerializer(typeof(LogicalSeriesExercise));				
					ex = (LogicalSeriesExercise) serializer.Deserialize(lector);
					
					lector.Close();				
					return (LogicalSeriesExercise) ex;
				}
				catch( Exception e)
				{
					lector.Close();
					return null;
				}
		}
		#endregion
		
		protected override void PopulatePanel ()
		{
			panel.Populate(currentSeries.Samples, currentSeries.Options, currentSeries.Position);
       	}
	}
}

