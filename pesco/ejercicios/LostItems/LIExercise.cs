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
	public class LIExercise : Exercise
	{
		
		private static string xmlUserFile = "lostitems-exercise.xml";
				
		#region Singleton instance
		private static LIExercise myOwnInstance = null;
		#endregion
		
		#region Exercise constants
		private static int [] roomsPerLevel = { 2, 4, 6, 8 };
		private static int [] itemsPerSubLevel = { 1, 2, 4 };
		private enum GameOptions { Room, RoomWithPickedItem, Hall };
		private static int maxPickedItems = 8;
		private const int WIDTH_SCREEN = 1100;
		private const int HEIGHT_SCREEN = 600;
		
		#endregion
		
		#region Exercise managers
		[XmlIgnoreAttribute]
		private LI_ItemsManager itemsManager;
		[XmlIgnoreAttribute]
		private LI_RoomsManager roomsManager;
		[XmlIgnoreAttribute]
		private LI_PositionsManager positionsManager;
		// [XmlIgnoreAttribute]
		// private LI_ResultsManager resultsManager;
			
		// Results
		LostItemsResults results = XmlUtil.DeserializeForUser<LostItemsResults>(Configuration.Current.GetExerciseConfigurationFolderPath () + Path.DirectorySeparatorChar + "LostItems.xml");
		
		[XmlIgnoreAttribute]
		public LI_ItemsManager ItemsManager {
			get {
				return this.itemsManager;
			}
			set {
				itemsManager = value;
			}
		}
		[XmlIgnoreAttribute]
		public LI_PositionsManager PositionsManager {
			get {
				return this.positionsManager;
			}
			set {
				positionsManager = value;
			}
		}
		[XmlIgnoreAttribute]
		public LI_RoomsManager RoomsManager {
			get {
				return this.roomsManager;
			}
			set {
				roomsManager = value;
			}
		}
		
		#endregion
		
		#region Exercise panels
		private LI_ScenePanel liPanel = null;
		private LI_HallPanel liHallPanel = null;
		// private List<LI_RoomPanel> liRoomPanels = new List<LI_RoomPanel>();
		private LI_RoomPanel currentShownRoom = null;
		private PanelVacio myPanel = null;
		private Gdk.Pixmap auxPixmap;
		#endregion
		
		#region Exercise variables
		private string currentMode = "game";
		private GameOptions currentOption = GameOptions.Room;
		private bool somethingChanged = true;
		private LI_Scene currentScene;
		private int currentRoom;
		private int itemsNotInRoom;
		private int coinsFound;
		private SingleResultLostItems currentResult;		
		private List<LI_RoomInGame> roomsInGame = null;
		private List<LI_ItemInGame> itemsInGame = null;
		private List<LI_Item> invalidItemsInGame = null;
		private List<LI_Item> pickedItems = null;
		private int currentPickedItemIndex = -1;
		private bool debugMode = false;
		private Gdk.Pixbuf coinPixbuf = Gdk.Pixbuf.LoadFromResource( "pesco.ejercicios.LostItems.img.coin.png" );
		private Gdk.Pixbuf alphaPixbuf = new Gdk.Pixbuf( Configuration.CommandExercisesDirectory+"/LostItems/img/alphared.png" );
		
		[XmlIgnoreAttribute]
		public bool DebugMode {
			get {
				return this.debugMode;
			}
			set {
				debugMode = value;
			}
		}

		public int CurrentLevel {
			get {
				return results.CurrentLevel;
			}
			set {
				results.CurrentLevel = value;
			}
		}
		public int CurrentSubLevel {
			get {
				return results.CurrentSubLevel;
			}
			set {
				results.CurrentSubLevel = value;
			}
		}
		[XmlIgnoreAttribute]
		public int CurrentRoom {
			get {
				return this.currentRoom;
			}
			set {
				currentRoom = value;
			}
		}
		/*
		public LI_ResultsManager ResultsManager {
			get {
				return this.resultsManager;
			}
			set {
				resultsManager = value;
			}
		}*/		
		#endregion
		
		#region Timers
		private int auxAnimationTimer;
		private int auxAnimationTimer2;
		private uint auxTimer;
		private GLib.TimeoutHandler currentHandler;
		private uint currentInterval;
		private bool pausedExercise;
		private int REPAINT_SPEED = 25;
		int pauseTimer = 100;
		#endregion				
		
		#region Graphics elements
		Gdk.GC auxGC;
		Pango.Layout auxLayout;
		#endregion
		
		#region Demo
		private Gdk.Pixbuf bgPixbuf;
		private Gdk.Pixbuf backgroundPixbuf;
		private Gdk.Pixbuf dialogPixbuf;
		private Gdk.Pixbuf pepe1Pixbuf;
		private Gdk.Pixbuf pepe2Pixbuf;
		private Gdk.Pixbuf bigVanPixbuf;
		int pepeStatus = 0;
		Gdk.Pixbuf auxPixbuf1;
		Gdk.Pixbuf auxPixbuf2;
		
		// Animations
		private string stringToSay;
		private int currentStep = 0;
		private int currentCharacter = 0;
		private int totalCharacters = 0;

		private bool firstFrameStep = true;
		private bool lastFrameStep = false;
		private bool demoLearnedOk = false;

		private	string [] animationsText = {"¡Bienvenido al ejercicio de objetos desordenados! En este ejercicio quiero que me ayudes a ordenar una serie de objetos que están repartidos por las habitaciones de una casa. Pulsa <span color=\"black\">Siguiente</span>.", 
							/*1*/	"Tendrás que entrar en cada una de las habitaciones de la casa y mirar por todas partes para ver si algún objeto de la habitación no pertenece a esa habitación. Pulsa <span color=\"black\">Siguiente</span>.",
							/*2*/	"Para entrar en las habitaciones tendrás que pulsar en las puertas. Por ejemplo, ahora quiero que entres en <span color=\"black\">{0}</span>, pulsando en la puerta de la derecha.",
							/*3*/	"Ya estás en la habitación. Ahora pulsa <span color=\"black\">Siguiente</span> y luego busca algún objeto que no sea de la habitación, como <span color=\"black\">{0}</span>. Cuando lo encuentres pulsa sobre él.",
							/*4*/	"",
							/*5*/	"Ahora tienes el objeto y aparece arriba a la izquierda. Pulsa <span color=\"black\">Siguiente</span>.",
							/*6*/	"Para salir de una habitación tienes que pulsar el botón de <span color=\"black\">Ir al pasillo</span> que está en la esquina superior derecha. Púlsalo ahora.",
							/*7*/	"Cuando hayas encontrado un objeto tendrás que colocarlo en la habitación correcta. Ahora colocarás <span color=\"black\">{0}</span> en <span color=\"black\">{1}</span>. Pulsa la puerta de la izquierda.",
							/*8*/	"Para colocar el objeto, primero tienes que seleccionarlo. Arriba a la izquierda puedes ver {0}. Pulsa ahora sobre el objeto.",
							/*9*/	"Ya tienes {0} seleccionado. Su sitio correcto estará marcado con un recuadro rojo en la habitación. Ahora pulsa <span color=\"black\">Siguiente</span> y luego busca el recuadro rojo para colocar el objeto.",
							/*10*/	"",
							/*11*/	"Ahora el objeto está en su sitio. Para completar el ejercicio tendrás que ordenar los objetos y recoger las monedas que encuentres en las habitaciones. Pulsa <span color=\"black\">Siguiente</span>.",			
							/*12*/	"He escondido una moneda en la habitación. Ahora quiero que pulses <span color=\"black\">Siguiente</span>, busques la moneda y pulses sobre ella.",
							/*13*/	"",
							/*14*/	"Ahora comenzaremos el ejercicio. Recuerda que tienes que encontrar y colocar todos los objetos desordenados y que también tienes que buscar las monedas. Pulsa <span color=\"black\">Siguiente</span>."};
			
		
		#endregion
		public static LIExercise GetInstance() {		
			return myOwnInstance;		
		}
		
		public LIExercise ()
		{
			category = ExerciseCategory.Attention;
		}
		
		public override bool inicializar ()
		{	
			// Exercise starts in demo mode. Notifying to session manager
			SessionManager.GetInstance().ChangeExerciseStatus("demo");
			currentMode = "demo";
			
			currentRoom = 0;
			
			// Init results
			ExerciceExecutionResult<SingleResultLostItems> lostItemsExecutionResult = 
					new ExerciceExecutionResult<SingleResultLostItems>(
						SessionManager.GetInstance().CurSession.IdSession, SessionManager.GetInstance().CurExecInd );
			results.LostItemsExecutionResults.Add( lostItemsExecutionResult );
			results.ResetExecution();
			// Set category and exercise id
			if ( results.CategoryId == 0 || results.ExerciseId == 0 ) {
				results.CategoryId = Convert.ToInt16(ExerciseCategory.Attention);
				results.ExerciseId = this.idEjercicio();
			}
			
			// Init panels
			myPanel = new PanelVacio();
			liPanel = new LI_ScenePanel(this);
			
			myPanel.Add(liPanel);
			
			SessionManager.GetInstance().ReplacePanel(myPanel);
			myPanel.ShowAll();
	
			// liRoomPanels.Add( new LI_RoomPanel() );
			// liPanel.ReplaceRoom( liRoomPanels[0] );
			
			// currentShownRoom = liRoomPanels[0];
			// currentShownRoom.ShowAll();
			
			// itemsManager = new LI_ItemsManager();
			// roomsManager = new LI_RoomsManager();
			// positionsManager = new LI_PositionsManager();
			
			positionsManager = XmlUtil.Deserialize<LI_PositionsManager>( "/ejercicios/LostItems/xml-templates/li-positions.xml");
			itemsManager = XmlUtil.Deserialize<LI_ItemsManager>( "/ejercicios/LostItems/xml-templates/li-items.xml" );
			roomsManager = XmlUtil.Deserialize<LI_RoomsManager>( "/ejercicios/LostItems/xml-templates/li-rooms.xml" );						
			
			return true;
			
		}
		
		#region XML Functions
		// Serialize exercise
		public void Serialize() {
			XmlUtil.SerializeForUser<LostItemsResults>( results, Configuration.Current.GetExerciseConfigurationFolderPath () + Path.DirectorySeparatorChar + "LostItems.xml" );
			XmlUtil.SerializeForUser<LIExercise>( this, Configuration.Current.GetExerciseConfigurationFolderPath()+ "/" +xmlUserFile);
		}
		
		public static LIExercise Deserialize()
		{
			string fullPath = Configuration.Current.GetExerciseConfigurationFolderPath() + "/" + xmlUserFile;
			
			if (!File.Exists(fullPath))
			{
				string s = Environment.CommandLine;			
				fullPath = Configuration.CommandDirectory + "/ejercicios/LostItems/xml-templates/" + xmlUserFile;
				// Console.WriteLine("Full path: " + fullPath);
			}
			
			XmlTextReader lector = new XmlTextReader(fullPath);
			try
			{	
				LIExercise exercise = new LIExercise();
				
				XmlSerializer serializer = new XmlSerializer(typeof(LIExercise));				
				exercise = (LIExercise) serializer.Deserialize(lector);
				
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
		#endregion
		public override void finalizar ()
		{
			if ( auxTimer != null ) {
				GLib.Source.Remove(auxTimer);	
			}
		}

		public override int idEjercicio ()
		{
			return 20;
		}

		public override void iniciar ()
		{
			auxGC = new Gdk.GC( liPanel.GdkWindow );
			liPanel.ImageBackground.Pixmap = new Gdk.Pixmap( liPanel.ImageBackground.GdkWindow, 1100, 600 );
			InitLayout();
			auxPixmap = liPanel.ImageBackground.Pixmap;				
			// Motion event
			/* liPanel.EventBoxBackground.MotionNotifyEvent += delegate(object o, Gtk.MotionNotifyEventArgs args) {								
				auxLayout.SetMarkup( args.Event.X+","+args.Event.Y );
				liPanel.ImageBackground.Pixmap.DrawLayout( auxGC, (int) args.Event.X, (int) args.Event.Y, auxLayout);
				liPanel.ImageBackground.QueueDraw();
			};*/
			
			// Click event
			liPanel.EventBoxBackground.ButtonReleaseEvent += HandleLiPanelImageBackgroundButtonPressEvent;
		
			// Expose event
			liPanel.ImageBackground.ExposeEvent += HandleLiPanelImageBackgroundExposeEvent;
			liPanel.Show();
			
			// InitGame();
			InitDemo();
		}
		
		public void InitGame() {
			
			if ( CurrentLevel == 0 )
				CurrentLevel = 1;
			
			liPanel.HboxDemo.HideAll();
			liPanel.HboxGameButtons.ShowAll();

			SessionManager.GetInstance().ChangeExerciseStatus("game");
			currentMode = "game";
			somethingChanged = true;
			
			while ( CreateScene() == false ) {}	
			
			currentOption = GameOptions.Hall;
			
			if ( auxTimer != null )  {
				GLib.Source.Remove(auxTimer);
			}

			currentHandler = new GLib.TimeoutHandler (Update);
			// currentInterval = (uint) REPAINT_SPEED;
			currentInterval = (uint) 200;
			auxTimer = GLib.Timeout.Add (currentInterval, currentHandler);
		
		}
		
		private void InitDemo() {
			
			dialogPixbuf = Gdk.Pixbuf.LoadFromResource( "pesco.ejercicios.resources.img.dialog.png" );
			pepe1Pixbuf = Gdk.Pixbuf.LoadFromResource( "pesco.ejercicios.resources.img.pepe1.png" );
			pepe2Pixbuf = Gdk.Pixbuf.LoadFromResource( "pesco.ejercicios.resources.img.pepe2.png" );

			currentMode = "demo";
			while ( CreateScene() == false ) {}
			
			// IMPORTANT: In demo, the invalid item in the first room is removed
			// This is done to secure a place for the valid item that is in the second room, 
			// because user won't be allowed to pick up items from first room		
			roomsInGame[0].ItemsPositionsStatus[itemsInGame[0].ItemPosition] = -1;
			itemsInGame[0].ItemPosition = -1;		
									
			currentOption = GameOptions.Hall;
			
			if ( auxTimer != null ) 
				GLib.Source.Remove(auxTimer);
			
			currentHandler = new GLib.TimeoutHandler (Update);
			currentInterval = (uint) REPAINT_SPEED;
			auxTimer = GLib.Timeout.Add (currentInterval, currentHandler);
		
		}
		
		
		#region Game logic functions
		private bool CreateScene() {
			
			int auxLevel = CurrentLevel;
			int auxSubLevel = CurrentSubLevel;
			if ( currentMode == "demo" ) {
				auxLevel = 0;
				auxSubLevel = 0;
			}
			// Select rooms for scene
			// Clean current rooms in game
			roomsInGame = new List<LI_RoomInGame>();
			itemsInGame = new List<LI_ItemInGame>();
			invalidItemsInGame = new List<LI_Item>();
			// currentResult = new LI_Result();
			currentResult = new SingleResultLostItems();
			
			currentResult.Level = auxLevel;
			currentResult.SubLevel = auxSubLevel;
				
			pickedItems = new List<LI_Item>();
			currentPickedItemIndex = -1;
			coinsFound = 0;
			itemsNotInRoom = 0;
			
			// Console.WriteLine("Level: "+currentLevel+" | SubLevel: "+CurrentSubLevel );
			
			// Pre: Select rooms
			for ( int i = 0; i < roomsPerLevel[auxLevel]; i++ ) {
				roomsInGame.Add( new LI_RoomInGame( roomsManager.GetNonUsedInGameRoom( roomsInGame ) ) );
			}
			// Console.WriteLine ("Pre: Rooms in game");			
			for ( int i = 0; i < roomsInGame.Count; i++ ) {
				currentResult.RoomsSelected.Add( roomsInGame[i].Room.Name );
				// Console.Write( i+": "+roomsInGame[i].Room.Name );
			}
			// Console.WriteLine("");

			// Fill rooms of scene			
			// First: invalid items
			itemsManager.ResetNonUsedList();
			itemsManager.RemoveItemsNotInGame(roomsInGame);
			// Console.WriteLine("First: Select invalid items. "+itemsPerSubLevel[auxSubLevel]+" items per "+roomsPerLevel[auxLevel]+ " rooms");
			
			int lookingForRoomIndex = 0;
			int lastRoom = -1;
			int abortCounter = 0;
			while ( itemsNotInRoom < (roomsPerLevel[auxLevel] * itemsPerSubLevel[auxSubLevel]) ) {					
				// Get the same quantity of items for each room
				lookingForRoomIndex = itemsNotInRoom / itemsPerSubLevel[auxSubLevel];
				if ( lastRoom != lookingForRoomIndex ) {
					// Console.WriteLine("Looking for room: "+roomsInGame[lookingForRoomIndex].Room.Name );
					lastRoom = lookingForRoomIndex;
				}
				// TODO: More items!!! Some combinations fail because there are not enough items
				// Dormitorio niños and Lavadero are not filled by invalid items
				// Look for invalid item for that room
				LI_Item auxItem = itemsManager.GetNonUsedInvalidItemForRoom( roomsInGame[lookingForRoomIndex], roomsInGame, invalidItemsInGame );								
				if ( auxItem == null ) {
					// First time we'll regenerate the items list
					if ( abortCounter == 0 ) {
						// Console.WriteLine( "Item nulo para habitación"+roomsInGame[lookingForRoomIndex].Room.Name );
						itemsInGame = new List<LI_ItemInGame>();
						invalidItemsInGame = new List<LI_Item>();
						itemsManager.ResetNonUsedList();
						itemsManager.RemoveItemsNotInGame(roomsInGame);					
						itemsNotInRoom = 0;
						lookingForRoomIndex = 0;
						lastRoom = -1;
						abortCounter++;
						continue;
					}
					// Second time we'll restart the process
					else if ( abortCounter == 1 ) {
						// Console.WriteLine("Proceso abortado. Restart");
						roomsInGame = new List<LI_RoomInGame>();
						itemsInGame = new List<LI_ItemInGame>();
						invalidItemsInGame = new List<LI_Item>();					
						itemsNotInRoom = 0;
						lookingForRoomIndex = 0;
						lastRoom = -1;
						abortCounter = 0;
						// Select rooms again
						for ( int i = 0; i < roomsPerLevel[auxLevel]; i++ ) {
							roomsInGame.Add( new LI_RoomInGame( roomsManager.GetNonUsedInGameRoom( roomsInGame ) ) );
						}
						itemsManager.ResetNonUsedList();
						itemsManager.RemoveItemsNotInGame(roomsInGame);
						continue;
					}
				}
				// Look for position for that item in the room
				int auxPosition = roomsManager.GetItemPositionInRoomFor ( roomsInGame[lookingForRoomIndex], auxItem );			
				if ( auxPosition == -1 ) {						
					// The item fits in the room, but there are not free positions
					continue;
				} else {
					// Add item and position to the items in game list
					itemsInGame.Add( new LI_ItemInGame( auxItem, auxPosition) );
					invalidItemsInGame.Add(auxItem);
					// Update position status in roon in game
					roomsInGame[lookingForRoomIndex].ItemsPositionsStatus[auxPosition] = auxItem.Id;
					itemsNotInRoom++;
				}
			}
			
			// Update current result
			currentResult.ItemsCounterBegin = itemsInGame.Count;			
			// Console.WriteLine("Items inválidos seleccionados:");
			for ( int i = 0; i < itemsInGame.Count; i++ ) {
				// Console.Write( itemsInGame[i].Item.Name+", " );
				currentResult.ItemsAtBegin.Add( 
				               					new LI_ResultItemRoom( itemsInGame[i].Item.Name, 
				                                      roomsManager.GetRoomByPositionId( itemsInGame[i].ItemPosition ).Name )
				                           		);
			}
			// Console.WriteLine("\nSecond: Fill rooms with valid items");
			// Second: valid items
			for ( int i = 0; i < roomsInGame.Count; i++ ) {
				// Pointer to roomsInGame[i]
				LI_RoomInGame auxRoomInGame = roomsInGame[i];
				Dictionary <int,int> auxiliarDictionary = new Dictionary<int, int>();
				// Iterate over positions
				foreach(KeyValuePair<int,int> entry in auxRoomInGame.ItemsPositionsStatus) {
					// Is this position empty? Maybe is already filled by an invalid item
					if (  entry.Value == -1 ) {	
						if ( entry.Key == -1 ) {
							Console.WriteLine("Tengo un -1 y no se porque");	
						}
						// Get a non already used item for the position
						LI_Item auxItem = itemsManager.GetNonUsedValidItemForRoomPosition( auxRoomInGame.RoomId, entry.Key, itemsInGame );
						// If there is an item for the position, add to dictionary and to itemsInGame
						if ( auxItem != null ) {
							auxiliarDictionary.Add( entry.Key, auxItem.Id );
							itemsInGame.Add( new LI_ItemInGame( auxItem, entry.Key ) );
						}
						// If there is not an item, position value will be -1
						else {
							if ( entry.Key == -1 ) {
								// Console.WriteLine("Tengo un -1 y no se porque");	
							}
							auxiliarDictionary.Add( entry.Key, -1 );
						}
					}
					// Position is filled, add current value to auxiliar dictionary
					else {
						if ( entry.Key != -1 ) {
							auxiliarDictionary.Add( entry.Key, entry.Value );
						} else {
							// Console.WriteLine("Wrong");	
						}
					}
				}				
				// Replace current room item positions status
				auxRoomInGame.ItemsPositionsStatus.Clear();
				auxRoomInGame.ItemsPositionsStatus = auxiliarDictionary;
			}
			// Console.WriteLine("");
			// Console.WriteLine("Third: secure a position for invalid items");
			// Third: secure a position for invalid items in the valid rooms
			foreach ( LI_Item itemEntry in invalidItemsInGame ) {
				// Look for valid room
				int validRoomInGameIndex = 0;
				for ( validRoomInGameIndex = 0; validRoomInGameIndex < roomsInGame.Count; validRoomInGameIndex++ ) {
					if ( roomsInGame[validRoomInGameIndex].RoomId == itemEntry.Room ) {
						break;	
					}
				}
				// Now Valid room in game is room validRoomInGameIndex
				// If there is an empty place for this item in that room?
				bool emptyPlace = false;
				bool reservedPlace = false;
				// Console.WriteLine( "Castañazo: "+validRoomInGameIndex );
				foreach ( KeyValuePair<int,int> positionStatusEntry in roomsInGame[validRoomInGameIndex].ItemsPositionsStatus ) {
					// If item fits in the place, and position status is -1 (so is free), then there is a place
					// Console.WriteLine("Verificando si "+itemEntry.Key.Name+" cabe en posición: "+positionStatusEntry.Key);
					try { 
						if ( itemEntry.FitInPosition ( positionsManager.GetPositionById( positionStatusEntry.Key ) ) &&
					    positionStatusEntry.Value == -1 && 
						roomsInGame[validRoomInGameIndex].ReservedPosition[positionStatusEntry.Key] == -1 ) {
							emptyPlace = true;
							roomsInGame[validRoomInGameIndex].ReservedPosition[positionStatusEntry.Key] = itemEntry.Id;							
							break;
						}
					} catch ( Exception e ) { 
						// Console.WriteLine("Algo fallo");
					}
				}
				// If there is not an empty place we have to free some position, but without deleting an invalid item
				if ( !emptyPlace ) {
					int emptyPlaceIndex = -1;
					foreach ( KeyValuePair<int,int> positionStatusEntry in roomsInGame[validRoomInGameIndex].ItemsPositionsStatus ) {
						// If item fits in the place, and position status is not -1 (so is not free), then delete item in that position
						if ( itemEntry.FitInPosition ( positionsManager.GetPositionById( positionStatusEntry.Key ) ) ) {
						    if ( positionStatusEntry.Value != -1 ) {
								if ( itemsManager.GetItemById( positionStatusEntry.Value ).Room == roomsInGame[validRoomInGameIndex].RoomId ) {
									// Console.WriteLine("El objeto "+itemEntry.Name+" reemplaza a "+itemsManager.GetItemById( positionStatusEntry.Value ).Name);
									foreach ( LI_ItemInGame itemToDelete in itemsInGame ) {
										if ( itemToDelete.ItemId == positionStatusEntry.Value ) {
											itemsInGame.Remove( itemToDelete );
											break;
										}
									}
			    					roomsInGame[validRoomInGameIndex].ItemsPositionsStatus[positionStatusEntry.Key] = -1;
									roomsInGame[validRoomInGameIndex].ReservedPosition[positionStatusEntry.Key] = itemEntry.Id;
									emptyPlaceIndex = positionStatusEntry.Key;
									break;
								} else {
									if ( roomsInGame[validRoomInGameIndex].ReservedPosition[positionStatusEntry.Key] == -1 ) {
										roomsInGame[validRoomInGameIndex].ReservedPosition[positionStatusEntry.Key] = itemEntry.Id;
										reservedPlace = true;
										break;
									} else {
										// Console.WriteLine("El objeto cabe, pero la posición está ocupada por otro objeto inválido.");		
									}
								}
							}
						}
					}
					if ( emptyPlaceIndex != -1) {
						roomsInGame[validRoomInGameIndex].ItemsPositionsStatus[emptyPlaceIndex] = -1;
					} else if ( !reservedPlace ) {
						// Console.WriteLine("Escenario imposible. Restart");
						return false;
					}
				}			
			}
			// Fourth: Coins! Don't forget the coins!
			for ( int i = 0; i < roomsInGame.Count; i++ ) {
				for ( int j = 0; j < itemsPerSubLevel[auxSubLevel]; j++ ) {
					// Look for a non used position					
					int auxPosition = roomsManager.GetCoinPositionInRoomFor ( roomsInGame[i] );
					// Console.WriteLine("Me dan posición, es la: "+auxPosition);		
					// Add coin and position to the coins positions tatus
					roomsInGame[i].CoinsPositionsStatus[auxPosition] = 1;
				}
			}
			currentResult.CoinsBegin = roomsPerLevel[auxLevel]*itemsPerSubLevel[auxSubLevel];
			
			// Final distribution
			for ( int i = 0; i < roomsInGame.Count; i++ ) {
				// Console.WriteLine("Habitación: "+roomsInGame[i].Room.Name);
				foreach(KeyValuePair<int,int> entry in roomsInGame[i].ItemsPositionsStatus) {
					// Console.Write( "Pos. "+entry.Key+": "+entry.Value+" | ");
				}
				// Console.WriteLine("");
			}
			
			// Current result set start time
			currentResult.Start = DateTime.Now;
			// Create Hall
			liHallPanel = new LI_HallPanel( this, roomsInGame );
		
			return true;
		}		
		
		private void CalculateNotInRoomItems() {
			/*	for ( int i = 0; i < itemsInGame.Count; i++ ) {
				if ( itemsInGame[i].Item.Room != 
				    positionsManager.GetPositionById( itemsInGame[i].
			}*/
		}
		#endregion
		
		#region Pick up and drop items functions
		// Pick up
		private void PickUpItemInPosition ( int indexInRoom, int positionId, int itemId ) {			
			pickedItems.Add( ItemsManager.GetItemById(itemId) );
			for ( int j = 0; j < itemsInGame.Count; j++ ) {
				if ( itemsInGame[j].ItemId == itemId ) {
					itemsInGame[j].ItemPosition = -1;
				}
			}
			roomsInGame[currentRoom].ItemsPositionsStatus[positionId] = -1;
		}
		
		// Drop
		private void DropItemInPosition ( int positionId ) {
			for ( int j = 0; j < itemsInGame.Count; j++ ) {
				if ( itemsInGame[j].ItemId == pickedItems[currentPickedItemIndex].Id ) {
					itemsInGame[j].ItemPosition = positionId;
					if ( itemsInGame[j].Item.Room == roomsInGame[CurrentRoom].RoomId ) {
						itemsNotInRoom--;	
					}
				}
			}
			roomsInGame[currentRoom].ItemsPositionsStatus[positionId] = pickedItems[currentPickedItemIndex].Id;
			pickedItems.Remove( pickedItems[currentPickedItemIndex] );
		}		
		#endregion
		
		#region Drawing functions
		private void InitLayout ()
		{
			
			auxLayout = new Pango.Layout ( liPanel.CreatePangoContext() );
			auxLayout.Width = Pango.Units.FromPixels (760);
			auxLayout.Justify = false;
			auxLayout.Wrap = Pango.WrapMode.Word;
			auxLayout.Alignment = Pango.Alignment.Left;
			auxLayout.FontDescription = Pango.FontDescription.FromString ("Ahafoni CLM 10");
			
		}
		
		private bool Update() {
			
			liPanel.ImageBackground.QueueDraw();
			liPanel.QueueDraw();
			return true;
			
		}
		
		public void Draw() {
		
			if ( currentMode == "game" ) {
				if ( somethingChanged ) {
					DrawGame();	
				}
			} else if ( currentMode == "demo" ) {
				DrawDemo();	
			}
		
		}
		
		public void DrawDemo() {
						
			// 0: Hello
			if (currentStep == 0) {
				if ( FirstFrameStep() ) {
					currentCharacter = 0;
					stringToSay = animationsText[currentStep];
					totalCharacters = animationsText[currentStep].Length;
					liPanel.ButtonGoBack.Sensitive = false;
					liPanel.ButtonGoLast.HideAll();
					liPanel.ButtonStartExercise.HideAll();
					liPanel.HboxDemo.Show();
					// liPanel.HboxDebug.HideAll();
					liPanel.HboxGameButtons.HideAll();
				}				
				Gdk.Rectangle auxRectangle = new Gdk.Rectangle(0,0,WIDTH_SCREEN,HEIGHT_SCREEN);
				auxPixmap.DrawRectangle( liPanel.Style.WhiteGC, true, auxRectangle );
				auxLayout.SetMarkup ("<span font_desc=\"Ahafoni CLM Bold 20\" color=\"blue\">" + stringToSay.Substring (0, currentCharacter) + "</span>");
				IncrementCharacterDialog();
				DrawDialog ();				
			// 1: Explanation 1
			} else if ( currentStep == 1 ) {
				if ( FirstFrameStep() ) {
					liPanel.ButtonGoBack.Sensitive = true;
					liPanel.ButtonGoForward.Sensitive = true;					
					currentCharacter = 0;
					stringToSay = animationsText[currentStep];
					totalCharacters = animationsText[currentStep].Length;					
					currentOption = GameOptions.Hall;
					DrawGame();
				}
				auxLayout.SetMarkup ("<span font_desc=\"Ahafoni CLM Bold 20\" color=\"blue\">" + stringToSay.Substring (0, currentCharacter) + "</span>");
				IncrementCharacterDialog();
				DrawDialog();
			} // 2: Enter in room
			else if ( currentStep == 2 ) {
				if ( FirstFrameStep() ) {				 	
					currentCharacter = 0;
					if ( roomsInGame[1].Room.Gender == "male" ) {
						stringToSay = String.Format( animationsText[currentStep], "el "+roomsInGame[1].Room.Name );
					} else {
						stringToSay = String.Format( animationsText[currentStep], "la "+roomsInGame[1].Room.Name );
					}
					totalCharacters = stringToSay.Length;
					liPanel.ButtonGoBack.Sensitive = false;
					liPanel.ButtonGoForward.Sensitive = false;
					DrawGame();
				}		
				auxLayout.SetMarkup ("<span font_desc=\"Ahafoni CLM Bold 20\" color=\"blue\">" + stringToSay.Substring (0, currentCharacter) + "</span>");
				IncrementCharacterDialog();
				DrawDialog();
			} // 3: Pick up item
			else if ( currentStep == 3 ) {
				if ( FirstFrameStep() ) {
					DrawGame();
					currentCharacter = 0;
					stringToSay = String.Format( animationsText[currentStep], itemsInGame[1].Item.Name );
					totalCharacters = stringToSay.Length;
					liPanel.ButtonGoBack.Sensitive = false;
					liPanel.ButtonGoForward.Sensitive = true;
					DrawGame();
				}
				auxLayout.SetMarkup ("<span font_desc=\"Ahafoni CLM Bold 20\" color=\"blue\">" + stringToSay.Substring (0, currentCharacter) + "</span>");
				IncrementCharacterDialog();
				DrawDialog();
			} // 4: Room without dialog waiting for item picked
			else if ( currentStep == 4 ) {
				if ( FirstFrameStep() ) {
					DrawGame();
					currentCharacter = 0;
					stringToSay = String.Format( animationsText[currentStep], itemsInGame[1].Item.Name );
					totalCharacters = stringToSay.Length;
					liPanel.ButtonGoBack.Sensitive = false;
					liPanel.ButtonGoForward.Sensitive = false;
					DrawGame();
				}
				auxLayout.SetMarkup ("<span font_desc=\"Ahafoni CLM Bold 20\" color=\"blue\">" + stringToSay.Substring (0, currentCharacter) + "</span>");
				IncrementCharacterDialog();
			}// 5: Item picked
			else if ( currentStep == 5 ) {
				if ( FirstFrameStep() ) {
					DrawGame();
					currentCharacter = 0;
					stringToSay = animationsText[currentStep];
					totalCharacters = stringToSay.Length;
					liPanel.ButtonGoForward.Sensitive = true;
					DrawGame();
				}
				auxLayout.SetMarkup ("<span font_desc=\"Ahafoni CLM Bold 20\" color=\"blue\">" + stringToSay.Substring (0, currentCharacter) + "</span>");
				IncrementCharacterDialog();
				DrawDialog();
			} // 6: Leave room
			else if ( currentStep == 6 ) {
				if ( FirstFrameStep() ) {
					DrawGame();
					currentCharacter = 0;
					stringToSay = animationsText[currentStep];
					totalCharacters = stringToSay.Length;
					liPanel.ButtonGoForward.Sensitive = false;
					DrawGame();
				}
				auxLayout.SetMarkup ("<span font_desc=\"Ahafoni CLM Bold 20\" color=\"blue\">" + stringToSay.Substring (0, currentCharacter) + "</span>");
				IncrementCharacterDialog();
				DrawDialog();
			} // 7: Explanation to put item
			else if ( currentStep == 7 ) {
				if ( FirstFrameStep() ) {
					DrawGame();
					currentCharacter = 0;
					string auxRoomName;
					if ( roomsInGame[0].Room.Gender == "male" ) {
						auxRoomName = "el "+roomsInGame[0].Room.Name;
					} else {
						auxRoomName = "la "+roomsInGame[0].Room.Name;
					}
					stringToSay = String.Format( animationsText[currentStep], itemsInGame[1].Item.Name, auxRoomName );
					totalCharacters = stringToSay.Length;
					// liPanel.ButtonGoForward.Sensitive = true;
					DrawGame();
				}
				auxLayout.SetMarkup ("<span font_desc=\"Ahafoni CLM Bold 20\" color=\"blue\">" + stringToSay.Substring (0, currentCharacter) + "</span>");
				IncrementCharacterDialog();
				DrawDialog();
			}  // 8: Select item
			else if ( currentStep == 8 ) {
				if ( FirstFrameStep() ) {
					DrawGame();
					currentCharacter = 0;
					stringToSay = String.Format( animationsText[currentStep], itemsInGame[1].Item.Name );
					totalCharacters = stringToSay.Length;
					DrawGame();
				}
				auxLayout.SetMarkup ("<span font_desc=\"Ahafoni CLM Bold 20\" color=\"blue\">" + stringToSay.Substring (0, currentCharacter) + "</span>");
				IncrementCharacterDialog();
				DrawDialog();
			}  // 9: Item selected
			else if ( currentStep == 9 ) {
				if ( FirstFrameStep() ) {
					DrawGame();
					currentCharacter = 0;
					stringToSay = String.Format( animationsText[currentStep], itemsInGame[1].Item.Name );
					totalCharacters = stringToSay.Length;
					liPanel.ButtonGoForward.Sensitive = true;
					DrawGame();
				}
				auxLayout.SetMarkup ("<span font_desc=\"Ahafoni CLM Bold 20\" color=\"blue\">" + stringToSay.Substring (0, currentCharacter) + "</span>");
				IncrementCharacterDialog();
				DrawDialog();
			} // 10: Room without dialog
			else if ( currentStep == 10 ) {
				if ( FirstFrameStep() ) {
					DrawGame();
					currentCharacter = 0;
					stringToSay = String.Format( animationsText[currentStep], itemsInGame[1].Item.Name );
					totalCharacters = stringToSay.Length;
					liPanel.ButtonGoForward.Sensitive = false;
					DrawGame();					
				}
			} // 11: Item in position
			else if ( currentStep == 11 ) {
				if ( FirstFrameStep() ) {
					DrawGame();
					currentCharacter = 0;
					stringToSay = String.Format( animationsText[currentStep], itemsInGame[1].Item.Name );
					totalCharacters = stringToSay.Length;
					liPanel.ButtonGoForward.Sensitive = true;
					DrawGame();					
				}
				auxLayout.SetMarkup ("<span font_desc=\"Ahafoni CLM Bold 20\" color=\"blue\">" + stringToSay.Substring (0, currentCharacter) + "</span>");
				IncrementCharacterDialog();
				DrawDialog();
			}  // 12: Coin hidden
			else if ( currentStep == 12 ) {
				if ( FirstFrameStep() ) {
					DrawGame();
					currentCharacter = 0;
					stringToSay = animationsText[currentStep];
					totalCharacters = stringToSay.Length;
					liPanel.ButtonGoForward.Sensitive = true;
					DrawGame();					
				}			
				auxLayout.SetMarkup ("<span font_desc=\"Ahafoni CLM Bold 20\" color=\"blue\">" + stringToSay.Substring (0, currentCharacter) + "</span>");
				IncrementCharacterDialog();
				DrawDialog();
			}	// 13: Room without dialog
			else if ( currentStep == 13 ) {
				if ( FirstFrameStep() ) {
					DrawGame();
					currentCharacter = 0;
					stringToSay = animationsText[currentStep];
					totalCharacters = stringToSay.Length;
					liPanel.ButtonGoForward.Sensitive = true;
					DrawGame();					
				}			
				auxLayout.SetMarkup ("<span font_desc=\"Ahafoni CLM Bold 20\" color=\"blue\">" + stringToSay.Substring (0, currentCharacter) + "</span>");
				IncrementCharacterDialog();
			} 	// 14: Demo completed
			else if ( currentStep == 14 ) {
				if ( FirstFrameStep() ) {
					DrawGame();
					currentCharacter = 0;
					stringToSay = animationsText[currentStep];
					totalCharacters = stringToSay.Length;
					liPanel.ButtonGoForward.Sensitive = true;
					DrawGame();					
				}			
				auxLayout.SetMarkup ("<span font_desc=\"Ahafoni CLM Bold 20\" color=\"blue\">" + stringToSay.Substring (0, currentCharacter) + "</span>");
				IncrementCharacterDialog();
				DrawDialog();
			} // 15: Show cartel
			else if ( currentStep == 15 ) {
				if ( FirstFrameStep() ) {
					DrawGame();
					currentCharacter = 0;
					liPanel.ButtonStartExercise.Sensitive = true;
					liPanel.ButtonStartExercise.ShowAll();
					liPanel.ButtonGoForward.HideAll();
					liPanel.ButtonGoBack.HideAll();
					DrawGame();
					PepeUtils.GenerateCartel( liPanel.CreatePangoContext(), auxGC, auxPixmap, "¡Ensayo superado! Comienza el ejercicio" );
				}
				PepeUtils.DrawCartel();
			}
		}
		
		private void DrawDialog ()
		{
			
			if (pepeStatus > 2) {
				auxPixmap.DrawPixbuf (auxGC, pepe1Pixbuf, 25, 0, 0, HEIGHT_SCREEN - pepe1Pixbuf.Height, pepe1Pixbuf.Width - 25, pepe1Pixbuf.Height, 0, 0,
				0);
			} else {
				auxPixmap.DrawPixbuf (auxGC, pepe2Pixbuf, 25, 0, 0, HEIGHT_SCREEN - pepe2Pixbuf.Height, pepe2Pixbuf.Width - 25, pepe2Pixbuf.Height, 0, 0,
				0);
			}
			
			auxPixmap.DrawPixbuf (auxGC, dialogPixbuf, 30, 60, 0, 0, dialogPixbuf.Width - 30, dialogPixbuf.Height - 60, 0, 0,
			0);

			// Draw text
			auxPixmap.DrawLayout (auxGC, 135, 0, auxLayout);

		}
		
		public void DrawGame() {
			
			if ( currentOption == GameOptions.Hall ) {
				DrawHall();	
			} else if ( currentOption == GameOptions.Room || currentOption == GameOptions.RoomWithPickedItem ) {
				DrawRoom( currentRoom );
			}
			DrawTopBar();
			
		}
		
		public void DrawHall() {
			
			liHallPanel.DrawInPixmap( auxGC, liPanel.ImageBackground.Pixmap );
		
		}
		
		public void DrawRoom(int idRoom) {
			
			// Auxiliar variables to clean code
			LI_RoomInGame auxRoomInGame = roomsInGame[idRoom];
			LI_Room auxRoom = auxRoomInGame.Room;
						
			// Draw background
			liPanel.ImageBackground.Pixmap.DrawPixbuf( 
			        auxGC,     		                           
			        auxRoom.BackgroundPixbuf,
			                                          0,0,
			                                          0,0,
			                                          1100, 600,
			                                          0, 0, 0);
			// Draw items positions
			if ( DebugMode ) {
				for ( int i = 0; i < auxRoom.ItemsPositions.Count; i++ ) {
					int auxPosition = auxRoom.ItemsPositions[i];
					LI_Position auxLIPosition = positionsManager.GetPositionById( auxPosition );
					auxGC.RgbFgColor = new Gdk.Color(255, 0, 0);
					liPanel.ImageBackground.Pixmap.DrawRectangle( auxGC, true, 
						new Gdk.Rectangle( auxLIPosition.X,
											auxLIPosition.Y,
											auxLIPosition.Width,
											auxLIPosition.Height) );
					auxLayout.SetMarkup("<span color=\"blue\"> Indice:"+i+" | Id: "+auxLIPosition.Id+" : "+auxLIPosition.X+","+auxLIPosition.Y+"</span>");
					liPanel.ImageBackground.Pixmap.DrawLayout( auxGC,
					                                          auxLIPosition.X,
																auxLIPosition.Y-15,
					                                          auxLayout);
				}
			}		
			
			// Draw coins positions
			if ( DebugMode ) {
				for ( int i = 0; i < auxRoom.CoinsPositions.Count; i++ ) {
					int auxPosition = auxRoom.CoinsPositions[i];
					LI_Position auxLIPosition = positionsManager.GetPositionById( auxPosition );
					auxGC.RgbFgColor = new Gdk.Color(0, 255, 0);
					liPanel.ImageBackground.Pixmap.DrawRectangle( auxGC, true, 
						new Gdk.Rectangle( auxLIPosition.X,
											auxLIPosition.Y,
											auxLIPosition.Width,
											auxLIPosition.Height) );
					auxLayout.SetMarkup("<span color=\"green\"> Indice:"+i+" | Id: "+auxLIPosition.Id+" : "+auxLIPosition.X+","+auxLIPosition.Y+"</span>");
					liPanel.ImageBackground.Pixmap.DrawLayout( auxGC,
					                                          auxLIPosition.X,
																auxLIPosition.Y-15,
					                                          auxLayout);
				}
			}
			
			// Draw coins in the room positions
			// Coins won't be drawn in the demo
			if ( currentMode == "game" || ( currentMode == "demo" && currentStep == 13) ) {
				foreach(KeyValuePair<int,int> entry in auxRoomInGame.CoinsPositionsStatus)
				{
					if ( entry.Value != -1 ) {
						// Console.WriteLine("Queremos pintar objeto "+entry.Value+" en posición "+entry.Key);
						// Get image and position values
						LI_Position auxLIPosition = positionsManager.GetPositionById( entry.Key );
						// Load pixbuf
						// Gdk.Pixbuf auxPixbuf = new Gdk.Pixbuf( Configuration.CommandExercisesDirectory+"/LostItems/img/coin.png");
						// Draw item
						liPanel.ImageBackground.Pixmap.DrawPixbuf( 
									auxGC,
									coinPixbuf,
									0,0,
									auxLIPosition.X + ( (auxLIPosition.Width-coinPixbuf.Width) / 2 ),
									auxLIPosition.Y + ( (auxLIPosition.Height-coinPixbuf.Height) ),
									coinPixbuf.Width, coinPixbuf.Height,
									0, 0, 0);
					}
				}
			}
			
			// Draw decoration
			for ( int i = 0; i < auxRoom.DecorationImages.Count; i++ ) {
				// Gdk.Pixbuf auxPixbuf = new Gdk.Pixbuf( Configuration.CommandExercisesDirectory+"/LostItems/img/rooms/decoration/"+auxRoom.DecorationImages[i] );
	
				liPanel.ImageBackground.Pixmap.DrawPixbuf( 
						auxGC,     		                           
						auxRoom.DecorationPixbufs[i],
						0,0,
						auxRoom.DecorationPositions[i].X,
						auxRoom.DecorationPositions[i].Y,
						auxRoom.DecorationPixbufs[i].Width, auxRoom.DecorationPixbufs[i].Height,
						0, 0, 0);
			}
			
			// Draw items avalaible positions. Avalaible position won't be drawn in demo step 8
			if ( ( currentOption == GameOptions.RoomWithPickedItem && currentMode == "game" )
			    || ( currentOption == GameOptions.RoomWithPickedItem && currentMode == "demo" && currentStep != 9 ) ) {
				Gdk.Pixbuf auxRedAlpha = alphaPixbuf;
				bool itemHasReserved = false;
				int itemReservedPositionIndex = -1;
				foreach ( KeyValuePair<int,int> positionReserve in roomsInGame[currentRoom].ReservedPosition ) {
					if ( positionReserve.Value == pickedItems[currentPickedItemIndex].Id ) {
						itemHasReserved = true;
						itemReservedPositionIndex = positionReserve.Key;
					}
				}
				// Item has not reserved a position
				if ( itemHasReserved == false ) {
					foreach ( KeyValuePair<int, int> positionStatusEntry in roomsInGame[CurrentRoom].ItemsPositionsStatus ) {						
						int auxPosition = positionStatusEntry.Key;
						LI_Position auxLIPosition = positionsManager.GetPositionById( auxPosition );
						if ( pickedItems[currentPickedItemIndex].FitInPosition( auxLIPosition ) &&
						    pickedItems[currentPickedItemIndex].Room == roomsInGame[CurrentRoom].RoomId &&
						    roomsInGame[currentRoom].ReservedPosition[auxPosition] == -1 &&
						    positionStatusEntry.Value == -1 ) {
							auxRedAlpha = auxRedAlpha.ScaleSimple( auxLIPosition.Width, auxLIPosition.Height, Gdk.InterpType.Bilinear );					
							liPanel.ImageBackground.Pixmap.DrawPixbuf( auxGC,
							                                        auxRedAlpha,
							                                        0, 0,
							                                        auxLIPosition.X, auxLIPosition.Y,
																	auxLIPosition.Width, auxLIPosition.Height,
							                                          0, 0, 0 );
						}
					}
				}
				// Item has reserved a position
				else {
					LI_Position auxLIPosition = positionsManager.GetPositionById( itemReservedPositionIndex );
					if ( roomsInGame[CurrentRoom].Room.ItemsPositions.Contains( auxLIPosition.Id ) ) {
						if ( roomsInGame[CurrentRoom].ItemsPositionsStatus[auxLIPosition.Id] == -1 ) {
							auxRedAlpha = auxRedAlpha.ScaleSimple( auxLIPosition.Width, auxLIPosition.Height, Gdk.InterpType.Bilinear );					
							liPanel.ImageBackground.Pixmap.DrawPixbuf( auxGC,
							                                        auxRedAlpha,
							                                        0, 0,
							                                        auxLIPosition.X, auxLIPosition.Y,
																	auxLIPosition.Width, auxLIPosition.Height,
							                                          0, 0, 0 );
						}
					}
				}
			}
			
			// Draw items in the room positions		
			foreach(KeyValuePair<int,int> entry in auxRoomInGame.ItemsPositionsStatus)
			{
				if ( entry.Value != -1 ) {
					// Console.WriteLine("Queremos pintar objeto "+entry.Value+" en posición "+entry.Key);
					// Get image and position values
			  		LI_Item auxItem = itemsManager.GetItemById( entry.Value );
					LI_Position auxLIPosition = positionsManager.GetPositionById( entry.Key );
					// Draw item
					int auxY = auxLIPosition.Y + ( (auxLIPosition.Height-auxItem.Height) );
					if ( auxLIPosition.Tags.Contains( "wardrobe" ) ) {
						auxY = auxLIPosition.Y;
					}
					liPanel.ImageBackground.Pixmap.DrawPixbuf( 
								auxGC,     		                           
								auxItem.ItemPixbuf,
								0,0,
								auxLIPosition.X + ( (auxLIPosition.Width-auxItem.Width) / 2 ),
								auxLIPosition.Y + ( (auxLIPosition.Height-auxItem.Height) ),
								auxItem.Width, auxItem.Height,
								0, 0, 0);
				}
			}
			
			/*for ( int i = 0; i < itemsManager.Items.Count; i++ ) {
				Console.WriteLine("Items...");
				int indexPositionToUse = 0;
				bool drawn = false;
				LI_Item auxItem = itemsManager.Items[i];
				Gdk.Pixbuf auxPixbuf = new Gdk.Pixbuf( Configuration.CommandExercisesDirectory+"/LostItems/img/items/"+auxItem.Image );				
				if ( auxItem.Room == idRoom ) {
					while ( indexPositionToUse < auxItem.ValidPositions.Count && !drawn ) {
						if ( roomsManager.Rooms[idRoom].ItemsPositions.IndexOf( auxItem.ValidPositions[indexPositionToUse] ) != -1 ) {
							LI_Position auxLIPosition = positionsManager.GetPositionId(auxItem.ValidPositions[indexPositionToUse]);
							liPanel.ImageBackground.Pixmap.DrawPixbuf( 
									auxGC,     		                           
									auxPixbuf,
									0,0,
									auxLIPosition.X + ( (auxLIPosition.Width-auxPixbuf.Width) / 2 ),
									auxLIPosition.Y + ( (auxLIPosition.Height-auxPixbuf.Height) ),
									auxPixbuf.Width, auxPixbuf.Height,
									0, 0, 0);
							roomsManager.Rooms[idRoom].ItemsPositions.Remove( 
								auxItem.ValidPositions[indexPositionToUse] );
							drawn = true;
						}
						indexPositionToUse++;						
					}				
				} else {		
					while ( indexPositionToUse < auxItem.InvalidPositions.Count && !drawn ) {
						if ( roomsManager.Rooms[idRoom].ItemsPositions.IndexOf( auxItem.InvalidPositions[indexPositionToUse] ) != -1 ) {
							LI_Position auxLIPosition = positionsManager.GetPositionId(auxItem.InvalidPositions[indexPositionToUse]);
							liPanel.ImageBackground.Pixmap.DrawPixbuf( 
									auxGC,     		                           
									auxPixbuf,
									0,0,
									auxLIPosition.X + ( (auxLIPosition.Width-auxPixbuf.Width) / 2 ),
									auxLIPosition.Y + ( (auxLIPosition.Height-auxPixbuf.Height) ),
									auxPixbuf.Width, auxPixbuf.Height,
									0, 0, 0);
							roomsManager.Rooms[idRoom].ItemsPositions.Remove( 
								auxItem.InvalidPositions[indexPositionToUse] );
							drawn = true;
						}
						indexPositionToUse++;						
					}
				}
			
			}*/
		}
		
		private int ItemReservedPosition ( int itemId ) {
			foreach ( KeyValuePair<int,int> positionReserve in roomsInGame[currentRoom].ReservedPosition ) {				
				if ( positionReserve.Value == itemId ) {
					return positionReserve.Key;
				}
			}	
			return -1;
		}
		
		private void ItemsCounterTextLayout() {
			auxLayout.Width = Pango.Units.FromPixels (200);
			auxLayout.Justify = false;
			auxLayout.Wrap = Pango.WrapMode.Word;
			auxLayout.Alignment = Pango.Alignment.Center;
			auxLayout.FontDescription = Pango.FontDescription.FromString ("Ahafoni CLM 12");
		}
		
		private void DrawItemsCounterText() {
			ItemsCounterTextLayout();
			auxLayout.SetMarkup("<span color=\"blue\">Objetos mal colocados:</span>\n<span color=\"red\"><big>"+itemsNotInRoom+"</big></span>");
			int auxWidth, auxHeight;
			auxLayout.GetPixelSize( out auxWidth, out auxHeight );
			liPanel.ImageBackground.Pixmap.DrawLayout(auxGC, 810, (50 - auxHeight) / 2, auxLayout);
			
			auxLayout.SetMarkup("<span color=\"blue\">Monedas encontradas:</span>\n<big><span color=\"red\">"+coinsFound+"</span> <span color=\"blue\">de</span> <span color=\"red\">"+currentResult.CoinsBegin+"</span></big>");
			auxLayout.GetPixelSize( out auxWidth, out auxHeight );
			liPanel.ImageBackground.Pixmap.DrawLayout(auxGC, 810, 50 + ((50 - auxHeight) / 2), auxLayout);
			InitLayout();
		}
		
		private void DrawTopBar() {
		
			// Alpha background				
			liPanel.ImageBackground.Pixmap.DrawPixbuf( auxGC,
			                                          new Gdk.Pixbuf( Configuration.CommandExercisesDirectory+"/LostItems/img/alpha1100x100.png") ,
			                                          0,0,
			                                          0,0,
			                                          1100,115,
			                                          0,0,0 );
			
			// Rectangles for items
			for ( int i = 0; i < maxPickedItems; i++ ) {
				if ( currentPickedItemIndex == i ) {
					auxGC.RgbFgColor = new Gdk.Color( 255, 0, 0 );
					auxGC.SetLineAttributes( 6, Gdk.LineStyle.Solid, Gdk.CapStyle.Round, Gdk.JoinStyle.Round );					
				} else {
					auxGC.RgbFgColor = new Gdk.Color( 200, 200, 200 );
					auxGC.SetLineAttributes( 2, Gdk.LineStyle.Solid, Gdk.CapStyle.Round, Gdk.JoinStyle.Round );
				}
				liPanel.ImageBackground.Pixmap.DrawRectangle( auxGC, false, new Gdk.Rectangle( (10*(i+1))+90*i, 3, 90, 90 ) );				
			}
			
			// Items
			int positionCounter = 0;
			for ( int i = 0; i < pickedItems.Count; i++ ) {
				Gdk.Pixbuf auxPixbuf = pickedItems[i].ItemPixbuf;
				double auxFactor = 0;
				if ( auxPixbuf.Width >= auxPixbuf.Height ) {
					auxFactor = 90.0 / auxPixbuf.Width;
				} else {
					auxFactor = 90.0 / auxPixbuf.Height;
				}
				double newWidth = auxPixbuf.Width * auxFactor;
				double newHeight = auxPixbuf.Height * auxFactor;
				auxPixbuf = auxPixbuf.ScaleSimple( (int) newWidth, (int) newHeight, Gdk.InterpType.Bilinear );
				liPanel.ImageBackground.Pixmap.DrawPixbuf( auxGC, auxPixbuf, 0, 0, ((10*(positionCounter+1))+90*positionCounter)+((90-auxPixbuf.Width)/2), 3+((90-auxPixbuf.Height)/2), auxPixbuf.Width, auxPixbuf.Height, 0, 0, 0);
				positionCounter++;	
			}
			
			// Button hall
			if  ( currentOption != GameOptions.Hall ) {
				liHallPanel.DrawHallButton( liPanel.ImageBackground.Pixmap, auxGC );
			}
			
			// Items counter text
			DrawItemsCounterText();
			
		}
		
		public void RedrawRoom(int idRoom) {
			
			this.currentRoom = idRoom;
			liPanel.QueueDraw();			
			
		}
		
		#endregion
		
		#region Handlers
		private void ClickGameDone( object o, Gtk.ButtonReleaseEventArgs args ) {
			
			int auxX = (int) args.Event.X;
			int auxY = (int) args.Event.Y;
					
			if ( itemsNotInRoom == 0 ) {
				liPanel.ButtonFinishExercise.Sensitive = true;	
			}
			
			// Option hall: Open room if a door is clicked
			if ( currentOption == GameOptions.Hall ) {
				int selectedRoom = -1;
				if ( auxY > 200 && auxY < 500 ) {
					
					if ( roomsInGame.Count == 4 ) {
						selectedRoom = auxX / (1100/4);
					} else if ( roomsInGame.Count == 6 ) {
						selectedRoom = auxX / (1100/6);
					}
					if ( selectedRoom != -1 ) {
						if ( currentPickedItemIndex != -1 ) {
							currentOption = GameOptions.RoomWithPickedItem;
						} else {
							currentOption = GameOptions.Room;
						}
						currentRoom = selectedRoom;
						currentResult.RegisterEnterRoom( roomsInGame[currentRoom].Room.Name );
						if ( itemsNotInRoom == 0 ) {
							liPanel.ButtonFinishExercise.Sensitive = true;	
						} else {
							liPanel.ButtonFinishExercise.Sensitive = false;
						}
					}
				
				}			
			// Option room: status when user is in a room and there is not an item selected			
			} else if ( currentOption == GameOptions.Room || currentOption == GameOptions.RoomWithPickedItem ) {
				// liPanel.LabelInfo.Text += " | Room: " + roomsInGame[currentRoom].Room.Name;
				// First option: User has clicked over an item in hand position
				if ( auxY < 100 && auxX < 1000) {
					int auxItemPickedClicked = auxX / 100;				
					// Is item picked clicked a valid item picked? Index should be less than current picked items counter
					if ( auxItemPickedClicked < pickedItems.Count ) {
						if ( currentPickedItemIndex != auxItemPickedClicked ) {								
							currentPickedItemIndex = auxItemPickedClicked;
							currentOption = GameOptions.RoomWithPickedItem;
							currentResult.RegisterSelectItem( pickedItems[currentPickedItemIndex].Name, roomsInGame[currentRoom].Room.Name );
						} else {
							currentResult.RegisterDeselectItem( pickedItems[currentPickedItemIndex].Name, roomsInGame[currentRoom].Room.Name );
							currentPickedItemIndex = -1;
							currentOption = GameOptions.Room;
						}
					}
				} else if ( auxY > 100 ) {
					// Second option: User has clicked over an item or coin position
					// Iterate over current room item positions
					RoomPositionClicked( auxX, auxY );
				}
				// Third option: User has clicked "Come back to hall" icon
				else if ( auxX > 950 && auxY < 110 ) {
					currentOption = GameOptions.Hall;
					currentResult.RegisterLeaveRoom( roomsInGame[currentRoom].Room.Name );
					liPanel.ButtonFinishExercise.Sensitive = true;
				}
			} else if ( currentOption == GameOptions.RoomWithPickedItem ) {
				
			}
				
		}
		
		private void ClickDemoDone( object o, Gtk.ButtonReleaseEventArgs args ) {
			
			int auxX = (int) args.Event.X;
			int auxY = (int) args.Event.Y;
			
			// Option hall: Open room if a door is clicked
			if ( currentStep == 2 || currentStep == 7 ) {
				int selectedRoom = -1;
				if ( auxY > 200 && auxY < 500 ) {
					
					if ( roomsInGame.Count == 2 ) {
						selectedRoom = auxX / (1100/2);
					}
					if ( selectedRoom == 1 && currentStep == 2 ) {
						currentRoom = selectedRoom;
						currentOption = GameOptions.Room;
						NextStep();
					} else if ( selectedRoom == 0 && currentStep == 7 ) {
						currentRoom = selectedRoom;
						currentOption = GameOptions.Room;
						NextStep();
					}
				}
			} else if ( currentStep == 4 ) {
				if ( auxY > 100 ) {
					// Second option: User has clicked over an item or coin position
					// Iterate over current room item positions
					for ( int i = 0; i < roomsInGame[currentRoom].Room.ItemsPositions.Count; i++ ) {
						LI_Position auxPosition = positionsManager.GetPositionById(roomsInGame[currentRoom].Room.ItemsPositions[i]);
						// If point clicked is a item position...
						if ( auxX >= auxPosition.X &&
						    auxX <= auxPosition.X + auxPosition.Width &&
						    auxY >= auxPosition.Y &&
						    auxY <= auxPosition.Y + auxPosition.Height ) {
								// liPanel.LabelInfo.Text += " | Position: " + auxPosition.Id;
								// Are we picking? or are we dropping? Check option
								// Picking
								if ( currentOption == GameOptions.Room ) {
									// ... and there is an item in that position ( so position status is not -1 )								
									if ( roomsInGame[currentRoom].ItemsPositionsStatus[roomsInGame[currentRoom].Room.ItemsPositions[i]] != -1 ) {
										LI_Item auxPickedItem = itemsManager.GetItemById( roomsInGame[currentRoom].ItemsPositionsStatus[roomsInGame[currentRoom].Room.ItemsPositions[i]] );
										// TODO: ... and picked item is not in the right room	
										if ( auxPickedItem.Room != roomsInGame[currentRoom].RoomId ) {
											// liPanel.LabelInfo.Text += " | Item: " + roomsInGame[currentRoom].ItemsPositionsStatus[roomsInGame[currentRoom].Room.ItemsPositions[i]];
											// Pick up item, indicating position index in room, position id and item id	( all integers )
											// We can pick less than 8 items
											PickUpItemInPosition( i, // index in room
										                     roomsInGame[currentRoom].Room.ItemsPositions[i], // position id
										                     roomsInGame[currentRoom].ItemsPositionsStatus[roomsInGame[currentRoom].Room.ItemsPositions[i]] ); // item id
											NextStep();
										}
									}
								}
							}
						}
				}
			} else if ( currentStep == 6 ) {
				if ( auxX > 950 && auxY < 100 ) {
					currentOption = GameOptions.Hall;
					NextStep();
				}
			} else if ( currentStep == 8 ) {
				if ( auxY < 100 && auxX < 1000) {
					int auxItemPickedClicked = auxX / 100;				
					// Is item picked clicked a valid item picked? Index should be less than current picked items counter
					if ( auxItemPickedClicked < pickedItems.Count ) {
						if ( currentPickedItemIndex != auxItemPickedClicked ) {								
							currentPickedItemIndex = auxItemPickedClicked;
							currentOption = GameOptions.RoomWithPickedItem;
							currentResult.RegisterSelectItem( pickedItems[currentPickedItemIndex].Name, roomsInGame[currentRoom].Room.Name );
							NextStep();							
						} else {
							currentResult.RegisterDeselectItem( pickedItems[currentPickedItemIndex].Name, roomsInGame[currentRoom].Room.Name );
							currentPickedItemIndex = -1;
							currentOption = GameOptions.Room;
						}
					}
				}
			} else if ( currentStep == 10 ) {
				RoomPositionClicked ( auxX, auxY );
				if ( itemsInGame[1].ItemPosition != -1 ) {
					NextStep();
				}
			} else if ( currentStep == 13 ) {
				RoomPositionClicked ( auxX, auxY );
				if ( coinsFound > 0 ) {
					NextStep();
				}
			}		
		}
		
		void RoomPositionClicked( int auxX, int auxY ) {
			
			for ( int i = 0; i < roomsInGame[currentRoom].Room.ItemsPositions.Count; i++ ) {
				LI_Position auxPosition = positionsManager.GetPositionById(roomsInGame[currentRoom].Room.ItemsPositions[i]);
				// If point clicked is a item position...
				if ( auxX >= auxPosition.X &&
				    auxX <= auxPosition.X + auxPosition.Width &&
				    auxY >= auxPosition.Y &&
				    auxY <= auxPosition.Y + auxPosition.Height ) {
						// liPanel.LabelInfo.Text += " | Position: " + auxPosition.Id;
						// Are we picking? or are we dropping? Check option
						// Picking
						if ( currentOption == GameOptions.Room ) {
							// ... and there is an item in that position ( so position status is not -1 )								
							if ( roomsInGame[currentRoom].ItemsPositionsStatus[roomsInGame[currentRoom].Room.ItemsPositions[i]] != -1 ) {
								LI_Item auxPickedItem = itemsManager.GetItemById( roomsInGame[currentRoom].ItemsPositionsStatus[roomsInGame[currentRoom].Room.ItemsPositions[i]] );
								// TODO: ... and picked item is not in the right room	
								if ( auxPickedItem.Room != roomsInGame[currentRoom].RoomId ) {
									// liPanel.LabelInfo.Text += " | Item: " + roomsInGame[currentRoom].ItemsPositionsStatus[roomsInGame[currentRoom].Room.ItemsPositions[i]];
									// Pick up item, indicating position index in room, position id and item id	( all integers )
									// We can pick less than 8 items
									if ( pickedItems.Count < maxPickedItems ) {
										PickUpItemInPosition( i, // index in room
								                     roomsInGame[currentRoom].Room.ItemsPositions[i], // position id
								                     roomsInGame[currentRoom].ItemsPositionsStatus[roomsInGame[currentRoom].Room.ItemsPositions[i]] ); // item id
										currentResult.RegisterPickUpItem( auxPickedItem.Name, roomsInGame[currentRoom].Room.Name );
									} 
									// Other case, a warning message is shown
									else {
										currentResult.RegisterMaxItemsReached( auxPickedItem.Name, roomsInGame[currentRoom].Room.Name );
										Gtk.MessageDialog dialog = new Gtk.MessageDialog (null, Gtk.DialogFlags.Modal, Gtk.MessageType.Warning, Gtk.ButtonsType.Ok, true, "<span size=\"xx-large\">Solo puedes llevar 8 objetos al mismo tiempo. Debes colocar alguno en su lugar antes de coger uno nuevo.</span>", "Ya tienes 8 objetos");
										GtkUtil.SetStyleRecursive( dialog, Configuration.Current.LargeFont );						
										int result = dialog.Run ();
										dialog.Destroy ();
									}
								}
							}
						} 
						// Dropping
						else if ( currentOption == GameOptions.RoomWithPickedItem ) {
						int	itemReservedPosition = ItemReservedPosition( pickedItems[currentPickedItemIndex].Id );
							if ( itemReservedPosition != -1 
						    	&& auxPosition.Id == itemReservedPosition ) {
								// Position is free? Item fits in position? 
								if ( roomsInGame[currentRoom].ItemsPositionsStatus[roomsInGame[currentRoom].Room.ItemsPositions[i]] == -1 &&
							    	pickedItems[currentPickedItemIndex].FitInPosition( auxPosition ) ) {
										// Register action
										currentResult.RegisterDropItem( pickedItems[currentPickedItemIndex].Name, roomsInGame[currentRoom].Room.Name );
										DropItemInPosition( roomsInGame[currentRoom].Room.ItemsPositions[i] );
										currentOption = GameOptions.Room;
										currentPickedItemIndex = -1;
										if ( itemsNotInRoom == 0 ) {
											liPanel.ButtonFinishExercise.Sensitive = true;	
										} else {
											liPanel.ButtonFinishExercise.Sensitive = false;
										}
								} 
								// Position is not free
								else if ( roomsInGame[currentRoom].ItemsPositionsStatus[roomsInGame[currentRoom].Room.ItemsPositions[i]] != -1 ) {
									currentResult.RegisterDropItemFilledPosition( pickedItems[currentPickedItemIndex].Name, roomsInGame[currentRoom].Room.Name );	
									Gtk.MessageDialog dialog = new Gtk.MessageDialog (null, Gtk.DialogFlags.Modal, Gtk.MessageType.Warning, Gtk.ButtonsType.Ok, true, "<span size=\"xx-large\">¡Ya hay un objeto en ese sitio! Debes cogerlo antes de dejar otro objeto en su lugar</span>", "¡Ya hay un objeto en ese sitio!");
									GtkUtil.SetStyleRecursive( dialog, Configuration.Current.LargeFont );						
									int result = dialog.Run ();
									dialog.Destroy ();									
								} 
								// Item is bigger than position
								else  if ( !pickedItems[currentPickedItemIndex].FitInPosition( auxPosition ) ) {
									currentResult.RegisterDropItemSmallerPosition( pickedItems[currentPickedItemIndex].Name, roomsInGame[currentRoom].Room.Name );										
									Gtk.MessageDialog dialog = new Gtk.MessageDialog (null, Gtk.DialogFlags.Modal, Gtk.MessageType.Warning, Gtk.ButtonsType.Ok, true, "<span size=\"xx-large\">El objeto que quieres colocar no cabe en ese sitio. Debes buscarle un sitio más grande.</span>", "¡El objeto no cabe en ese sitio!");
									GtkUtil.SetStyleRecursive( dialog, Configuration.Current.LargeFont );						
									int result = dialog.Run ();
									dialog.Destroy ();
								}
							}
						}								    
				}
			} 
			// Iterate over current room coin positions
			// November 2013 patch to give more space for clicking
			int coinMargin = 3;
			for ( int i = 0; i < roomsInGame[currentRoom].Room.CoinsPositions.Count; i++ ) {
				LI_Position auxPosition = positionsManager.GetPositionById(roomsInGame[currentRoom].Room.CoinsPositions[i]);
				// If point clicked is a item position...
				if ( auxX >= auxPosition.X - coinMargin &&
				    auxX <= auxPosition.X + auxPosition.Width + coinMargin &&
				    auxY >= auxPosition.Y - coinMargin &&
				    auxY <= auxPosition.Y + auxPosition.Height + coinMargin ) {
					// Is there a coin in that position?
					if ( roomsInGame[currentRoom].CoinsPositionsStatus[auxPosition.Id] == 1 ) {
						roomsInGame[currentRoom].CoinsPositionsStatus[auxPosition.Id] = -1;
						coinsFound++;
						currentResult.RegisterCoinFound( roomsInGame[currentRoom].Room.Name );
					}
				}
			}	
		}
		
		void HandleLiPanelImageBackgroundButtonPressEvent (object o, Gtk.ButtonReleaseEventArgs args)
		{
			if ( currentMode == "game" ) {
				ClickGameDone( o, args );
				somethingChanged = true;
			} else if ( currentMode == "demo" ) {
				ClickDemoDone( o, args );	
			}
							
		}

		void HandleLiPanelImageBackgroundExposeEvent (object o, Gtk.ExposeEventArgs args)
		{
			Draw();
		}
		
		public void FinishExercise () {
			
			// Save final time
			currentResult.End = DateTime.Now;
			
			TimeSpan auxTimeSpan = new TimeSpan();
			auxTimeSpan = currentResult.End - currentResult.Start;
			
			currentResult.TimeElapsed = Convert.ToInt32(auxTimeSpan.TotalSeconds );
			
			// Save final items counter
			currentResult.ItemsCounterEnd = currentResult.ItemsCounterBegin - itemsNotInRoom;
			
			// Save final items and room
			for ( int i = 0; i < itemsInGame.Count; i++ ) {
				// Console.WriteLine(  "Buscando posición de: "+itemsInGame[i].Item.Name+" - "+itemsInGame[i].ItemPosition );
				if ( itemsInGame[i].ItemPosition == -1 ){
					currentResult.ItemsAtEnd.Add( new LI_ResultItemRoom( itemsInGame[i].Item.Name, "COGIDO" ) );
				} else {
					LI_Room itemCurrentRoom = roomsManager.GetRoomByPositionId( itemsInGame[i].ItemPosition );
					if ( itemsInGame[i].Item.Room != itemCurrentRoom.Id ) {
						currentResult.ItemsAtEnd.Add( new LI_ResultItemRoom( itemsInGame[i].Item.Name, itemCurrentRoom.Name ) );
					}
				}
			}
			
			// Coins
			currentResult.CoinsEnd = coinsFound;
			
			// Calculate next level
			if ( currentResult.ItemsCounterEnd == currentResult.ItemsCounterBegin &&
			    currentResult.CoinsBegin == currentResult.CoinsEnd ) {
				// Increase sublevel. If sublevel == 2, increase level
				if ( CurrentSubLevel < 2 ) {
					CurrentSubLevel++;	
				} else {
					if ( CurrentLevel < 1 ) {
						CurrentLevel++;
						CurrentSubLevel = 0;
					}
				}
			}			
			// Save results
			results.setResult( currentResult );
			// resultsManager.Results.Add( currentResult );			
			Serialize();
			
			// Calculate score
			double auxScore = ( 
			                (( (double) currentResult.ItemsCounterEnd / (double) currentResult.ItemsCounterBegin ) * 0.6) 
			                +
							(( (double) currentResult.CoinsEnd / (double) currentResult.CoinsBegin ) * 0.4 )
							) * 100.0;
			
			SessionManager.GetInstance().RepetitionFinished();
			if ( SessionManager.GetInstance().HaveToFinishCurrentExercise() ) {
				SessionManager.GetInstance().ExerciseFinished( (int) auxScore );
				if ( auxTimer != null ) 
					GLib.Source.Remove(auxTimer);
				liPanel.Destroy();
				SessionManager.GetInstance().TakeControl();
			} else {
				PodiumPanel auxPodium = new PodiumPanel( (int) auxScore );
				auxPodium.BalloonText = "¡Enhorabuena, tienes una medalla! Ahora repetiremos el ejercicio. Pulsa <span color=\"black\">Continuar</span>";
				auxPodium.ButtonOK.Label = "Continuar";
				GtkUtil.SetStyle( auxPodium.ButtonOK, Configuration.Current.MediumFont );				
				auxPodium.ButtonOK.Clicked += delegate {
					//  Remove Podium
					myPanel.Remove(auxPodium);
					auxPodium.Destroy();
					myPanel.Add( liPanel );
					myPanel.Show();
					InitGame();			
				};
				
				if ( auxTimer != null )
					GLib.Source.Remove( auxTimer );
				
				myPanel.Hide();
				myPanel.Remove(liPanel);
				myPanel.Add(auxPodium);
				auxPodium.ShowAll();
				myPanel.ShowAll();
			}
		}
		#endregion
		
		#region Demo functions						
		private void IncrementCharacterDialog() {
			
			if ( currentCharacter < totalCharacters - 1 ) {
				if ( stringToSay[currentCharacter] == '<' && stringToSay[currentCharacter+1] == 's') {
					while ( stringToSay[currentCharacter-1] != 'n' || stringToSay[currentCharacter] != '>' ) {
						currentCharacter++;
					}
				}
			}
			if (currentCharacter < totalCharacters) {
				while ( currentCharacter < totalCharacters ) {
					if ( stringToSay[currentCharacter] == ' ' )
						break;
					currentCharacter++;
				}
				if ( currentCharacter < totalCharacters ) {
					currentCharacter++;
				}
				pepeStatus = currentCharacter % 5;
			} else {
				pepeStatus = 0;	
			}
		}
		
		private bool FirstFrameStep() {
			if ( firstFrameStep == true ) {
				firstFrameStep = false;
				return true;	
			}
			
			return false;
		}
		
		private bool LastFrameStep() {
			if ( currentCharacter == totalCharacters - 1 ) {
				return true;	
			}
			return false;
		}
		
		private bool AfterLastFrameStep() {
		
			if ( currentCharacter >= animationsText[currentStep].Length ) {
				return true;	
			}
			
			return false;
		}

		public void BackStep() {
			firstFrameStep = true;
			currentCharacter = 0;
			currentStep--;
		}
		
		public void NextStep() {
			firstFrameStep = true;
			currentCharacter = 0;
			currentStep++;
		}
		

		#endregion

		public override string NombreEjercicio ()
		{
			throw new NotImplementedException ();
		}

		public override void pausa ()
		{
			if (!pausedExercise) {
				pausedExercise = true;
				GLib.Source.Remove (auxTimer);
			} else {
				pausedExercise = false;
				auxTimer = GLib.Timeout.Add (currentInterval, currentHandler);
			}
		}
		
}
}


