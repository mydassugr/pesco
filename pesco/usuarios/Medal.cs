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
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace pesco
{
	
	public enum MedalValue{
		Gold,
		Silver,
		Bronze
	}
	
	public class Medal
	{
		MedalValue medalValue;
		
		[XmlElement("value")]
		public MedalValue MedalValue {
			get {
				return this.medalValue;
			}
			set {
				medalValue = value;
			}
		}

		DateTime date;
		
		[XmlElement("date")]
		public DateTime Date {
			get {
				return this.date;
			}
			set{
				date = value;	
			}
		}

		
		int exerciseId;
		
		[XmlElement("exercise-id")]
		public int ExerciseId {
			get {
				return this.exerciseId;
			}
			set {
				exerciseId = value;
			}
		}

		int sessionId;
		
		[XmlElement("session-id")]
		public int SessionId {
			get {
				return this.sessionId;
			}
			set {
				sessionId = value;
			}
		}		
		
		public Medal (MedalValue val, DateTime dat, int exerciseID, int sessionID)
		{
			this.medalValue = val;
			date = dat;
			exerciseId = exerciseID;
			sessionId = sessionID;			
		}
		
		public Medal(){
			
		}
	}
}


