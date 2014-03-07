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
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using System;

namespace pesco
{


	public class LI_Item
	{
		
		#region Variables
		private int id;
		private string name;
		private string image;
		private int room;
		private List<int> avoidRoom;		
		private int width;		
		private int height;
		private List<string> tags;
		private Gdk.Pixbuf itemPixbuf = null;
		
		// private List <int> validPositions = new List<int>();
		// private List <int> invalidPositions = new List<int>();
		
		#endregion
		
		#region Public getters/setters
		public int Id {
			get {
				return this.id;
			}
			set {
				id = value;
			}
		}
		
		public string Name {
			get {
				return this.name;
			}
			set {
				name = value;
			}
		}
		
		public string Image {
			get {
				return this.image;
			}
			set {
				image = value;
			}
		}

		public int Room {
			get {
				return this.room;
			}
			set {
				room = value;
			}
		}

		public int Width {
			get {
				return this.width;
			}
			set {
				width = value;
			}
		}
		
		public List<int> AvoidRoom {
			get {
				return this.avoidRoom;
			}
			set {
				avoidRoom = value;
			}
		}
		
		public int Height {
			get {
				return this.height;
			}
			set {
				height = value;
			}
		}
		
		public List<string> Tags {
			get {
				return this.tags;
			}
			set {
				tags = value;
			}
		}
		#endregion

		[XmlIgnoreAttribute]
		public Gdk.Pixbuf ItemPixbuf {
			get {
				if ( this.itemPixbuf == null ) {
					// this.itemPixbuf = new Gdk.Pixbuf( Configuration.CommandExercisesDirectory+"/LostItems/img/items/"+this.Image );
					char auxSeparator = System.IO.Path.DirectorySeparatorChar;					
					Gtk.Image auxImage = new Gtk.Image( Configuration.CommandExercisesDirectory + auxSeparator + "LostItems" 
					                                   + auxSeparator + "img" + auxSeparator + "items" + auxSeparator + this.Image );
					this.itemPixbuf = auxImage.Pixbuf.ScaleSimple( this.width, this.height, Gdk.InterpType.Bilinear );
				}
				return this.itemPixbuf;
			}
			set {
				itemPixbuf = value;
			}
		}

		public LI_Item ()
		{
		}
		
		public LI_Item ( int id, string name, string image, int room, List <string> tags ) {
		                // List<int> validPositions, List <int> invalidPositions ) {
		
			this.id = id;
			this.name = name;
			this.image = image;
			this.room = room;
			this.tags = tags;
			/*this.validPositions = validPositions;
			this.invalidPositions = invalidPositions;*/
			
		}
		
		public bool FitInPosition ( LI_Position position ) {
		
			// Console.WriteLine("Comprobando si "+this.Name+" con "+this.Width+","+this.Height+" cabe en "+position.Id+" con "+position.Width+","+position.Height);
			if ( this.Width <= position.Width && this.Height <= position.Height ) {				
				foreach ( string tag in this.Tags ) {
					if ( position.Tags.Contains( tag ) ) {
						// Console.WriteLine( tag+" esta en los tags");
						return true;
					}
				}
			}
			
			return false;
		}
		
		public bool FitInRoom ( LI_Room room ) {
			
			// Console.WriteLine("Comprobando "+this.Name+" en habitación "+room.Name);
			for ( int i = 0; i < room.ItemsPositions.Count; i++ ) {
				LI_Position auxPosition = LIExercise.GetInstance().PositionsManager.GetPositionById(room.ItemsPositions[i]); 
				if ( this.FitInPosition( auxPosition) ) {
					return true;
				}
			}
			
			return false;
		}
	}
}

