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
using Cairo;
using Gtk;
using Gdk;

namespace pesco
{
	
	[System.ComponentModel.ToolboxItem(true)]
	public partial class PodiumPanel : Gtk.Bin
	{
	
		const int WIDTH_SCREEN = 1000;
		const int HEIGHT_SCREEN = 600;

		// Draw elements
		Gtk.DrawingArea drawingArea = new Gtk.DrawingArea();
		private Gdk.Pixbuf bgPixbuf;
		private Gdk.Pixbuf backgroundPixbuf;
		private Gdk.Pixbuf dialogPixbuf;
		private Gdk.Pixbuf pepe1Pixbuf;
		private Gdk.Pixbuf pepe2Pixbuf;
		private Gdk.Pixbuf medalPixbuf;
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
		string balloonText = "¡Enhorabuena! Has completado el ejercicio y has obtenido una medalla de... ";

		bool firstFrameStep = true;
		
		// Timers
		private int auxAnimationTimer;
		private uint auxTimer;
		private GLib.TimeoutHandler currentHandler;
		private uint currentInterval;
		private bool pausedExercise;
		private const int REPAINT_SPEED = 50;
		private int pauseTimer = 15;

		// Layout 
		Pango.Layout auxLayout;

		// Medals elements
		private int currentMedal = 0;
		private string [] medalPixbux = {"pesco.ejercicios.figures.medalla_oro.png",
								"pesco.ejercicios.figures.medalla_plata.png",
								"pesco.ejercicios.figures.medalla_bronce.png"};
		private string [] medalName = {"Oro", "Plata", "Bronce"};
		private string [] medalText = {"¡Lo has hecho estupendamente!\nSigue ejercitando así tu cerebro",
								"¡Lo has hecho estupendamente! Con un poco más\nde entrenamiento seguro que consigues el oro ¡Ánimo!",
								"¡No está nada mal! Sigue ejercitando así\ntu cerebro y pronto llegarás a la plata y el oro"};
		private string [] medalColor = {"gold", "white", "brown"};
			
		public Gtk.Button ButtonOK{
			get{
				return this.buttonOK;	
			}
		}
		
		public string BalloonText {
			get {
				return balloonText;
			}
			set {
				balloonText = value;
			}
		}
		
		public PodiumPanel ()
		{
			this.Build ();
			currentMedal = 0;
		}
		
		~PodiumPanel() {

		}
		
		public PodiumPanel (int score)
		{
			this.Build ();
			GtkUtil.SetStyle( buttonOK, Configuration.Current.MediumFont );
			if ( score < 50 ) {
				currentMedal = 2;
			} else if ( score >= 50 && score < 80 ) {
				currentMedal = 1;	
			} else if ( score >= 80 && score <= 100 ) {
				currentMedal = 0;	
			}
			InitPanel();
			this.Destroyed += delegate {
				if ( cairoCMedal != null )
					((IDisposable) cairoCMedal).Dispose(); 
				if ( cairoCPepe != null )
					((IDisposable) cairoCPepe).Dispose();
				if ( auxTimer != null ) 
					GLib.Source.Remove(auxTimer);		
			};
		}		

		private void InitLayout ()
		{
			
			auxLayout = new Pango.Layout (this.CreatePangoContext ());
			auxLayout.Width = Pango.Units.FromPixels (720);
			auxLayout.Justify = false;
			auxLayout.Wrap = Pango.WrapMode.Word;
			auxLayout.Alignment = Pango.Alignment.Left;
			auxLayout.FontDescription = Pango.FontDescription.FromString ("Ahafoni CLM Bold 26");
			
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
		
		public void DestroyPanel() {
			
		
		}
		
		public void InitPanel ()
		{

			InitLayout ();

			GtkUtil.SetStyle( buttonOK, Configuration.Current.MediumFont );
			
			imageBackground.ExposeEvent += Expose_Event;

			backgroundPixbuf = Gdk.Pixbuf.LoadFromResource ("pesco.ejercicios.resources.img.background.png");
			dialogPixbuf = Gdk.Pixbuf.LoadFromResource ("pesco.ejercicios.resources.img.dialog.png");
			pepe1Pixbuf = Gdk.Pixbuf.LoadFromResource ("pesco.ejercicios.resources.img.pepe1.png");
			pepe2Pixbuf = Gdk.Pixbuf.LoadFromResource ("pesco.ejercicios.resources.img.pepe2.png");
			
			if ( currentMedal == 0 ) 
				medalPixbuf = Gdk.Pixbuf.LoadFromResource ("pesco.ejercicios.figures.medalla_oro.png" );
			else if ( currentMedal == 1 ) 
				medalPixbuf = Gdk.Pixbuf.LoadFromResource ("pesco.ejercicios.figures.medalla_plata.png" );
			else if ( currentMedal == 2 )
				medalPixbuf = Gdk.Pixbuf.LoadFromResource ("pesco.ejercicios.figures.medalla_bronce.png" );
			
			imageBackground.Pixmap = new Gdk.Pixmap ( hboxCentral.GdkWindow, WIDTH_SCREEN, HEIGHT_SCREEN, 24);
			
			auxGC = new Gdk.GC(imageBackground.Pixmap);
			DrawDialogBackground();
			
			cairoCMedal = Gdk.CairoHelper.Create ( imageBackground.Pixmap );
			cairoCPepe = Gdk.CairoHelper.Create ( imageBackground.Pixmap );

			if ( auxTimer != null && auxTimer > 0 ) 
				GLib.Source.Remove(auxTimer);
			
			currentHandler = new GLib.TimeoutHandler (Update);
			currentInterval = REPAINT_SPEED;
			auxTimer = GLib.Timeout.Add (currentInterval, currentHandler);

		}

		void Expose_Event (object obj, ExposeEventArgs args)
		{
			// Draw background
			DrawDialogBackground();		
			
			// Congratulations!
			if (currentStep == 0) {				
				if ( FirstFrameStep() ) {
					totalCharacters = balloonText.Length;
					stringToSay = balloonText;
					pauseTimer = 1;
					buttonOK.Sensitive = false;
				}
				if ( AfterLastFrameStep() ) {
					pauseTimer--;	
				}
				if ( currentCharacter > 10 ) {
					// Draw Medal
					DrawMedal();	
				}
				// Balloon text
				PepeUtils.IncrementCharacterDialog(ref currentCharacter, ref pepeStatus, stringToSay);
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
					stringToSay = balloonText;					
				}
				// Balloon text
				InitLayout();
				auxLayout.SetMarkup( "<span color=\"blue\">"+stringToSay+"</span>" );
				imageBackground.Pixmap.DrawLayout( auxGC, 200, 40, auxLayout );	
				// Draw Pepe
				DrawPepeTalking();			
				// Draw Medal
				DrawMedal();
				if ( !auxIncreasing ) {
					NextStep();	
				}
			}
			// After medal shown
			else if ( currentStep == 2 ) {
				if ( FirstFrameStep() ) {
					stringToSay = balloonText;
				}
				else {
					DrawPepeJumping();
					buttonOK.Sensitive = true;
				}
				// Medal
				DrawMedal();
				// Balloon text
				InitLayout();
				auxLayout.SetMarkup( "<span color=\"blue\">"+stringToSay+"</span>" );
				imageBackground.Pixmap.DrawLayout( auxGC, 200, 40, auxLayout );	
				// Gold text
				MedalLayout();
				auxLayout.SetMarkup( "<span color=\""+medalColor[currentMedal]+"\">"+medalName[currentMedal]+"</span>" );
				imageBackground.Pixmap.DrawLayout( auxGC, 140, 480, auxLayout );
				// Sub text
				MedalSubTextLayout();
				auxLayout.SetMarkup( "<span color=\""+medalColor[currentMedal]+"\">"+medalText[currentMedal]+"</span>" );
				imageBackground.Pixmap.DrawLayout( auxGC, 140, 535, auxLayout );				
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
		
		private void IncrementCharacterDialog() {
			
			if ( currentCharacter < totalCharacters-1 ) {
				if ( stringToSay[currentCharacter] == '<' && stringToSay[currentCharacter+1] == 's') {
					while ( stringToSay[currentCharacter-1] != 'n' || stringToSay[currentCharacter] != '>' ) {
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
		
		bool Update() {
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
					auxFactor += 0.01;	
				} else {
					auxFactor -= 0.01;	
				}
				
				if ( auxFactor >= 1 ) {
					auxIncreasing = false;
				} 
				if ( !auxIncreasing && auxFactor <= 0.9 ) {
					auxIncreasing = true;
				}
				
				cairoCMedal.Matrix = new Matrix();
				cairoCMedal.Translate( 442+((256.0-(256.0*auxFactor))/2), 222+((256.0-(256.0*auxFactor))/2) );
				cairoCMedal.Scale( auxFactor, auxFactor );								
				CairoHelper.SetSourcePixbuf (cairoCMedal, medalPixbuf, 0, 0);
				cairoCMedal.PaintWithAlpha (auxAlpha);
		}
		
		private void DrawPepe() {

				cairoCPepe.Matrix = new Matrix();					                           
				cairoCPepe.Translate( 0, 600 - (pepe1Pixbuf.Height) );
				CairoHelper.SetSourcePixbuf (cairoCPepe, pepe1Pixbuf, 0, 0);
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
				cairoCPepe.Matrix = new Matrix();            
				cairoCPepe.Translate( 0, 600 - (pepe1Pixbuf.Height) - auxFactor2 );											
				CairoHelper.SetSourcePixbuf (cairoCPepe, pepe1Pixbuf, 0, 0);
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
	}
}

