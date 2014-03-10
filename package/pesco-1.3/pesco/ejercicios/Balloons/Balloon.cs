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


	public class Balloon
	{
		public Pango.Layout layout;
		public int angle;
		public int posX;
		public int posY;
		public int offsetX;
		public Random r = new Random(DateTime.Now.Millisecond);
		
		const int WIDTH_GLOBO = 171;
		const int HEIGHT_GLOBO = 230;
		
		int PARENT_WIDTH = 1000;
		int PARENT_HEIGHT = 600;
		
		public Gdk.Pixmap pixmapBalloon;
		public Gdk.Pixbuf pixbufBalloon;
		
		public char character = 'A';
		
		public int PosY {
			get {
				return posY;
			}
			set {
				posY = value;
			}
		}
		
		
		public int PosX {
			get {
				return posX;
			}
			set {
				posX = value;
			}
		}
		
		public char Character {
			get {
				return character;
			}
			set {
				character = value;
			}
		}
		
		public int Angle {
			get {
				return angle;
			}
			set {
				angle = value;
			}
		}	
		
		public Pango.Layout Layout {
			get {
				return layout;
			}
			set {
				layout = value;
			}
		}
				
		public Balloon ()
		{
		}
		
		public Balloon ( Gtk.Widget parent, char letra, int offsetX ) {
		
			this.Character = letra;
			this.offsetX = offsetX;

			PARENT_WIDTH = ((BalloonsPanel)parent).WIDTH_SCREEN;
			PARENT_HEIGHT = ((BalloonsPanel)parent).HEIGHT_SCREEN;
					
			layout = new Pango.Layout(parent.CreatePangoContext());
			layout.Width = Pango.Units.FromPixels(WIDTH_GLOBO);
			layout.Wrap = Pango.WrapMode.Word;
			layout.Alignment = Pango.Alignment.Center;
			layout.FontDescription = Pango.FontDescription.FromString("Ahafoni CLM Bold 50");
			layout.SetMarkup("<span color=\"blue\">" + Character + "</span>");
		
			DrawBalloon();
		
		}
		
		public void DrawBalloon () {
			
			pixmapBalloon = new Gdk.Pixmap( null, WIDTH_GLOBO, HEIGHT_GLOBO, 24 );
			
			Gdk.GC gc = new Gdk.GC(pixmapBalloon);
			gc.RgbFgColor = new Gdk.Color( 0, 255, 0 );
			
			pixmapBalloon.DrawRectangle( gc, true, 0, 0, WIDTH_GLOBO, HEIGHT_GLOBO);
			Gtk.Image auxImage = new Gtk.Image( Configuration.CommandExercisesDirectory + System.IO.Path.DirectorySeparatorChar 
			                                   + "Balloons" + System.IO.Path.DirectorySeparatorChar + "img" 
			                                   + System.IO.Path.DirectorySeparatorChar + "balloon230-24bits.png");
			pixmapBalloon.DrawPixbuf ( gc, 
	                                   auxImage.Pixbuf, 
	                                   0,0,0,0,
	                                   WIDTH_GLOBO,HEIGHT_GLOBO,
	                                   0,0,0);
			pixmapBalloon.DrawLayout ( gc, 0, HEIGHT_GLOBO/4, Layout);
			pixbufBalloon = Gdk.Pixbuf.FromDrawable( pixmapBalloon, pixmapBalloon.Colormap, 
			                                         0,0,0,0,
			                                         WIDTH_GLOBO, HEIGHT_GLOBO);
		
			pixbufBalloon = pixbufBalloon.AddAlpha( true, 0, 255, 0 );
				
		}
		
		// // Returns false if balloon is out limits of screen
		public bool UpdatePosition() {
			
			// PosX += BallonsExercise.getInstance().PixelsPerFrameSubLevel[ BallonsExercise.getInstance().CurrentSubLevel ];
			PosX += offsetX;
			Angle += 2;
			PosY = (int) (Math.Sin(Angle*(Math.PI/180)) * ( (PARENT_HEIGHT / 10)) + 160);
			
			if ( Angle == 360 )
				Angle = 0;
			
			if ( PosX > PARENT_WIDTH ) {
				return false;
			} else {
				return true;	
			}
		
		}
		
	}
}
