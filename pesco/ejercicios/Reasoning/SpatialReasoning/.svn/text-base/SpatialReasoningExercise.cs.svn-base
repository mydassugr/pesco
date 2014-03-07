using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace pesco
{
	[XmlRoot("spatial-reasoning-exercise")]
	public class SpatialReasoningExercise : ReasoningExercise<SpatialReasoningElement, SpatialReasoningPanel>
	{
		int inicio = 0;
		public static string xmlUserFile = "spatial-reasoning.xml";
		
		[XmlIgnoreAttribute]
		protected ReasoningExerciseSesionResults finalRes = XmlUtil.DeserializeForUser<ReasoningExerciseSesionResults>(Configuration.Current.GetExerciseConfigurationFolderPath () + "/SpatialReasoning.xml");

		protected SpatialReasoningSeries currentSeries;
		
		protected int[] distractorsPosition = new int[2];

		/// <summary>
		/// 
		/// </summary>
		protected List<SpatialReasoningSeries> seriesPool;

		/// <summary>
		/// 
		/// </summary>
		[XmlElement("pool")]
		public List<SpatialReasoningSeries> SeriesPool {
			get { return this.seriesPool; }
			set { seriesPool=value;}
		}

		public SpatialReasoningExercise ()
		{
			// inicializamos los resultados 
			if(finalRes.SpatialReasoningExerciceExecutionResults == null){
				finalRes = new ReasoningExerciseSesionResults(true);
				
			}
			currentSesionResults = new ReasoningExerciseSesionResults(true);
			finalRes.CategoryId=Convert.ToInt16(ExerciseCategory.Reasoning);
			finalRes.CurrentLevel=Convert.ToInt16( this.CurrentLevel);
			finalRes.ExerciseId= this.idEjercicio();
			
			ExerciceExecutionResult <SingleResultSpatialReasoningExercice> exerciceExecution =  new ExerciceExecutionResult<SingleResultSpatialReasoningExercice>(SessionManager.GetInstance().CurSession.IdSession, SessionManager.GetInstance().CurExecInd);
			finalRes.SpatialReasoningExerciceExecutionResults.Add(exerciceExecution);
			currentSesionResults.SpatialReasoningExerciceExecutionResults.Add(exerciceExecution);
			
			seriesPool = new List<SpatialReasoningSeries> ();
			currentSeries = new SpatialReasoningSeries ();
			this.instructions = "<big>Voy a mostrarte una imagen en la parte central de " + 
				"la pantalla. En la parte baja aparecerán una serie de piezas que " + 
					"son parte de la imagen de arriba. \n\nTienes que pulsar sobre los dos " + 
					"piezas que creas que <b>no</b> forman parte de la imagen del centro.\n\nCuando tenga señaladas las dos imágenes, " + 
					"pulsa el botón <b>¡Listo!</b><big>.";
		}

		#region Exercise Interface

		public override int idEjercicio ()
		{
		return 13;
		}

		public override void iniciar(){
		 // Changing mode to "demo" mode
            SessionManager.GetInstance().ChangeExerciseStatus("demo");
            
            // Creating panels
            ExerciseDemoSpatialReasoning WinDemosr= new ExerciseDemoSpatialReasoning (this);
            SessionManager.GetInstance().ReplacePanel( WinDemosr);
            WinDemosr.InitPanel();
		
		}
		
		public override bool inicializar ()
		{
			this.panel = new SpatialReasoningPanel ();
			//this.window = new Gtk.Window ("Razonamiento >> Piezas de Puzzle");
			
			this.instructions = "<big>Voy a mostrarte una imagen en la parte central de " + 
				"la pantalla. En la parte baja aparecerán una serie de piezas que " + 
					"son parte de la imagen de arriba. \n\nTienes que pulsar sobre los dos " + 
					"piezas que creas que <b>no</b> forman parte de la imagen del centro.\n\nCuando tenga señaladas las dos imágenes, " + 
					"pulsa el botón <b>¡Listo!</b></big>.";
			this.instructionsImageResourceName = "pesco.ejercicios.Reasoning.LogicalSeries.figures.spatial-reasoning.png";
			
			//this.window.Maximize ();
			
			// the event checks the answer of the user, store it a generate the next 
			panel.ReadyButton.Clicked += delegate(object sender, EventArgs e) {
					
				bool activo=false;
				
					if (!errorMostrado || screeningMode )
					{
						if(panel.numSelectd ==2){
							activo=true;
							end = DateTime.Now;
							TimeSpan timespan = end - start;
							
							//1. Check the answer and append it to the results list
							//currentSesionResults.Add (this.CheckAnswer (panel.GetSelectedItems ()));
						
							SingleResultSpatialReasoningExercice singleReasoningExercice = this.getReasoningResult( panel.GetSelectedItems());
							singleReasoningExercice.TimeElapsed = Convert.ToInt16( timespan.TotalSeconds);
						
							finalRes.SpatialReasoningExerciceExecutionResults[finalRes.SpatialReasoningExerciceExecutionResults.Count -1].SingleResults.Add(singleReasoningExercice);
							//currentSesionResults.SpatialReasoningExerciceExecutionResults[currentSesionResults.SpatialReasoningExerciceExecutionResults.Count -1].SingleResults.Add(singleReasoningExercice);
							SessionManager.GetInstance ().RepetitionFinished ();
							
							foreach ( SpatialReasoningToggleButton w in panel.buttons){
								if(w.IsDistractor)
									w.SetAsCorrect();
								w.IsSolution=true;
							}
							foreach(SpatialReasoningToggleButton s in panel.selectd){
								if(!s.IsDistractor)
									s.SetAsWrong();
							}
							
							panel.ShowCorrectExplanation(true);
							
							GtkUtil.PimpButtonFromStock(panel.ReadyButton, "Siguiente", "gtk-go-forward");
							GtkUtil.SetStyle(panel.ReadyButton, Configuration.Current.ButtonFont);
						}
						else{
							activo=false;
							Gtk.MessageDialog md = new Gtk.MessageDialog (null, Gtk.DialogFlags.DestroyWithParent, 
							                                              Gtk.MessageType.Error, Gtk.ButtonsType.Ok, 
							                                              "<big><big><big>Debes seleccionar <b>dos fragmentos</b> para poder continuar.</big></big></big>");					
							GtkUtil.SetStyleRecursive( md, Configuration.Current.MediumFont );	
							Gtk.ResponseType result = (Gtk.ResponseType)md.Run ();
							md.Destroy ();
						}
					}
					if (errorMostrado || screeningMode) {
						
						activo=true;
						//2. Generates the next series & adds the new series to the panel
						if (!Next (this.currentLevel))
							this.finishExercice ();
						else {
							PopulatePanel ();
							panel.ShowAll ();
							panel.HideExplanation ();
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
			return "Piezas de Puzzle";
		}

		public override void pausa ()
		{
			throw new NotImplementedException ();
		}

		
		#endregion

		public SingleResultSpatialReasoningExercice getReasoningResult (List<int> selected){
			
			SingleResultSpatialReasoningExercice sr = new SingleResultSpatialReasoningExercice();
			if ( selected[0] == 0)
			{
				sr.Result = "Fail";
			}
			else{
				if(selected[0] ==1)
					sr.Result = "Semivalid";
				else{
					sr.Result = "Valid";
					
				}
			}
			
			
			//sr.HelpUsed=withHelp;
			sr.Corrects = selected [0];
			sr.Fails = 2- selected [0];
			sr.Level = this.CurrentLevel.ToString();
			sr.QuestionId = currentSeries.ID;
			
			return sr;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="selected">
		/// A <see cref="List<SpatialReasoningElement>"/>
		/// </param>
		/// <returns>
		/// A <see cref="ReasoningExerciseResult"/>
		/// </returns>
		public override ReasoningExerciseResult CheckAnswer (List<SpatialReasoningElement> selected)
		{
			ReasoningExerciseResult r = new ReasoningExerciseResult ();
			return r;
		}

		public ReasoningExerciseResult CheckAnswer (List<int> selected)
		{
			ReasoningExerciseResult r = new ReasoningExerciseResult ();
			
			r.Aciertos = selected[0];
			r.Omisiones = 2 - r.Aciertos;
			r.Errores = selected[1] - r.Aciertos;
			r.Tiempo = end - start;
			r.Id = this.currentSeries.ID;
			
			return r;
		}
		
		protected void SetFinalResult(int scoreArg){
			
			finalRes.SpatialReasoningExerciceExecutionResults[finalRes.SpatialReasoningExerciceExecutionResults.Count -1].TotalCorrects =0;
			finalRes.SpatialReasoningExerciceExecutionResults[finalRes.SpatialReasoningExerciceExecutionResults.Count -1].TotalFails =0;
			finalRes.SpatialReasoningExerciceExecutionResults[finalRes.SpatialReasoningExerciceExecutionResults.Count -1].TotalTimeElapsed =0;
			finalRes.SpatialReasoningExerciceExecutionResults[finalRes.SpatialReasoningExerciceExecutionResults.Count -1].Score =scoreArg;
			foreach(SingleResultSpatialReasoningExercice sr in finalRes.SpatialReasoningExerciceExecutionResults[finalRes.SpatialReasoningExerciceExecutionResults.Count -1].SingleResults){
				if( sr.Result.Equals("Valid"))
					finalRes.SpatialReasoningExerciceExecutionResults[finalRes.SpatialReasoningExerciceExecutionResults.Count -1].TotalCorrects ++;
				
				if( sr.Result.Equals("Fail"))
					finalRes.SpatialReasoningExerciceExecutionResults[finalRes.SpatialReasoningExerciceExecutionResults.Count -1].TotalFails ++;
			
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
		public override void Serialize (int scoreArg)
		{
			if( finalRes.SpatialReasoningExerciceExecutionResults !=null){
				SetFinalResult(scoreArg);
				XmlUtil.SerializeForUser<ReasoningExerciseSesionResults>(finalRes,Configuration.Current.GetExerciseConfigurationFolderPath () +"/SpatialReasoning.xml");
			}
			
			/*string fullPath = Configuration.Current.GetExerciseConfigurationFolderPath () + "/" + xmlUserFile;
			
			XmlTextWriter escritor = new XmlTextWriter (fullPath, null);
			
			Console.WriteLine ("Serializando " + fullPath + "!!!");
			
			try {
				escritor.Formatting = Formatting.Indented;
				
				escritor.WriteStartDocument ();
				
				escritor.WriteDocType ("spatial-reasoning-exercise", null, null, null);
				
				// hoja de estilo para pode ver en un navegador el xml
				//escritor.WriteProcessingInstruction("xml-stylesheet", "type='text/xsl' href='cuestionario.xsl'");
				
				XmlSerializer serializer = new XmlSerializer (typeof(SpatialReasoningExercise));
				
				XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces ();
				
				namespaces.Add ("", "");
				
				serializer.Serialize (escritor, this, namespaces);
				
				escritor.WriteEndDocument ();
				escritor.Close ();
			} catch (Exception e) {
				escritor.Close ();
				Console.WriteLine ("Error al serializar" + e.Message);
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
		public static SpatialReasoningExercise Deserialize ()
		{
			
			string fullPath = Configuration.Current.GetExerciseConfigurationFolderPath () + "/" + xmlUserFile;
			
			if (!File.Exists (fullPath)) {
				string s = Environment.CommandLine;
				fullPath = Configuration.CommandDirectory + "/ejercicios/Reasoning/SpatialReasoning/xml-templates/" + xmlUserFile;
			}
			
			XmlTextReader lector = new XmlTextReader (fullPath);
			
			try {
				exercise = new SpatialReasoningExercise ();
				
				XmlSerializer serializer = new XmlSerializer (typeof(SpatialReasoningExercise));
				exercise = (SpatialReasoningExercise)serializer.Deserialize (lector);
				
				lector.Close ();
				
			} catch (Exception e) {
				lector.Close ();
				return new SpatialReasoningExercise ();
			}
			SpatialReasoningExercise lseAux = Deserialize(Configuration.CommandDirectory + "/ejercicios/Reasoning/SpatialReasoning/xml-templates/" + xmlUserFile);
			((SpatialReasoningExercise) exercise).seriesPool = lseAux.seriesPool;
			return (SpatialReasoningExercise)exercise;
			
		}
		
		public static  SpatialReasoningExercise Deserialize(string path)
		{
			XmlTextReader lector = new XmlTextReader(path);
				try
				{
					SpatialReasoningExercise ex = new SpatialReasoningExercise();
					
					XmlSerializer serializer = new XmlSerializer(typeof(SpatialReasoningExercise));				
					ex = (SpatialReasoningExercise) serializer.Deserialize(lector);
					
					lector.Close();				
					return (SpatialReasoningExercise) ex;
				}
				catch( Exception e)
				{
					lector.Close();
					return null;
				}
		}
		/*SpatialReasoningExercise.exercise = new SpatialReasoningExercise();
			SpatialReasoningExercise.exercise.Serialize();
			return exercise as SpatialReasoningExercise;*/
		#endregion

		protected override void PopulatePanel ()
		{
			panel.Populate (currentSeries, distractorsPosition);
		}


		/// <summary>
		/// Selects a random series with a certain difficulty
		/// </summary>
		/// <param name="d">
		/// A <see cref="ReasoningExerciseDifficulty"/> 
		/// </param>
		protected override bool Next (ReasoningExerciseDifficulty d)
		{
			
			base.Next (d);
			//return true;
			
			if (SessionManager.GetInstance ().HaveToFinishCurrentExercise ()) {
				// Console.WriteLine ("OVER Max number of executions!!!!");
				return false;
			}
			
			// if there are series to choose
			if (seriesPool.Count > 0) {
				// select all the series with difficulty = d
				List<SpatialReasoningSeries> pool = new List<SpatialReasoningSeries> ();
				
				foreach (SpatialReasoningSeries s in seriesPool)
					if (s.Difficulty == d)
						pool.Add (s);
				
				TaskListExercise.Shuffle<SpatialReasoningSeries> (pool);
				
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
						
						repetitions++;
						
						// Console.WriteLine (this.showedExercises);
						// Console.WriteLine (currentSeries.ID);
						
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
			/*
			// DEBUG CODE - It lists all the series and finish when all the series are listed
			if (inicio >= this.seriesPool.Count) return false;
			
			currentSeries = seriesPool[inicio];
			inicio++;
			//TaskListExercise.Shuffle<T>(currentSeries.Elements);
			return true;
			*/
			
		}
		
		public override void ayuda() {
			
			throw new NotImplementedException ();
			
		}
	}
}

