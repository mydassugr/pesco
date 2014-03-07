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
using Gdk;
using System.Collections.Generic;

namespace pesco
{
	
	public class MedalSessionBox {
	
		private int boxSessionId;
		private string boxName;
		private SortedDictionary<MedalValue, int> medals;
		private bool visible;
				
		public string BoxName {
			get {
				return this.boxName;
			}
			set {
				boxName = value;
			}
		}

		public int BoxSessionId {
			get {
				return this.boxSessionId;
			}
			set {
				boxSessionId = value;
			}
		}

		public SortedDictionary<MedalValue, int> Medals {
			get {
				return this.medals;
			}
			set {
				medals = value;
			}
		}

		public bool Visible {
			get {
				return this.visible;
			}
			set {
				visible = value;
			}
		}

		public MedalSessionBox ( int sessionIndex ) {
			
		 	medals = User.GetMedalsBySessionAndValue(sessionIndex);
			boxSessionId = sessionIndex;
			if ( medals != null ) {
				if ( medals[MedalValue.Gold] == 0 
					&& medals[MedalValue.Silver] == 0 
					&& medals[MedalValue.Bronze] == 0 ) {
					visible = false;
				} else {				          
					// Draw box
					visible = true;
				}
			} else {
				visible = false;	
			}
		
		}				
		
	}
	
	[System.ComponentModel.ToolboxItem(true)]
	public partial class SessionsPodiumPanel : Gtk.Bin
	{
		
		// Sizes
		const int WIDTH_SCREEN = 1000;
		const int HEIGHT_SCREEN = 600;
		const int OFFSET_TOP = 78;
		const int MARGIN_LEFT = 42;
		const int OFFSET_BOXES = 41;
		const int BOX_WIDTH = 198;
		const int BOX_HEIGHT = 144;
		const int OFFSET_BOXES_WIDTH = 41;
		const int OFFSET_BOXES_HEIGHT = 16;
		const int PADDING_BOX_TOP = 4;
		const int BOX_FIRSTROWLINE = 40;
		const int BOX_SECONDBOXLINE = 98;
		const int MEDAL_WIDTH = 56;
		const int MEDAL_HEIGTH = 56;
		const int MEDAL_BOX_WIDTH = BOX_WIDTH / 3;
		const int MEDAL_BOX_HEIGTH = BOX_SECONDBOXLINE - BOX_FIRSTROWLINE;
		
		// Drawing elements
		Gdk.GC auxGC;
		private Gdk.Pixbuf backgroundPixbuf;
				
		private Gdk.Pixbuf medalPixbuf;
		private Gdk.Pixbuf goldPixbufScaled;
		private Gdk.Pixbuf silverPixbufScaled;
		private Gdk.Pixbuf bronzePixbufScaled;
		
		// Layout 
		Pango.Layout auxLayout;
		
		// Timers
		private int auxAnimationTimer;
		private uint auxTimer;
		private GLib.TimeoutHandler currentHandler;
		private uint currentInterval;
		private bool pausedExercise;
		private const int REPAINT_SPEED = 50;
		private int pauseTimer = 15;
		
		private MainWindow parentWindow;
		
		// List of medal boxes
		List <MedalSessionBox> medalBoxes = new List<MedalSessionBox>();
		
		public Gtk.Button ButtonClose {
			get {
				return this.buttonClose;	
			}			
		}
		
		public SessionsPodiumPanel ()
		{
			this.Build ();		
		}
		
		public SessionsPodiumPanel ( MainWindow parentWindow )
		{
			this.Build ();
			this.parentWindow = parentWindow;
		}
		
		public void InitPanel() {
			
			// Data
			GtkUtil.SetStyle( buttonClose, Configuration.Current.MediumFont );
			InitLayout();
			InitMedals();
			GenerateBoxes();
			
			// Images
			
			imageBackground.ExposeEvent += Expose_Event;

			char auxSeparator = System.IO.Path.DirectorySeparatorChar;
			backgroundPixbuf = new Gdk.Pixbuf ( Configuration.CommandDirectory+
			                                   auxSeparator+"gui"+auxSeparator+"welcome"
			                                   +auxSeparator+"img"+auxSeparator+"background.png" );
			
			imageBackground.Pixmap = new Gdk.Pixmap ( this.GdkWindow, WIDTH_SCREEN, HEIGHT_SCREEN, 24);
			
			
			auxGC = new Gdk.GC(imageBackground.Pixmap);
			
			if ( auxTimer != null && auxTimer > 0 ) 
				GLib.Source.Remove(auxTimer);
			
			eventBox.ButtonPressEvent += BoxClicked;

			currentHandler = new GLib.TimeoutHandler (Update);
			currentInterval = REPAINT_SPEED;
			auxTimer = GLib.Timeout.Add (currentInterval, currentHandler);
		}
		
		private void InitMedals() {
			goldPixbufScaled = Gdk.Pixbuf.LoadFromResource ("pesco.ejercicios.figures.medalla_oro.png" );
			goldPixbufScaled = goldPixbufScaled.ScaleSimple( MEDAL_WIDTH, MEDAL_HEIGTH, InterpType.Bilinear );
			silverPixbufScaled = Gdk.Pixbuf.LoadFromResource ("pesco.ejercicios.figures.medalla_plata.png" );
			silverPixbufScaled = silverPixbufScaled.ScaleSimple( MEDAL_WIDTH, MEDAL_HEIGTH, InterpType.Bilinear );
			bronzePixbufScaled = Gdk.Pixbuf.LoadFromResource ("pesco.ejercicios.figures.medalla_bronce.png" );
			bronzePixbufScaled = bronzePixbufScaled.ScaleSimple( MEDAL_WIDTH, MEDAL_HEIGTH, InterpType.Bilinear );	
		}
		
		private void InitLayout ()
		{
			
			auxLayout = new Pango.Layout (this.CreatePangoContext ());			
		}
		
		private void TopLayout ()
		{
			auxLayout.Width = Pango.Units.FromPixels (1000);
			auxLayout.Justify = false;
			auxLayout.Wrap = Pango.WrapMode.Word;
			auxLayout.Alignment = Pango.Alignment.Center;
			auxLayout.FontDescription = Pango.FontDescription.FromString ("Ahafoni CLM Bold 36");

		}
		
		private void LayoutSessionsLabel ()
		{			
			auxLayout.Width = Pango.Units.FromPixels (198);
			auxLayout.Justify = false;
			auxLayout.Wrap = Pango.WrapMode.Word;
			auxLayout.Alignment = Pango.Alignment.Center;
			auxLayout.FontDescription = Pango.FontDescription.FromString ("Ahafoni CLM Bold 20");			
		}		
		
		private void LayoutMedalsNumberLabel ()
		{			
			auxLayout.Width = Pango.Units.FromPixels (MEDAL_BOX_WIDTH);
			auxLayout.Justify = false;
			auxLayout.Wrap = Pango.WrapMode.Word;
			auxLayout.Alignment = Pango.Alignment.Center;
			auxLayout.FontDescription = Pango.FontDescription.FromString ("Ahafoni CLM Bold 18");			
		}	
		
		bool Update() {
			imageBackground.QueueDraw();
			return true;			
		}
		
		void Expose_Event (object obj, ExposeEventArgs args)
		{
		
			imageBackground.Pixmap.DrawPixbuf( auxGC, backgroundPixbuf, 0, 0, 0, 0, WIDTH_SCREEN, HEIGHT_SCREEN, 0, 0, 0);
			string stringToSay = "Hola!";
			TopLayout();
			auxLayout.SetMarkup( "<span color=\"black\">MEDALLERO</span>" );
			imageBackground.Pixmap.DrawLayout( auxGC, 0, 10, auxLayout );
			DrawBoxes();
			
		}		
		
		private void GenerateBoxes() {
			
			// TODO: Check if we have to start from session 1 or from session 2
			for ( int i = 1; i <= 12; i++ ) {			
				medalBoxes.Add( new MedalSessionBox(i) );
			}
		
		}
		
		private void DrawBoxes() {
			
			// TODO: Check if we have to start from session 1 or from session 2
			for ( int i = 0; i < medalBoxes.Count; i++ ) {

				// Calculate row and col
				int col = (i) % 4;
				int row = (i) / 4;				
				int session = i;
				
				// Get medals in this session
				if ( medalBoxes[i].Visible ) {
					DrawBox( medalBoxes[i].BoxSessionId, row, col, medalBoxes[i].Medals );	
				}
			
			}
						
		}
		
		private void DrawBox(int session, int row, int col, SortedDictionary<MedalValue, int> medals) {			
			
				// White box
				auxGC.RgbFgColor = new Gdk.Color( 255, 255, 255 );
				imageBackground.Pixmap.DrawRectangle( auxGC, true, new Rectangle( MARGIN_LEFT + col * BOX_WIDTH + col * OFFSET_BOXES_WIDTH,
				                                                                 OFFSET_TOP + row * BOX_HEIGHT + row * OFFSET_BOXES_HEIGHT,
				                                                                 BOX_WIDTH, BOX_HEIGHT ) );
				// Box margin
				auxGC.RgbFgColor = new Gdk.Color( 0, 0, 0 );
				imageBackground.Pixmap.DrawRectangle( auxGC, false, new Rectangle( MARGIN_LEFT + col * BOX_WIDTH + col * OFFSET_BOXES_WIDTH,
				                                                                 OFFSET_TOP + row * BOX_HEIGHT + row * OFFSET_BOXES_HEIGHT,
				                                                                 BOX_WIDTH, BOX_HEIGHT ) );
				
				// Session label
				LayoutSessionsLabel();
				auxLayout.SetMarkup( "<span color=\"black\">SESIÓN "+session+"</span>" );
				imageBackground.Pixmap.DrawLayout( auxGC, MARGIN_LEFT + col * BOX_WIDTH + col * OFFSET_BOXES_WIDTH,
				                                  OFFSET_TOP + PADDING_BOX_TOP + row * BOX_HEIGHT + row * OFFSET_BOXES_HEIGHT, auxLayout );	
				
				// First row line
				imageBackground.Pixmap.DrawLine( auxGC, MARGIN_LEFT + col * BOX_WIDTH + col * OFFSET_BOXES_WIDTH,
				                                		OFFSET_TOP + row * BOX_HEIGHT + row * OFFSET_BOXES_HEIGHT + BOX_FIRSTROWLINE,
				                                		MARGIN_LEFT + col * BOX_WIDTH + col * OFFSET_BOXES_WIDTH + BOX_WIDTH,
				                                		OFFSET_TOP + row * BOX_HEIGHT + row * OFFSET_BOXES_HEIGHT + BOX_FIRSTROWLINE );
				// Second row line
				/* imageBackground.Pixmap.DrawLine( auxGC, MARGIN_LEFT + col * BOX_WIDTH + col * OFFSET_BOXES_WIDTH,
				                                		OFFSET_TOP + row * BOX_HEIGHT + row * OFFSET_BOXES_HEIGHT + BOX_SECONDBOXLINE,
				                                		MARGIN_LEFT + col * BOX_WIDTH + col * OFFSET_BOXES_WIDTH + BOX_WIDTH,
				                                		OFFSET_TOP + row * BOX_HEIGHT + row * OFFSET_BOXES_HEIGHT + BOX_SECONDBOXLINE);
				
				// First col line
				imageBackground.Pixmap.DrawLine( auxGC, MARGIN_LEFT + col * BOX_WIDTH + col * OFFSET_BOXES_WIDTH + BOX_WIDTH / 3,
				                                		OFFSET_TOP + row * BOX_HEIGHT + row * OFFSET_BOXES_HEIGHT + BOX_FIRSTROWLINE,
				                                		MARGIN_LEFT + col * BOX_WIDTH + col * OFFSET_BOXES_WIDTH + BOX_WIDTH / 3,
				                                		OFFSET_TOP + row * BOX_HEIGHT + row * OFFSET_BOXES_HEIGHT + BOX_HEIGHT);

				imageBackground.Pixmap.DrawLine( auxGC, MARGIN_LEFT + col * BOX_WIDTH + col * OFFSET_BOXES_WIDTH + (BOX_WIDTH / 3) * 2,
				                                		OFFSET_TOP + row * BOX_HEIGHT + row * OFFSET_BOXES_HEIGHT + BOX_FIRSTROWLINE,
				                                		MARGIN_LEFT + col * BOX_WIDTH + col * OFFSET_BOXES_WIDTH + (BOX_WIDTH / 3) * 2,
				                                		OFFSET_TOP + row * BOX_HEIGHT + row * OFFSET_BOXES_HEIGHT + BOX_HEIGHT);
				*/
				// Draw medals
				imageBackground.Pixmap.DrawPixbuf( auxGC, goldPixbufScaled, 0, 0, 
				                                  MARGIN_LEFT + col * BOX_WIDTH + col * OFFSET_BOXES_WIDTH + (BOX_WIDTH/3)*0 + (MEDAL_BOX_WIDTH - MEDAL_WIDTH) / 2,
				                                  OFFSET_TOP + row * BOX_HEIGHT + row * OFFSET_BOXES_HEIGHT + BOX_FIRSTROWLINE + (MEDAL_BOX_HEIGTH - MEDAL_HEIGTH) / 2 + 5,
				                                  MEDAL_WIDTH, MEDAL_HEIGTH, 0, 0, 0  );
				
				imageBackground.Pixmap.DrawPixbuf( auxGC, silverPixbufScaled, 0, 0, 
				                                  MARGIN_LEFT + col * BOX_WIDTH + col * OFFSET_BOXES_WIDTH + (BOX_WIDTH/3)*1 + (MEDAL_BOX_WIDTH - MEDAL_WIDTH) / 2,
				                                  OFFSET_TOP + row * BOX_HEIGHT + row * OFFSET_BOXES_HEIGHT + BOX_FIRSTROWLINE + (MEDAL_BOX_HEIGTH - MEDAL_HEIGTH) / 2 + 5,
				                                  MEDAL_WIDTH, MEDAL_HEIGTH, 0, 0, 0  );
				
				imageBackground.Pixmap.DrawPixbuf( auxGC, bronzePixbufScaled, 0, 0, 
				                                  MARGIN_LEFT + col * BOX_WIDTH + col * OFFSET_BOXES_WIDTH + (BOX_WIDTH/3)*2 + (MEDAL_BOX_WIDTH - MEDAL_WIDTH) / 2,
				                                  OFFSET_TOP + row * BOX_HEIGHT + row * OFFSET_BOXES_HEIGHT + BOX_FIRSTROWLINE + (MEDAL_BOX_HEIGTH - MEDAL_HEIGTH) / 2 + 5,
				                                  MEDAL_WIDTH, MEDAL_HEIGTH, 0, 0, 0  );				
				
				// Draw numbers
				LayoutMedalsNumberLabel();
				int layoutWidth;
				int layoutHeight;
			
				// Draw gold
				auxLayout.GetPixelSize( out layoutWidth, out layoutHeight );
				auxLayout.SetMarkup("<span color=\"gold\">"+medals[MedalValue.Gold]+"</span>");
				imageBackground.Pixmap.DrawLayout( auxGC, 
				                                  MARGIN_LEFT + col * BOX_WIDTH + col * OFFSET_BOXES_WIDTH + (BOX_WIDTH/3)*0,
				                                  OFFSET_TOP + row * BOX_HEIGHT + row * OFFSET_BOXES_HEIGHT + BOX_SECONDBOXLINE + (MEDAL_BOX_HEIGTH - layoutHeight) / 2 + 10,				                                  
				                                  auxLayout );
				// Draw silver
				auxLayout.SetMarkup("<span color=\"grey\">"+medals[MedalValue.Silver]+"</span>");
				imageBackground.Pixmap.DrawLayout( auxGC, 
				                                  MARGIN_LEFT + col * BOX_WIDTH + col * OFFSET_BOXES_WIDTH + (BOX_WIDTH/3)*1,
				                                  OFFSET_TOP + row * BOX_HEIGHT + row * OFFSET_BOXES_HEIGHT + BOX_SECONDBOXLINE + (MEDAL_BOX_HEIGTH - layoutHeight) / 2 + 10,
				                                  auxLayout );
				// Draw bronze
				auxLayout.SetMarkup("<span color=\"brown\">"+medals[MedalValue.Bronze]+"</span>");
				imageBackground.Pixmap.DrawLayout( auxGC, 
				                                  MARGIN_LEFT + col * BOX_WIDTH + col * OFFSET_BOXES_WIDTH + (BOX_WIDTH/3)*2,
				                                  OFFSET_TOP + row * BOX_HEIGHT + row * OFFSET_BOXES_HEIGHT + BOX_SECONDBOXLINE + (MEDAL_BOX_HEIGTH - layoutHeight) / 2 + 10,
				                                  auxLayout );				
	
		}
		
		
		private void BoxClicked(object o, ButtonPressEventArgs args) {
				
			double x = args.Event.X;
			double y = args.Event.Y;
			int col = -1;
			int row = -1;
			int box = -1;
			// Calculate box
			if ( y >= OFFSET_TOP && y < OFFSET_TOP+BOX_HEIGHT*3+OFFSET_BOXES_HEIGHT*2) {
				col = (int) x / ( WIDTH_SCREEN / 4 );
				row = (int) (y - OFFSET_TOP) / (BOX_HEIGHT+OFFSET_BOXES_HEIGHT);				
				box = row*4+col;
			}			
			
			if ( box != -1 && medalBoxes[box].Visible == true ) {
			
				this.HideAll();
				int sessionId = box + 1;
				TotalPodiumPanel auxTotalPodiumPanel = new TotalPodiumPanel( sessionId );
				parentWindow.VBoxMain.Add( auxTotalPodiumPanel );
				auxTotalPodiumPanel.ShowAll();
				auxTotalPodiumPanel.InitPanel();
				auxTotalPodiumPanel.ButtonOK.Label = "Volver al medallero";
				GtkUtil.SetStyle(auxTotalPodiumPanel.ButtonOK, Configuration.Current.MediumFont);
				auxTotalPodiumPanel.SetBalloonText( "Abajo puedes ver las medallas obtenidas en la sesión "+( sessionId )+". ");
				auxTotalPodiumPanel.ButtonOK.Clicked += delegate {					
					this.ShowAll();
					auxTotalPodiumPanel.Destroy();					
				};
				
			}
			
		}
	}
}


