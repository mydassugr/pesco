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
using Gtk;

namespace pesco
{

	/// <summary>
	///  Clase que se encarga de controlar el proceso de registro del usuario
	/// </summary>
	public class RegisterManager
	{

		/// <summary>
		/// Constructor, es privado para evitar que se instancie la clase
		/// </summary>
		private RegisterManager ()
		{
		}

		public static bool FinishTest ()
		{
			FunctionalScaleDialog def = FunctionalScaleDialog.GetDailyLifeQuestionaryDialog ();
			// daily life questionary is NOT finished
			if (!def.Quest.IsFinished ()) {
				def.RunDialog ();
			} else {
				// daily life questionary is finished
				def.Response = ResponseType.Ok;
			}
			
			ResponseType r = def.Response;
			
			def = FunctionalScaleDialog.GetInstrumentalActivitiesQuestionaryDialog ();
			// instrumental test is finished
			if (def.Quest.IsFinished ())
			// instrumental activities questionary is finished and the user finished cancel the previous questionary
				return true; else if (r == ResponseType.Ok) {
				def.RunDialog ();
				
				if (def.Response == ResponseType.Ok)
					// the user finished the questionary
					return true;
				else
					return false;
				// the user didn't finished the test
			} else
				return false;
			// the user didn't finished daily life questionary
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns>
		/// A <see cref="Boolean"/>
		/// </returns>
		public static Boolean RegisterUser ()
		{
			//1. Registro de los datos personales
			/*UserRegistrationDialog w = new UserRegistrationDialog ();
			w.Run ();
			
			if (w.Response.Equals (ResponseType.Ok)) {
				
				//TODO 3. Prueba de habilidades informáticas
				
				//2. Primera escala funcional
				
				FunctionalScaleDialog def = FunctionalScaleDialog.GetDailyLifeQuestionaryDialog ();
				
				if (!def.Quest.IsFinished ()) {
					def.RunDialog ();
				} else {
					//Mostrar mensaje de notificación de cuestionario ya realizado
					Gtk.MessageDialog md = new Gtk.MessageDialog (def, Gtk.DialogFlags.DestroyWithParent, Gtk.MessageType.Question, Gtk.ButtonsType.YesNo, "Aparentemente ya has completado el test. ¿Deseas volver a completarlo desde el principio?");
					
					Gtk.ResponseType result = (Gtk.ResponseType)md.Run ();
					md.Destroy ();
					
					if (result == Gtk.ResponseType.Yes) {
						def.Quest.Reset ();
						def.RunDialog ();
					} else {
						def.Response = ResponseType.Ok;
					}
				}
				
				if (def.Response.Equals (ResponseType.Ok)) {
					
					//3. Segunda escala funcional
					def = FunctionalScaleDialog.GetInstrumentalActivitiesQuestionaryDialog ();
					if (!def.Quest.IsFinished ()) {
						def.RunDialog ();
						return true;
					} else {
						//Mostrar mensaje de notificación de cuestionario ya realizado
						Gtk.MessageDialog md = new Gtk.MessageDialog (def, Gtk.DialogFlags.DestroyWithParent, Gtk.MessageType.Question, Gtk.ButtonsType.YesNo, "Aparentemente ya has completado el test. ¿Deseas volver a completarlo desde el principio?");
						
						Gtk.ResponseType result = (Gtk.ResponseType)md.Run ();
						md.Destroy ();
						
						if (result == Gtk.ResponseType.Yes) {
							def.Quest.Reset ();
							def.RunDialog ();
							return true;
						} else
							return true;
					}
				} else
					return false;
			} else
				return false;
				*/
			return true;
		}
		
		
	}
}

