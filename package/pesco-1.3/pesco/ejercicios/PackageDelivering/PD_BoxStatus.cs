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
using System;

namespace pesco
{


	public class PD_BoxStatus
	{
		private int idBox;
		private string boxName;
		private string status;
		private int initialPosition;
		private int goalPosition;
		private int currentPosition;
				
		private Gdk.Pixbuf pixbufBoxBig;
		private Gdk.Pixbuf pixbufBoxSmall;
		
		public int IdBox {
			get {
				return idBox;
			}
			set {
				idBox = value;
			}
		}
		
		public string Status {
			get {
				return status;
			}
			set {
				status = value;
			}
		}
		
		
		public int InitialPosition {
			get {
				return initialPosition;
			}
			set {
				initialPosition = value;
			}
		}		
		
		public int GoalPosition {
			get {
				return goalPosition;
			}
			set {
				goalPosition = value;
			}
		}		
		
		[XmlIgnore]
		public Gdk.Pixbuf PixbufBoxSmall {
			get {
				return pixbufBoxSmall;
			}
			set {
				pixbufBoxSmall = value;
			}
		}
		
		[XmlIgnore]
		public Gdk.Pixbuf PixbufBoxBig {
			get {
				return pixbufBoxBig;
			}
			set {
				pixbufBoxBig = value;
			}
		}
		
		[XmlIgnore]
		public int CurrentPosition {
			get {
				return currentPosition;
			}
			set {
				currentPosition = value;
			}
		}

		public string BoxName {
			get {
				return boxName;
			}
			set {
				boxName = value;
			}
		}
		
		
		public PD_BoxStatus ()
		{
			
			
		}
		
		public PD_BoxStatus ( int idBox, string status, int initialPosition, int goalPosition)
		{
			IdBox = idBox;
			Status = status;			
			InitialPosition = initialPosition;
			GoalPosition = goalPosition;			
		}
		
		public void InitBox() {
		
			CurrentPosition = InitialPosition;
			LoadPixbuf();
			
		}
		
		public void LoadPixbuf() {
			
			Gdk.Pixbuf auxPixbuf = null;
			// 0: Red box
			if ( idBox == 0 ) {
				auxPixbuf = Gdk.Pixbuf.LoadFromResource ("pesco.ejercicios.PackageDelivering.img.box.boxred.png");				
			} 
			// 1: Blue box 
			else if ( idBox == 1 ) {
				auxPixbuf = Gdk.Pixbuf.LoadFromResource ("pesco.ejercicios.PackageDelivering.img.box.boxblue.png");
			}
			// 2: Brown box
			else if ( idBox == 2 ) {
				auxPixbuf = Gdk.Pixbuf.LoadFromResource ("pesco.ejercicios.PackageDelivering.img.box.boxbrown.png");				
			} 
			// 3: Yellow box
			else if ( idBox == 3 ) {
				auxPixbuf = Gdk.Pixbuf.LoadFromResource ("pesco.ejercicios.PackageDelivering.img.box.boxyellow.png");
			}
			// 4: Black box
			else if ( idBox == 4 ) {
				auxPixbuf = Gdk.Pixbuf.LoadFromResource ("pesco.ejercicios.PackageDelivering.img.box.boxblack.png");
			}
			// 5: White
			else if ( idBox == 5 ) {
				auxPixbuf = Gdk.Pixbuf.LoadFromResource ("pesco.ejercicios.PackageDelivering.img.box.boxwhite.png");
			}
			
			this.PixbufBoxSmall = auxPixbuf.ScaleSimple( 50, 50, Gdk.InterpType.Bilinear );
			this.pixbufBoxBig = auxPixbuf.ScaleSimple( 150, 150, Gdk.InterpType.Bilinear );
			
		}
	}
}

