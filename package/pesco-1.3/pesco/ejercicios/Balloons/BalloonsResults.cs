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


namespace pesco
{

	public class SingleResultBalloons: SingleResultElement {
		
		private string showedSerie;
		private int balloonsNumber;
		private int balloonsGoal;
		private int corrects;
		private int fails;
		private int omissions;
		private int level;
		private int subLevel;
		private int timeElapsed;
				
		[XmlElement("ShowedSerie")]
		public string ShowedSerie {
			get {
				return this.showedSerie;
			}
			set {
				showedSerie = value;
			}
		}
		
		[XmlElement("BalloonsNumber")]
		public int BalloonsNumber {
			get {
				return this.balloonsNumber;
			}
			set {
				balloonsNumber = value;
			}
		}
		
		[XmlElement("BalloonsGoal")]
		public int BalloonsGoal {
			get {
				return this.balloonsGoal;
			}
			set {
				balloonsGoal = value;
			}
		}

		[XmlElement("Corrects")]
		public int Corrects {
			get {
				return this.corrects;
			}
			set {
				corrects = value;
			}
		}

		[XmlElement("Fails")]
		public int Fails {
			get {
				return this.fails;
			}
			set {
				fails = value;
			}
		}
		
		[XmlElement("Omissions")]		
		public int Omissions {
			get {
				return this.omissions;
			}
			set {
				omissions = value;
			}
		}
		
		[XmlElement("Level")]
		public int Level {
			get {
				return this.level;
			}
			set {
				level = value;
			}
		}

		[XmlElement("SubLevel")]
		public int SubLevel {
			get {
				return this.subLevel;
			}
			set {
				subLevel = value;
			}
		}

		[XmlElement("TimeElapsed")]
		public int TimeElapsed {
			get {
				return this.timeElapsed;
			}
			set {
				timeElapsed = value;
			}
		}

		public SingleResultBalloons(){}
		
		public SingleResultBalloons(string showedSerie,int balloonsNumber,int balloonsGoal, int corrects,
		                                 int fails, int omissions, int level, int subLevel, int timeElapsed){
			
			ShowedSerie = showedSerie;
			BalloonsNumber = balloonsNumber;
			BalloonsGoal = balloonsGoal;
			Corrects = corrects;
			Fails = fails;
			Omissions = omissions;
			Level = level;
			SubLevel = subLevel;
			TimeElapsed = timeElapsed;
			
		}
		
	}

	public class BalloonsResults : ExerciseResults 
	{
		
		List <ExerciceExecutionResult<SingleResultBalloons>> balloonsResults;
		
		int currentSubLevel = 0;

		[XmlElement("CurrentSubLevel")]
		public int CurrentSubLevel {
			get {
				return this.currentSubLevel;
			}
			set {
				currentSubLevel = value;
			}
		}
		
		[XmlElement("BalloonsExecutionResults")]
		public List<ExerciceExecutionResult<SingleResultBalloons>> BalloonsExecutionResults {
			get {return this.balloonsResults;}
			set {balloonsResults = value;}
		}		
		
		[XmlIgnore]		
		public ExerciceExecutionResult<SingleResultBalloons> CurrentExecution {		
			get { 
				if ( balloonsResults.Count > 0 ) {
					return balloonsResults[balloonsResults.Count - 1];
				} else {
					return null;	
				}
			}
			set {
				balloonsResults[balloonsResults.Count - 1] = value;	
			}			
		}
		
		public BalloonsResults ()
		{
			BalloonsExecutionResults = new List<ExerciceExecutionResult<SingleResultBalloons>>();
		}
		
		public void setResultado(string showedSerie, int balloonsNumber,int balloonsGoal, int corrects,
								int fails, int omissions, int level, int subLevel, int timeElapsed)
		{
			SingleResultBalloons re = new SingleResultBalloons(showedSerie, balloonsNumber, balloonsGoal, corrects,
		                                  fails, omissions, level, subLevel, timeElapsed);
			BalloonsExecutionResults[BalloonsExecutionResults.Count-1].SingleResults.Add(re);
        }		
		
		public void ResetExecution() {
			
			this.CurrentExecution.TotalCorrects = 0;
			this.CurrentExecution.TotalFails = 0;
			this.CurrentExecution.TotalOmissions = 0;
			this.CurrentExecution.TotalTimeElapsed = 0;

		}
		
		public override string ToString ()
		{
			return null;
		}

		 
	}
}

