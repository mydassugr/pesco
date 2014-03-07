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
	public partial class PanelDemoPyramidsTestButton : Gtk.ToggleButton
	{
		
		private PanelDemoPyramids parentPanel;
		private bool valid;
		private string explanation;
		private Gtk.Image image;
		private Gdk.Pixbuf tickOk = Gdk.Pixbuf.LoadFromResource( "pesco.ejercicios.Piramides.figuras.tickgreen.png" );
		private Gtk.Alignment auxAlignment;
		
		public PanelDemoPyramidsTestButton ()
		{
			this.Build ();
		}
		
		public PanelDemoPyramidsTestButton (PanelDemoPyramids parent, string id, bool valid, string explanation )
		{
			this.Build ();

			this.parentPanel = parent;
			this.valid = valid;
			this.explanation = explanation;
			Gtk.Image auxImage = new Gtk.Image( Configuration.CommandExercisesDirectory + System.IO.Path.DirectorySeparatorChar + 
								"Piramides" + System.IO.Path.DirectorySeparatorChar + "figuras" + 
								System.IO.Path.DirectorySeparatorChar +	id+".jpg");
			Gdk.Pixbuf auxPixbuf = auxImage.Pixbuf;
			this.image97.Pixbuf = auxPixbuf;
			
			this.Toggled += ButtonToggled;
		}
		
		public void ButtonToggled (object sender, EventArgs e) {
					
			if ( this.valid ) {
			
				// Create the image with green tick compositing original image and tick image
		        Gdk.Pixbuf bg = this.image97.Pixbuf;
		        Gdk.Pixbuf fg = this.tickOk;
		
		        Gdk.Pixbuf both = new Gdk.Pixbuf(Gdk.Colorspace.Rgb, true, 8,
		                Math.Max(bg.Width, fg.Width), Math.Max(bg.Height, fg.Height));
		        both.Fill(0);
		
				// Composite pyramid image and tick image in a new image
		        bg.CopyArea(0, 0, bg.Width, bg.Height, both, 0, 0);
		        fg.Composite(both, 0, 0, fg.Width, 105, 0, 0, 1, 1,
		                Gdk.InterpType.Bilinear, 255); 
				
				this.image97.Pixbuf = both;
				this.parentPanel.SayGreat("¡Genial!");
				this.Toggled -= ButtonToggled;				
			} else {
				this.parentPanel.SayBad(this.explanation);
			}

		}
	}
}

