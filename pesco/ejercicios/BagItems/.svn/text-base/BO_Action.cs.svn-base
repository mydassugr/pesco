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
	
	public class BO_Action
	{

	
		string id;
		string texto;
		string tipo;

		public string Tipo {
			get {
				if (tipo == "agregar") {
					return "add";	
				} else if (tipo == "eliminar" ) {
					return "remove";
				}
				return tipo;
			}
			set {
				tipo = value;
			}
		}
		
		
		public string Texto {
			get {
				return texto;
			}
			set {
				texto = value;
			}
		}
		
		
		public string Id {
			get {
				return id;
			}
			set {
				id = value;
			}
		}
						
		public BO_Action ()
		{
		}
		
		public BO_Action (string id, string texto, string tipo)
		{
			Id = id;
			Texto = texto;
			Tipo = tipo;
		}
	}
}

