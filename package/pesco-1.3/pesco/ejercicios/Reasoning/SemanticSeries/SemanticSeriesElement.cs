using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;


namespace pesco
{
	
	
	
	public class SemanticSeriesElement : pesco.ReasoningExerciseElement
	{
		
		private string word;
		private int positionId;
		
		[XmlElement("PositionId")]
		public int PositionId {
			get {return this.positionId;}
		}
		
		[XmlElement("word")]
		public string Word {
			get {
				return this.word;
			}
		}
		
		#region implemented abstract members of pesco.ReasoningExerciseElement
		public override Gtk.Widget GetWidget ()
		{
			return new Gtk.Label(word);
		}
		
		#endregion
		
		public SemanticSeriesElement () : base(){}
		
		public SemanticSeriesElement (string value, int pos) : base()
		{
			this.word = value;
			this.positionId =pos;
		}
	

		public override bool Equals (object obj)
		{
			if (obj.GetType().Equals(typeof(SemanticSeriesElement)))
				return this.Word == (obj as SemanticSeriesElement).Word;
			
			else return false;
		}
		#region implemented abstract members of pesco.ReasoningExerciseElement
		public override string Value {
			get {
				return this.word;
			}
			set {
				this.word = value;	
			}
		}
		public override int Position {
			get {
				return this.positionId;
			}
			set {
				this.positionId = value;	
			}
		}
		
		#endregion
	}
}

