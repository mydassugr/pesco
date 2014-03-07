using System;
using Gtk;
using System.Collections.Generic;

namespace pesco
{
	public abstract partial class ReasoningPanel : Gtk.Bin
	{

		public abstract void HideExplanation();
		public abstract void HideHelpButton();
		
		public abstract void ShowExplanation();
		public abstract  void ShowCorrectExplanation(bool correct);
		public abstract void MoveSolution(string key);
		public abstract bool SolutionSelected();
		
		public abstract  Gtk.Button HelpButton{
			get;
		}
		public abstract Gtk.Button ReadyButton{
			get;	
		}
		
		protected Gtk.HBox optionsContainer;
		
		
		public void ShowCorrectAnswers<T>(List<T> correctElements, List<T> wrongElements) where T:ReasoningExerciseElement
		{			
			foreach ( ReasoningExerciseToggleButton w in optionsContainer)
			{
				w.IsSolution=true;
				foreach(T e in wrongElements)
				{
					
					if(e.Value.Equals(w.Key))
						w.SetAsWrong();	
				}				
				
				foreach(T e in correctElements)
				{
					if(e.Value.Equals(w.Key))
					{
						w.SetAsCorrect();
						this.MoveSolution(w.Key);
					}
				}
			}
		}
	}
}

