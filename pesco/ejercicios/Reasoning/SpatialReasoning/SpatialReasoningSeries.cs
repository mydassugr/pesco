using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace pesco
{
	public class SpatialReasoningSeries
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
		
		protected SpatialReasoningElement original;
		
		[XmlElement("original")]
		public SpatialReasoningElement Original {
			get {
				return this.original;
			}
			set {
				original = value;
			}
		}

		[XmlElement("distractor")]
		protected SpatialReasoningElement distractor;
		
		public SpatialReasoningElement Distractor {
			get {
				return this.distractor;
			}
			set {
				distractor = value;
			}
		}

		
		public SpatialReasoningSeries ()
		{
		}
		

	}
}

