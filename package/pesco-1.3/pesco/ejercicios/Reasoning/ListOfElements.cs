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
	public class ListOfElements<T> where T:ReasoningExerciseElement
	{
		/// <summary>
		/// 
		/// </summary>
		protected ReasoningExerciseDifficulty difficulty;
		
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
		
		/// <summary>
		/// 
		/// </summary>
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
		/// List of the elements to be presented to the user
		/// </summary>
		protected List<T> elements ;
		
		/// <summary>
		/// List of the elements belonging to the correct answer
		/// </summary>
		protected List<T> wrongElements;
		
		[XmlElement("elements")]
		public List<T> Elements {
			get {
				return this.elements;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[XmlElement("answers")]
		public List<T> WrongElements {
			get {
				return this.wrongElements;
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		public ListOfElements ()
		{
			this.elements = new System.Collections.Generic.List<T>();
			this.wrongElements = new System.Collections.Generic.List<T>();
		}
		
		/// <summary>
		/// 
		/// </summary>
		public void Clear()
		{
			elements.Clear();
			wrongElements.Clear();
		}
	}
}


