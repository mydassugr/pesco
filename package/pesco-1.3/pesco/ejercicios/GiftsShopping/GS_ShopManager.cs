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


	public class GS_ShopManager : XmlSerializable
	{

		private List <GS_Shop> shops = new List<GS_Shop>();
		
		[XmlElement("shops")]
		public List<GS_Shop> Shops {
			get {
				return shops;
			}
			set {
				shops = value;
			}
		}
		
		public GS_ShopManager() {
		
			xmlPath = "/ejercicios/GiftsShopping/xml-templates/gs-shops.xml";

		}
		
		public GS_Shop GetShop ( int idShop ) {
		
			for ( int i = 0; i < Shops.Count; i++ ) {
				if ( Shops[i].Id == idShop ) {
					return Shops[i];
				}
			}
			
			return null;
			
		}		
	}
}

