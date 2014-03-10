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
	/// <summary>
	/// 
	/// </summary>
	public class Series <T>  where T:ReasoningExerciseElement
	{
		
		string explanation = "insert here explanation";
		
		/// <summary>
		/// 
		/// </summary>
		[XmlElement("explanation")]
		public string Explanation {
			get {
				return this.explanation;
			}
			set {
				explanation = value;
			}
		}
		
		string solutionExplanation = "insert here explanation";
		
		[XmlElement("solutionExplanation")]
		public string SolutionExplanation {
			get {
				return this.solutionExplanation;
			}
			set {
				solutionExplanation = value;
			}
		}

		
		/// <summary>
		/// 
		/// </summary>
		protected ReasoningExerciseDifficulty difficulty;
		
		/// <summary>
		/// 
		/// </summary>
		[XmlElement("difficulty")]
		public ReasoningExerciseDifficulty Difficulty {
			get {
				return this.difficulty;
			}
			set {
				difficulty = value;
			}
		}
		
		int id;
		
		/// <summary>
		/// 
		/// </summary>
		[XmlElement("ID")]
		public  int ID{
			get{return id;}
			set{id =value;}	
		}
		
		/// <summary>
		/// List of the samples to be presented to the user
		/// </summary>
		protected List<T> samples;
		
		/// <summary>
		/// List of options 
		/// </summary>
		protected List<T> options;
		
		/// <summary>
		/// List of elements that are part of the correct answer
		/// </summary>
		protected List<T> correctElements;
		
		/// <summary>
		/// 
		/// </summary>
		protected int position;
		
		/// <summary>
		/// 
		/// </summary>
		[XmlElement("position")]
		public int Position {
			get {
				return this.position;
			}
			set {
				if (value >= 0)
					position = value;
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		public Series ()
		{
			samples  = new List<T>();
			options = new List<T>();
			correctElements = new List<T>();
		}
		
		/// <summary>
		/// 
		/// </summary>
		[XmlElement("correct-elements")]
		public List<T> CorrectElements {
			get {
				return this.correctElements;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[XmlElement("options")]
		public List<T> Options {
			get {
				return this.options;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[XmlElement("samples")]
		public List<T> Samples {
			get {
				return this.samples;
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		public void Clear()
		{
			this.correctElements.Clear ();
			this.samples.Clear ();
			this.options.Clear ();	
		}
	}
}


