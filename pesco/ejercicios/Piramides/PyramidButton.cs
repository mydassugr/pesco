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


	public class PyramidButton : Button
	{
		private Image buttonImageNormal;
		private Image buttonImageOk;
		
		public bool correctPyramid;
		public bool checkedPyramid;
		
		public bool CheckedPyramid {
			get {
				return checkedPyramid;
			}
			set {
				checkedPyramid = value;
			}
		}
		
		public bool CorrectPyramid {
			get {
				return correctPyramid;
			}
			set {
				correctPyramid = value;
			}
		}
		
		
		// Constructors
	  	public PyramidButton(): base() {}
		
		public PyramidButton(Image image, bool correct): base(image) {
			
			checkedPyramid = false;
			correctPyramid = correct;
			// A new image has to be created in order to have a copy of the original one (original one
			// will be replaced by original + red tick composition)
			buttonImageNormal = new Image(image.Pixbuf);
						
			// Create the image with red tick compositing original image and tick image
	        Gdk.Pixbuf bg = image.Pixbuf;
			Gtk.Image auxImage = new Gtk.Image( Configuration.CommandExercisesDirectory + System.IO.Path.DirectorySeparatorChar + 
			                                   "Piramides" +  System.IO.Path.DirectorySeparatorChar +
			                                   "figuras" +  System.IO.Path.DirectorySeparatorChar + "tickred.png" );
	        Gdk.Pixbuf fg = auxImage.Pixbuf;
	
	        Gdk.Pixbuf both = new Gdk.Pixbuf(Gdk.Colorspace.Rgb, true, 8,
	                Math.Max(bg.Width, fg.Width), Math.Max(bg.Height, fg.Height));
	        both.Fill(0);
	
			// Composite pyramid image and tick image in a new image
	        bg.CopyArea(0, 0, bg.Width, bg.Height, both, 0, 0);
	        fg.Composite(both, 0, 0, fg.Width, 105, 0, 0, 1, 1,
	                Gdk.InterpType.Bilinear, 255); 
			
			this.buttonImageOk = new Image(both);	
		}
		
		// Replace images
		public void ToggleImages() {
			
			if ( checkedPyramid == false ) { 
				((Image) Child).Pixbuf = buttonImageOk.Pixbuf;
			} else {
				((Image) Child).Pixbuf = buttonImageNormal.Pixbuf;
			}
			
		}
		
		// Toggle pyramid button
		public void TogglePyramid() {	
			ToggleImages();
			checkedPyramid = !this.checkedPyramid;
		}
		
	}
}
