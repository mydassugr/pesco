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
	public class QuestionaryExercise : Exercise
	{
		
		#region implemented abstract members of pesco.Exercise
		public override int idEjercicio ()
		{
			return 31;
		}
		
		
		public override string NombreEjercicio ()
		{
			return "Cuestionario";
		}
		
		
		public override bool inicializar ()
		{
			// SessionManager.GetInstance().ReplacePanel( new Prueba2() );
			
			return true;
		}
		
		
		public override void iniciar ()
		{
			
			FunctionalScaleDialog def = FunctionalScaleDialog.GetDailyLifeQuestionaryDialog ();
			
			if (!def.Quest.IsFinished ()) {
					SessionManager.GetInstance().ReplacePanel( def);
			} 
			else {
				//Mostrar mensaje de notificación de cuestionario ya realizado
				/*Gtk.MessageDialog md = new Gtk.MessageDialog (def, Gtk.DialogFlags.DestroyWithParent, Gtk.MessageType.Question, Gtk.ButtonsType.YesNo, "Aparentemente ya has completado el test. ¿Deseas volver a completarlo desde el principio?");
				
				Gtk.ResponseType result = (Gtk.ResponseType)md.Run ();
				md.Destroy ();
				
				if (result == Gtk.ResponseType.Yes) {
					def.Quest.Reset ();
					SessionManager.GetInstance().ReplacePanel( def);
				} else {
					SessionManager.GetInstance().ExerciseFinished(-1);
					SessionManager.GetInstance().TakeControl();
				}*/
			}
			//SessionManager.GetInstance().ReplacePanel( new FunctionalScaleDialog());
		}
		
		
		public override void finalizar ()
		{
			throw new System.NotImplementedException();
		}
		
		
		public override void pausa ()
		{
			throw new System.NotImplementedException();
		}
		
		#endregion
		public QuestionaryExercise ()
		{
			
		}
	}
}


