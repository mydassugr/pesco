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
using System.Linq;

namespace pesco
{
	public class BO_Scene
	{
		
		private int level;
		private List <BO_Place> places = new List<BO_Place>();
		private List <string> avalaiblePlaces = new List<string>();
		private List <BO_SituationQuantity> situations = new List<BO_SituationQuantity>();
		private List <int> placesToStop = new List<int>();
		private Dictionary<int, List<BO_SituationQuantity>> situationsInPlaces = new Dictionary<int, List<BO_SituationQuantity>>();
		private Dictionary<string, int> itemsQuantityInGame = new Dictionary<string, int>();
		private Dictionary<BO_Item,int> itemsAtLeavingHome = new Dictionary<BO_Item,int>();
		private Dictionary<BO_Item,int> itemsInBag = new Dictionary<BO_Item, int>();		
		private List <BO_Place> allPlaces;
		private ExerciseBagItems exerBO;
		private int currentPlaceIndex = -1;
		private int currentSituationIndex = 0;
		private bool abueloEnLugar = false;
		private bool homeAlreadyLeft = false;
		private bool somethingLostAlready = false;
		
		public bool AbueloEnLugar {
			get {
				return this.abueloEnLugar;
			}
			set {
				abueloEnLugar = value;
			}
		}

		public bool HomeAlreadyLeft {
			get {
				return this.homeAlreadyLeft;
			}
			set {
				homeAlreadyLeft = value;
			}
		}

		
		
		public int Level {
			get {
				return this.level;
			}
			set {
				level = value;
			}
		}
		
		public int CurrentPlaceIndex {
			get {
				return this.currentPlaceIndex;
			}
			set {
				currentPlaceIndex = value;
			}
		}

		public Dictionary<BO_Item, int> ItemsAtLeavingHome {
			get {
				return this.itemsAtLeavingHome;
			}
			set {
				itemsAtLeavingHome = value;
			}
		}

		public Dictionary<string, int> ItemsQuantityInGame {
			get {
				return this.itemsQuantityInGame;
			}
			set {
				itemsQuantityInGame = value;
			}
		}
		
		public Dictionary<BO_Item, int> ItemsInBag {
			get {
				return this.itemsInBag;
			}
			set {
				itemsInBag = value;
			}
		}

		public Dictionary<int, List<BO_SituationQuantity>> SituationsInPlaces {
			get {
				return this.situationsInPlaces;
			}
			set {
				situationsInPlaces = value;
			}
		}

		public List<int> PlacesToStop {
			get {
				return this.placesToStop;
			}
			set {
				placesToStop = value;
			}
		}

		public List<string> AvalaiblePlaces {
			get {
				return this.avalaiblePlaces;
			}
			set {
				avalaiblePlaces = value;
			}
		}

		public List<BO_Place> Places {
			get {
				return this.places;
			}
			set {
				places = value;
			}
		}

		public BO_Scene ( ExerciseBagItems exerciseInstance )
		{
			this.exerBO = exerciseInstance;
		}
		
		public void InitScene( List <BO_Place> allPlaces ) {
		
			this.allPlaces = allPlaces;
			
			for ( int i = 0; i < this.allPlaces.Count; i++ ) {
				this.AvalaiblePlaces.Add( this.allPlaces[i].Id );
			}
			
		}
		
		public void CreateScene( int level ) {
			
			// Assign level
			Level = level;
			
			// Select items at home
			ChooseObjectsAtHome();

			// Select shops
			SelectFirst4Shops();
			SelectSecond4Shops();
			
			// Select actions
			SelectActions();
				
		}
		
		public void SelectFirst4Shops() {
			
			Random r = new Random( DateTime.Now.Millisecond );
			while ( this.Places.Count < 4 ) {
				int auxRandomPlace = r.Next(0, this.AvalaiblePlaces.Count);
				BO_Place auxPlace;
				for ( int i = 0; i < this.allPlaces.Count; i++ ) {
					if ( this.allPlaces[i].Id == this.AvalaiblePlaces[auxRandomPlace] 
					    && this.allPlaces[i].objetosAccionAgregar.Count > 0 ) {						
						this.Places.Add( allPlaces[i] );		
					}
				}				
				this.AvalaiblePlaces.RemoveAt( auxRandomPlace );
			}
			
		}

		public void SelectSecond4Shops() {
		
			List <BO_Item> auxObjectsInGame = new List<BO_Item>();
			for ( int i = 0; i < this.Places.Count; i++ ) {
				for ( int j = 0; j < this.Places[i].objetosAccionAgregar.Count; j++ ) {
					auxObjectsInGame.Add( this.Places[i].objetosAccionAgregar[j].Objeto );	
				}
			}
						
			Dictionary<BO_Place, int> placesOrdered = new Dictionary<BO_Place, int>();
			for ( int i = 0; i < this.AvalaiblePlaces.Count; i++ ) {
				BO_Place auxPlace = null;
				for ( int k = 0; k < this.allPlaces.Count; k++ ) {
					if ( this.allPlaces[k].Id == this.AvalaiblePlaces[i] ) {
						auxPlace = this.allPlaces[k];
					}
				}				
				placesOrdered.Add( auxPlace, 0 );
				foreach ( BO_ItemActionWho entry in auxPlace.objetosAccionEliminar ) {
					if ( auxObjectsInGame.Contains( entry.Objeto ) ) {
						placesOrdered[auxPlace] = placesOrdered[auxPlace] + 1;
					}					
				}
			}
			
		    var sortedDict = (from entry in placesOrdered orderby entry.Value descending select entry);
			
			for ( int i = 0; i < 4; i++ ) {
				this.Places.Add( sortedDict.ElementAt(i).Key );
			}
			
		}

		public void SelectActions() {
		
			BO_Item lastItem = null;
			
			Random r = new Random( DateTime.Now.Millisecond );
			// Depending of level, 4, 6 or 8 places to stop will be chosen
			// If places to stop is 8, we'll add all the places
			if ( exerBO.placesByLevel[Level] == 8 ) 	{
				for ( int i = 0; i < 8; i++ ) {
					this.PlacesToStop.Add(i);	
				}				
			} 
			// If not, we'll add places from the first and the last places avalaible
			else {
				List <int> auxPlacesIndex = new List<int>();
				for ( int i = 0; i < 8; i++ ) { 
					auxPlacesIndex.Add( i ); 
				}
				while ( this.PlacesToStop.Count < exerBO.placesByLevel[Level] / 2 ) {
					int randomIndex = r.Next( 0, auxPlacesIndex.Count / 2 );
					if ( this.Places[randomIndex].objetosAccionAgregar.Count != 0 ) {
						this.PlacesToStop.Add( auxPlacesIndex[randomIndex] );
						auxPlacesIndex.RemoveAt( randomIndex );
					}
				}					
				while ( this.PlacesToStop.Count < exerBO.placesByLevel[Level] ) {
					int randomIndex = r.Next( auxPlacesIndex.Count / 2, auxPlacesIndex.Count );	
					this.PlacesToStop.Add( auxPlacesIndex[randomIndex] );
					auxPlacesIndex.RemoveAt( randomIndex );
				}
			}
				
			// Ordering places
			this.PlacesToStop = (from number in this.PlacesToStop
    			orderby number ascending
    			select number).ToList();
			
			// Select actions of kind "to add"
			for ( int i = 0; i < this.PlacesToStop.Count; i++ ) {
				// Console.WriteLine("Lugar "+i+" que corresponde a "+currentScene.Places[currentScene.PlacesToStop[i]].Nombre );
				if ( this.Places[this.PlacesToStop[i]].objetosAccionAgregar.Count > 0 ) {
					// Choose a random action
					int auxSituation = r.Next( 0, this.Places[this.PlacesToStop[i]].objetosAccionAgregar.Count );
					// Choose a random quantity, and check if it is possible to add this quantity of items
					// Wait... are there 3 units of this item already? (Maybe taking 3 coins at home and trying to get a new one somewhere)
					int auxQuantity = r.Next( 1, 4 );
					if ( this.itemsInBag.ContainsKey( this.Places[this.PlacesToStop[i]].objetosAccionAgregar[auxSituation].Objeto ) ) {
						if ( this.itemsInBag[ this.Places[this.PlacesToStop[i]].objetosAccionAgregar[auxSituation].Objeto ] == 3 ) {
							// In this case, abort adding this action
							continue;
						} else {
							// If there aren't 3, we have to check if current quantity plus random quantity does not exceed 3
							// And if 3 is exceeded, we have to choose a new random quantity
							while ( (this.itemsInBag[ this.Places[this.PlacesToStop[i]].objetosAccionAgregar[auxSituation].Objeto ] + auxQuantity) > 3 ) {
								auxQuantity = r.Next( 1, 4 );	
							}
						}
					}
					// Create a new list of actions for this place
					List <BO_SituationQuantity> auxList = new List<BO_SituationQuantity>();				
					auxList.Add(new BO_SituationQuantity( this.Places[this.PlacesToStop[i]].objetosAccionAgregar[auxSituation], auxQuantity ) );
					this.SituationsInPlaces.Add( this.PlacesToStop[i], auxList );				
					// Item exists already in game/bag? If not, add it
					if ( this.ItemsQuantityInGame.ContainsKey( this.Places[this.PlacesToStop[i]].objetosAccionAgregar[auxSituation].Objeto.Id ) ) {
						this.ItemsQuantityInGame[this.Places[this.PlacesToStop[i]].objetosAccionAgregar[auxSituation].Objeto.Id] += auxQuantity;
						this.ItemsInBag[this.Places[this.PlacesToStop[i]].objetosAccionAgregar[auxSituation].Objeto] += auxQuantity;
					} else {
						this.ItemsQuantityInGame.Add( this.Places[this.PlacesToStop[i]].objetosAccionAgregar[auxSituation].Objeto.Id, auxQuantity );
						this.ItemsInBag.Add( this.Places[this.PlacesToStop[i]].objetosAccionAgregar[auxSituation].Objeto, auxQuantity );
					}
					lastItem = this.Places[this.PlacesToStop[i]].objetosAccionAgregar[auxSituation].Objeto;
				}
				// Select actions of kind "to remove"
				if ( this.Places[this.PlacesToStop[i]].objetosAccionEliminar.Count > 0 ) {
					int auxSituation = r.Next( 0, this.Places[this.PlacesToStop[i]].objetosAccionEliminar.Count );
					BO_ItemActionWho auxItemActionWho = this.Places[this.PlacesToStop[i]].objetosAccionEliminar[auxSituation];
					// Console.WriteLine("Buscando si puedo devolver del objeto: "+auxItemActionWho.Objeto.NombreSimple);
					if ( this.ItemsQuantityInGame.ContainsKey(auxItemActionWho.Objeto.Id) ) {
						// Console.WriteLine("Del objeto "+auxItemActionWho.Objeto.NombreSimple+" hay "+currentScene.ItemsQuantityInGame[auxItemActionWho.Objeto.Id]+" elementos");
						int auxMaxQuantity = this.ItemsQuantityInGame[auxItemActionWho.Objeto.Id];
						if ( auxMaxQuantity > 0 ) {
							int auxQuantity = r.Next( 1, auxMaxQuantity );
							BO_SituationQuantity auxSituationQuantity = new BO_SituationQuantity( auxItemActionWho, auxQuantity );
							// Console.WriteLine("Añadiendo devolución de "+auxItemActionWho.Objeto.NombreSimple+", cantidad: "+auxQuantity);
							if ( this.SituationsInPlaces.ContainsKey(this.PlacesToStop[i]) ) {
								this.SituationsInPlaces[this.PlacesToStop[i]].Add( auxSituationQuantity );
							} else {
								List <BO_SituationQuantity> auxList = new List<BO_SituationQuantity>();
								auxList.Add(new BO_SituationQuantity( this.Places[this.PlacesToStop[i]].objetosAccionEliminar[auxSituation], auxQuantity ) );
								this.SituationsInPlaces.Add( this.PlacesToStop[i], auxList);
							}
							this.ItemsQuantityInGame[auxItemActionWho.Objeto.Id] -= auxQuantity; 
							this.itemsInBag[auxItemActionWho.Objeto] -= auxQuantity;
						}
					}
				}
				// Select actions of kind "to lost"
				// In level 0 (demo level) we'll stop always
				if ( ( r.Next(0, 4)  >= 2 || ( exerBO.PanelBolsaObjetos.CurrentMode == "demo" && somethingLostAlready == false ) )  && this.itemsInBag.Keys.Count > 0 ) {
					somethingLostAlready = true;
					int randomItemIndex = r.Next(0, this.ItemsInBag.Keys.Count);
					while ( lastItem == itemsInBag.Keys.ElementAt(randomItemIndex) ) {
						randomItemIndex = r.Next(0, this.ItemsInBag.Keys.Count);
					}
					BO_Item randomItem = ItemsInBag.Keys.ElementAt(randomItemIndex);
					// Console.WriteLine( "El objeto escogido es: "+randomItem.NombreSimple );
					int auxMaxQuantity = this.ItemsInBag[randomItem];
					// Console.WriteLine( "Hay "+auxMaxQuantity+" unidades de este objeto" );
					if ( auxMaxQuantity > 0 ) {
						int auxQuantity = r.Next( 1, auxMaxQuantity );
							BO_ItemActionWho auxItemActionWho = new BO_ItemActionWho( randomItem, new BO_Action("perder", "pierdo", "eliminar"), new BO_Who());
							BO_SituationQuantity auxSituationQuantity = new BO_SituationQuantity( auxItemActionWho , auxQuantity );
							if ( this.SituationsInPlaces.ContainsKey(this.PlacesToStop[i]) ) {
								this.SituationsInPlaces[this.PlacesToStop[i]].Add( auxSituationQuantity );
							} else {
								List <BO_SituationQuantity> auxList = new List<BO_SituationQuantity>();
								auxList.Add( auxSituationQuantity );
								this.SituationsInPlaces.Add( this.PlacesToStop[i], auxList);
							}
							this.ItemsQuantityInGame[auxItemActionWho.Objeto.Id] -= auxQuantity;
							this.itemsInBag[auxItemActionWho.Objeto] -= auxQuantity;
					}
				}
			}
			
		}		
		
		public void ChooseObjectsAtHome() {
			
			Random r = new Random( DateTime.Now.Millisecond );
			int auxRandom;
			
			exerBO.itemsAvalaibleAtHome = ListUtils.MixList( exerBO.itemsAvalaibleAtHome );

			for ( int i = 0; i < exerBO.itemsAvalaibleAtHome.Count; i++ ) {
				
				// If we have reached the limit of 6 objects at beggining we don't add more objects
				if ( ( exerBO.PanelBolsaObjetos.CurrentMode == "game" && exerBO.CurrentLevel == 1 && this.objectsAtHomeCount() >= 3 ) 
				    || ( exerBO.PanelBolsaObjetos.CurrentMode == "game" && exerBO.CurrentLevel == 2 && this.objectsAtHomeCount() >= 4 )
				    || ( exerBO.PanelBolsaObjetos.CurrentMode == "game" && exerBO.CurrentLevel == 3 && this.objectsAtHomeCount() == 6 )
				    || ( exerBO.PanelBolsaObjetos.CurrentMode == "demo" && this.objectsAtHomeCount() >= 2 ) )
					break;
				
				// We choose to add or not the current item randomly ( 0 = no, >1 = yes )
				auxRandom = r.Next(0,3);
				// If we choose no, we have to check if we can still filling the bag with at least
				// 2 items. If not, we force to add the item.
				/*if ( auxRandom > 4 && 
				    	( this.objectsAtHomeCount() + (exerBO.itemsAvalaibleAtHome.Count - 1 - i) >= 2)
				   	)
					continue;
				*/
				// If the current item is "coin" we can add more than one
				if ( exerBO.itemsAvalaibleAtHome[i].Id != "moneda" ) {
					itemsAtLeavingHome.Add( exerBO.itemsAvalaibleAtHome[i], 1 );
					itemsInBag.Add( exerBO.itemsAvalaibleAtHome[i], 1 );
					itemsQuantityInGame.Add( exerBO.itemsAvalaibleAtHome[i].Id, 1 );
				} else {
					auxRandom = r.Next(1,3);
					while ( this.objectsAtHomeCount() + auxRandom > 6 ) {
						auxRandom = r.Next(1,3);	
					}						
					itemsAtLeavingHome.Add( exerBO.itemsAvalaibleAtHome[i], auxRandom );
					itemsInBag.Add( exerBO.itemsAvalaibleAtHome[i], auxRandom );
					itemsQuantityInGame.Add( exerBO.itemsAvalaibleAtHome[i].Id, auxRandom );
				}
			}
			
		}	
		
		public int objectsAtHomeCount() {
		
			int auxCount = 0;
			foreach(KeyValuePair<BO_Item,int> entry in this.ItemsAtLeavingHome)
			{
			  auxCount += entry.Value;
			}
			return auxCount;
			
		}
						
		public bool ObjectInBag ( BO_Item bagObject, Dictionary<BO_Item, int> bag ) {
		
			foreach(KeyValuePair<BO_Item,int> auxEntry in bag)
			{
				if ( ((BO_Item) auxEntry.Key).Id == bagObject.Id )
					return true;
			}
			
			return false;
		}

		public BO_Place CurrentPlace() {
			return Places[CurrentPlaceIndex];
		}			

		public int CurrentSituationIndex {
			get {
				return this.currentSituationIndex;
			}
			set {
				currentSituationIndex = value;
			}
		}

		public List <BO_SituationQuantity> SituationsAtPlace() {
			
			return null;
		}
		
		public void abueloLlegaALugar( bool force ) {
			
			CurrentPlaceIndex++;			
			abueloEnLugar = true;
			
			if ( SituationsInPlaces.ContainsKey(CurrentPlaceIndex) ) {
				for (int i = SituationsInPlaces[CurrentPlaceIndex].Count; i > 0; i-- ) {
					exerBO.situationsQueue.Push( SituationsInPlaces[CurrentPlaceIndex][i-1] );
				}
			}
			
		}

			
		public void Print() {
					
			// Print at home items
			foreach ( KeyValuePair<BO_Item, int> entry in itemsAtLeavingHome ) {
				Console.WriteLine("En casa:"+entry.Key.Id+" cantidad: "+entry.Value);
			}
			
			// Print situations			
			foreach ( KeyValuePair <int, List<BO_SituationQuantity>> entry in SituationsInPlaces ) {
				for ( int i = 0; i < entry.Value.Count; i++ ) {
					Console.WriteLine( "En sitio "+Places[entry.Key].Id+" "+ Places[entry.Key].Nombre+" : ");
					Console.Write( entry.Value[i].ItemActionWho.Accion.Texto+" "+entry.Value[i].ItemActionWho.Objeto.NombreSimple+" "+entry.Value[i].Quantity);
				}
			}
			
		}
	}
}


