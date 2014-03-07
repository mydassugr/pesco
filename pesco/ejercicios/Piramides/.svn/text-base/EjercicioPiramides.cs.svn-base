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
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace pesco
{

	public class EjercicioPiramides : Exercise
	{
		// XML File
		public static string xmlUserFile = "pyramids-exercise.xml";		
		
		// Singleton instance
		private static EjercicioPiramides myOwnInstance = null;
		
		#region Exercise constants
		// Available images
		private const int correctFiguresNumber = 8;
		private const int distractorsFiguresNumber = 31;
		// Figures per panel
		private const int correctFiguresPerPanel = 12;
		private const int distractorsPerPanel = 28;
		// Time per panel
		public int SecondsPerPanel = 20;
		#endregion
		
		// Total score
		private int totalShown = 0;
		private int totalCorrects = 0;
		
		#region Board elements
		private PyramidButton[] vectorPyramids = new PyramidButton[40];
		private String[] vectorPiramydsID = new String[40];
		private List<int> freePositions = new List<int>(); 
		#endregion		
		
		#region	Exercise panels
		private PanelPyramidsBoard panelPyramidsBoard = new PanelPyramidsBoard();
		private PanelDemoPyramids panelDemoPyramids = new PanelDemoPyramids();
		private PanelVacio myPanel = null;
		#endregion
		
		PanelDemoPyramids demoPanel;
				
		bool auxFirstTime = true;
		
		// Level
		private int currentLevel;
		
		// List for results
		// private List <PyramidsResults> results = new List<PyramidsResults>();
		PyramidsResults results = XmlUtil.DeserializeForUser<PyramidsResults>(Configuration.Current.GetExerciseConfigurationFolderPath () + Path.DirectorySeparatorChar + "Pyramids.xml");
		
		public int CurrentLevel {
			get {
				return this.currentLevel;
			}
			set {
				currentLevel = value;
			}
		}
		
		[System.Xml.Serialization.XmlIgnoreAttribute]		
		public PyramidButton[] VectorPiramides {
			get {
				return vectorPyramids;
			}
			set {
				vectorPyramids = value;
			}
		}
		
		public PyramidsResults Results {
			get {
				return results;
			}
			set {
				results = value;
			}
		}
		
		// Funcion Singleton
		public static EjercicioPiramides getInstance() {
		
			if ( myOwnInstance != null )
				return myOwnInstance;
			else
				myOwnInstance = new EjercicioPiramides();
				return myOwnInstance;
		}		

		public EjercicioPiramides ()
		{
			category = ExerciseCategory.Attention;
		}
		
		public override void finalizar ()
		{
			if ( panelPyramidsBoard != null )
				if ( !panelPyramidsBoard.ExercisePaused() )
					panelPyramidsBoard.PauseExercise();
			
			myOwnInstance = null;
		}
		
		public override bool inicializar ()			
		{
			// Changing mode to "demo" mode
			SessionManager.GetInstance().ChangeExerciseStatus("demo");
			
			// There is just one level by now
			CurrentLevel = 1;
			
			// Init results
			ExerciceExecutionResult<SingleResultPyramids> pyramidsExecutionResult = 
					new ExerciceExecutionResult<SingleResultPyramids>(
						SessionManager.GetInstance().CurSession.IdSession, SessionManager.GetInstance().CurExecInd );
			results.PyramidsExecutionResults.Add( pyramidsExecutionResult );
			results.ResetExecution();
			
			// Creating panels
			myPanel = new PanelVacio();
			panelDemoPyramids = new PanelDemoPyramids();
			
			// Creating window
			//VentanaEjercicios venej = new VentanaEjercicios();
			//venej.auxEj = myOwnInstance;
			//venej.agregarPanelEjercicio( myPanel );
			SessionManager.GetInstance().ReplacePanel( myPanel );
			// VentanaEjercicios venej = new VentanaEjercicios();
			// venej.auxEj = myOwnInstance;
			// venej.agregarPanelEjercicio( myPanel );
			
			// Adding demo panel to empty panel						
			myPanel.Add(panelDemoPyramids);
			
			SessionManager.GetInstance().ReplacePanel( myPanel );
			// Showing window
			myPanel.ShowAll();
			// venej.ShowAll();
			myPanel.ShowAll();

			// Initializing demo panel
			panelDemoPyramids.InitPanel();
			
			return true;
		}
		
		void clickedCallback (object obj, EventArgs args)
	    {
				
				PyramidButton aux = (PyramidButton) obj;
				if ( aux.CheckedPyramid == false )
					this.panelPyramidsBoard.SetSelectedPyramids( 
				         this.panelPyramidsBoard.GetSelectedPyramids() + 1);
				else
					this.panelPyramidsBoard.SetSelectedPyramids(
				         this.panelPyramidsBoard.GetSelectedPyramids() - 1);
				aux.TogglePyramid();
	    }
		
		public override void iniciar ()
		{	
			// Changing mode to "game" mode
			SessionManager.GetInstance().ChangeExerciseStatus("game");
			
			// If this is first time, remove demo panel and add pyramids board
			if ( auxFirstTime ) {
				panelPyramidsBoard = new PanelPyramidsBoard();
				myPanel.Remove(panelDemoPyramids);
				myPanel.Add(panelPyramidsBoard);
				auxFirstTime = false;
			}
			
			// Set seconds counter
			panelPyramidsBoard.SetTimeLeft( SecondsPerPanel );
			
			GenerateBoard();
			ShowBoard();

		}
		
		
		public bool ShowBoard() {
					
			Table table = new Table(5, 8, true);
			for ( uint i = 0; i < 5; i++ ) {
				for ( uint j = 0; j < 8; j++ ) {
					vectorPyramids[i*8+j].Clicked += clickedCallback;
					table.Attach( vectorPyramids[i*8+j], j, j+1, i, i+1);				
				}	
			}

			panelPyramidsBoard.addPiramides(table);

			return false;
		}
		
		public void GenerateBoard() {
			
			// Inicializamos las posiciones libres
			freePositions.Clear();
			for (int i = 0; i < 40; i++ ) {
				vectorPiramydsID[i] = "v";
				freePositions.Add(i);
			}
			Random r = new Random(DateTime.Now.Millisecond);
			// Colocar piramides correctas
			int indiceUltimaImagen = -1;
			for ( int i = 0; i < correctFiguresPerPanel; i++ ) {				
				// Escogemos una posicion aleatoria de la lista de posiciones libres
				int posicionAleatoria = r.Next(0, freePositions.Count);
				// Escoger imagen entre las correctas				
				int indiceImagenCorrecta = r.Next(1,correctFiguresNumber);
				// Evitamos coger la misma imagen dos veces consecutivas
				if ( freePositions[posicionAleatoria] != 0 ) {
					while ( vectorPiramydsID[freePositions[posicionAleatoria]-1] == "c"+indiceImagenCorrecta ) {
						indiceImagenCorrecta = r.Next(1,correctFiguresNumber);
					}
				}
				// Añadimos boton piramide al vector botones y eliminamos posicion libre
				string imgPath = Configuration.CommandExercisesDirectory + Path.DirectorySeparatorChar + 
												"Piramides" + Path.DirectorySeparatorChar + "figuras" + Path.DirectorySeparatorChar +
												"c"+indiceImagenCorrecta+".jpg";

				Gtk.Image auxImage = new Gtk.Image( imgPath);
				vectorPyramids[freePositions[posicionAleatoria]] = new PyramidButton( auxImage, true);
				vectorPiramydsID[freePositions[posicionAleatoria]] = "c"+indiceImagenCorrecta;
				freePositions.RemoveAt(posicionAleatoria);
			}
			indiceUltimaImagen = -1;
			// Colocamos piramides distractores
			for ( int i = 0; i < distractorsPerPanel; i++ ) {
				// Escogemos una posicion aleatoria de la lista de posiciones libres
				int posicionAleatoria = r.Next(0, freePositions.Count);
				// Escoger imagen entre los distractores
				int indiceImagenDistractor = r.Next(1,distractorsFiguresNumber);
				// Evitamos coger la misma imagen dos veces consecutivas
				if ( freePositions[posicionAleatoria] != 0 ) { 
					while ( vectorPiramydsID[freePositions[posicionAleatoria]-1] == "d"+indiceImagenDistractor ) {
						indiceImagenDistractor = r.Next(1,distractorsFiguresNumber);
					}
				}
				// Añadimos boton piramide al vector botones y eliminamos posicion libre
				Gtk.Image auxImage = new Gtk.Image( Configuration.CommandExercisesDirectory + Path.DirectorySeparatorChar + 
								"Piramides" + Path.DirectorySeparatorChar + "figuras" + Path.DirectorySeparatorChar +
								"d"+indiceImagenDistractor+".jpg");
				vectorPyramids[freePositions[posicionAleatoria]] = new PyramidButton( auxImage, false);
				vectorPiramydsID[freePositions[posicionAleatoria]] = "d"+indiceImagenDistractor;
				freePositions.RemoveAt(posicionAleatoria);
			}		
		
		}
		
		public override void pausa() {
			if ( panelPyramidsBoard != null ) {
				panelPyramidsBoard.PauseExercise();
			}
		}
		
		public override int idEjercicio ()
		{
			return 6;
		}
		
		public override string NombreEjercicio ()
		{			
			return "Piŕamides";
		}
		
		public void RepetionFinished() {

			// Save board results
			SingleResultPyramids pyramidsResults = SaveBoardResults();

			// Save total scores
			totalShown += correctFiguresPerPanel;
			totalCorrects += pyramidsResults.Corrects - pyramidsResults.Fails 
				- (pyramidsResults.Omissions/2);
			
			results.CurrentExecution.TotalCorrects += pyramidsResults.Corrects;
			results.CurrentExecution.TotalFails += pyramidsResults.Fails;
			results.CurrentExecution.TotalOmissions += pyramidsResults.Omissions;
			Serialize();
			
			// Notifying session manager
			SessionManager.GetInstance().RepetitionFinished();
			
			// Do I have to finish?
			if ( SessionManager.GetInstance().HaveToFinishCurrentExercise() ) {
				// Calculate final score for medal
				double score = (double) totalCorrects / (double) totalShown;
				score = score * 100;
				if ( score < 0 )
					score = 0;
				// Call session manager
				SessionManager.GetInstance().ExerciseFinished( (int) score);
				// Finish exercise and return control
				finalizar();
				SessionManager.GetInstance().TakeControl();
			} else {
				// Start a new sequence
				EjercicioPiramides ejP = EjercicioPiramides.getInstance();
				ejP.iniciar();
			}
		}
		
		public SingleResultPyramids SaveBoardResults() {
			
			SingleResultPyramids pyramidsResults = new SingleResultPyramids();
					
			for ( int i = 0; i < VectorPiramides.Length; i++ ) {
				if ( VectorPiramides[i].CheckedPyramid == true && VectorPiramides[i].CorrectPyramid == true )
					pyramidsResults.Corrects = pyramidsResults.Corrects + 1;
				else if ( VectorPiramides[i].CheckedPyramid == true && VectorPiramides[i].CorrectPyramid == false )
					pyramidsResults.Fails = pyramidsResults.Fails + 1;
			}
			
			pyramidsResults.Omissions = 12 - pyramidsResults.Corrects ;
			
			results.setResult( pyramidsResults );

			Serialize();
			
			return pyramidsResults;
		}

		
		public void Serialize(){

			XmlUtil.SerializeForUser<PyramidsResults>( results, Configuration.Current.GetExerciseConfigurationFolderPath () + Path.DirectorySeparatorChar + "Pyramids.xml" );
			string fullPath = Configuration.Current.GetExerciseConfigurationFolderPath()+ "/" + xmlUserFile;
			
			XmlTextWriter escritor = new XmlTextWriter(fullPath, null);
		
			try
			{
				escritor.Formatting = Formatting.Indented;
				
				escritor.WriteStartDocument();
				
				escritor.WriteDocType("pyramids-exercise", null, null, null);
						
				XmlSerializer serializer = new XmlSerializer(typeof(EjercicioPiramides));
				
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
			}
	}	
	
	public static EjercicioPiramides Deserialize()
	{
		string fullPath = Configuration.Current.GetExerciseConfigurationFolderPath() + "/" + xmlUserFile;
		
		if (!File.Exists(fullPath))
		{
			string s = Environment.CommandLine;			
			fullPath = Configuration.CommandDirectory + "/ejercicios/Piramides/xml-templates/" + xmlUserFile;
		}
		
		XmlTextReader lector = new XmlTextReader(fullPath);

		EjercicioPiramides exercise = new EjercicioPiramides();
		
		XmlSerializer serializer = new XmlSerializer(typeof(EjercicioPiramides));				
		exercise = (EjercicioPiramides) serializer.Deserialize(lector);
		
		lector.Close();				
		
		myOwnInstance = exercise;
		
		return (EjercicioPiramides) exercise;
	}
			
	}
}

