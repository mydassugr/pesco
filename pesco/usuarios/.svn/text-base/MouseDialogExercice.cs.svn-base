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
	public class MouseDialogExercice:Exercise
	{
		#region implemented abstract members of pesco.Exercise
		public override int idEjercicio ()
		{
			return 32;
		}
		
		
		public override string NombreEjercicio ()
		{
			return "MouseDialog";
		}
		
		
		public override bool inicializar ()
		{
			return true;
		}
		
		
		public override void iniciar ()
		{
			MouseTestDialog md= new MouseTestDialog();
			SessionManager.GetInstance().ReplacePanel( md );
			
		}
		
		
		public override void finalizar ()
		{
		}
		
		
		public override void pausa ()
		{
			throw new System.NotImplementedException();
		}
		
		#endregion
		public MouseDialogExercice ()
		{
		}
	}
}


