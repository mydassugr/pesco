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


	public class LI_RoomInGame
	{
		
		private int roomId;
		private LI_Room room;
		private Dictionary<int,int> itemsPositionsStatus;
		private Dictionary<int,int> reservedPosition;
		private Dictionary<int,int> coinsPositionsStatus;
		
		public Dictionary<int, int> CoinsPositionsStatus {
			get {
				return this.coinsPositionsStatus;
			}
			set {
				coinsPositionsStatus = value;
			}
		}

		public Dictionary<int, int> ItemsPositionsStatus {
			get {
				return this.itemsPositionsStatus;
			}
			set {
				itemsPositionsStatus = value;
			}
		}
				
		public Dictionary<int, int> ReservedPosition {
			get {
				return this.reservedPosition;
			}
			set {
				reservedPosition = value;
			}
		}
		
		public LI_Room Room {
			get {
				return this.room;
			}
			set {
				room = value;
			}
		}

		public int RoomId {
			get {
				return this.roomId;
			}
			set {
				roomId = value;
			}
		}

		public LI_RoomInGame ()
		{			
		}
		
		public LI_RoomInGame (LI_Room room) {
			// Set id and room
			this.roomId = room.Id;
			this.room = room;
			// Reset positions
			this.itemsPositionsStatus = new Dictionary<int,int>();
			this.coinsPositionsStatus = new Dictionary<int, int>();
			this.reservedPosition = new Dictionary<int, int>();
			for ( int i = 0; i < room.ItemsPositions.Count; i++ ) {
				this.reservedPosition.Add( room.ItemsPositions[i], -1 );
			}
			for ( int i = 0; i < room.ItemsPositions.Count; i++ ) {
				this.itemsPositionsStatus.Add( room.ItemsPositions[i], -1 );
			}
			for ( int i = 0; i < room.CoinsPositions.Count; i++ ) {
				this.coinsPositionsStatus.Add( room.CoinsPositions[i], -1 );
			}			
		}
	}
}

