using System;
using System.Collections.Generic;
using Gtk;
using Gdk;
using System.Diagnostics;

namespace pesco
{

	[System.ComponentModel.ToolboxItem(true)]
	public partial class WelcomePanel : Gtk.Bin
	{

		// Consts
		const int WIDTH_SCREEN = 1000;
		const int HEIGHT_SCREEN = 600;
	
		// Draw elements
		private Gdk.Pixbuf bgPixbuf;
		private Gdk.Pixbuf backgroundPixbuf;
		private Gdk.Pixbuf titlePixbuf;
		private Gdk.Pixbuf dialogPixbuf;
		private Gdk.Pixbuf pepe1Pixbuf;
		private Gdk.Pixbuf pepe2Pixbuf;
		private Gdk.Pixbuf medalPixbuf;
		private Gdk.Pixbuf goldPixbufScaled;
		private Gdk.Pixbuf silverPixbufScaled;
		private Gdk.Pixbuf bronzePixbufScaled;
		int pepeStatus = 0;
		Gdk.GC auxGC;
		double auxAlpha = 0;
		double auxFactor = 0;
		double auxFactor2 = 0;
		bool auxIncreasing = true;
		bool auxIncreasing2 = true;
		List <WelcomeCloudImg> clouds = new List<WelcomeCloudImg>();		
		
		// Animations
		string stringToSay;
		int currentStep = 0;
		int currentCharacter = 0;
		int totalCharacters = 0;
		string [] animationsText = {"Test"};

		bool firstFrameStep = true;
		
		// Timers
		private int auxAnimationTimer;
		private uint auxTimer;
		private GLib.IdleHandler currentHandler;
		private uint currentInterval;
		private bool pausedExercise;
		private const int REPAINT_SPEED = 50;
		private int pauseTimer = 100;
		long currentTicks = 0;
		long lastTicks = 0;
		long difference = 0;
		long interval = (Stopwatch.Frequency / 30); // Computer speed. 30 is FPS.
		
		// Pango Layout 
		Pango.Layout auxLayout;
	
		public WelcomePanel () {
		
			this.Build ();
			// GtkUtil.SetStyle( buttonOK, Configuration.Current.MediumFont );

			this.Destroyed += delegate {

			};
		
		}
		
		~WelcomePanel() {
			if ( auxTimer != null ) 
				GLib.Source.Remove(auxTimer);
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
					
		public void GenerateClouds() {
			
			for ( int i = 0; i < 10; i++ ) {
			
				WelcomeCloudImg auxCloud = new WelcomeCloudImg();
				auxCloud.GenerateRandom ( 1000 + ( 150*i) );
				clouds.Add( auxCloud );
				
			}
			
		}
		
		public void InitPanel ()
		{
			
			InitLayout ();

			imageBackground.ExposeEvent += Expose_Event;

			backgroundPixbuf = new Gdk.Pixbuf ( Configuration.CommandDirectory+"/gui/welcome/img/background.png" );
			titlePixbuf = new Gdk.Pixbuf ( Configuration.CommandDirectory+"/gui/welcome/img/title.png" );
			
			
			dialogPixbuf = Gdk.Pixbuf.LoadFromResource ("pesco.ejercicios.gui.img.dialog.png");
			pepe1Pixbuf = Gdk.Pixbuf.LoadFromResource ("pesco.ejercicios.gui.img.pepe1.png");
			pepe2Pixbuf = Gdk.Pixbuf.LoadFromResource ("pesco.ejercicios.gui.img.pepe2.png");					

			imageBackground.Pixmap = new Gdk.Pixmap ( this.GdkWindow, WIDTH_SCREEN, HEIGHT_SCREEN, 24);
			
			GenerateClouds();
			
			auxGC = new Gdk.GC(imageBackground.Pixmap);
		
			// Remove this to avoid animation
			if ( currentHandler != null ) 
				GLib.Idle.Remove(currentHandler);
			
			currentHandler = new GLib.IdleHandler( Update );
			auxTimer = GLib.Idle.Add ( currentHandler );
		
			imageBackground.QueueDraw();
			DrawBackground();
			DrawTitle();
		}
			
		void Expose_Event (object obj, ExposeEventArgs args)
		{
			
			DrawBackground();
			
			for ( int i = 0; i < clouds.Count; i++ ) {
				clouds[i].Update();
				clouds[i].Draw( imageBackground.Pixmap, auxGC);	
			}
			
			DrawTitle();
			
		}
		
		private void DrawBackground() {
			imageBackground.Pixmap.DrawPixbuf( auxGC, backgroundPixbuf, 0, 0, 0, 0, WIDTH_SCREEN, HEIGHT_SCREEN, 0, 0, 0);
		}
		
		private void DrawTitle() {
			imageBackground.Pixmap.DrawPixbuf( auxGC, titlePixbuf, 0, 0, 0, 0, titlePixbuf.Width, titlePixbuf.Height, 0, 0, 0);					
		}
		
		private void DrawPepeTalking() {
		
		
			if ( pepeStatus > 2 ) {
				imageBackground.Pixmap.DrawPixbuf( auxGC, pepe1Pixbuf, 0, 0, 0, HEIGHT_SCREEN - pepe1Pixbuf.Height, pepe1Pixbuf.Width, pepe1Pixbuf.Height, 0, 0, 0);	
			} else {
				imageBackground.Pixmap.DrawPixbuf( auxGC, pepe2Pixbuf, 0, 0, 0, HEIGHT_SCREEN - pepe2Pixbuf.Height, pepe2Pixbuf.Width, pepe2Pixbuf.Height, 0, 0, 0);		
			}
			
		}
		
		#region Animation control functions		
		private bool Update() {
			
			if ( this == null ) {
				Console.WriteLine("This should not happen");
				return false;				
			}
			
			currentTicks = System.Diagnostics.Stopwatch.GetTimestamp();
			difference = currentTicks - lastTicks;

			if ( difference > interval ) {
				imageBackground.QueueDraw();
				lastTicks = System.Diagnostics.Stopwatch.GetTimestamp();				
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
		#endregion
		
		protected virtual void OnButtonOKClicked (object sender, System.EventArgs e)
		{
			NextStep();
		}
		
		
	}
}
