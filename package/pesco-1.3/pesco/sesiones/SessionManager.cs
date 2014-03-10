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
using System.Collections.Generic;
using System;
using Gtk;
using System.Diagnostics;
using System.Net;
using System.Collections.Specialized;
using System.Threading;

namespace pesco
{


	public class SessionManager
	{
		/// Main window of the application
		SessionWindow p;
		Widget w;

		int totalGold = 0;
		int totalSilver = 0;
		int totalBronze = 0;

		private bool forceAbort = false;

		/// XML filename to save session-manager
		public static string xmlUserFile = "session-manager.xml";

		/// Current session and exercise entry from user profile sessions
		protected Session curSession = null;
		protected ExerciseEntry currentExerciseEntry = null;

		/// List of executions
		List<Execution> executions = new List<Execution> ();

		/// Indexs of executions and execution exercises lists
		protected int curExecInd = -1;
		protected int curExecExerciseInd = -1;
		protected int curSessExerciseInd = -1;

		// Singleton instance
		protected static SessionManager current;

		// Aux variable to keep exercise status
		private string exerciseStatus = "nonstarted";
		
		[XmlIgnoreAttribute]
		public string ExerciseStatus {
			get {
				return this.exerciseStatus;
			}
			set {
				exerciseStatus = value;
			}
		}

		private string backupStatus = "";
		private bool finishTimer = false;
		[XmlIgnoreAttribute]
		public Exercise e = null;

		//  Score
		private int exerciseScore;

		// Computer speed 
		// Console.WriteLine( "Frequency: " + System.Diagnostics.Stopwatch.Frequency );
		private long interval = 0;
		private int fps = 60;
		// Console.WriteLine( "Interval: " + interval );
		private long currenTicks = System.Diagnostics.Stopwatch.GetTimestamp ();
		// Console.WriteLine( "Interval: " + ticks );

		[XmlElement("executions")]
		public List<Execution> Executions {
			get { return executions; }
			set { executions = value; }
		}

		[XmlIgnore]
		public int CurExecExerciseInd {
			get { return curExecExerciseInd; }
			set { curExecExerciseInd = value; }
		}

		[XmlIgnore]
		public int CurExecInd {
			get { return curExecInd; }
			set { curExecInd = value; }
		}

		[XmlIgnore]
		private ExerciseEntry CurrentExerciseEntry {
			get { return currentExerciseEntry; }
			set { currentExerciseEntry = value; }
		}
		[XmlIgnore]
		public Session CurSession {
			get { return curSession; }
			set { curSession = value; }
		}

		[XmlIgnore]
		public long Interval {
			get { return interval; }
			set { interval = value; }
		}

		[XmlIgnore]
		public long CurrenTicks {
			get { return currenTicks; }
			set { currenTicks = value; }
		}

		[XmlIgnore]
		public int Fps {
			get { return fps; }
			set { fps = value; }
		}



		private SessionManager ()
		{
		}

		public void StartSessionManager ()
		{
			// Calculate computer speed
			interval = (long)(Stopwatch.Frequency / Fps);
			
			// Create main window
			p = new SessionWindow ();
			
			// Add timers
			GLib.Timeout.Add (10000, UpdateTimers);
			
			// Are there executions already?
			if (executions == null) {
				CurExecInd = 0;
			} else {
				CurExecInd = executions.Count;
			}
			
			// Add current execution
			executions.Add (new Execution (CurExecInd));
			
			executions[CurExecInd].InitTimers ();
			// Remove comment in this line to enable upload files:
			// CompressAndUploadProfile ();
			ChooseNextExercise ();
		}

		private void ChooseNextExercise ()
		{
			
			CurExecExerciseInd = -1;
			
			UserSessions userSessions = UserSessions.GetInstance ();
			
			Execution auxExecution = GetLastExecutionWithExercises ();
			
			// Case 1: If we don't have executions, we'll start with the first session
			// This should happens just the first time we launch the application.
			if (auxExecution == null) {
				//Console.WriteLine ("Case 0: There aren't executions with exercises. This is the first one.");
				FirstExecution ();
				return;
			}
			
			// We have an execution with exercises. Get session and last exercise.
			Session auxSession = userSessions.GetSession (auxExecution.SessionId);
			ExerciseExecution auxLastExercise = auxExecution.GetLastExercise ();
			
			//Console.WriteLine ("Case 2-3: We have executions with exercises already.");
			// Check if we finish a session in the last execution
			if (auxLastExercise.ExerciseId == auxSession.GetLastExercise ().Id && (auxLastExercise.Result == "completed" || auxLastExercise.Result == "ignored")) {
				// Case 2: New session
				if (auxSession.IdSession != userSessions.Sessions.Count) {
					//Console.WriteLine ("Case 2: Session finished in last execution. Starting new execution with new session.");
					AddExercise (auxSession.IdSession + 1, 0);
					// Case 3: No sessions left
				} else {
					//Console.WriteLine ("Case 3: Session finished in last execution. There aren't sessions left!");
					p.SetLabelText ("Ya has finalizado todas las sesiones. ¡Enhorabuena!");
					p.StartSessionButton.Label = "Salir";
					p.StartSessionButton.Clicked += delegate {
						SessionManager.GetInstance().FinishApplication();
					};
				}
				// We didn't finish a session, so we have to continue with the last exercise
			} else {
				int exercisePosition = userSessions.GetExerciseIdPosition (auxExecution.SessionId, auxLastExercise.ExerciseId);
				// Case 4-5: Last exercise was finished
				if (auxLastExercise.Result == "completed" || auxLastExercise.Result == "ignored") {
					// Get exercise after last exercise finished
					//Console.WriteLine ("Case 4-5: Session didn't finish in last execution. Last exercise was completed or ignored. Continuing with next exercise.");
					AddExercise (auxExecution.SessionId, exercisePosition + 1);
					// Case 6: Last exercise wasn't finished, continue with it
				} else {
					//Console.WriteLine ("Case 6: Session didn't finish in last execution. Last exercise was completed or ignored. Repeating last exercise.");
					AddExercise (auxExecution.SessionId, exercisePosition);
				}
			}
		}

		private void FirstExecution ()
		{
			
			UserSessions userSessions = UserSessions.GetInstance ();
			
			curSession = userSessions.GetSession (1);
			
			curSessExerciseInd = 0;
			
			currentExerciseEntry = curSession.GetExerciseEntry (curSessExerciseInd);
			// CurrentExerciseEntry.showInfo ();
			
			executions[CurExecInd].SessionId = 1;
			
			curExecExerciseInd = 0;
			executions[CurExecInd].AddExercise (currentExerciseEntry);
			
			Executions[curExecInd].Exercises[curExecExerciseInd].InitTimers ();
			p.SetLabelText (curSession.Starttext);
			p.Show ();
		}

		private void AddExercise (int sessionid, int exerciseposition)
		{
			
			// Get session required
			UserSessions userSessions = UserSessions.GetInstance ();
			
			CurSession = userSessions.GetSession (sessionid);
			
			// Get exercise required
			curSessExerciseInd = exerciseposition;
			currentExerciseEntry = curSession.GetExerciseEntry (exerciseposition);
			
			curExecExerciseInd = executions[CurExecInd].Exercises.Count;
			
			// Add execution
			executions[CurExecInd].SessionId = sessionid;
			
			// Check exercise dependency
			// ExerciseEntry auxExerciseEntry = CurSession.GetExerciseEntry (exerciseposition);
			ExerciseEntry auxExerciseEntry = currentExerciseEntry;
			executions[CurExecInd].AddExercise (auxExerciseEntry);
			
			// Console.WriteLine ("Checking independecy in exercise " + auxExerciseEntry.Id + ". " + "It is: " + auxExerciseEntry.DependsOn);
			if (auxExerciseEntry.IsIndependent ()) {
				// Console.WriteLine ("Case 4,6: Exercise " + auxExerciseEntry.Id + " is independent, launching it.");
			} else if (!auxExerciseEntry.IsIndependent ()) {
				DateTime auxLastDependsOnExerDate = GetLastExerExecDate (auxExerciseEntry.DependsOn);
				bool ignoreExercise = false;
				TimeSpan auxElapsedTime = DateTime.Now - auxLastDependsOnExerDate;
				
				// Console.WriteLine ("Case 5,7: Exercise " + auxExerciseEntry.Id + " is dependent. Checking elapsed time.");
				// Console.WriteLine ("Case 5,7: Elapsed time is: " + (auxElapsedTime.TotalMinutes) + " minutes.");
				
				if ((int)auxElapsedTime.TotalMinutes > auxExerciseEntry.DependsMaxTime) {
					// Console.WriteLine ("Case 5,7: Elapsed time (" + auxElapsedTime.TotalMinutes + ") is longer than " + "DependsMaxTime (" + (auxExerciseEntry.DependsMaxTime) + "). Launching exercise.");
					ignoreExercise = true;
				} else {
					// Console.WriteLine ("Case 5,7: Elapsed time (" + auxElapsedTime.TotalMinutes + ") is smaller than " + "DependsMaxTime (" + (auxExerciseEntry.DependsMaxTime) + "). Launching exercise.");
				}
				
				if (ignoreExercise) {
					executions[CurExecInd].Exercises[curExecExerciseInd].Result = "ignored";
					Console.WriteLine (CurSession.GetLastExercise ().Id + " vs " + auxExerciseEntry.Id);
					// If exercise is ignored we have to check if it the last, to go to next
					if (CurSession.GetLastExercise ().Id != auxExerciseEntry.Id) {
						ChooseNextExercise ();
						// or to finish session
					} else {
						// TODO: If it is the last, don't launch it!!
						forceAbort = true;
						Serialize ();
						p.Remove (p.Child);
						FinishTimer();
						TotalPodiumPanel auxPodium = new TotalPodiumPanel ();
						p.Add (auxPodium);
						p.ShowAll ();
						auxPodium.InitPanel ();
					}
					
					return;
				}
				
			}
			
			Executions[curExecInd].Exercises[curExecExerciseInd].InitTimers ();
			if ( exerciseposition == 0 ) {
				p.SetLabelText (CurSession.Starttext);
			} else {
				p.SetLabelText ("¡Hola! La última vez que entraste en PESCO te quedaste por la sesión "+sessionid+". Ahora continuaremos por esa sesión.");
			}
			p.Show ();
			
		}

		public void LaunchExercise ()
		{
			if (forceAbort != true) {
				
				// Getting exercise id
				int ejercicioId = CurrentExerciseEntry.Id;
				
				// Creating exercise instance
				e = Exercise.GetEjercicio (ejercicioId);
				e.ExecutionId = CurExecInd;
				
				// Changing status. This is supposed to be changed to "demo" 
				// by the exercise as soon as possible!
				exerciseStatus = "started";
				
				// Initializing exercise
				e.inicializar ();
				
				if (ejercicioId < 4 || ejercicioId == 8 || ejercicioId == 9 || (ejercicioId >= 11 && ejercicioId!=30))
					e.iniciar ();
			}
			
		}

		public void ReplacePanel (Gtk.Bin newPanel)
		{
			
			p.HideIntroSessionsShowExercise (newPanel);
			newPanel.Show ();
			
		}

		public Gtk.Widget GetTopLevelWin ()
		{
			return p.Toplevel;
		}

		public static SessionManager GetInstance ()
		{
			return current;
		}

		private Execution GetLastExecution ()
		{
			
			if (Executions.Count == 0)
				return null;
			else
				return Executions[Executions.Count - 1];
			
		}

		private DateTime GetLastExerExecDate (int exerciseId)
		{
			
			for (int i = Executions.Count - 1; i >= 0; i--) {
				for (int j = Executions[i].Exercises.Count - 1; j >= 0; j--) {
					if (Executions[i].Exercises[j].ExerciseId == exerciseId && Executions[i].Exercises[j].IsCompleted ())
						return Executions[i].Exercises[j].EndTime;
				}
			}
			
			return DateTime.MinValue;
		}

		private Execution GetLastExecutionWithExercises ()
		{
			
			if (Executions.Count == 0) {
				return null;
			} else {
				for (int i = Executions.Count - 1; i >= 0; i--) {
					if (Executions[i].Exercises.Count > 0) {
						// Console.WriteLine ("Last execution with exercises is: " + i);
						return Executions[i];
					}
				}
			}
			
			return null;
		}

		// Function called by exercises to change "demo" mode to "game" mode
		public void ChangeExerciseStatus (string mode)
		{
			
			// Console.WriteLine( "Changing exercise status" );
			// Is mode valid?
			if (mode != "demo" && mode != "game") {
				Console.WriteLine (mode + " mode is not valid!");
			}
			// Is there a exercise running?
			if (CurExecInd != -1 && CurExecExerciseInd != 1) {
				exerciseStatus = mode;
			}
			
		}

		// Function called by exercises to return the control of the application
		public void TakeControl ()
		{
			
			if(exerciseScore >=0){
				PodiumPanel panel = new PodiumPanel (exerciseScore);
				panel.InitPanel ();
				
				ReplacePanel (panel);
				
				panel.ButtonOK.Clicked += delegate {
					
					// If we have done all the exercises of the session, the session is finished.
					if ((curSessExerciseInd + 1) == curSession.Exercises.Count) {
						FinishTimer();
						// Remove next line to enable upload files at the end of a session
						CompressAndUploadProfile ();
						// Show total podium of the session
						TotalPodiumPanel auxTotalPodiumPanel = new TotalPodiumPanel ();
						ReplacePanel (auxTotalPodiumPanel);
						auxTotalPodiumPanel.InitPanel ();
					} 
					// If not, launch next exercise
					else {					
						panel.Destroy ();
						ChooseNextExercise ();
						LaunchExercise ();
					}
				};
			}
			else {
				if ((curSessExerciseInd + 1) == curSession.Exercises.Count) {
					FinishTimer();
					// Remove next line to enable upload files at the end of a session
					// CompressAndUploadProfile ();
					// Show total podium of the session
					if ( CurSession.IdSession == 1 ) {
						string message = "¡Muy bien! Has terminado la primera sesión. " +
						"En la siguiente sesión, realizarás una serie de ejercicios que medirán " +
						" tu nivel de memoria, atención, razonamiento y planificación. ¡Hasta pronto!";
						PESCOGoodByeDialog pgbd = new PESCOGoodByeDialog();
						pgbd.SetText(message);
						SessionManager.GetInstance().ReplacePanel(pgbd);
						pgbd.Dialog.InitPanel();
					} else {
						TotalPodiumPanel auxTotalPodiumPanel = new TotalPodiumPanel ();
						ReplacePanel (auxTotalPodiumPanel);
						auxTotalPodiumPanel.InitPanel ();	
					}
				} else {
					ChooseNextExercise ();
					LaunchExercise ();
				}
			}
			
		}

		/// <summary>
		/// 
		/// </summary>
		public void ExerciseFinished (int score)
		{
			
			MedalValue mv = MedalValue.Bronze;
			if (score > 80) {
				totalGold++;
				mv = MedalValue.Gold;
			} else if (score <= 80 && score > 50) {
				totalSilver++;
				mv = MedalValue.Silver;
			} else if (score <= 50 && score >= 0) {
				mv = MedalValue.Bronze;
				totalBronze++;
			}
			
			Executions[CurExecInd].Exercises[CurExecExerciseInd].Result = "completed";
			this.exerciseScore = score;
			
			if(score >= 0){
				User u = User.Deserialize ();
				
				u.Medals.Add (new Medal (mv, DateTime.Now, currentExerciseEntry.Id, curSession.IdSession));
				
				u.Serialize ();
			}
			// TODO: cognitive screening => evaluate profile
			if (this.curSession.IdSession == 1) {
				
			}
			
			Serialize ();
		}

		/// Function called by exercises to know if they have to finish
		public bool HaveToFinishCurrentExercise ()
		{
			
			if (CurrentExerciseEntry.DurationKind == "notime") {
				return false;
			} else if (CurrentExerciseEntry.DurationKind == "time") {
				if (Executions[CurExecInd].Exercises[curExecExerciseInd].RealTime > (CurrentExerciseEntry.Duration * 60)) {
					return true;
				}
				return false;
			} else if (CurrentExerciseEntry.DurationKind == "repeat") {
				if (Executions[CurExecInd].Exercises[CurExecExerciseInd].Repetitions >= CurrentExerciseEntry.Repetitions) {
					return true;
				}
				return false;
			}
			return false;
			
		}

		// Function used by exercises to notice that it is 
		public void RepetitionFinished ()
		{
			Executions[curExecInd].Exercises[curExecExerciseInd].Repetitions++;
			// Console.WriteLine ("New repetition. Current numer of repetitions " + Executions[curExecInd].Exercises[curExecExerciseInd].Repetitions);
		}

		// Function used by exercises to finish the application
		public void FinishApplication ()
		{
			FinishTimer ();
			p.Destroy ();
			p.Dispose ();
			Application.Quit ();
		}

		/// <summary>
		/// 
		/// </summary>
		public void PauseExercise ()
		{
			backupStatus = exerciseStatus;
			exerciseStatus = "paused";
		}

		/// <summary>
		/// 
		/// </summary>
		public void UnpauseExercise ()
		{
			exerciseStatus = backupStatus;
			backupStatus = "";
		}

		// Function used to finish timer
		public void FinishTimer ()
		{
			finishTimer = true;
		}

		private bool UpdateTimers ()
		{
			
			if (finishTimer == true)
				return false;
			
			if (CurExecInd != -1) {
				Executions[CurExecInd].UpdateTimers ();
			}
			
			if (curExecInd != -1 && curExecExerciseInd != -1 && (exerciseStatus == "demo" || exerciseStatus == "game" || exerciseStatus == "started" || exerciseStatus == "paused")) {
				Executions[CurExecInd].Exercises[curExecExerciseInd].UpdateTimers (exerciseStatus);
			}
			
			Serialize ();
			return true;
		}

		public long UpdateCurrenTicks ()
		{
			currenTicks = System.Diagnostics.Stopwatch.GetTimestamp ();
			return currenTicks;
		}
		
		public static void Wav2MP3() {
			
			ProcessStartInfo startInfo = new ProcessStartInfo ();
			startInfo.WorkingDirectory = Configuration.ConfigurationFolderPath ();
			startInfo.FileName = Configuration.CommandDirectory+"/mp3script";
			startInfo.RedirectStandardOutput = true;
			startInfo.RedirectStandardError = true;
			startInfo.UseShellExecute = false;
			Process p = Process.Start(startInfo);
			string output = p.StandardOutput.ReadToEnd();
			string outputerror = p.StandardError.ReadToEnd();
 			p.WaitForExit();
			
		}
		
		public static void CompressAndUploadProfile ()
		{
			// Disable expect 100 continue checking
			System.Net.ServicePointManager.Expect100Continue = false;
		
			// Compress WAV
			// Wav2MP3();
			
			// Values
			User auxUser = User.Deserialize ();
			string fileName = Environment.UserName + "-" + auxUser.BirthDate.DayOfYear + "-" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Hour + DateTime.Now.Minute + ".tar.gz";
			string temporalFile = Configuration.PersonalFolder + "/" + fileName;
			string dataFolder = ".pesco";
			string serverURL = Configuration.Current.ServerURL;
			string uploadFileUrl = serverURL+"upload_file.php";
			string uploadValuesUrl = serverURL+"upload_values.php";
			
			// Create process for compressing file
			ProcessStartInfo startInfo = new ProcessStartInfo ();
			startInfo.WorkingDirectory = Configuration.ConfigurationFolderPath ();
			startInfo.FileName = "tar";
			startInfo.Arguments = "-czf \"" + temporalFile + "\" . --exclude=\"*.wav\"";
			// Console.WriteLine( startInfo.FileName+" "+startInfo.Arguments );
			Process p = Process.Start (startInfo);
			// Wait until compression process finish
			p.WaitForInputIdle ();
			Thread.Sleep(2500);
			// Console.WriteLine("Fichero comprimido");
			FileInfo f = new FileInfo (temporalFile);
			// Try to upload file
			try {
				byte[] responseArray;
				WebClient Client = new WebClient ();
				// Upload to servidor113.ugr.es
				responseArray = Client.UploadFile (uploadFileUrl, temporalFile);
				// Decode and display the response, just for debug
				// Console.WriteLine("\nResponse Received.The contents of the file uploaded are:\n{0}", System.Text.Encoding.ASCII.GetString(responseArray));
				string response = System.Text.Encoding.ASCII.GetString (responseArray);
				// Console.WriteLine("Response: "+response);
				if (response == "OK") {
					// Upload values
					NameValueCollection nvc = new NameValueCollection ();
					if ( auxUser.Name != null )
						nvc.Add ("firstname", auxUser.Name);
					if ( auxUser.Firstlastname != null )
						nvc.Add ("surname", auxUser.Firstlastname);			
					nvc.Add ("filename", fileName);
					nvc.Add ("size", f.Length.ToString ());
					// Request
					Client = new WebClient ();
					responseArray = Client.UploadValues (uploadValuesUrl, nvc);
					// Console.WriteLine("\nResponse Received:\n{0}", System.Text.Encoding.ASCII.GetString(responseArray));
					// Delete temporal file
					// startInfo.FileName = "rm";
					// startInfo.Arguments = temporalFile;
					// Process.Start (startInfo);					
				} else {
					// Delete temporal file
					// startInfo.FileName = "rm";
					// startInfo.Arguments = temporalFile;
					// Process.Start (startInfo);
				}				
				
			} catch (WebException e) {
				Console.WriteLine (e.ToString ());
			}
			
		}

		private void Serialize ()
		{
			
			string fullPath = Configuration.Current.GetConfigurationFolderPath () + "/" + xmlUserFile;
			
			if (!Directory.Exists (Configuration.Current.GetConfigurationFolderPath ()))
				Directory.CreateDirectory (Configuration.Current.GetConfigurationFolderPath ());
			
			XmlTextWriter escritor = new XmlTextWriter (fullPath, null);
			
			try {
				XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces ();
				
				namespaces.Add ("", "");
				
				escritor.Formatting = Formatting.Indented;
				
				escritor.WriteStartDocument ();
				
				escritor.WriteDocType ("session-manager", null, null, null);
				
				XmlSerializer serializer = new XmlSerializer (typeof(SessionManager));
				
				serializer.Serialize (escritor, this, namespaces);
				
				escritor.WriteEndDocument ();
				escritor.Close ();
			} catch (Exception e) {
				escritor.Close ();
				Console.WriteLine ("Error al serializar" + e.Message);
			}
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
		public static SessionManager Deserialize ()
		{
			
			string fullPath = Configuration.Current.GetConfigurationFolderPath () + Path.DirectorySeparatorChar + xmlUserFile;
			Console.WriteLine (fullPath);
			
			if (!File.Exists (fullPath)) {
				string s = Environment.CommandLine;
				fullPath = Configuration.CommandDirectory + Path.DirectorySeparatorChar + "sesiones" + Path.DirectorySeparatorChar + "xml-templates" + Path.DirectorySeparatorChar + xmlUserFile;
				Console.WriteLine ("Full path: " + fullPath);
			}
			
			XmlTextReader lector = new XmlTextReader (fullPath);
			try {
				current = new SessionManager ();
				
				XmlSerializer serializer = new XmlSerializer (typeof(SessionManager));
				current = (SessionManager)serializer.Deserialize (lector);
				
				lector.Close ();
				return current;
			} catch (Exception e) {
				Console.WriteLine (e.ToString ());
				lector.Close ();
				return null;
			}
		}
		
		public TimeSpan ExerciceTime(){
			Executions[curExecInd].Exercises[curExecExerciseInd].UpdateTimers(exerciseStatus);
			return Executions[curExecInd].Exercises[curExecExerciseInd].EndTime - Executions[curExecInd].Exercises[curExecExerciseInd].StartTime;
		}
		public void RestartTimes(){
			Executions[curExecInd].Exercises[curExecExerciseInd].InitTimers();
		}
		
	}
}

