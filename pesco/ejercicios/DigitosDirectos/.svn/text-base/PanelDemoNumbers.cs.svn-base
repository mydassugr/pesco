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
using Gtk;

namespace pesco
{

	[System.ComponentModel.ToolboxItem(true)]

	public partial class PanelDemoNumbers : Gtk.Bin
	{
		const int WIDTH_SCREEN = 1000;
		const int HEIGHT_SCREEN = 600;

		// Draw elements
		Gtk.DrawingArea drawingArea = new Gtk.DrawingArea ();
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

		// Aux elements
		Gtk.Button auxButton1;

		// Tests elements
		PanelDemoNumbersKeyboard auxKeyboard;
		string [] goalNumbers = { "72", "36", "837" };
		int currentGoalNumber = 0;
		int goalPosition = 0;

		// Animations
		string stringToSay;
		int currentStep = 0;
		int currentCharacter = 0;
		int totalCharacters = 0;		
		string [] animationsText = { "¡Hola de nuevo! Ahora vamos a hacer el ejercicio de Dictado de Números para medir cómo está tu memoria.",
				/* 1 */ "Para ello, voy a ir escribiendo números en una pizarra como la de abajo. Los escribiré de uno en uno y, cuando termine, tú tendrás que volver a escribirlos en el mismo orden.",
				/* 2 */ "Ahora vamos a hacer tres ensayos. Escribiré las secuencias de números y luego te mostraré un panel para que los escribas en el mismo orden en el que te los mostré. Cuando estés listo, pulsa el botón <span color=\"black\">Siguiente</span>.",
				/* 3 */ "",
				/* 4 */ "",
				/* 5 */ "¡Pulsa ahora el primer número que escribí!",
				/* 6 */ "¡Perfecto! Ahora vamos a realizar otro ensayo más. Pulsa <span color=\"black\">Siguiente</span> cuando estés listo.",
				/* 7 */ "¡Perfecto! Ya estás preparado para comenzar. ¡Ojo! Al principio escribiré número pequeños, pero si aciertas iré escribiendo cada vez números más grandes. Pulsa ahora el botón <span color=\"black\">Comenzar ejercicio</span>."
		};
		
		bool firstFrameStep = true;
		
		// Timers
		private int auxAnimationTimer;
		private int auxTiempoAnimacion2;
		private uint auxTimer;
		private GLib.TimeoutHandler currentHandler;
		private uint currentInterval;
		private bool pausedExercise;
		private const int REPAINT_SPEED = 50;
		int pauseTimer = 0;

		// Layout 
		Pango.Layout layout;

		protected virtual void StartExerciseClicked (object sender, System.EventArgs e)
		{
			if ( auxTimer != null && auxTimer > 0 ) {
				GLib.Source.Remove( auxTimer );	
			}
			ExerciseDirectNumbers ejDigitosDirectos = ExerciseDirectNumbers.getInstance ();
			ejDigitosDirectos.iniciar ();
		}

		public PanelDemoNumbers ()
		{
			this.Build ();
			GtkUtil.SetStyle( buttonStartExercise, Configuration.Current.MediumFont );
			GtkUtil.SetStyle( buttonGoBack, Configuration.Current.MediumFont );
			GtkUtil.SetStyle( buttonGoForward, Configuration.Current.MediumFont );
			GtkUtil.SetStyle( buttonGoLast, Configuration.Current.MediumFont );
			auxKeyboard = new PanelDemoNumbersKeyboard (this);
		}

		private void InitLayout ()
		{			
			layout = new Pango.Layout (this.CreatePangoContext ());
			layout.Width = Pango.Units.FromPixels (720);
			layout.Justify = false;
			layout.Wrap = Pango.WrapMode.Word;
			layout.Alignment = Pango.Alignment.Left;
			layout.FontDescription = Pango.FontDescription.FromString ("Ahafoni CLM Bold 36");		
		}

		public void InitPanel ()
		{
			
			InitLayout ();
			
			// Disable button Go Last
			buttonGoLast.ShowAll();
			buttonStartExercise.HideAll();
			
			// Expose event
			imageBackground.ExposeEvent += Expose_Event;
			
			// Load pixbufs
			backgroundPixbuf = Gdk.Pixbuf.LoadFromResource ("pesco.ejercicios.DigitosDirectos.img.background.png");
			dialogPixbuf = Gdk.Pixbuf.LoadFromResource ("pesco.ejercicios.DigitosDirectos.img.dialog.png");
			pepe1Pixbuf = Gdk.Pixbuf.LoadFromResource ("pesco.ejercicios.DigitosDirectos.img.pepe1.png");
			pepe2Pixbuf = Gdk.Pixbuf.LoadFromResource ("pesco.ejercicios.DigitosDirectos.img.pepe2.png");
			
			// Init pixmap and GC
			imageBackground.Pixmap = new Gdk.Pixmap (fixed1.GdkWindow, WIDTH_SCREEN, HEIGHT_SCREEN, 24);
			auxGC = new Gdk.GC (imageBackground.Pixmap);
			
			currentHandler = new GLib.TimeoutHandler (Update);
			currentInterval = 25;
			auxTimer = GLib.Timeout.Add (currentInterval, currentHandler);
			
		}

		void Expose_Event (object obj, ExposeEventArgs args)
		{
			// Draw background
			DrawBackground ();
			
			// Hello
			if (currentStep == 0) {
				if ( FirstFrameStep() ) {
					buttonStartExercise.HideAll();
					buttonGoLast.HideAll();
					buttonGoBack.Sensitive = false;
					currentCharacter = 0;
					totalCharacters = animationsText[currentStep].Length;
					stringToSay = animationsText[currentStep];
				}
				PepeUtils.IncrementCharacterDialog( ref currentCharacter, ref pepeStatus, stringToSay );
				layout.SetMarkup ("<span font_desc=\"Ahafoni CLM Bold 20\" color=\"blue\">" + stringToSay.Substring (0, currentCharacter) + "</span>");
				DrawDialog ();
			// Explanation 1
			} else if (currentStep == 1) {
				if ( FirstFrameStep() ) {
					auxPixbuf1 = Gdk.Pixbuf.LoadFromResource( "pesco.ejercicios.DigitosDirectos.img.blackboard.png" );
					buttonGoBack.Sensitive = true;
					currentCharacter = 0;
					totalCharacters = animationsText[currentStep].Length;
					stringToSay = animationsText[currentStep];
				}
				layout.SetMarkup ("<span font_desc=\"Ahafoni CLM Bold 20\" color=\"blue\">" + stringToSay.Substring (0, currentCharacter) + "</span>");
				PepeUtils.IncrementCharacterDialog( ref currentCharacter, ref pepeStatus, stringToSay );
				DrawDialog ();
				imageBackground.Pixmap.DrawPixbuf (auxGC, auxPixbuf1, 0, 0, 335, 245, auxPixbuf1.Width, auxPixbuf1.Height, 0, 0,
				0);
			// Explanation 3: Ready
			} else if (currentStep == 2) {
				if ( FirstFrameStep() ) {
					buttonGoForward.Sensitive = true;
					buttonGoBack.Sensitive = true;	
					totalCharacters = animationsText[currentStep].Length;
					stringToSay = animationsText[currentStep];
					currentCharacter = 0;		
				}
				PepeUtils.IncrementCharacterDialog( ref currentCharacter, ref pepeStatus, stringToSay );
				layout.SetMarkup ("<span font_desc=\"Ahafoni CLM Bold 20\" color=\"blue\">" + stringToSay.Substring (0, currentCharacter) + "</span>");
				DrawDialog ();
			// Explanation: Draw numbers. This step will be repeated for each sequence
			} else if (currentStep == 3) {
				if ( FirstFrameStep() ) {
					buttonGoForward.Sensitive = false;
					buttonGoBack.Sensitive = false;
					goalPosition = 0;
					pauseTimer = 40;
					fixed1.Remove( auxKeyboard );
					fixed1.Remove( auxButton1 );					
				}
				pauseTimer--;
				if (pauseTimer == 0) {
					if ( goalPosition < goalNumbers[currentGoalNumber].Length - 1) {
						pauseTimer = 40;
						goalPosition++;
						currentCharacter = 0;
					} else {
						currentStep++;	
					}
				}
				imageBackground.Pixmap.DrawPixbuf (auxGC, auxPixbuf1, 0, 0, (WIDTH_SCREEN - auxPixbuf1.Width) / 2, (HEIGHT_SCREEN - auxPixbuf1.Height) / 2, auxPixbuf1.Width, auxPixbuf1.Height, 0, 0,
				0);
				// Draw number
				layout.SetMarkup ("<span font_desc=\"Ahafoni CLM Bold 120\" color=\"white\">"+goalNumbers[currentGoalNumber][goalPosition]+"</span>");
				int width;
				int height;
				layout.GetPixelSize (out width, out height);
				imageBackground.Pixmap.DrawLayout (auxGC, (WIDTH_SCREEN - width) / 2, (HEIGHT_SCREEN - height) / 2, layout);
			// Explanation: Show numbers panel
			} else if (currentStep == 4) {
				if (pauseTimer == 0) {
					goalPosition = 0;
					pauseTimer = 40;
					currentStep++;
					firstFrameStep = true;
					// Keyboard panel
					fixed1.Put (auxKeyboard, 400, 245);
					auxKeyboard.Sensitive = true;
					// See again button
					auxButton1 = new Gtk.Button ("Ver de\nnuevo");
					auxButton1.WidthRequest = 170;
					auxButton1.HeightRequest = 200;
					GtkUtil.SetStyle (auxButton1, Configuration.Current.ExtraLargeFont);
					auxButton1.Clicked += delegate {
						fixed1.Remove( auxKeyboard );
						fixed1.Remove( auxButton1 );
						currentCharacter = 0;
						pauseTimer = 60;
						currentStep = 3;
						firstFrameStep = true;
					};
					// Put button
					fixed1.Put (auxButton1, 825, 245);
					fixed1.ShowAll ();
				}
			// Panel
			} else if (currentStep == 5) {
				if ( FirstFrameStep() ) {
					buttonGoForward.Sensitive = false;
					buttonGoBack.Sensitive = false;
					currentCharacter = 0;
					stringToSay = animationsText[currentStep];
					totalCharacters = animationsText[currentStep].Length;
				}
				layout.SetMarkup ("<span font_desc=\"Ahafoni CLM Bold 20\" color=\"blue\">" + stringToSay.Substring(0, currentCharacter) + "</span>");
				PepeUtils.IncrementCharacterDialog( ref currentCharacter, ref pepeStatus, stringToSay );
				DrawDialog ();
			} 
			// Trial passed
			else if (currentStep == 6) {
				// This is a fix. If we have passed all the trials, go to last step ( 7 ) and ignore this step
				if ( currentGoalNumber == goalNumbers.Length ) {
					currentStep = 7;
				} else {		
					if ( FirstFrameStep() ) {
						buttonGoBack.HideAll();
						buttonGoForward.ShowAll();
						buttonGoForward.Sensitive = true;
						auxKeyboard.Sensitive = false;
						auxButton1.Sensitive = false;					
						currentCharacter = 0;
						totalCharacters = animationsText[currentStep].Length;
						stringToSay = animationsText[currentStep];
					}
					layout.SetMarkup ("<span font_desc=\"Ahafoni CLM Bold 20\" color=\"blue\">" +  stringToSay.Substring (0, currentCharacter) + "</span>");
					PepeUtils.IncrementCharacterDialog( ref currentCharacter, ref pepeStatus, stringToSay );
					DrawDialog();
				}
			}			
			else if (currentStep == 7) {
				// This is a fix. If we haven't passed all the trials, go to next trial and show it ( step 3 )
				if ( currentGoalNumber < goalNumbers.Length ) {
					currentStep = 3;
				} else {				
					if ( FirstFrameStep() ) {
						buttonStartExercise.ShowAll();
						buttonGoBack.HideAll();
						buttonGoForward.HideAll();
						buttonStartExercise.Sensitive = true;
						auxKeyboard.Sensitive = false;
						auxButton1.Sensitive = false;
						currentCharacter = 0;
						totalCharacters = animationsText[currentStep].Length;
						stringToSay = animationsText[currentStep];
					}
					layout.SetMarkup ("<span font_desc=\"Ahafoni CLM Bold 20\" color=\"blue\">" +  stringToSay.Substring (0, currentCharacter) + "</span>");
					PepeUtils.IncrementCharacterDialog( ref currentCharacter, ref pepeStatus, stringToSay );
					DrawDialog();
				}
			}
			
		}

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
		
		private void DrawBackground ()
		{
			
			imageBackground.Pixmap.DrawPixbuf (auxGC, backgroundPixbuf, 0, 0, 0, 0, WIDTH_SCREEN, HEIGHT_SCREEN, 0, 0,
			0);
			
		}

		private void DrawDialog ()
		{
			
			imageBackground.Pixmap.DrawPixbuf (auxGC, dialogPixbuf, 0, 0, 0, 0, WIDTH_SCREEN, HEIGHT_SCREEN, 0, 0,
			0);
			if (pepeStatus > 2) {
				imageBackground.Pixmap.DrawPixbuf (auxGC, pepe1Pixbuf, 0, 0, 0, HEIGHT_SCREEN - pepe1Pixbuf.Height, pepe1Pixbuf.Width, pepe1Pixbuf.Height, 0, 0,
				0);
			} else {
				imageBackground.Pixmap.DrawPixbuf (auxGC, pepe2Pixbuf, 0, 0, 0, HEIGHT_SCREEN - pepe2Pixbuf.Height, pepe2Pixbuf.Width, pepe2Pixbuf.Height, 0, 0,
				0);
			}
			
			// Draw text
			imageBackground.Pixmap.DrawLayout (auxGC, 200, 38, layout);
		}

		public void Say (string text)
		{
			
			stringToSay = text;
			totalCharacters = text.Length;
			currentCharacter = 0;
		}

		public void buttonClicked (string number)
		{
			// Trial passed?
			if (goalNumbers[currentGoalNumber][goalPosition] == number[0] && goalPosition == goalNumbers[currentGoalNumber].Length - 1) {
				currentGoalNumber++;
				NextStep();
			}
			// Not finished, but current number ok?
			else if ( goalNumbers[currentGoalNumber][goalPosition] == number[0] && goalPosition == 0 ) {
				Say ("¡Bien! ¡Ahora el segundo número!");
				goalPosition++;
			}
			else if ( goalNumbers[currentGoalNumber][goalPosition] == number[0] && goalPosition == 1 ) {
				Say ("¡Genial! ¡Ahora el tercer número!");
				goalPosition++;
			}	
			// Fail in first number?
			else if (goalNumbers[currentGoalNumber][goalPosition] != number[0] && goalPosition == 0) {
				Say ("Ése no es el primer número que se ha mostrado. Inténtalo de nuevo. Si no lo recuerdas, puedes pulsar el botón <span color=\"black\">Ver de nuevo</span>.");
			} 
			// Fail in second number?
			else if (goalNumbers[currentGoalNumber][goalPosition] != number[0] && goalPosition == 1) {
				Say ("Vaya, ése no es el segundo número que escribí. Si no lo recuerdas pulsa en el botón \"Ver de nuevo\".");
			}
			// Fail in third number?
			else if (goalNumbers[currentGoalNumber][goalPosition] != number[0] && goalPosition == 2) {
				Say ("Vaya, ése no es el tercer número que escribí. Si no lo recuerdas pulsa en el botón \"Ver de nuevo\".");
			}
			
		}

		bool Update ()
		{
			
			imageBackground.QueueDraw ();
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
		
		private void PreviousStep() {
			currentStep--;
			firstFrameStep = true;
		}
		
		private void NextStep() {
			currentStep++;
			firstFrameStep = true;
		}
		
		protected virtual void OnButtonGoLastClicked (object sender, System.EventArgs e)
		{
			currentStep = animationsText.Length - 1;
			firstFrameStep = true;
		}
		
		protected virtual void OnButtonGoForwardClicked (object sender, System.EventArgs e)
		{
			NextStep();
		}
		
		protected virtual void OnButtonGoBackClicked (object sender, System.EventArgs e)
		{
			PreviousStep();
		}
		
		
		
		
	}
}

