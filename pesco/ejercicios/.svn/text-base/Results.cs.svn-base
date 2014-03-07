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
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Reflection;

namespace pesco
{
	
	public abstract class SingleResultElement
	{
		
		public SingleResultElement ()
		{

		}
	}
	
	public class ExerciceExecutionResult<T> where T: SingleResultElement{
		
		int sessionId;
		int executionId;
		string levelReached;
		int totalCorrects;
		int totalFails;
		int totalOmissions= -1;
		int totalHelpCounter= -1;
		int totalTimeElapsed;
		int score;
		
		List<T> singleResults = new List<T>();			
				
		[XmlElement("LevelReached")]
		public string LevelReached {
			get {return this.levelReached;}
			set {levelReached = value;}
		}

		[XmlElement("SessionId")]
		public int SessionId {
			get {return this.sessionId;}
			set {sessionId = value;}
		}
		
		[XmlElement("ExecutionId")]
		public int ExecutionId {
			get {return this.executionId;}
			set {executionId = value;}
		}

		[XmlElement("TotalCorrects")]
		public int TotalCorrects {
			get {return this.totalCorrects;}
			set {totalCorrects = value;}
		}

		[XmlElement("TotalFails")]
		public int TotalFails {
			get {return this.totalFails;}
			set {totalFails = value;}
		}

		[XmlElement("TotalHelpCounter")]
		public int TotalHelpCounter {
			get {return this.totalHelpCounter;}
			set {totalHelpCounter = value;}
		}
		
		[XmlElement("TotalOmissions")]
		public int TotalOmissions {
			get {return this.totalOmissions;}
			set {totalOmissions = value;}
		}

		[XmlElement("TotalTimeElapsed")]
		public int TotalTimeElapsed {
			get {return this.totalTimeElapsed;}
			set {totalTimeElapsed = value;}
		}

		[XmlElement("Score")]		
		public int Score {
			get {
				return this.score;
			}
			set {
				score = value;
			}
		}

		[XmlElement("SingleResults")]
		public List<T> SingleResults {
			get {return this.singleResults;}
			set {singleResults = value;}
		}

		public ExerciceExecutionResult(){
			SingleResults = new List<T>();
		}
		
		public ExerciceExecutionResult(int sessionIdArg,string levelReachedArg,int totalCorrectsArg,int totalFailsArg,int totalTimeElapsedArg, int totalOmisionsArg, int totalHelpCounterArg){
			
			SessionId = sessionIdArg;
			LevelReached=levelReachedArg;
			TotalCorrects= totalCorrectsArg;
			TotalFails= totalFailsArg;
			TotalTimeElapsed= totalTimeElapsedArg;
			TotalOmissions = totalOmisionsArg;
			
			
			//SingleResults = new List<T>();
		}
		
		public ExerciceExecutionResult(int sessionIdArg,string levelReachedArg,int totalCorrectsArg,int totalFailsArg,int totalTimeElapsedArg){
			
			SessionId = sessionIdArg;
			LevelReached=levelReachedArg;
			TotalCorrects= totalCorrectsArg;
			TotalFails= totalFailsArg;
			TotalTimeElapsed= totalTimeElapsedArg;
			
			//SingleResults = new List<T>();
		}
		public ExerciceExecutionResult(int sessionIdArg,int executionIdArg){
			
			SessionId = sessionIdArg;
			ExecutionId = executionIdArg;
			
			//SingleResults = new List<T>();
		}
		
	}
	public abstract class Results: XmlSerializable
	{
		
		int exerciseId;
		int categoryId;
		int currentLevel;
		
		
		[XmlElement("CategoryId")]
		public int CategoryId {
			get {return this.categoryId;}
			set {categoryId = value;}
		}

		[XmlElement("CurrentLevel")]
		public int CurrentLevel {
			get {return this.currentLevel;}
			set {currentLevel = value;}
		}

		

		[XmlElement("ExerciseId")]
		public int ExerciseId {
			get {return this.exerciseId;}
			set {exerciseId = value;}
		}
		

		public Results ()
		{
			//vowelsNumberExecutionResults= new List<ExerciceExecutionResult <SingleResultElement>>();
		}
		
		public Results (int exerciseIdArg,int categoryIdArg,int currentLevelArg)
		{
			ExerciseId = exerciseIdArg;
			CategoryId= categoryIdArg;
			CurrentLevel= currentLevelArg;
			
		}
	}
}

