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

namespace pesco
{


	[System.ComponentModel.ToolboxItem(true)]
	public partial class PanelDemoPyramids : Gtk.Bin
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
		int pepeStatus = 0;
		Gdk.GC auxGC;
		Gdk.Pixbuf auxPixbuf1;
		Gdk.Pixbuf auxPixbuf2;
		Gdk.Pixbuf auxPixbuf3;
		Gdk.Pixbuf auxPixbuf4;
		
		// Panel test
		PanelDemoPyramidsTest auxPanelTest;
		int validChecked = 0;

		// Animations
		string stringToSay;
		int currentStep = 0;
		int currentCharacter = 0;
		int totalCharacters = 0;
		List<string> animationsText = new List<string> (7);
		bool firstFrameStep = true;
		
		// Timers
		private int auxAnimationTimer;
		private int auxAnimationTimer2;
		private uint auxTimer;
		private GLib.TimeoutHandler currentHandler;
		private uint currentInterval;
		private bool pausedExercise;
		private const int REPAINT_SPEED = 50;
		int pauseTimer = 100;
		
		private long auxFrames;
		private long auxCycles;
		private long auxTotalCycles;
		
		private GLib.IdleHandler idleHandler;		
		// Layout 
		Pango.Layout auxLayout;

		protected virtual void comenzarEjercicioClicked (object sender, System.EventArgs e)
		{
			EjercicioPiramides ep = EjercicioPiramides.getInstance ();
			ep.iniciar ();
		}

		public PanelDemoPyramids ()
		{
			this.Build ();
			GtkUtil.SetStyle( buttonGoBack, Configuration.Current.MediumFont );
			GtkUtil.SetStyle( buttonGoForward, Configuration.Current.MediumFont );
			GtkUtil.SetStyle( buttonGoLast, Configuration.Current.MediumFont );
			GtkUtil.SetStyle( buttonComenzarEjercicio, Configuration.Current.MediumFont );
			buttonComenzarEjercicio.HideAll();
		}

		private void InitTexts ()
		{
			// 0
			animationsText.Add ("¡Saludos desde Egipto!");
			// 1
			animationsText.Add ("Estoy buscando postales de pirámides y necesito que me ayudes a encontrarlas. Pulsa <span color=\"black\">Siguiente</span>.");
			// 2
			animationsText.Add ("Me interesan aquellas postales que contengan una pirámide grande y dos pirámides pequeñas con puerta en el lado soleado. Por ejemplo, busco postales como las que aparecen abajo. Pulsa <span color=\"black\">Siguiente</span>.");
			// 3
			animationsText.Add ("Fíjate bien, porque he visto postales como las de abajo donde no hay una pirámide grande o pirámides pequeñas que no tienen puerta o que tienen la puerta en el lado de la sombra. Esas no me interesan. Pulsa <span color=\"black\">Siguiente</span>.");
			// 4
			animationsText.Add ("Te mostraré unos paneles y tendrás que marcar las postales con, al menos, una pirámide grande y dos pequeñas con puerta en el lado soleado. Si te equivocas, podrás pulsar para desmarcarlas. Pulsa <span color=\"black\">Siguiente</span>.");
			// 5
			animationsText.Add ("Ahora vamos a hacer una prueba. En el panel de abajo hay 8 postales correctas. No te preocupes si te equivocas, yo te indicaré el motivo. ¡Inténtalo ahora!");
			// 6
			animationsText.Add ("¡Perfecto! Ya sabes cómo son las postales que busco. Ahora podemos empezar. El ejercicio durará 5 minutos, trabaja tan rápido como puedas sin cometer errores. ¡Pulsa en <span color=\"black\">Comenzar ejercicio</span> cuando estés preparado!");
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

		public void InitPanel ()
		{
			
			InitTexts ();
			InitLayout ();
			
			auxPanelTest = new PanelDemoPyramidsTest(this);

			imageBackground.ExposeEvent += Expose_Event;
			
			backgroundPixbuf = Gdk.Pixbuf.LoadFromResource ("pesco.ejercicios.Piramides.img.background.png");
			dialogPixbuf = Gdk.Pixbuf.LoadFromResource ("pesco.ejercicios.Piramides.img.dialog.png");
			pepe1Pixbuf = Gdk.Pixbuf.LoadFromResource ("pesco.ejercicios.Piramides.img.camel1.png");
			pepe2Pixbuf = Gdk.Pixbuf.LoadFromResource ("pesco.ejercicios.Piramides.img.camel2.png");
			
			imageBackground.Pixmap = new Gdk.Pixmap (fixed1.GdkWindow, WIDTH_SCREEN, HEIGHT_SCREEN, 24);
			auxGC = new Gdk.GC(imageBackground.Pixmap);
						
			/*
			currentHandler = new GLib.TimeoutHandler (Update);
			currentInterval = 25;
			auxTimer = GLib.Timeout.Add (currentInterval, currentHandler);
			*/
			idleHandler = new GLib.IdleHandler( Update );
			auxTimer = GLib.Idle.Add (idleHandler);
			
		}

		void Expose_Event (object obj, ExposeEventArgs args)
		{
			// Draw background
			DrawDialogBackground();
			long auxLong = System.Diagnostics.Stopwatch.GetTimestamp();
			// Hello
			if (currentStep == 0) {				
				if ( FirstFrameStep() ) {
					buttonGoBack.Sensitive = false;
					totalCharacters = animationsText[currentStep].Length;
					stringToSay = animationsText[currentStep];
					buttonComenzarEjercicio.HideAll();
					buttonGoLast.HideAll();
				}
				PepeUtils.IncrementCharacterDialog( ref currentCharacter, ref pepeStatus, stringToSay );
				DrawDialog();
				auxLayout.SetMarkup( "<span color=\"blue\">"+stringToSay.Substring(0, currentCharacter)+"</span>" );
			} 
			// Explanation 1
			else if ( currentStep == 1 ) {
				if ( FirstFrameStep() ) {
					buttonGoBack.Sensitive = true;
					totalCharacters = animationsText[currentStep].Length;
					stringToSay = animationsText[currentStep];
					auxLayout.FontDescription = Pango.FontDescription.FromString ("Ahafoni CLM Bold 24");
				}
				PepeUtils.IncrementCharacterDialog( ref currentCharacter, ref pepeStatus, stringToSay );
				DrawDialog();
				auxLayout.SetMarkup( "<span color=\"blue\">"+stringToSay.Substring(0, currentCharacter)+"</span>" );				
			}
			// Explanation 2: Pyramids OK
			else if ( currentStep == 2 ) {
				if ( FirstFrameStep() ) {
					totalCharacters = animationsText[currentStep].Length;
					stringToSay = animationsText[currentStep];
					auxLayout.FontDescription = Pango.FontDescription.FromString ("Ahafoni CLM Bold 20");					
					Gtk.Image auxImage = new Gtk.Image( Configuration.CommandExercisesDirectory + System.IO.Path.DirectorySeparatorChar + 
						"Piramides" + System.IO.Path.DirectorySeparatorChar + "figuras" + System.IO.Path.DirectorySeparatorChar +
						"c1.jpg");
					auxPixbuf1 = auxImage.Pixbuf;
					auxImage = new Gtk.Image( Configuration.CommandExercisesDirectory + System.IO.Path.DirectorySeparatorChar + 
						"Piramides" + System.IO.Path.DirectorySeparatorChar + "figuras" + System.IO.Path.DirectorySeparatorChar +
						"c2.jpg");	
					auxPixbuf2 = auxImage.Pixbuf;
					auxImage = new Gtk.Image( Configuration.CommandExercisesDirectory + System.IO.Path.DirectorySeparatorChar + 
						"Piramides" + System.IO.Path.DirectorySeparatorChar + "figuras" + System.IO.Path.DirectorySeparatorChar +
						"c3.jpg");						
					auxPixbuf3 = auxImage.Pixbuf;
					auxPixbuf1 = auxPixbuf1.ScaleSimple(200,200,Gdk.InterpType.Bilinear);
					auxPixbuf2 = auxPixbuf2.ScaleSimple(200,200,Gdk.InterpType.Bilinear);					
					auxPixbuf3 = auxPixbuf3.ScaleSimple(200,200,Gdk.InterpType.Bilinear);	
				}
				PepeUtils.IncrementCharacterDialog( ref currentCharacter, ref pepeStatus, stringToSay );
				DrawDialog();
				auxLayout.SetMarkup( "<span color=\"blue\">"+stringToSay.Substring(0, currentCharacter)+"</span>" );
				imageBackground.Pixmap.DrawPixbuf( auxGC, auxPixbuf1, 0, 0, 275, 300, 200, 200,0 ,0, 0 );
				imageBackground.Pixmap.DrawPixbuf( auxGC, auxPixbuf2, 0, 0, 500, 300, 200, 200,0 ,0, 0 );
				imageBackground.Pixmap.DrawPixbuf( auxGC, auxPixbuf3, 0, 0, 725, 300, 200, 200,0 ,0, 0 );
			}
			// Explanation: Pyramids BAD
			else if ( currentStep == 3 ) {
				if ( FirstFrameStep() ) {
					totalCharacters = animationsText[currentStep].Length;
					stringToSay = animationsText[currentStep];
					Gtk.Image auxImage = new Gtk.Image( Configuration.CommandExercisesDirectory + System.IO.Path.DirectorySeparatorChar + 
						"Piramides" + System.IO.Path.DirectorySeparatorChar + "figuras" + System.IO.Path.DirectorySeparatorChar +
						"d20.jpg");						
					auxPixbuf1 = auxImage.Pixbuf;
					auxImage = new Gtk.Image( Configuration.CommandExercisesDirectory + System.IO.Path.DirectorySeparatorChar + 
						"Piramides" + System.IO.Path.DirectorySeparatorChar + "figuras" + System.IO.Path.DirectorySeparatorChar +
						"d16.jpg");						
					auxPixbuf2 = auxImage.Pixbuf;
					auxImage = new Gtk.Image( Configuration.CommandExercisesDirectory + System.IO.Path.DirectorySeparatorChar + 
						"Piramides" + System.IO.Path.DirectorySeparatorChar + "figuras" + System.IO.Path.DirectorySeparatorChar +
						"d29.jpg");						
					auxPixbuf3 = auxImage.Pixbuf;
					auxPixbuf1 = auxPixbuf1.ScaleSimple(200,200,Gdk.InterpType.Bilinear);
					auxPixbuf2 = auxPixbuf2.ScaleSimple(200,200,Gdk.InterpType.Bilinear);					
					auxPixbuf3 = auxPixbuf3.ScaleSimple(200,200,Gdk.InterpType.Bilinear);	
				}
				auxLayout.SetMarkup( "<span color=\"blue\">"+stringToSay.Substring(0, currentCharacter)+"</span>" );
				PepeUtils.IncrementCharacterDialog( ref currentCharacter, ref pepeStatus, stringToSay );
				DrawDialog();
				if ( currentCharacter > animationsText[currentStep].Length - 1 ) {
					imageBackground.Pixmap.DrawPixbuf( auxGC, auxPixbuf1, 0, 0, 275, 300, 200, 200,0 ,0, 0 );
					imageBackground.Pixmap.DrawPixbuf( auxGC, auxPixbuf2, 0, 0, 500, 300, 200, 200,0 ,0, 0 );
					imageBackground.Pixmap.DrawPixbuf( auxGC, auxPixbuf3, 0, 0, 725, 300, 200, 200,0 ,0, 0 );
				}
			}
			// Explanation: PreTest
			else if ( currentStep == 4 ) {
				if ( FirstFrameStep() ) {
					totalCharacters = animationsText[currentStep].Length;
					stringToSay = animationsText[currentStep];
					buttonGoForward.Sensitive = true;
					fixed1.Remove( auxPanelTest );
				}
				auxLayout.SetMarkup( "<span color=\"blue\">"+stringToSay.Substring(0, currentCharacter)+"</span>" );
				PepeUtils.IncrementCharacterDialog( ref currentCharacter, ref pepeStatus, stringToSay );
				DrawDialog();
			}
			// Test
			else if ( currentStep == 5 ) {
				if ( FirstFrameStep() ) {
					stringToSay = animationsText[currentStep];
					totalCharacters = animationsText[currentStep].Length;
					fixed1.Put( auxPanelTest, 300, 250 );
					fixed1.ShowAll();						
					buttonGoForward.Sensitive = false;
				}
				PepeUtils.IncrementCharacterDialog( ref currentCharacter, ref pepeStatus, stringToSay );
				DrawDialog();
				auxLayout.SetMarkup( "<span color=\"blue\">"+stringToSay.Substring(0, currentCharacter)+"</span>" );						
			} else if ( currentStep == 6 ) {
				if ( FirstFrameStep() ) {
					stringToSay = animationsText[currentStep];
					totalCharacters = animationsText[currentStep].Length;
					buttonComenzarEjercicio.ShowAll();
					buttonGoBack.HideAll();
					buttonGoForward.HideAll();
					auxPanelTest.HideAll();
					PepeUtils.GenerateCartel( this.PangoContext, auxGC, imageBackground.Pixmap, "¡Ensayo superado! Comienza el ejercicio" );
					
				}
				PepeUtils.DrawCartel();
				auxLayout.SetMarkup( "<span color=\"blue\">"+stringToSay.Substring(0, currentCharacter )+"</span>" );
				// PepeUtils.IncrementCharacterDialog( ref currentCharacter, ref pepeStatus, stringToSay );
				// DrawDialog();
			}
					
			// Draw text
			imageBackground.Pixmap.DrawLayout( auxGC,200, 37, auxLayout );
			
			// Cycles counter
			/*
			auxFrames++;
			long auxDif = (System.Diagnostics.Stopwatch.GetTimestamp()-auxLong);
			auxTotalCycles += auxDif;
			if ( auxFrames == SessionManager.GetInstance().Fps ) {
				Console.WriteLine( "Ciclos por segundo:"+auxTotalCycles+" | Velocidad PC: "+System.Diagnostics.Stopwatch.Frequency ) ;				
				auxFrames = 0;
				auxTotalCycles = 0;
			}*/
		}
		
		private void DrawDialogBackground() {
		
			imageBackground.Pixmap.DrawPixbuf( auxGC, backgroundPixbuf, 0, 0, 0, 0, WIDTH_SCREEN, HEIGHT_SCREEN, 0, 0, 0);
			
		}
			
		private void DrawDialog() {
			
			imageBackground.Pixmap.DrawPixbuf( auxGC, dialogPixbuf, 0, 0, 0, 0, WIDTH_SCREEN, HEIGHT_SCREEN, 0, 0, 0);
			if ( pepeStatus > 2 ) {
				imageBackground.Pixmap.DrawPixbuf( auxGC, pepe1Pixbuf, 0, 0, 0, 165, pepe1Pixbuf.Width, pepe1Pixbuf.Height, 0, 0, 0);	
			} else {
				imageBackground.Pixmap.DrawPixbuf( auxGC, pepe2Pixbuf, 0, 0, 0, 165, pepe2Pixbuf.Width, pepe2Pixbuf.Height, 0, 0, 0);		
			}				
			
		}
		
		public void SayGreat(string text) {		
			validChecked++;
			if ( validChecked < 3 ) {
				stringToSay = "¡Bien! Ya sólo faltan "+( 8 - validChecked)+" postales";
			} else if ( validChecked < 5 ) {
				stringToSay = "¡Genial! Ya sólo faltan "+( 8 - validChecked)+" postales";
			} else if ( validChecked < 7 ) {				
				stringToSay = "¡Ya casi las tienes! Sólo faltan "+( 8 - validChecked)+" postales";
			} else if ( validChecked < 8 ) {
				stringToSay = "¡Sólo falta una!";
			} else {
				NextStep();
			}
			totalCharacters = stringToSay.Length;
			currentCharacter = 0;	
		}
		
		public void SayBad( string text ) {
			stringToSay = text;
			totalCharacters = stringToSay.Length;
			currentCharacter = 0;
		}
		
		bool Update() {		
			
			long currentTicks = System.Diagnostics.Stopwatch.GetTimestamp();
			long difference = currentTicks - SessionManager.GetInstance().CurrenTicks;
			long interval = SessionManager.GetInstance().Interval;
						
			if ( difference > interval ) {
				imageBackground.QueueDraw();
				SessionManager.GetInstance().UpdateCurrenTicks();
			}			
			
			return true;			
		}
		
		private bool FirstFrameStep ()
		{
			if (firstFrameStep == true) {
				firstFrameStep = false;
				return true;
			}
			
			return false;
		}

		private bool AfterLastFrameStep()
		{
			if ( currentCharacter >= totalCharacters ) {			
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
		
		private void GoLast() {
			currentStep = animationsText.Count - 1;
			currentCharacter = 0;
			firstFrameStep = true;
		}
		
		protected virtual void OnButtonGoLastClicked (object sender, System.EventArgs e)
		{
			GoLast();
		}
		
		protected virtual void OnButtonGoBackClicked (object sender, System.EventArgs e)
		{
			BackStep();
		}
		
		protected virtual void OnButtonGoForwardClicked (object sender, System.EventArgs e)
		{
			NextStep();
		}
		
		
		
		
	}
}

