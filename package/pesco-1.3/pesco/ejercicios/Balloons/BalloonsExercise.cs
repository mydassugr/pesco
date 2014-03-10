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
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using Gtk;
using System;
using System.Collections.Generic;

namespace pesco
{


	public class BalloonsExercise : Exercise
	{
		
		private static string xmlUserFile = "BalloonsExercise.xml";
		
		#region Singleton instance
		private static BalloonsExercise myOwnInstance = null;
		#endregion
		
		#region Exercise panels
		private BalloonsPanel balloonsPanel = null;
		private PanelVacio myPanel = null;
		#endregion
		
		#region Variables temporizadores
		private bool pausedExercise;
		#endregion				
		// private List <BalloonsResults> results = new List<BalloonsResults>();
		// Results
		// If XML file does not exists, a new instance will be returned
		BalloonsResults balloonsResults = XmlUtil.DeserializeForUser<BalloonsResults>(Configuration.Current.GetExerciseConfigurationFolderPath () + "/Balloons.xml");
		
		// Time elapsed calculation
		DateTime Start;
		DateTime End;
		
		// Level number values are 1, 2, 3
		// private int currentLevel = 1;
		// IMPORTANT: Sublevel number values are 0, 1, 2
		// private int currentSubLevel = 0;
		private int currentCorrects = 0;
		private int currentFails = 0;
		private int currentOmissions = 0;
		
		[XmlElement("PixelsPerFrameSubLevel")]
		public int [] PixelsPerFrameSubLevel = { 7, 10, 14 };
		
		// Ballons per sublevel ( faster = more ballons, it has to be, more or less, 2 minutes per serie )
		// Fixed: 40 ballons for all levels
		[XmlElement("BallonsPerSubLevel")]
		public int [] ballonsPerSubLevel = { 40, 40, 40 };
				
		[XmlIgnoreAttribute]
		public int CurrentLevel {
			get {
				return balloonsResults.CurrentLevel;
			}
			set {
				balloonsResults.CurrentLevel = value;
			}
		}
		
		[XmlIgnoreAttribute]
		public int CurrentSubLevel {
			get {
				return balloonsResults.CurrentSubLevel;
			}
			set {
				balloonsResults.CurrentSubLevel = value;
			}
		}
		
		[XmlIgnore]
		public int CurrentOmissions {
			get {
				return currentOmissions;
			}
			set {
				currentOmissions = value;
			}
		}
		
		[XmlIgnore]
		public int CurrentFails {
			get {
				return currentFails;
			}
			set {
				currentFails = value;
			}
		}
		
		[XmlIgnore]
		public int CurrentCorrects {
			get {
				return currentCorrects;
			}
			set {
				currentCorrects = value;
			}
		}		
		
		// Function Singleton
		public static BalloonsExercise getInstance() {
		
			if ( myOwnInstance != null )
				return myOwnInstance;
			else
				myOwnInstance = new BalloonsExercise();
				return myOwnInstance;
		}		
		
		public BalloonsExercise ()			
		{
			category = ExerciseCategory.Attention;
		}

		public override void finalizar ()
		{
			
			if ( !pausedExercise )
				pausa();
			
			myOwnInstance = null;
			
		}
		
		public override bool inicializar ()
		{			
			// Exercise starts in demo mode. Notifying to session manager
			SessionManager.GetInstance().ChangeExerciseStatus("demo");
			
			// Init results
			ExerciceExecutionResult<SingleResultBalloons> balloonsExecutionResult = 
					new ExerciceExecutionResult<SingleResultBalloons>(
						SessionManager.GetInstance().CurSession.IdSession, SessionManager.GetInstance().CurExecInd );
			balloonsResults.BalloonsExecutionResults.Add( balloonsExecutionResult );
			balloonsResults.ResetExecution();
			
			// Results
			if ( balloonsResults.CategoryId == 0 || balloonsResults.ExerciseId == 0 ) {
				balloonsResults.CategoryId = Convert.ToInt16(ExerciseCategory.Attention);
				balloonsResults.ExerciseId = this.idEjercicio();
				
			}
			
			if ( balloonsResults.CurrentLevel == 0 ) {
				balloonsResults.CurrentLevel = 1;
			}
			
			// Init panels
			myPanel = new PanelVacio();
			balloonsPanel = new BalloonsPanel();
			
			myPanel.Add(balloonsPanel);
			
			SessionManager.GetInstance().ReplacePanel(myPanel);
			myPanel.Show();
			
			// Init variables and demo
			balloonsPanel.InitVariables();
			balloonsPanel.InitDemo();
			
			return true;
		}
		
		public override void iniciar ()
		{
			if ( this.myPanel.Child != null )
				this.myPanel.Child.Destroy();
			
			myPanel.Add(balloonsPanel);
			myPanel.ShowAll();
			
			balloonsPanel.InitVariables();
			balloonsPanel.NextExecution();
		}
		
		public void SerieFinished() {
			
			int auxTotalCorrects = CurrentCorrects + CurrentOmissions;
			double porcentageCorrect = 0.01;
			// Avoid divisions by zero. It is almost impossible.
			if ( auxTotalCorrects > 0 ) {
				porcentageCorrect = (double) (CurrentCorrects-CurrentFails) / (double) auxTotalCorrects;
			}
			// Save results
			TimeSpan timeElapsed = balloonsPanel.End - balloonsPanel.Start;
			balloonsResults.setResultado( balloonsPanel.GetSerie(), ballonsPerSubLevel[CurrentSubLevel], auxTotalCorrects, CurrentCorrects, CurrentFails, CurrentOmissions,
			                             CurrentLevel, CurrentSubLevel, (int) timeElapsed.TotalSeconds) ;
			balloonsResults.CurrentExecution.TotalCorrects += CurrentCorrects;
			balloonsResults.CurrentExecution.TotalFails += CurrentFails;
			balloonsResults.CurrentExecution.TotalOmissions += CurrentOmissions;
			balloonsResults.CurrentExecution.TotalTimeElapsed += (int) timeElapsed.TotalSeconds;
			balloonsResults.CurrentExecution.LevelReached = CurrentLevel.ToString();

			Serialize();
			SessionManager.GetInstance().RepetitionFinished();
			
			// Reset results
			CurrentCorrects = 0;
			CurrentFails = 0;
			CurrentOmissions = 0;
					
			// Do we have to finish?
			if ( SessionManager.GetInstance().HaveToFinishCurrentExercise() ) {
				SessionManager.GetInstance().ExerciseFinished( (int) porcentageCorrect * 100 );
				SessionManager.GetInstance().TakeControl();
				finalizar();
			} else {			
				// If not, check level
				// If porcentage > 0.8 we have to increase the current sublevel.
				// If we are in max sublevel, we have to increase level and launch demo.
				// Remember, Level values are 1,2,3. SubLevel values are 0,1,2.				
				if ( porcentageCorrect > 0.8 && !( CurrentSubLevel == 2 && CurrentLevel == 3 ) ) {
					// Are there more sublevels? Increase it
					bool levelIncreased = false;
					if ( CurrentSubLevel < 2 ) {
						CurrentSubLevel++;
					} 
					// If not, increase level
					else if ( CurrentSubLevel == 2 ) {
						levelIncreased = true;
						CurrentLevel++;
						CurrentSubLevel = 0;
					}					
					Serialize();
					string levelText = "";
					// If we have increased the level we have to notice user using the demo...
					// ...but just if we are not going to finish the exercise
					if ( !SessionManager.GetInstance().HaveToFinishCurrentExercise() && levelIncreased ) {
						balloonsPanel.InitDemo( 1 );
					} else if ( !SessionManager.GetInstance().HaveToFinishCurrentExercise() && !levelIncreased ) {
						balloonsPanel.NextExecution();
					}
				}
				// If we haven't increase level, launch next execution
				else {				
					balloonsPanel.NextExecution();		
				}
			}
			
		}
		
		public void Serialize() {			
			XmlUtil.SerializeForUser<BalloonsExercise>( this, Configuration.CommandExercisesDirectory+ Path.DirectorySeparatorChar + xmlUserFile);
			XmlUtil.SerializeForUser<BalloonsResults>( balloonsResults,Configuration.Current.GetExerciseConfigurationFolderPath () + Path.DirectorySeparatorChar + "Balloons.xml" );
		}
		
		public override int idEjercicio ()
		{
			return 4;
		}
		
		public override void pausa ()
		{
			balloonsPanel.pausa();
		}	
		
		public override string NombreEjercicio ()
		{
			return "Globos";
		}
		
		public static BalloonsExercise Deserialize()
		{
			
			BalloonsExercise exercise = new BalloonsExercise();
			myOwnInstance = exercise;
			return exercise;
			
			/* string fullPath = Configuration.Current.GetExerciseConfigurationFolderPath() + "/" + xmlUserFile;
			
			if (!File.Exists(fullPath))
			{
				string s = Environment.CommandLine;			
				fullPath = Configuration.CommandDirectory + "/ejercicios/Ballons/xml-templates/" + xmlUserFile;
			}
			
			XmlTextReader lector = new XmlTextReader(fullPath);
			try
			{	
				BallonsExercise exercise = new BallonsExercise();
				
				XmlSerializer serializer = new XmlSerializer(typeof(BallonsExercise));				
				exercise = (BallonsExercise) serializer.Deserialize(lector);
				
				myOwnInstance = exercise;
				
				lector.Close();				
				return exercise;
			}
			catch( Exception e)
			{
				Console.WriteLine( e.ToString() );
				lector.Close();
				return null;
			}*/
		}
	}
}

