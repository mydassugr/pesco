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

namespace pesco
{

	
	[System.ComponentModel.ToolboxItem(true)]
	public partial class PanelDemoPyramidsTest : Gtk.Bin
	{
		
		string [] pyramidsIds = {"c1", "d4", "c2", "c3", "d5", "d9",
								"d17", "c4", "d19", "d22", "c5", "d23",
								"c6", "d24", "d25", "c7", "c8", "d26"};
		
		string [] explanations = {"",
								"No me sirve. No hay pirámide grande y las pequeñas no tienen puerta.",
								"",
								"",
								"En esa postal no hay pirámides pequeñas.",
								"No es como las que busco. Si te fijas bien verás que las pirámides pequeñas no tienen puerta.",
								/* New row */
								"Sólo una de las pirámides pequeñas tienen puerta en esa postal. En las postales que busco tiene que haber dos pirámides pequeñas con puerta.",
								"",
								"Fíjate bien, solo una de las pirámides pequeñas tiene puerta.",
								"Esa postal es casi como las que busco, pero las puertas están en el lado con sombra de la pirámides. Yo busco las puertas en el lado del sol.",
								"",					
								"En esa postal sólo hay una pirámide pequeña con puerta y además está a la sombra.",
								/* New row */
								"",
								"En esa postal no hay pirámide grande y las pirámides pequeñas tienen la puerta a la sombra.",
								"En esa postal una de las pirámides pequeñas tiene la puerta en el lado de la sombra.",
								"",
								"",
								"En esa postal una de las pirámides pequeñas no tiene puerta."};
		
		bool [] valids = { true, false, true, true, false, false, 
						false, true, false, false, true, false, 
						true, false, false, true, true , false };
		
		PanelDemoPyramids parentPanel;
		
		protected virtual void toggleButtonClicked (object sender, System.EventArgs e)
		{
			
			Gdk.Pixbuf tickgreen = Gdk.Pixbuf.LoadFromResource( "pesco.ejercicios.Piramides.figuras.tickgreen.png" );
			Gtk.ToggleButton auxButton = ( Gtk.ToggleButton ) sender;
			
			// Create the image with red tick compositing original image and tick image
	        Gdk.Pixbuf bg = ( ( Gtk.Image) auxButton.Image).Pixbuf;			
	        Gdk.Pixbuf fg = Gdk.Pixbuf.LoadFromResource("pesco.ejercicios.Piramides.figuras.tickred.png");
	
	        Gdk.Pixbuf both = new Gdk.Pixbuf(Gdk.Colorspace.Rgb, true, 8,
	                Math.Max(bg.Width, fg.Width), Math.Max(bg.Height, fg.Height));
	        both.Fill(0);
	
			// Composite pyramid image and tick image in a new image
	        bg.CopyArea(0, 0, bg.Width, bg.Height, both, 0, 0);
	        fg.Composite(both, 0, 0, fg.Width, 105, 0, 0, 1, 1,
	                Gdk.InterpType.Bilinear, 255); 
			
			( ( Gtk.ToggleButton ) sender).Image = new Gtk.Image( both );
			
			
		}
		
		

		public PanelDemoPyramidsTest ( PanelDemoPyramids parent )
		{
			this.Build ();
			
			this.parentPanel = parent;
			
			for ( uint i = 0; i < 18; i++ ) {
			
				PanelDemoPyramidsTestButton auxButton = 
					new PanelDemoPyramidsTestButton( parent, pyramidsIds[i], valids[i], explanations[i] );
				
				tableTest.Attach( auxButton,  i % 6, (i % 6) + 1, i / 6, (i / 6) + 1 );				
				                                                                        				
			}
			
		}
		
		
	}
}

