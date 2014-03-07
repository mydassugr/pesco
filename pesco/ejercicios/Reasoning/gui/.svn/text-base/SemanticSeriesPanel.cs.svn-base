using System;
using Gtk;
using System.Collections.Generic;


namespace pesco
{

	[System.ComponentModel.ToolboxItem(true)]
	public partial class SemanticSeriesPanel : ReasoningPanel
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
		public override void HideHelpButton(){
			int actualSession= SessionManager.GetInstance().CurSession.IdSession;
			if(actualSession == 1 || actualSession == 11)
				helpButton.Visible=false;
		}
		
		public override  void HideExplanation(){
			explanation.Markup = "";
			labelSolution.Visible=false;
		}
		
		public override  void ShowExplanation(){
			explanation.Markup = "<span color=\"blue\">"+"<b>Pista:</b> " + explanationText+"</span>";
            imagepepe.PixbufAnimation= new Gdk.PixbufAnimation (Configuration.CommandDirectory+"/ejercicios/resources/img/reasoning/pepehabla.gif");           		
		}
		
		public override  void ShowCorrectExplanation(bool correct){
			
			if(!correct)
				explanation.Markup = "<span color=\"blue\">"+"<b>Explicación:</b> " + explanationText+"</span>";
			else
		    	explanation.Markup = "<span color=\"blue\">"+"<b>!Muy bien!</b> " + explanationText+"</span>";
            imagepepe.PixbufAnimation= new Gdk.PixbufAnimation (Configuration.CommandDirectory+"/ejercicios/resources/img/reasoning/pepehabla.gif");
		}
		
		public override  Button HelpButton{
			get{
				return helpButton;	
			}
		}
		
		#endregion
		
		
		
		public SemanticSeriesPanel ()
		{
			this.Build ();
			
         
			GtkUtil.SetStyle(this, Configuration.Current.MediumFont);
			GtkUtil.SetStyle(label1, Configuration.Current.SmallFont);
			this.optionsContainer = this.hbox1;
			
		}

		public void Populate (List<SemanticSeriesElement> elements)
		{
			
			labelSolution.Visible=false;
			// remove the old buttons, making room for the new ones
			foreach (Widget w in hbox1)
				hbox1.Remove(w);
			
			ReasoningExerciseToggleButton tb;
			
			// for every element in the series
			foreach (SemanticSeriesElement sse in elements) {
				tb = new ReasoningExerciseToggleButton (sse.Word, sse.Position);
				
				hbox1.Add (tb);
				
				tb.Clicked += delegate(object sender, EventArgs e) {
					
					// if the sender is a ReasoningExerciseToggleButtons, as it may be...
					if ((sender as ReasoningExerciseToggleButton).Active) {
						
						// unselect all the buttons but the last selected
						foreach (ReasoningExerciseToggleButton w in hbox1)
							if (!w.Equals(sender)) w.Active = false;						
					}
				};
			}
		}



		/// <summary>
		/// Returs all the selected items
		/// </summary>
		/// <returns>
		/// A <see cref="List<SemanticSeriesElement>"/> containing all the selected elements in the panels
		/// </returns>
		public List<SemanticSeriesElement> GetSelectedItems ()
		{
			List<SemanticSeriesElement> sses = new List<SemanticSeriesElement> ();
		
			foreach (ReasoningExerciseToggleButton w in hbox1)					
					if (w.Active)
						sses.Add (new SemanticSeriesElement (w.Key, w.Position));
			
			return sses;
		}
		
		/*protected void OnClickHelp (object sender, System.EventArgs e)
		{
			ShowExplanation();
		}*/
		
		public override void MoveSolution(string key){
			GtkUtil.SetStyle(labelSolution, Configuration.Current.LargeFont);
			labelSolution.Markup="<span color=\"green\"><big><b>"+key+"</b></big></span>"; 
			labelSolution.Visible=true;
		}
		
		public override bool SolutionSelected(){
			foreach (ReasoningExerciseToggleButton reb  in hbox1.Children){
				if(reb.Active==true)
					return true;
			}
			
			return false;
		}
			
		public override Button ReadyButton
		{
			get {return this.readyButton;}	
		}
	}
}

