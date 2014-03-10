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
using Gtk;
using System.Collections.Generic;

using System.Collections;

namespace pesco
{

	public class PD_Place
	{

		private string id;
		private string name;
		private string img;
		private string text;

		private Gdk.Pixbuf pixbufLugar;
		
		public string Id {
			get { return id; }
			set { id = value; }
		}

		public string Name {
			get { return name; }
			set { name = value; }
		}

		public string Img {
			get { return img; }
			set { img = value; }
		}

		[XmlIgnore]
		public Gdk.Pixbuf PixbufLugar {
			get {
				return pixbufLugar;
			}
			set {
				pixbufLugar = value;
			}
		}
		
		public string Text {
			get {
				return text;
			}
			set {
				text = value;
			}
		}
		
		
		public PD_Place ()
		{
		}

		public PD_Place (string id, string name, string text, string img)
		{
			
			this.id = id;
			this.Name = name;
			this.Text = text;
			this.Img = img;
					
		}
		
		public void LoadPixbuf() {
			this.pixbufLugar = Gdk.Pixbuf.LoadFromResource (img);
			this.pixbufLugar = this.pixbufLugar.ScaleSimple( 110, 110, Gdk.InterpType.Bilinear );
		}
		
	}
}


