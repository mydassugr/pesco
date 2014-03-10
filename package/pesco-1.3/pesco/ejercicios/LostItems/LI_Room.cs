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
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace pesco
{


	public class LI_Room
	{
		private int id;
		private string name;
		private string gender;
		private string image;
		private string background;
		
		private List <string> decorationImages = new List<string>();
		private List <LI_Position> decorationPositions = new List<LI_Position>();
		private List <int> itemsPositions = new List<int>();
		private List <int> coinsPositions = new List<int>();
		
		private Gdk.Pixbuf backgroundPixbuf = null;
		private List <Gdk.Pixbuf> decorationPixbufs = new List<Gdk.Pixbuf>();
		private bool decorationLoaded = false;
		
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
		
		public string Gender {
			get {
				return this.gender;
			}
			set {
				gender = value;
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
		
		public string Background {
			get {
				return this.background;
			}
			set {
				background = value;
			}
		}

		public List<string> DecorationImages {
			get {
				return this.decorationImages;
			}
			set {
				decorationImages = value;
			}
		}

		public List<LI_Position> DecorationPositions {
			get {
				return this.decorationPositions;
			}
			set {
				decorationPositions = value;
			}
		}
		
		public List<int> ItemsPositions {
			get {
				return this.itemsPositions;
			}
			set {
				itemsPositions = value;
			}
		}
		
		public List<int> CoinsPositions {
			get {
				return this.coinsPositions;
			}
			set {
				coinsPositions = value;
			}
		}
		
		[XmlIgnore]
		 public Gdk.Pixbuf BackgroundPixbuf {
			get {
				if ( this.backgroundPixbuf == null ) {
					// this.backgroundPixbuf = new Gdk.Pixbuf( Configuration.CommandExercisesDirectory+"/LostItems/img/rooms/"+image );
					this.backgroundPixbuf = Gdk.Pixbuf.LoadFromResource( "pesco.ejercicios.LostItems.img.rooms."+image );
				}
				return this.backgroundPixbuf;
			}
			set {
				backgroundPixbuf = value;
			}
		}
		
		[XmlIgnore]
		public List<Gdk.Pixbuf> DecorationPixbufs {
			get {
				if ( !decorationLoaded ) {
					for ( int i = 0; i < decorationImages.Count; i++ ) {
						decorationPixbufs.Add( Gdk.Pixbuf.LoadFromResource( "pesco.ejercicios.LostItems.img.rooms.decoration."+decorationImages[i]) );
						decorationLoaded = true;
					}				
				}
				return this.decorationPixbufs;
			}
			set {
				decorationPixbufs = value;
			}
		}
		
		public LI_Room ()
		{
			
		}
		
		public LI_Room ( int id, string name, string image, string background, 
		                List <string> decorationImages, List<LI_Position> decorationPositions,
		                List<int> itemsPositions, List<int> coinsPositions )
		{
			this.id = id;
			this.name = name;
			this.image = image;
			this.background = background;
			this.decorationImages = decorationImages;
			this.decorationPositions = decorationPositions;
			this.itemsPositions = itemsPositions;
			this.coinsPositions = coinsPositions;
			// this.BackgroundPixbuf = new Gdk.Pixbuf( Configuration.CommandExercisesDirectory+"/LostItems/img/rooms/"+image );
			this.BackgroundPixbuf = Gdk.Pixbuf.LoadFromResource( "pesco.ejercicios.LostItems.img.rooms."+image );
		}
	}
}

