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

namespace pesco
{


	[System.ComponentModel.ToolboxItem(true)]
	public partial class PanelPackageDelivering : Gtk.Bin
	{
		private Gdk.Pixbuf fondoPixbuf;
		private Gdk.Pixmap auxPixmap;

		private PD_Position[] positions = new PD_Position[16];

		const int WIDTH_SCREEN = 998;
		const int HEIGHT_SCREEN = 600;
		
		// TotalPodiumPanel auxPodium = null;
		WelcomePanel auxPodium = null;
		// Exercise pointer
		ExercisePackageDelivering exer = null;
		
		// Places consts
		const int PLACE_WIDTH = 110;
		const int PLACE_HEIGHT = 110;
		const int FIRST_PLACE_X = 120;
		const int FIRST_PLACE_Y = 120;
		// Distances consts
		const int PLACE_DISTANCE_X = 260;
		const int PLACE_DISTANCE_Y = 130;
		// Boxes consts
		const int BOX_WIDTH = 50;
		const int BOX_HEIGHT = 50;

		// Car
		private PD_Car car = new PD_Car ();
		
		// Arrows
		private List<Gdk.Pixbuf> arrowsPixbuf;
		
		// Scene
		private PD_Scene currentScene;
		private int currentSceneId;

		// Goals
		private List<PD_BoxStatus> goalsToDeliver = new List<PD_BoxStatus> (3);
		private List<PD_BoxStatus> goalsToPickUp = new List<PD_BoxStatus> (3);

		// Demo
		// Draw elements
		// Gtk.DrawingArea drawingArea = new Gtk.DrawingArea ();
		private Gdk.Pixbuf bgPixbuf;
		private Gdk.Pixbuf backgroundPixbuf;
		private Gdk.Pixbuf dialogPixbuf;
		private Gdk.Pixbuf pepe1Pixbuf;
		private Gdk.Pixbuf pepe2Pixbuf;
		private Gdk.Pixbuf bigVanPixbuf;
		int pepeStatus = 0;
		Gdk.GC auxGC;
		Gdk.Pixbuf auxPixbuf1;
		Gdk.Pixbuf auxPixbuf2;
		
		// Layout 
		Pango.Layout auxLayout;
		
		// Animations
		string currentMode = "demo";
		string stringToSay;
		int currentStep = 0;
		int currentCharacter = 0;
		int totalCharacters = 0;
		string [] animationsText = {"¡Bienvenido al ejercicio del repartidor de paquetes!", 
							/*1*/	"Este ejercicio sirve para entrenar tu capacidad de organización. Te vas a convertir en un repartidor de paquetes y tendrás que entregar y recoger paquetes en lugares determinados de la ciudad que aparece abajo.",
							/*2*/	"Tendrás que desplazarte mediante la furgoneta, pulsando en los edificios, pero sólo podrás ir de uno en uno. Por ejemplo, para ir de <span color=\"black\">Correos</span> a la <span color=\"black\">Tienda de Muebles</span> tendrás que pasar por la <span color=\"black\">Tienda de Telefonía</span>.",
							/*3*/	"Ahora quiero que pruebes a desplazarte. Mi furgoneta está en <span color=\"black\">Correos</span>. Quiero que vayas a la <span color=\"black\">Tienda de Muebles</span>. Recuerda que tienes que pasar por la <span color=\"black\">Tienda de Telefonía</span> antes. Pulsa en los edificios para desplazarte.",
							/*4*/	"",
							/*5*/	"Ahora que ya sabes moverte por la ciudad, fíjate en el panel <span color=\"black\">Furgoneta</span> de la derecha. En este panel verás la gasolina y los paquetes que llevas en la furgoneta.",
							/*6*/	"Para evitar gastar mucha gasolina, tienes que planificar la ruta que vas a seguir para entregar y recoger tus paquetes. Intenta gastar la menor gasolina posible.",							
							/*7*/	"Para entregar o recoger un paquete tienes que usar los botones de <span color=\"black\">Entregar</span> y <span color=\"black\">Recoger</span> que aparecen abajo.",
							/*8*/	"Para practicar, intenta entregar la caja amarilla en la <span color=\"black\">Tienda de Muebles</span>. Para ello, pulsa el dibujo de la <span color=\"black\">caja amarilla</span>.",
							/*9*/	"¡Genial! Has entregado una caja, que ahora aparece en la puerta de la tienda en un recuadro rojo. Ahora intenta volver a recogerla pulsando sobre la caja.",
							/*10*/ 	"¡Perfecto! Has recogido la caja y ahora vuelve a estar en la furgoneta. Ten en cuenta que sólo podrás llevar 3 cajas al mismo tiempo. Pulsa en <span color=\"black\">Siguiente</span> para continuar.",
							/*11*/	"Debajo del panel de <span color=\"black\">Furgoneta</span> están las listas de paquetes que tienes que entregar y recoger en el recorrido. ¡Pulsa en <span color=\"black\">Siguiente</span> y comienza el reparto!.",
							/*12*/ 	"Ahora que sabes moverte, entregar y recoger cajas puedes comenzar pulsando el botón de <span color=\"black\">Comenzar ejercicio</span>. Empezarás en la <span color=\"black\">Tienda de Deportes</span>. ¡Sigue las indicaciones de la derecha y recuerda planificar bien tu ruta!"};
		
		int pauseTimer = 200;
		bool firstFrameStep = true;
		bool lastFrameStep = false;
		bool demoLearnedOk = false;
		// This variable is used to blink car in step 3
		int auxCarBlink = 0;
		
		// Timers
		private int auxAnimationTimer;
		private int auxAnimationTimer2;
		private uint auxTimer;
		private GLib.TimeoutHandler currentHandler;
		private uint currentInterval;
		private bool pausedExercise = false;
		private const int REPAINT_SPEED = 50;
		
		public PD_Scene CurrentScene {
			get { return currentScene; }
			set { currentScene = value; }
		}

		public PD_Car Car {
			get { return car; }
			set { car = value; }
		}

		public PanelPackageDelivering ()
		{
			this.Build ();
			// Buttons
//			GtkUtil.SetStyle (buttonDeliver.Child, Configuration.Current.MediumFont);
//			GtkUtil.SetStyle (buttonPickUp.Child, Configuration.Current.MediumFont);
			GtkUtil.SetStyle (buttonFinish.Child, Configuration.Current.MediumFont);
			// Labels
			GtkUtil.SetStyle (labelVanTop, Configuration.Current.SmallFont);
			GtkUtil.SetStyle (labelFuel, Configuration.Current.SmallFont);
			GtkUtil.SetStyle (labelInTheVan, Configuration.Current.SmallFont);
			GtkUtil.SetStyle (labelToPickUpTop, Configuration.Current.SmallFont);
			GtkUtil.SetStyle (labelToDeliver1, Configuration.Current.SmallFont);
			GtkUtil.SetStyle (labelToDeliver2, Configuration.Current.SmallFont);
			GtkUtil.SetStyle (labelToDeliver3, Configuration.Current.SmallFont);
			GtkUtil.SetStyle (labelToDeliverTop, Configuration.Current.SmallFont);
			GtkUtil.SetStyle (labelToPickUp1, Configuration.Current.SmallFont);
			GtkUtil.SetStyle (labelToPickUp2, Configuration.Current.SmallFont);
			GtkUtil.SetStyle (labelToPickUp3, Configuration.Current.SmallFont);
			
			// Frames
			framePickUp.ModifyBg(StateType.Normal, new Gdk.Color(0,0,255) );
			frameDeliver.ModifyBg(StateType.Normal, new Gdk.Color(255,0,0) );
			frameVan.ModifyBg(StateType.Normal, new Gdk.Color(0,0,0) );
			// Instructions buttons
			GtkUtil.SetStyle( buttonGoBack, Configuration.Current.MediumFont );
			GtkUtil.SetStyle( buttonGoForward, Configuration.Current.MediumFont );
			GtkUtil.SetStyle( buttonGoLast, Configuration.Current.MediumFont );
			GtkUtil.SetStyle( buttonStartExercise, Configuration.Current.MediumFont );
					
			this.exer = ExercisePackageDelivering.getInstance();
		}

		public void InitPanel (int sceneId)
		{
			// Scene
			currentSceneId = sceneId;
			currentScene = ExercisePackageDelivering.getInstance().SceneManager.Scenes[currentSceneId];
			
			// Init box images
			for (int i = 0; i < currentScene.BoxesStatus.Length; i++) {
				currentScene.BoxesStatus[i].InitBox ();
			}
			
			// Init drawingareaFixed
			drawingAreaFixed.SetSizeRequest (WIDTH_SCREEN, HEIGHT_SCREEN);
			drawingAreaFixed.ExposeEvent += Expose_Event;
			drawingAreaFixed.AddEvents ((int)Gdk.EventMask.ButtonPressMask);
			eventBoxImage.ButtonPressEvent -= DrawingAreaClicked;
			eventBoxImage.ButtonPressEvent += DrawingAreaClicked;
			
			// Init arrows
			/* arrowsPixbuf = new List<Gdk.Pixbuf>();
			for ( int i = 0; i < 8; i++ ) {
				arrowsPixbuf.Add( new Image( Configuration.CommandExercisesDirectory+"/PackageDelivering/img/arrows/arrow"+i+".png").Pixbuf.ScaleSimple(80,80, Gdk.InterpType.Bilinear) );
			}*/
			
			// Init Pango Layout
			InitLayout();
			
			fondoPixbuf = Gdk.Pixbuf.LoadFromResource ("pesco.ejercicios.PackageDelivering.img.background.png");
			bigVanPixbuf = Gdk.Pixbuf.LoadFromResource ("pesco.ejercicios.PackageDelivering.img.bigvan.png");
			
			auxPixmap = new Gdk.Pixmap (drawingAreaFixed.GdkWindow, WIDTH_SCREEN, HEIGHT_SCREEN, 24);

			auxGC = new Gdk.GC (auxPixmap);
			
			// Set pixmap as background
			drawingAreaFixed.Pixmap = auxPixmap;
			
			// Init positions
			for (int i = 0; i < 16; i++) {
				positions[i] = new PD_Position (FIRST_PLACE_X + ((i % 4) * PLACE_DISTANCE_X), FIRST_PLACE_Y + ((i / 4) * PLACE_DISTANCE_Y));
			}
						
			// Fill top bar
			FillItemsInVan ();
			FillImagesTopBar ();
			
			// Fill goals
			FillGoals ();
			FillGoalsTopBar ();
			
			// Fuel
			UpdateFuelBar();
		}

		private void InitLayout ()
		{			
			auxLayout = new Pango.Layout (this.CreatePangoContext ());
			auxLayout.Width = Pango.Units.FromPixels (720);
			auxLayout.Justify = false;
			auxLayout.Wrap = Pango.WrapMode.Word;
			auxLayout.Alignment = Pango.Alignment.Left;
			auxLayout.FontDescription = Pango.FontDescription.FromString ("Ahafoni CLM Bold 36");		
		}
		
		public void InitGame() {
			
			currentMode = "game";
			car.Position.X = positions[0].X;
			car.Position.Y = positions[0].Y;
			car.CurrentPlace = 0;
			
			this.ShowAll();
			vboxCentral.HeightRequest = 600;
			hboxDemoButtons.HideAll();
			
			if ( currentHandler != null ) {
				GLib.Source.Remove( auxTimer );
			}
			
			ExercisePackageDelivering.getInstance().InitResults();
			
			currentHandler = new GLib.TimeoutHandler (UpdateGame);
			currentInterval = 25;
			auxTimer = GLib.Timeout.Add (currentInterval, currentHandler);
		}
		
		public void InitDemo() {
			
			currentMode = "demo";
			
			dialogPixbuf = Gdk.Pixbuf.LoadFromResource( "pesco.ejercicios.resources.img.dialog.png" );
			pepe1Pixbuf = Gdk.Pixbuf.LoadFromResource( "pesco.ejercicios.PackageDelivering.img.pepe1.png" );
			pepe2Pixbuf = Gdk.Pixbuf.LoadFromResource( "pesco.ejercicios.PackageDelivering.img.pepe2.png" );
			
			frameDeliver.HideAll();
			framePickUp.HideAll();
			frameVan.HideAll();
			
			vboxLateralButtons.HideAll();
			// hboxBottomButtons.HideAll();
			vboxCentral.HeightRequest = 650;
			hboxDemoButtons.ShowAll();
			
			drawingAreaFixed.ShowAll();
									
			currentHandler = new GLib.TimeoutHandler (UpdateDemo);
			currentInterval = 25;
			auxTimer = GLib.Timeout.Add (currentInterval, currentHandler);
		}

		public void UpdateFuelBar() {
			
			if ( ( (double) car.CurrentFuel / (double) CARCONSTS.MAXFUEL) > 0.8 ) {
				imageFuel.Pixbuf = Gdk.Pixbuf.LoadFromResource( "pesco.ejercicios.PackageDelivering.img.fuel.fuel100.png" );
				imageFuel.Pixbuf = imageFuel.Pixbuf.ScaleSimple( imageFuel.Pixbuf.Width, 20, Gdk.InterpType.Bilinear );
			} else if ( ( (double) car.CurrentFuel / (double) CARCONSTS.MAXFUEL) > 0.6 ) {
				imageFuel.Pixbuf = Gdk.Pixbuf.LoadFromResource( "pesco.ejercicios.PackageDelivering.img.fuel.fuel80.png" );
				imageFuel.Pixbuf = imageFuel.Pixbuf.ScaleSimple( imageFuel.Pixbuf.Width, 20, Gdk.InterpType.Bilinear );				
			} else if ( ( (double) car.CurrentFuel / (double) CARCONSTS.MAXFUEL) > 0.4 ) {
				imageFuel.Pixbuf = Gdk.Pixbuf.LoadFromResource( "pesco.ejercicios.PackageDelivering.img.fuel.fuel60.png" );
				imageFuel.Pixbuf = imageFuel.Pixbuf.ScaleSimple( imageFuel.Pixbuf.Width, 20, Gdk.InterpType.Bilinear );				
			} else if ( ( (double) car.CurrentFuel / (double) CARCONSTS.MAXFUEL) > 0.2 ) {
				imageFuel.Pixbuf = Gdk.Pixbuf.LoadFromResource( "pesco.ejercicios.PackageDelivering.img.fuel.fuel40.png" );
				imageFuel.Pixbuf = imageFuel.Pixbuf.ScaleSimple( imageFuel.Pixbuf.Width, 20, Gdk.InterpType.Bilinear );				
			} else if ( ( (double) car.CurrentFuel / (double) CARCONSTS.MAXFUEL) > 0.0 ) {
				imageFuel.Pixbuf = Gdk.Pixbuf.LoadFromResource( "pesco.ejercicios.PackageDelivering.img.fuel.fuel20.png" );
				imageFuel.Pixbuf = imageFuel.Pixbuf.ScaleSimple( imageFuel.Pixbuf.Width, 20, Gdk.InterpType.Bilinear );				
			} else {
				imageFuel.Pixbuf = Gdk.Pixbuf.LoadFromResource( "pesco.ejercicios.PackageDelivering.img.fuel.fuel0.png" );
				imageFuel.Pixbuf = imageFuel.Pixbuf.ScaleSimple( imageFuel.Pixbuf.Width, 20, Gdk.InterpType.Bilinear );
			}
		}
		
		public void FillItemsInVan ()
		{
			
			car.Items.Clear ();
			for (int i = 0; i < currentScene.BoxesStatus.Length; i++) {
				if (currentScene.BoxesStatus[i].Status == "van") {
					car.Items.Add (CurrentScene.BoxesStatus[i].IdBox);
				}
			}
		}

		// Fill images in top bar
		public void FillImagesTopBar ()
		{
			
			Gdk.Pixbuf bg = Gdk.Pixbuf.LoadFromResource ("pesco.ejercicios.PackageDelivering.img.box.boxborder.png");

			// Generating a new table
			Gtk.Table auxTable = new Table (1, 3, true);
			auxTable.Attach (new Image (bg), 0, 1, 0, 1);
			auxTable.Attach (new Image (bg), 1, 2, 0, 1);
			auxTable.Attach (new Image (bg), 2, 3, 0, 1);
			for (int i = 0; i < car.Items.Count; i++) {
				if (i == 0) {
					Gtk.Image auxImage1 = new Image (currentScene.BoxesStatus[car.Items[i]].PixbufBoxSmall.ScaleSimple(39,39,Gdk.InterpType.Bilinear));
					Gtk.Button auxButton1 = new Gtk.Button( auxImage1 );
					auxButton1.Name = currentScene.BoxesStatus[car.Items[i]].IdBox.ToString();
					auxButton1.Clicked += DeliverCallbackTopBar;
					auxTable.Attach ( auxButton1, 0, 1, 0, 1 );					
				} else if (i == 1) {
					Gtk.Image auxImage2 = new Image (currentScene.BoxesStatus[car.Items[i]].PixbufBoxSmall.ScaleSimple(39,39,Gdk.InterpType.Bilinear));
					Gtk.Button auxButton2 = new Gtk.Button( auxImage2 );
					auxButton2.Name = currentScene.BoxesStatus[car.Items[i]].IdBox.ToString();
					auxButton2.Clicked += DeliverCallbackTopBar;		
					auxTable.Attach ( auxButton2, 1, 2, 0, 1 );
				} else if (i == 2) {
					Gtk.Image auxImage3 = new Image (currentScene.BoxesStatus[car.Items[i]].PixbufBoxSmall.ScaleSimple(39,39,Gdk.InterpType.Bilinear));
					Gtk.Button auxButton3 = new Gtk.Button( auxImage3 );
					auxButton3.Name = currentScene.BoxesStatus[car.Items[i]].IdBox.ToString();
					auxButton3.Clicked += DeliverCallbackTopBar;					
					auxTable.Attach ( auxButton3, 2, 3, 0, 1);
				}
			}
			if (hboxTable.Children.Length > 0) {
				hboxTable.Children[0].Destroy ();
			}
			hboxTable.PackStart (auxTable);
			hboxTable.ShowAll ();
		}

		// Fill goals in current panel		
		public void FillGoals ()
		{
			for (int i = 0; i < currentScene.BoxesStatus.Length; i++) {
				if (currentScene.BoxesStatus[i].Status == "van") {
					goalsToDeliver.Add (currentScene.BoxesStatus[i]);
				} else if (currentScene.BoxesStatus[i].Status == "shop") {					
					goalsToPickUp.Add (currentScene.BoxesStatus[i]);
				}
			}
		}

		public void FillGoalsTopBar ()
		{
			string strike = "false";
			for (int i = 0; i < goalsToDeliver.Count; i++) {
				if (i == 0) {
					if ( currentScene.BoxesStatus[3+i].CurrentPosition == currentScene.BoxesStatus[3+i].GoalPosition ) {
						strike = "true";
					} else {
						strike = "false";
					}
					labelToDeliver1.Markup = "<span strikethrough=\""+strike+"\">- La " + goalsToDeliver[i].BoxName + " en " + ExercisePackageDelivering.getInstance ().PlacesManager.Places[goalsToDeliver[i].GoalPosition].Text+"</span>";
				}
				if (i == 1) {
					if ( currentScene.BoxesStatus[3+i].CurrentPosition == currentScene.BoxesStatus[3+i].GoalPosition ) {
						strike = "true";
					} else {
						strike = "false";
					}
					labelToDeliver2.Markup = "<span strikethrough=\""+strike+"\">- La " + goalsToDeliver[i].BoxName + " en " + ExercisePackageDelivering.getInstance ().PlacesManager.Places[goalsToDeliver[i].GoalPosition].Text+"</span>";
				}
				if (i == 2) {
					if ( currentScene.BoxesStatus[3+i].CurrentPosition == currentScene.BoxesStatus[3+i].GoalPosition ) {
						strike = "true";
					} else {
						strike = "false";
					}
					labelToDeliver3.Markup = "<span strikethrough=\""+strike+"\">- La " + goalsToDeliver[i].BoxName + " en " + ExercisePackageDelivering.getInstance ().PlacesManager.Places[goalsToDeliver[i].GoalPosition].Text+"</span>";
				}
			}
			
			for (int i = 0; i < goalsToPickUp.Count; i++) {
				if (i == 0) {
					if (currentScene.BoxesStatus[i].GoalPosition == currentScene.BoxesStatus[i].CurrentPosition ) {
						strike = "true";
					} else {
						strike = "false";
					}
					labelToPickUp1.Markup = "<span strikethrough=\""+strike+"\">- La " + goalsToPickUp[i].BoxName + " " + "en " + ExercisePackageDelivering.getInstance ().PlacesManager.Places[goalsToPickUp[i].InitialPosition].Text+"</span>";
				}
				if (i == 1) {
					if (currentScene.BoxesStatus[i].GoalPosition == currentScene.BoxesStatus[i].CurrentPosition ) {
						strike = "true";
					} else {
						strike = "false";
					}
					labelToPickUp2.Markup = "<span strikethrough=\""+strike+"\">- La " + goalsToPickUp[i].BoxName + " " + "en " + ExercisePackageDelivering.getInstance ().PlacesManager.Places[goalsToPickUp[i].InitialPosition].Text+"</span>";
				}
				if (i == 2) {
					if (currentScene.BoxesStatus[i].GoalPosition == currentScene.BoxesStatus[i].CurrentPosition ) {
						strike = "true";
					} else {
						strike = "false";
					}
					labelToPickUp3.Markup = "<span strikethrough=\""+strike+"\">- La " + goalsToPickUp[i].BoxName + " " + "en " + ExercisePackageDelivering.getInstance ().PlacesManager.Places[goalsToPickUp[i].InitialPosition].Text+"</span>";
				}
			}
			
		}

		public bool UpdateDemo () {
			car.UpdatePosition();
			drawingAreaFixed.QueueDraw ();
			return true;
		}
		
		public bool UpdateGame ()
		{			
			if (car.UpdatePosition () == false) {
				EnableButtons ();
			}
			
			drawingAreaFixed.QueueDraw ();
			
			return true;			
		}

		void Expose_Event (object obj, ExposeEventArgs args)
		{				
			// If we are in demo mode, call demo function
			if ( currentMode == "demo" ) {
				DrawDemo();		
			} else {
				DrawGame();	
			}
		}
		
		#region Drawing functions
		public void DrawGame() {
			DrawBackground();
			DrawBoxes();
			DrawCar();
			if ( car.State == CARCONSTS.STOPPED ) {
				DrawArrows();
				DrawBoxesInAvalaiblePlaces();
			}
		}
		
		public void DrawBoxes() {
		
			// Boxes over places
			for (int i = 0; i < currentScene.BoxesStatus.Length; i++) {
				if (currentScene.BoxesStatus[i].Status == "shop") {					
					auxPixmap.DrawPixbuf (drawingAreaFixed.Style.BaseGC (StateType.Normal), currentScene.BoxesStatus[i].PixbufBoxSmall, 0, 0, positions[currentScene.BoxesStatus[i].CurrentPosition].X + BOX_WIDTH, positions[currentScene.BoxesStatus[i].CurrentPosition].Y + BOX_HEIGHT / 2, BOX_WIDTH, BOX_HEIGHT, 0, 0,
					0);
					// If we are in a place with a box and car is stopped, draw a red rectangle around the box
					if ( Car.CurrentPlace == currentScene.BoxesStatus[i].CurrentPosition && car.State == CARCONSTS.STOPPED ) {
						Gdk.GC boxGC = new Gdk.GC( auxPixmap );
						boxGC.SetLineAttributes( 5, Gdk.LineStyle.Solid, Gdk.CapStyle.Round, Gdk.JoinStyle.Round );
						boxGC.RgbFgColor = new Gdk.Color( 255, 0, 0 );
						int offsetX = 0;
						// Places 13 and 14 have a little offset 
						offsetX = 0;
						if ( i == 13 || i == 14 ) {
							offsetX = -35;	
						}					
						auxPixmap.DrawRectangle( boxGC, false, new Gdk.Rectangle( positions[currentScene.BoxesStatus[i].CurrentPosition].X + BOX_WIDTH - 2, positions[currentScene.BoxesStatus[i].CurrentPosition].Y + BOX_HEIGHT / 2 - 2, BOX_WIDTH + 4, BOX_HEIGHT + 4 ) );
					}
				}
			}
			
		}
		
		public void DrawDemo() {
						
			// Hello
			if (currentStep == 0) {
				if ( FirstFrameStep() ) {
					currentCharacter = 0;
					stringToSay = animationsText[currentStep];
					totalCharacters = animationsText[currentStep].Length;
					buttonGoBack.Sensitive = false;
					buttonGoLast.HideAll();
					buttonStartExercise.HideAll();
					frameVan.HideAll();
					framePickUp.HideAll();
					frameDeliver.HideAll();
					hboxGameButtons.HideAll();					
				}
				if ( LastFrameStep() ) {
					buttonGoBack.Sensitive = true;
				}
				Gdk.Rectangle auxRectangle = new Gdk.Rectangle(0,0,WIDTH_SCREEN,HEIGHT_SCREEN);
				auxPixmap.DrawRectangle( drawingAreaFixed.Style.WhiteGC, true, auxRectangle );
				auxLayout.SetMarkup ("<span font_desc=\"Ahafoni CLM Bold 20\" color=\"blue\">" + stringToSay.Substring (0, currentCharacter) + "</span>");
				PepeUtils.IncrementCharacterDialog( ref currentCharacter, ref pepeStatus,  stringToSay );
				DrawBigVan();
				DrawDialog ();				
			// Explanation 1
			} else if ( currentStep == 1 ) {
				if ( FirstFrameStep() ) {
					buttonGoBack.Sensitive = true;
					currentCharacter = 0;
					totalCharacters = animationsText[currentStep].Length;
					stringToSay = animationsText[currentStep];
				}
				if ( AfterLastFrameStep() ) {
					DrawBackground();
				}
				auxLayout.SetMarkup ("<span font_desc=\"Ahafoni CLM Bold 20\" color=\"blue\">" + stringToSay.Substring (0, currentCharacter) + "</span>");
				PepeUtils.IncrementCharacterDialog( ref currentCharacter, ref pepeStatus,  stringToSay );
				DrawBigVan();
				DrawDialog();
			} // Explanation movement in the city
			else if ( currentStep == 2 ) {
				if ( FirstFrameStep() ) {
				 	car.CurrentPlace = 15;
					car.Position.X = positions[15].X;
					car.Position.Y = positions[15].Y;
					currentCharacter = 0;
					totalCharacters = animationsText[currentStep].Length;
					buttonGoForward.Sensitive = true;
					stringToSay = animationsText[currentStep];
				}		
				DrawBackground();
				auxLayout.SetMarkup ("<span font_desc=\"Ahafoni CLM Bold 20\" color=\"blue\">" + stringToSay.Substring (0, currentCharacter) + "</span>");
				PepeUtils.IncrementCharacterDialog( ref currentCharacter, ref pepeStatus,  stringToSay );
				DrawDialog();	
			} 
			// Learning text
			else if ( currentStep == 3 ) {
				if ( FirstFrameStep() ) {
					currentCharacter = 0;
					totalCharacters = animationsText[currentStep].Length;
					buttonGoForward.Sensitive = false;
					stringToSay = animationsText[currentStep];			
				}
				DrawBackground();				
				auxLayout.SetMarkup ("<span font_desc=\"Ahafoni CLM Bold 20\" color=\"blue\">" + stringToSay.Substring (0, currentCharacter) + "</span>");
				PepeUtils.IncrementCharacterDialog( ref currentCharacter, ref pepeStatus,  stringToSay );
				DrawBoxesInAvalaiblePlaces();
				DrawCar();	
				DrawDialog();				
				if ( LastFrameStep() ) {
					NextStep();
				}
			} // Learning to drive...
			else if ( currentStep == 4 ) {
				if ( FirstFrameStep() ) { 
					NextStep();
					stringToSay = animationsText[currentStep-1];
					totalCharacters = animationsText[currentStep-1].Length;
					currentCharacter = totalCharacters;
					buttonGoBack.Sensitive = false;
				}
				DrawBackground();
				auxLayout.SetMarkup ("<span font_desc=\"Ahafoni CLM Bold 20\" color=\"blue\">" + stringToSay.Substring(0, currentCharacter) + "</span>");
				PepeUtils.IncrementCharacterDialog( ref currentCharacter, ref pepeStatus,  stringToSay );
				DrawBoxesInAvalaiblePlaces();
				DrawCar();
				DrawDialog();				
			}
			// Showing fuel
			else if ( currentStep == 5 ) {
				if ( FirstFrameStep() ) { 
					stringToSay = animationsText[currentStep];
					totalCharacters = animationsText[currentStep].Length;
					currentCharacter = 0;
					frameVan.ShowAll();
				}
				DrawBackground();
				auxLayout.SetMarkup ("<span font_desc=\"Ahafoni CLM Bold 20\" color=\"blue\">" + stringToSay.Substring(0, currentCharacter) + "</span>");				
				PepeUtils.IncrementCharacterDialog( ref currentCharacter, ref pepeStatus,  stringToSay );
				DrawDialog();		
				DrawCar();
			}
			else if ( currentStep == 6 ) {
				if ( FirstFrameStep() ) { 
					stringToSay = animationsText[currentStep];
					totalCharacters = animationsText[currentStep].Length;
					currentCharacter = 0;
				}
				DrawBackground();
				auxLayout.SetMarkup ("<span font_desc=\"Ahafoni CLM Bold 20\" color=\"blue\">" + stringToSay.Substring(0, currentCharacter) + "</span>");				
				PepeUtils.IncrementCharacterDialog( ref currentCharacter, ref pepeStatus,  stringToSay );
				DrawDialog();
				DrawCar();
			}
			// Showing buttons to pick up and to deliver
			else if ( currentStep == 7 ) {
				if ( FirstFrameStep() ) { 
					NextStep();
					stringToSay = animationsText[currentStep];
					totalCharacters = animationsText[currentStep].Length;
					currentCharacter = 0;
					// hboxBottomButtons.ShowAll();
//					buttonPickUp.Sensitive = false;
//					buttonDeliver.Sensitive = false;
					buttonFinish.Sensitive = false;
				}
				DrawBackground();
				auxLayout.SetMarkup ("<span font_desc=\"Ahafoni CLM Bold 20\" color=\"blue\">" + stringToSay.Substring(0, currentCharacter) + "</span>");				
				PepeUtils.IncrementCharacterDialog( ref currentCharacter, ref pepeStatus,  stringToSay );
				DrawDialog();
				DrawCar();
			} 
			// Learning to deliver
			else if ( currentStep == 8 ) {
				if ( FirstFrameStep() ) { 
					stringToSay = animationsText[currentStep];
					totalCharacters = animationsText[currentStep].Length;
					currentCharacter = 0;
//					buttonDeliver.Sensitive = true;
					buttonGoForward.Sensitive = false;
				}
				DrawBackground();
				auxLayout.SetMarkup ("<span font_desc=\"Ahafoni CLM Bold 20\" color=\"blue\">" + stringToSay.Substring(0, currentCharacter) + "</span>");				
				PepeUtils.IncrementCharacterDialog( ref currentCharacter, ref pepeStatus,  stringToSay );								
				DrawBoxes();
				DrawDialog();				
				DrawCar();
			} 
			// Learning to pick up
			else if ( currentStep == 9 ) {
				if ( FirstFrameStep() ) { 
					stringToSay = animationsText[currentStep];
					totalCharacters = animationsText[currentStep].Length;
					currentCharacter = 0;
//					buttonDeliver.Sensitive = false;
//					buttonPickUp.Sensitive = true;
				}
				DrawBackground();
				auxLayout.SetMarkup ("<span font_desc=\"Ahafoni CLM Bold 20\" color=\"blue\">" + stringToSay.Substring(0, currentCharacter) + "</span>");				
				PepeUtils.IncrementCharacterDialog( ref currentCharacter, ref pepeStatus,  stringToSay );
				DrawBoxes();
				DrawDialog();
				DrawCar();
			} 			 
			// Showing tasks
			else if ( currentStep == 10 ) {
				if ( FirstFrameStep() ) { 
					stringToSay = animationsText[currentStep];
					totalCharacters = animationsText[currentStep].Length;
					currentCharacter = 0;
					frameDeliver.ShowAll();
					framePickUp.ShowAll();
					buttonGoBack.Sensitive = false;
					buttonGoForward.Sensitive = true;
				}
				DrawBackground();
				auxLayout.SetMarkup ("<span font_desc=\"Ahafoni CLM Bold 20\" color=\"blue\">" + stringToSay.Substring(0, currentCharacter) + "</span>");				
				PepeUtils.IncrementCharacterDialog( ref currentCharacter, ref pepeStatus,  stringToSay );
				DrawDialog();
				DrawCar();
			} 
			// Pick up and deliver learned
			else if ( currentStep == 11 ) {
				if ( FirstFrameStep() ) { 
					stringToSay = animationsText[currentStep];
					totalCharacters = animationsText[currentStep].Length;
					currentCharacter = 0;
					// buttonDeliver.Sensitive = false;
					// buttonPickUp.Sensitive = false;
					buttonGoBack.Sensitive = false;
				}
				DrawBackground();
				auxLayout.SetMarkup ("<span font_desc=\"Ahafoni CLM Bold 20\" color=\"blue\">" + stringToSay.Substring(0, currentCharacter) + "</span>");				
				PepeUtils.IncrementCharacterDialog( ref currentCharacter, ref pepeStatus,  stringToSay );
				DrawBoxes();
				DrawDialog();
				DrawCar();
			} 
			// Last!
			else if ( currentStep == 12 ) {
				if ( FirstFrameStep() ) { 
					stringToSay = animationsText[currentStep];
					totalCharacters = animationsText[currentStep].Length;
					currentCharacter = 0;
					buttonGoForward.Label = "Comenzar ejercicio";
					GtkUtil.SetStyle( buttonGoForward, Configuration.Current.MediumFont );
					buttonGoForward.HideAll();
					buttonGoBack.HideAll();
					buttonStartExercise.ShowAll();
					PepeUtils.GenerateCartel( this.PangoContext, auxGC, auxPixmap, "¡Ensayo superado! Comienza el ejercicio" );
				}
				DrawBackground();
				auxLayout.SetMarkup ("<span font_desc=\"Ahafoni CLM Bold 20\" color=\"blue\">" + stringToSay.Substring(0, currentCharacter) + "</span>");				
				// PepeUtils.IncrementCharacterDialog( ref currentCharacter, ref pepeStatus,  stringToSay );
				// DrawBoxes();
				// DrawDialog();
				// DrawCar();
				PepeUtils.DrawCartel();
			}
			
		}
		
		private void DrawBigVan() {
			// auxPixmap.DrawPixbuf (drawingAreaFixed.Style.BaseGC (StateType.Normal), bigVanPixbuf, 0, 0, 0, HEIGHT_SCREEN - bigVanPixbuf.Height, bigVanPixbuf.Width, bigVanPixbuf.Height, 0, 0,
			//	0);
			drawingAreaFixed.Pixmap.DrawPixbuf( drawingAreaFixed.Style.BaseGC (StateType.Normal), bigVanPixbuf, 0, 0, 0, HEIGHT_SCREEN - bigVanPixbuf.Height, bigVanPixbuf.Width, bigVanPixbuf.Height, 0, 0,
				0);
		}
		
		private void DrawBackground() {
		
			auxPixmap.DrawPixbuf (drawingAreaFixed.Style.BaseGC (StateType.Normal), fondoPixbuf, 0, 0, 0, 0, WIDTH_SCREEN, HEIGHT_SCREEN, 0, 0,
				0);
			
			// Places
			for (int i = 0; i < 16; i++) {
				if ( i == 13 || i ==  14 ) {
					auxPixmap.DrawPixbuf (drawingAreaFixed.Style.BaseGC (StateType.Normal), ExercisePackageDelivering.getInstance ().PlacesManager.Places[i].PixbufLugar, 0, 0, (positions[i].X - PLACE_WIDTH / 2) - 35, positions[i].Y - PLACE_HEIGHT/2 + 10, PLACE_WIDTH, PLACE_HEIGHT, 0, 0,
					0);
				} else {
					auxPixmap.DrawPixbuf (drawingAreaFixed.Style.BaseGC (StateType.Normal), ExercisePackageDelivering.getInstance ().PlacesManager.Places[i].PixbufLugar, 0, 0, (positions[i].X - PLACE_WIDTH / 2), positions[i].Y - PLACE_HEIGHT/2 + 10, PLACE_WIDTH, PLACE_HEIGHT, 0, 0,
				0);	
				}
			}				
		}
		
		private void DrawCar() {
			// Car
			auxPixmap.DrawPixbuf (drawingAreaFixed.Style.BaseGC (StateType.Normal), car.CurrentCar, 0, 0, car.Position.X - (car.WIDTH / 2)- 11, car.Position.Y - 10, car.WIDTH, car.HEIGHT, 0, 0,
			0);		
		}
		
		private void DrawDialog ()
		{
			
			auxPixmap.DrawPixbuf (auxGC, dialogPixbuf, 0, 0, 0, 0, WIDTH_SCREEN, HEIGHT_SCREEN, 0, 0,
			0);
			if (pepeStatus > 2) {
				auxPixmap.DrawPixbuf (auxGC, pepe1Pixbuf, 0, 0, 0, HEIGHT_SCREEN - pepe1Pixbuf.Height, pepe1Pixbuf.Width, pepe1Pixbuf.Height, 0, 0,
				0);
			} else {
				auxPixmap.DrawPixbuf (auxGC, pepe2Pixbuf, 0, 0, 0, HEIGHT_SCREEN - pepe2Pixbuf.Height, pepe2Pixbuf.Width, pepe2Pixbuf.Height, 0, 0,
				0);
			}	
			// Draw text
			auxPixmap.DrawLayout (auxGC, 200, 38, auxLayout);
		}
		
		private void DrawArrows () {
			
				/*
				int arrowDistanceX = 120;
				int arrowDistanceY = 90;
				// NW Arrow
				if ( (car.CurrentPlace / 4) > 0 && ( car.CurrentPlace % 4 ) > 0 ) {
					if ( CurrentScene.Conections[car.CurrentPlace][(car.CurrentPlace-5)] == 1 ) {
						auxPixmap.DrawPixbuf( auxGC, arrowsPixbuf[0], 0, 0, car.Position.X - (car.WIDTH / 2) - arrowDistanceX, 
					                     car.Position.Y - arrowDistanceY, arrowsPixbuf[0].Width,
			                     		arrowsPixbuf[0].Height, 0, 0, 0 );
				}					                                               
				} // N arrow
				if ( (car.CurrentPlace / 4) > 0 ) {
					if ( CurrentScene.Conections[car.CurrentPlace][(car.CurrentPlace-4)] == 1 ) {
						auxPixmap.DrawPixbuf( auxGC, arrowsPixbuf[1], 0, 0, car.Position.X - (car.WIDTH / 2), 
					                     	car.Position.Y - arrowDistanceY, arrowsPixbuf[1].Width,
						                     arrowsPixbuf[1].Height, 0, 0, 0 );
					}					                                               
				} // NE
				if ( (car.CurrentPlace / 4) > 0 &&  (car.CurrentPlace % 4) < 3 ) {
					if ( CurrentScene.Conections[car.CurrentPlace][(car.CurrentPlace-3)] == 1 ) {
						auxPixmap.DrawPixbuf( auxGC, arrowsPixbuf[2], 0, 0, car.Position.X - (car.WIDTH / 2) + arrowDistanceX, 
					                     	car.Position.Y - arrowDistanceY, arrowsPixbuf[2].Width,
						                     arrowsPixbuf[2].Height, 0, 0, 0 );
					}					                                               			
				} // E
				if ( (car.CurrentPlace % 4) < 3 ) {
					if ( CurrentScene.Conections[car.CurrentPlace][(car.CurrentPlace+1)] == 1 ) {
						auxPixmap.DrawPixbuf( auxGC, arrowsPixbuf[3], 0, 0, car.Position.X - (car.WIDTH / 2) + arrowDistanceX, 
					                     	car.Position.Y - (car.HEIGHT / 2), arrowsPixbuf[3].Width,
						                     arrowsPixbuf[3].Height, 0, 0, 0 );
					}	                                               			
				}
				// SE
				if ( (car.CurrentPlace / 4) < 3 && (car.CurrentPlace % 4) < 3 ) {
					if ( CurrentScene.Conections[car.CurrentPlace][(car.CurrentPlace+5)] == 1 ) {
						auxPixmap.DrawPixbuf( auxGC, arrowsPixbuf[4], 0, 0, car.Position.X - (car.WIDTH / 2) + arrowDistanceX, 
					                     	car.Position.Y + arrowDistanceY, 
					                     	arrowsPixbuf[4].Width, arrowsPixbuf[4].Height, 0, 0, 0 );
					}	                                               			
				}
				// S
				if ( (car.CurrentPlace / 4) < 3 ) {
					if ( CurrentScene.Conections[car.CurrentPlace][(car.CurrentPlace+4)] == 1 ) {
						auxPixmap.DrawPixbuf( auxGC, arrowsPixbuf[5], 0, 0, car.Position.X - (car.WIDTH / 2), 
					                     	car.Position.Y + arrowDistanceY, 
					                     	arrowsPixbuf[5].Width, arrowsPixbuf[5].Height, 0, 0, 0 );
					}
				}
				// SW
				if ( (car.CurrentPlace / 4) < 3 && (car.CurrentPlace % 4) > 0 ) {
					if ( CurrentScene.Conections[car.CurrentPlace][(car.CurrentPlace+3)] == 1 ) {
						auxPixmap.DrawPixbuf( auxGC, arrowsPixbuf[6], 0, 0, car.Position.X - (car.WIDTH / 2) - arrowDistanceX,
					                     	car.Position.Y + arrowDistanceY, 
					                     	arrowsPixbuf[6].Width, arrowsPixbuf[6].Height, 0, 0, 0 );
					}
				}
				// W
				if ( (car.CurrentPlace % 4) > 0 ) {
					if ( CurrentScene.Conections[car.CurrentPlace][(car.CurrentPlace-1)] == 1 ) {
						auxPixmap.DrawPixbuf( auxGC, arrowsPixbuf[7], 0, 0, car.Position.X - (car.WIDTH / 2) - arrowDistanceX, 
					                     	car.Position.Y, 
					                     	arrowsPixbuf[7].Width, arrowsPixbuf[7].Height, 0, 0, 0 );
					}					                                               			
				}
			
		
				*/
		}
		
		private void DrawBoxesInAvalaiblePlaces() {
						
			if ( currentMode == "demo" && demoLearnedOk == true ) {
				return ;	
			}
			
			int boxWidth = 115;
			int boxHeight = 95;
			// Set line style
			Gdk.GC boxGC = new Gdk.GC( auxPixmap );
			boxGC.SetLineAttributes( 7, Gdk.LineStyle.Solid, Gdk.CapStyle.Round, Gdk.JoinStyle.Round );
			boxGC.RgbFgColor = new Gdk.Color( 255, 0, 0 );
			int offsetX = 0;
			// Iterate over places. If place is connected with current place, draw a box around it
			for ( int i = 0; i < CurrentScene.Places.Length; i++ ) {			
				if ( CurrentScene.Conections[Car.CurrentPlace][i] == 1 ) {
					// Places 13 and 14 have a little offset 
					offsetX = 0;
					if ( i == 13 || i == 14 ) {
						offsetX = -35;	
					}					
					auxPixmap.DrawRectangle( boxGC, false, new Gdk.Rectangle( positions[i].X - boxWidth/2 + offsetX, positions[i].Y - (boxHeight/2)+8, boxWidth, boxHeight ) );
				}				
			}
			
		}
		
		#endregion
		private void IncrementCharacterDialog() {
			
			if ( currentCharacter < totalCharacters - 2 ) {
				if ( stringToSay[currentCharacter] == '<' && stringToSay[currentCharacter+1] == 's') {
					while ( stringToSay[currentCharacter] != 'n' || stringToSay[currentCharacter+1] != '>' ) {
						currentCharacter++;
					}
				}
			}
			if (currentCharacter < totalCharacters) {
				currentCharacter++;
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
		
		private void DrawingAreaClicked (object o, ButtonPressEventArgs evnt)
		{
					
			double auxX = evnt.Event.X;
			double auxY = evnt.Event.Y;
					
			int auxColumna = 0;
			int auxFila = 0;
			
			if (auxX >= 0 && auxX < 260) {
				auxColumna = 0;
			} else if (auxX >= 260 && auxX < 520) {
				auxColumna = 1;
			} else if (auxX >= 520 && auxX < 780) {
				auxColumna = 2;
			} else if (auxX >= 780 && auxX < 998) {
				auxColumna = 3;
			}
			
			int auxFIRST_Y = 80; // FIRST_PLACE_Y - PLACE_WIDTH / 2
			int auxOFFSET_Y = PLACE_DISTANCE_Y;

			if (auxY >= auxFIRST_Y && auxY < auxFIRST_Y + auxOFFSET_Y * 1 ) {
				auxFila = 0;
			} else if (auxY >= auxFIRST_Y + (auxOFFSET_Y * 1)  && auxY < auxFIRST_Y + (auxOFFSET_Y * 2) ) {
				auxFila = 1;
			} else if (auxY >= auxFIRST_Y + (auxOFFSET_Y * 2)  && auxY < auxFIRST_Y + (auxOFFSET_Y * 3) ) {
				auxFila = 2;
			} else if (auxY >= auxFIRST_Y + (auxOFFSET_Y * 3)  && auxY < auxFIRST_Y + (auxOFFSET_Y * 4) ) {
				auxFila = 3;
			}
			
			int destination = (auxFila * 4) + auxColumna;
			
			if ( currentMode == "game" ) {
				ClickInGame( destination );
			} else {
				ClickInDemo( destination );	
			}
		}

		private void ClickInGame(int destination) {

			// If we click over current place, we will pick the box in the place, (if there is one)
			if ( destination == car.CurrentPlace ) {			
				bool boxFound = false;
				bool spaceInVan = true;
				for (int i = 0; i < currentScene.BoxesStatus.Length && boxFound == false && spaceInVan == true; i++) {
					// Console.WriteLine("Test! Caja en "+currentScene.BoxesStatus[i].CurrentPosition+" vs "+ car.CurrentPlace);
					if ( currentScene.BoxesStatus[i].CurrentPosition == car.CurrentPlace) {
						boxFound = true;
						if (car.Items.Count >= 3) {
							exer.Results.AddAction( new PD_ResultsAction("RECOGIDA", "ERROR", currentScene.BoxesStatus[i].BoxName, "Furgoneta llena.") );
							Gtk.MessageDialog dialog = new Gtk.MessageDialog (null, Gtk.DialogFlags.Modal, Gtk.MessageType.Warning, Gtk.ButtonsType.Ok, true, "<span size=\"xx-large\">Ya tienes 3 paquetes en la furgoneta. Intenta entregar alguno antes de coger uno nuevo.</span>", "Ya tienes 3 paquetes");
							GtkUtil.SetStyleRecursive( dialog, Configuration.Current.LargeFont );						
							this.Sensitive = false;
							int result = dialog.Run ();
							dialog.Destroy ();
							this.Sensitive = true;
							spaceInVan = false;
						} else {
							currentScene.BoxesStatus[i].Status = "van";
							currentScene.BoxesStatus[i].CurrentPosition = -1;
							car.Items.Add (i);
							if ( currentMode == "game" ) {
								exer.Results.AddAction( new PD_ResultsAction( "RECOGIDA", "OK", currentScene.BoxesStatus[i].BoxName, "" ) );
								FillGoalsTopBar();
							} else if ( currentMode == "demo" ) {
								NextStep();
							}
							FillImagesTopBar ();
						}
					}
				}
				if (boxFound == false) {
				}
				
			} 
			
			if (currentScene.Conections[car.CurrentPlace][destination] == 0) {
				// exer.Currentexer.Results.AddAction( new PDResultsAction("MOVIMIENTO", "ERROR", exer.PlacesManager.Places[car.CurrentPlace].Name, exer.PlacesManager.Places[destination].Name) );
			} else {
				if (car.State != CARCONSTS.MOVING) {
					exer.Results.AddAction( new PD_ResultsAction("MOVIMIENTO", "OK", exer.PlacesManager.Places[car.CurrentPlace].Name, exer.PlacesManager.Places[destination].Name ) );
					car.MoveTo (destination, positions[destination]);
					UpdateFuelBar();
					DisableButtons ();
				}
			}
			
		}
		
		private void Say( string toSay ) {			
			stringToSay = toSay;
			currentCharacter = 0;
			totalCharacters = toSay.Length;
		}
		
		private void ClickInDemo( int destination ) {
		
			// Console.WriteLine( "CurrentStep: "+currentStep+" | Destination: "+destination+" | Current Place: "+car.CurrentPlace);
			if ( ( currentStep == 3 || currentStep == 4 ) && Car.State != CARCONSTS.MOVING ) {
				if ( car.CurrentPlace == 15 && destination == 11 ) {					
					Say( "¡Genial! Ya estás más cerca de la <span color=\"black\">Tienda de muebles</span>" ); 
					car.MoveTo (destination, positions[destination]);
				} else if ( car.CurrentPlace == 15 && destination != 11 ) {
					Say( "Recuerda que tienes que pasar por la <span color=\"black\">Tienda de telefonía</span> antes de ir a la <span color=\"black\">Tienda de muebles</span>" ); 
				} else if ( car.CurrentPlace == 11 && destination == 10 ) {
					Say( "¡Perfecto! ¡Has llegado a la <span color=\"black\">Tienda de muebles</span>! Ahora pulsa en <span color=\"black\">Siguiente</span> para continuar con las instrucciones del ejercicio." );
					car.MoveTo (destination, positions[destination]);
					buttonGoBack.Sensitive = false;
					buttonGoForward.Sensitive = true;
					demoLearnedOk = true;
				} else if ( car.CurrentPlace == 11 && destination != 10 ) {				
					Say( "Recuerda que tu destino es la tienda de muebles." );
				}
			}
			
			if ( destination == car.CurrentPlace && currentStep == 9 ) {			
				bool boxFound = false;
				bool spaceInVan = true;
				for (int i = 0; i < currentScene.BoxesStatus.Length && boxFound == false && spaceInVan == true; i++) {
					// Console.WriteLine("Test! Caja en "+currentScene.BoxesStatus[i].CurrentPosition+" vs "+ car.CurrentPlace);
					if ( currentScene.BoxesStatus[i].CurrentPosition == car.CurrentPlace) {
						boxFound = true;
						if (car.Items.Count >= 3) {
							exer.Results.AddAction( new PD_ResultsAction("RECOGIDA", "ERROR", currentScene.BoxesStatus[i].BoxName, "Furgoneta llena.") );
							Gtk.MessageDialog dialog = new Gtk.MessageDialog (null, Gtk.DialogFlags.Modal, Gtk.MessageType.Warning, Gtk.ButtonsType.Ok, true, "<span size=\"xx-large\">Ya tienes 3 paquetes en la furgoneta. Intenta entregar alguno antes de coger uno nuevo.</span>", "Ya tienes 3 paquetes");
							GtkUtil.SetStyleRecursive( dialog, Configuration.Current.LargeFont );						
							this.Sensitive = false;
							int result = dialog.Run ();
							dialog.Destroy ();
							this.Sensitive = true;
							spaceInVan = false;
						} else {
							currentScene.BoxesStatus[i].Status = "van";
							currentScene.BoxesStatus[i].CurrentPosition = -1;
							car.Items.Add (i);
							if ( currentMode == "game" ) {
								exer.Results.AddAction( new PD_ResultsAction( "RECOGIDA", "OK", currentScene.BoxesStatus[i].BoxName, "" ) );
								FillGoalsTopBar();
							} else if ( currentMode == "demo" ) {
								NextStep();
							}
							FillImagesTopBar ();
						}
					}
				}
				if (boxFound == false) {
				}
				
			} 			
			
		}
		
		public void DisableButtons ()
		{
			
//			buttonDeliver.Sensitive = false;
//			buttonPickUp.Sensitive = false;
			buttonFinish.Sensitive = false;
			eventBoxImage.ButtonPressEvent -= DrawingAreaClicked;
			
		}

		public void EnableButtons ()
		{
			
//			buttonDeliver.Sensitive = true;
//			buttonPickUp.Sensitive = true;
			buttonFinish.Sensitive = true;
			eventBoxImage.ButtonPressEvent -= DrawingAreaClicked;			
			eventBoxImage.ButtonPressEvent += DrawingAreaClicked;
			
		}

		protected virtual void PickUpCallback (object sender, System.EventArgs e) {
			
			bool boxFound = false;
			for (int i = 0; i < currentScene.BoxesStatus.Length && boxFound == false; i++) {
				if ( currentScene.BoxesStatus[i].CurrentPosition == car.CurrentPlace) {
					boxFound = true;					
					if (car.Items.Count >= 3) {
						exer.Results.AddAction( new PD_ResultsAction("RECOGIDA", "ERROR", currentScene.BoxesStatus[i].BoxName, "Furgoneta llena.") );
						Gtk.MessageDialog dialog = new Gtk.MessageDialog (null, Gtk.DialogFlags.Modal, Gtk.MessageType.Warning, Gtk.ButtonsType.Ok, true, "<span size=\"xx-large\">Ya tienes 3 paquetes en la furgoneta. Intenta entregar alguno antes de coger uno nuevo.</span>", "Ya tienes 3 paquetes");
						GtkUtil.SetStyleRecursive( dialog, Configuration.Current.LargeFont );						
						this.Sensitive = false;
						this.drawingAreaFixed.Sensitive = false;
						int result = dialog.Run ();
						this.drawingAreaFixed.Sensitive = false;
						dialog.Destroy ();
						this.Sensitive = true;
					} else {
						currentScene.BoxesStatus[i].Status = "van";
						currentScene.BoxesStatus[i].CurrentPosition = -1;
						car.Items.Add (i);
						if ( currentMode == "game" ) {
							exer.Results.AddAction( new PD_ResultsAction( "RECOGIDA", "OK", currentScene.BoxesStatus[i].BoxName, "" ) );
							FillGoalsTopBar();
						} else if ( currentMode == "demo" ) {
							NextStep();
						}
						FillImagesTopBar ();
					}
				}
			}
			if (boxFound == false) {
				exer.Results.AddAction( new PD_ResultsAction("RECOGIDA", "ERROR", exer.PlacesManager.Places[car.CurrentPlace].Name, "Nada en el lugar") );
				Gtk.MessageDialog dialog = new Gtk.MessageDialog (null, Gtk.DialogFlags.Modal, Gtk.MessageType.Warning, Gtk.ButtonsType.Ok, true, "<span size=\"xx-large\">¡No hay ningún paquete en este lugar!</span>", "No hay paquete");
				GtkUtil.SetStyleRecursive( dialog, Configuration.Current.LargeFont );
				int result = dialog.Run ();
				dialog.Destroy ();
			}
		}

		protected virtual void DeliverCallback (object sender, System.EventArgs e)
		{
			bool emptyPlace = true;
			// Is there something in the van?
			if ( CarIsEmpty() ) {

				Gtk.MessageDialog dialog = new Gtk.MessageDialog (null, Gtk.DialogFlags.Modal, Gtk.MessageType.Warning, Gtk.ButtonsType.Ok, "<span size=\"xx-large\">¡La furgoneta esta vacía!</span>", "La furgoneta esta vacía");
				GtkUtil.SetStyleRecursive( dialog, Configuration.Current.LargeFont );				
				this.Sensitive = false;
				this.drawingAreaFixed.Sensitive = false;
				int result = dialog.Run ();
				this.drawingAreaFixed.Sensitive = true;
				dialog.Destroy ();
				this.Sensitive = true;
				exer.Results.AddAction( new PD_ResultsAction("ENTREGA", "ERROR", exer.PlacesManager.Places[car.CurrentPlace].Name,"Furgoneta vacia") );
			} 
			// We have something...
			else {
				// Is there something in the place?
				// Yes
				for (int i = 0; i < currentScene.BoxesStatus.Length; i++) {
					if (currentScene.BoxesStatus[i].Status == "shop" && CurrentScene.BoxesStatus[i].CurrentPosition == car.CurrentPlace) {
						emptyPlace = false;
						exer.Results.AddAction( new PD_ResultsAction("ENTREGA", "ERROR", exer.PlacesManager.Places[car.CurrentPlace].Name,"Ya hay un objeto") );
						Gtk.MessageDialog dialog = new Gtk.MessageDialog (null, Gtk.DialogFlags.Modal, Gtk.MessageType.Warning, Gtk.ButtonsType.Ok, "<span size=\"xx-large\">¡Ya hay un paquete en este lugar!</span>", "Ya hay un paquete");
						GtkUtil.SetStyleRecursive( dialog, Configuration.Current.LargeFont );
						this.Sensitive = false;
						drawingAreaFixed.Sensitive = false;
						int result = dialog.Run ();
						drawingAreaFixed.Sensitive = true;
						dialog.Destroy ();
						this.Sensitive = true;
					}
				}
				// The place is empty
				if (emptyPlace) {
					PD_DialogDeliverPackage auxDialog = new PD_DialogDeliverPackage (this);
					auxDialog.Run ();
				}
			}
		}

		public void DeliverPackage (int PackageIndex)
		{
			if ( currentMode == "game" ) {
				car.Items.Remove (PackageIndex);
				CurrentScene.BoxesStatus[PackageIndex].Status = "shop";
				CurrentScene.BoxesStatus[PackageIndex].CurrentPosition = car.CurrentPlace;
				exer.Results.AddAction( new PD_ResultsAction( "ENTREGA","OK", currentScene.BoxesStatus[PackageIndex].BoxName, "") );
				FillImagesTopBar ();
				FillGoalsTopBar();
			} else if ( currentMode == "demo" && currentStep == 8 ) {
				car.Items.Remove (PackageIndex);
				CurrentScene.BoxesStatus[PackageIndex].Status = "shop";
				CurrentScene.BoxesStatus[PackageIndex].CurrentPosition = car.CurrentPlace;
				FillImagesTopBar ();
				NextStep();
			}
		}
		
		public bool CarIsEmpty() {
		
			for (int i = 0; i < currentScene.BoxesStatus.Length; i++) {			
				if ( currentScene.BoxesStatus[i].Status == "van" ) {
					return false;	
				}				
			}
			
			return true;			
		}
		
		protected virtual void OnFinishButtonClicked (object sender, System.EventArgs e)
		{
			// Confirm the finish action
			Gtk.MessageDialog dialog = new Gtk.MessageDialog (	null,
				Gtk.DialogFlags.Modal,
				Gtk.MessageType.Question,
				Gtk.ButtonsType.YesNo,
				"<span size=\"xx-large\">¿Ya has repartido y recogido todos los paquetes?</span>",
				"Reparto terminado"
			);
			GtkUtil.SetStyleRecursive( dialog, Configuration.Current.LargeFont );
			int result = dialog.Run ();
			
			if ( result == (int) Gtk.ResponseType.Yes )
			{														
				
				// Calculate score. If we have some box in a wrong place, score will be 0
				int score = 0;
				bool boxesInRightPlace = true;
				for ( int i = 0; i < currentScene.BoxesStatus.Length; i++ ) {
					if ( currentScene.BoxesStatus[i].CurrentPosition !=
					    currentScene.BoxesStatus[i].GoalPosition ) {
						boxesInRightPlace = false;
					}
				}
				if ( boxesInRightPlace == false ) {
					score = 0;	
				} else {
					// If boxes are in the right place, we'll count the movements
					// Movements < 15 will be a golden medal
					if ( exer.Results.CurrentSingleResult.GetMovementsCounter() < 15 )  {
						score = 100;
					}
					// Movements between 15 and 20 will be a silver medal
					else if ( exer.Results.CurrentSingleResult.GetMovementsCounter() < 20 )  {
						score = 60;
					}
					// Any other case, bronze medal
					else {
						score = 1;
					}
				}
				
				// Save results
				exer.Results.SetFinalStatus( CurrentScene.BoxesStatus );
				exer.Serialize();
				// Results.SaveResults();
				
				SessionManager.GetInstance().ExerciseFinished(score);
				SessionManager.GetInstance().TakeControl();
				Pause();
				ExercisePackageDelivering.getInstance().finalizar();
			}
			dialog.Destroy();
		}
		
		private void BackStep() {
			firstFrameStep = true;
			currentCharacter = 0;
			currentStep--;
		}
		
		private void NextStep() {
			firstFrameStep = true;
			currentCharacter = 0;
			currentStep++;
		}
		
		protected virtual void GoForward (object sender, System.EventArgs e)
		{			
			NextStep();
		}
		
		protected virtual void OnButtonBackwardClicked (object sender, System.EventArgs e)
		{
			BackStep();
		}
		
		protected virtual void OnButtonGotoLastClicked (object sender, System.EventArgs e)
		{
			currentStep = 12;
			firstFrameStep = true;
			totalCharacters = animationsText[12].Length;
			currentCharacter = 0;
		}
		
		protected virtual void OnButtonStartExerciseClicked (object sender, System.EventArgs e)
		{			
			InitPanel( currentSceneId );
			hboxGameButtons.ShowAll();
			InitGame();
		}
		
		public void Pause ()
		{
			if (!pausedExercise) {
				pausedExercise = true;
				GLib.Source.Remove (auxTimer);
			} else {
				pausedExercise = false;
				auxTimer = GLib.Timeout.Add (currentInterval, currentHandler);
			}
		}
		
		protected virtual void DeliverCallbackTopBar (object sender, System.EventArgs e) {

			bool emptyPlace = true;
			// Is there something in the place?
			// Yes
			if ( currentMode == "game" || ( currentMode == "demo" && currentStep == 8 ) ) {
				for (int i = 0; i < currentScene.BoxesStatus.Length; i++) {
					if (currentScene.BoxesStatus[i].Status == "shop" && CurrentScene.BoxesStatus[i].CurrentPosition == car.CurrentPlace) {
						emptyPlace = false;
						exer.Results.AddAction( new PD_ResultsAction("ENTREGA", "ERROR", exer.PlacesManager.Places[car.CurrentPlace].Name,"Ya hay un objeto") );
						Gtk.MessageDialog dialog = new Gtk.MessageDialog (null, Gtk.DialogFlags.Modal, Gtk.MessageType.Warning, Gtk.ButtonsType.Ok, "<span size=\"xx-large\">¡Ya hay un paquete en este lugar!</span>", "Ya hay un paquete");
						GtkUtil.SetStyleRecursive( dialog, Configuration.Current.LargeFont );
						this.Sensitive = false;
						int result = dialog.Run ();
						this.Sensitive = true;
						dialog.Destroy ();
					}
				}
				// The place is empty
				if (emptyPlace) {
					DeliverPackage( Int32.Parse( ((Gtk.Button)sender).Name ) );
				}
			}
			
		}
	}
}

