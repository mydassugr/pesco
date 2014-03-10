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

namespace pesco
{


	public class BO_Grandfather
	{

		public Pango.Layout layout;

		public int posX = 220;
		public int posY = 420;
		
		const int WIDTH_GLOBO = 171;
		const int HEIGHT_GLOBO = 230;
		
		public Gdk.Pixbuf pixbufPaso1;
		public Gdk.Pixbuf pixbufPaso2;
		public Gdk.Pixbuf pixbufPaso1Normal;
		public Gdk.Pixbuf pixbufPaso2Normal;
		public Gdk.Pixbuf pixbufPaso1Flip;
		public Gdk.Pixbuf pixbufPaso2Flip;
		
		public Gdk.Pixbuf pixbufAbuelo;
		
		public int pasoActual = 0;
		public int auxAnimacion = 0;
		
		public string orientacion = "norte";
		
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
		
		public Pango.Layout Layout {
			get {
				return layout;
			}
			set {
				layout = value;
			}
		}
				
		public BO_Grandfather () {
						
			dibujarAbuelo();
		
		}
		
		public void dibujarAbuelo () {
			
			pixbufPaso1Normal = Gdk.Pixbuf.LoadFromResource("pesco.ejercicios.BagItems.img.abuelo.AbueloPaso1.png");
			pixbufPaso2Normal = Gdk.Pixbuf.LoadFromResource("pesco.ejercicios.BagItems.img.abuelo.AbueloPaso2.png");
			
			pixbufPaso1Flip = pixbufPaso1Normal.Flip(true);
			pixbufPaso2Flip = pixbufPaso2Normal.Flip(true);
			
			pixbufPaso1 = pixbufPaso1Normal;
			pixbufPaso2 = pixbufPaso2Normal;
			
			pixbufAbuelo = pixbufPaso1;			
		
		}
		
		// Devuelve false si el globo se encuentra fuera del área de dibujo
		public bool actualizarPosicion() {
			
			auxAnimacion++;
			
			if ( orientacion == "norte" ) {
				posY -= 3;
				if ( posY == 240 ) {
					orientacion = "este";
					pixbufPaso1 = pixbufPaso1Normal;
					pixbufPaso2 = pixbufPaso2Normal;
				}
			} else if ( orientacion == "este" ) {
				posX += 3;
				if ( posX == 970 ) 
					orientacion = "sur";
			} else if ( orientacion == "sur" ) {
				posY += 3;	
				if ( posY == 480 ) {
					orientacion = "oeste";
					pixbufPaso1 = pixbufPaso1Flip;
					pixbufPaso2 = pixbufPaso2Flip;
				}
			} else if ( orientacion == "oeste") {
				posX -= 3;
				if ( posX == 220 )
					orientacion = "norte";
			}
			
			if ( auxAnimacion == 10 ) {
				if ( pasoActual == 0 ) {
					pixbufAbuelo = pixbufPaso1;
					pasoActual = 1;
				} else {
					pixbufAbuelo = pixbufPaso2;
					pasoActual = 0;
				}
				auxAnimacion = 0;
			}
			
			return true;
		}
		
		public void mover(int pixeles) {
			
			pixeles += 8;
			
			if ( orientacion == "norte" ) {
				posY -= pixeles;				
			} else if ( orientacion == "este" ) {
				posX += pixeles;				
			} else if ( orientacion == "sur" ) {
				posY += pixeles;	
			} else if ( orientacion == "oeste") {
				posX -= pixeles;
			}
		}		
	}
}

