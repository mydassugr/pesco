using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using Gdk;
using Gtk;

namespace pesco
{
	[XmlRoot("semantic-analogies-exercise")]
	public class SemanticAnalogiesExercise : CompleteSeriesExercise<SemanticSeriesElement, SemanticAnalogiesPanel>
	{
		public static string xmlUserFile = "semantic-analogies.xml";
		[XmlIgnoreAttribute]
		protected ReasoningExerciseSesionResults finalRes = XmlUtil.DeserializeForUser<ReasoningExerciseSesionResults>(Configuration.Current.GetExerciseConfigurationFolderPath () + "/SemanticAnalogies.xml");
		
		#region Abstract methods of Exercise
		public override void ayuda ()
		{
			panel.ExplanationText = this.CurrentSeries.Explanation;
			panel.ShowExplanation();
		}
		public void OnHelpClicked (object sender, System.EventArgs e){
			this.ayuda();
		}

		public override int idEjercicio ()
		{
			return 15;
		}

		/*public override void iniciar(){
			throw new NotImplementedException ();
		}
  */
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
            ExerciseDemoSemanticAnalogies demosa= new ExerciseDemoSemanticAnalogies (this);
            SessionManager.GetInstance().ReplacePanel(demosa);
            demosa.InitPanel();
        }
		public override bool inicializar ()
		{
			this.panel = new SemanticAnalogiesPanel();			
			/*this.window = new Gtk.Window("Razonamiento >> " + this.NombreEjercicio());*/
			bool activo=true;
			this.instructions = "<big>Voy a mostrarte una serie de adivinanzas (como la que puedes ver en la imagen de abajo) " +
				"donde tienes que asociar dos palabras.\n\nEn la parte " +
				"inferior de la pantalla aparecerán las posibles soluciones. Pulsa la que " +
				 "creas correcta (se marcará en amarillo) y después sobre el botón " +
				 "<span color='black'>¡Listo!</span> para ver si has acertado. Una vez mostrada la solución, pulsa " +
				 "<span color='black'>Siguiente</span> para continuar con la próxima adivinaza.</big>";
			panel.ExplanationText = this.currentSeries.Explanation;			
			panel.SolutionExplanationText = this.currentSeries.SolutionExplanation;
			panel.HelpButton.Clicked += this.OnClickHelp;
			
			this.instructionsImageResourceName = "pesco.ejercicios.Reasoning.SemanticAnalogies.pantallazo2.png";
			// the event checks the answer of the user, store it a generate the next 
			panel.ReadyButton.Clicked += delegate(object sender, EventArgs e) {
				
				if (!errorMostrado  || screeningMode)
				{				
					activo=panel.SolutionSelected();
					if(activo){
						end = DateTime.Now;
						TimeSpan timespan = end-start;
						
						//1. Check the answer and append it to the results list
						//currentSesionResults.Add(this.CheckAnswer(panel.GetSelectedItems()));
						SingleResultReasoningExercice singleReasoningExercice=this.getReasoningResult( panel.GetSelectedItems());
						singleReasoningExercice.TimeElapsed = Convert.ToInt16( timespan.TotalSeconds);
					
						
						finalRes.ReasoningExerciceExecutionResults[finalRes.ReasoningExerciceExecutionResults.Count -1].SingleResults.Add(singleReasoningExercice);
						//currentSesionResults.ReasoningExerciceExecutionResults[currentSesionResults.ReasoningExerciceExecutionResults.Count -1].SingleResults.Add(singleReasoningExercice);
						SessionManager.GetInstance().RepetitionFinished();
						
						panel.ShowCorrectAnswers<SemanticSeriesElement>(currentSeries.CorrectElements, panel.GetSelectedItems());
						panel.ExplanationText = this.CurrentSeries.Explanation; 
						panel.SolutionExplanationText = this.currentSeries.SolutionExplanation;
						
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
                        
						
						GtkUtil.PimpButtonFromStock(panel.ReadyButton, "Siguiente", "gtk-go-forward" );
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
			return "Adivinanzas";
		}

		public override void pausa ()
		{
			throw new NotImplementedException ();
		}

		#endregion
		
			public SingleResultReasoningExercice getReasoningResult (List<SemanticSeriesElement> selected){
			
			SingleResultReasoningExercice sr = new SingleResultReasoningExercice();
			if ( selected.Count== 0 || selected.Count != currentSeries.CorrectElements.Count)
			{
				sr.Result = "Fail";
			}
			else if (selected.Contains(currentSeries.CorrectElements[0]))
			{
				sr.Result = "Valid";
			}
			else 
			{
				sr.Result = "Fail";
			}
			
			sr.HelpUsed=withHelp;
			sr.Level = this.CurrentLevel.ToString();
			sr.QuestionId = currentSeries.ID;
			sr.AnswerIdSelected = selected[0].PositionId;
			
			return sr;
		}
		
		public override ReasoningExerciseResult CheckAnswer(List<SemanticSeriesElement> selected)
		{
			/*ReasoningExerciseResult r = new ReasoningExerciseResult();
			if ( selected.Count== 0 || selected.Count != currentSeries.CorrectElements.Count)
			{
				r.Errores = 1; r.Aciertos = 0;
				r.Omisiones = currentSeries.CorrectElements.Count - r.Aciertos;
			}
			else if (selected.Contains(currentSeries.CorrectElements[0]))
			{
				r.Errores = 0; r.Aciertos = 1;	
				r.Omisiones = currentSeries.CorrectElements.Count - r.Aciertos;
			}
			else 
			{
				r.Errores = 1; r.Aciertos = 0;
				r.Omisiones = currentSeries.CorrectElements.Count - r.Aciertos;
			}
			
			r.Tiempo = end - start;
			r.Id = this.currentSeries.ID;
			r.Ayuda= this.withHelp;
			
			//panel.ShowCorrectAnswers<SemanticSeriesElement>(this.currentSeries.CorrectElements, selected);
			//panel.ExplanationText = currentSeries.Explanation;
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
		
		public override void Serialize(int scoreArg)
		{
			
			if( finalRes.ReasoningExerciceExecutionResults !=null){
				SetFinalResult(scoreArg);
				XmlUtil.SerializeForUser<ReasoningExerciseSesionResults>(finalRes,Configuration.Current.GetExerciseConfigurationFolderPath () + "/SemanticAnalogies.xml");
			}
			/*string fullPath = Configuration.Current.GetExerciseConfigurationFolderPath() + "/" + xmlUserFile;
			
			XmlTextWriter escritor = new XmlTextWriter(fullPath, null);
		
			try
			{
				escritor.Formatting = Formatting.Indented;
				
				escritor.WriteStartDocument();
				
				escritor.WriteDocType("semantic-analogies-exercise", null, null, null);
				
				// hoja de estilo para pode ver en un navegador el xml
				//escritor.WriteProcessingInstruction("xml-stylesheet", "type='text/xsl' href='cuestionario.xsl'");
				
				XmlSerializer serializer = new XmlSerializer(typeof(SemanticAnalogiesExercise));
				
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
		
		public static  SemanticAnalogiesExercise Deserialize()
		{
			string fullPath = Configuration.Current.GetExerciseConfigurationFolderPath() + "/" + xmlUserFile;
			
			if (!File.Exists(fullPath))
			{
				string s = Environment.CommandLine;			
				fullPath = Configuration.CommandDirectory + "/ejercicios/Reasoning/SemanticAnalogies/xml-templates/" + xmlUserFile;
			}
			
			XmlTextReader lector = new XmlTextReader(fullPath);
			
			try
			{
				exercise = new SemanticAnalogiesExercise();
				
				XmlSerializer serializer = new XmlSerializer(typeof(SemanticAnalogiesExercise));				
				exercise = (SemanticAnalogiesExercise) serializer.Deserialize(lector);
				
				lector.Close();		
				
			}
			catch( Exception e)
			{
				lector.Close();
				return null;
			}
			
			SemanticAnalogiesExercise lseAux = Deserialize(Configuration.CommandDirectory + "/ejercicios/Reasoning/SemanticAnalogies/xml-templates/" + xmlUserFile);
			((SemanticAnalogiesExercise) exercise).SeriesPool = lseAux.SeriesPool;
			return (SemanticAnalogiesExercise) exercise;
		}		
		
		public static  SemanticAnalogiesExercise Deserialize(string path)
		{
		
			XmlTextReader lector = new XmlTextReader(path);
				try
				{
					SemanticAnalogiesExercise ex = new SemanticAnalogiesExercise();
					
					XmlSerializer serializer = new XmlSerializer(typeof(SemanticAnalogiesExercise));				
					ex = (SemanticAnalogiesExercise) serializer.Deserialize(lector);
					
					lector.Close();				
					return (SemanticAnalogiesExercise) ex;
				}
				catch( Exception e)
				{
					lector.Close();
					return null;
				}
		}
		protected override void PopulatePanel ()
		{
			panel.Populate(currentSeries.Samples, currentSeries.Options);
			//panel.ExplanationText= CurrentSeries.Explanation;
			
		}
	}
}

