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
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace pesco
{


	public class ExerciseBagItems : Exercise
	{
		// XML FIle
		private static string xmlUserFile = "bagitems-exercise.xml";
		
		// Results
		// private List <BO_Results> results = new List<BO_Results>();
				
		BagItemsResults results = XmlUtil.DeserializeForUser<BagItemsResults>(Configuration.Current.GetExerciseConfigurationFolderPath () + Path.DirectorySeparatorChar + "BagItems.xml");
		// Singleton pattern
		private static ExerciseBagItems myOwnInstance = null;
		private PanelBagItems panelBolsaObjetos = null;
		private PanelVacio myPanel = null;
		
		#region Exercise constants
		[XmlIgnoreAttribute]
		public int LUGARESMAPA = 8;
		[XmlIgnoreAttribute]
		public int[] placesByLevel = new int[4] {2, 4, 6, 8};
		[XmlIgnoreAttribute]
		public int[] itemsByLevel = new int[3] {6, 12, 15};
		#endregion
		
		#region Positions constants
		[XmlIgnore]
		public BO_Position[] posicionLugares = new BO_Position[8];
		[XmlIgnore]
		public BO_Position[] posicionTextosDialogos = new BO_Position[3];
		#endregion		
		
		#region Variables
		// Random generator
		private Random r;
		
		// List for places, items, actions...
		[XmlIgnore]
		public List <BO_Place> allPlaces = new List<BO_Place>();
		[XmlIgnore]
		public List <BO_Item> itemsAvalaibleAtHome = new List <BO_Item>();		
		[XmlIgnore]
		public Hashtable objetos = new Hashtable();
		[XmlIgnore]		
		public Hashtable acciones = new Hashtable();
		[XmlIgnore]
		public Hashtable quienes = new Hashtable();
		
		// Scenario
		private BO_Scene currentScene;
		[XmlIgnore]		
		public BO_Scene CurrentScene {
			get {
				return this.currentScene;
			}
			set {
				currentScene = value;
			}
		}

		// Situations
		[XmlIgnore]
		public Stack <BO_SituationQuantity> situationsQueue = new Stack<BO_SituationQuantity>();
		
		// Variables
		private int placesVisited = 0;
		private int currentLevel = 1;
		
		// Score
		private int globalScore = 0;
		private int globalRepetitions = 0;
		private int globalRepetitionsInLevel = 0;
		
		// Timers
		private bool pausedExercise = false;
		
		
		public int CurrentLevel {
			get {
				return results.CurrentLevel;
			}
			set {
				results.CurrentLevel = value;
			}
		}

		public BagItemsResults Results {
			get {
				return results;
			}
			set {
				results = value;
			}
		}
			
		#endregion
		[XmlIgnore]
		public PanelBagItems PanelBolsaObjetos {
			get {
				return panelBolsaObjetos;
			}
			set {
				panelBolsaObjetos = value;
			}
		}
		
		// Funcion Singleton
		public static ExerciseBagItems getInstance() {
		
			if ( myOwnInstance != null )
				return myOwnInstance;
			else
				myOwnInstance = new ExerciseBagItems();
				return myOwnInstance;
		}

		public ExerciseBagItems () 
		{
			category = ExerciseCategory.Memory;
		}
	
		public override void finalizar ()
		{
			if ( !pausedExercise )
				pausa();
			
			myOwnInstance = null;
		}
		
		public void Serialize() {
			
			XmlUtil.SerializeForUser<BagItemsResults>( results, Configuration.Current.GetExerciseConfigurationFolderPath () + Path.DirectorySeparatorChar + "BagItems.xml" );
			// XmlUtil.SerializeForUser<ExerciseBagItems>( this, Configuration.Current.GetExerciseConfigurationFolderPath()+ "/" +xmlUserFile);
			
		}

		public override bool inicializar ()
		{
			myPanel = new PanelVacio();
			
			SessionManager.GetInstance().ReplacePanel(myPanel);
			
			// Initializing places positions
			posicionLugares[0] = new BO_Position(220,160);
			posicionLugares[1] = new BO_Position(410,160);
			posicionLugares[2] = new BO_Position(600,160);
			posicionLugares[3] = new BO_Position(790,160);
			posicionLugares[4] = new BO_Position(820,390);
			posicionLugares[5] = new BO_Position(630,390);
			posicionLugares[6] = new BO_Position(440,390);
			posicionLugares[7] = new BO_Position(250,390);
			
			// Initializing dialogs positions
			posicionTextosDialogos[0] = new BO_Position(250, 80);
			posicionTextosDialogos[1] = new BO_Position(250, 220);
			posicionTextosDialogos[2] = new BO_Position(250, 380);
			
			r = new Random(DateTime.Now.Millisecond);
			
			if ( panelBolsaObjetos != null ) {
				myPanel.Remove( panelBolsaObjetos );
				panelBolsaObjetos.Destroy();
			}
			
			panelBolsaObjetos = new PanelBagItems();
			
			// Loading resources
			cargarQuienes();
			cargarObjetos();
			cargarAcciones();
			cargarLugares();
				
			// Init demo
			CreateScene( 0 );
			// currentScene.Print();
			SessionManager.GetInstance().ChangeExerciseStatus("demo");
			myPanel.Add(panelBolsaObjetos);
			myPanel.ShowAll();
			panelBolsaObjetos.initPanel();
			
			// This exercise levels go from 1 to 3
			if ( results.CurrentLevel == 0 ) {
				results.CurrentLevel = 1;
			}
			
			// If level in last session was 3, we'll start in 2
			if ( CurrentLevel > 3 )
				CurrentLevel = 3;
						
			iniciar();
			
			return true;
		}		
				
		public override void iniciar ()
		{
			panelBolsaObjetos.InitDemo();
		}
		
		public void CreateScene( int level ) {
			
			currentScene = new BO_Scene( this );
			
			// Init scene with avalaible places
			currentScene.InitScene( allPlaces );
			currentScene.CreateScene( level );
						
		}	
	
		public void ResetExercise() {

			// Create scene
			CreateScene( CurrentLevel );
			
			// Situations
			situationsQueue = new Stack<BO_SituationQuantity>();

			// Variables
			placesVisited = 0;
			
		}
		
		public override int idEjercicio ()
		{
			return 5;
		}

		public override void pausa ()
		{
			
		}
				
		public void cargarLugares () {
			
			// Load places
			XmlReader textReader = XmlUtil.GetResource("pesco.ejercicios.BagItems.xml-templates.lugares.xml");  
				
			BO_Place auxPlace = new BO_Place();
			allPlaces = new List<BO_Place>();
			// Iterating over places
            while (textReader.Read()) {
				textReader.MoveToElement();
	            if ( textReader.Name == "lugar" && textReader.AttributeCount >= 3 ) {					 
					string atrId = XmlUtil.getAtributo(textReader,0);
					string atrNombre = XmlUtil.getAtributo(textReader,1);
					string atrImagen = XmlUtil.getAtributo(textReader,2);
					auxPlace = new BO_Place(atrId, atrNombre, atrImagen);
					if ( atrId != "casa" ) {
						allPlaces.Add( auxPlace );
					}
				} else if ( textReader.Name == "objeto" && textReader.AttributeCount >= 3 ) {
					string accionId = XmlUtil.getAtributo(textReader,0);
					string objetoId = XmlUtil.getAtributo(textReader,1);
					string quienId = XmlUtil.getAtributo(textReader,2);
					
					if (auxPlace.Id != "casa" ) {						
						allPlaces[allPlaces.Count-1].agregarAccion( 
					 		new BO_ItemActionWho( (BO_Item) objetos[objetoId], 
					                              (BO_Action) acciones[accionId], 
					                              (BO_Who) quienes[quienId]) );
					} else {
						itemsAvalaibleAtHome.Add( (BO_Item) objetos[objetoId] );	
					}
				}
        	}
			
		}
			
		public void cargarObjetos () {
		
			// Load items
			XmlReader textReader = XmlUtil.GetResource("pesco.ejercicios.BagItems.xml-templates.objetos.xml");  
			
			// Iterating over items
			while (textReader.Read()) {
				textReader.MoveToElement();
				if ( textReader.Name == "objeto" && textReader.AttributeCount >= 5 ) {
					string atrId = XmlUtil.getAtributo(textReader,0);
					string atrGenero = XmlUtil.getAtributo(textReader,1);
					string atrNombreSingular = XmlUtil.getAtributo(textReader,2);
					string atrNombrePlural = XmlUtil.getAtributo(textReader,3);
					string atrNombreSimple = XmlUtil.getAtributo(textReader,4);
					string atrImg = XmlUtil.getAtributo(textReader,5);
					objetos.Add( atrId, new BO_Item(atrId, atrGenero, atrNombreSingular, 
					                                  atrNombrePlural, atrNombreSimple, atrImg) );
				}
			}
		}
		
		public void cargarAcciones () {
		
			// Load actions
			XmlReader textReader = XmlUtil.GetResource("pesco.ejercicios.BagItems.xml-templates.acciones.xml");  
			
			// Iterating over actions
			while (textReader.Read()) {
				textReader.MoveToElement();
				if ( textReader.Name == "accion" && textReader.AttributeCount >= 3 ) {
					string atrId = XmlUtil.getAtributo(textReader,0);
					string atrTexto = XmlUtil.getAtributo(textReader,1);
					string atrTipo = XmlUtil.getAtributo(textReader,2);
					acciones.Add( atrId, new BO_Action(atrId, atrTexto, atrTipo) );
				}
			}
		}
		
		public void cargarQuienes () {
		
			// Load whos
			XmlReader textReader = XmlUtil.GetResource("pesco.ejercicios.BagItems.xml-templates.quienes.xml");  
			
			// Iterating over whos
			while (textReader.Read()) {
				textReader.MoveToElement();
				if ( textReader.Name == "quien" && textReader.AttributeCount >= 2 ) {
					string atrId = XmlUtil.getAtributo(textReader,0);
					string atrTexto = XmlUtil.getAtributo(textReader,1);
					quienes.Add( atrId, new BO_Who(atrId, atrTexto) );
				}
			}
		}
			
		public bool haveToStopAtPlace( int place ) {
				
			if ( currentScene.SituationsInPlaces.ContainsKey(place) ) {
				if ( currentScene.SituationsInPlaces[place].Count > 0 ) {
					return true;	
				}				
			}
			return false;
		}
				
		public BO_SituationQuantity getCurrentSituation() {
			if ( situationsQueue.Count > 0 )
				return situationsQueue.Peek();
			else
				return null;
		}
		
		public void finishCurrentSituation() {
			situationsQueue.Pop();		
		}

		public override string NombreEjercicio ()
		{
			return "Bolsa de objetos";
		}
		
		
		// TODO: Change all these things to new format
		public SingleResultBagItems GetResults() {
		
			// Creating auxiliar Lists for the BO_Results class
			Dictionary<BO_Item, int> auxSelected = panelBolsaObjetos.itemsToChooseInBagPositionsSelected;
			List <string> auxCorrectsItems = new List<string>();			
			List <string> auxItemsInBag = new List<string>();			
			int auxCorrects = 0;
			int auxSemiCorrects = 0;
			int auxFails = 0;
			int auxOmissions = 0;
			double auxScore = 0;
			
			// Save correct items			
			foreach( KeyValuePair<BO_Item, int> kvp in CurrentScene.ItemsInBag )
    		{
        		auxItemsInBag.Add ( kvp.Key.NombreSimple+":"+kvp.Value );
				// If the user omitted an item
				if ( auxSelected[kvp.Key] == 0 && kvp.Value != 0 ) {
					auxOmissions++;
				}
    		}
			// Save selected items
			// Checking selected items
  			foreach( KeyValuePair<BO_Item, int> kvp in auxSelected )
    		{
				if ( kvp.Value > 0 ) {
        			auxCorrectsItems.Add( kvp.Key.NombreSimple +":"+kvp.Value );
				}
				if ( CurrentScene.ItemsInBag.ContainsKey( kvp.Key ) && kvp.Value > 0 ) {
					if ( CurrentScene.ItemsInBag[kvp.Key] == kvp.Value ) {
						// If the user guessed item and quantity
						auxCorrects++;
						auxScore = auxScore + 1;
					} else {
						// If the user guessed just item
						auxSemiCorrects++;
						auxScore = auxScore + 0.5;
					}
				} else if ( !(CurrentScene.ItemsInBag.ContainsKey( kvp.Key )) && kvp.Value > 0 ) {
					// We checked an item that is not in the bag
					auxFails++;
					auxScore = auxScore - 1;
				}
    		}
			auxScore = auxScore - auxOmissions;
			// Normalizing score
			if ( auxScore <= 0 ) {
				auxScore = 0;	
			} else {
				auxScore = ( auxScore / ( auxCorrects + auxSemiCorrects + auxFails ) ) * 100;
			}
			// Update global scores
			globalScore += (int) auxScore;
			globalRepetitions++;
			globalRepetitionsInLevel++;
			// Check if we have to gain or to lose a level
			// To increase level we have to play at least two times in the same level
			if ( globalRepetitionsInLevel >= 2 ) {
				// We'll increase level if average score is > 80 in level
				if ( globalScore/globalRepetitionsInLevel > 80 ) {
					if ( CurrentLevel < 2 ) {
						CurrentLevel++;
					}
					globalRepetitionsInLevel = 0;
				}
				// If average score is < 50 after three times 
				else if ( globalScore/globalRepetitionsInLevel < 50 && globalRepetitionsInLevel > 2 && CurrentLevel > 1 ) {
					if ( CurrentLevel > 0 ) {
						CurrentLevel--;
					}
					globalRepetitionsInLevel = 0;
				}
			}
		
			return new SingleResultBagItems( auxItemsInBag, auxCorrectsItems, auxCorrects, auxSemiCorrects, auxOmissions, auxFails, (int) auxScore );

		}
		
		private int SaveResults () {
			
			SingleResultBagItems auxResults = GetResults();
			Results.setResult( auxResults );
			// Results.Add( auxResults );
			Serialize();
			return (int) auxResults.Score;
		}
		
		public void FinishRepetition() {

			// Saving results of current repetition
			int auxScore = SaveResults();
			
			// Noticing session manager
			SessionManager.GetInstance().RepetitionFinished();
			
			if ( SessionManager.GetInstance().HaveToFinishCurrentExercise() ) {				
				SessionManager.GetInstance().ExerciseFinished( auxScore );
				SessionManager.GetInstance().TakeControl();
				finalizar();
			} else {
				panelBolsaObjetos.Pause();
				panelBolsaObjetos.Destroy();
				PodiumPanel auxPodium = new PodiumPanel( auxScore );
				auxPodium.BalloonText = "¡Enhorabuena! Has completado el recorrido por la ciudad y has obtenido una medalla de...";
				auxPodium.ButtonOK.Label = "Volver a la ciudad para hacer otro recorrido";
				GtkUtil.SetStyle( auxPodium.ButtonOK, Configuration.Current.MediumFont );
				auxPodium.ButtonOK.Clicked += delegate {
					//  Remove Podium
					myPanel.Remove(auxPodium);
					auxPodium.Destroy();
					
					// Create new Bag Items panel
					panelBolsaObjetos = new PanelBagItems();
					myPanel.Add(panelBolsaObjetos);
					panelBolsaObjetos.ShowAll();
					myPanel.ShowAll();					
					
					// Init panel
					panelBolsaObjetos.initPanel();
					
					// Reset exercise and create new one
					ResetExercise();
					panelBolsaObjetos.InitGame();
					
				};
				myPanel.Remove(panelBolsaObjetos);
				// panelBolsaObjetos.Destroy();
				myPanel.Add(auxPodium);				
				auxPodium.ShowAll();
				myPanel.ShowAll();
			}

		}
		
		public static ExerciseBagItems Deserialize()
		{
			string fullPath = Configuration.Current.GetExerciseConfigurationFolderPath() + "/" + xmlUserFile;
			
			if (!File.Exists(fullPath))
			{
				string s = Environment.CommandLine;			
				fullPath = Configuration.CommandDirectory + "/ejercicios/BagItems/xml-templates/" + xmlUserFile;
				Console.WriteLine("Full path: " + fullPath);
			}
			
			XmlTextReader lector = new XmlTextReader(fullPath);
			try
			{	
				ExerciseBagItems exercise = new ExerciseBagItems();
				
				XmlSerializer serializer = new XmlSerializer(typeof(ExerciseBagItems));				
				exercise = (ExerciseBagItems) serializer.Deserialize(lector);
				
				myOwnInstance = exercise;
				
				// Console.WriteLine( exercise.CurrentLevel );
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

