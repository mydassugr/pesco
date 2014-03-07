/**

Copyright 2011 Grupo de Investigación GEDES
Lenguajes y sistemas informáticos
Universidad de Granada

Licensed under the EUPL, Version 1.1 or – as soon they 
will be approved by the European Commission - subsequent  
versions of the EUPL (the "Licence"); 
You may not use this work except in compliance with the 
Licence. 
You may obtain a copy of the Licence at: 

http://ec.europa.eu/idabc/eupl  

Unless required by applicable law or agreed to in 
writing, software distributed under the Licence is 
distributed on an "AS IS" basis, 
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either 
express or implied. 
See the Licence for the specific language governing 
permissions and limitations under the Licence. 



*/
using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

using Gtk;

namespace pesco
{
	/// <summary>
	/// 
	/// </summary>
	public enum ReasoningExerciseDifficulty
	{
		/// <summary>
		/// 
		/// </summary>
		low,

		/// <summary>
		/// 
		/// </summary>
		medium,

		/// <summary>
		/// 
		/// </summary>
		high
	}

	/// <summary>
	/// ReasoningExercise<T, Q> represents a reasoning Exercise composed of elements of the type T and it is shown in a panel with the type Q
	/// </summary>
	public abstract class ReasoningExercise<T, Q> : Exercise where T : ReasoningExerciseElement where Q : ReasoningPanel
	{
		/// <summary>
		/// Random number generator
		/// </summary>
		protected Random r = new Random (DateTime.Now.Millisecond);

		protected string instructions = "Please Fill!";
		protected string instructionsImageResourceName = "pesco.pag-tareas.png";

		#region Singleton Patter
		/// <summary>
		/// The only allowed instance of Reasoning Exercise
		/// </summary>
		protected static ReasoningExercise<T, Q> exercise;
		#endregion

		protected uint maxRepetitions = 2;
		protected uint repetitions = 0;

		protected List<int> showedExercises = new List<int> ();

		protected bool screeningMode = false;


		#region GUI
		/// <summary>
		/// The window that will show the exercise
		/// </summary>
		//protected Gtk.Window window;
		//protected PanelV exercicePanel;
		
		/// <summary>
		/// The panel containing the exercise wills
		/// </summary>
		protected Q panel;

		protected abstract void PopulatePanel ();
		#endregion

		/// <summary>
		/// Current level of the exercise
		/// </summary>
		protected ReasoningExerciseDifficulty currentLevel;

		/// <summary>
		/// Current level of the exercise 
		/// </summary>
		[XmlElement("current-level")]
		public ReasoningExerciseDifficulty CurrentLevel {
			get { return this.currentLevel; }
			set { currentLevel = value; }
		}

		/// <summary>
		/// Results of the exercise, classified by session
		/// </summary>
		protected List<ReasoningExerciseSesionResults> resultsBySesion;

		/// <summary>
		/// Results of the exercise, classified by session
		/// </summary>
		[XmlElement("sesion-results")]
		public List<ReasoningExerciseSesionResults> ResultsBySesion {
			get { return this.resultsBySesion; }
		}

		/// <summary>
		/// Results of the current session
		/// </summary>explanation.Visible = false;
		protected ReasoningExerciseSesionResults currentSesionResults;
		
		

		/// <summary>
		/// Creates a new Reasoning Exercise
		/// </summary>
		protected ReasoningExercise (bool sm)
		{
			resultsBySesion = new List<ReasoningExerciseSesionResults> ();
			currentSesionResults = new ReasoningExerciseSesionResults ();
			category = ExerciseCategory.Reasoning;
			this.resultsBySesion.Add (currentSesionResults);
			this.screeningMode = sm;
		}

		protected ReasoningExercise ()
		{
			resultsBySesion = new List<ReasoningExerciseSesionResults> ();
			currentSesionResults = new ReasoningExerciseSesionResults ();
			category = ExerciseCategory.Reasoning;
			this.resultsBySesion.Add (currentSesionResults);
			this.screeningMode = false;
		}

		#region Time
		/// <summary>
		/// Time when the exercise starts
		/// </summary>
		protected DateTime start;

		/// <summary>
		/// Time where the exercise ends
		/// </summary>
		protected DateTime end;
		#endregion



		#region XML serialization
		/// <summary>
		/// Serializes the exercise into an XML file
		/// </summary>
		/// <param name="filename">
		/// A <see cref="System.String"/>
		/// </param>
		public abstract void Serialize (int scoreArg);

		#endregion

		#region implemented abstract members of pesco.Ejercicio
		public bool tieneEnsayo ()
		{
			return false;
		}


		public bool tieneDemo ()
		{
			return true;
		}


		public override void pausa ()
		{
			//
		}

		public override bool inicializar ()
		{
			//this.window = new Gtk.Window ("Razonamiento >> " + this.NombreEjercicio ());
			panel.HelpButton.Clicked += this.OnClickHelp;
			
			if (screeningMode)
				currentLevel = ReasoningExerciseDifficulty.low;
			return true;
		}
		
		public void iniciarEjercicio(){
			if (Next (currentLevel)) {
				
				PopulatePanel ();
								
				//window.Add (panel);
				start = DateTime.Now;
				currentSesionResults.Start (SessionManager.GetInstance().CurSession.IdSession,currentLevel.ToString());
				
				//Shows the exercice in the principal win
				SessionManager.GetInstance().ReplacePanel(panel);
				panel.ShowAll();
				panel.HideHelpButton();
			
			
			} else{ 
				// recalculte level
				this.EvaluateLevel ();
				
				// serialize
				this.Serialize (0);
				exercise = null;
				SessionManager.GetInstance().ExerciseFinished(medalValue);
				SessionManager.GetInstance().TakeControl();
			}
		}
		/*public override void iniciar ()
		{
			
			
			
			window.DeleteEvent += delegate(object o, DeleteEventArgs args) {
				MessageDialog md = new MessageDialog (window, DialogFlags.DestroyWithParent, MessageType.Question, ButtonsType.YesNo, "¿Desea realmente abandonar el ejercicio?");
				
				ResponseType result = (ResponseType)md.Run ();
				
				if (result == ResponseType.Yes) {
					this.Serialize ();
					Application.Quit ();
					args.RetVal = false;
					// <-- Destroy window
				} else {
					md.Destroy ();
					args.RetVal = true;
					// <-- Don't destroy window
				}
			};
			
			if (Next (currentLevel)) {
				ReasoningInstructionDialog rid = new ReasoningInstructionDialog ();
				
				rid.SetText ("<big><big>"+this.instructions + "</big></big>");
				rid.SetImage (this.instructionsImageResourceName);

				Console.WriteLine("Maximizo!");
				rid.Maximize ();				
				rid.SetTitle("¡Hola! Este ejercicio se denomina: " + this.NombreEjercicio() + ". Su objetivo es medir tu capacidad de razonamiento.");
				rid.Run ();
				rid.Destroy ();
								
				//window.Remove(caja);
				PopulatePanel ();
				//window.Add (panel);
				start = DateTime.Now;
				currentSesionResults.Start ();
				
				//Shows the exercice in the principal win
				SessionManager.GetInstance().ReplacePanel(panel);
				
				//window.Maximize ();
				//window.ShowAll ();
				
				
			} else
				this.finalizar ();
		}*/

		public override void finalizar ()
		{
			// recalculte level
			/*this.EvaluateLevel ();
			
			// serialize
			this.Serialize ();
			
			
			
			//exercicePanel=null;
			exercise = null;
		
			
			SessionManager.GetInstance().ExerciseFinished(medalValue);
			SessionManager.GetInstance().TakeControl();*/
			exercise = null;
		}
			
		public void finishExercice(){
			// recalculte level
			this.EvaluateLevel ();
			
			// serialize
			this.Serialize (this.medalValue);
			
			//exercicePanel=null;
			exercise = null;
		
			
			SessionManager.GetInstance().ExerciseFinished(medalValue);
			SessionManager.GetInstance().TakeControl();
		}
		public int getElementPosition (string elementValue){
			
			int position;
			
			
			string [] imageName= elementValue.Split(new string[] {"."}, StringSplitOptions.RemoveEmptyEntries);
			string [] figureName = imageName[6].Split(new string[] {"figure"}, StringSplitOptions.RemoveEmptyEntries);
			position = Convert.ToInt16( figureName[0]);
			
			return position;
		}
		
		#endregion
		
		#region Events
		public void OnClickHelp (object sender, System.EventArgs e)
		{
			withHelp = true;
			exercise.ayuda ();
		}

		protected bool errorMostrado = false;
		#endregion

		#region Exercise Selection
		/// <summary>
		/// Select a random exercise
		/// </summary>
		protected virtual bool Next ()
		{
			withHelp = false;
			return true;
		}


		/// <summary>
		/// Selects a random exercise with a certain difficulty
		/// </summary>
		/// <param name="d">
		/// A <see cref="ReasoningExerciseDifficulty"/>
		/// </param>
		protected virtual bool Next (ReasoningExerciseDifficulty d)
		{
			withHelp = false;
			return true;
		}
		#endregion

		#region Results Evaluation

		protected bool withHelp = false;

		/// <summary>
		/// Evaluate the results for the  current sessions
		/// </summary>
		/// <returns>
		/// True if more of the 80% of the user replies were correct and false otherwise
		/// </returns>
		public bool EvaluateResults ()
		{
						
			double aciertosCounter = 0;
			double error = 0;
			double minAciertos = 0;
		
			try{
				int currentSessionNumber =currentSesionResults.ReasoningExerciceExecutionResults.Count -1;
				
				if(currentSesionResults.ReasoningExerciceExecutionResults[currentSessionNumber].SingleResults.Count ==0)
					return false;
				
				
				foreach ( SingleResultReasoningExercice r in currentSesionResults.ReasoningExerciceExecutionResults[currentSessionNumber].SingleResults){
					if( r.Result.Equals("Valid")){
						if(!r.HelpUsed)
							aciertosCounter ++;
						else
							aciertosCounter += 0.5;
					}   
				}
				
				error = currentSesionResults.ReasoningExerciceExecutionResults[currentSessionNumber].SingleResults.Count * 0.8;
				minAciertos = Math.Floor (error);
				
					
				
				// Console.WriteLine ("Error count " + aciertosCounter + "Max error allowed " + minAciertos);
				
				bool res;
				int minSilver=Convert.ToInt16( (currentSesionResults.ReasoningExerciceExecutionResults[currentSesionResults.ReasoningExerciceExecutionResults.Count -1].SingleResults.Count -currentSesionResults.ReasoningExerciceExecutionResults[currentSesionResults.ReasoningExerciceExecutionResults.Count -1].SingleResults.Count*0.4));
				// if the user is correct in more than the 80%, increase the level
				if (aciertosCounter >= minAciertos)
					res = true;
				else
					res = false;
				
				if (aciertosCounter == currentSesionResults.ReasoningExerciceExecutionResults[currentSesionResults.ReasoningExerciceExecutionResults.Count -1].SingleResults.Count)
					medalValue = 100;
				else if (aciertosCounter < currentSesionResults.ReasoningExerciceExecutionResults[currentSessionNumber].SingleResults.Count && aciertosCounter >minSilver )
					medalValue = 60;
				else
					medalValue = 0;
	
				return res;
			}catch{
				EvaluateResults (true);
			}
			
			return false;
		}

		public bool EvaluateResults (bool spatialReasoningExercice)
		{
				
			double aciertosCounter = 0;
			double error = 0;
			double minAciertos = 0;
			
			if(currentSesionResults.SpatialReasoningExerciceExecutionResults.Count ==0)
				return false;
			
			int currentSessionNumber = currentSesionResults.SpatialReasoningExerciceExecutionResults.Count -1;
			foreach ( SingleResultSpatialReasoningExercice r in currentSesionResults.SpatialReasoningExerciceExecutionResults[currentSessionNumber].SingleResults){
				if( r.Result.Equals("Valid")){
					aciertosCounter ++;
					
				}   
			}
			
			error = currentSesionResults.SpatialReasoningExerciceExecutionResults.Count * 0.8;
			minAciertos = Math.Floor (error);
			
				
			
			// Console.WriteLine ("Error count " + aciertosCounter + "Max error allowed " + minAciertos);
			
			bool res;
			// if the user is correct in more than the 80%, increase the level
			if (aciertosCounter >= minAciertos)
				res = true;
			else
				res = false;
			
			if (aciertosCounter == currentSesionResults.SpatialReasoningExerciceExecutionResults[currentSessionNumber].SingleResults.Count)
					medalValue = 100;
				else if (aciertosCounter < 5 && aciertosCounter > (currentSesionResults.SpatialReasoningExerciceExecutionResults[currentSessionNumber].SingleResults.Count -currentSesionResults.SpatialReasoningExerciceExecutionResults[currentSessionNumber].SingleResults.Count*0.4))
					medalValue = 60;
				else
					medalValue = 0;

			return res;
		}
		/// <summary>
		/// Level up the user if necessary
		/// </summary>
		public void EvaluateLevel ()
		{
			// if user was right 80% of the time & haven't reached yet de highest level...
			if (EvaluateResults () && currentLevel < ReasoningExerciseDifficulty.high)
				currentLevel++;
			//... to the next level!
		}


		public abstract ReasoningExerciseResult CheckAnswer (List<T> selected);
		
		public abstract void ayuda();
		#endregion
	}
}


