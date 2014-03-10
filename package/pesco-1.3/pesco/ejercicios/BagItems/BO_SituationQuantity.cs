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


	public class BO_SituationQuantity
	{
		private BO_ItemActionWho itemActionWho;
		private int quantity;
		
		public BO_ItemActionWho ItemActionWho {
			get {
				return itemActionWho;
			}
			set {
				itemActionWho = value;
			}
		}
		
		
		public int Quantity {
			get {
				return quantity;
			}
			set {
				quantity = value;
			}
		}
		
		
		
		public BO_SituationQuantity ()
		{
		}
		
		public BO_SituationQuantity (BO_ItemActionWho obAccQui, int quantity)
		{
			this.ItemActionWho = obAccQui;
			this.Quantity = quantity;
		}
		
		public string Kind() {
		
			if ( ItemActionWho.Accion.Tipo == "add" )
				return "add";
			else if ( ItemActionWho.Accion.Tipo == "remove" )
				return "remove";
			
			return "undefined";
			
		}
	}
}

