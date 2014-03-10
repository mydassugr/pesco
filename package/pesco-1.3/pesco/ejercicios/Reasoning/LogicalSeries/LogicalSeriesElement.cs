using System;
using Gtk;
using Gdk;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace pesco
{
	public class LogicalSeriesElement : pesco.ReasoningExerciseElement
	{
		protected string resourceName;
		private int positionId;
		
		[XmlElement("PositionId")]
		public int PositionId {
			get {return this.positionId;}
		}	
		
		public string ValueOfElement {
			get {
				return this.resourceName;
			}
		}
		
		#region implemented abstract members of pesco.ReasoningExerciseElement
		public override Gtk.Widget GetWidget ()
		{   
			Console.WriteLine("Trying: "+resourceName);
			return new Gtk.Image(Pixbuf.LoadFromResource(resourceName));
		}
		
		#endregion
		
		public LogicalSeriesElement (string rn, int pos)
		{
			this.resourceName = rn;
			this.positionId =pos;
		}
		
		public LogicalSeriesElement () : base()
		{
		}
		
		public override bool Equals (object obj)
		{
			if (obj.GetType().Equals(typeof(LogicalSeriesElement)))
				return this.ValueOfElement == (obj as LogicalSeriesElement).ValueOfElement;
			
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
		public override int  Position {
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

