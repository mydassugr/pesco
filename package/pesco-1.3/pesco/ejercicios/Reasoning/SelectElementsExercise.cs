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
	/// Reasoning exercise that presents to the user a list of options, some of them being the correct ones
	/// </summary>
	public abstract class SelectElementsExercise<T, Q>:ReasoningExercise<T, Q> where T : ReasoningExerciseElement where Q : ReasoningPanel
	{
		/// <summary>
		/// 
		/// </summary>
		protected ListOfElements<T> currentList;
						
		int inicio = 0;
		
		
		/// <summary>
		/// 
		/// </summary>
		[XmlIgnore]
		public ListOfElements<T> ListOfElements {
			get {
				return this.currentList;
			}
			set{
				currentList = value;
			}
		}
		
		
		/// <summary>
		/// 
		/// </summary>
		protected List<ListOfElements<T>> poolOfElements;

		/// <summary>
		/// 
		/// </summary>
		/// 
		
		[XmlElement("pool")]
		public List<ListOfElements<T>> PoolOfElements {
			get {
				return this.poolOfElements;
			}
			set { poolOfElements=value;}
		}

		/// <summary>
		/// 
		/// </summary>
		public SelectElementsExercise () : base()
		{
			this.currentList = new ListOfElements<T>();
			this.poolOfElements = new List<ListOfElements<T>>();
			
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <returns>
		/// A <see cref="System.Boolean"/>
		/// </returns>
		protected override bool Next()
		{
			if (poolOfElements.Count > 0)
			{		
				int pos = r.Next(0, poolOfElements.Count);				
				this.currentList =  poolOfElements[pos];
				TaskListExercise.Shuffle<T>(currentList.Elements);
				repetitions++;
				return true;
			}
			else
			{
				/*Gtk.MessageDialog md = new Gtk.MessageDialog (window, Gtk.DialogFlags.DestroyWithParent, Gtk.MessageType.Error, Gtk.ButtonsType.Ok, "Actualmente no existen ejercicios de este tipo");
				
				md.Run ();
				md.Destroy ();*/
				return false;
			}
		}
				
		/// <summary>
		/// Selects a random list with a certain difficulty
		/// </summary>
		/// <param name="d">
		/// A <see cref="ReasoningExerciseDifficulty"/> indicating the desired difficulty
		/// </param>
		protected override bool Next(ReasoningExerciseDifficulty d)
		{
			
			
			if (SessionManager.GetInstance().HaveToFinishCurrentExercise())
			{
				// Console.WriteLine ("End of exercise.");
				return false;
			}
			
			
			if (poolOfElements.Count > 0)
			{		
				List<ListOfElements<T>> pool = new List<ListOfElements<T>>();
				
				foreach(ListOfElements<T> s in poolOfElements)
					if (s.Difficulty == d)
						pool.Add(s);
				
				TaskListExercise.Shuffle<ListOfElements<T>>(pool);
				
				if (pool.Count > 0)
				{
					int contador = 0;
					// tries to select a yet-not-showed series
					do {
						this.currentList = pool[contador];
						contador++;
					} while (this.showedExercises.Contains (currentList.ID) && contador < pool.Count);
					
					// if all the series with level d have been shown already => finish exercise
					if (contador >= pool.Count) {
						/*Gtk.MessageDialog md = new Gtk.MessageDialog (window, Gtk.DialogFlags.DestroyWithParent, Gtk.MessageType.Error, Gtk.ButtonsType.Ok, "Actualmente no existen ejercicios de tu nivel actual");						
						md.Run ();
						md.Destroy ();*/
						return false;
					} else { 
						this.showedExercises.Add (currentList.ID);
						TaskListExercise.Shuffle<T>(currentList.Elements);
						repetitions++;
						
						return true;
					}
				}
				else {
					/*Gtk.MessageDialog md = new Gtk.MessageDialog (window, Gtk.DialogFlags.DestroyWithParent, Gtk.MessageType.Error, Gtk.ButtonsType.Ok, "Actualmente no existen ejercicios de tu nivel actual");
			
					md.Run ();
					md.Destroy ();*/
					return false;
				}
			}
			else
			{
				/*Gtk.MessageDialog md = new Gtk.MessageDialog (window, Gtk.DialogFlags.DestroyWithParent, Gtk.MessageType.Error, Gtk.ButtonsType.Ok, "Actualmente no existen ejercicios de este tipo");
			
				md.Run ();
				md.Destroy ();*/
				return false;
			}
			/*
			
			// DEBUG CODE - It lists all the series and finish when all the series are listed
			if (inicio >= this.PoolOfElements.Count) return false;
			
			currentList = poolOfElements[inicio];
			inicio++;
			TaskListExercise.Shuffle<T>(currentList.Elements);
			return true;
			*/
			
			
		}
	}
}


