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
using Gtk;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Mono.Unix;


namespace pesco
{


	[System.ComponentModel.ToolboxItem(true)]
	public partial class PanelBagItems : Gtk.Bin
	{
		private ExerciseBagItems auxEj;

		#region Game graphics variables
		private Gdk.Pixbuf fondoPixbuf;		
		private Gdk.Pixbuf fondoDialogoPixbuf;
		private Gdk.Pixbuf fondoDialogoPixbuf2;
		private Gdk.Pixbuf selectionPixbuf;
		private Pango.Layout auxLayout;
		private BO_Grandfather miniAbuelo = new BO_Grandfather();		
		private Gdk.Pixmap auxPixmap;
		Gdk.GC auxGC;		
		#endregion
		
		#region Panel and game constants
		const int WIDTH_SCREEN = 1024;
		const int HEIGHT_SCREEN = 600;
		
		const int WIDTH_MINIABUELO = 45;
		const int HEIGHT_MINIABUELO = 76;
		
		const int DISTANCIA_ENTRE_OBJETOS = 175;
		
		const int DURACION_DIALOGO_MS = 160;
		const int DURACION_DESAPARECEROBJETO_MS = 45;
		
		const int REPAINT_SPEED = 1;
		
		private BO_Position[] dlgPosItems = new BO_Position[6];
		private BO_Position[] dlgPosItems2Remove = new BO_Position[6];
		#endregion		

		#region Select items in bag dialog constants
		const int DIALOGSELECTITEMS_ROW1_POSX = 245;
		const int DIALOGSELECTITEMS_ROW2_POSX = 245+370;
		const int DIALOGSELECTITEMS_DISTANCETEXT = 110;
		const int DIALOGSELECTITEMS_DISTANCEITEMS = 85;
		
		const int DIALOGSELECTITEMS_POSY = 46;
		const int DIALOGSELECTITEMS_DISTANCEY = 67;
		
		private BO_Position bolsaDialogo = new BO_Position(15,420);
		#endregion
		
		#region Timers
		private int auxAnimationTimer;
		private int auxAnimationTimer2;
		private uint auxTimer;
		private GLib.IdleHandler currentHandler;
		private uint currentInterval;
		private bool pausedExercise;
		
		private string currentAnimation = "Street";
		private bool fadingOutItem = false;
		private int currentFadingOutItem = 0;
		private bool panelStarted = false;
		#endregion
		
		Cairo.Context auxCairo;
		
		#region Cycles counter
		private long auxFrames;
		private long auxCycles;
		private long auxTotalCycles;
		#endregion	
		
		#region Choose items in bag screen
		private List <BO_Item> itemsToChooseInBag = new List<BO_Item>();
		private List <int> itemsToChooseInBagPositionsFree = new List<int>();
		private List <BO_Item> itemsToChooseInBagPositionsFinal = new List<BO_Item>();
		public Dictionary<BO_Item, int> itemsToChooseInBagPositionsSelected = new Dictionary<BO_Item, int>();
		#endregion
		
		#region Ramdon generator
		Random r;
		#endregion
		
		#region Demo variables
		private Gdk.Pixbuf bgPixbuf;
		private Gdk.Pixbuf backgroundPixbuf;
		private Gdk.Pixbuf dialogDemoPixbuf;
		private Gdk.Pixbuf pepe1Pixbuf;
		private Gdk.Pixbuf pepe2Pixbuf;
		private Gdk.Pixbuf bigVanPixbuf;
		int pepeStatus = 0;
		Gdk.GC gc;
		
		// Animations
		string currentMode = "demo";
		string stringToSay;
		int currentStep = 0;
		int currentCharacter = 0;
		int totalCharacters = 0;
		string [] animationsText = { 
			Catalog.GetString("¡Bienvenido al ejercicio de la bolsa de objetos! En este ejercicio vas a ejercitar tu memoria." ),
							/*1*/	Catalog.GetString("Te voy a mostrar cómo voy realizando tareas por la ciudad. Saldré de casa con unos objetos y pararé en algunos sitios. Tienes que llevar la cuenta de los objetos que van quedando en la bolsa."),
							/*2*/	Catalog.GetString("Vamos a hacer un ensayo. Saldré de casa con unos objetos, pasaré por unas tiendas y cogeré o dejaré objetos. Al final te preguntaré qué llevo en la bolsa. ¡Pulsa <span color=\"black\">Siguiente</span> y presta atención!"),
							/*3*/	"",
							/*4*/	Catalog.GetString("Ahora te mostraré una pantalla para que selecciones los objetos que tengo en la bolsa. Pulsa <span color=\"black\">Siguiente</span> y selecciona los objetos. Cuando termines, pulsa <span color=\"black\">Siguiente</span> de nuevo."),
							/*5*/	Catalog.GetString("Ahora quiero que me indiques qué objeto y cuántas unidades he cogido. Si te fijas, en el panel verás que puedo coger 1, 2 ó 3 unidades de cada objeto."),
							/*6*/	Catalog.GetString("Ahora sabes en qué consiste el ejercicio. Recuerda, presta atención a los objetos que cojo o dejo, al final tendrás que indicarme los que quedan dentro de la bolsa. Cuando estés listo pulsa el botón <span color=\"black\">Iniciar nuevo recorrido</span>.")};
				
		int pauseTimer = 125;
		bool firstFrameStep = true;
		bool lastFrameStep = false;
		
		#endregion
		
		public string CurrentMode {
			get {
				return currentMode;
			}
			set {
				currentMode = value;
			}
		}		
		#region Init and reset functions
		public PanelBagItems() {
			this.Build ();
			GtkUtil.SetStyle( buttonStartFinishExercise, Configuration.Current.MediumFont );
			GtkUtil.SetStyle( buttonGoBack, Configuration.Current.MediumFont );
			GtkUtil.SetStyle( buttonGoForward, Configuration.Current.MediumFont );
			GtkUtil.SetStyle( buttonGoLast, Configuration.Current.MediumFont );
			GtkUtil.SetStyle( buttonRepeatDemo, Configuration.Current.MediumFont );

			buttonStartFinishExercise.Sensitive = true;
			auxEj = ExerciseBagItems.getInstance();
		}
		
		public void InitDrawingArea() {
			
			// Init drawingArea
			drawingArea.SetSizeRequest(WIDTH_SCREEN, HEIGHT_SCREEN);
			drawingArea.ExposeEvent += Expose_Event;
			drawingArea.AddEvents ((int) Gdk.EventMask.ButtonPressMask);
			
			// Init pixmap background and graphic context
			auxPixmap = new Gdk.Pixmap(drawingArea.GdkWindow, WIDTH_SCREEN, HEIGHT_SCREEN, 24);
			auxGC = new Gdk.GC(auxPixmap);
			auxCairo = Gdk.CairoHelper.Create ( auxPixmap );
			
			// Set pixmap as background
			drawingArea.GdkWindow.SetBackPixmap(auxPixmap, false);
		}
		
		public void initPanel() {

			InitDrawingArea();
			
			// Load backgrounds
			fondoPixbuf = Gdk.Pixbuf.LoadFromResource("pesco.ejercicios.BagItems.img.fondo1024.png");
			fondoDialogoPixbuf = Gdk.Pixbuf.LoadFromResource("pesco.ejercicios.BagItems.img.fondo1024-dialogo.png");
			selectionPixbuf = Gdk.Pixbuf.LoadFromResource("pesco.ejercicios.BagItems.img.seleccion60.png");
			dialogDemoPixbuf = Gdk.Pixbuf.LoadFromResource( "pesco.ejercicios.BagItems.img.dialog.png" );
			pepe1Pixbuf = Gdk.Pixbuf.LoadFromResource( "pesco.ejercicios.BagItems.img.pepe1.png" );
			pepe2Pixbuf = Gdk.Pixbuf.LoadFromResource( "pesco.ejercicios.BagItems.img.pepe2.png" );
			
			// Init layout
			InitLayout();		
			
			// Init Random generator
			r = new Random(DateTime.Now.Millisecond);			
			
		}
		
		public void InitLayout() {
			
			// Init pango layout
			auxLayout = new Pango.Layout(drawingArea.CreatePangoContext());
			auxLayout.Width = Pango.Units.FromPixels(700);
			auxLayout.Wrap = Pango.WrapMode.Word;
			auxLayout.Alignment = Pango.Alignment.Left;
			auxLayout.FontDescription = Pango.FontDescription.FromString("Ahafoni CLM Bold 32");
		}
						
		public void InitGame() {							
			
			// Notify session manager
			SessionManager.GetInstance().ChangeExerciseStatus("game");
			
			// Remove current timer if exists
			if ( auxTimer != null ) {
				GLib.Source.Remove( auxTimer );	
			}

			// Show all but demo buttons
			ShowAll();
			buttonStartFinishExercise.ShowAll();
			hboxButtonsDemo.HideAll();

			// Reset image
			DrawBackground();
			drawingArea.QueueDraw();
			
			// Reset variables
			currentMode = "game";
			panelStarted = false;
			
			currentAnimation = "Street";
			fadingOutItem = false;
			currentFadingOutItem = 0;
			
			// Change button label
			buttonStartFinishExercise.Label = Catalog.GetString("¡Iniciar nuevo recorrido!");
			GtkUtil.SetStyle( buttonStartFinishExercise, Configuration.Current.MediumFont );
		}
		
		public void LaunchGame() {
		
			// Reset layout
			InitLayout();
			
			// Reset drawing area clicked
			drawingArea.ButtonPressEvent -= DrawingAreaClicked;
			drawingArea.ButtonPressEvent += DrawingAreaClicked;
			
			// Set variables
			panelStarted = true;
			currentMode = "game";			
			pausedExercise = false;
			
			// Reset exercises variables
			auxEj.ResetExercise();
			
			currentAnimation = "Street";
			
			// Remove current timer if exists
			if ( auxTimer != null ) {
				GLib.Source.Remove( auxTimer );	
			}
			
			// Start animation
			// currentHandler = new GLib.TimeoutHandler (UpdateGame);
			currentHandler = new GLib.IdleHandler( Update );
			currentInterval = REPAINT_SPEED;
			// auxTimer = GLib.Timeout.Add (currentInterval, currentHandler);
			auxTimer = GLib.Idle.Add ( currentHandler );
			
			// Reset items position
			resetItemPositionsInDialog("add");	
		}
		
		public void InitDemo() {
			
			drawingArea.ButtonPressEvent += DrawingAreaClicked;
			
			currentMode = "demo";
			buttonStartFinishExercise.HideAll();
			hboxButtonsDemo.ShowAll();
			buttonRepeatDemo.HideAll();
			currentStep = 0;
			firstFrameStep = true;
			// Start animation			
			if ( auxTimer != null ) {
				GLib.Source.Remove( auxTimer );	
			}
			// currentHandler = new GLib.TimeoutHandler (UpdateDemo);
			currentHandler = new GLib.IdleHandler( Update );
			currentInterval = REPAINT_SPEED;
			// auxTimer = GLib.Timeout.Add (currentInterval, currentHandler);
			auxTimer = GLib.Idle.Add ( currentHandler);
				
		}
				
		private void resetItemPositionsInDialog(string kind) {
			
			if ( kind == "add" ) {
				dlgPosItems[0] = 
						new BO_Position( (auxEj.posicionTextosDialogos[1].X),
							auxEj.posicionTextosDialogos[1].Y);				
				dlgPosItems[1] = 
						new BO_Position( (auxEj.posicionTextosDialogos[1].X) + (DISTANCIA_ENTRE_OBJETOS),
						 	auxEj.posicionTextosDialogos[1].Y);				
				dlgPosItems[2] = 
						new BO_Position ( ( auxEj.posicionTextosDialogos[1].X) + (DISTANCIA_ENTRE_OBJETOS) * 2,
							auxEj.posicionTextosDialogos[1].Y);
				dlgPosItems[3] = 
						new BO_Position( (auxEj.posicionTextosDialogos[2].X),
							auxEj.posicionTextosDialogos[2].Y);				
				dlgPosItems[4] = 
						new BO_Position( (auxEj.posicionTextosDialogos[2].X) + (DISTANCIA_ENTRE_OBJETOS),
						 	auxEj.posicionTextosDialogos[2].Y);				
				dlgPosItems[5] = 
						new BO_Position ( ( auxEj.posicionTextosDialogos[2].X) + (DISTANCIA_ENTRE_OBJETOS) * 2,
							auxEj.posicionTextosDialogos[2].Y);	
			} else if ( kind == "remove" ) {			
				dlgPosItems[0] = new BO_Position( bolsaDialogo.X, bolsaDialogo.Y);			
				dlgPosItems[1] = new BO_Position( bolsaDialogo.X, bolsaDialogo.Y);			
				dlgPosItems[2] = new BO_Position( bolsaDialogo.X, bolsaDialogo.Y);
				dlgPosItems[3] = new BO_Position( bolsaDialogo.X, bolsaDialogo.Y);			
				dlgPosItems[4] = new BO_Position( bolsaDialogo.X, bolsaDialogo.Y);			
				dlgPosItems[5] = new BO_Position( bolsaDialogo.X, bolsaDialogo.Y);				
			}
			
		}
		
		#endregion
		
		#region Drawing functions
		
		private void DrawBackground() {
			
			// Draw background
			auxPixmap.DrawPixbuf ( drawingArea.Style.BaseGC (StateType.Normal), 		                       
			                                   fondoPixbuf, 
			                                   0,0,0,0,
			                                   WIDTH_SCREEN,HEIGHT_SCREEN,
			                                   0,0,0);			
			
		
		}
		
		private void DrawDemo() {
		

			// Hello
			if (currentStep == 0) {
				if ( FirstFrameStep() ) {
					currentCharacter = 0;
					totalCharacters = animationsText[currentStep].Length;
					stringToSay = animationsText[currentStep];
					buttonGoLast.HideAll();
					buttonGoBack.Sensitive = false;
					// PepeUtils.GenerateCartel( drawingArea.CreatePangoContext(), auxGC, auxPixmap, "¡Elsa e Inma for presidents!" );
				}
				DrawBackground();
				IncrementCharacterDialog();
				auxLayout.SetMarkup ("<span font_desc=\"Ahafoni CLM Bold 20\" color=\"blue\">" + stringToSay.Substring (0, currentCharacter) + "</span>");
				DrawDialog ();
				// PepeUtils.DrawCartel();
			// Explanation 1
			} else if ( currentStep == 1 ) {
				if ( FirstFrameStep() ) {
					buttonGoBack.Sensitive = true;
					currentCharacter = 0;
					totalCharacters = animationsText[currentStep].Length;
					stringToSay = animationsText[currentStep];
				}
				DrawBackground();
				DrawPlaces();
				auxLayout.SetMarkup ("<span font_desc=\"Ahafoni CLM Bold 20\" color=\"blue\">" + stringToSay.Substring (0, currentCharacter) + "</span>");
				IncrementCharacterDialog();
				DrawDialog();
			} // Pre test
			else if ( currentStep == 2 ) {
				if ( FirstFrameStep() ) {		 	
					currentCharacter = 0;
					stringToSay = animationsText[currentStep];
					totalCharacters = stringToSay.Length;
					buttonGoForward.Sensitive = true;
				}		
				DrawBackground();
				DrawPlaces();
				IncrementCharacterDialog();
				auxLayout.SetMarkup ("<span font_desc=\"Ahafoni CLM Bold 20\" color=\"blue\">" + stringToSay.Substring (0, currentCharacter) + "</span>");
				DrawDialog();
			} // Test ( show game ) 
			else if ( currentStep == 3 ) {
				buttonGoForward.Sensitive = false;
				buttonGoBack.Sensitive = false;
				if ( auxEj.CurrentScene.CurrentPlaceIndex == 7 && closeToHome() ) {
					currentStep++;
					firstFrameStep = true;					
				} else {
					UpdateGame();
					DrawGame();
				}
			} // After game
			else if ( currentStep == 4 ) {
				if ( FirstFrameStep() ) {
					currentCharacter = 0;
					stringToSay = animationsText[currentStep];
					totalCharacters = animationsText[currentStep].Length;
					buttonGoForward.Sensitive = true;
				}
				IncrementCharacterDialog();
				DrawBackground();
				DrawPlaces();
				auxLayout.SetMarkup ("<span font_desc=\"Ahafoni CLM Bold 20\" color=\"blue\">" + stringToSay.Substring( 0, currentCharacter ) + "</span>");
				DrawDialog();
			} // Select items screen
			else if ( currentStep == 5 ) {
				if ( FirstFrameStep() ) {
					buttonGoForward.Sensitive = false;
				}
				UpdateGame();
				DrawGame();
			} // Check demo
			else if ( currentStep == 6 ) {
				SingleResultBagItems auxResults = auxEj.GetResults();
				if ( FirstFrameStep() ) {					
					auxResults = auxEj.GetResults();
					// If omissiones > 2 or fails > 2, we'll repeat the demo
					if ( auxResults.Omissions > 2 || auxResults.Fails > 2 ) {
						stringToSay = "¡Has tenido "+(auxResults.Omissions)+" fallos! Para asegurarme de que has entendido el ejercicio, vamos a hacer otro ensayo. Pulsa <span color=\"black\">Repetir ensayo</span>.";
						buttonRepeatDemo.ShowAll();
						buttonGoForward.HideAll();
					} else {
						stringToSay = "¡Perfecto! Has completado el ensayo correctamente. Ahora comenzaremos el ejercicio. Pulsa <span color=\"black\">Iniciar nuevo recorrido</span> y presta atención.";
						// buttonGoForward.HideAll();
						// buttonGoBack.HideAll();
						buttonStartFinishExercise.Sensitive = true;
						buttonStartFinishExercise.ShowAll();
						hboxButtonsDemo.HideAll();
						PepeUtils.GenerateCartel( this.CreatePangoContext(), auxGC, auxPixmap, "¡Has superado el ensayo! Comienza el ejercicio" );
					}
					totalCharacters = stringToSay.Length;
					currentCharacter = 0;
				}
				DrawBackground();
				DrawPlaces();				
				IncrementCharacterDialog();
				auxLayout.SetMarkup ("<span font_desc=\"Ahafoni CLM Bold 20\" color=\"blue\">" + stringToSay.Substring (0, currentCharacter) + "</span>");
				if ( auxResults.Omissions > 2 || auxResults.Fails > 2 )
					DrawDialog();
				else
					PepeUtils.DrawCartel();
			} else if ( currentStep == 7 ) {
				if ( FirstFrameStep() ) {
					DrawBackground();
				}
				DrawBackground();
			}

		}
		
		private void DrawGame() {
			
			// Background and places we'll be always painted
			DrawBackground();
			
			// Draw places
			DrawPlaces();
						
			// Drawing animations			
			if ( currentAnimation == "Street" ) {
				drawGrandfather( miniAbuelo );
			}
			else if ( currentAnimation == "DialogLeavingHome" ) {
				drawDialogBackground();
				ShowTextDialog( 0, "Hoy he salido de casa con los siguientes objetos..." );
			}
			else if ( currentAnimation == "DialogLeavingHomeItems" ) {
				drawDialogBackground();	
				drawDialogLeavingHomeItems();
			}
			else if ( currentAnimation == "DialogPlace" ) {
				drawDialogBackground();
				ShowTextDialog( 0, "En "+auxEj.CurrentScene.CurrentPlace().Nombre+"..." );
				drawPlace( auxEj.CurrentScene.CurrentPlace(), 500, 250 );				
			}
			else if ( currentAnimation == "DialogSituation" ) {
				drawDialogBackground();
				drawSituation( auxEj.getCurrentSituation() );
			} else if ( currentAnimation == "DialogItemsInBagQuestion" ) {
				drawDialogBackground();
				ShowTextDialog( 1, "¿Qué cosas quedan en la bolsa?" );
			} else if ( currentAnimation == "DialogItemsInBag" ) {
				DrawDialogItemsInBag();
			}
			
		}
		
		private void DrawPlaces() {
		
			// Draw places
			for ( int i = 0; i < auxEj.CurrentScene.Places.Count; i++ ) {
				drawPlace( auxEj.CurrentScene.Places[i], auxEj.posicionLugares[i].X,
				          auxEj.posicionLugares[i].Y);
			}			
		}
		
		private void DrawDialog ()
		{
			
			auxPixmap.DrawPixbuf ( auxGC, dialogDemoPixbuf, 0, 0, 0, 0, dialogDemoPixbuf.Width, dialogDemoPixbuf.Height, 0, 0,
			0);
			if (pepeStatus > 2) {
				auxPixmap.DrawPixbuf ( auxGC, pepe1Pixbuf, 0, 0, 0, HEIGHT_SCREEN - pepe1Pixbuf.Height, pepe1Pixbuf.Width, pepe1Pixbuf.Height, 0, 0,
				0);
			} else {
				auxPixmap.DrawPixbuf ( auxGC, pepe2Pixbuf, 0, 0, 0, HEIGHT_SCREEN - pepe2Pixbuf.Height, pepe2Pixbuf.Width, pepe2Pixbuf.Height, 0, 0,
				0);
			}	
			// Draw text
			auxPixmap.DrawLayout ( auxGC, 200, 38, auxLayout);
		}

		private void drawDialogBackground() {
			
			auxPixmap.DrawPixbuf ( drawingArea.Style.BaseGC (StateType.Normal), 		                       
			                       fondoDialogoPixbuf,
			                       0,0,0,0,
			                       WIDTH_SCREEN,HEIGHT_SCREEN,
			                       0,0,0);
			auxPixmap.DrawPixbuf ( drawingArea.Style.BaseGC (StateType.Normal), 		                       
			                       pepe1Pixbuf,
			                       0,0,0,HEIGHT_SCREEN - pepe1Pixbuf.Height,
			                       pepe1Pixbuf.Width, pepe1Pixbuf.Height,
			                       0,0,0);	
		}
		
		private void drawPlace(BO_Place place, int posX, int posY) {
			
			auxPixmap.DrawPixbuf( drawingArea.Style.BaseGC (StateType.Normal), 		                       
				                     place.pixbufLugar,
				                     0,0,
									 posX, posY,
				                     150, 150,
				                     0,0,0);	
		}
		
		private void drawItem(BO_Item item, int posX, int posY) {
			auxCairo.IdentityMatrix();
			auxCairo.Translate( posX, posY );
			Gdk.CairoHelper.SetSourcePixbuf( auxCairo, item.pixbufObjeto, 
			                                                  0.0, 0.0 );
			auxCairo.Paint();	
			
		}
		
		private void drawItemSmall(BO_Item item, int posX, int posY) {
			
			auxPixmap.DrawPixbuf( drawingArea.Style.BaseGC (StateType.Normal), 		                       
				                     item.pixbufObjetoSmall,
				                     0,0,
									 posX, posY,
				                     55, 55,
				                     0,0,0);
			
		}
		
		private void drawItem(BO_Item item, int posX, int posY, int sizeX, int sizeY) {
			
			Gdk.Pixbuf auxPixbuf = item.pixbufObjeto.ScaleSimple(sizeX, sizeY, Gdk.InterpType.Bilinear);
			
			auxPixmap.DrawPixbuf( drawingArea.Style.BaseGC (StateType.Normal), 		                       
				                     item.pixbufObjeto,
				                     0,0,
									 posX, posY,
				                     sizeX, sizeY,
				                     0,0,0);
		}
		
		private void drawDialogLeavingHomeItems() {
			
			int itemIndex = 0;
			int i = 0;
			String itemsText = "";
			foreach(KeyValuePair<BO_Item,int> entry in auxEj.CurrentScene.ItemsAtLeavingHome)
			{	
				itemIndex++;
				if ( itemIndex == auxEj.CurrentScene.ItemsAtLeavingHome.Count ) {
					char[] charsToTrim = {',', ' '};
					itemsText = itemsText.TrimEnd( charsToTrim );
					if ( entry.Value > 1 )
						itemsText += " y "+entry.Value+" "+entry.Key.NombrePlural;
					else
						itemsText += " y "+entry.Value+" "+entry.Key.NombreSingular;
				} else {
					if ( entry.Value > 1 )
						itemsText += entry.Value+" "+entry.Key.NombrePlural+", ";
					else
						itemsText += entry.Value+" "+entry.Key.NombreSingular+", ";	
				}
				for ( int j = 0; j < entry.Value; j++ ) {
					drawItem( entry.Key,
				         dlgPosItems[i].X,
				         dlgPosItems[i].Y);
					i++;
				}
			}
			ShowTextDialog( 0, itemsText);
		}
		
		private void drawSituation(BO_SituationQuantity situation) {
			
			// The situation has a "who"?
			if ( situation.ItemActionWho.Quien == null ) {
				ShowTextDialog( 0, situation.ItemActionWho.Accion.Texto+":");
			} else {
				ShowTextDialog( 0, situation.ItemActionWho.Accion.Texto+" "
			                    + situation.ItemActionWho.Quien.Texto+":");
			}

			// Drawing items
			for ( int i = 0; i < situation.Quantity; i++ ) {
					drawItem( situation.ItemActionWho.Objeto, 
				         dlgPosItems[i].X,
				         dlgPosItems[i].Y);
			}
			
			// Drawing items descriptions
			if ( situation.Quantity == 1 ) {
				ShowTextDialog(2, situation.Quantity +" "+
				                    situation.ItemActionWho.Objeto.NombreSingular);
			} else {
				ShowTextDialog(2, situation.Quantity +" "+
					                    situation.ItemActionWho.Objeto.NombrePlural);
			}
		}
		
		private void drawGrandfather(BO_Grandfather grandfather) {
			
			auxPixmap.DrawPixbuf ( drawingArea.Style.BaseGC (StateType.Normal), 
				                                   grandfather.pixbufAbuelo,
				                                   0,0,grandfather.PosX,grandfather.PosY,
				                                   WIDTH_MINIABUELO,HEIGHT_MINIABUELO,
				                                   0,0,0);	
		}
				
		public void ShowTextDialog ( int linea, string texto ) {
		
			
			auxLayout.SetMarkup(texto);
			
			auxPixmap.DrawLayout ( auxGC, 
			                      auxEj.posicionTextosDialogos[linea].X,
			                      auxEj.posicionTextosDialogos[linea].Y, 
			                      auxLayout);			
			
		}
		
		private void DrawDialogItemsInBag() { 
			
			drawDialogBackground();
			
			for ( int i = 0; i < itemsToChooseInBagPositionsFinal.Count; i++ ) {
				
				// First column items
				int auxPositionX = DIALOGSELECTITEMS_ROW1_POSX;
				// Second column items
				if ( i >= 8 )
					auxPositionX = DIALOGSELECTITEMS_ROW2_POSX;
				
				// Name of the item with first letter uppercase
				String auxString = itemsToChooseInBagPositionsFinal[i].NombreSimple;
				auxString = auxString.Substring(0, 1).ToUpper()+auxString.Substring(1);
				drawText( auxPositionX, 
				         DIALOGSELECTITEMS_POSY+DIALOGSELECTITEMS_DISTANCEY*(i % 8 )+
				         (DIALOGSELECTITEMS_DISTANCEY/2)-10,
				         12,
				         auxString );
				
				// Drawing items
				for ( int j = 0; j < 3; j++ ) {
					drawItemSmall( itemsToChooseInBagPositionsFinal[i], 
						auxPositionX+DIALOGSELECTITEMS_DISTANCETEXT+(j%3)*DIALOGSELECTITEMS_DISTANCEITEMS,
						DIALOGSELECTITEMS_POSY+DIALOGSELECTITEMS_DISTANCEY*(i % 8 ));
					// To represent more than one item, we'll draw items over items, separated just
					// by 8 pixels.
					// Quantity 2
					if ( j == 1 || j == 2 ) {
						drawItemSmall( itemsToChooseInBagPositionsFinal[i], 
							auxPositionX+DIALOGSELECTITEMS_DISTANCETEXT+(j%3)*DIALOGSELECTITEMS_DISTANCEITEMS+8,
							DIALOGSELECTITEMS_POSY+DIALOGSELECTITEMS_DISTANCEY*(i % 8 ));
					} // For quantity 3, we'll draw again
					if ( j == 2 ) {
						drawItemSmall( itemsToChooseInBagPositionsFinal[i], 
							auxPositionX+DIALOGSELECTITEMS_DISTANCETEXT+(j%3)*DIALOGSELECTITEMS_DISTANCEITEMS+16,
							DIALOGSELECTITEMS_POSY+DIALOGSELECTITEMS_DISTANCEY*(i % 8 ));
					}
					// If item is selected, we'll draw a red circle around it
					if ( itemsToChooseInBagPositionsSelected[itemsToChooseInBagPositionsFinal[i]] == 1 ) {
						drawRedCircle( auxPositionX+DIALOGSELECTITEMS_DISTANCETEXT,
				         	DIALOGSELECTITEMS_POSY+DIALOGSELECTITEMS_DISTANCEY*(i % 8 ) );
					} else if ( itemsToChooseInBagPositionsSelected[itemsToChooseInBagPositionsFinal[i]] == 2 ) {
						drawRedCircle( auxPositionX+DIALOGSELECTITEMS_DISTANCETEXT+(1)*DIALOGSELECTITEMS_DISTANCEITEMS,
				         	DIALOGSELECTITEMS_POSY+DIALOGSELECTITEMS_DISTANCEY*(i % 8 ) );
					} else if ( itemsToChooseInBagPositionsSelected[itemsToChooseInBagPositionsFinal[i]] == 3 ) {
					     drawRedCircle(auxPositionX+DIALOGSELECTITEMS_DISTANCETEXT+(2)*DIALOGSELECTITEMS_DISTANCEITEMS,
				         	DIALOGSELECTITEMS_POSY+DIALOGSELECTITEMS_DISTANCEY*(i % 8 ) );
					}
				}			
				// Finally, we'll draw numbers 1, 2 and 3 under the item
				drawText( auxPositionX+DIALOGSELECTITEMS_DISTANCETEXT
					         +55/2,
				         DIALOGSELECTITEMS_POSY+DIALOGSELECTITEMS_DISTANCEY*(i % 8 )+50,
				         14,
				         "1" );
				drawText( auxPositionX+DIALOGSELECTITEMS_DISTANCETEXT+(1)*DIALOGSELECTITEMS_DISTANCEITEMS
					         +55/2,
				         DIALOGSELECTITEMS_POSY+DIALOGSELECTITEMS_DISTANCEY*(i % 8 )+50,
				         14,
				         "2" );
				drawText( auxPositionX+DIALOGSELECTITEMS_DISTANCETEXT+(2)*DIALOGSELECTITEMS_DISTANCEITEMS
					         +55/2,
				        DIALOGSELECTITEMS_POSY+DIALOGSELECTITEMS_DISTANCEY*(i % 8 )+50,
				         14,
				         "3" );
			}
					
		}
		
		private void drawRedCircle(int x, int y) {
			
			auxPixmap.DrawPixbuf ( drawingArea.Style.BaseGC (StateType.Normal), 
				                                   selectionPixbuf,
				                                   0,0, x, y,
				                                   60, 60,
				                                   0,0,0);	
		}
		
		private void DrawingAreaClicked(object o, ButtonPressEventArgs evnt) {		
			
			double auxX = evnt.Event.X;
			double auxY = evnt.Event.Y;
			
			int auxColumna = 0;
			int auxCantidad = 0;
			if ( auxX > DIALOGSELECTITEMS_ROW2_POSX ) {
				auxColumna = 1;
				auxCantidad = (int) (auxX - (DIALOGSELECTITEMS_ROW2_POSX+DIALOGSELECTITEMS_DISTANCETEXT)) /
								DIALOGSELECTITEMS_DISTANCEITEMS;
			} else {
				auxCantidad = (int) (auxX - (DIALOGSELECTITEMS_ROW1_POSX+DIALOGSELECTITEMS_DISTANCETEXT)) /
								DIALOGSELECTITEMS_DISTANCEITEMS;
			}
			auxCantidad++;
			int auxFila = (int) (auxY - DIALOGSELECTITEMS_POSY) / DIALOGSELECTITEMS_DISTANCEY;
			int auxObjeto;
			
			if ( auxFila < 0 || auxFila >= 8 ) {
				// Not item selected
			} else {
				// We'll do something just in game mode, or in step 3 of demo mode
				if ( (currentMode == "game" && currentAnimation == "DialogItemsInBag" ) 
				    || ( currentMode == "demo" && currentStep == 5 ) ) {
					// Look for selected item
					auxObjeto = (auxColumna * 8) + auxFila;
					if ( itemsToChooseInBagPositionsSelected[itemsToChooseInBagPositionsFinal[auxObjeto]] == auxCantidad ) {
						itemsToChooseInBagPositionsSelected[itemsToChooseInBagPositionsFinal[auxObjeto]] = 0;
					} else {
						itemsToChooseInBagPositionsSelected[itemsToChooseInBagPositionsFinal[auxObjeto]] = auxCantidad;
					}
					
					if ( currentMode == "demo" && currentStep == 5 ) {
						buttonGoForward.Sensitive = true;
					}
				}
			}
		}
		
		private void moveItemsInSituation( string kind, int item, int animationTime ) {
				if ( kind == "add" ) {
					dlgPosItems[item].X = (int) ( bolsaDialogo.X + ( ( item % 3 ) * 13 )
				                               +  
		           ( ( auxEj.posicionTextosDialogos[1].X + (DISTANCIA_ENTRE_OBJETOS * ( item % 3) )
	               - bolsaDialogo.X )
					/ (double) DURACION_DESAPARECEROBJETO_MS ) * animationTime );
			
					dlgPosItems[item].Y = (int) ( bolsaDialogo.Y +  
						( ( auxEj.posicionTextosDialogos[1].Y + (DISTANCIA_ENTRE_OBJETOS * ( item / 3 ) )
				      	- bolsaDialogo.Y )
						/ (double) DURACION_DESAPARECEROBJETO_MS ) * animationTime );
				} else if ( kind == "remove" ) {
					dlgPosItems[item].X = 
							(int) ( 
					   			( auxEj.posicionTextosDialogos[1].X + DISTANCIA_ENTRE_OBJETOS * currentFadingOutItem )
					 			- 
					           	( 
					  				( auxEj.posicionTextosDialogos[1].X + (DISTANCIA_ENTRE_OBJETOS * currentFadingOutItem)
					               - bolsaDialogo.X )
									/ (double) DURACION_DESAPARECEROBJETO_MS 
					  			) * animationTime 
					   		);
					
					dlgPosItems[item].Y = (int) ( auxEj.posicionTextosDialogos[1].Y -  
							( ( auxEj.posicionTextosDialogos[1].Y - bolsaDialogo.Y )
							/ (double) DURACION_DESAPARECEROBJETO_MS ) * animationTime );
				}
		}
		
		private void drawText( int x, int y, int size, string text ) {
		
			
			auxLayout.FontDescription = Pango.FontDescription.FromString("Ahafoni CLM Bold "+size);
			auxLayout.SetMarkup(text);
			
			auxPixmap.DrawLayout ( auxGC, 
			                      x,
			                      y, 
			                      auxLayout);			
			
		}
		#endregion
		
		#region Update functions
		public void Pause() {	
			if (!pausedExercise) {
				pausedExercise = true;
				GLib.Source.Remove ( auxTimer );
			} else {
				pausedExercise = false;
				auxTimer = GLib.Idle.Add( currentHandler );
			}
		}		
		
		private bool Update() {
			
			long currentTicks = System.Diagnostics.Stopwatch.GetTimestamp();
			long difference = currentTicks - SessionManager.GetInstance().CurrenTicks;
			long interval = SessionManager.GetInstance().Interval;
			
			if ( !pausedExercise ) {
				if ( difference > interval ) {			
					if ( currentMode == "game" ) {
						UpdateGame();
					} else if ( currentMode == "demo" ) {
						UpdateDemo();	
					}
			    	SessionManager.GetInstance().UpdateCurrenTicks();			
				}
			}
			
			return true;
		}
		
		private bool UpdateDemo () {
			
			drawingArea.QueueDraw ();				

			return true;
		}
		
		public bool UpdateGame() {
			
			// Animations
			// a) Street: Show map with shops.
			// b) DialogLeavingHome: Show items at beggining
			// c) DialogPlace: "In the place..."
			// d) DialogSituation: Add or remove Item. "I buy x objects to y..."
			// e) DialogItemsInBag: User has to select the items that are in the bag.
			
			if ( currentAnimation == "Street") {
				
				// Moving grandfather
				miniAbuelo.actualizarPosicion();
				
				// If we are starting the route we show the objects that we have at the begining
				if ( auxEj.CurrentScene.HomeAlreadyLeft == false ) {
					// Leaving home
					if ( closeToHome() ) {
						// auxEj.chooseObjectsAtHome();
						auxAnimationTimer = (DURACION_DIALOGO_MS+(DURACION_DIALOGO_MS/2)) / 2;
						currentAnimation = "DialogLeavingHome";
						auxEj.CurrentScene.HomeAlreadyLeft = true;
					}	
				}
				// If we have finished the route we show the screen to choose the quantity
				if ( auxEj.CurrentScene.CurrentPlaceIndex == 7 ) {
					if ( closeToHome() ) {
						// Arrived at home
						GenerateDialogItemsInBag();
						auxAnimationTimer = DURACION_DIALOGO_MS / 2;
						currentAnimation = "DialogItemsInBagQuestion";
					}
				} 
				// If we haven't finished we have to check for new places to stop
				else {								
					// Are we close to a new place? 
					if ( closeToNewPlace() ) {
							
							// If we are, we call Exercise to look for new actions
							auxEj.CurrentScene.abueloLlegaALugar( false );
						
							// Do we have to stop in this place? Are there situations?
							// In this case we have to reset timers and change current animation
							if ( auxEj.haveToStopAtPlace( auxEj.CurrentScene.CurrentPlaceIndex ) ) {												
								auxAnimationTimer = DURACION_DIALOGO_MS / 2;
								currentAnimation = "DialogPlace";
							}						
					}
				}
			}
			// Leaving the home...
			else if ( currentAnimation == "DialogLeavingHome" ) {
				auxAnimationTimer = auxAnimationTimer - REPAINT_SPEED;
				if ( auxAnimationTimer <= 0 ) {
					currentFadingOutItem = 0;
					auxAnimationTimer = 
						DURACION_DIALOGO_MS + DURACION_DESAPARECEROBJETO_MS * auxEj.CurrentScene.objectsAtHomeCount();
					currentAnimation = "DialogLeavingHomeItems";
					auxAnimationTimer2 = DURACION_DESAPARECEROBJETO_MS;
					resetItemPositionsInDialog("add");
					currentFadingOutItem = 0;
				}
			} 
			else if ( currentAnimation == "DialogLeavingHomeItems" )
			{
				// Updating animation timers
				auxAnimationTimer = auxAnimationTimer - REPAINT_SPEED;
				
				// Add items to the bag
				if ( auxAnimationTimer < DURACION_DESAPARECEROBJETO_MS * auxEj.CurrentScene.objectsAtHomeCount() + 60) {
				
					if ( fadingOutItem == false ) {
						fadingOutItem = true;
						auxAnimationTimer2 = DURACION_DESAPARECEROBJETO_MS;
					} else if ( auxAnimationTimer2 <= 0 ) {
						fadingOutItem = false;
						if ( currentFadingOutItem < auxEj.CurrentScene.objectsAtHomeCount()) {
							currentFadingOutItem++;
						}
					} else {
						auxAnimationTimer2 -= REPAINT_SPEED;
						// Move the items in the dialog
						if ( currentFadingOutItem != auxEj.CurrentScene.objectsAtHomeCount() )
							moveItemsInSituation( "add", 
						                     currentFadingOutItem, 
						                     auxAnimationTimer2 );
					}
				}
				
				// Is the dialog finished? Go to next dialog or to street
				if ( auxAnimationTimer <= 0 ) {
					auxAnimationTimer = DURACION_DIALOGO_MS;
					currentAnimation = "Street";
				}
				
			}
			// Show ballon with the text "At place..."
			else if ( currentAnimation == "DialogPlace" ) {
				auxAnimationTimer = auxAnimationTimer - REPAINT_SPEED;
				if ( auxAnimationTimer <= 0 ) {
					currentFadingOutItem = 0;
					auxAnimationTimer = DURACION_DIALOGO_MS;
					currentAnimation = "DialogSituation";
					auxAnimationTimer2 = DURACION_DESAPARECEROBJETO_MS;
					resetItemPositionsInDialog(auxEj.getCurrentSituation().Kind());
				}					
			} 
			// Show situations, for example "I buy 2 fish", "I give 3 coins"...
			else if ( currentAnimation == "DialogSituation" ) {
					
					// Updating animation timers
					auxAnimationTimer = auxAnimationTimer - REPAINT_SPEED;
					
					// When dialog time is more than a half, we start adding
					// items to bag dialog
					if ( auxAnimationTimer < DURACION_DESAPARECEROBJETO_MS * 3 + 100) {

						if ( fadingOutItem == false ) {
							fadingOutItem = true;
							auxAnimationTimer2 = DURACION_DESAPARECEROBJETO_MS;
						} else if ( auxAnimationTimer2 <= 0 ) {
							fadingOutItem = false;
							if ( currentFadingOutItem < auxEj.getCurrentSituation().Quantity) {
								currentFadingOutItem++;
							}
						} else {
							auxAnimationTimer2 -= REPAINT_SPEED;
							// Move the items in the dialog
							if ( currentFadingOutItem != auxEj.getCurrentSituation().Quantity )
								moveItemsInSituation( auxEj.getCurrentSituation().Kind(), 
							                     currentFadingOutItem, 
							                     auxAnimationTimer2 );
						}
										
					 
						// Is the dialog finished? Go to next dialog or to street
						if ( auxAnimationTimer <= 0 ) {
							auxAnimationTimer = DURACION_DIALOGO_MS;
							auxEj.finishCurrentSituation();
							if ( auxEj.getCurrentSituation() != null ) {
								currentFadingOutItem = 0;
								fadingOutItem = false;
								resetItemPositionsInDialog(auxEj.getCurrentSituation().Kind());
								currentAnimation = "DialogSituation";
							} else {
								currentAnimation = "Street";
							}
						}
					} 					
			} else if ( currentAnimation == "DialogItemsInBagQuestion" ) {
				if ( auxAnimationTimer <= 0 ) {
					currentAnimation = "DialogItemsInBag";
				}
				auxAnimationTimer = auxAnimationTimer - REPAINT_SPEED;
			} else if ( currentAnimation == "DialogItemsInBag" ) {
				if ( currentMode == "game" ) {
					buttonStartFinishExercise.ShowAll();
				}
				buttonStartFinishExercise.Sensitive = true;			
			}
			
			drawingArea.QueueDraw();
			return true;
			
		}
		
		#endregion
		
		#region Game logic functions
		private bool closeToNewPlace() {
			
			if ( miniAbuelo.PosX > auxEj.posicionLugares[auxEj.CurrentScene.CurrentPlaceIndex+1].X + 25&&
				    miniAbuelo.PosX < auxEj.posicionLugares[auxEj.CurrentScene.CurrentPlaceIndex+1].X + 75 &&
				    miniAbuelo.PosY > auxEj.posicionLugares[auxEj.CurrentScene.CurrentPlaceIndex+1].Y &&
				    miniAbuelo.PosY < auxEj.posicionLugares[auxEj.CurrentScene.CurrentPlaceIndex+1].Y + 100 )
				return true;
			else
				return false;
		}
		
		private bool closeToHome() {
		
			// Home is in X: 230, Y: 430
			if ( miniAbuelo.PosX > 230 - 25&&
				    miniAbuelo.PosX < 230 + 25 &&
				    miniAbuelo.PosY > 430-25 &&
				    miniAbuelo.PosY < 430+25 )
				return true;
			else
				return false;
			
		}		
		
		private void GenerateDialogItemsInBag() {
		
			itemsToChooseInBagPositionsFree.Clear();
			itemsToChooseInBagPositionsSelected.Clear();
			itemsToChooseInBagPositionsFinal.Clear();
			
			// Fill bag positions
			for ( int i = 0; i < 16; i++ ) {
				itemsToChooseInBagPositionsFree.Add(i);
				itemsToChooseInBagPositionsFinal.Add( new BO_Item() );
			}
			
			int auxRandom = 0;
			if ( currentMode == "game" || currentMode == "demo" ) {
				foreach (KeyValuePair<BO_Item,int> entry in auxEj.CurrentScene.ItemsInBag) {
					auxRandom = r.Next( 0, itemsToChooseInBagPositionsFree.Count);
					itemsToChooseInBagPositionsFinal[itemsToChooseInBagPositionsFree[auxRandom]] = 
					entry.Key;
					itemsToChooseInBagPositionsFree.RemoveAt(auxRandom);
					itemsToChooseInBagPositionsSelected.Add(entry.Key, 0);
				}				
			}/* else if ( currentMode == "demo" ) {
				BO_Item auxItem = auxEj.CurrentScene.SituationsInPlaces[auxEj.LUGARESMAPA-1][0].ItemActionWho.Objeto;
				auxRandom = 13;
				itemsToChooseInBagPositionsFinal[itemsToChooseInBagPositionsFree[auxRandom]] = auxItem;
				itemsToChooseInBagPositionsFree.RemoveAt(auxRandom);
				itemsToChooseInBagPositionsSelected.Add(auxItem, 0);
				auxEj.CurrentScene.ItemsInBag.Clear();
				auxEj.CurrentScene.ItemsInBag.Add( auxEj.CurrentScene.SituationsInPlaces[auxEj.LUGARESMAPA-1][0].ItemActionWho.Objeto, auxEj.CurrentScene.SituationsInPlaces[auxEj.LUGARESMAPA-1][0].Quantity );
			}*/
			
			// Choose random positions for distractors
			foreach (string key in auxEj.objetos.Keys) 
			{
				if (  itemsToChooseInBagPositionsFree.Count == 0)
					break;
   				if ( !auxEj.CurrentScene.ObjectInBag( (BO_Item) auxEj.objetos[key], auxEj.CurrentScene.ItemsInBag) ) {
					auxRandom = r.Next( 0, itemsToChooseInBagPositionsFree.Count);
					itemsToChooseInBagPositionsFinal[itemsToChooseInBagPositionsFree[auxRandom]] = 
						(BO_Item) auxEj.objetos[key];
					itemsToChooseInBagPositionsFree.RemoveAt(auxRandom);
					itemsToChooseInBagPositionsSelected.Add( (BO_Item) auxEj.objetos[key], 0);
				}
			}			
			
		}
		
		#endregion
		
		#region Demo control functions		
		private void IncrementCharacterDialog() {
			
			if ( currentCharacter < totalCharacters - 1 ) {
				if ( stringToSay[currentCharacter] == '<' && stringToSay[currentCharacter+1] == 's') {
					while ( stringToSay[currentCharacter] != 'n' || stringToSay[currentCharacter+1] != '>' ) {
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
		
		private void Say( string toSay ) {			
			stringToSay = toSay;
			currentCharacter = 0;
			totalCharacters = toSay.Length;
		}
		private void NextStep() {
			firstFrameStep = true;
			currentStep++;
		}
		
		private void BackStep() {
			firstFrameStep = true;
			currentStep--;
		}		
		
		protected virtual void OnButtonBackwardClicked (object sender, System.EventArgs e)
		{
			BackStep();
		}
		
		protected virtual void OnButtonGoForwardClicked (object sender, System.EventArgs e)
		{
			NextStep();
		}
		
		protected virtual void OnButtonGoLastClicked (object sender, System.EventArgs e)
		{
			stringToSay = "¡Perfecto! Has completado el ensayo correctamente. Ahora comenzaremos el ejercicio. Pulsa <span color=\"black\">Iniciar nuevo recorrido</span> y presta atención.";
			totalCharacters = stringToSay.Length;
			currentCharacter = 0;						
			buttonGoForward.HideAll();
			buttonGoBack.HideAll();
			buttonStartFinishExercise.Sensitive = true;
			buttonStartFinishExercise.ShowAll();
			hboxButtonsDemo.HideAll();
		}		
		
		protected virtual void RepeatDemo( object sender, System.EventArgs e ) {
			
			buttonGoForward.ShowAll();
			buttonRepeatDemo.HideAll();
			currentStep = 3;
			currentCharacter = 0;
			InitLayout();
			firstFrameStep = true;
			auxEj.CurrentScene.CurrentPlaceIndex = -1;
			auxEj.CurrentScene.HomeAlreadyLeft = false;
			currentAnimation = "Street";								
		
		}
		#endregion
		
		// Finish button callback
		protected virtual void FinishButtonCallback (object sender, System.EventArgs e)
		{
			
			if ( !panelStarted ) {
				LaunchGame();
				buttonStartFinishExercise.Label = "¡He terminado!";
				GtkUtil.SetStyle( buttonStartFinishExercise, Configuration.Current.MediumFont );
				buttonStartFinishExercise.HideAll();
				buttonStartFinishExercise.Sensitive = false;
			}
			else {
				ExerciseBagItems.getInstance().FinishRepetition();
			}
		
		}
		
		// Drawing area expose function
		void Expose_Event(object obj, ExposeEventArgs args){
			
			// long auxLong = System.Diagnostics.Stopwatch.GetTimestamp();
			if ( currentMode == "game" ) {
				DrawGame();	
			} else if ( currentMode == "demo" ) {
				DrawDemo();	
			}
			// Console.WriteLine("Ciclos:"+ (System.Diagnostics.Stopwatch.GetTimestamp() - auxLong));
			
		}
		
	}		
}

