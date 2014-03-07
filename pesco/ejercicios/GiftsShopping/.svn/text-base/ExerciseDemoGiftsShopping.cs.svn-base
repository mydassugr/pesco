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
	public partial class ExerciseDemoGiftsShopping : Gtk.Bin
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
		int validChecked = 0;

		// Animations
		string stringToSay;
		int currentStep = 0;
		int currentCharacter = 0;
		int totalCharacters = 0;
		string [] animationsText = { "¡Bienvenido al ejercicio de compra de regalos! En este ejercicio vas a entrenar tu capacidad de planificación y organización. Para ello, quiero que me ayudes a comprar regalos para una serie de personas.",
				/*1*/ "Te mostraré la lista de personas y sus gustos y tendrás que elegir el regalo más adecuado para cada uno. También mostraré el presupuesto del que dispones. Observa la lista de abajo para ver un ejemplo.",
				/*2*/ "Esta lista la podrás consultar siempre que quieras pulsando el botón <span color=\"black\">Ver lista de personas y sus gustos</span> de la parte superior izquierda de la pantalla. No hace falta que memorices la lista.",
				/*3*/ "Ten en cuenta el presupuesto que tienes. No puedes pasarte, pero no conviene que te sobre mucho dinero. El dinero gastado y el total del que dispones podrás verlo arriba a la derecha. Tómate el tiempo que quieras.",
				/*4*/ "Cuando comience el ejercicio verás una lista de tiendas. Tienes que pulsar sobre la tienda para ver los objetos que tiene y sus precios.",
				/*5*/ "Dentro de las tiendas, podrás comprar el objeto que quieras pulsando sobre él. Si te equivocas comprando un objeto, no pasa nada, puedes devolverlo pulsando el objeto de nuevo.",
				/*6*/ "Puedes ver toda la compra que ya has hecho pulsando en <span color=\"black\">Ver mi compra</span>. Desde esta pantalla puedes devolver un objeto comprado. Sólo tienes que pulsar sobre el objeto y decir que <span color=\"black\">Sí</span> quieres devolverlo.",
				/*7*/ "Cuando hayas terminado de realizar la compra de todos los regalos, tienes que pulsar sobre el botón <span color=\"black\">Finalizar compra</span>.",
				/*8*/ "Recuerda que tienes que elegir los regalos más adecuados para cada persona, teniendo en cuenta el presupuesto que tienes.",
				/*9*/ "Al comenzar el ejercicio te mostraré la lista de personas a las que tienes que comprar regalos. Léela tranquilamente y recuerda que siempre la puedes consultar pulsando el botón <span color=\"black\">Ver lista de personas y sus gustos</span>.",
				/*10*/ "Ten en cuenta el presupuesto del que dispones. Cuando estés preparado pulsa el botón <span color=\"black\">Comenzar ejercicio</span>."};

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

		// Layout 
		Pango.Layout auxLayout;

		public ExerciseDemoGiftsShopping ()
		{
			this.Build ();
			GtkUtil.SetStyle( buttonGoBack, Configuration.Current.MediumFont );
			GtkUtil.SetStyle( buttonGoForward, Configuration.Current.MediumFont );
			GtkUtil.SetStyle( buttonGoLast, Configuration.Current.MediumFont );
			GtkUtil.SetStyle( buttonStartExercise, Configuration.Current.MediumFont );
			
		}

		private void InitLayout ()
		{
			
			auxLayout = new Pango.Layout (this.CreatePangoContext ());
			auxLayout.Width = Pango.Units.FromPixels (720);
			auxLayout.Justify = false;
			auxLayout.Wrap = Pango.WrapMode.Word;
			auxLayout.Alignment = Pango.Alignment.Left;
			auxLayout.FontDescription = Pango.FontDescription.FromString ("Ahafoni CLM Bold 20");
			
		}

		public void InitPanel ()
		{
			
			//InitTexts ();
			InitLayout ();

			imageBackground.ExposeEvent += Expose_Event;

			backgroundPixbuf = Gdk.Pixbuf.LoadFromResource ("pesco.ejercicios.GiftsShopping.img.background.png");
			dialogPixbuf = Gdk.Pixbuf.LoadFromResource ("pesco.ejercicios.resources.img.dialog.png");
			pepe1Pixbuf = Gdk.Pixbuf.LoadFromResource ("pesco.ejercicios.GiftsShopping.img.pepe1.png");
			pepe2Pixbuf = Gdk.Pixbuf.LoadFromResource ("pesco.ejercicios.GiftsShopping.img.pepe2.png");
			
			imageBackground.Pixmap = new Gdk.Pixmap ( hboxCentral.GdkWindow, WIDTH_SCREEN, HEIGHT_SCREEN, 24);
			auxGC = new Gdk.GC(imageBackground.Pixmap);
			
			if ( auxTimer != null && auxTimer > 0 ) 
				GLib.Source.Remove(auxTimer);
			
			currentHandler = new GLib.TimeoutHandler (Update);
			currentInterval = 25;
			auxTimer = GLib.Timeout.Add (currentInterval, currentHandler);
			
		}

		void Expose_Event (object obj, ExposeEventArgs args)
		{
			// Draw background
			DrawDialogBackground();
			
			// Hello
			if (currentStep == 0) {				
				if ( FirstFrameStep() ) {
					buttonGoBack.Sensitive = false;
					stringToSay = animationsText[currentStep];
					totalCharacters = animationsText[currentStep].Length;
					buttonStartExercise.HideAll();
					buttonGoLast.HideAll();
				}
				DrawDialog();
				PepeUtils.IncrementCharacterDialog( ref currentCharacter, ref pepeStatus, stringToSay );
				auxLayout.SetMarkup( "<span color=\"blue\">"+stringToSay.Substring(0, currentCharacter)+"</span>" );
			} 
			// Explanation 1
			else if ( currentStep == 1 ) {
				if ( FirstFrameStep() ) {
					buttonGoBack.Sensitive = true;
					stringToSay = animationsText[currentStep];
					totalCharacters = animationsText[currentStep].Length;
					auxPixbuf1 = Gdk.Pixbuf.LoadFromResource("pesco.ejercicios.GiftsShopping.screenshots.1.jpg");
				}
				DrawDialog();
				PepeUtils.IncrementCharacterDialog( ref currentCharacter, ref pepeStatus, stringToSay );
				auxLayout.SetMarkup( "<span color=\"blue\">"+stringToSay.Substring(0, currentCharacter)+"</span>" );				
				if ( AfterLastFrameStep() )
					imageBackground.Pixmap.DrawPixbuf( auxGC, auxPixbuf1, 0, 0, 300, 250, auxPixbuf1.Width, auxPixbuf1.Height,0 ,0, 0 );				
			}
			// Explanation 2: Pyramids OK
			else if ( currentStep == 2 ) {
				if ( FirstFrameStep() ) {
					totalCharacters = animationsText[currentStep].Length;
					stringToSay = animationsText[currentStep];
					auxPixbuf1 = Gdk.Pixbuf.LoadFromResource("pesco.ejercicios.GiftsShopping.screenshots.2.jpg");
				}
				DrawDialog();
				PepeUtils.IncrementCharacterDialog( ref currentCharacter, ref pepeStatus, stringToSay );				
				auxLayout.SetMarkup( "<span color=\"blue\">"+stringToSay.Substring(0, currentCharacter)+"</span>" );
				if ( AfterLastFrameStep() )
					imageBackground.Pixmap.DrawPixbuf( auxGC, auxPixbuf1, 0, 0, 300, 250, auxPixbuf1.Width, auxPixbuf1.Height,0 ,0, 0 );
			}
			// Explanation: Pyramids BAD
			else if ( currentStep == 3 ) {
				if ( FirstFrameStep() ) {
					totalCharacters = animationsText[currentStep].Length;
					stringToSay = animationsText[currentStep];
					auxPixbuf1 = Gdk.Pixbuf.LoadFromResource("pesco.ejercicios.GiftsShopping.screenshots.3.jpg");
				}
				DrawDialog();
				auxLayout.SetMarkup( "<span color=\"blue\">"+stringToSay.Substring(0, currentCharacter)+"</span>" );
				PepeUtils.IncrementCharacterDialog( ref currentCharacter, ref pepeStatus, stringToSay );
				if ( AfterLastFrameStep() )
					imageBackground.Pixmap.DrawPixbuf( auxGC, auxPixbuf1, 0, 0, 300, 250, auxPixbuf1.Width, auxPixbuf1.Height,0 ,0, 0 );
			}
			// Explanation: PreTest
			else if ( currentStep == 4 ) {
				if ( FirstFrameStep() ) {
					totalCharacters = animationsText[currentStep].Length;
					stringToSay = animationsText[currentStep];
					auxPixbuf1 = Gdk.Pixbuf.LoadFromResource("pesco.ejercicios.GiftsShopping.screenshots.4.jpg");
				}
				DrawDialog();
				auxLayout.SetMarkup( "<span color=\"blue\">"+stringToSay.Substring(0, currentCharacter)+"</span>" );
				PepeUtils.IncrementCharacterDialog( ref currentCharacter, ref pepeStatus, stringToSay );
				if ( AfterLastFrameStep() )
					imageBackground.Pixmap.DrawPixbuf( auxGC, auxPixbuf1, 0, 0, 300, 250, auxPixbuf1.Width, auxPixbuf1.Height,0 ,0, 0 );
			}
			// Explanation
			else if ( currentStep == 5 ) {
				if ( FirstFrameStep() ) {
					totalCharacters = animationsText[currentStep].Length;
					stringToSay = animationsText[currentStep];
					auxPixbuf1 = Gdk.Pixbuf.LoadFromResource("pesco.ejercicios.GiftsShopping.screenshots.5.jpg");
				}
				DrawDialog();
				PepeUtils.IncrementCharacterDialog( ref currentCharacter, ref pepeStatus, stringToSay );
				auxLayout.SetMarkup( "<span color=\"blue\">"+stringToSay.Substring(0, currentCharacter)+"</span>" );
				if ( AfterLastFrameStep() )
					imageBackground.Pixmap.DrawPixbuf( auxGC, auxPixbuf1, 0, 0, 300, 250, auxPixbuf1.Width, auxPixbuf1.Height,0 ,0, 0 );
			} else if ( currentStep == 6 ) {
				if ( FirstFrameStep() ) {
					totalCharacters = animationsText[currentStep].Length;
					stringToSay = animationsText[currentStep];
					auxPixbuf1 = Gdk.Pixbuf.LoadFromResource("pesco.ejercicios.GiftsShopping.screenshots.6.jpg");
				}
				DrawDialog();
				auxLayout.SetMarkup( "<span color=\"blue\">"+stringToSay.Substring(0, currentCharacter )+"</span>" );
				PepeUtils.IncrementCharacterDialog( ref currentCharacter, ref pepeStatus, stringToSay );
				if ( AfterLastFrameStep() )
					imageBackground.Pixmap.DrawPixbuf( auxGC, auxPixbuf1, 0, 0, 300, 250, auxPixbuf1.Width, auxPixbuf1.Height,0 ,0, 0 );
			} else if ( currentStep == 7 ) {
				if ( FirstFrameStep() ) {
					totalCharacters = animationsText[currentStep].Length;
					stringToSay = animationsText[currentStep];
					auxPixbuf1 = Gdk.Pixbuf.LoadFromResource("pesco.ejercicios.GiftsShopping.screenshots.7.jpg");
				}
				DrawDialog();
				auxLayout.SetMarkup( "<span color=\"blue\">"+stringToSay.Substring(0, currentCharacter )+"</span>" );
				PepeUtils.IncrementCharacterDialog( ref currentCharacter, ref pepeStatus, stringToSay );
				if ( AfterLastFrameStep() )
					imageBackground.Pixmap.DrawPixbuf( auxGC, auxPixbuf1, 0, 0, 300, 250, auxPixbuf1.Width, auxPixbuf1.Height,0 ,0, 0 );
			} else if ( currentStep == 8 ) {
				if ( FirstFrameStep() ) {
					totalCharacters = animationsText[currentStep].Length;
					stringToSay = animationsText[currentStep];
					auxPixbuf1 = Gdk.Pixbuf.LoadFromResource("pesco.ejercicios.GiftsShopping.screenshots.6.jpg");
				}
				DrawDialog();
				auxLayout.SetMarkup( "<span color=\"blue\">"+stringToSay.Substring(0, currentCharacter )+"</span>" );
				PepeUtils.IncrementCharacterDialog( ref currentCharacter, ref pepeStatus, stringToSay );
			} else if ( currentStep == 9 ) {
				if ( FirstFrameStep() ) {
					stringToSay = animationsText[currentStep];
					totalCharacters = animationsText[currentStep].Length;
				}
				DrawDialog();
				auxLayout.SetMarkup( "<span color=\"blue\">"+stringToSay.Substring(0, currentCharacter )+"</span>" );
				PepeUtils.IncrementCharacterDialog( ref currentCharacter, ref pepeStatus, stringToSay );
			} else if ( currentStep == 10 ) {
				if ( FirstFrameStep() ) {
					stringToSay = animationsText[currentStep];
					totalCharacters = animationsText[currentStep].Length;
					buttonGoForward.Sensitive = false;
					buttonStartExercise.ShowAll();
					buttonGoBack.HideAll();
					buttonGoForward.HideAll();
					PepeUtils.GenerateCartel( this.PangoContext, auxGC, imageBackground.Pixmap, "¡Fin de las instrucciones! Comienza el ejercicio" );
				}
				PepeUtils.DrawCartel();
				auxLayout.SetMarkup( "<span color=\"blue\">"+stringToSay.Substring(0, currentCharacter )+"</span>" );
				PepeUtils.IncrementCharacterDialog( ref currentCharacter, ref pepeStatus, stringToSay );
			}
					
			// Draw text
			if ( currentStep != 10 )
				imageBackground.Pixmap.DrawLayout( auxGC,200, 40, auxLayout );
			
		}
		
		private void DrawDialogBackground() {
		
			imageBackground.Pixmap.DrawPixbuf( auxGC, backgroundPixbuf, 0, 0, 0, 0, WIDTH_SCREEN, HEIGHT_SCREEN, 0, 0, 0);

		}
		
		private void DrawDialog() {
			
			imageBackground.Pixmap.DrawPixbuf( auxGC, dialogPixbuf, 0, 0, 0, 0, WIDTH_SCREEN, HEIGHT_SCREEN, 0, 0, 0);
			if ( pepeStatus > 2 ) {
				imageBackground.Pixmap.DrawPixbuf( auxGC, pepe1Pixbuf, 0, 0, 0, HEIGHT_SCREEN - pepe1Pixbuf.Height, pepe1Pixbuf.Width, pepe1Pixbuf.Height, 0, 0, 0);	
			} else {
				imageBackground.Pixmap.DrawPixbuf( auxGC, pepe2Pixbuf, 0, 0, 0, HEIGHT_SCREEN - pepe2Pixbuf.Height, pepe2Pixbuf.Width, pepe2Pixbuf.Height, 0, 0, 0);		
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
		
		bool Update() {
		
			imageBackground.QueueDraw();
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
		
		private bool AfterLastFrameStep ()
		{
			
			if (currentCharacter >= animationsText[currentStep].Length) {
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
			currentStep = animationsText.Length - 1;
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
		
		protected virtual void OnButtonStartExerciseClicked (object sender, System.EventArgs e)
		{
			// Notify session manager
			SessionManager.GetInstance().ChangeExerciseStatus("game");
			
			ExerciseGiftsShopping exGiftsShopping = ExerciseGiftsShopping.getInstance();
			exGiftsShopping.NewSituation();					
		}
	}
}
