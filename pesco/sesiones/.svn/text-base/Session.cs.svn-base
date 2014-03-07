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
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using System;

namespace pesco
{

	/// <summary>
	/// Clase que representa una sesión de ejercicios.
	/// </summary>
	public class Session
	{

		[XmlElement("starttext")]
		protected string starttext;
		/// <summary>
		/// Lista de ejercicios en los que consistirá la sesión
		/// </summary>
		/// 
		[XmlElement("exercisesEntries")]
		protected List <ExerciseEntry> exercises = new List<ExerciseEntry>();
		
		[XmlElement("idsession")]
		protected int idSession;		
		
		public int IdSession {
			get {
				return idSession;
			}
			set {
				idSession = value;
			}
		}
	
		public List<ExerciseEntry> Exercises {
			get {
				return exercises;
			}
			set {
				exercises = value;
			}
		}
		
		public string Starttext {
			get {
				return starttext;
			}
			set {
				starttext = value;
			}
		}
		
		
		public Session ()
		{
			
		}
		
		public Session ( int id, string starttext, List<ExerciseEntry> exercises ) {
		
			this.IdSession = id;
			this.Starttext = starttext;
			this.Exercises = exercises;
			
		}
		
		public ExerciseEntry GetExerciseEntry( int id ) {

			if ( id >= 0 && id < Exercises.Count ) {
				return Exercises[id];
			} else {
				Gtk.MessageDialog dialog = new Gtk.MessageDialog (null, Gtk.DialogFlags.Modal, Gtk.MessageType.Error, Gtk.ButtonsType.Ok, "<span size=\"xx-large\">El fichero de sesiones es incorrecto. Se ejecutará la sesión "+this.IdSession+" por completo. Consulte con el administrador de la sala.</span>", "Fichero de sesiones incorrecto");
				GtkUtil.SetStyleRecursive( dialog, Configuration.Current.LargeFont );
				Gtk.ResponseType result = (Gtk.ResponseType) dialog.Run ();
				dialog.Destroy();			
				return Exercises[0];				
			}			
		}
		
		public ExerciseEntry GetLastExercise() {
		
			if ( Exercises.Count == 0 )
				return null;
			else
				return Exercises[Exercises.Count-1];			
		}
	}
}

