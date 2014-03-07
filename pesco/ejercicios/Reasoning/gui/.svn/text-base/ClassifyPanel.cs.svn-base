using System;
using Gtk;
using Gdk;
using System.Collections.Generic;

namespace pesco
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class ClassifyPanel : ReasoningPanel
	{
	
		#region Help
		string explanationText;
		
		private string DirImg =Configuration.ProgramDir()+System.IO.Path.DirectorySeparatorChar+"ejercicios"+System.IO.Path.DirectorySeparatorChar+"Reasoning"+System.IO.Path.DirectorySeparatorChar+"Classify"+
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
			if(actualSession == 1 || actualSession == 2 || actualSession == 12)
				helpButton.Visible=false;
		}
		
		public override  void HideExplanation(){
			explanation.Markup = "";
            table1.Visible=false;
         	}
	   
		public override  void ShowExplanation(){
            explanation.Markup = "<span color=\"blue\">"+"<b>Pista:</b> " + explanationText+"</span>";
		    imagepepe.PixbufAnimation= new Gdk.PixbufAnimation (Configuration.CommandDirectory+"/ejercicios/resources/img/reasoning/pepehabla.gif");
          
		}
        
		
		public override  void ShowCorrectExplanation(bool correct){
        
			if(!correct){
				explanation.Markup = "<span color=\"blue\">"+"<b>Explicación:</b> " + explanationText+"</span>";
         		// imagehelp.PixbufAnimation= new Gdk.PixbufAnimation (Configuration.CommandDirectory+"/ejercicios/resources/img/bombillamovil.gif");
			}
			else{
		    	explanation.Markup = "<span color=\"blue\">"+"<b>¡Muy bien!</b> " + explanationText+"</span>";
         	//	imagehelp.PixbufAnimation= new Gdk.PixbufAnimation (Configuration.CommandDirectory+"/ejercicios/resources/img/ayudamovil.gif");
			}
         
            imagepepe.PixbufAnimation= new Gdk.PixbufAnimation (Configuration.CommandDirectory+"/ejercicios/resources/img/reasoning/pepehabla.gif");
		}
		
		public override  Button HelpButton{
			get{
				return helpButton;	
			}
		}
		
		#endregion
		
		public ClassifyPanel ()
		{
			this.Build ();
			int actualSession= SessionManager.GetInstance().CurSession.IdSession;
			
			explanation.Hide();
			GtkUtil.SetStyle(this, Configuration.Current.MediumFont);
			optionsContainer = hbox1;
            GtkUtil.SetStyle(this.label1, Configuration.Current.MediumFont); 
			
			if(actualSession == 1 || actualSession == 2 || actualSession == 12)
				helpButton.Visible=false;
			
           
		}

		
		public void Populate (List<ClassifyExerciseElement> elements)
		{
            table1.Visible=false;
			// remove the old buttons, making room for the new ones
			foreach (Widget w in hbox1)
				hbox1.Remove(w);
			
			ReasoningExerciseToggleButton tb;
			
			// for every element in the series
			foreach (ClassifyExerciseElement sse in elements) {
				string imgName= DirImg+sse.Value.Replace("_BAR_",System.IO.Path.DirectorySeparatorChar.ToString());
				tb = new ReasoningExerciseToggleButton (imgName, imgName, sse.Position);
				
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
		/// A <see cref="List<ClassifyExerciseElement>"/> containing all the selected elements in the panels
		/// </returns>
		public List<ClassifyExerciseElement> GetSelectedItems ()
		{
			List<ClassifyExerciseElement> sses = new List<ClassifyExerciseElement> ();
		
			foreach (ReasoningExerciseToggleButton w in hbox1)		
					if (w.Active)
						sses.Add (new ClassifyExerciseElement (w.Key, w.Position));
			
			return sses;
		}
		
		
		public override void MoveSolution(string key){
			
			if(table1.Children.GetLength(0)>0){
				table1.Children[0].Destroy();
			}
				
			foreach (ReasoningExerciseToggleButton w in hbox1)					{
				if (w.Position.ToString()==key){
					
					Frame frame1= new Frame();
					frame1.ShadowType= ShadowType.In;
					string imgName= w.Key;
					Gtk.Image SolutionImage = new Gtk.Image(imgName);
					frame1.Add( SolutionImage);
					frame1.ModifyBg(StateType.Normal,  new Gdk.Color (0x77, 0xff, 0x77));
					table1.Attach(frame1,0,1,0,1,AttachOptions.Expand,0,0,0);
					table1.Visible=true;
						
				}
			}
			table1.ShowAll();
		}
		
		public void ShowClassifyCorrectAnswers<T>(List<T> correctElements, List<T> wrongElements) where T:ReasoningExerciseElement
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
		
		/*
		
		public bool SeleccionarTarea(string tarea)
		{
			bool res = false;
			
			foreach ( Widget w in tablaBotones.Children)
			{
				if (w.GetType().Equals(typeof(ToggleButton)))
				{				
					ToggleButton tb = (ToggleButton) w;
				
					if (tb.Label ==  tarea)
						if (tb.Active)
							return false;
						else 
						{
							tb.IsFocus = true;
							tb.ModifyBg(StateType.Normal, new Gdk.Color(0xff, 0xff,0x77));
							return true;
						}
				}
			}
			
			return res;
		} 
		*/
		
	}
}

