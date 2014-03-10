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
using System.IO;
using Gtk;
using System.Collections.Generic;

namespace pesco
{

	[System.ComponentModel.ToolboxItem(true)]
	public partial class BalloonsPanel : Gtk.Bin
	{
		// Exercise instance
		BalloonsExercise exerciseInstance = null;

		#region Panel constants
		const int WIDTH_BALLOON = 171;
		const int HEIGHT_BALLOON = 230;

		public int WIDTH_SCREEN = 1000;
		public int HEIGHT_SCREEN = 600;
		#endregion

		#region Exercise constants
		// const int balloonsPerSerie = 40;
		// 1000 ms / 60 fps ~= 17ms per frame
		const int MSPERFRAME = 17;
		#endregion

		#region Timers variables
		private int auxAnimationTimer;
		private uint auxTimer;
		// private GLib.IdleHandler currentHandler;
		private GLib.TimeoutHandler currentHandler;
		private uint currentInterval;
		private bool pausedExercise = false;
		#endregion

		#region Time control
		public DateTime Start;
		public DateTime End;
		#endregion

		#region Cycles counter
		private long auxFrames;
		private long auxCycles;
		private long auxTotalCycles;
		#endregion

		Random r = new Random (DateTime.Now.Millisecond);

		#region Exercise variables
		string currentMode = "demo";

		private Gdk.Pixmap bufferPixmap;
		private Gdk.Pixmap auxPixmap;
		private Gdk.Pixmap backgroundPixmap;
		private Gdk.Pixmap pepePixmap;
		private Gdk.Pixbuf fondoPixbuf;

		private Balloon[] balloonTest;
		// = new Balloon[balloonsPerSerie];
		private char[] characters;
		// = new char[balloonsPerSerie];
		private List<int> currentDisplayedBalloons = new List<int> ();
		private int lastBalloonShowed = 0;
		private int currentLevel;
		#endregion

		#region Demo elements
		private bool stopAnimation = false;
		private bool firstExecutionAfterPause = false;
		private bool buttonPulsarClicked = false;
		private bool startedSerie = false;
		private bool finishedSerie = false;

		// Auxiliar graphics elements
		private Pango.Layout auxLayout;
		private Gdk.GC auxGC;

		// Demo
		private Gdk.Pixbuf dialogPixbuf;
		private Gdk.Pixbuf pepe1Pixbuf;
		private Gdk.Pixbuf pepe2Pixbuf;
		// private Gdk.Pixbuf ballonPixbuf;

		int pepeStatus = 0;
		Balloon balloonDemo1;
		Balloon balloonDemo2;
		char[] balloonsCharDemo;
		char[] balloonsCharDemo1 = { 'D', 'B', 'B', 'F', 'A', 'C', 'C', 'C', 'H', 'H',
		'E', 'R', 'K', 'K', 'F', 'Y', 'W', 'W', 'J', 'O',
		'V', 'X', 'X', 'U', 'U', 'T', 'B', 'S', 'S', 'L',
		'A', 'M', 'M', 'I', 'E', 'Q', 'E', 'G', 'F', 'F' };
		char[] balloonsCharDemo2 = { 'D', 'B', 'H', 'B', 'A', 'C', 'Z', 'C', 'E', 'H',
		'B', 'H', 'K', 'K', 'F', 'Y', 'W', 'J', 'W', 'O',
		'V', 'X', 'X', 'U', 'T', 'U', 'B', 'L', 'S', 'D',
		'S', 'M', 'I', 'M', 'E', 'Q', 'E', 'G', 'F', 'F' };
		char[] balloonsCharDemo3 = { 'D', 'B', 'H', 'A', 'B', 'C', 'Z', 'E', 'C', 'B',
		'H', 'I', 'K', 'H', 'F', 'Y', 'W', 'J', 'Y', 'O',
		'J', 'X', 'X', 'U', 'T', 'B', 'U', 'L', 'S', 'D',
		'M', 'S', 'I', 'E', 'T', 'Q', 'E', 'G', 'F', 'F',
		'G' };

		int currentCharDemo = 0;
		char currentBalloon;
		int demoCharCorrects = 0;
		#endregion

		#region Level animations
		// Animations
		string stringToSay;
		int currentStep = 0;
		int currentCharacter = 0;
		int totalCharacters = 0;
		string[] animationsText;
		string[] animationsTextLevel1 = { "¡Bienvenido al ejercicio de los globos! Este ejercicio va a entrenar tu capacidad de atención. En la pantalla van a ir apareciendo una serie de globos con una letra dentro.", 		/*2*/"En el primer nivel tu tarea consistirá en pulsar el botón izquierdo del ratón cuando aparezcan dos o más globos seguidos con la misma letra. El fondo se pondrá gris cada vez que pulses el botón izquierdo.", 		/*3*/"Ahora vamos a hacer un ensayo. Cuando veas dos globos seguidos con la misma letra, pulsa el botón izquierdo del ratón. Tienes que acertar 3 veces.", 		/*4*/"", 		/*5*/"¡Perfecto! ¡Has superado el ensayo! Ahora puedes realizar el ejercicio. Para ello, pulsa el botón <span color=\"black\">Comenzar ejercicio</span> y después haz clic en cualquier parte de la pantalla." };

		string[] animationsTextLevel2 = { "¡Bienvenido de nuevo al ejercicio de los globos! Estás en el nivel 2 del ejercicio. Por eso tienes que fijarte en globos que aparezcan 2 posiciones antes.", 		/*2*/"Por ejemplo, tendrás que pulsar el botón si aparece la secuencia A, B, A, pues aparece una A dos posiciones después, pero NO tendrás que pulsar si aparecen dos A seguidas.", 		/*3*/"Ahora vamos a hacer un ensayo. Cuando veas un globo igual al que apareció dos posiciones antes, por ejemplo la secuencia A,B,A, pulsa el botón izquierdo del ratón. Tienes que acertar 3 veces.", 		/*4*/"", 		/*5*/"¡Perfecto! ¡Has superado el ensayo! Ahora puedes realizar el ejercicio. Para ello pulsa el botón <span color=\"black\">Comenzar ejercicio</span> y después haz pulsa el botón izquierdo del ratón." };

		string[] animationsTextLevel3 = { "¡Bienvenido de nuevo al ejercicio de los globos! Estás en el nivel 3 del ejercicio. Por eso esta vez tienes que fijarte en globos que aparezcan 3 posiciones antes.", 		/*2*/"Por ejemplo, tendrás que pulsar el botón si aparece la secuencia A, B, C, A, pues aparece una A tres posiciones después, pero NO tendrás que pulsar si aparece la secuencia A,A o la secuencia A,B,A.", 		/*3*/"Ahora vamos a hacer un ensayo. Fíjate en los globos que van a aparecer. Cuando veas un globo igual al que apareció tres posiciones antes, por ejemplo la secuencia A,B,C,A, pulsa el botón izquierdo del ratón. Tienes que acertar 3 veces.", 		/*4*/"", 		/*5*/"¡Perfecto! ¡Has superado el ensayo! Ahora puedes realizar el ejercicio. Para ello pulsa el <span color=\"black\">botón Comenzar ejercicio</span> y después pulsa el botón del ratón." };

		int pauseTimer = 200;
		bool firstFrameStep = true;
		bool lastFrameStep = false;
		bool demoLearnedOk = false;
		#endregion
		public BalloonsPanel ()
		{
			this.Build ();
			GtkUtil.SetStyle (buttonPulsar, Configuration.Current.LargeFont);
			GtkUtil.SetStyle (labelLevelString, Configuration.Current.MediumFont);
			GtkUtil.SetStyle (labelLevel, Configuration.Current.MediumFont);
			GtkUtil.SetStyle (labelBalloonsString, Configuration.Current.MediumFont);
			GtkUtil.SetStyle (labelBalloons, Configuration.Current.MediumFont);
			GtkUtil.SetStyle (labelVelocity, Configuration.Current.MediumFont);
			GtkUtil.SetStyle (labelVelocityString, Configuration.Current.MediumFont);
			GtkUtil.SetStyle (buttonGoBack, Configuration.Current.MediumFont);
			GtkUtil.SetStyle (buttonGoForward, Configuration.Current.MediumFont);
			GtkUtil.SetStyle (buttonGoLast, Configuration.Current.MediumFont);
			GtkUtil.SetStyle (buttonStartExercise, Configuration.Current.MediumFont);
			
			labelBalloonsString.HideAll ();
			labelBalloons.HideAll ();
			
			// Variable to access exercise instance
			exerciseInstance = BalloonsExercise.getInstance ();
			
			// Event mask
			// drawingAreaGame.AddEvents ((int) Gdk.EventMask.ButtonPressMask);
			eventBox.ButtonPressEvent += callbackButtonPulsar;
			
			buttonPulsar.HideAll ();
		}

		// Drawing function
		void Expose_Event (object obj, ExposeEventArgs args)
		{
			
			if (currentMode == "game")
				DrawGame (); 
			else if (currentMode == "demo")
				DrawDemo ();
			
		}

		// Draw game
		public void DrawGame ()
		{
			
			if (!stopAnimation) {
				DrawBackground ();
				for (int i = 0; i < currentDisplayedBalloons.Count; i++) {
					bufferPixmap.DrawPixbuf (this.drawingAreaGame.Style.BaseGC (StateType.Normal), balloonTest[currentDisplayedBalloons[i]].pixbufBalloon, 0, 0, balloonTest[currentDisplayedBalloons[i]].PosX, balloonTest[currentDisplayedBalloons[i]].PosY, WIDTH_BALLOON, HEIGHT_BALLOON, 0, 0,
					0);
				}
				auxPixmap.DrawDrawable (auxGC, bufferPixmap, 0, 0, 0, 0, WIDTH_SCREEN, HEIGHT_SCREEN);
				
			}
			// TODO: Make some graphical change to notice user that screen has been clicked already
		}
				
			
		
		public void DrawBackground ()
		{
			/*
			bufferPixmap.DrawPixbuf (this.drawingAreaGame.Style.BaseGC (StateType.Normal), fondoPixbuf, 0, 0, 0, 0, WIDTH_SCREEN, HEIGHT_SCREEN, 0, 0,
			0);*/
			if ( !buttonPulsarClicked )
				auxGC.RgbFgColor = new Gdk.Color (186, 235, 255);
			else 
				auxGC.RgbFgColor = new Gdk.Color (220, 220, 220);
			
			bufferPixmap.DrawRectangle (auxGC, true, new Gdk.Rectangle (0, 0, 1000, 600));
		}

		public void InitVariables ()
		{
			
			// Screen
			int auxWidth;
			int auxHeight;
			
			this.drawingAreaGame.ExposeEvent += Expose_Event;
			
			// Load pixbufs
			Gtk.Image auxImage = new Gtk.Image (Configuration.CommandExercisesDirectory + System.IO.Path.DirectorySeparatorChar + "Balloons" + System.IO.Path.DirectorySeparatorChar + "img" + System.IO.Path.DirectorySeparatorChar + "balloons-background-8bits.png");
			fondoPixbuf = auxImage.Pixbuf;
			fondoPixbuf = fondoPixbuf.ScaleSimple (WIDTH_SCREEN, HEIGHT_SCREEN, Gdk.InterpType.Bilinear);
			
			// Generate auxiliar layout
			auxLayout = new Pango.Layout (this.drawingAreaGame.CreatePangoContext ());
			auxLayout.Width = Pango.Units.FromPixels (720);
			auxLayout.Wrap = Pango.WrapMode.Word;
			auxLayout.Alignment = Pango.Alignment.Left;
			auxLayout.FontDescription = Pango.FontDescription.FromString ("Ahafoni CLM Bold 60");
			
			// Create pixmaps
			bufferPixmap = new Gdk.Pixmap (this.drawingAreaGame.GdkWindow, WIDTH_SCREEN, HEIGHT_SCREEN, 24);
			auxPixmap = new Gdk.Pixmap (this.drawingAreaGame.GdkWindow, WIDTH_SCREEN, HEIGHT_SCREEN, 24);
			backgroundPixmap = new Gdk.Pixmap (this.drawingAreaGame.GdkWindow, WIDTH_SCREEN, HEIGHT_SCREEN, 24);
			pepePixmap = new Gdk.Pixmap (this.drawingAreaGame.GdkWindow, WIDTH_SCREEN, HEIGHT_SCREEN, 24);
			
			
			// Initialize graphics context
			auxGC = new Gdk.GC (bufferPixmap);
			
			// Define auxiliar pixmap as drawing area background
			this.drawingAreaGame.GdkWindow.SetBackPixmap (auxPixmap, false);
			
			// Level
			currentLevel = BalloonsExercise.getInstance ().CurrentLevel;
			
		}

		public void NextExecution ()
		{
			
			// Set labels			
			labelLevel.Markup = "<span size=\"x-large\" color=\"red\">" + Convert.ToString (BalloonsExercise.getInstance ().CurrentLevel) + "</span>";
			labelBalloons.Markup = "<span size=\"x-large\" color=\"red\">" + Convert.ToString (0) + "</span>";
			labelVelocity.Markup = "<span size=\"x-large\" color=\"red\">" + Convert.ToString (BalloonsExercise.getInstance ().CurrentSubLevel + 1) + "</span>";
			
			// Generate seed			
			int randomChar = ListUtils.NextInt (0, 26);
			int valids = 1;
			int repeats = 0;
			
			// Get ballons per serie according to level velocity
			int balloonsPerSerie = exerciseInstance.ballonsPerSubLevel[exerciseInstance.CurrentSubLevel];
			balloonTest = new Balloon[balloonsPerSerie];
			characters = new char[balloonsPerSerie];
			
			// Generate Balloons
			while (valids < exerciseInstance.ballonsPerSubLevel[exerciseInstance.CurrentSubLevel] * 0.20 || valids > exerciseInstance.ballonsPerSubLevel[exerciseInstance.CurrentSubLevel] * 0.40) {
				repeats = 0;
				valids = 1;
				for (int i = 0; i < balloonTest.Length; i++) {
					// Approximately 66% of the sequences will be incorrect
					// We have to check if we have enough ballons to repeat the last (i > currentLevel)
					// probability = (double) ( (double) (balloonsPerSerie/valids)/ (double) balloonsPerSerie);					
					// Console.Write(repeats+",");
					if (ListUtils.NextInt (0, 100) > 33 || i < currentLevel || repeats > 2) {
						repeats = 0;
						randomChar = ListUtils.NextInt (0, 26);
						if (i > currentLevel) {
							while (randomChar == balloonTest[i - currentLevel].Character - 'A') {
								randomChar = ListUtils.NextInt (0, 26);
							}
						}
					} else {
						repeats++;
						randomChar = balloonTest[i - currentLevel].Character - 'A';
					}
					// Generate new balloon
					balloonTest[i] = new Balloon (this, (char)('A' + randomChar), BalloonsExercise.getInstance ().PixelsPerFrameSubLevel[BalloonsExercise.getInstance ().CurrentSubLevel]);
					characters[i] = (char)('A' + randomChar);
					balloonTest[i].PosX = -200;
					balloonTest[i].Angle = r.Next (0, 360);
				}
				valids = 0;
				// Console.WriteLine("");
				for (int i = 0; i < exerciseInstance.ballonsPerSubLevel[exerciseInstance.CurrentSubLevel]; i++) {
					// Console.Write( balloonTest[i].Character+"," );
					if (i > BalloonsExercise.getInstance ().CurrentLevel - 1)
						if (balloonTest[i].Character == balloonTest[i - BalloonsExercise.getInstance ().CurrentLevel].Character)
							valids++;
				}
				// Console.WriteLine("Validas: "+(valids));
			}
			// Define 0 as one of the current balloons
			lastBalloonShowed = 0;
			currentDisplayedBalloons = new List<int> ();
			currentDisplayedBalloons.Add (lastBalloonShowed);
			
			// Save start time
			Start = DateTime.Now;
			
			// We'll wait before user start the serie		
			startedSerie = false;
			stopAnimation = true;
			buttonPulsar.Label = "¡Iniciar nueva serie!";
			GtkUtil.SetStyle (buttonPulsar, Configuration.Current.LargeFont);
			
			// Show dialog message
			auxLayout.SetMarkup ("<span font_desc=\"Ahafoni CLM Bold 20\" color=\"blue\">¡Haz clic cuando estés listo para comenzar otra serie!</span>");
			DrawDialog ();
			auxPixmap.DrawDrawable (auxGC, bufferPixmap, 0, 0, 0, 0, WIDTH_SCREEN, HEIGHT_SCREEN);
			
			buttonPulsar.Sensitive = true;
			buttonPulsar.Show ();
			eventBox.ButtonPressEvent -= callbackButtonPulsar;
			eventBox.ButtonPressEvent += callbackButtonPulsar;
		}

		public string GetSerie ()
		{
			
			string serie = "";
			for (int i = 0; i < characters.Length; i++) {
				serie += characters[i];
			}
			return serie;
			
		}

		protected void OnDeleteEvent (object sender, DeleteEventArgs a)
		{
			Application.Quit ();
			a.RetVal = true;
		}

		public bool UpdateGame ()
		{
			/*long currentTicks = System.Diagnostics.Stopwatch.GetTimestamp();
			long difference = currentTicks - SessionManager.GetInstance().CurrenTicks;
			long interval = SessionManager.GetInstance().Interval;
						
			if ( difference > interval ) {
			*/			
			drawingAreaGame.QueueDraw ();
			
			if (stopAnimation == false) {
				// If 50 balloons serie is over, we start again
				for (int i = 0; i < currentDisplayedBalloons.Count; i++) {
					// We update the position in every repaint.
					// If current displayed balloon is outside the screen updatePosition 
					// will return false.
					// If current ballon is outside the drawing area, delete it, add a new one
					// check omissions, and enable "Pulsar" button.
					if (!((Balloon)balloonTest[currentDisplayedBalloons[i]]).UpdatePosition ()) {
						currentDisplayedBalloons.Remove (currentDisplayedBalloons[i]);
						
						if (lastBalloonShowed + 1 < exerciseInstance.ballonsPerSubLevel[exerciseInstance.CurrentSubLevel]) {
							currentDisplayedBalloons.Add (lastBalloonShowed + 1);
							lastBalloonShowed++;
						} else {
							stopAnimation = true;
							finishedSerie = true;
							FinishSerie ();
							return false;
						}
						// If we have more than "level" balloons, we have to check omissions
						if (lastBalloonShowed - 1 >= currentLevel && !buttonPulsarClicked && balloonTest[lastBalloonShowed - 1].character == balloonTest[lastBalloonShowed - 1 - currentLevel].character) {
							// The user omits a correct balloon
							BalloonsExercise.getInstance ().CurrentOmissions++;
						}
						// We force the button to be active for next iteration
						// even if the user didn't click it.
						if (lastBalloonShowed >= currentLevel) {
							// buttonPulsar.Sensitive = true;
							eventBox.ButtonPressEvent -= callbackButtonPulsar;
							eventBox.ButtonPressEvent += callbackButtonPulsar;
							drawingAreaGame.Sensitive = true;
							buttonPulsarClicked = false;
						}
						labelBalloons.Markup = "<span size=\"x-large\" color=\"red\">" + Convert.ToString (lastBalloonShowed) + "</span>";
					}
				}
				
				if (!stopAnimation)
					DrawGame ();
			}
			
			return true;
		}

		private void FinishSerie ()
		{
			End = DateTime.Now;
			if (finishedSerie) {
				BalloonsExercise.getInstance ().SerieFinished ();
				finishedSerie = false;
			}
		}

		protected virtual void callbackButtonPulsar (object sender, System.EventArgs e)
		{
			
			if (currentMode == "game") {
				// Starting a serie...
				if (!startedSerie) {
					lastBalloonShowed = 0;
					startedSerie = true;
					stopAnimation = false;
					buttonPulsar.Label = "¡Letra repetida!";
					GtkUtil.SetStyle (buttonPulsar, Configuration.Current.LargeFont);
					// Button is hidden since user can click around all the screen
					buttonPulsar.HideAll ();
					buttonPulsar.Sensitive = false;
					eventBox.ButtonPressEvent -= callbackButtonPulsar;
					// Start timers
					if (auxTimer > 0) {
						GLib.Source.Remove (auxTimer);
					}
					currentHandler = new GLib.TimeoutHandler (UpdateGame);
					currentInterval = MSPERFRAME;
					
					auxTimer = GLib.Timeout.Add (currentInterval, currentHandler);
					return;
				}
				
				// Normal behaviour
				buttonPulsarClicked = true;
				drawingAreaGame.Sensitive = false;
				
				// ((Gtk.Image)sender).Sensitive = false;
				eventBox.ButtonPressEvent -= callbackButtonPulsar;
				// drawingAreaGame.State = StateType.Insensitive;
				
				if (lastBalloonShowed != 0) {
					
					if (characters[lastBalloonShowed] == characters[lastBalloonShowed - currentLevel]) {
						// The user hits the current balloon
						BalloonsExercise.getInstance ().CurrentCorrects++;
					} else {
						// The user fails the current balloon
						BalloonsExercise.getInstance ().CurrentFails++;
					}
					
				}
			} else if (currentMode == "demo") {
				
				if (balloonsCharDemo[currentCharDemo] == balloonsCharDemo[currentCharDemo - currentLevel]) {
					demoCharCorrects++;
					if (demoCharCorrects == 1) {
						Say ("¡Genial! Ya sólo tienes que acertar dos globos.");
					} else if (demoCharCorrects == 2) {
						Say ("¡Muy bien! Ahora sólo te falta un globo.");
					} else if (demoCharCorrects == 3) {
						NextStep ();
					}
				} else {
					demoCharCorrects = 0;
					if (currentLevel == 1) {
						Say ("Te has equivocado. Recuerda que tienes que pulsar cuando aparezcan dos globos consecutivos con la misma letra y que tienes que acertar 3 veces seguidas sin equivocarte.");
					} else if (currentLevel == 2) {
						Say ("Te has equivocado. Recuerda que tienes que pulsar cuando aparezca una letra que haya aparecido dos globos antes y que tienes que acertar 3 veces seguidas sin equivocarte.");
					} else if (currentLevel == 3) {
						Say ("Te has equivocado. Recuerda que tienes que pulsar cuando aparezca una letra que haya aparecido tres globos antes y que tienes que acertar 3 veces seguidas sin equivocarte.");
					}
				}
				
				buttonPulsarClicked = true;
				drawingAreaGame.Sensitive = false;
				
			}
		}

		public void ShowMessage (String mensaje)
		{
			
			auxLayout.SetMarkup (mensaje);
			
			bufferPixmap.DrawLayout (auxGC, 0, HEIGHT_SCREEN - 220, auxLayout);
			
			this.drawingAreaGame.QueueDraw ();
			
		}

		public void pausa ()
		{
			
			if (!pausedExercise) {
				pausedExercise = true;
				if (auxTimer > 0)
					GLib.Source.Remove (auxTimer);
			} else {
				pausedExercise = false;
				auxTimer = GLib.Timeout.Add (currentInterval, currentHandler);
			}
		}

		public void InitGame ()
		{
			
			SessionManager.GetInstance ().ChangeExerciseStatus ("game");
			currentMode = "game";
			hboxTop.ShowAll ();
			hboxDemo.HideAll ();
			// buttonPulsar.ShowAll();
			buttonPulsar.HideAll ();
			
			// Balloons
			labelBalloonsString.HideAll ();
			labelBalloons.HideAll ();
			
			startedSerie = false;
		}

		#region Demo functions

		public void InitDemo ()
		{
			InitDemo (0);
		}

		public void InitDemo (int step)
		{
			
			currentMode = "demo";
			currentCharDemo = 0;
			demoCharCorrects = 0;
			
			Gtk.Image auxImg = new Image (Configuration.ProgramDir () + System.IO.Path.DirectorySeparatorChar + "ejercicios" + System.IO.Path.DirectorySeparatorChar + "Balloons" + System.IO.Path.DirectorySeparatorChar + "demo" + System.IO.Path.DirectorySeparatorChar + "pepe1.png");
			pepe1Pixbuf = auxImg.Pixbuf;
			
			auxImg = new Image (Configuration.ProgramDir () + System.IO.Path.DirectorySeparatorChar + "ejercicios" + System.IO.Path.DirectorySeparatorChar + "Balloons" + System.IO.Path.DirectorySeparatorChar + "demo" + System.IO.Path.DirectorySeparatorChar + "pepe2.png");
			pepe2Pixbuf = auxImg.Pixbuf;
			
			auxImg = new Image (Configuration.ProgramDir () + System.IO.Path.DirectorySeparatorChar + "ejercicios" + System.IO.Path.DirectorySeparatorChar + "resources" + System.IO.Path.DirectorySeparatorChar + "img" + System.IO.Path.DirectorySeparatorChar + "dialog.png");
			dialogPixbuf = auxImg.Pixbuf;
			// ballonPixbuf = Gdk.Pixbuf.LoadFromResource( "pesco.ejercicios.Ballons.demo.balloon.png" );			
			
			buttonPulsar.Sensitive = false;
			eventBox.ButtonPressEvent -= callbackButtonPulsar;
			
			buttonPulsar.Label = "¡Letra repetida!";
			buttonPulsar.HideAll ();
			GtkUtil.SetStyle (buttonPulsar, Configuration.Current.LargeFont);
			hboxTop.HideAll ();
			hboxDemo.Show ();
			buttonGoForward.ShowAll ();
			buttonGoBack.ShowAll ();
			buttonStartExercise.Hide ();
			
			currentLevel = BalloonsExercise.getInstance ().CurrentLevel;
			// Init variables
			if (currentStep == 0) {
				currentStep = 0;
			} else {
				// Change initial text if user has increased level
				currentStep = 0;
				buttonGoForward.Sensitive = true;
				buttonGoBack.Sensitive = false;
			}
			firstFrameStep = true;
			
			// Init texts and example sequences			
			if (currentLevel == 1) {
				animationsText = animationsTextLevel1;
				balloonsCharDemo = balloonsCharDemo1;
			} else if (currentLevel == 2) {
				animationsText = animationsTextLevel2;
				balloonsCharDemo = balloonsCharDemo2;
			} else if (currentLevel == 3) {
				animationsText = animationsTextLevel3;
				balloonsCharDemo = balloonsCharDemo3;
			}
			if (step == 1) {
				animationsText[0] = "¡Has subido de nivel! Ahora estás en el nivel " + exerciseInstance.CurrentLevel + ". Por eso, ahora tendrás que fijarte en los globos que aparezcan " + exerciseInstance.CurrentLevel + " posiciones antes.";
			}
			
			// Remove timer if exists
			if (auxTimer > 0) {
				GLib.Source.Remove (auxTimer);
			}
			
			// Init demo timer
			currentHandler = new GLib.TimeoutHandler (UpdateDemo);
			// 1000ms / 60fps ~= 17ms per frame
			currentInterval = MSPERFRAME;
			auxTimer = GLib.Timeout.Add (currentInterval, currentHandler);
			// Init demo idle timer
			// currentHandler = new GLib.IdleHandler( UpdateDemo );
			// auxTimer = GLib.Idle.Add ( currentHandler );
		}

		private void DrawDialog ()
		{
			
			bufferPixmap.DrawPixbuf (auxGC, dialogPixbuf, 0, 0, 0, 0, WIDTH_SCREEN, HEIGHT_SCREEN, 0, 0,
			0);
			if (pepeStatus > 2) {
				bufferPixmap.DrawPixbuf (auxGC, pepe1Pixbuf, 0, 0, 0, HEIGHT_SCREEN - pepe1Pixbuf.Height, pepe1Pixbuf.Width, pepe1Pixbuf.Height, 0, 0,
				0);
			} else {
				bufferPixmap.DrawPixbuf (auxGC, pepe2Pixbuf, 0, 0, 0, HEIGHT_SCREEN - pepe2Pixbuf.Height, pepe2Pixbuf.Width, pepe2Pixbuf.Height, 0, 0,
				0);
			}
			// Draw text
			bufferPixmap.DrawLayout (auxGC, 200, 38, auxLayout);
		}

		private void NextStep ()
		{
			firstFrameStep = true;
			currentStep++;
		}

		private void BackStep ()
		{
			if (currentStep != 0) {
				firstFrameStep = true;
				currentCharacter = 0;
				currentStep--;
			}
		}

		private bool FirstFrameStep ()
		{
			if (firstFrameStep == true) {
				firstFrameStep = false;
				return true;
			}
			
			return false;
		}

		private bool LastFrameStep ()
		{
			if (currentCharacter == totalCharacters - 1) {
				return true;
			}
			return false;
		}

		private bool AfterLastFrameStep ()
		{
			
			if (currentCharacter >= animationsText[currentStep].Length) {
				return true;
			}
			
			return false;
		}

		private bool UpdateDemo ()
		{
			/*
			long currentTicks = System.Diagnostics.Stopwatch.GetTimestamp();
			long difference = currentTicks - SessionManager.GetInstance().CurrenTicks;
			long interval = SessionManager.GetInstance().Interval;
						
			if ( difference > interval ) {
				drawingAreaGame.QueueDraw();
				SessionManager.GetInstance().UpdateCurrenTicks();
			}*/			
			
			if (currentMode == "game")
				return false;
			
			drawingAreaGame.QueueDraw ();
			return true;
		}

		private void DrawDemo ()
		{
			auxPixmap.DrawDrawable (auxGC, bufferPixmap, 0, 0, 0, 0, WIDTH_SCREEN, HEIGHT_SCREEN);
			// long auxLong = System.Diagnostics.Stopwatch.GetTimestamp ();
			// Hello
			if (currentStep == 0) {
				if (FirstFrameStep ()) {
					currentCharacter = 0;
					totalCharacters = animationsText[currentStep].Length;
					stringToSay = animationsText[currentStep];
					buttonGoForward.Sensitive = true;
					buttonGoBack.Sensitive = false;
					buttonPulsar.Sensitive = false;
					eventBox.ButtonPressEvent -= callbackButtonPulsar;
					buttonGoLast.HideAll ();
					buttonStartExercise.HideAll ();
					pepePixmap.DrawPixbuf (auxGC, pepe1Pixbuf, 0, 0, 0, HEIGHT_SCREEN - pepe1Pixbuf.Height, pepe1Pixbuf.Width, pepe1Pixbuf.Height, 0, 0,
					0);
				}
				DrawBackground ();
				bufferPixmap.DrawPixbuf (auxGC, pepe1Pixbuf, 0, 0, 0, HEIGHT_SCREEN - pepe1Pixbuf.Height, pepe1Pixbuf.Width, pepe1Pixbuf.Height, 0, 0,
				0);
				auxLayout.SetMarkup ("<span font_desc=\"Ahafoni CLM Bold 20\" color=\"blue\">" + stringToSay.Substring (0, currentCharacter) + "</span>");
				PepeUtils.IncrementCharacterDialog (ref currentCharacter, ref pepeStatus, stringToSay);
				bufferPixmap.DrawLayout (auxGC, 200, 40, auxLayout);
				DrawDialog ();
				DrawBalloonDemo ();
				
			// Explanation 1
			} else if (currentStep == 1) {
				if (FirstFrameStep ()) {
					currentCharacter = 0;
					totalCharacters = animationsText[currentStep].Length;
					stringToSay = animationsText[currentStep];
				}
				DrawBackground ();
				auxLayout.SetMarkup ("<span font_desc=\"Ahafoni CLM Bold 20\" color=\"blue\">" + stringToSay.Substring (0, currentCharacter) + "</span>");
				PepeUtils.IncrementCharacterDialog (ref currentCharacter, ref pepeStatus, stringToSay);
				DrawDialog ();
				DrawBalloonDemo ();
			// Pre-demo
			} else if (currentStep == 2) {
				if (FirstFrameStep ()) {
					currentCharacter = 0;
					totalCharacters = animationsText[currentStep].Length;
					stringToSay = animationsText[currentStep];
				}
				DrawBackground ();
				auxLayout.SetMarkup ("<span font_desc=\"Ahafoni CLM Bold 20\" color=\"blue\">" + stringToSay.Substring (0, currentCharacter) + "</span>");
				PepeUtils.IncrementCharacterDialog (ref currentCharacter, ref pepeStatus, stringToSay);
				DrawDialog ();
			// Demo game
			} else if (currentStep == 3) {
				if (FirstFrameStep ()) {
					totalCharacters = animationsText[currentStep - 1].Length;
					currentCharacter = totalCharacters;
					stringToSay = animationsText[currentStep - 1];
					hboxDemo.HideAll ();
					// buttonPulsar.ShowAll();
					buttonGoForward.Sensitive = false;
					// Create an initial balloon for demo
					balloonDemo1 = new Balloon (this, balloonsCharDemo[currentCharDemo], BalloonsExercise.getInstance ().PixelsPerFrameSubLevel[0]);
					balloonDemo1.PosX = -200;
					balloonDemo1.PosY = -100;
					balloonDemo1.Angle = r.Next (0, 360);
					auxGC.RgbFgColor = new Gdk.Color (186, 235, 255);
					backgroundPixmap.DrawRectangle (auxGC, true, new Gdk.Rectangle (0, 0, 1000, 600));
					backgroundPixmap.DrawPixbuf (auxGC, pepe1Pixbuf, 0, 0, 0, HEIGHT_SCREEN - pepe1Pixbuf.Height, pepe1Pixbuf.Width, pepe1Pixbuf.Height, 0, 0,
					0);
					// backgroundPixmap.DrawPixbuf( auxGC, ballonPixbuf, 0,0, 8, HEIGHT_SCREEN - ballonPixbuf.Height + 3, ballonPixbuf.Width, ballonPixbuf.Height, 0, 0, 0 );
					backgroundPixmap.DrawPixbuf (auxGC, dialogPixbuf, 0, 0, 0, 0, WIDTH_SCREEN, HEIGHT_SCREEN, 0, 0,
					0);
				}
				bufferPixmap.DrawDrawable (auxGC, backgroundPixmap, 0, 0, 0, 0, WIDTH_SCREEN, HEIGHT_SCREEN);
				auxLayout.SetMarkup ("<span font_desc=\"Ahafoni CLM Bold 20\" color=\"blue\">" + stringToSay.Substring (0, currentCharacter) + "</span>");
				PepeUtils.IncrementCharacterDialog (ref currentCharacter, ref pepeStatus, stringToSay);
				// DrawDialog();				
				if (!balloonDemo1.UpdatePosition ()) {
					// Enable ready button			
					if (currentCharDemo + 1 == currentLevel) {
						eventBox.ButtonPressEvent -= callbackButtonPulsar;
						eventBox.ButtonPressEvent += callbackButtonPulsar;
						// drawingAreaGame.Sensitive = true;
						// buttonPulsar.Sensitive = true;						
					}
					if (currentCharDemo + 1 == balloonsCharDemo.Length) {
						currentCharDemo = 0;
						// buttonPulsar.Sensitive = false;
						eventBox.ButtonPressEvent -= callbackButtonPulsar;
					} else {
						currentCharDemo++;
					}
					// Create balloons
					balloonDemo1 = new Balloon (this, balloonsCharDemo[currentCharDemo], BalloonsExercise.getInstance ().PixelsPerFrameSubLevel[0]);
					balloonDemo1.PosX = -200;
					balloonDemo1.PosY = -100;
					balloonDemo1.Angle = r.Next (0, 360);
					buttonPulsarClicked = false;
				}
				// Draw text
				bufferPixmap.DrawLayout (auxGC, 200, 38, auxLayout);
				// Draw balloon
				bufferPixmap.DrawPixbuf (auxGC, balloonDemo1.pixbufBalloon, 0, 0, balloonDemo1.PosX, balloonDemo1.PosY, WIDTH_BALLOON, HEIGHT_BALLOON, 0, 0,
				0);
			// Demo completed
			} else if (currentStep == 4) {
				if (FirstFrameStep ()) {
					totalCharacters = animationsText[currentStep].Length;
					currentCharacter = 0;
					stringToSay = animationsText[currentStep];
					buttonPulsar.Sensitive = false;
					eventBox.ButtonPressEvent -= callbackButtonPulsar;
					hboxDemo.ShowAll ();
					buttonPulsar.HideAll ();
					buttonGoBack.HideAll ();
					buttonGoForward.HideAll ();
					buttonGoLast.HideAll ();
					buttonStartExercise.ShowAll ();
					PepeUtils.GenerateCartel (this.CreatePangoContext (), auxGC, auxPixmap, "¡Ensayo superado!¡Comienza el ejercicio!");
				}
				auxGC.RgbFgColor = new Gdk.Color (0, 0, 0);
				PepeUtils.DrawCartel ();
				DrawBackground ();
				auxLayout.SetMarkup ("<span font_desc=\"Ahafoni CLM Bold 20\" color=\"blue\">" + stringToSay.Substring (0, currentCharacter) + "</span>");
				PepeUtils.IncrementCharacterDialog (ref currentCharacter, ref pepeStatus, stringToSay);
				// DrawDialog();				
				
			}
		}
			/* auxFrames++;
			long auxDif = (System.Diagnostics.Stopwatch.GetTimestamp()-auxLong);
			auxTotalCycles += auxDif;
			if ( auxFrames == SessionManager.GetInstance().Fps ) {
				Console.WriteLine( "Ciclos por segundo:"+auxTotalCycles+" | Velocidad PC: "+System.Diagnostics.Stopwatch.Frequency ) ;				
				auxFrames = 0;
				auxTotalCycles = 0;
			}*/			
		
				private void DrawBalloonDemo ()
		{
			
			// bufferPixmap.DrawPixbuf( auxGC, ballonPixbuf, 0,0, 8, HEIGHT_SCREEN - ballonPixbuf.Height + 3, ballonPixbuf.Width, ballonPixbuf.Height, 0, 0, 0 );	
		}

		private void Say (string toSay)
		{
			stringToSay = toSay;
			currentCharacter = 0;
			totalCharacters = toSay.Length;
		}
		#endregion

		protected virtual void OnButtonGoForwardClicked (object sender, System.EventArgs e)
		{
			NextStep ();
		}

		protected virtual void OnButtonGoBackClicked (object sender, System.EventArgs e)
		{
			BackStep ();
		}

		protected virtual void OnButtonStartExerciseClicked (object sender, System.EventArgs e)
		{
			NextExecution ();
			InitGame ();
			callbackButtonPulsar (buttonPulsar, new EventArgs ());
		}

		protected virtual void OnButtonGoLastClicked (object sender, System.EventArgs e)
		{
			currentStep = 4;
			currentCharacter = 0;
			firstFrameStep = true;
		}
		
	}
}

