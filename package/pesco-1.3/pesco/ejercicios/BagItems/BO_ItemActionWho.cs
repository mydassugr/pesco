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

namespace pesco
{


	public class BO_ItemActionWho
	{

		private BO_Item objeto;
		private BO_Action accion;
		private BO_Who quien;
		
		public BO_Item Objeto {
			get {
				return objeto;
			}
			set {
				objeto = value;
			}
		}
		
		
		public BO_Action Accion {
			get {
				return accion;
			}
			set {
				accion = value;
			}
		}
		
		public BO_Who Quien {
			get {
				return quien;
			}
			set {
				quien = value;
			}
		}
		
		public BO_ItemActionWho ()
		{
		}
		
		public BO_ItemActionWho (BO_Item objeto, BO_Action accion, BO_Who quien) {
			
			Objeto = objeto;
			Accion = accion;
			Quien = quien;
			
		}
	}
}

