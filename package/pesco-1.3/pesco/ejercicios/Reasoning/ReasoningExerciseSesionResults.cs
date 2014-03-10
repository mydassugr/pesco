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
	public class SingleResultSpatialReasoningExercice: SingleResultElement
	{
		int questionId;
		int corrects; 
		int fails;
		string result; // (Valid, Semivalid, Fail)
		string level;
		int timeElapsed;
	

		[XmlElement("QuestionId")]
		public int QuestionId {
			get {return this.questionId;}
			set {questionId = value;}
		}
		
		[XmlElement("Corrects")]
		public int Corrects {
			get {return this.corrects;}
			set {corrects = value;}
		}
		
		[XmlElement("Fails")]
		public int Fails {
			get {return this.fails;}
			set {fails = value;}
		}
		
		
		[XmlElement("Result")]
		public string Result {
			get {return this.result;}
			set {result = value;}
		}
		
		[XmlElement("Level")]
		public string Level {
			get {return this.level;}
			set {level = value;}
		}

		[XmlElement("TimeElapsed")]
		public int TimeElapsed {
			get {return this.timeElapsed;}
			set {timeElapsed = value;}
		}
			
		public SingleResultSpatialReasoningExercice(){}
		
		public SingleResultSpatialReasoningExercice(int questionIdArg, int correctsArg, int failsArg, string resultArg, string levelArg, int timeElapsedArg){
			QuestionId = questionIdArg;
			Corrects= correctsArg;
			Fails= failsArg;
			Result= resultArg;
			Level= levelArg;
			TimeElapsed = timeElapsedArg;
		}
	}
	public class SingleResultReasoningExercice: SingleResultElement
	{
		int questionId;
		int answerIdSelected; //posicion
		bool helpUsed;
		string result; //valid fail semivalid
		string level; //low, medium, high
		int timeElapsed;
	
		
		[XmlElement("QuestionId")]
		public int QuestionId {
			get {return this.questionId;}
			set {questionId = value;}
		}	
		
		[XmlElement("AnswerIdSelected")]
		public int AnswerIdSelected {
			get {return this.answerIdSelected;}
			set {answerIdSelected = value;}
		}

		[XmlElement("HelpUsed")]
		public bool  HelpUsed {
			get {return this.helpUsed;}
			set {helpUsed = value;}
		}

		[XmlElement("Result")]
		public string Result {
			get {return this.result;}
			set {result = value;}
		}

		[XmlElement("Level")]
		public string Level {
			get {return this.level;}
			set {level = value;}
		}
		
		[XmlElement("TimeElapsed")]
		public int TimeElapsed {
			get {return this.timeElapsed;}
			set {timeElapsed = value;}
		}
		
		public SingleResultReasoningExercice (){}
		public SingleResultReasoningExercice (int questionIdArg, int answerIdSelectedArg, bool helpUsedArg, string resultArg, string levelArg, int timeElapsedArg){
			QuestionId = questionIdArg;
			AnswerIdSelected = answerIdSelectedArg;
			HelpUsed = helpUsedArg;
			Result= resultArg;
			Level = levelArg;
			TimeElapsed = timeElapsedArg;
			
		}
}
	/// <summary>
	/// 
	/// </summary>
	/// <summary>
	/// 
	/// </summary>
	public class ReasoningExerciseSesionResults: Results
	{
/*		/// <summary>
		/// 
		/// </summary>
		List<ReasoningExerciseResult> results;
		
		/// <summary>
		/// 
		/// </summary>
		DateTime sesionDateTime;
		
		int actualSessionId;
		string actualDifficulty;
		int totalCorrects;
		int totalFails;
		
		/// <summary>
		/// 
		/// </summary>
		[XmlElement("date-time")]
		public DateTime SesionDateTime {
			get {return this.sesionDateTime;}
			set {sesionDateTime = value;}
		}

		[XmlElement("session-difficulty")]
		public string ActualDifficulty {
			get {return this.actualDifficulty;}
			set {actualDifficulty = value;}
		}

		[XmlElement("actual-Session-ID")]
		public int ActualSessionId {
			get {return this.actualSessionId;}
			set {actualSessionId = value;}
		}

		[XmlElement("totalCorrects")]
		public int TotalCorrects {
			get {return this.totalCorrects;}
			set {totalCorrects = value;}
		}
		
		[XmlElement("totalFails")]
		public int TotalFails {
			get {return this.totalFails;}
			set {totalFails = value;}
		}
		
		/// <summary>
		/// 
		/// </summary>
		[XmlElement("results")]
		public List<ReasoningExerciseResult> Results {
			get {
				return this.results;
			}
		}

		

		/// <summary>
		/// 
		/// </summary>
		public ReasoningExerciseSesionResults ()
		{
			this.results = new List<ReasoningExerciseResult>();
		}
	
		*/	
		
		
		List <ExerciceExecutionResult<SingleResultReasoningExercice>> reasoningExerciceExecutionResults	= null;
		List <ExerciceExecutionResult<SingleResultSpatialReasoningExercice>> spatialReasoningExerciceExecutionResults = null	;

			
		[XmlElement("ReasoningExerciceExecutionResults")]
		public List<ExerciceExecutionResult<SingleResultReasoningExercice>> ReasoningExerciceExecutionResults {
			get {return this.reasoningExerciceExecutionResults;}
			set {reasoningExerciceExecutionResults = value;}
		}
		
		[XmlElement("SpatialReasoningExerciceExecutionResults")]
		public List<ExerciceExecutionResult<SingleResultSpatialReasoningExercice>> SpatialReasoningExerciceExecutionResults {
			get {return this.spatialReasoningExerciceExecutionResults;}
			set {spatialReasoningExerciceExecutionResults = value;}
		}
		
		public void setReasoningResult(int questionIdArg, int answerIdSelectedArg, bool helpUsedArg, string resultArg, string levelArg, int timeElapsedArg)
		{

			SingleResultReasoningExercice re = new SingleResultReasoningExercice(questionIdArg, answerIdSelectedArg, helpUsedArg, resultArg, levelArg, timeElapsedArg);
			reasoningExerciceExecutionResults[reasoningExerciceExecutionResults.Count -1].SingleResults.Add(re);
			
        }
		public void setSpatialReasoningResult(int questionIdArg, int correctsArg, int failsArg, string resultArg, string levelArg, int timeElapsedArg)
		{
			SingleResultSpatialReasoningExercice re = new SingleResultSpatialReasoningExercice( questionIdArg, correctsArg, failsArg, resultArg, levelArg, timeElapsedArg);
			spatialReasoningExerciceExecutionResults[spatialReasoningExerciceExecutionResults.Count -1].SingleResults.Add(re);
			
        }
		
		public ReasoningExerciseSesionResults ()
		{}
		
			
		public ReasoningExerciseSesionResults (bool spatialReasoning)
		{
			if(!spatialReasoning)
				reasoningExerciceExecutionResults = new List<ExerciceExecutionResult<SingleResultReasoningExercice>>();
			else
				spatialReasoningExerciceExecutionResults = new List<ExerciceExecutionResult<SingleResultSpatialReasoningExercice>>();
		}
		
		
		
		
		public void Start()
		{
			//this.sesionDateTime = DateTime.Now;
		}
		public void Start(int idSessionArg, string difficultyArg)
		{
			/*this.sesionDateTime = DateTime.Now;
			this.actualDifficulty = difficultyArg;
			this.actualSessionId= idSessionArg;
			this.totalFails=0;
			this.totalCorrects=0;*/
		}
		
		/*/// <summary>
		/// 
		/// </summary>
		/// <param name="r">
		/// A <see cref="ReasoningExerciseResult"/>
		/// </param>
		public void Add(ReasoningExerciseResult r)
		{
			if(r.Aciertos == 1)
				totalCorrects ++;
			else if(r.Errores ==1)
				totalFails ++;
			
			this.results.Add(r);
		}*/
	}
}


