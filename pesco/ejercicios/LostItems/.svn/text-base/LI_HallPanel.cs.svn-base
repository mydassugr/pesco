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

namespace pesco
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class LI_HallPanel : Gtk.Bin
	{

		private LIExercise exerInst = null;
		private List<LI_RoomInGame> roomsInGame = null;
		private int idRoom;
		private Gdk.GC auxGC;
		private Gdk.Pixmap auxPixmap;
		private Pango.Layout auxLayout;
		
		// Door pixbuf
		private Gdk.Pixbuf doorPixbuf;

		public Gtk.Image ImageBackground {

			get { return imageBackground; }
		}

		public Gtk.EventBox EventBoxBackground {
			get { return eventboxBackground; }
		}

		public LI_HallPanel ()
		{
			this.Build ();
		}

		public LI_HallPanel (LIExercise instance, List<LI_RoomInGame> rooms)
		{
			this.Build ();
			this.exerInst = instance;
			this.roomsInGame = rooms;
			
			auxLayout = new Pango.Layout ( this.CreatePangoContext() );
			
			this.doorPixbuf = new Gdk.Pixbuf( Configuration.CommandExercisesDirectory + "/LostItems/img/hall/door.png" );
			this.doorPixbuf = this.doorPixbuf.ScaleSimple( 58, 86, Gdk.InterpType.Bilinear );			
		}

		public void DrawInPixmap (Gdk.GC gc, Gdk.Pixmap pixmap)
		{
			
			this.auxGC = gc;
			this.auxPixmap = pixmap;

			if (roomsInGame.Count == 2) {
				DrawHall2Doors ();
			} else if (roomsInGame.Count == 4) {
				DrawHall4Doors ();
			} else if (roomsInGame.Count == 6) {
				DrawHall6Doors ();
			} else if (roomsInGame.Count == 8) {
				DrawHall8Doors ();
			}
		}
		
		private void PangoLayout4Doors () {			
			auxLayout.Width = Pango.Units.FromPixels (180);			
			auxLayout.Justify = false;
			auxLayout.Wrap = Pango.WrapMode.Word;
			auxLayout.Alignment = Pango.Alignment.Center;
			auxLayout.FontDescription = Pango.FontDescription.FromString ("Ahafoni CLM 16");		
		}

		private void PangoLayout6Doors () {			
			auxLayout.Width = Pango.Units.FromPixels (170);			
			auxLayout.Justify = false;
			auxLayout.Wrap = Pango.WrapMode.Word;
			auxLayout.Alignment = Pango.Alignment.Center;
			auxLayout.FontDescription = Pango.FontDescription.FromString ("Ahafoni CLM 16");		
		}
		
		private void PangoLayout8Doors () {			
			auxLayout.Width = Pango.Units.FromPixels (130);			
			auxLayout.Justify = false;
			auxLayout.Wrap = Pango.WrapMode.Word;
			auxLayout.Alignment = Pango.Alignment.Center;
			auxLayout.FontDescription = Pango.FontDescription.FromString ("Ahafoni CLM 14");		
		}		
		
		private void HallLayout () {
			auxLayout.Width = Pango.Units.FromPixels (100);			
			auxLayout.Justify = false;
			auxLayout.Wrap = Pango.WrapMode.Word;
			auxLayout.Alignment = Pango.Alignment.Center;
			auxLayout.FontDescription = Pango.FontDescription.FromString ("Ahafoni CLM 14");		
		}
		
		public void DrawHallButton( Gdk.Pixmap pixmap, Gdk.GC gc ) {
			// Door image
			gc.RgbFgColor = new Gdk.Color( 0xfc, 0xfc, 0xfc );
			pixmap.DrawRectangle( gc, true, new Gdk.Rectangle( 1004, 0, 96, 116) );
			gc.RgbFgColor = new Gdk.Color( 0x00, 0x00, 0x00 );
			pixmap.DrawRectangle( gc, false, new Gdk.Rectangle( 1004, 0, 96, 116) );			
			pixmap.DrawPixbuf( gc, doorPixbuf, 0, 0, 1000 + ( (100 - 58) / 2 ), 0, doorPixbuf.Width - 4, doorPixbuf.Height - 8, 0, 0, 0 );
			// Hall text
			HallLayout();
			auxLayout.SetMarkup( "<span color=\"red\">IR AL PASILLO</span>" );
			pixmap.DrawLayout( gc, 1000, 73, auxLayout );
		}
		
		public void DrawHall2Doors ()
		{
			// Rooms images
			for (int i = 0; i < roomsInGame.Count; i++) {
				
				Gdk.Pixbuf auxPixbuf = roomsInGame[i].Room.BackgroundPixbuf.ScaleSimple( 550, 300, Gdk.InterpType.Bilinear );
				auxPixmap.DrawPixbuf (auxGC, 
				                      auxPixbuf, 
				                      227, 0, 76 + (i+1) * 180 + (i+1) * 76, 250, 190, 300, 
				                      0, 0,	0);
			}
			
			// Wall background
			auxPixmap.DrawPixbuf (auxGC, 
			                        new Gdk.Pixbuf (Configuration.CommandExercisesDirectory + "/LostItems/img/hall/hall2doors.png"), 
			                        0, 0, 0, 0, 1100, 600, 
			                        0, 0, 0);
			
			// Room names
			PangoLayout4Doors();
			for (int i = 0; i < roomsInGame.Count; i++) {
				auxLayout.SetMarkup( "<span color=\"blue\">"+roomsInGame[i].Room.Name.ToUpper()+"</span>" );
				int width;
				int height;
				auxLayout.GetPixelSize( out width, out height );
				auxPixmap.DrawLayout( auxGC, 76 + (i+1) * 180 + (i+1) * 76, 213 - height / 2,  auxLayout );
			}
			
		}
		
		public void DrawHall4Doors ()
		{
			// Rooms images
			for (int i = 0; i < roomsInGame.Count; i++) {
				
				Gdk.Pixbuf auxPixbuf = roomsInGame[i].Room.BackgroundPixbuf.ScaleSimple( 550, 300, Gdk.InterpType.Bilinear );
				auxPixmap.DrawPixbuf (auxGC, 
				                      auxPixbuf, 
				                      227, 0, 76 + i * 180 + i * 76, 250, 190, 300, 
				                      0, 0,	0);
			}
			
			// Wall background
			auxPixmap.DrawPixbuf (auxGC, 
			                        new Gdk.Pixbuf (Configuration.CommandExercisesDirectory + "/LostItems/img/hall/hall4doors.png"), 
			                        0, 0, 0, 0, 1100, 600, 
			                        0, 0, 0);
			
			// Room names
			PangoLayout4Doors();
			for (int i = 0; i < roomsInGame.Count; i++) {
				auxLayout.SetMarkup( "<span color=\"blue\">"+roomsInGame[i].Room.Name.ToUpper()+"</span>" );
				int width;
				int height;
				auxLayout.GetPixelSize( out width, out height );
				auxPixmap.DrawLayout( auxGC, 76 + i * 180 + i * 76, 213 - height / 2,  auxLayout );
			}
			
		}

		public void DrawHall6Doors ()
		{
			int doorWidth = 170;
			int doorHeight = 280;
			int offsetX = 10;
			int middleSeparator = 0;
			// Rooms images
			for (int i = 0; i < roomsInGame.Count; i++) {
				
				Gdk.Pixbuf auxPixbuf = roomsInGame[i].Room.BackgroundPixbuf.ScaleSimple( 513, doorHeight, Gdk.InterpType.Bilinear );
				// With 6 doors, there is a double offset in the middle of the wall (between door 3 and 4)
				if ( i == 3 )
					middleSeparator = 10;
				
				auxPixmap.DrawPixbuf (auxGC, 
				                      auxPixbuf, 
				                      227, 0, offsetX + i * doorWidth + i * offsetX+middleSeparator, 600-50-doorHeight, doorWidth, doorHeight, 
				                      0, 0,	0);
			}
			
			// Wall background
			auxPixmap.DrawPixbuf (auxGC, 
			                        new Gdk.Pixbuf (Configuration.CommandExercisesDirectory + "/LostItems/img/hall/hall6doors.png"), 
			                        0, 0, 0, 0, 1100, 600, 
			                        0, 0, 0);
			
			PangoLayout6Doors();
			// Room names
			for (int i = 0; i < roomsInGame.Count; i++) {
				auxLayout.SetMarkup( "<span color=\"blue\">"+roomsInGame[i].Room.Name.ToUpper()+"</span>" );
				int width;
				int height;
				auxLayout.GetPixelSize( out width, out height );
				// With 6 doors, there is a double offset in the middle of the wall (between door 3 and 4)
				if ( i == 3 )
					middleSeparator = 10;
				
				auxPixmap.DrawLayout( auxGC, offsetX + i * doorWidth + i * offsetX+middleSeparator, 223 - height / 2,  auxLayout );
			}
			
		}

		public void DrawHall8Doors () {
			
			int doorWidth = 120;
			int doorHeight = 220;
			int offsetX = 6;
			int middleSeparator = 0;
			// Rooms images
			for (int i = 0; i < roomsInGame.Count; i++) {
				
				Gdk.Pixbuf auxPixbuf = roomsInGame[i].Room.BackgroundPixbuf.ScaleSimple( 513, doorHeight, Gdk.InterpType.Bilinear );
				
				auxPixmap.DrawPixbuf (auxGC, 
				                      auxPixbuf, 
				                      227, 0, offsetX + i * doorWidth + i * offsetX+middleSeparator, 600-50-doorHeight, doorWidth, doorHeight, 
				                      0, 0,	0);
			}
				
			// Wall background
			auxPixmap.DrawPixbuf (auxGC, 
			                        new Gdk.Pixbuf (Configuration.CommandExercisesDirectory + "/LostItems/img/hall/hall8doors.png"), 
			                        0, 0, 0, 0, 1100, 600, 
			                        0, 0, 0);
			
			// Room names
			PangoLayout8Doors();
			middleSeparator = 0;
			for (int i = 0; i < roomsInGame.Count; i++) {
				auxLayout.SetMarkup( "<span color=\"blue\">"+roomsInGame[i].Room.Name.ToUpper()+"</span>" );
				int width;
				int height;
				auxLayout.GetPixelSize( out width, out height );
				// With 6 doors, there is a double offset in the middle of the wall (between door 3 and 4)
				if ( i == 3 )
					middleSeparator = 10;
				
				auxPixmap.DrawLayout( auxGC, offsetX + i * doorWidth + i * offsetX+middleSeparator, 223 - height / 2,  auxLayout );
			}
			
		}
		
	}
}


