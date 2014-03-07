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
using Cairo;
using Gtk;
using Gdk;

namespace pesco
{

	public class MedalItem { 
		
		private string type = "";
		private GPosition position = null;
		private double alpha = 0;
		private double factor = 0;
		
		public static int MEDAL_WIDTH = 60;
		public static int MEDAL_HEIGHT = 60;
		
		public Gdk.Pixbuf medalPixbuf = null;
		
		public string Type {
			get {
				return type;
			}
			set {
				type = value;
			}
		}
		
		public GPosition Position {
			get {
				return position;
			}
			set {
				position = value;
			}
		}

		public MedalItem() {}
		
		public MedalItem( string type, GPosition position ) {
			
			Type = type;
			Position = position;
			
		}
		
		public void Update() {
			
		}
		
	}
	
	public class MedalsCategory {
		
		// Consts
		public static int CATEGORY_WIDTH = 225;
		public static int CATEGORY_HEIGHT = 200;
		
		public static int PADDING_TOP_NAME = 50;
		
		private string categoryName = "";
		private string categorySubtext = "";
		private GPosition position = null;
		private List<MedalItem> medalsList = new List<MedalItem>();
	
		public GPosition Position {
			get {
				return position;
			}
			set {
				position = value;
			}
		}
		
		public string CategoryName {
			get {
				return categoryName;
			}
			set {
				categoryName = value;
			}
		}
		
		public List<MedalItem> MedalsList {
			get {
				return medalsList;
			}
			set {
				medalsList = value;
			}
		}		
		
		public MedalsCategory() {}
	}
	
	

	[System.ComponentModel.ToolboxItem(true)]
	public partial class TotalPodiumPanel : Gtk.Bin
	{

		// Consts
		const int WIDTH_SCREEN = 1000;
		const int HEIGHT_SCREEN = 600;

		const int COLUMNS_ORIG_X = 100;
		const int COLUMNS_ORIG_Y = 200;
		
		// Category Consts
		public static int CATEGORY_WIDTH = 225;
		public static int CATEGORY_HEIGHT = 200;
		
		// Columns consts
		const int PADDING_MEDALS_LEFT = 100;
		const int PADDING_MEDALS_TOP = 100;
		const int COL_WIDTH = 900;
		const int COL_HEIGHT = 400;
		
		// Draw elements
		Gtk.DrawingArea drawingArea = new Gtk.DrawingArea();
		private Gdk.Pixbuf bgPixbuf;
		private Gdk.Pixbuf backgroundPixbuf;
		private Gdk.Pixbuf dialogPixbuf;
		private Gdk.Pixbuf pepe1Pixbuf;
		private Gdk.Pixbuf pepe2Pixbuf;
		private Gdk.Pixbuf medalPixbuf;
		private Gdk.Pixbuf goldPixbufScaled;
		private Gdk.Pixbuf silverPixbufScaled;
		private Gdk.Pixbuf bronzePixbufScaled;
		int pepeStatus = 0;
		Gdk.GC auxGC;
		Cairo.Context cairoCMedal;
		Cairo.Context cairoCPepe;
		double auxAlpha = 0;
		double auxFactor = 0;
		double auxFactor2 = 0;
		bool auxIncreasing = true;
		bool auxIncreasing2 = true;
		
		// Animations
		string stringToSay;
		int currentStep = 0;
		int currentCharacter = 0;
		int totalCharacters = 0;
		string [] animationsText = {
						"¡Enhorabuena! Has completado la sesión. Abajo puedes ver las medallas que has obtenido. Pulsa en siguiente para continuar.",
						"Eso es todo por esta sesión. ¡Nos vemos en la próxima!"};

		bool firstFrameStep = true;
		
		// Timers
		private int auxAnimationTimer;
		private uint auxTimer;
		private GLib.TimeoutHandler currentHandler;
		private uint currentInterval;
		private bool pausedExercise;
		private const int REPAINT_SPEED = 50;
		private int pauseTimer = 100;

		// Layout 
		Pango.Layout auxLayout;

		// Medals elements
		private int currentMedal = 0;
		private string [] medalPixbux = {"pesco.ejercicios.figures.medalla_oro.png",
								"pesco.ejercicios.figures.medalla_plata.png",
								"pesco.ejercicios.figures.medalla_bronce.png"};
		
		System.Collections.Generic.SortedDictionary<ExerciseCategory, List<Medal>> medalDictionary;
			
		int goldMedalsCounter = 0;
		int silverMedalsCounter = 0;
		int bronzeMedalsCounter = 0;
		
		// Categories
		List <MedalsCategory> medalsCategories = new List<MedalsCategory>();
		
		public Gtk.Button ButtonOK{
			get{
				return this.buttonOK;	
			}
		}
		
		// Constructor to be called at the end of a session
		public TotalPodiumPanel () {
		
			this.Build ();
			InitPanel();
			
			// Get list of categories and medals
			medalDictionary = User.GetMedalsBySessionAndCategory( SessionManager.GetInstance().CurSession.IdSession );
			
			// Call generator
			GenerateMedalsForEndSession();
		
		}
		
		// Constructor to be called to show a specific session medals
		public TotalPodiumPanel (int sessionId) {
		
			this.Build ();
			InitPanel();
			
			// Get list of categories and medals
			medalDictionary = User.GetMedalsBySessionAndCategory( sessionId );
			
			// Call generator
			GenerateMedalsForEndSession();
		
		}
		
		// Constructor to be called at the end of an exercise, with a list of medal items
		public TotalPodiumPanel ( string exerciseName, string balloonText, List<Medal> medalDictionary ) {
		
			this.Build ();
			InitPanel();
			
			animationsText[0] = balloonText;
			GenerateMedalsForExercise( exerciseName, medalDictionary );
					
		}
		
		// Constructor to be called at the end of an exercise, with an array of integers
		public TotalPodiumPanel ( string exerciseName, string balloonText, int [] medalsArray ) {
		
			this.Build ();
			GtkUtil.SetStyle( buttonOK, Configuration.Current.MediumFont );
			InitPanel();
			
			animationsText[0] = balloonText;
			
			List<Medal> auxMedals = new List<Medal>();
			for ( int i = 0; i < medalsArray[0]; i++ ) {
				auxMedals.Add( new Medal(MedalValue.Gold, DateTime.Now, -1, -1) );
			}
			for ( int i = 0; i < medalsArray[1]; i++ ) {
				auxMedals.Add( new Medal(MedalValue.Silver, DateTime.Now, -1, -1) );
			}
			for ( int i = 0; i < medalsArray[2]; i++ ) {
				auxMedals.Add( new Medal(MedalValue.Bronze, DateTime.Now, -1, -1) );
			}
						
			GenerateMedalsForExercise( exerciseName, auxMedals );
					
		}
		
		~TotalPodiumPanel() {
		
			if ( cairoCMedal != null )
				((IDisposable) cairoCMedal).Dispose(); 
			if ( cairoCPepe != null )
				((IDisposable) cairoCPepe).Dispose(); 	
			
		}
		
		public void SetBalloonText( string text ) {
			
			animationsText[0] = text;
			
		}
		
		private void InitLayout ()
		{
			
			auxLayout = new Pango.Layout (this.CreatePangoContext ());
			auxLayout.Width = Pango.Units.FromPixels (720);
			auxLayout.Justify = false;
			auxLayout.Wrap = Pango.WrapMode.Word;
			auxLayout.Alignment = Pango.Alignment.Left;
			auxLayout.FontDescription = Pango.FontDescription.FromString ("Ahafoni CLM Bold 22");
			
		}
		
		private void CategoryLayout() {
			auxLayout.Width = Pango.Units.FromPixels (CATEGORY_WIDTH);
			auxLayout.Justify = false;
			auxLayout.Wrap = Pango.WrapMode.Word;
			auxLayout.Alignment = Pango.Alignment.Center;
			auxLayout.FontDescription = Pango.FontDescription.FromString ("Ahafoni CLM Bold 16");
		}
		
		private void MedalLayout() {
			auxLayout.Width = Pango.Units.FromPixels (860);
			auxLayout.Justify = false;
			auxLayout.Wrap = Pango.WrapMode.Word;
			auxLayout.Alignment = Pango.Alignment.Center;
			auxLayout.FontDescription = Pango.FontDescription.FromString ("Ahafoni CLM Bold 34");
		}
		
		private void MedalSubTextLayout() {
			auxLayout.Width = Pango.Units.FromPixels (860);
			auxLayout.Justify = false;
			auxLayout.Wrap = Pango.WrapMode.Word;
			auxLayout.Alignment = Pango.Alignment.Center;
			auxLayout.FontDescription = Pango.FontDescription.FromString ("Ahafoni CLM Bold 20");
		}
			
		public void InitPanel ()
		{
			GtkUtil.SetStyle( buttonOK, Configuration.Current.MediumFont );
			InitLayout ();
			
			// Attach callbacks
			imageBackground.ExposeEvent += Expose_Event;
			
			// Destro Cairo contexts and timer on destroy			
			this.Destroyed += delegate {
				if ( currentHandler != null ) 
					GLib.Source.Remove( auxTimer );
				if ( cairoCMedal != null )
					((IDisposable) cairoCMedal).Dispose(); 
				if ( cairoCPepe != null )
					((IDisposable) cairoCPepe).Dispose(); 				
			};
			
			// Load pixbufs
			backgroundPixbuf = Gdk.Pixbuf.LoadFromResource ("pesco.ejercicios.resources.img.background.png");
			dialogPixbuf = Gdk.Pixbuf.LoadFromResource ("pesco.ejercicios.gui.img.dialog.png");
			pepe1Pixbuf = Gdk.Pixbuf.LoadFromResource ("pesco.ejercicios.gui.img.pepe1.png");
			pepe2Pixbuf = Gdk.Pixbuf.LoadFromResource ("pesco.ejercicios.gui.img.pepe2.png");

			goldPixbufScaled = Gdk.Pixbuf.LoadFromResource ("pesco.ejercicios.figures.medalla_oro.png" );
			goldPixbufScaled = goldPixbufScaled.ScaleSimple( MedalItem.MEDAL_WIDTH, MedalItem.MEDAL_HEIGHT, InterpType.Bilinear );
			silverPixbufScaled = Gdk.Pixbuf.LoadFromResource ("pesco.ejercicios.figures.medalla_plata.png" );
			silverPixbufScaled = silverPixbufScaled.ScaleSimple( MedalItem.MEDAL_WIDTH, MedalItem.MEDAL_HEIGHT, InterpType.Bilinear );
			bronzePixbufScaled = Gdk.Pixbuf.LoadFromResource ("pesco.ejercicios.figures.medalla_bronce.png" );
			bronzePixbufScaled = bronzePixbufScaled.ScaleSimple( MedalItem.MEDAL_WIDTH, MedalItem.MEDAL_HEIGHT, InterpType.Bilinear );

			imageBackground.Pixmap = new Gdk.Pixmap ( this.GdkWindow, WIDTH_SCREEN, HEIGHT_SCREEN, 24);
			
			auxGC = new Gdk.GC(imageBackground.Pixmap);
			DrawDialogBackground();
			
			// Creating Cairo Contexts
			cairoCMedal = Gdk.CairoHelper.Create ( imageBackground.Pixmap );
			cairoCPepe = Gdk.CairoHelper.Create ( imageBackground.Pixmap );		
			
			// Init timer
			/* if ( auxTimer != null ) 
				GLib.Source.Remove(auxTimer);
			
			currentHandler = new GLib.IdleHandler( Update );
			auxTimer = GLib.Idle.Add ( currentHandler );*/
			currentHandler = new GLib.TimeoutHandler( Update );
			currentInterval = 50;
			auxTimer = GLib.Timeout.Add( currentInterval, currentHandler );
			
		}
		
		public void GenerateMedalsForEndSession () {
						
			int i=0;
			int totalCategories = 0;
			
			// Calculating categories with medals
			foreach(ExerciseCategory cat in medalDictionary.Keys){
				if ( medalDictionary[cat].Count != 0 ) {
					totalCategories++;	
				}
			}
			
			// Generating positions
			foreach(ExerciseCategory cat in medalDictionary.Keys){
				if ( medalDictionary[cat].Count != 0 ) {
					MedalsCategory auxMedalCategory = new MedalsCategory();
	
					auxMedalCategory.CategoryName = Exercise.ExerciseCategoryToString(cat).ToUpper();
					auxMedalCategory.Position = new GPosition (
						COLUMNS_ORIG_X +
						( ( COL_WIDTH - ( MedalsCategory.CATEGORY_WIDTH * totalCategories ) ) / 2 ) + MedalsCategory.CATEGORY_WIDTH * i,
						COLUMNS_ORIG_Y
						);
					
					int j=0;
					foreach (Medal m in medalDictionary[cat]){	
						
						if (m.MedalValue == MedalValue.Gold) {
							auxMedalCategory.MedalsList.Add( new MedalItem( "gold", new GPosition( 
								auxMedalCategory.Position.X + (( MedalsCategory.CATEGORY_WIDTH - (MedalItem.MEDAL_WIDTH * 3) ) / 2) + ( MedalItem.MEDAL_WIDTH * (j % 3) ),
								PADDING_MEDALS_TOP +
								auxMedalCategory.Position.Y + ( MedalItem.MEDAL_HEIGHT * (j / 3) ) ) ) );		
							j++;
							goldMedalsCounter++;
						}
						else if (m.MedalValue == MedalValue.Silver) {
							auxMedalCategory.MedalsList.Add( new MedalItem( "silver", new GPosition( 
								auxMedalCategory.Position.X + (( MedalsCategory.CATEGORY_WIDTH - (MedalItem.MEDAL_WIDTH * 3) ) / 2) + ( MedalItem.MEDAL_WIDTH * (j % 3) ),
								PADDING_MEDALS_TOP +
								auxMedalCategory.Position.Y + ( MedalItem.MEDAL_HEIGHT * (j / 3) ) ) ) );	
							j++;
							silverMedalsCounter++;
						}
						else if (m.MedalValue == MedalValue.Bronze) {
							auxMedalCategory.MedalsList.Add( new MedalItem( "bronze", new GPosition( 
								auxMedalCategory.Position.X + (( MedalsCategory.CATEGORY_WIDTH - (MedalItem.MEDAL_WIDTH * 3) ) / 2) + ( MedalItem.MEDAL_WIDTH * (j % 3) ),
								PADDING_MEDALS_TOP +
								auxMedalCategory.Position.Y + ( MedalItem.MEDAL_HEIGHT * (j / 3) ) ) ) );	
							j++;
							bronzeMedalsCounter++;
						}
						
					}
					medalsCategories.Add( auxMedalCategory );
					i++;
				}
			}
			
		}
		
		public void GenerateMedalsForExercise ( string exerciseName, List<Medal> medals ) {
			
			// Generating positions
			if ( medals.Count != 0 ) {
				int totalCategories = 1;
				int i = 0;
				
				MedalsCategory auxMedalCategory = new MedalsCategory();

				auxMedalCategory.CategoryName = exerciseName.ToUpper();
				auxMedalCategory.Position = new GPosition (
					COLUMNS_ORIG_X +
					( ( COL_WIDTH - ( MedalsCategory.CATEGORY_WIDTH * totalCategories ) ) / 2 ) + MedalsCategory.CATEGORY_WIDTH * i,
					COLUMNS_ORIG_Y
					);
				
				int j=0;
				foreach (Medal m in medals){	
					
					if (m.MedalValue == MedalValue.Gold) {
						auxMedalCategory.MedalsList.Add( new MedalItem( "gold", new GPosition( 
							auxMedalCategory.Position.X + (( MedalsCategory.CATEGORY_WIDTH - (MedalItem.MEDAL_WIDTH * 3) ) / 2) + ( MedalItem.MEDAL_WIDTH * (j % 3) ),
							PADDING_MEDALS_TOP +
							auxMedalCategory.Position.Y + ( MedalItem.MEDAL_HEIGHT * (j / 3) ) ) ) );		
						j++;
						goldMedalsCounter++;
					}
					else if (m.MedalValue == MedalValue.Silver) {
						auxMedalCategory.MedalsList.Add( new MedalItem( "silver", new GPosition( 
							auxMedalCategory.Position.X + (( MedalsCategory.CATEGORY_WIDTH - (MedalItem.MEDAL_WIDTH * 3) ) / 2) + ( MedalItem.MEDAL_WIDTH * (j % 3) ),
							PADDING_MEDALS_TOP +
							auxMedalCategory.Position.Y + ( MedalItem.MEDAL_HEIGHT * (j / 3) ) ) ) );	
						j++;
						silverMedalsCounter++;
					}
					else if (m.MedalValue == MedalValue.Bronze) {
						auxMedalCategory.MedalsList.Add( new MedalItem( "bronze", new GPosition( 
							auxMedalCategory.Position.X + (( MedalsCategory.CATEGORY_WIDTH - (MedalItem.MEDAL_WIDTH * 3) ) / 2) + ( MedalItem.MEDAL_WIDTH * (j % 3) ),
							PADDING_MEDALS_TOP +
							auxMedalCategory.Position.Y + ( MedalItem.MEDAL_HEIGHT * (j / 3) ) ) ) );	
						j++;
						bronzeMedalsCounter++;
					}
					
				}
				medalsCategories.Add( auxMedalCategory );
			}
			
		}
		
		void DrawCategories() {
			
			Gdk.Pixbuf auxMedalPixbuf = null;
			for ( int i = 0; i < medalsCategories.Count; i ++ ) {
				for ( int j = 0; j < medalsCategories[i].MedalsList.Count; j++ ) {					
					// Medals
					if ( medalsCategories[i].MedalsList[j].Type == "gold" ) {
						auxMedalPixbuf = goldPixbufScaled;
					} else if ( medalsCategories[i].MedalsList[j].Type == "silver" ) {
						auxMedalPixbuf = silverPixbufScaled;
					} else if ( medalsCategories[i].MedalsList[j].Type == "bronze" ) {
						auxMedalPixbuf = bronzePixbufScaled;
					}
					// Category name
					CategoryLayout();
					auxLayout.SetMarkup( "<span color=\"black\">"+medalsCategories[i].CategoryName+"</span>" );
					imageBackground.Pixmap.DrawLayout( auxGC, medalsCategories[i].Position.X, 
					                                  medalsCategories[i].Position.Y + MedalsCategory.PADDING_TOP_NAME, auxLayout );

					imageBackground.Pixmap.DrawPixbuf( auxGC, auxMedalPixbuf, 0, 0,
					    medalsCategories[i].MedalsList[j].Position.X, 
						medalsCategories[i].MedalsList[j].Position.Y, 					                                  
						auxMedalPixbuf.Width, 
						auxMedalPixbuf.Width, 0, 0, 0);

				}		
			}
			
		}
		
		void Expose_Event (object obj, ExposeEventArgs args)
		{
			// Draw background
			DrawDialogBackground();		
			
			// Congratulations!
			if (currentStep == 0) {				
				if ( FirstFrameStep() ) {
					stringToSay = animationsText[0];
					totalCharacters = animationsText[0].Length;
					pauseTimer = 10;
				}
				if ( AfterLastFrameStep() ) {
					pauseTimer--;	
				}
				// Balloon text
				PepeUtils.IncrementCharacterDialog( ref currentCharacter, ref pepeStatus, stringToSay );
				InitLayout();
				auxLayout.SetMarkup( "<span color=\"blue\">"+stringToSay.Substring(0, currentCharacter)+"</span>" );
				imageBackground.Pixmap.DrawLayout( auxGC,200, 40, auxLayout );
				// Pepe
				DrawPepeTalking();
				if ( pauseTimer == 0 ) {
					NextStep();	
				}
			} 
			// Show medal!
			else if ( currentStep == 1 ) {
				if ( FirstFrameStep() ) {
					stringToSay = animationsText[0];
					totalCharacters = animationsText[0].Length;
				}
				// Balloon text
				InitLayout();
				auxLayout.SetMarkup( "<span color=\"blue\">"+stringToSay+"</span>" );
				imageBackground.Pixmap.DrawLayout( auxGC, 200, 40, auxLayout );	
				// Draw Pepe
				DrawPepeTalking();	
				// Draw Medal
				DrawCategories();
				DrawGlobalMedalsText();
			}
			// After medal shown
			else if ( currentStep == 2 ) {
				if ( FirstFrameStep() ) {
					stringToSay = animationsText[1];
					totalCharacters = animationsText[1].Length;
					ButtonOK.Clicked += delegate {
						Destroy();
						SessionManager.GetInstance().FinishApplication();
					};
				}
				// Medal
				// DrawMedal();
				// Balloon text
				InitLayout();
				PepeUtils.IncrementCharacterDialog( ref currentCharacter, ref pepeStatus, stringToSay );
				auxLayout.SetMarkup( "<span color=\"blue\">"+stringToSay.Substring(0, currentCharacter)+"</span>" );
				imageBackground.Pixmap.DrawLayout( auxGC, 200, 40, auxLayout );	
				// Pepe
				DrawPepe();
				// Medals
				DrawCategories();
				DrawGlobalMedalsText();
				
			}
			
		}
		
		private void DrawDialogBackground() {
			imageBackground.Pixmap.DrawPixbuf( auxGC, backgroundPixbuf, 0, 0, 0, 0, WIDTH_SCREEN, HEIGHT_SCREEN, 0, 0, 0);
			imageBackground.Pixmap.DrawPixbuf( auxGC, dialogPixbuf, 0, 0, 0, 0, WIDTH_SCREEN, HEIGHT_SCREEN, 0, 0, 0);				
		}
		
		private void DrawPepeTalking() {
		
		
			if ( pepeStatus > 2 ) {
				imageBackground.Pixmap.DrawPixbuf( auxGC, pepe1Pixbuf, 0, 0, 0, HEIGHT_SCREEN - pepe1Pixbuf.Height, pepe1Pixbuf.Width, pepe1Pixbuf.Height, 0, 0, 0);	
			} else {
				imageBackground.Pixmap.DrawPixbuf( auxGC, pepe2Pixbuf, 0, 0, 0, HEIGHT_SCREEN - pepe2Pixbuf.Height, pepe2Pixbuf.Width, pepe2Pixbuf.Height, 0, 0, 0);		
			}
			
		}
		private void DrawGlobalMedalsText() {
		
			string auxText = "";
			
			if ( goldMedalsCounter != 0 ) {
				if ( goldMedalsCounter == 1 ) {	
					auxText = "1 medalla de oro"; 
				} else {
					auxText = goldMedalsCounter+ " medallas de oro";	
				}
			}
			
			if ( silverMedalsCounter != 0 ) {
				if ( goldMedalsCounter != 0 ) {
					if ( bronzeMedalsCounter == 0 ) {
						auxText += " y ";
					} else {
						auxText += ", ";
					}
				}
				if ( silverMedalsCounter == 1 ) {	
					auxText += "1 medalla de plata"; 
				} else {
					auxText += silverMedalsCounter+ " medallas de plata";	
				}
			}
			
			if ( bronzeMedalsCounter != 0  ) {
				if ( goldMedalsCounter != 0 || silverMedalsCounter != 0 ) {
					auxText += " y ";
				}
				if ( silverMedalsCounter == 1 ) {	
					auxText += "1 medalla de bronce"; 
				} else {
					auxText += bronzeMedalsCounter+ " medallas de bronce";	
				}
			}
			
			
			auxLayout.SetMarkup( "<span color=\"black\">"+auxText+"</span>" );
			int width, height;
			auxLayout.Width = Pango.Units.FromPixels( 900 );
			auxLayout.Alignment = Pango.Alignment.Center;
			auxLayout.GetPixelSize( out width, out height );
			imageBackground.Pixmap.DrawLayout( auxGC, 100, 525, auxLayout );
			
		}
			
		private void IncrementCharacterDialog() {
			if (currentCharacter < totalCharacters) {
				currentCharacter++;
				pepeStatus = currentCharacter % 7;
			} else {
				pepeStatus = 0;	
			}
		}
		
		private bool Update() {
			
			/*
			long currentTicks = System.Diagnostics.Stopwatch.GetTimestamp();
			long difference = currentTicks - SessionManager.GetInstance().CurrenTicks;
			long interval = SessionManager.GetInstance().Interval;

			if ( difference > interval ) {			
				imageBackground.QueueDraw();
		    	SessionManager.GetInstance().UpdateCurrenTicks();
			}*/
			imageBackground.QueueDraw();
			
			return true;
		}
		
		private void DrawMedal() {
		
				// Medal
				if ( auxAlpha < 1 ) {
					auxAlpha += 0.01;
					auxFactor += 0.005;
				} 
				if ( auxIncreasing == true ) {
					auxFactor += 0.005;	
				} else {
					auxFactor -= 0.005;	
				}
				
				if ( auxFactor >= 1 ) {
					auxIncreasing = false;
				} 
				if ( !auxIncreasing && auxFactor <= 0.9 ) {
					auxIncreasing = true;
				}
								
				CairoHelper.SetSourcePixbuf (cairoCMedal, medalPixbuf, 0, 0);
				cairoCMedal.Matrix = new Matrix();					                           
				cairoCMedal.Translate( 442+((256.0-(256.0*auxFactor))/2), 222+((256.0-(256.0*auxFactor))/2) );
				cairoCMedal.Scale( auxFactor, auxFactor );
				cairoCMedal.PaintWithAlpha (auxAlpha);
		}
		
		private void DrawPepe() {
													
				CairoHelper.SetSourcePixbuf (cairoCPepe, pepe1Pixbuf, 0, 0);
				cairoCPepe.Matrix = new Matrix();					                           
				cairoCPepe.Translate( 0, 600 - (pepe1Pixbuf.Height) );
				cairoCPepe.Paint();
		
		}
		
		private void DrawPepeJumping() {
			
				if ( auxIncreasing2 ) {
					auxFactor2 += 4;	
				} else {
					auxFactor2 -= 2;	
				}
				if ( auxFactor2 == 32 ) {
					auxIncreasing2 = false;
				} else if ( auxFactor2 == 0 ) {
					auxIncreasing2 = true;	
				}
											
				CairoHelper.SetSourcePixbuf (cairoCPepe, pepe1Pixbuf, 0, 0);
				cairoCPepe.Matrix = new Matrix();					                           
				cairoCPepe.Translate( 0, 600 - (pepe1Pixbuf.Height) - auxFactor2 );
				cairoCPepe.Paint();
	
		}
		
		private bool FirstFrameStep ()
		{
			if (firstFrameStep == true) {
				firstFrameStep = false;
				return true;
			}
			
			return false;
		}
		
		private bool AfterLastFrameStep ()
		{
			
			if (currentCharacter >= stringToSay.Length) {
				return true;
			}
			
			return false;
		}
		
		private void NextStep() {
			firstFrameStep = true;
			currentStep++;
			currentCharacter = 0;
		}
		
		private void BackStep() {
			if ( currentStep != 0 ) {
				firstFrameStep = true;
				currentCharacter = 0;
				currentStep--;
			}
		}
		protected virtual void OnButtonOKClicked (object sender, System.EventArgs e)
		{
			NextStep();
		}
		
		
	}
}

