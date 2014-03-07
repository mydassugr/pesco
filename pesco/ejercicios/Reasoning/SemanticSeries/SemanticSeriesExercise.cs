using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace pesco
{
	[XmlRoot("semantic-series-exercise")]
	public class SemanticSeriesExercise : SelectElementsExercise<SemanticSeriesElement, SemanticSeriesPanel>
	{
		/// <summary>
		/// 
		/// </summary>
		public static string xmlUserFile = "semantic-series.xml";
		
		[XmlIgnoreAttribute]
		protected ReasoningExerciseSesionResults finalRes = XmlUtil.DeserializeForUser<ReasoningExerciseSesionResults>(Configuration.Current.GetExerciseConfigurationFolderPath () + "/SemanticSeries.xml");
		
		[XmlIgnoreAttribute]
		int prePos=0;
		[XmlIgnoreAttribute]
		int postPos=0;
		[XmlIgnoreAttribute]
		protected List <int> preList = new List<int>(new int[]{1,2,12,23,25,16});
		[XmlIgnoreAttribute]
		protected List <int> postList = new List<int>(new int[] {15,17,22,24,13,3});	
		#region Abstract methods of Exercise

		public override void ayuda ()
		{
			panel.ExplanationText = this.currentList.Explanation;
			panel.ShowExplanation();
		}

		public override int idEjercicio ()
		{
			return 11;
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
           ExerciseDemoSemanticSeries WinDemoSs= new ExerciseDemoSemanticSeries (this);
           SessionManager.GetInstance().ReplacePanel(WinDemoSs);
           WinDemoSs.InitPanel();
		}	
				
		public override bool inicializar ()
		{
			this.panel = new SemanticSeriesPanel();
			bool activo=true;
			//this.window = new Gtk.Window("Razonamiento >> " + this.NombreEjercicio());
			
			this.instructions = "<big><big>¿Cúal de las siguientes palabras no encaja?</big></big>";
			this.instructionsImageResourceName = "pesco.ejercicios.Reasoning.LogicalSeries.figures.semantic-series.png";
			
			
			panel.ExplanationText = currentList.Explanation;			
			panel.HelpButton.Clicked += this.OnClickHelp;
		
			// the event checks the answer of the user, store it a generate the next 
			panel.ReadyButton.Clicked += delegate(object sender, EventArgs e) {
				if (!errorMostrado || screeningMode )
				{	
					activo=panel.SolutionSelected();
					if(activo){
						end = DateTime.Now;
						TimeSpan timespan = end -start ;
						
						//1. Check the answer and append it to the results list
						//currentSesionResults.Add(this.CheckAnswer(panel.GetSelectedItems()));
						SingleResultReasoningExercice singleReasoningExercice=this.getReasoningResult( panel.GetSelectedItems());
						singleReasoningExercice.TimeElapsed = Convert.ToInt16( timespan.TotalSeconds);
					
						finalRes.ReasoningExerciceExecutionResults[finalRes.ReasoningExerciceExecutionResults.Count -1].SingleResults.Add(singleReasoningExercice);
						//currentSesionResults.ReasoningExerciceExecutionResults[currentSesionResults.ReasoningExerciceExecutionResults.Count -1].SingleResults.Add(singleReasoningExercice);
						SessionManager.GetInstance().RepetitionFinished();
						
						
						panel.ShowCorrectAnswers<SemanticSeriesElement>(currentList.WrongElements, panel.GetSelectedItems());
						panel.ExplanationText = currentList.Explanation; 
						
						//If the answer is right
						/*ReasoningExerciseResult r = this.currentSesionResults.Results[this.currentSesionResults.Results.Count-1];
						
						if(r.Aciertos == 0)
							panel.ShowCorrectExplanation(false);
						else 
							panel.ShowCorrectExplanation(true);*/
						
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
					
				}
				if (errorMostrado || screeningMode) {
				
					//2. Generates the next series & adds the new series to the panel
					if (!Next(this.currentLevel))
						this.finishExercice();
					else {
						PopulatePanel();
						panel.ShowAll();
						panel.HideExplanation();
						panel.HideHelpButton();
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

		public  override string NombreEjercicio ()
		{
			return "¿Qué palabra sobra?";
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
								
				ListOfElements <SemanticSeriesElement> se = PoolOfElements.Find ( delegate (ListOfElements <SemanticSeriesElement> o) {return o.ID== preList[prePos]; });
				this.currentList = se;
				prePos ++;
				
				return true;
			}
			else{
				if(SessionManager.GetInstance().CurSession.IdSession ==11){
					if (SessionManager.GetInstance ().HaveToFinishCurrentExercise ()) {
						Console.WriteLine ("OVER Max number of executions!!!!");
						return false;
					}
										
					ListOfElements <SemanticSeriesElement> se = PoolOfElements.Find ( delegate (ListOfElements <SemanticSeriesElement> o) {return o.ID== postList[postPos]; });
					this.currentList = se;
					postPos ++;
					
					return true;
				}
				else{
					if (SessionManager.GetInstance().HaveToFinishCurrentExercise())
					{
						// Console.WriteLine ("End of exercise.");
						return false;
					}
					
					
					if (poolOfElements.Count > 0)
					{		
						List<ListOfElements<SemanticSeriesElement>> pool = new List<ListOfElements<SemanticSeriesElement>>();
						
						foreach(ListOfElements<SemanticSeriesElement> s in poolOfElements)
							if (s.Difficulty == d && !preList.Contains(s.ID) && !postList.Contains(s.ID))
								pool.Add(s);
						
						TaskListExercise.Shuffle<ListOfElements<SemanticSeriesElement>>(pool);
						
						if (pool.Count > 0)
						{
							int contador = 0;
							// tries to select a yet-not-showed series
							do {
								this.currentList = pool[contador];
								contador++;
							} while (this.showedExercises.Contains (currentList.ID) && contador < pool.Count);
							
							// if all the series with level d have been shown already => finish exercise
							if (contador >= pool.Count) {
								return false;
							} else { 
								this.showedExercises.Add (currentList.ID);
								TaskListExercise.Shuffle<SemanticSeriesElement>(currentList.Elements);
								repetitions++;
								
								return true;
							}
						}
						else {
							return false;
						}
					}
					else
					{
						return false;
					}
				}
			}
						
		
			
			return true;
		}	
		#endregion
	
		
		#region Evaluation
		
		public SingleResultReasoningExercice getReasoningResult (List<SemanticSeriesElement> selected){
			
			SingleResultReasoningExercice sr = new SingleResultReasoningExercice();
			if ( selected.Count== 0 || selected.Count != currentList.WrongElements.Count)
			{
				sr.Result = "Fail";
			}
			else if (selected.Contains(currentList.WrongElements[0]))
			{
				sr.Result = "Valid";
			}
			else 
			{
				sr.Result = "Fail";
			}
			
			sr.HelpUsed=withHelp;
			sr.Level = this.CurrentLevel.ToString();
			sr.QuestionId = currentList.ID;
			sr.AnswerIdSelected = selected[0].PositionId;
			
			return sr;
		}
		public override ReasoningExerciseResult CheckAnswer(List<SemanticSeriesElement> selected)
		{
			
			/*ReasoningExerciseResult r = new ReasoningExerciseResult();
			if ( selected.Count== 0 || selected.Count != currentList.WrongElements.Count)
			{
				r.Errores = 1; r.Aciertos = 0;
				r.Omisiones = currentList.WrongElements.Count - r.Aciertos;
			}
			else if (selected.Contains(currentList.WrongElements[0]))
			{
				r.Errores = 0; r.Aciertos = 1;	
				r.Omisiones = currentList.WrongElements.Count - r.Aciertos;
			}
			else 
			{
				r.Errores = 1; r.Aciertos = 0;
				r.Omisiones = currentList.WrongElements.Count - r.Aciertos;
			}
			
			r.Tiempo = end - start;
			r.Id = this.currentList.ID;
			r.Ayuda=withHelp;
			//Console.WriteLine(r);
			
			//panel.ShowCorrectAnswers<SemanticSeriesElement>(currentList.WrongElements, selected);
			//panel.ExplanationText = currentList.Explanation;
			//panel.ShowExplanation();
			
			return r;*/
			return null;
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
		#endregion
		
		#region XML Serialization
		
		public override void Serialize(int scoreArg)
		{
			
			if( finalRes.ReasoningExerciceExecutionResults !=null){
				SetFinalResult(scoreArg);
				XmlUtil.SerializeForUser<ReasoningExerciseSesionResults>(finalRes,Configuration.Current.GetExerciseConfigurationFolderPath () + "/SemanticSeries.xml");
			}
			/*string fullPath = Configuration.Current.GetExerciseConfigurationFolderPath() + "/" + xmlUserFile;
			
			XmlTextWriter escritor = new XmlTextWriter(fullPath, null);
		
			try
			{
				escritor.Formatting = Formatting.Indented;
				
				escritor.WriteStartDocument();
				
				escritor.WriteDocType("semantic-series-exercise", null, null, null);
				
				// hoja de estilo para pode ver en un navegador el xml
				//escritor.WriteProcessingInstruction("xml-stylesheet", "type='text/xsl' href='cuestionario.xsl'");
				
				XmlSerializer serializer = new XmlSerializer(typeof(SemanticSeriesExercise));
				
				XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
				
				namespaces.Add("","");
				
				serializer.Serialize(escritor, this, namespaces);
				
				escritor.WriteEndDocument();
				escritor.Close();
			}
			catch(Exception e)
			{
				escritor.Close();
				Console.WriteLine("Error al serializar" +  e.Message);
			}*/
		}
		
		
		public static SemanticSeriesExercise Deserialize()
		{
			string fullPath = Configuration.Current.GetExerciseConfigurationFolderPath() + Path.DirectorySeparatorChar + xmlUserFile;
			// Console.WriteLine( "Fullpath: " +fullPath );
			if (!File.Exists(fullPath))
			{
				string s = Environment.CommandLine;			
				fullPath = Configuration.CommandDirectory + Path.DirectorySeparatorChar + "ejercicios" + Path.DirectorySeparatorChar + "Reasoning" + Path.DirectorySeparatorChar + "SemanticSeries" + Path.DirectorySeparatorChar + "xml-templates" + Path.DirectorySeparatorChar + xmlUserFile;
			}
						
			XmlTextReader lector = new XmlTextReader(fullPath);
			
			try
			{
				exercise = new SemanticSeriesExercise();
				
				XmlSerializer serializer = new XmlSerializer(typeof(SemanticSeriesExercise));				
				exercise = (SemanticSeriesExercise) serializer.Deserialize(lector);
				
				lector.Close();		
				
			}
			catch(Exception e)
			{
				Console.WriteLine(e.ToString());
				lector.Close();
				return null;
			}
			
			SemanticSeriesExercise lseAux = Deserialize(Configuration.CommandDirectory + Path.DirectorySeparatorChar + "ejercicios" + Path.DirectorySeparatorChar + "Reasoning" + Path.DirectorySeparatorChar + "SemanticSeries" + Path.DirectorySeparatorChar + "xml-templates" + Path.DirectorySeparatorChar + xmlUserFile);
			((SemanticSeriesExercise) exercise).PoolOfElements = lseAux.PoolOfElements;
			
			return (SemanticSeriesExercise) exercise;
		}
		
		public static  SemanticSeriesExercise Deserialize(string path)
		{
			XmlTextReader lector = new XmlTextReader(path);
				try
				{
					SemanticSeriesExercise ex = new SemanticSeriesExercise();
					
					XmlSerializer serializer = new XmlSerializer(typeof(SemanticSeriesExercise));				
					ex = (SemanticSeriesExercise) serializer.Deserialize(lector);
					
					lector.Close();				
					return (SemanticSeriesExercise) ex;
				}
				catch( Exception e)
				{
					lector.Close();
					return null;
				}
		}
		protected override void PopulatePanel ()
		{
			panel.Populate(currentList.Elements);
			panel.ExplanationText= currentList.Explanation;
		}		
		#endregion
	}
}

