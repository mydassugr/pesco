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

namespace pesco
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class IntroductionDialog : Gtk.Bin
	{
		
		public DialogPanel Dialog {
			get{
				return dialogPanel;	
			}
		}
		
		protected virtual void buttonClicked (object sender, System.EventArgs e)
		{
			
			if ( Dialog.CurrentStep == Dialog.TextsToShow.Count - 1 ) {
				SessionManager.GetInstance().ExerciseFinished(-1);
				SessionManager.GetInstance().TakeControl();
				this.Destroy();
			} else {
				Dialog.NextStep();	
			}
			
		}
				
		public IntroductionDialog ()
		{
			this.Build ();
			List <string> auxTexts = new List<string>();
			auxTexts.Add( "Durante 12 sesiones de aproximadamente una hora realizaremos ejercicios para estimular diferentes áreas: la memoria, la atención, el razonamiento y la planificación.");
			auxTexts.Add( "Puedes interrumpir las sesiones en cualquier momento, aunque te recomiendo que si comienzas una, intentes completarla en el mismo día.");
			auxTexts.Add( "Antes de hacer cada ejercicio te explicaré en que consiste y te haré una prueba para que practiques. Después tendrás que hacerlos tu solo.");
			auxTexts.Add( "Ahora que sabes como vamos a trabajar, me gustaría que contestases unas preguntas sobre tí. Confía en mi: ¡tus respuestas serán totalmente confidenciales y nadie podrá verlas!");
			dialogPanel.SetText( auxTexts );
			dialogPanel.InitPanel();
			
			GtkUtil.SetStyle(this.buttonNext, Configuration.Current.ButtonFont);			
		}
	}
}


