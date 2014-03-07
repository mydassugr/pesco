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


	public class BO_Item
	{
		private string id;
		private string genero;
		private string nombreSingular;
		private string nombrePlural;
		private string nombreSimple;
		private string img;
		
		public Gdk.Pixbuf pixbufObjeto;
		public Gdk.Pixbuf pixbufObjetoSmall;
		
		public string NombreSingular {
			get {
				return nombreSingular;
			}
			set {
				nombreSingular = value;
			}
		}
		
		
		public string NombrePlural {
			get {
				return nombrePlural;
			}
			set {
				nombrePlural = value;
			}
		}
		
		
		public string Img {
			get {
				return img;
			}
			set {
				img = value;
			}
		}
		
		
		public string Id {
			get {
				return id;
			}
			set {
				id = value;
			}
		}
		
		
		public string Genero {
			get {
				return genero;
			}
			set {
				genero = value;
			}
		}
		
		public string NombreSimple {
			get {
				return nombreSimple;
			}
			set {
				nombreSimple = value;
			}
		}
		
		
		
		
		public BO_Item ()
		{
		}
		
		public BO_Item (string id, string genero, string nombresingular, string nombreplural, string nombresimple, string img) {
		
			this.Id = id;
			this.Genero = genero;
			this.NombreSingular = nombresingular;
			this.NombrePlural = nombreplural;
			this.NombreSimple = nombresimple;
			this.Img = img;
			
			this.pixbufObjeto = Gdk.Pixbuf.LoadFromResource(img);
			this.pixbufObjeto = this.pixbufObjeto.ScaleSimple(150, 150, Gdk.InterpType.Bilinear);
			this.pixbufObjetoSmall = this.pixbufObjeto.ScaleSimple(55, 55, Gdk.InterpType.Bilinear);
		}
	}
}

