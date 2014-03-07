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


	public class PD_SceneManager : XmlSerializable
	{

		private List <PD_Scene> scenes = new List<PD_Scene>();
		public List<PD_Scene> Scenes {
			get {
				return scenes;
			}
			set {
				scenes = value;
			}
		}
		
		public PD_SceneManager ()
		{			
			xmlPath = "/ejercicios/PackageDelivering/xml-templates/pd-scenes.xml";
			/*			
			int [] auxPlaces  = new int[16];
			for ( int i = 0; i < 16; i++ ) {
				auxPlaces[i] = i;	
			}
			
			int [][] conections = new int[16][];
			for ( int i = 0; i < 16; i++ ) {
				conections[i] = new int[8];	
			}
			for ( int i = 0; i < 16; i++ ) {
				for ( int j = 0; j < 8; j++ ) {
					conections[i][j] = 0;	
				}
			}
			
			PD_BoxStatus [] boxesStatus = new PD_BoxStatus[4];
			boxesStatus[0] = new PD_BoxStatus(0, "pending", 1, 5);
			boxesStatus[1] = new PD_BoxStatus(1, "pending", 2, 5);
			boxesStatus[2] = new PD_BoxStatus(2, "pending", 3, 15);
			boxesStatus[3] = new PD_BoxStatus(3, "pending", 4, 20);			

			Scenes.Add( new PD_Scene(auxPlaces, conections, boxesStatus) );
			*/
		}
	}
}

