/**

Copyright 2011 Grupo de Investigación GEDES
Lenguajes y sistemas informáticos
Universidad de Granada

Licensed under the EUPL, Version 1.1 or – as soon they 
will be approved by the European Commission - subsequent  
versions of the EUPL (the "Licence"); 
You may not use this work except in compliance with the 
Licence. 
You may obtain a copy of the Licence at: 

http://ec.europa.eu/idabc/eupl  

Unless required by applicable law or agreed to in 
writing, software distributed under the Licence is 
distributed on an "AS IS" basis, 
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either 
express or implied. 
See the Licence for the specific language governing 
permissions and limitations under the Licence. 



*/
using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
namespace pesco
{
	/// <summary>
	/// Reasoning exercise that presents to the user a list of options
	/// </summary>
	public abstract class CompleteSeriesExercise<T, Q> : ReasoningExercise<T, Q> where T : ReasoningExerciseElement where Q : ReasoningPanel
	{
		/// <summary>
		/// 
		/// </summary>
		protected Series<T> currentSeries;

		int inicio = 0;

		/// <summary>
		/// 
		/// </summary>
		[XmlIgnore]
		public Series<T> CurrentSeries {
			get { return this.currentSeries; }
			set { currentSeries = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		protected List<Series<T>> seriesPool;

		/// <summary>
		/// 
		/// </summary>
		[XmlElement("pool")]
		public List<Series<T>> SeriesPool {
			get { return this.seriesPool; }
			set { seriesPool=value;		}
		}

		/// <summary>
		/// 
		/// </summary>
		protected CompleteSeriesExercise () : base()
		{
			currentSeries = new Series<T> ();
			this.seriesPool = new List<Series<T>> ();
		}

		/// <summary>
		/// Selects a random series exercise
		/// </summary>
		protected override bool Next ()
		{
			base.Next ();
			
			if (seriesPool.Count > 0) {
				int pos = r.Next (0, seriesPool.Count);
				this.currentSeries = seriesPool[pos];
				
				TaskListExercise.Shuffle<T> (currentSeries.Options);
				
				repetitions++;
				
				return true;
			} else
				return false;
		}

		/// <summary>
		/// Selects a random series with a certain difficulty
		/// </summary>
		/// <param name="d">
		/// A <see cref="ReasoningExerciseDifficulty"/> 
		/// </param>
		protected override bool Next (ReasoningExerciseDifficulty d)
		{
			
			base.Next (d);
			
			if (SessionManager.GetInstance ().HaveToFinishCurrentExercise ()) {
				// Console.WriteLine ("End of exercise.");
				return false;
			}
			
			// if there are series to choose
			if (seriesPool.Count > 0) {
				// select all the series with difficulty = d
				List<Series<T>> pool = new List<Series<T>> ();
				
				foreach (Series<T> s in seriesPool)
					if (s.Difficulty == d)
						pool.Add (s);
				
				// shuffle the list of available exercises
				TaskListExercise.Shuffle<Series<T>>(pool);
				
				// select random series
				if (pool.Count > 0) {
					int contador = 0;
					
					// tries to select a yet-not-showed series
					do {
						this.currentSeries = pool[contador];
						contador++;
					} while (this.showedExercises.Contains (currentSeries.ID) && contador < pool.Count);
					
					// if all the series with level d have been shown already => finish exercise
					if (contador >= pool.Count) {
						/*Gtk.MessageDialog md = new Gtk.MessageDialog (window, Gtk.DialogFlags.DestroyWithParent, Gtk.MessageType.Error, Gtk.ButtonsType.Ok, "Actualmente no existen ejercicios de tu nivel actual");						
						md.Run ();
						md.Destroy ();*/
						return false;
					} else { 

						this.showedExercises.Add (currentSeries.ID);						
						TaskListExercise.Shuffle<T> (currentSeries.Options);
						repetitions++;
						
						Console.WriteLine(this.showedExercises);
						Console.WriteLine(currentSeries.ID);
						
						return true;
					}
					
				// there are no series with level d :( 
				} else {
					/*Gtk.MessageDialog md = new Gtk.MessageDialog (window, Gtk.DialogFlags.DestroyWithParent, Gtk.MessageType.Error, Gtk.ButtonsType.Ok, "Actualmente no existen ejercicios de tu nivel actual");
					
					md.Run ();
					md.Destroy ();*/
					return false;
				}
			// there are no series to choose :/
			} else {
				/*Gtk.MessageDialog md = new Gtk.MessageDialog (window, Gtk.DialogFlags.DestroyWithParent, Gtk.MessageType.Error, Gtk.ButtonsType.Ok, "Actualmente no existen ejercicios de este tipo");
				
				md.Run ();
				md.Destroy ();*/
				return false;
			}
						
		
			/*
			// DEBUG CODE - It lists all the series and finish when all the series are listed
			if (inicio >= this.SeriesPool.Count) return false;
			
			Console.WriteLine(this.SeriesPool.Count + "\n");
			
			currentSeries = SeriesPool[inicio];
			inicio++;
			TaskListExercise.Shuffle<T>(CurrentSeries.Options);
			return true;
			
			*/
			return true;
		}
	}
}


