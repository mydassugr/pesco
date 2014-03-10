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
using System;

namespace pesco
{


	public class ExerciseEntry
	{
		/// <summary>
		/// Exercise id
		/// </summary>
		[XmlElement("id")]
		int id;
		
		/// <summary>
		/// An exercise can be repeated during x time or x repetitions
		/// Kind value can be "time", "repetitions" or "notime"
		/// </summary>
		[XmlElement("durationkind")]
		string durationKind;
		
		/// <summary>
		/// Duration defines the minutes or the number of repetitions for the exercise
		/// </summary>
		[XmlElement("duration")]
		int duration;
		
		/// <summary>
		/// Repetitions defines the number of repetitions for the exercise
		/// </summary>
		[XmlElement("repetitions")]
		int repetitions;
		
		[XmlElement("dependson")]
		int dependsOn;
		
		[XmlElement("dependsmaxtime")]
		int dependsMaxTime;
		
		public int Id {
			get {
				return id;
			}
			set {
				id = value;
			}
		}
		
		public string DurationKind {
			get {
				return durationKind;
			}
			set {
				durationKind = value;
			}
		}
		
		public int Duration {
			get {
				return duration;
			}
			set {
				duration = value;
			}
		}

		public int Repetitions {
			get {
				return repetitions;
			}
			set {
				repetitions = value;
			}
		}
		
		public int DependsOn {
			get {
				return dependsOn;
			}
			set {
				dependsOn = value;
			}
		}
		
		
		public int DependsMaxTime {
			get {
				return dependsMaxTime;
			}
			set {
				dependsMaxTime = value;
			}
		}
		
		
		
		public ExerciseEntry ()
		{
		}
		
		public ExerciseEntry(int id, string durationkind, int duration, int repetitions, int dependson, int dependsmaxtime) {		
			Id = id;
			DurationKind = durationkind;
			Duration = duration;
			Repetitions = repetitions;
			DependsOn = dependson;
			DependsMaxTime = dependsmaxtime;			
		}
		
		public void showInfo() {
			
			Console.WriteLine("Id: "+id+"| DurationKind: "+DurationKind+" | "+
			                  "Duration: "+ Duration+" | Repetitions: "+Repetitions);
			
		}
		
		public bool IsIndependent() {
			
			return (DependsOn == -1);
			
		}
		
	}
}

