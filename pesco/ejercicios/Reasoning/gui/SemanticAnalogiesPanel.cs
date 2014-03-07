using System;
using System.Collections.Generic;
using Gtk;


namespace pesco
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class SemanticAnalogiesPanel : ReasoningPanel
	{
		#region Help
		string explanationText;	
		
		public string ExplanationText{
			set {
				explanationText = value;
			}
			get {
				return explanationText;
			}
		}
		
		string solutionExplanationText;
		
		public string SolutionExplanationText {
			get {
				return this.solutionExplanationText;
			}
			set {
				solutionExplanationText = value;
			}
		}
		
		public override void HideHelpButton(){
			int actualSession= SessionManager.GetInstance().CurSession.IdSession;
			if(actualSession == 1 || actualSession == 2 || actualSession == 12)
				helpButton.Visible=false;
		}
		
		
		public override  void HideExplanation(){
			explanation.Markup = "";
		}
		
		public override  void ShowExplanation(){
            explanation.Markup = "<span color=\"blue\">"+"<b>Pista:</b> " + explanationText+"</span>";
            imagepepe.PixbufAnimation= new Gdk.PixbufAnimation (Configuration.CommandDirectory+"/ejercicios/resources/img/reasoning/pepehabla.gif");
		}
		
		public override  void ShowCorrectExplanation(bool correct){
			
			List<SemanticSeriesElement> sses = GetSelectedItems();
			
			
			if(!correct)
				explanation.Markup = "<span color=\"blue\">"+"<b>Explicación:</b> " + solutionExplanationText+ "</span>";
			else
		    	explanation.Markup = "<span color=\"blue\">"+"<b>¡Muy bien!</b> " + solutionExplanationText+ "</span>";
			
            imagepepe.PixbufAnimation= new Gdk.PixbufAnimation (Configuration.CommandDirectory+"/ejercicios/resources/img/reasoning/pepehabla.gif");
		}
		
		public  override Button HelpButton{
			get{
				return helpButton;	
			}
			
		}	
		#endregion
		
		public SemanticAnalogiesPanel ()
		{
			this.Build ();
			int actualSession= SessionManager.GetInstance().CurSession.IdSession;
			GtkUtil.SetStyle(this, Configuration.Current.MediumFont);
			GtkUtil.SetStyle(botonListo, Configuration.Current.ButtonFont);
			
			this.optionsContainer = cajaOpciones;
			
			if(actualSession == 1 || actualSession == 2 || actualSession == 12)
				helpButton.Visible=false;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="samples">
		/// A <see cref="List<LogicalSeriesElement>"/>
		/// </param>
		/// <param name="options">
		/// A <see cref="List<LogicalSeriesElement>"/>
		/// </param>
		public void Populate (List<SemanticSeriesElement> samples, List<SemanticSeriesElement> options)
		{
			// remove the old labels, making room for the new ones!
			foreach (Widget w in this.cajaMuestras)
				cajaMuestras.Remove (w);
			
			// remove the old buttons, making room for the new ones!
			foreach (Widget w in this.cajaOpciones)
			{
				cajaOpciones.Remove (w);
			}
			
			// adding the samples
			foreach (SemanticSeriesElement lse in samples) {
				this.cajaMuestras.Add (lse.GetWidget ());
				
			}
			GtkUtil.SetStyle(cajaMuestras, Configuration.Current.LargeFont);
			// add de options
			ReasoningExerciseToggleButton tb;
			
			foreach (SemanticSeriesElement lse in options) {
				tb = new ReasoningExerciseToggleButton (lse.Value, lse.Position);
				
				tb.Clicked += delegate(object sender, EventArgs e) {
					
					// if the sender is a ReasoningExerciseToggleButtons, as it may be...
					if ((sender as ReasoningExerciseToggleButton).Active) {
						
						// unselect all the buttons but the last selected
						foreach (ReasoningExerciseToggleButton w in cajaOpciones)
							if (!w.Equals (sender))
								w.Active = false;
					}
				};
				this.cajaOpciones.Add (tb);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns>
		/// A <see cref="List<LogicalSeriesElement>"/>
		/// </returns>
		public List<SemanticSeriesElement> GetSelectedItems ()
		{
			List<SemanticSeriesElement> sses = new List<SemanticSeriesElement> ();
			
			foreach (ReasoningExerciseToggleButton w in cajaOpciones)
				if (w.Active)
					sses.Add (new SemanticSeriesElement (w.Key, w.Position));
			
			return sses;
		}
		
		public override void MoveSolution(string key){
			string mtext=((Gtk.Label)this.cajaMuestras.Children[0]).Text;
			((Gtk.Label)this.cajaMuestras.Children[0]).Markup=mtext +"<span color=\"green\"><big><b>"+key+"</b></big></span>";
			
		}
		
		public override bool SolutionSelected(){
			foreach (ReasoningExerciseToggleButton reb  in cajaOpciones.Children){
				if(reb.Active==true)
					return true;
			}
			
			return false;
		}
			
		
		/// <summary>
		/// 
		/// </summary>
		public override Button ReadyButton{
			get{ return this.botonListo;}	
		}
	}
}

