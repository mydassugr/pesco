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
using System.IO;
using System;

namespace pesco
{


	public class GS_Person
	{

		private int id;
		private string name;
		private string img;
		private int edad;
		
		[XmlElement("id")]
		public int Id {
			get {
				return id;
			}
			set {
				id = value;
			}
		}
		
		[XmlElement("name")]
		public string Name {
			get {
				return name;
			}
			set {
				name = value;
			}
		}
		
		[XmlElement("edad")]
		public int Edad {
			get {
				return edad;
			}
			set {
				edad = value;
			}
		}
		
		[XmlElement("img")]
		public string Img {
			get {
				return img;
			}
			set {
				img = value;
			}
		}
		
		public GS_Person ()
		{
		}
		
		public GS_Person (int id, string name, int edad, string img)
		{
			
			Id = id;
			Name = name;
			Edad = edad;
			Img = img;
		}		
	}
}

