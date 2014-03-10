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
using System.Collections.Generic;

namespace pesco
{
	
	public class GS_ItemManager : XmlSerializable
	{
		
		private List <GS_Item> items = new List<GS_Item>();
		
		[XmlElement("items")]
		public List<GS_Item> Items {
			get {
				return items;
			}
			set {
				items = value;
			}
		}
				
		public GS_ItemManager ()
		{			
			xmlPath = "/ejercicios/GiftsShopping/xml-templates/gs-items.xml";
			
			List <string> auxTags = new List<string>();
		}
	
		public GS_Item GetItem( int id ) {
		
			for ( int i = 0; i < Items.Count; i++ ) {
				if ( Items[i].Id == id ) {
					return Items[i];
				}
			}
			
			return null;
			
		}
		
		public List<GS_Item> GetItemsOfShop( int idShop ) {
		
			List <GS_Item> auxItems = new List<GS_Item>();
			for ( int i = 0; i < Items.Count; i++ ) {
				if ( Items[i].Shop == idShop	) {
					auxItems.Add( Items[i] );
				}
			}
			
			return auxItems;
		}
	}
}

