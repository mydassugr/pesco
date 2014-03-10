using System;
using System.Collections.Generic;
using System.Security.Cryptography;

using Gdk;
using Gtk;


namespace pesco
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class SpatialReasoningPanel : ReasoningPanel
	{
		Pixbuf original;
		Pixbuf distractor;
		
		Pixbuf[] pieces_original = new Pixbuf[6];
		Pixbuf[] pieces_distractor = new Pixbuf[6];
		
		 public int numSelectd = 0;
		
		public List<SpatialReasoningToggleButton> selectd = new List<SpatialReasoningToggleButton>();
		
		public SpatialReasoningToggleButton[] buttons = new SpatialReasoningToggleButton[8];
		
		private string DirImg =Configuration.ProgramDir()+System.IO.Path.DirectorySeparatorChar+"ejercicios"+System.IO.Path.DirectorySeparatorChar+"Reasoning"+System.IO.Path.DirectorySeparatorChar+"SpatialReasoning"+
			System.IO.Path.DirectorySeparatorChar+"figures"+System.IO.Path.DirectorySeparatorChar;
		
		Random r = new Random(DateTime.Now.Millisecond);
		
		
		public SpatialReasoningPanel ()
		{
			this.Build ();			
			GtkUtil.SetStyle(botonListo, Configuration.Current.ButtonFont);
            //GtkUtil.SetStyle(label2, Configuration.Current.SmallFont);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="samples">
		/// A <see cref="List<SpatialReasoningElement>"/>
		/// </param>
		/// <param name="options">
		/// A <see cref="List<SpatialReasoningElement>"/>
		/// </param>
		public void Populate (SpatialReasoningSeries series, int[] positions)
		{
						
			selectd.Clear();
			numSelectd = 0;
			
			original = new Gdk.Pixbuf(DirImg+series.Original.Value);
			distractor = new Gdk.Pixbuf(DirImg+series.Distractor.Value);
			
			int x = 0;
			int y = 0;
			
			int hIncrement = (int)Math.Floor(original.Width  / 3.0);
			int vIncrement = (int) Math.Floor(original.Height / 2.0);

			for (int i=0; i<6; i++)
			{
				pieces_original[i] = new Pixbuf(original,x, y, hIncrement, vIncrement);
											
				if ( (i+1) % 3 == 0){
					y += vIncrement;
					x = 0;
				}
				else {
					x += hIncrement;
				}
			}
			
			pieces_distractor[0] = new Pixbuf(distractor,   0,  0,  hIncrement, vIncrement);
			pieces_distractor[1] = new Pixbuf(distractor, hIncrement, vIncrement,  hIncrement, vIncrement);
			
			
			// remove the old labels, making room for the new ones!
			foreach (Widget w in this.cajaMuestras)
				cajaMuestras.Remove (w);
			
			
			
			// remove the old buttons, making room for the new ones!
			foreach (Widget w in this.cajaOpciones)
			{
				cajaOpciones.Remove (w);
			}
			
			this.cajaMuestras.Add (new Gtk.Image(original)/*series.Original.GetWidget()*/);
			int pos = 0;
			
			
			for(int i=0; i<pieces_original.Length; ++i)
			{
				buttons[pos] = new SpatialReasoningToggleButton(pieces_original[i],false);
				//buttons[pos].SetBtnImage(pieces_original[i]);
				pos++;
			}
			
			buttons[pos] = new SpatialReasoningToggleButton(pieces_distractor[0],true);
			pos++;
			buttons[pos] = new SpatialReasoningToggleButton(pieces_distractor[1],true);
			pos++;
			Shuffle<SpatialReasoningToggleButton>(ref buttons);
			
			foreach(SpatialReasoningToggleButton srtb in buttons){
				cajaOpciones.Add(srtb);
				srtb.Clicked += delegate(object sender, EventArgs e) {
					
					SpatialReasoningToggleButton button = sender as SpatialReasoningToggleButton;
					
					// if the sender is a ReasoningExerciseToggleButtons, as it may be...
					if (button.Active) {
						if (numSelectd == 2)
						{
							// desactivate button
							button.Active = false;
							
							// 	show warning message
							// Console.WriteLine("hay mas de uno seleccionado!!!!!   " + numSelectd);
							
							Gtk.MessageDialog md = new Gtk.MessageDialog (null, Gtk.DialogFlags.DestroyWithParent, 
					                                              Gtk.MessageType.Error, Gtk.ButtonsType.Ok, 
					                                              "<big><big><big>Solo puedes seleccionar <b>dos fragmentos</b>. Si quieres cambiar tu selección desmarca antes una de las opciones elegidas.</big></big></big>");					
							Gtk.ResponseType result = (Gtk.ResponseType)md.Run ();
							md.Destroy ();
						}
						else {
							numSelectd++;
							selectd.Add(button);
							// Console.WriteLine("hay menos de dos seleccionados puedes seguir!!!!!   " + numSelectd);
							
						}
					}
					else {
						if (selectd.Contains(button)){
							numSelectd--;
							// Console.WriteLine("desmarcando!!!!!   " + numSelectd);
							selectd.Remove(button);
						}
						else {
							// Console.WriteLine("no cuela!!!!!   " + numSelectd);	
						}
					}
				};
			}
            
           	cajaOpciones.ShowAll();
       	}

		public static void Shuffle<T>(ref T[] list)
       {
           RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
           int n = list.Length;
           while (n > 1)
           {
               byte[] box = new byte[1];
               do provider.GetBytes(box);
               while (!(box[0] < n * (Byte.MaxValue / n)));
               int k = (box[0] % n);
               n--;
               T value = list[k];
               list[k] = list[n];
               list[n] = value;
           }
       }
		
		/// <summary>
		/// 
		/// </summary>
		/// <returns>
		/// A <see cref="List<SpatialReasoningElement>"/>
		/// </returns>
		public List<int> GetSelectedItems ()
		{
			List<int> sses = new List<int> ();
			int seleccionadas = 0;
			int aciertos = 0;
			foreach (SpatialReasoningToggleButton w in cajaOpciones)
				if (w.Active)
				{
					seleccionadas++;
					if (w.IsDistractor)
						aciertos++;
				}
			
			sses.Add(aciertos);
			sses.Add(seleccionadas);
			return sses;
		}
		
		#region implemented abstract members of pesco.ReasoningPanel
		public string ExplanationText {
			get {
				return "";
			}
			set {
				;
			}
		}
		
		public override void HideHelpButton(){
			
		}
		
		public override void HideExplanation ()
		{
			explanation.Markup = "";
		}
		
		
		public override void ShowExplanation ()
		{
			
		}
		
		public override  void ShowCorrectExplanation(bool correct){
			List<int> si= GetSelectedItems();
			ReasoningExerciseResult r = new ReasoningExerciseResult ();
			r.Aciertos=si[0];
			
			GtkUtil.SetStyle( explanation, Configuration.Current.MediumFont);
			if(r.Aciertos ==2)
				explanation.Markup = "<span color='blue'><b>¡Muy bien!</b> Has acertado las <b>dos</b>. </span>";
			if(r.Aciertos ==1)
				explanation.Markup = "<span color='blue'><b>¡Bien!</b> Has tenido <b>un</b> acierto.\n La imagen errónea aparece con una cruz roja y las correctas aparecen en verde.</span>";
			if(r.Aciertos ==0)
				explanation.Markup = "<span color='blue'>Fíjate bien, en verde se muestran los dos fragmentos que no pertenecen a la imagen.</span>";
			
					 
		}
		public override void MoveSolution(string key){
		}
		public override bool SolutionSelected(){
			
			return false;
		}
		
		public override Button HelpButton {
			get {
				return null;
			}
		}
		
		#endregion
		/// <summary>
		/// 
		/// </summary>
		public override Button ReadyButton{
			get{ return this.botonListo;}	
		}
		
	}
}

