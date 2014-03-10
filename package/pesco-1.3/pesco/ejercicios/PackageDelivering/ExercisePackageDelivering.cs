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
using System;
using System.Collections.Generic;

namespace pesco
{


	public class ExercisePackageDelivering : Exercise
	{
		
		private static ExercisePackageDelivering myOwnInstance;
		
		public static string xmlUserFile = "package-delivering.xml";
		private PanelVacio myPanel = null;
		
		private PanelPackageDelivering currentPanelPD = null;

		private PD_SceneManager sceneManager;
		private PD_PlacesManager placesManager;
		// private PD_ResultsManager resultsManager;
		
		private int currentScenarioId;

		// Results
		// private PD_Results currentResults;
		PackageDeliveringResults results = XmlUtil.DeserializeForUser<PackageDeliveringResults>(Configuration.Current.GetExerciseConfigurationFolderPath () + Path.DirectorySeparatorChar + "PackageDelivering.xml");
		
		[XmlIgnoreAttribute]
		public PackageDeliveringResults Results {
			get {
				return this.results;
			}
			set {
				results = value;
			}
		}

		[XmlIgnoreAttribute]
		public PD_SceneManager SceneManager {
			get {
				return sceneManager;
			}
			set {
				sceneManager = value;
			}
		}
		
		[XmlIgnoreAttribute]
		public PD_PlacesManager PlacesManager {
			get {
				return placesManager;
			}
			set {
				placesManager = value;
			}
		}
		
		/*[XmlIgnore]
		public PD_ResultsManager ResultsManager {
			get {
				return resultsManager;
			}
			set {
				resultsManager = value;
			}
		}*/
		
		// Function Singleton
		public static ExercisePackageDelivering getInstance() {
		
			if ( myOwnInstance != null )
				return myOwnInstance;
			else
			{
				myOwnInstance = new ExercisePackageDelivering();
				return myOwnInstance;
			}
		}
		
		public ExercisePackageDelivering ()
		{
			category = ExerciseCategory.Planification;
		}

		public override void finalizar ()
		{			
			myOwnInstance = null;
		}
				
		public override int idEjercicio ()
		{
			return 17;
		}
		
		public override bool inicializar ()
		{
			// Deserialize managers
			sceneManager = XmlUtil.Deserialize<PD_SceneManager>("/ejercicios/PackageDelivering/xml-templates/pd-scenes.xml");
			placesManager = XmlUtil.Deserialize<PD_PlacesManager>("/ejercicios/PackageDelivering/xml-templates/pd-places.xml");
			// resultsManager = XmlUtil.DeserializeForUser<PD_ResultsManager>(Configuration.Current.GetExerciseConfigurationFolderPath()+ "/" + xmlUserFile);
			
			// Init results
			ExerciceExecutionResult<SingleResultPackageDelivering> packageDeliveringShoppingExecutionResult = 
					new ExerciceExecutionResult<SingleResultPackageDelivering>(
						SessionManager.GetInstance().CurSession.IdSession, SessionManager.GetInstance().CurExecInd );
			Results.PackageDeliveringExecutionResults.Add( packageDeliveringShoppingExecutionResult );
			
			// Set category and exercise Id
			if ( Results.CategoryId == 0 || Results.ExerciseId == 0 ) {
				Results.CategoryId = Convert.ToInt16(ExerciseCategory.Planification);
				Results.ExerciseId = this.idEjercicio();				
			}
			
			// Load places pixbuf
			for ( int i = 0; i < placesManager.Places.Count; i++ ) {
				placesManager.Places[i].LoadPixbuf();	
			}
			
			// Creating main panel and replacing in session manager
			myPanel = new PanelVacio();			
			currentPanelPD = new PanelPackageDelivering(); 			
			myPanel.Add( currentPanelPD );	
			SessionManager.GetInstance().ReplacePanel(myPanel);
			myPanel.ShowAll();
			// We have two scenes for this exercise
			// In session < 6 (screening pre) we load scene 1
			// In session > 6 (screening post) we load scene 2
			if ( SessionManager.GetInstance().CurSession.IdSession < 6 ) {
				currentPanelPD.InitPanel( 0 );
			} else {
				currentPanelPD.InitPanel( 1 );
			}
			currentPanelPD.InitDemo();
			
			return true;
		}
		
		public override void iniciar ()
		{
			// currentPanelPD.ShowAll();
		}
		
		public void InitResults() {
		
			// resultsManager.NewResults(currentScenarioId);
			Results.NewResults(currentScenarioId);
		}
		
		public override string NombreEjercicio ()
		{
			return "Repartidor de paquetes";
		}
		
		public override void pausa ()
		{
			if ( currentPanelPD != null ) {
				currentPanelPD.Pause();	
			}
		}

		// Serialize exercise
		public void Serialize() {		
			XmlUtil.SerializeForUser<PackageDeliveringResults>( results, Configuration.Current.GetExerciseConfigurationFolderPath () + Path.DirectorySeparatorChar + "PackageDelivering.xml" );
			// XmlUtil.SerializeForUser<ExercisePackageDelivering>( this, Configuration.Current.GetExerciseConfigurationFolderPath()+ "/" +xmlUserFile);
		}
		
		// Deserialize exercise
		public static ExercisePackageDelivering Deserialize()
		{
			// XML In this exercise has not anything to modify, so we'll get if from Command folder always
			// string fullPath = Configuration.Current.GetExerciseConfigurationFolderPath() + "/" + xmlUserFile;
			string fullPath = Configuration.CommandDirectory + "/ejercicios/PackageDelivering/xml-templates/" + xmlUserFile;
			
			if (!File.Exists(fullPath))
			{
				string s = Environment.CommandLine;			
				fullPath = Configuration.CommandDirectory + "/ejercicios/PackageDelivering/xml-templates/" + xmlUserFile;
				Console.WriteLine("Full path: " + fullPath);
			}
			
			XmlTextReader lector = new XmlTextReader(fullPath);
			try
			{	
				ExercisePackageDelivering exercise = new ExercisePackageDelivering();
				
				XmlSerializer serializer = new XmlSerializer(typeof(ExercisePackageDelivering));				
				exercise = (ExercisePackageDelivering) serializer.Deserialize(lector);
				
				myOwnInstance = exercise;
				
				lector.Close();				
				return exercise;
			}
			catch( Exception e)
			{
				Console.WriteLine( e.ToString() );
				lector.Close();
				return null;
			}
		}				
				
	}
}

