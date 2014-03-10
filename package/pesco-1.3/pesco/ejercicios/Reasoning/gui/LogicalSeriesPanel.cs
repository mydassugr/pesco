using System;
using System.Collections.Generic;
using System.Reflection;
using Gtk;


namespace pesco
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class LogicalSeriesPanel : ReasoningPanel
	{
	
		string explanationText;
		private string DirImg =Configuration.ProgramDir()+System.IO.Path.DirectorySeparatorChar+"ejercicios"+System.IO.Path.DirectorySeparatorChar+"Reasoning"+System.IO.Path.DirectorySeparatorChar+"LogicalSeries"+
			System.IO.Path.DirectorySeparatorChar+"figures"+System.IO.Path.DirectorySeparatorChar;
		
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
		
		public  override void HideExplanation(){
			explanation.Markup = "";
		}
		
		public override  void ShowExplanation(){
		    explanation.Markup = "<span color=\"blue\">"+"<b>Pista:</b> " + explanationText+"</span>";
            imagepepe.PixbufAnimation= new Gdk.PixbufAnimation (Configuration.CommandDirectory+"/ejercicios/resources/img/reasoning/pepehabla.gif");
		}
		
		public override  void ShowCorrectExplanation(bool correct){
			if(!correct)
				explanation.Markup = "<span color=\"blue\">"+"<b>Explicación:</b> " + explanationText+"</span>";
			else
		   		explanation.Markup = "<span color='blue'><b>¡Muy bien!</b>" + explanationText+"</span>";
            imagepepe.PixbufAnimation= new Gdk.PixbufAnimation (Configuration.CommandDirectory+"/ejercicios/resources/img/reasoning/pepehabla.gif");
		}
		
		public override  Button HelpButton{
			get{
				return helpButton;	
			}
		}
		
		public LogicalSeriesPanel ()
		{
			this.Build ();
        	GtkUtil.SetStyle(this, Configuration.Current.MediumFont);
			GtkUtil.SetStyle(botonListo, Configuration.Current.ButtonFont);
			GtkUtil.SetStyle(label1, Configuration.Current.SmallFont);
			
			this.optionsContainer =cajaOpciones;
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
		public void Populate (List<LogicalSeriesElement> samples, List<LogicalSeriesElement> options, int position)
		{
			// remove the old figures, making room for the new ones!
			foreach (Widget w in this.cajaMuestras)
				cajaMuestras.Remove (w);
					
			
			// remove the old buttons, making room for the new ones!
			foreach (Widget w in this.cajaOpciones)
				cajaOpciones.Remove (w);
			
			uint fil = 0;
			uint col = 0;
			
			uint i=0;
			
			uint gap = samples.Count >= 8 ? (uint)3 : (uint)8;
			
			
		//	cajaMuestras.Homogeneous = true;
			if (samples.Count >=8) {
				cajaMuestras.NRows = 3;
				cajaMuestras.NColumns = 3;
			}
			else {
				cajaMuestras.NRows = 1;
				cajaMuestras.NColumns = (uint)samples.Count;
			}
			
			// adding the samples
			foreach (LogicalSeriesElement lse in samples) {
				if (i == position){
					
					Frame fr = new Frame();
					fr.ShadowType = ShadowType.In;
					
					fr.Add(new Gtk.Image(Gdk.Pixbuf.LoadFromResource("pesco.ejercicios.Reasoning.LogicalSeries.figures.placeholder.png")));
					cajaMuestras.Attach(fr, i%gap, i%gap+1, fil, fil+1,AttachOptions.Expand,AttachOptions.Expand,0,0); 
					i++;
				}
				
				
					Frame f = new Frame();
					f.ShadowType = ShadowType.In;
					
					string imgName= DirImg+ lse.Value.Replace("_BAR_",System.IO.Path.DirectorySeparatorChar.ToString());
					f.Add(new Gtk.Image(imgName/*lse.GetWidget()*/));
					this.cajaMuestras.Attach(f, i%gap, i%gap+1, fil,fil+1,AttachOptions.Expand,AttachOptions.Expand,0,0);
				
				i++;
				if (i % gap == 0){
					fil++;
				}
			}
			
			if (i == position){
				Frame fr = new Frame();
				fr.ShadowType = ShadowType.In;
				fr.Add(new Gtk.Image(Gdk.Pixbuf.LoadFromResource("pesco.ejercicios.Reasoning.LogicalSeries.figures.placeholder.png")));
				cajaMuestras.Attach(fr, i%gap, i%gap+1, fil, fil+1,AttachOptions.Expand,AttachOptions.Expand,0,0); 
			}
			
			// add de options
			ReasoningExerciseToggleButton tb;
			
			foreach (LogicalSeriesElement lse in options) {
				string imgName= DirImg+lse.Value.Replace("_BAR_",System.IO.Path.DirectorySeparatorChar.ToString());
				tb = new ReasoningExerciseToggleButton (imgName, imgName, lse.Position);
				
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
		public List<LogicalSeriesElement> GetSelectedItems ()
		{
			List<LogicalSeriesElement> sses = new List<LogicalSeriesElement> ();
			
			foreach (ReasoningExerciseToggleButton w in cajaOpciones)
				if (w.Active)
					sses.Add (new LogicalSeriesElement (w.Key, w.Position));
			
			return sses;
		}
		
		protected virtual void OnHelpButtonClicked (object sender, System.EventArgs e)
		{
			ShowExplanation();
		}
		
		public override void MoveSolution(string key){
			Gtk.Image sol= ((Gtk.Image)((Gtk.Frame)cajaMuestras.Children[cajaMuestras.Children.GetLength(0)-1]).Children[0]);
			//((Gtk.Frame)cajaMuestras.Children[cajaMuestras.Children.GetLength(0)-1]).Children[0].Destroy();
			
			foreach (ReasoningExerciseToggleButton w in cajaOpciones){
				if(w.Position.ToString()==key){ 
					
					//((Gtk.Frame)cajaMuestras.Children[0]).ModifyFg(StateType.Normal,  new Gdk.Color (0x77, 0xff, 0x77));
					((Gtk.Frame)cajaMuestras.Children[0]).ModifyBg(StateType.Normal,  new Gdk.Color (0x77, 0xff, 0x77));
					
					string imgName= w.Key;
					((Gtk.Image)((Gtk.Frame)cajaMuestras.Children[0]).Child).Pixbuf= new Gdk.Pixbuf(imgName).Copy();
						
				}
			}
			this.ShowAll();
		}
		
		public void ShowLogicalSeriesCorrectAnswers<T>(List<T> correctElements, List<T> wrongElements) where T:ReasoningExerciseElement
		{			
			
			foreach ( ReasoningExerciseToggleButton w in optionsContainer)
			{
				w.IsSolution=true;
				foreach(T e in wrongElements)
				{
					
					if(e.Position.Equals(w.Position))
						w.SetAsWrong();	
				}				
				
				foreach(T e in correctElements)
				{
					if(e.Position.Equals(w.Position))
					{
						w.SetAsCorrect();
						this.MoveSolution(w.Position.ToString());
					}
				}
			}
			
			
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

