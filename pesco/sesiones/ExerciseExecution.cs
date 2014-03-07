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
using System.Collections.Generic;
using System;

namespace pesco
{


	public class ExerciseExecution
	{
		
		protected DateTime startTime;		
		protected DateTime endTime;
		protected DateTime lastCallTime;
		
		protected int realTime;
		protected int elapsedTime;
		protected int repetitions;
		
		int exerciseId;
		string result;
		[XmlElement("result")]
		public string Result {
			get {
				return result;
			}
			set {
				result = value;
			}
		}
		
		[XmlElement("exerciseId")]
		public int ExerciseId {
			get {
				return exerciseId;
			}
			set {
				exerciseId = value;
			}
		}
		[XmlElement("starttime")]
		public DateTime StartTime {
			get {
				return startTime;
			}
			set {
				startTime = value;
			}
		}
				
		[XmlElement("endtime")]
		public DateTime EndTime {
			get {
				return endTime;
			}
			set {
				endTime = value;
			}
		}

		[XmlElement("realtime")]
		public int RealTime {
			get {
				return realTime;
			}
			set {
				realTime = value;
			}
		}

		[XmlElement("elapsedtime")]
		public int ElapsedTime {
			get {
				return elapsedTime;
			}
			set {
				elapsedTime = value;
			}
		}
		
		[XmlElement("repetitions")]
		public int Repetitions {
			get {
				return repetitions;
			}
			set {
				repetitions = value;
			}
		}
		
		public ExerciseExecution ()
		{
			repetitions = 0;
		}
		
		public bool IsCompleted() {
			
			if ( this.Result == "completed" ) {
				return true;	
			}
			
			return false;
		}
		
		public void InitTimers() {
		
			StartTime = DateTime.Now;
			lastCallTime = DateTime.Now;
		}
		
		public void UpdateTimers(string exerciseStatus) {
			
			EndTime = DateTime.Now;
			TimeSpan auxTimeSpan = new TimeSpan();			
			auxTimeSpan = EndTime - StartTime;
			ElapsedTime = Convert.ToInt32(auxTimeSpan.TotalSeconds );
			// Console.WriteLine( "Updating timers with status: " + exerciseStatus );
			if ( exerciseStatus != "paused" && exerciseStatus != "demo" ) {
				auxTimeSpan = DateTime.Now - lastCallTime ;
				RealTime += Convert.ToInt32( auxTimeSpan.TotalSeconds );
			}
			lastCallTime = DateTime.Now;
		}

	}
}

