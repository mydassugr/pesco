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
/*
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System;
using System.Collections.Generic;

namespace pesco
{

	public class PD_ResultsManager : XmlSerializable
	{

		private List <PD_Results> results = new List<PD_Results>();
		private PD_Results currentResults = new PD_Results();
		
		public List<PD_Results> Results {
			get {
				return results;
			}
			set {
				results = value;
			}
		}		
		
		[XmlIgnoreAttribute]
		public PD_Results CurrentResults {
			get {
				return currentResults;
			}
			set {
				currentResults = value;
			}
		}
		
		public void NewResults( int scenarioId ) {
			
			results.Add( new PD_Results( scenarioId ) );
		}
		
		public void AddAction( PD_ResultsAction action ) {
		
			Results[Results.Count-1].AddAction( action );
			
		}
		
		public void SetFinalStatus( PD_BoxStatus [] boxstatus ) {
			
			for ( int i = 0; i < boxstatus.Length; i++ ) {
			
				PD_ResultsBox auxResultBox = new PD_ResultsBox();
				auxResultBox.Boxname = boxstatus[i].BoxName;
				if ( boxstatus[i].GoalPosition == -1 ) {
					auxResultBox.Goal = "van";	
				} else {
					auxResultBox.Goal = boxstatus[i].GoalPosition.ToString();	
				}
				
				if ( boxstatus[i].CurrentPosition == -1 ) {
					auxResultBox.Position = "van";	
				} else {
					auxResultBox.Position = boxstatus[i].CurrentPosition.ToString();	
				}
				
				if ( boxstatus[i].CurrentPosition == boxstatus[i].GoalPosition )
					auxResultBox.Achieved = "YES";
				else
					auxResultBox.Achieved = "NO";
				
				Results[Results.Count-1].Boxes.Add( auxResultBox );
			}
			
		}
		
		public void SaveResults() {
			XmlUtil.SerializeForUser<PD_ResultsManager>(this, Configuration.Current.GetExerciseConfigurationFolderPath()+ "/" + xmlPath);	
		}
		
		public PD_ResultsManager () 
		{
			xmlPath = "package-delivering.xml";
		}
	}
}
*/
