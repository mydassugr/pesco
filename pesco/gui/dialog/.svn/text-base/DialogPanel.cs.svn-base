using System;
using Gtk;
using Gdk;
using System.Diagnostics;
using System.Collections.Generic;

namespace pesco
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class DialogPanel : Gtk.Bin
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
		int pepeStatus = 0;
		Gdk.GC auxGC;
		// Pango Layout 
		Pango.Layout auxLayout;
		
		// Animations
		string stringToSay;
		int currentStep = 0;		

		public int CurrentStep {
			get {
				return this.currentStep;
			}
			set {
				currentStep = value;
			}
		}

		int currentCharacter = 0;
		int totalCharacters = 0;

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
		
		// Texts
		List <string> textsToShow = new List<string>();
		
		public List<string> TextsToShow {
			get {
				return this.textsToShow;
			}
			set {
				textsToShow = value;
			}
		}

int currentTextIndex = 0;
		
		public DialogPanel ()
		{
			this.Build ();			
			
		}
		
		~DialogPanel() {
		
			if ( currentHandler != null ) 
				GLib.Idle.Remove(currentHandler);
			
		}
		
		private void InitLayout ()
		{
			
			auxLayout = new Pango.Layout (this.CreatePangoContext ());
			auxLayout.Width = Pango.Units.FromPixels (730);
			auxLayout.Justify = false;
			auxLayout.Wrap = Pango.WrapMode.Word;
			auxLayout.Alignment = Pango.Alignment.Left;
			auxLayout.FontDescription = Pango.FontDescription.FromString ("Ahafoni CLM Bold 20");
			
		}		
	
		public void InitPanel ()
		{
			
			InitLayout ();
			
			this.imageBackground.ExposeEvent += Expose_Event;

			backgroundPixbuf = new Gdk.Pixbuf ( Configuration.CommandDirectory+"/gui/welcome/img/background.png" );
			
			dialogPixbuf = new Gdk.Pixbuf ( Configuration.CommandExercisesDirectory+"/resources/img/dialog.png" );
			pepe1Pixbuf = new Gdk.Pixbuf ( Configuration.CommandExercisesDirectory+"/resources/img/pepe1.png" );
			pepe2Pixbuf = new Gdk.Pixbuf ( Configuration.CommandExercisesDirectory+"/resources/img/pepe2.png" );

			imageBackground.Pixmap = new Gdk.Pixmap ( this.GdkWindow, WIDTH_SCREEN, HEIGHT_SCREEN, 24);
						
			auxGC = new Gdk.GC(imageBackground.Pixmap);
						
			if ( currentHandler != null ) 
				GLib.Idle.Remove( currentHandler );
			
			currentHandler = new GLib.IdleHandler( Update );
			auxTimer = GLib.Idle.Add ( currentHandler );
			
		}
		
			public void SetText( string text ) {
			
			textsToShow.Add(text);
			
		}
		
		public void SetText( List <string> texts ) {
			
			textsToShow = texts;	
			
		}
		
		void Expose_Event (object obj, ExposeEventArgs args)
		{

			// Draw background
			DrawBackground();
					
			// Draw dialog background
			DrawDialogBackground();	
			
			if ( textsToShow.Count > 0 ) {
				// Draw current text
				if ( FirstFrameStep() ) {
					stringToSay = textsToShow[currentTextIndex];
					totalCharacters = textsToShow[currentTextIndex].Length;
					InitLayout();				
				}		
				
				// Balloon text
				PepeUtils.IncrementCharacterDialog( ref currentCharacter, ref pepeStatus, stringToSay );
				auxLayout.SetMarkup( "<span color=\"blue\">"+stringToSay.Substring(0, currentCharacter)+"</span>" );
				imageBackground.Pixmap.DrawLayout( auxGC,200, 34, auxLayout );
				// Pepe
				DrawPepeTalking();
			}
			
		}
		
		private void DrawBackground() {
			imageBackground.Pixmap.DrawPixbuf( auxGC, backgroundPixbuf, 0, 0, 0, 0, WIDTH_SCREEN, HEIGHT_SCREEN, 0, 0, 0);
		}
			
		private void DrawDialogBackground() {
			imageBackground.Pixmap.DrawPixbuf( auxGC, dialogPixbuf, 0, 0, 0, 0, WIDTH_SCREEN, HEIGHT_SCREEN, 0, 0, 0);
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
		
		public void NextStep() {
			firstFrameStep = true;
			currentStep++;
			currentTextIndex++;
			currentCharacter = 0;
		}
		
		public void BackStep() {
			if ( currentStep != 0 ) {
				firstFrameStep = true;
				currentCharacter = 0;
				currentStep--;
				currentTextIndex--;
			}
		}
		#endregion
	}
}

