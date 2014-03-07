using System;
namespace pesco
{
	public class SpatialReasoningElement : ReasoningExerciseElement
	{
		
		protected string resourceName;
		private int positionId;
		
				
		public string ValueOfElement {
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
		
		public SpatialReasoningElement (string rn, int pos) : base()
		{
			this.resourceName = rn;
			this.positionId = pos;
		}
		
		public SpatialReasoningElement () : base()
		{
		}
		
		public override bool Equals (object obj)
		{
			if (obj.GetType().Equals(typeof(SpatialReasoningElement)))
				return this.ValueOfElement == (obj as SpatialReasoningElement).ValueOfElement;
			
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

