using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace pesco
{
	public class ClassifyExerciseElement : pesco.ReasoningExerciseElement
	{
		private string resourceName;
		private int positionId;
		
		[XmlElement("PositionId")]
		public int PositionId {
			get {return this.positionId;}
			
		}		

		[XmlElement("resource-name")]
		public string ResourceName {
			get {
				return this.resourceName;
			}
		}
		
		
		#region implemented abstract members of pesco.ReasoningExerciseElement
		public override Gtk.Widget GetWidget ()
		{
			return new Gtk.Image(Gdk.Pixbuf.LoadFromResource(resourceName));
		}
		
		#endregion
		
		public ClassifyExerciseElement () : base(){}
		
		public ClassifyExerciseElement (string value, int pos) : base()
		{
			this.resourceName = value;
			this.positionId = pos;
		}
	

		public override bool Equals (object obj)
		{
			if (obj.GetType().Equals(typeof(ClassifyExerciseElement)))
				return this.ResourceName == (obj as ClassifyExerciseElement).resourceName;
			
			else return false;
		}
		#region implemented abstract members of pesco.ReasoningExerciseElement
		public override string Value {
			get {
				return this.resourceName;
			}
			set {
				this.resourceName = value;	
			}
		}
		
		public override int Position {
			get {return this.positionId;}
			set {this.positionId = value;	}
		}
		
		#endregion
	}
}

