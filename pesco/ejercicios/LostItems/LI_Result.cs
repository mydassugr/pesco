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

using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace pesco
{
	
	public class LI_ResultAction {
	
		private string action;
		private string item;
		private string room;
		
		public string Action {
			get {
				return this.action;
			}
			set {
				action = value;
			}
		}

		public string Item {
			get {
				return this.item;
			}
			set {
				item = value;
			}
		}

		public string Room {
			get {
				return this.room;
			}
			set {
				room = value;
			}
		}
		
		public LI_ResultAction( string action, string item, string room ) {
		
			this.action = action;
			this.item = item;
			this.room = room;
		}
		
		public LI_ResultAction() {}
	}	
	
	public class LI_ResultItemRoom {
	
		private string item;
		private string room;
		
		public string Item {
			get {
				return this.item;
			}
			set {
				item = value;
			}
		}

		public string Room {
			get {
				return this.room;
			}
			set {
				room = value;
			}
		}
		
		public LI_ResultItemRoom( string item, string room ) {
			this.item = item;
			this.room = room;
		}
		
		public LI_ResultItemRoom() {}
	}
	
	public class LI_ResultCoinRoom {
		
	}
	
	public class SingleResultLostItems : SingleResultElement
	{
		
		private int executionId;
		private int timeElapsed;
		private DateTime start;
		private DateTime end;
		private int level;
		private int subLevel;
		private List<string> roomsSelected;
		private int itemsCounterBegin;
		private int itemsCounterEnd;
		private List <LI_ResultItemRoom> itemsAtBegin;
		private List <LI_ResultItemRoom> itemsAtEnd;
		private int coinsBegin;
		private int coinsEnd;
		private List <LI_ResultAction> actions;
				
		public int ExecutionId {
			get {
				return this.executionId;
			}
			set {
				executionId = value;
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

		public int SubLevel {
			get {
				return this.subLevel;
			}
			set {
				subLevel = value;
			}
		}

		public DateTime End {
			get {
				return this.end;
			}
			set {
				end = value;
			}
		}

		public DateTime Start {
			get {
				return this.start;
			}
			set {
				start = value;
			}
		}
		
		public int TimeElapsed {
			get {
				return this.timeElapsed;
			}
			set {
				timeElapsed = value;
			}
		}
		
		public List<string> RoomsSelected {
			get {
				return this.roomsSelected;
			}
			set {
				roomsSelected = value;
			}
		}	
				
		public int ItemsCounterBegin {
			get {
				return this.itemsCounterBegin;
			}
			set {
				itemsCounterBegin = value;
			}
		}

		public int ItemsCounterEnd {
			get {
				return this.itemsCounterEnd;
			}
			set {
				itemsCounterEnd = value;
			}
		}
		
		public List <LI_ResultItemRoom> ItemsAtBegin {
			get {
				return this.itemsAtBegin;
			}
			set {
				itemsAtBegin = value;
			}
		}

		public List <LI_ResultItemRoom> ItemsAtEnd {
			get {
				return this.itemsAtEnd;
			}
			set {
				itemsAtEnd = value;
			}
		}

		public int CoinsBegin {
			get {
				return this.coinsBegin;
			}
			set {
				coinsBegin = value;
			}
		}

		public int CoinsEnd {
			get {
				return this.coinsEnd;
			}
			set {
				coinsEnd = value;
			}
		}
		
		public List<LI_ResultAction> Actions {
			get {
				return this.actions;
			}
			set {
				actions = value;
			}
		}
		
		public void RegisterEnterRoom( string room )  {
			actions.Add( new LI_ResultAction( "EntrarEnHabitacion", "", room ) );
		}
			            
		public void RegisterLeaveRoom( string room )  {
			actions.Add( new LI_ResultAction( "SalirDeHabitacion", "", room ) );			
		}
		
		public void RegisterSelectItem( string item, string room )  {
			actions.Add( new LI_ResultAction( "SeleccionarObjeto", item, room ) );			
		}
		
		public void RegisterDeselectItem( string item, string room )  {
			actions.Add( new LI_ResultAction( "DeseleccionarObjeto", item, room ) );			
		}
		
		public void RegisterDropItem( string item, string room )  {
			actions.Add( new LI_ResultAction( "ColocarObjeto", item, room ) );			
		}
		
		public void RegisterDropItemFilledPosition( string item, string room )  {
			actions.Add( new LI_ResultAction( "ColocarObjetoEnPosicionOcupada", item, room ) );			
		}		
		
		public void RegisterDropItemSmallerPosition( string item, string room )  {
			actions.Add( new LI_ResultAction( "ColocarObjetoEnPosicionPequeña", item, room ) );			
		}				
		
		public void RegisterPickUpItem( string item, string room )  {
			actions.Add( new LI_ResultAction( "CogerObjeto", item, room ) );			
		}
		
		public void RegisterMaxItemsReached( string item, string room )  {
			actions.Add( new LI_ResultAction( "CogerObjetoConLimiteAlcanzado", item, room ) );			
		}		
		
		public void RegisterCoinFound( string room )  {
			actions.Add( new LI_ResultAction( "MonedaEncontrada", "", room ) );			
		}				
		
		public SingleResultLostItems ()
		{
			roomsSelected = new List<string>();
			itemsAtBegin = new List<LI_ResultItemRoom>();
			itemsAtEnd = new List<LI_ResultItemRoom>();
			actions = new List<LI_ResultAction>();
		}
	}
	
	public class LostItemsResults : ExerciseResults 
	{
		
		private int currentSubLevel;		

		public int CurrentSubLevel {
			get {
				return this.currentSubLevel;
			}
			set {
				currentSubLevel = value;
			}
		}

		List <ExerciceExecutionResult<SingleResultLostItems>> lostItemsExecutionResults;
		
		[XmlElement("LostItemsExecutionResults")]
		public List<ExerciceExecutionResult<SingleResultLostItems>> LostItemsExecutionResults {
			get {return this.lostItemsExecutionResults;}
			set {lostItemsExecutionResults = value;}
		}

		[XmlIgnore]
		public ExerciceExecutionResult<SingleResultLostItems> CurrentExecution {		
			get { 
				if ( lostItemsExecutionResults.Count > 0 ) {
					return lostItemsExecutionResults[lostItemsExecutionResults.Count - 1];
				} else {
					return null;	
				}
			}
			set {
				lostItemsExecutionResults[lostItemsExecutionResults.Count - 1] = value;	
			}			
		}
	
		public LostItemsResults ()
		{
			lostItemsExecutionResults = new List<ExerciceExecutionResult<SingleResultLostItems>>();
		}
		
		/* public void setResult( int corrects, int fails, int omissions )
		{
			SingleResultPyramids re = new SingleResultPyramids( corrects, fails, omissions );
			PyramidsExecutionResults[PyramidsExecutionResults.Count-1].SingleResults.Add(re);
        }*/
		
		public void setResult(SingleResultLostItems re)
		{
			LostItemsExecutionResults[LostItemsExecutionResults.Count-1].SingleResults.Add(re);
        }
		
		public override string ToString ()
		{
			return null;
		}

		public void ResetExecution() {
			
			this.CurrentExecution.TotalCorrects = 0;
			this.CurrentExecution.TotalFails = 0;
			this.CurrentExecution.TotalOmissions = 0;
			this.CurrentExecution.TotalTimeElapsed = 0;

		}
		 
	}
}


