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
using System.Collections.Generic;
using System;

namespace pesco
{


	public class LI_RoomsManager : XmlSerializable
	{
		
		List <LI_Room> rooms = new List<LI_Room>();
		
		public List<LI_Room> Rooms {
			get {
				return this.rooms;
			}
			set {
				rooms = value;
			}
		}


		public LI_RoomsManager ()
		{
			xmlPath = "/"+Configuration.ExercisesFolderName+"/LostItems/xml-templates/li-rooms.xml";
			/*
			// Sample room
			List <string> decorationImages = new List<string>();
			List <LI_Position> decorationsPositions = new List<LI_Position>();
			List <int> itemsPositions = new List<int>();
			List <int> coinsPositions = new List<int>();
			// Decoration
			decorationImages.Add("bath.png");
			decorationsPositions.Add( new LI_Position(40,332) );
			decorationImages.Add("wc.png");
			decorationsPositions.Add( new LI_Position(859,325) );
			// Items positions
			itemsPositions.Add( 1 );
			itemsPositions.Add( 2 );
			itemsPositions.Add( 3 );
			itemsPositions.Add( 4 );
			itemsPositions.Add( 5 );
			itemsPositions.Add( 6 );
			itemsPositions.Add( 7 );
			itemsPositions.Add( 8 );
			itemsPositions.Add( 9 );
			itemsPositions.Add( 10 );
			// Coins positions
			coinsPositions.Add( 11 );
			coinsPositions.Add( 12 );
			coinsPositions.Add( 13 );
			coinsPositions.Add( 14 );
						
			rooms.Add( new LI_Room(0, "Baño", "bathroom.png", "bathroombackground", decorationImages, decorationsPositions, itemsPositions, coinsPositions ) );
			*/
		}
		
		public LI_Room GetRoomById ( int idRoom ) {
			
			for ( int i = 0; i < rooms.Count; i++ ) {
				if ( rooms[i].Id == idRoom ) {
					return rooms[i];	
				}		
			}
			
			return null;
		}
		
		public LI_Room GetNonUsedInGameRoom ( List <LI_RoomInGame> usedRooms ) {

			List <LI_Room> auxRooms = ListUtils.Shuffle<LI_Room>(rooms);
			// Look for a non used room
			// This search is not optimized, but in the worst case we are going to look between 8 rooms
			for ( int i = 0; i < auxRooms.Count; i++ ) {
				bool alreadyUsed = false;
				for ( int j = 0; j < usedRooms.Count; j++ ) {
					if ( auxRooms[i].Id == usedRooms[j].RoomId ) {
						alreadyUsed = true;
					}
				}				
				if ( !alreadyUsed ) {
					return auxRooms[i];	
				}
			}
			return null;
		}
		
		public int GetItemPositionInRoomFor	( LI_RoomInGame roomInGame, LI_Item item ) {
			
			LI_Room room = roomInGame.Room;
			List <int> auxShufflePositions = ListUtils.Shuffle<int>( room.ItemsPositions );
			for ( int i = 0; i < auxShufflePositions.Count; i++ ) {
				// Console.WriteLine( "Buscando por ID: "+auxShufflePositions[i]);
				LI_Position auxPosition = LIExercise.GetInstance().PositionsManager.GetPositionById( auxShufflePositions[i] );
				try { 
					// Console.Write( " | Peto al buscar posicion "+auxPosition.Id);
				} catch ( Exception e ) {
					Console.WriteLine( e.ToString() );	
				}
				try { 
					// Console.Write(" para el item "+item.Name);
				} catch ( Exception e ) {
					Console.WriteLine( e.ToString() );		
				}
				// Console.WriteLine( "No se encuentra posición de:" + roomInGame.ItemsPositionsStatus[auxPosition.Id] );
				if ( item.FitInPosition( auxPosition ) && roomInGame.ItemsPositionsStatus[auxPosition.Id] == -1 ) {
				    // Console.WriteLine("Posición para "+item.Name+" es "+auxPosition.Id);
					return auxPosition.Id;
				}
			}
			return -1;
		}
		
		public int GetCoinPositionInRoomFor ( LI_RoomInGame roomInGame ) {
		
			List <int> auxShufflePositions = ListUtils.Shuffle<int>( roomInGame.Room.CoinsPositions );
			for ( int i = 0; i < auxShufflePositions.Count; i++ ) {
				if  ( roomInGame.CoinsPositionsStatus[auxShufflePositions[i]] == -1 ) {
					return auxShufflePositions[i];
				}
			}
			return -1;
		}
		
		public LI_Room GetRoomByPositionId( int positionId ) {
		
			for ( int i = 0; i < rooms.Count; i++ ) {
				if ( rooms[i].ItemsPositions.Contains( positionId ) ) {
					return rooms[i];	
				}
			}
			
			return null;
		}

	}
	
}

