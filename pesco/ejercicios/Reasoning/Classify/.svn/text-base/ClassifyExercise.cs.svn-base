using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;

namespace pesco
{
	[XmlRoot("classify-exercise")]
	public class ClassifyExercise : SelectElementsExercise<ClassifyExerciseElement, ClassifyPanel>
	{
		public static string xmlUserFile = "classify.xml";
		
		int cuenta = 0;
		
		[XmlIgnoreAttribute]
		protected ReasoningExerciseSesionResults finalRes = XmlUtil.DeserializeForUser<ReasoningExerciseSesionResults>(Configuration.Current.GetExerciseConfigurationFolderPath () + "/Classify.xml");
		

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
			ExerciceDemoClassi demols= new ExerciceDemoClassi(this);
			SessionManager.GetInstance().ReplacePanel( demols );
            demols.InitPanel();
		}
		public override bool inicializar ()
		{
			
			this.instructions = "<big><big>Voy a mostrarte una serie de figuras. Tienes " +
				"que pulsar sobre la figura que no encaje con las demás.\n\nAbajo te mostramos un ejemplo. La solución correcta está marcada en verde y la incorrecta en rojo. La justificación de la solución correcta también se muestra en la imagen de abajo.</big></big>";
			this.instructionsImageResourceName = "pesco.ejercicios.Reasoning.LogicalSeries.figures.classify.png";
			this.panel = new ClassifyPanel();
			bool activo=true;
            panel.ExplanationText = currentList.Explanation;            
            panel.HelpButton.Clicked += this.OnClickHelp;
            
			// the event checks the answer of the user, store it a generate the next 
			panel.ReadyButton.Clicked += delegate(object sender, EventArgs e) {
				
			if (!errorMostrado || screeningMode ){			
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
					
					
					panel.ShowClassifyCorrectAnswers<ClassifyExerciseElement>(currentList.WrongElements, panel.GetSelectedItems());
					panel.ExplanationText = currentList.Explanation; 
                    //If the answer is right
                    /*ReasoningExerciseResult r = this.currentSesionResults.Results[this.currentSesionResults.Results.Count-1];
                        
                    if(r.Aciertos == 0)
                        panel.ShowCorrectExplanation(false);
                    else 
                       panel.ShowCorrectExplanation(true);*/
					
					//If the answer is right
					if( singleReasoningExercice.Result.Equals("Fail"))
						   panel.ShowCorrectExplanation(false);
                    else 
                       panel.ShowCorrectExplanation(true);
                        
                    GtkUtil.PimpButtonFromStock(panel.ReadyButton, "Siguiente", "gtk-go-forward");
					GtkUtil.SetStyle(panel.ReadyButton, Configuration.Current.ButtonFont);
                    }
                else{
                    Gtk.MessageDialog md = new Gtk.MessageDialog (null, Gtk.DialogFlags.DestroyWithParent, 
                                               Gtk.MessageType.Error, Gtk.ButtonsType.Ok, 
                                               "<big><big><big>Debes elegir alguna solución, para poder continuar.</big></big></big>");                  
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
		//return base.inicializar();	
            return true;   
		}

		public  override string NombreEjercicio ()
		{
			return "Encuentra la Figura Intrusa";
		}

		public override void pausa ()
		{
			throw new NotImplementedException ();
		}
	
		
		#endregion
		
		public SingleResultReasoningExercice getReasoningResult (List<ClassifyExerciseElement> selected){
			
			SingleResultReasoningExercice sr = new SingleResultReasoningExercice();
			if ( selected.Count== 0 || selected.Count != currentList.WrongElements.Count)
			{
				sr.Result = "Fail";
			}
			else if (selected[0].PositionId== currentList.WrongElements[0].PositionId)
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
		public override ReasoningExerciseResult CheckAnswer(List<ClassifyExerciseElement> selected)
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
			r.Ayuda = withHelp;			
			r.Id = this.currentList.ID;
            
		    //panel.ShowCorrectAnswers<ClassifyExerciseElement>(currentList.WrongElements, selected);
            //panel.ExplanationText = currentList.Explanation;
            //panel.ShowExplanation();
			
			//Console.WriteLine(r);
			
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
				XmlUtil.SerializeForUser<ReasoningExerciseSesionResults>(finalRes,Configuration.Current.GetExerciseConfigurationFolderPath () + "/Classify.xml");
			}
			/*string fullPath = Configuration.Current.GetExerciseConfigurationFolderPath() + Path.DirectorySeparatorChar + xmlUserFile;
			
			XmlTextWriter escritor = new XmlTextWriter(fullPath, null);
		
			try
			{
				escritor.Formatting = Formatting.Indented;
				
				escritor.WriteStartDocument();
				
				escritor.WriteDocType("classify-exercise", null, null, null);
				
				XmlSerializer serializer = new XmlSerializer(typeof(ClassifyExercise));
				
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
		
		public static ClassifyExercise Deserialize()
		{
			string fullPath = Configuration.Current.GetExerciseConfigurationFolderPath() + Path.DirectorySeparatorChar + xmlUserFile;
			
			if (!File.Exists(fullPath))
			{
				string s = Environment.CommandLine;			
				fullPath = Configuration.CommandDirectory + Path.DirectorySeparatorChar + "ejercicios" + Path.DirectorySeparatorChar + "Reasoning" + Path.DirectorySeparatorChar+ "Classify" + Path.DirectorySeparatorChar + "xml-templates" + Path.DirectorySeparatorChar + xmlUserFile;
			}
			
			XmlTextReader lector = new XmlTextReader(fullPath);
			
			try
			{
				exercise = new ClassifyExercise();
				
				XmlSerializer serializer = new XmlSerializer(typeof(ClassifyExercise));				
				exercise = (ClassifyExercise) serializer.Deserialize(lector);
				
				lector.Close();		
				//return (ClassifyExercise) exercise;
			}
			catch( Exception e)
			{
				lector.Close();
				return null;
			}
			
			ClassifyExercise lseAux = Deserialize(Configuration.CommandDirectory + Path.DirectorySeparatorChar + "ejercicios" + Path.DirectorySeparatorChar + "Reasoning" + Path.DirectorySeparatorChar + "Classify" + Path.DirectorySeparatorChar + "xml-templates" + Path.DirectorySeparatorChar + xmlUserFile);
			((ClassifyExercise) exercise).PoolOfElements = lseAux.PoolOfElements;
				
			return (ClassifyExercise) exercise;
		}
		
		public static  ClassifyExercise Deserialize(string path)
		{
		
			XmlTextReader lector = new XmlTextReader(path);
				try
				{
					ClassifyExercise ex = new ClassifyExercise();
					
					XmlSerializer serializer = new XmlSerializer(typeof(ClassifyExercise));				
					ex = (ClassifyExercise) serializer.Deserialize(lector);
					
					lector.Close();				
					return (ClassifyExercise) ex;
				}
				catch( Exception e)
				{
					lector.Close();
					return null;
				}
		}
		protected int getSelectedPosition()
		{
			/*int position =1;
			
			
			List<ClassifyExerciseElement> selectedItem= panel.GetSelectedItems();
			foreach( ClassifyExerciseElement ce in currentOrderList.Elements){
				
				if(ce.Equals(selectedItem[0])){
					return position;
				}
				else
					position ++;
			}
			
			return position;*/
			return 1;
		}
		protected override void PopulatePanel ()
		{
			panel.Populate(currentList.Elements);
            panel.ExplanationText= currentList.Explanation;
       	}
		
		 
		
	}
}

