using System;
using Gtk;
using System.Collections.Generic;
using Gdk;

namespace ecng
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class TaskListPanel : Gtk.Bin
	{
		/// <summary>
		/// 
		/// </summary>
		Timer t;

		int row = 0;
		int column = 0;

		int timeLimit;

		public Button ButtonNextExercise {
			get { return this.buttonNextExercise; }
		}

		public Button ButtonNextInstruction {
			get { return buttonNextInstruction; }
		}

		public Button ButtonContinueExercise {
			get { return this.buttonContinueExercise; }
		}
		
		public Button ButtonStartExercises {
			get { return this.buttonStartExercises; }
		}
		
		public Label LabelPauseText {
			get { return this.labelPauseText; }
		}

		public Gtk.Image GetImage {
			get { return image; }
		}

		public void ShowStartExercisesButton()
		{
			buttonNextInstruction.Hide();
			buttonNextInstruction.Sensitive = false;
				
			buttonNextExercise.Hide();
			buttonNextExercise.Sensitive = false;
			
			buttonContinueExercise.Hide();
			buttonContinueExercise.Sensitive = false;
			
			buttonStartExercises.Show();
			buttonStartExercises.Sensitive = true;
		}
		
		public void ShowContinueExerciseButton()
		{
			buttonNextInstruction.Hide();
			buttonNextInstruction.Sensitive = false;
				
			buttonNextExercise.Hide();
			buttonNextExercise.Sensitive = false;
			
			buttonContinueExercise.Show();
			buttonContinueExercise.Sensitive = true;
			
			buttonStartExercises.Hide();
			buttonStartExercises.Sensitive = false;
		}
		
		public void ShowNextInstructionButton()
		{
			buttonNextInstruction.Show();
			buttonNextInstruction.Sensitive = true;
				
			buttonNextExercise.Hide();
			buttonNextExercise.Sensitive = false;
			
			buttonContinueExercise.Hide();
			buttonContinueExercise.Sensitive = false;
			
			buttonStartExercises.Hide();
			buttonStartExercises.Sensitive = false;
		}
		
		public void ShowNextExerciseButton()
		{
			buttonNextInstruction.Hide();
			buttonNextInstruction.Sensitive = false;
				
			buttonNextExercise.Show();
			buttonNextExercise.Sensitive = true;
			
			buttonContinueExercise.Hide();
			buttonContinueExercise.Sensitive = false;
			
			buttonStartExercises.Hide();
			buttonStartExercises.Sensitive = false;
		}
		
		public TaskListPanel (List<string> listaPalabras, int exposure) : this(listaPalabras, exposure, false)
		{
		}

		public TaskListPanel (List<string> listaPalabras, int exposure, bool rojo)
		{
			this.Build ();
			
			Label b;
			
			int x = 90;
			int y = 30;
			int delta = 36;
			
			for (int i = 0; i < listaPalabras.Count; ++i) {
				
				b = new Label ();
				b.ShowAll ();
				b.Text = "-" + listaPalabras[i];
				GtkUtil.SetStyle (b, Configuration.Current.HandwrittingStyle);
				
				if (rojo)
					b.ModifyFg (StateType.Normal, new Gdk.Color (255, 0, 0));
				
				y = y + delta;
				
				fixed5.Put (b, x, y);
				
			}
			
			timeLimit = exposure;
			this.Show ();
			GtkUtil.SetStyle (this.labelPauseText, Configuration.Current.SmallFont);
			
			GtkUtil.SetStyle (this.buttonNextExercise,     Configuration.Current.ButtonFont);
			GtkUtil.SetStyle (this.ButtonNextInstruction,  Configuration.Current.ButtonFont);
			GtkUtil.SetStyle (this.ButtonContinueExercise, Configuration.Current.ButtonFont);
			GtkUtil.SetStyle (this.ButtonStartExercises,   Configuration.Current.ButtonFont);
			
			this.labelPauseText.Wrap = true;
			this.labelPauseText.Justify = Justification.Fill;
			this.labelPauseText.ModifyFont(Pango.FontDescription.FromString("Ahafoni CLM 20"));
			this.vbox1.ShowAll ();
			this.vbox3.Hide ();
		}

		public void StartTimer (TimerAction action)
		{
			Console.WriteLine("START TIMER!!!");
			
			if (t != null) {
				t.StopTimer();
				vbox1.Remove (t);
			}
			
			t = new Timer (action, timeLimit);
			vbox3.Add (t);
			this.vbox1.Hide ();
			this.vbox3.ShowAll ();
			
			t.StartClock ();
		}

		public void StopTimer ()
		{
			vbox1.Remove (t);
			t.StopTimer ();
			t.Destroy();
			
		}

		public void ShowLeftPanel ()
		{
			this.vbox3.Hide ();
			this.vbox1.ShowAll ();
			this.Show ();
		}

		public void ShowRightPanel ()
		{
			this.vbox3.ShowAll ();
			this.vbox1.Hide ();
			this.Show ();
		}

		public void SetInstructions (string s, Pixbuf pb)
		{
			this.labelPauseText.ModifyFont(Pango.FontDescription.FromString("Ahafoni CLM Bold 20"));
			labelPauseText.UseMarkup = true;
			labelPauseText.Markup = s;
			if (pb != null) {
				image.Pixbuf = pb;
				image.Show ();
			} else
				image.Hide ();
		}
		
		public void SetInstructionsSmall (string s, Pixbuf pb)
		{
			this.labelPauseText.ModifyFont(Pango.FontDescription.FromString("Ahafoni CLM  10"));
			labelPauseText.UseMarkup = true;
			labelPauseText.Markup = s;
			if (pb != null) {
				image.Pixbuf = pb;
				image.Show ();
			} else
				image.Hide ();
		}
	}
}

