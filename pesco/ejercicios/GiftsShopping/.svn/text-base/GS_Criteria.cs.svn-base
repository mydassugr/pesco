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


	public class GS_Criteria
	{
		
		private int id;
		private string text;
		private int person;
		private int item;
		private int price;
		
		[XmlElement("text")]		
		public string Text {
			get {
				return text;
			}
			set {
				text = value;
			}
		}
		
		[XmlElement("person")]		
		public int Person {
			get {
				return person;
			}
			set {
				person = value;
			}
		}
		
		[XmlElement("item")]		
		public int Item {
			get {
				return item;
			}
			set {
				item = value;
			}
		}
		
		[XmlElement("id")]		
		public int Id {
			get {
				return id;
			}
			set {
				id = value;
			}
		}
		
		[XmlIgnore]
		public int Price {
			get {
				return price;
			}
			set {
				price = value;
			}
		}
		
		
		
		
		public GS_Criteria ()
		{
		}
		
		public GS_Criteria( int id, string text, int person, int item ) {
		
			Id = id;
			Text = text;
			Person = person;
			Item = item;
			
		}
			
	}
}

