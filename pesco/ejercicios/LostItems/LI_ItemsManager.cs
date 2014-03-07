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
	
	public class LI_ItemsManager : XmlSerializable
	{
				
		List <LI_Item> items = new List<LI_Item>();		
		private List <LI_Item> nonUsedItems = new List<LI_Item>();
		
		public List<LI_Item> Items {
			get {
				return this.items;
			}
			set {
				items = value;
			}
		}
		
		public LI_ItemsManager ()
		{			
			
			xmlPath = "/"+Configuration.ExercisesFolderName+"/LostItems/xml-templates/li-items.xml";
			
		}
		
		public LI_Item GetItemById( int itemId ) {
			
			for ( int i = 0; i < items.Count; i++ ) {
				if ( items[i].Id == itemId ) {
					return items[i];	
				}
			}
			return null;
		}
		
		public void ResetNonUsedList()  {
			
			nonUsedItems = new List<LI_Item>();
			foreach ( LI_Item item in items ) {
				nonUsedItems.Add( item );
			}
			
		}
		
		public void RemoveItemsNotInGame( List<LI_RoomInGame> roomsInGame ) {
		
			foreach ( LI_Item item in items ) {
				bool removeCurrent = true;
				foreach ( LI_RoomInGame room in roomsInGame ) {
					if ( room.RoomId == item.Room ) {
						removeCurrent = false;
						break;
					}
				}
				if ( removeCurrent ) {
					nonUsedItems.Remove( item );
				}									
			}
			
		}
		
		public LI_Item GetNonUsedInvalidItemForRoom( LI_RoomInGame roomInGame, List <LI_RoomInGame> roomsInGame, List <LI_Item> itemsAlreadyUsed ) {
			
			List <LI_Item> auxItems = ListUtils.Shuffle<LI_Item>(nonUsedItems);			
			// Look for a non used item			
			for ( int i = 0; i < auxItems.Count; i++ ) {
				// Is invalid in this room?
				if ( auxItems[i].Room == roomInGame.RoomId ) {
					// If it is not invalid, discard and continue
					continue;
				}
				// Fits in this room?
				if ( auxItems[i].FitInRoom( roomInGame.Room ) == false ) {
					// If it does not fit, discard and continue
					continue;	
				}				
				// Get item from original list
				LI_Item auxItemToReturn = GetItemById( auxItems[i].Id );
				// Remove and return
				nonUsedItems.Remove( auxItems[i] );
				return auxItemToReturn;				
			}			
			return null;
		}
				
		public LI_Item GetNonUsedValidItemForRoomPosition( int roomId, int positionId, List <LI_ItemInGame> itemsAlreadyUsed ) {
			
			List <LI_Item> auxItems = ListUtils.Shuffle<LI_Item>(nonUsedItems);
			// Look for a non used item
			for ( int i = 0; i < auxItems.Count; i++ ) {
				if ( auxItems[i].Room != roomId ) {
					// If it is not valid item, discard and continue
					continue;
				}
				
				// Fits the item in the desired position?
				LI_Position auxPosition = LIExercise.GetInstance().PositionsManager.GetPositionById( positionId );
				if ( auxItems[i].FitInPosition( auxPosition) ) {
					// If it fits, remove from non used items and return it
					LI_Item auxItemToReturn = GetItemById( auxItems[i].Id );
					nonUsedItems.Remove( auxItems[i] );
					return auxItemToReturn;
				}
			}
			
			return null;
		}
	}
}

