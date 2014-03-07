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
using System.Collections.Generic;
using System.IO;

namespace pesco
{
/*	public class SingleResultSpatialReasoningExercice: SingleResultElement
	{
		int questionId;
		int corrects; 
		int fails;
		int result; // (Valid, Semivalid, Fail)
		int level;
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
		public int Result {
			get {return this.result;}
			set {result = value;}
		}
		
		[XmlElement("Level")]
		public int Level {
			get {return this.level;}
			set {level = value;}
		}

		[XmlElement("TimeElapsed")]
		public int TimeElapsed {
			get {return this.timeElapsed;}
			set {timeElapsed = value;}
		}
			
		public SingleResultSpatialReasoningExercice(){}
		
		public SingleResultSpatialReasoningExercice(int questionIdArg, int correctsArg, int failsArg, int resultArg, int levelArg, int timeElapsedArg){
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
		int result; //valid fail semivalid
		int level;
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
		public int Result {
			get {return this.result;}
			set {result = value;}
		}

		[XmlElement("Level")]
		public int Level {
			get {return this.level;}
			set {level = value;}
		}
		
		[XmlElement("TimeElapsed")]
		public int TimeElapsed {
			get {return this.timeElapsed;}
			set {timeElapsed = value;}
		}
		
		public SingleResultReasoningExercice (){}
		public SingleResultReasoningExercice (int questionIdArg, int answerIdSelectedArg, bool helpUsedArg, int resultArg, int levelArg, int timeElapsedArg){
			QuestionId = questionIdArg;
			AnswerIdSelected = answerIdSelectedArg;
			HelpUsed = helpUsedArg;
			Result= resultArg;
			Level = levelArg;
			TimeElapsed = timeElapsedArg;
			
		}
}
*/
	/// <summary>
	/// 
	/// </summary>
	public class ReasoningExerciseResult
	{
		int aciertos;
		int errores;
		int omisiones;
		int id;
		bool ayuda;
		
		[XmlElement("ayuda")]
		public bool Ayuda {
			get {
				return this.ayuda;
			}
			set {
				ayuda = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[XmlElement("id")]
		public int Id {
			get {
				return this.id;
			}
			set {
				id = value;
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		TimeSpan tiempo;

		/// <summary>
		/// 
		/// </summary>
	    [XmlElement("tiempo")]
	    public string XmlDuration
	    {
	        get { return tiempo.ToString(); }
	        set { tiempo = TimeSpan.Parse(value); }
	    }
		
		/// <summary>
		/// 
		/// </summary>
		[XmlIgnore]
		public TimeSpan Tiempo {
			get { return tiempo; }
			set { tiempo = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		[XmlElement("omision")]
		public int Omisiones {
			get { return omisiones; }
			set { omisiones = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		[XmlElement("error")]
		public int Errores {
			get { return errores; }
			set { errores = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		[XmlElement("acierto")]
		public int Aciertos {
			get { return aciertos; }
			set { aciertos = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="a">
		/// A <see cref="System.Int32"/>
		/// </param>
		/// <param name="e">
		/// A <see cref="System.Int32"/>
		/// </param>
		/// <param name="o">
		/// A <see cref="System.Int32"/>
		/// </param>
		/// <param name="t">
		/// A <see cref="TimeSpan"/>
		/// </param>
		public ReasoningExerciseResult (int a, int e, int o, TimeSpan t)
		{
			aciertos = a;
			Errores = e;
			omisiones = o;
			Tiempo = t;
		}
		
		

		public ReasoningExerciseResult ()
		{}
		/*public ReasoningExerciseResult (bool spatialReasoning)
		{
			if(!spatialReasoning)
				reasoningExerciceExecutionResults = new List<ExerciceExecutionResult<SingleResultReasoningExercice>>();
			else
				spatialReasoningExerciceExecutionResults = new List<ExerciceExecutionResult<SingleResultSpatialReasoningExercice>>();
			
		}*/
				
	
		/// <summary>
		/// 
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/>
		/// </returns>
		public override string ToString ()
		{
			return string.Format ("[ReasoningExerciseResult: Tiempo={0}, Omisiones={1}, Errores={2}, Aciertos={3}]", Tiempo, Omisiones, Errores, Aciertos);
		}
}
}


