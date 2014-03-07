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


	public class LI_ItemInGame
	{
		
		private int itemId;
		private LI_Item item;
		// Status will be position of the item, or -1 if item is not in a room
		private int itemPosition;
		
		public LI_Item Item {
			get {
				return this.item;
			}
			set {
				item = value;
			}
		}

		public int ItemId {
			get {
				return this.itemId;
			}
			set {
				itemId = value;
			}
		}

		public int ItemPosition {
			get {
				return this.itemPosition;
			}
			set {
				itemPosition = value;
			}
		}

		public LI_ItemInGame ()
		{
		}
		
		public LI_ItemInGame (LI_Item item, int position) {
		
			this.itemId = item.Id;
			this.item = item;
			this.itemPosition = position;
			
		}
	}
}

