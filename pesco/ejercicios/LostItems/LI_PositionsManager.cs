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
using System.Collections.Generic;

namespace pesco
{
	public class LI_PositionsManager : XmlSerializable
	{
		
		List <LI_Position> positions = new List<LI_Position>();

		public List<LI_Position> Positions {
			get {
				return this.positions;
			}
			set {
				positions = value;
			}
		}		
		
		public LI_PositionsManager ()
		{
			xmlPath = "/"+Configuration.ExercisesFolderName+"/LostItems/xml-templates/li-positions.xml";		
		}
		
		public LI_Position GetPositionById( int id ) {
		
			for (int i = 0; i < positions.Count; i++ ) {
				// Console.WriteLine("Id arg: "+id+" vs Id Room: "+positions[i].Id);
				if ( positions[i].Id == id ) {
					return positions[i];	
				}
				
			}
			
			return null;
			
		}
	}
}


