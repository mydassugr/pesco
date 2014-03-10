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
using Gtk;
using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;

namespace pesco
{

	[XmlRoot("directnumbers-exercise")]
	public class ExerciseDirectNumbers : Exercise
	{

		public static string xmlUserFile = "directnumbers.xml";

		// Singleton instance
		private static ExerciseDirectNumbers myOwnInstance = null;
		private PanelCuentaAtras panelCountdown;

		private PanelDirectNumbers panelDirectNumbers = new PanelDirectNumbers ();
		private PanelDemoNumbers panelDemoNumbers = new PanelDemoNumbers();
		
		private PanelVacio myPanel = null;

		#region Exercises variables
		private string currentSequence = "";
		private bool alreadyFailed = false;
		
		[XmlIgnore]
		public string CurrentSequence {
			get {
				return this.currentSequence;
			}
			set {
				currentSequence = value;
			}
		}

		private string currentUserSequence = "";
		private int currentPosition;
		
		[XmlIgnoreAttribute]
		public int CurrentPosition {
			get {
				return this.currentPosition;
			}
			set {
				currentPosition = value;
			}
		}
		
		private string currentMode = "";
		private int currentShowingPosition;
		private int correctsCounter = 0;
		private int errorsCounter = 0;
		private int currentTotalTries = 0;
		private int currentLevel;
		private const int maxLevel = 9;
		private int currentLevelTries = 0;
		private string [] gameNumberSequences = { "86", "14", "746", "175", "8793", "7162", "51926", "62871",
												"572846", "347528", "7158426", "1685297", "97138254", "61539274", 
												"751482963", "846537192"};
		
		#endregion

		#region Timers variables
		private int auxAnimationTime;
		private uint auxTimer;
		private GLib.TimeoutHandler currentHanlder;
		private uint currentInterval;
		private bool pausedExercise;
		private bool finishedExercise = false;
		#endregion

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
		
		[XmlIgnoreAttribute]
		// If XML file does not exists, a new instance will be returned
		DirectNumbersResults directNumbersResults = XmlUtil.DeserializeForUser<DirectNumbersResults>(Configuration.Current.GetExerciseConfigurationFolderPath () + "/DirectNumbers.xml");
		private ExerciceExecutionResult<SingleResultDirectNumbers> directNumbersExecutionResult;
		
		[XmlIgnoreAttribute]
		public List<DirectNumbersResult> resultsList;

		[XmlIgnoreAttribute]
		public int CurrentLevel {
			get { return this.currentLevel; }
			set { currentLevel = value; }
		}

		[XmlIgnoreAttribute]
		public DateTime Start {
			get { return start; }
			set { start = value; }
		}
		
		[XmlIgnoreAttribute]
		public DateTime End {
			get { return end; }
			set { end = value; }
		}
		
		public int TiempoEmpleado ()
		{
			
			TimeSpan auxTimespan = End - Start;
			return auxTimespan.Minutes * 60 + auxTimespan.Seconds;
			
		}

		// Function Singleton
		public static ExerciseDirectNumbers getInstance ()
		{
			
			if (myOwnInstance != null)
				return myOwnInstance;
			else {
				myOwnInstance = new ExerciseDirectNumbers ();
				return myOwnInstance;
			}
		}

		public override int idEjercicio ()
		{
			return 7;
		}

		public ExerciseDirectNumbers ()
		{
			category = ExerciseCategory.Memory;
			
		}

		public override void finalizar ()
		{
			if ( !pausedExercise ) {
				pausa();
			}
			
			finishedExercise = true;

			myOwnInstance = null;
		}

		public override bool inicializar ()
		{
			// Changing mode to demo mode
			SessionManager.GetInstance().ChangeExerciseStatus("demo");

			// Creating panels
			myPanel = new PanelVacio ();
			panelDemoNumbers = new PanelDemoNumbers();
			myPanel.Add (panelDemoNumbers);			
			
			// Replacing in session manager
			SessionManager.GetInstance().ReplacePanel(myPanel);
			myPanel.ShowAll();
			// Initializing panel
			panelDemoNumbers.InitPanel();			
			
			// Creating execution result
			directNumbersExecutionResult = new ExerciceExecutionResult<SingleResultDirectNumbers>(
						SessionManager.GetInstance().CurSession.IdSession, SessionManager.GetInstance().CurExecInd );
			directNumbersResults.DirectNumbersExecutionResults.Add( directNumbersExecutionResult );
			
			// Setting exercise info in results
			if ( directNumbersResults.ExerciseId == 0 ) {
				directNumbersResults.CategoryId = Convert.ToInt16(ExerciseCategory.Memory);
				directNumbersResults.ExerciseId = this.idEjercicio();
			}
			
			return true;
			
		}

		// Start game
		public override void iniciar ()
		{
			// Changing mode to game mode
			SessionManager.GetInstance().ChangeExerciseStatus("game");
			
			FirstGame();
		}

		public void FirstGame ()
		{
			
			
			// Level goes from 1 to 7
			CurrentLevel = 1;
			
			if (myPanel.Child != null)
				myPanel.Child.Destroy ();
			
			// Adding exercise panel and showing it
			myPanel.Add (panelDirectNumbers);
			panelDirectNumbers.ShowAll ();
			
			// Launching game
			NextGame ();
		}

		public bool NextGame ()
		{
			
			// Reset cursor positions
			currentPosition = 0;
			currentShowingPosition = 0;
			currentUserSequence = "";
			
			// Generate a random number sequence
			// Since 05/2011 numbers are not random anymore. This call is saved "for the future"
			// GenerateNumberSequence ();
			// Now numbers are manually defined
			GenerateFixedNumberSequence();
			
			// If this is the first time, we won't show the text "Nueva secuencia de números"
			if ( currentTotalTries == 0 ) {
				bool firstTime = true;
				panelDirectNumbers.ShowButtonStartSequence( firstTime );
			} else {
				panelDirectNumbers.ShowButtonStartSequence( false );	
			}
			
			return false;
		}

		public void NextSequence() {
			
			// Increase total tries
			currentTotalTries++;
			
			// Show first number of the sequence
			panelDirectNumbers.HideButtonsShowNumbers (currentSequence[currentShowingPosition].ToString ());
			panelDirectNumbers.ShowBlackboard();
			// Show rest of numbers of sequence, one number per second
			currentHanlder = new GLib.TimeoutHandler (ShowSequence);			
			
			currentInterval = 1000;
			auxTimer = GLib.Timeout.Add (currentInterval, currentHanlder);
			
			SessionManager.GetInstance ().RepetitionFinished ();	
		}
		
		public bool ShowSequence ()
		{
			
			// If current showed position == level we have showed all the sequence
			// So we have to hide the sequence and show the buttons
			if (currentShowingPosition == currentLevel) {
				// Hide sequence and show buttons
				panelDirectNumbers.HideNumbersShowButtons ();
				panelDirectNumbers.EnableButtons ();
				// Start timer
				Start = DateTime.Now;
				return false;
			// If not we have to show the rest of the sequence
			} else {
				currentShowingPosition++;
				panelDirectNumbers.setLabelNumero (currentSequence[currentShowingPosition].ToString ());
				return true;
			}
		}

		public void GenerateRandomNumberSequence ()
		{
			
			// Reset current sequence
			currentSequence = "";
			int auxLastNumber = -1;
			// Generate random number sequence with lenght = level + 1
			Random r = new Random (DateTime.Now.Millisecond);
			int i = 0;
			for (i = 0; i < currentLevel + 1; i++) {
				int auxNumber = r.Next (0, 9);
				while (auxNumber == auxLastNumber) {
					auxNumber = r.Next (0, 9);
				}
				currentSequence = currentSequence.Insert (i, auxNumber.ToString ());
				auxLastNumber = auxNumber;
			}
			
		}
		
		public void GenerateFixedNumberSequence ()
		{
			
			// Reset current sequence
			currentSequence = "";
			int auxLastNumber = -1;
			// Generate random number sequence with lenght = level + 1
			// Level goes from 1 to 7, so first number position will be at index 0. That means index = level - 1.
			currentSequence = gameNumberSequences[ ( (currentLevel - 1) *2) + currentLevelTries ];
			
		}

		/* public void NumberPressed (char numero)
		{
			currentUserSequence = currentUserSequence.Insert (currentPosition, numero.ToString ());
			// If the number pressed is right and we have written all the numbers
			// of the sequence, we increment the corrects counter. If corrects counter is 2
			// we increase the level
			if (currentSequence[currentPosition].CompareTo (numero) == 0 && currentPosition == currentLevel) {
				CurrentPosition++;
				panelDirectNumbers.UpdateLabelWithSequence();				
				currentUserSequence = currentUserSequence.Insert (currentPosition, numero.ToString ());
				currentLevelTries++;
				correctsCounter++;
				panelDirectNumbers.DisableButtons ();				
				End = DateTime.Now;
				int timeElapsed = TiempoEmpleado ();
				// resultsList.Add (new DirectNumbersResult (CurrentLevel, currentSequence, currentUserSequence, "ok", t));
				// Changed to fix new data format for results
				directNumbersResults.setResultado( currentSequence, currentUserSequence, currentLevel, "ok", timeElapsed );
				Serialize ();
				// If we have tried 2 sequences in this level, at least 1 is correct, and there are
				// more levels, we increase the level
				if ( currentLevel < 8 && currentLevelTries == 2 && correctsCounter >= 1) {
					correctsCounter = 0;
					errorsCounter = 0;
					currentLevelTries = 0;
					currentLevel++;
				}
				GLib.Timeout.Add (1000, new GLib.TimeoutHandler (NextGame));
			// If sequence is right until current position but sequence is not completed,
			// we increment the cursor position
			} else if (currentSequence[currentPosition].CompareTo (numero) == 0) {
				currentPosition++;
				panelDirectNumbers.UpdateLabelWithSequence();
			// If there is a mistake in the number we have to increment the errors counter
			// If the errors counter reach the max possible errors the exercise is finished
			} else if (currentSequence[currentPosition].CompareTo (numero) != 0) {
				currentLevelTries++;
				errorsCounter++;
				panelDirectNumbers.DisableButtons ();
				End = DateTime.Now;
				int timeElapsed = TiempoEmpleado ();
				// resultsList.Add (new DirectNumbersResult (CurrentLevel, currentSequence, currentUserSequence, "fail", t));
				// Changed to fix new data format
				directNumbersResults.setResultado( currentSequence, currentUserSequence, currentLevel, "fail", timeElapsed );
				Serialize ();
				// If we have tried 2 sequences in this level, at least 1 is correct, and there are
				// more levels, we increase the level
				if ( currentLevel < 8 && currentLevelTries == 2 && correctsCounter >= 1) {
					correctsCounter = 0;
					errorsCounter = 0;
					currentLevelTries = 0;
					currentLevel++;
					GLib.Timeout.Add (2000, new GLib.TimeoutHandler (NextGame));
				} 
				// If errors counter is 0, we increase it
				else if ( errorsCounter == 1 ) {
					GLib.Timeout.Add (2000, new GLib.TimeoutHandler (NextGame));
				} 
				// If errors counter is 2, we have failed this level, so game finish
				else if (errorsCounter == 2) {
					int score = 0;
					if ( CurrentLevel < 4 ) {
						score = 30;
					} else if ( CurrentLevel <= 4 && CurrentLevel < 6 ) {
						score = 75;	
					} else if ( CurrentLevel >= 6  ) {
						score = 100;
					}
					SessionManager.GetInstance ().ExerciseFinished (score);
					SessionManager.GetInstance ().TakeControl ();
					finalizar ();
				}
			}
			
		}*/

		public void NumberPressed (char numero)
		{
			if ( numero.CompareTo('B') == 0 ) {
				if ( currentUserSequence.Length > 0 ) {
					currentUserSequence = currentUserSequence.Substring(0, currentUserSequence.Length - 1);
					CurrentPosition--;
				}
			} else {
				currentUserSequence = currentUserSequence.Insert (currentPosition, numero.ToString ());
				CurrentPosition++;				
			}
			panelDirectNumbers.UpdateLabelWithSequence();
			// If the number pressed is right and we have written all the numbers
			// of the sequence, we increment the corrects counter. If corrects counter is 2
			// we increase the level
			if ( CurrentSequence.Length == currentUserSequence.Length ) {
				// Disable buttons
				panelDirectNumbers.DisableButtons ();
				// Get elapsed time
				End = DateTime.Now;
				int timeElapsed = TiempoEmpleado ();	
				// Sequence is OK!
				if ( currentSequence.CompareTo (currentUserSequence) == 0 ) {
					currentLevelTries++;
					correctsCounter++;
					directNumbersResults.setResultado( currentSequence, currentUserSequence, currentLevel, "OK", timeElapsed );
					Serialize ();
					if ( currentLevel < maxLevel && currentLevelTries == 2 && correctsCounter >= 1) {
						correctsCounter = 0;
						errorsCounter = 0;
						currentLevelTries = 0;
						currentLevel++;
						// If level has reached 9, there are not more levels so game is ended
						if ( currentLevel == maxLevel ) {
							directNumbersResults.CurrentExecution.Score = 100;
							Serialize();
							SessionManager.GetInstance ().ExerciseFinished (100);
							SessionManager.GetInstance ().TakeControl ();
							finalizar ();
						} else {
							currentInterval = 1000;
							currentHanlder = new GLib.TimeoutHandler (NextGame);
							auxTimer = GLib.Timeout.Add ( currentInterval, currentHanlder );
						}
					} else if ( currentLevel < maxLevel && currentLevelTries == 1 ) {
						currentInterval = 1000;
						currentHanlder = new GLib.TimeoutHandler (NextGame);
						auxTimer = GLib.Timeout.Add ( currentInterval, currentHanlder );
					}
				}
				// Sequence is WRONG!
				else {
					currentLevelTries++;
					errorsCounter++;
					directNumbersResults.setResultado( currentSequence, currentUserSequence, currentLevel, "FAIL", timeElapsed );	
					Serialize ();				
					// If we have tried 2 sequences in this level, at least 1 is correct, and there are
					// more levels, we increase the level
					if ( currentLevel < maxLevel && currentLevelTries == 2 && correctsCounter >= 1) {
						Console.WriteLine("Un acierto, un error");
						correctsCounter = 0;
						errorsCounter = 0;
						currentLevelTries = 0;
						currentLevel++;
						if ( currentLevel == maxLevel ) {
							Console.WriteLine("Nivel máximo alcanzado, saliendo");
							directNumbersResults.CurrentExecution.Score = 100;
							Serialize();
							SessionManager.GetInstance ().ExerciseFinished (100);
							SessionManager.GetInstance ().TakeControl ();
							finalizar ();
						} else {
							currentInterval = 1000;
							currentHanlder = new GLib.TimeoutHandler (NextGame);
							auxTimer = GLib.Timeout.Add ( currentInterval, currentHanlder );
						}
					}
					// If this is not second try, play again
					else if ( currentLevelTries != 2 ) {
						currentInterval = 1000;
						currentHanlder = new GLib.TimeoutHandler (NextGame);
						auxTimer = GLib.Timeout.Add ( currentInterval, currentHanlder );
					}
					// If errors counter is 2, or we have reached last level but we have failed in second try,
					// the game finish
					else if ( errorsCounter == 2 || currentLevel == maxLevel ) {
						int score = 0;
						if ( CurrentLevel < 4 ) {
							score = 30;
						} else if ( CurrentLevel <= 4 && CurrentLevel < 6 ) {
							score = 75;	
						} else if ( CurrentLevel >= 6  ) {
							score = 100;
						}
						directNumbersResults.CurrentExecution.Score = score;
						Serialize();
						SessionManager.GetInstance ().ExerciseFinished (score);
						SessionManager.GetInstance ().TakeControl ();
						finalizar ();
					}			
				}				
				Serialize ();
			}
			
		}
		
		public override void pausa ()
		{
			
			if ( finishedExercise ) {
				return;	
			}
				
			if (!pausedExercise) {
				pausedExercise = true;
				if ( SessionManager.GetInstance().ExerciseStatus == "game" ) {
					GLib.Source.Remove (auxTimer);
				}
			} else {
				pausedExercise = false;
				if ( SessionManager.GetInstance().ExerciseStatus == "game" ) {
					auxTimer = GLib.Timeout.Add (currentInterval, currentHanlder);
				}
			}
			
		}

		public override string NombreEjercicio ()
		{
			return "Dígitos directos";
		}

		public void Serialize ()
		{
			
			XmlUtil.SerializeForUser<DirectNumbersResults>(directNumbersResults,Configuration.Current.GetExerciseConfigurationFolderPath () + "/DirectNumbers.xml");
			/* string fullPath = Configuration.Current.GetExerciseConfigurationFolderPath () + "/" + xmlUserFile;
			
			if (!Directory.Exists (Configuration.Current.GetExerciseConfigurationFolderPath ()))
				Directory.CreateDirectory (Configuration.Current.GetExerciseConfigurationFolderPath ());
			
			XmlTextWriter escritor = new XmlTextWriter (fullPath, null);
			
			try {
				escritor.Formatting = Formatting.Indented;
				escritor.WriteStartDocument ();
				escritor.WriteDocType ("directnumbers-exercise", null, null, null);
				
				XmlSerializer serializer = new XmlSerializer (typeof(ExerciseDirectNumbers));
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
		public static ExerciseDirectNumbers Deserialize ()
		{
			ExerciseDirectNumbers exercise = new ExerciseDirectNumbers();
			return (ExerciseDirectNumbers) exercise;
			
			/*
			string fullPath = Configuration.Current.GetExerciseConfigurationFolderPath () + "/" + xmlUserFile;
			
			Console.WriteLine(fullPath);
			if (!File.Exists (fullPath)) {
				string s = Environment.CommandLine;
				fullPath = Configuration.CommandDirectory + "/ejercicios/DigitosDirectos/xml-templates/" + xmlUserFile;
				Console.WriteLine ("Full path: " + fullPath);
			}
			
			XmlTextReader lector = new XmlTextReader (fullPath);
			try {
				//ExerciseDirectNumbers exercise = ExerciseDirectNumbers.getInstance ();
				ExerciseDirectNumbers exercise = new ExerciseDirectNumbers();
								
				XmlSerializer serializer = new XmlSerializer (typeof(ExerciseDirectNumbers));
				exercise = (ExerciseDirectNumbers)serializer.Deserialize (lector);
				myOwnInstance = exercise;
				
				lector.Close ();
				return (ExerciseDirectNumbers)exercise;
			} catch (Exception e) {
				lector.Close ();
				return null;
			}*/

		}
		
	}
	
}


