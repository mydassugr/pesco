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

	public class GS_CriteriaManager : XmlSerializable
	{		
		private List <GS_Criteria> criterias = new List<GS_Criteria>();
		
		[XmlElement("criterias")]
		public List<GS_Criteria> Criterias {
			get {
				return criterias;
			}
			set {
				criterias = value;
			}
		}
		
				
		public GS_CriteriaManager () {
			
			xmlPath = "/ejercicios/GiftsShopping/xml-templates/gs-criterias.xml";
		
		}
		
		public GS_Criteria GetNewCriteria( List<GS_Criteria> currentCriterias, List<GS_Shop> avalaibleShops ) {
						
			Random r = new Random();
			int candidate = -1;
			bool validCandidate = false;			
			while ( !validCandidate ) {				
				candidate = r.Next(0, Criterias.Count );
				// If candidate is not already choosen and shop is avalaible
				if ( !currentCriterias.Contains( Criterias[candidate] ) ) {
					validCandidate = true;	
				} else {
					validCandidate = false;	
				}

				GS_Item auxItem = ExerciseGiftsShopping.getInstance().itemManager.GetItem(Criterias[candidate].Item);
								
				if ( validCandidate && avalaibleShops.Contains( 
				    	ExerciseGiftsShopping.getInstance().shopManager.GetShop (
				    		ExerciseGiftsShopping.getInstance().itemManager.GetItem(Criterias[candidate].Item).Shop )
				       ) 
				    ) 
				{
					validCandidate = true;
				} else {
					validCandidate = false;	
				}
			}
			
			return Criterias[candidate];
			
		}
	}
}

