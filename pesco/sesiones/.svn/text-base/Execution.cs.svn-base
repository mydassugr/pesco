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


	public class Execution
	{
		
		int executionId;
		int sessionId;
		string date;

		protected DateTime startTime;		
		protected DateTime endTime;
		
	
		[XmlElement("executionid")]
		public int ExecutionId {
			get {
				return executionId;
			}
			set {
				executionId = value;
			}
		}
		
		[XmlElement("sessionId")]
		public int SessionId {
			get {
				return sessionId;
			}
			set {
				sessionId = value;
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
				
		[XmlElement("exercisesExecutions")]
		List <ExerciseExecution> exercises = new List<ExerciseExecution>();		
		
		public List<ExerciseExecution> Exercises {
			get {
				return exercises;
			}
			set {
				exercises = value;
			}
		}
		
		[XmlElement("date")]
		public string Date {
			get {
				return date;
			}
			set {
				date = value;
			}
		}
		
		public Execution() {}
		
		public Execution ( int executionid )
		{
			ExecutionId = executionid;
		}
		
		public void InitTimers() {
			
			StartTime = DateTime.Now;
			
		}
		
		public void UpdateTimers() {
		
			EndTime = DateTime.Now;
			
		}
		
		public void AddExercise( ExerciseEntry exercise ) {
		
			ExerciseExecution auxExerciseExecution = new ExerciseExecution();
			auxExerciseExecution.ExerciseId = exercise.Id;
			auxExerciseExecution.Result = "incomplete";
			
			exercises.Add( auxExerciseExecution );
			
		}
		
		public ExerciseExecution GetFirstIncompleteExercise() {
		
			for ( int i = 0; i < exercises.Count; i++ ) {
				if ( !exercises[i].IsCompleted() ) {
					return exercises[i];	
				}
			}
			
			return null;
			
		}
		
		public ExerciseExecution GetLastExercise() {
			
			if ( Exercises.Count == 0 )
				return null;
			else
				return Exercises[Exercises.Count-1];
		}
			     
	}
}

