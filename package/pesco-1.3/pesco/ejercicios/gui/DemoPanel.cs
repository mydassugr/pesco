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

namespace pesco
{
	
	[System.ComponentModel.ToolboxItem(true)]
	public partial class DemoPanel : Gtk.Bin
	{
		
		// Consts
		const int WIDTH_SCREEN = 1000;
		const int HEIGHT_SCREEN = 600;
		
		// Draw elements
		private Gdk.Pixbuf bgPixbuf;
		private Gdk.Pixbuf backgroundPixbuf;
		private Gdk.Pixbuf dialogPixbuf;
		private Gdk.Pixbuf pepe1Pixbuf;
		private Gdk.Pixbuf pepe2Pixbuf;
		int pepeStatus = 0;
		Gdk.GC auxGC;
		
		// Animations
		string stringToSay;
		int currentStep = 0;
		int currentCharacter = 0;
		int totalCharacters = 0;
		bool firstFrameStep = true;
		
		// Timers
		private int auxAnimationTimer;
		private int auxTiempoAnimacion2;
		private uint auxTimer;
		private GLib.TimeoutHandler currentHandler;
		private uint currentInterval;
		private bool pausedExercise;
		private const int REPAINT_SPEED = 50;
		int pauseTimer = 100;
		
		// Layout 
		Pango.Layout layout;
		
		public Button ButtonNext{
			get{
				return this.buttonGoForward;	
			}
		}
				
		public DemoPanel (string text)
		{
			this.Build ();
			
			// Init layout
			InitLayout();
			
			GtkUtil.SetStyle(buttonGoForward, Configuration.Current.MediumFont);
			
			// Attaching Expose
			imageBackground.ExposeEvent += Expose_Event;
			
			// Load resources
			backgroundPixbuf = Gdk.Pixbuf.LoadFromResource ("pesco.ejercicios.resources.img.background.png");
			dialogPixbuf = Gdk.Pixbuf.LoadFromResource ("pesco.ejercicios.resources.img.dialog.png");
			pepe1Pixbuf = Gdk.Pixbuf.LoadFromResource ("pesco.ejercicios.resources.img.pepe1.png");
			pepe2Pixbuf = Gdk.Pixbuf.LoadFromResource ("pesco.ejercicios.resources.img.pepe2.png");

			// Init graphics
			imageBackground.Pixmap = new Gdk.Pixmap (this.GdkWindow, WIDTH_SCREEN, HEIGHT_SCREEN, 24);
			auxGC = new Gdk.GC(imageBackground.Pixmap);
						
			// Start time
			currentHandler = new GLib.TimeoutHandler (Update);
			currentInterval = 25;
			auxTimer = GLib.Timeout.Add (currentInterval, currentHandler);
			
			// Text to say
			stringToSay = text;
		}
		
		private void InitLayout ()
		{			
			layout = new Pango.Layout (this.CreatePangoContext ());
			layout.Width = Pango.Units.FromPixels (720);
			layout.Justify = true;
			layout.Wrap = Pango.WrapMode.Word;
			layout.Alignment = Pango.Alignment.Left;
			layout.FontDescription = Pango.FontDescription.FromString ("Ahafoni CLM Bold 20");	
		}
		
		void Expose_Event (object obj, ExposeEventArgs args)
		{
			// Draw background
			DrawDialogBackground();
			
			// Hello
			if (currentStep == 0) {				
				if ( FirstFrameStep() ) {
					totalCharacters = stringToSay.Length;
				}
				IncrementCharacterDialog();
				layout.SetMarkup( "<span color=\"blue\">"+stringToSay.Substring(0, currentCharacter)+"</span>" );
			} 

			// Draw text
			imageBackground.Pixmap.DrawLayout( auxGC, 200, 40, layout );		
		
		}
		
		private void DrawDialogBackground() {
		
			imageBackground.Pixmap.DrawPixbuf( auxGC, backgroundPixbuf, 0, 0, 0, 0, WIDTH_SCREEN, HEIGHT_SCREEN, 0, 0, 0);
			imageBackground.Pixmap.DrawPixbuf( auxGC, dialogPixbuf, 0, 0, 0, 0, WIDTH_SCREEN, HEIGHT_SCREEN, 0, 0, 0);
			if ( pepeStatus > 2 ) {
				imageBackground.Pixmap.DrawPixbuf( auxGC, pepe1Pixbuf, 0, 0, 0, 600 - pepe1Pixbuf.Height, pepe1Pixbuf.Width, pepe1Pixbuf.Height, 0, 0, 0);	
			} else {
				imageBackground.Pixmap.DrawPixbuf( auxGC, pepe2Pixbuf, 0, 0, 0, 600 - pepe2Pixbuf.Height, pepe2Pixbuf.Width, pepe2Pixbuf.Height, 0, 0, 0);		
			}			
		}
		
		private void IncrementCharacterDialog() {
			if (currentCharacter < totalCharacters) {
				currentCharacter++;
				pepeStatus = currentCharacter % 5;
			} else {
				pepeStatus = 0;	
			}
		}
		
		private bool Update() {		
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
	}
}


