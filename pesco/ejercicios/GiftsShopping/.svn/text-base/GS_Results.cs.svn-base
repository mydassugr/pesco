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
using System.Xml;
using System.Xml.Serialization;
using System;
using System.Collections.Generic;

namespace pesco
{


	public class SingleResultGiftsShopping : SingleResultElement
	{

		private List <GS_Criteria> criterias;
		private List <GS_Item> shopping;
		private List <string> criteriasText = new List<string>();
		private List <string> itemsGoal = new List<string>();
		private List <string> shoppingName = new List<string>();
		
		private int maxBudget;
		private int usedBudget;
		private int timeElapsed;
		private int similarity;
		private int corrects;
		
		[XmlElement("CriteriasText")]
		public List<string> CriteriasText {
			get {
				return this.criteriasText;
			}
			set {
				criteriasText = value;
			}
		}
		
		[XmlElement("ItemsGoal")]
		public List<string> ItemsGoal {
			get {
				return this.itemsGoal;
			}
			set {
				itemsGoal = value;
			}
		}
		
		[XmlElement("ShoppingName")]
		public List<string> ShoppingName {
			get {
				return this.shoppingName;
			}
			set {
				shoppingName = value;
			}
		}

		[XmlIgnore]
		public List<GS_Criteria> Criterias {
			get {
				return criterias;
			}
			set {
				criterias = value;
			}
		}

		[XmlIgnore]		
		public List<GS_Item> Shopping {
			get {
				return shopping;
			}
			set {
				shopping = value;
			}
		}
		
		[XmlElement("UsedBudget")]
		public int UsedBudget {
			get {
				return usedBudget;
			}
			set {
				usedBudget = value;
			}
		}
		
		[XmlElement("MaxBudget")]
		public int MaxBudget {
			get {
				return maxBudget;
			}
			set {
				maxBudget = value;
			}
		}
		
		[XmlElement("TimeElapsed")]
		public int TimeElapsed {
			get {
				return timeElapsed;
			}
			set {
				timeElapsed = value;
			}
		}

		[XmlElement("Similarity")]
		public int Similarity {
			get {
				return similarity;
			}
			set {
				similarity = value;
			}
		}

		[XmlElement("Corrects")]
		public int Corrects {
			get {
				return corrects;
			}
			set {
				corrects = value;
			}
		}
		
		public SingleResultGiftsShopping ()
		{
		}
		
		public SingleResultGiftsShopping (  List <GS_Criteria> criterias, List <GS_Item> shopping, int maxBudget, int usedBudget, int timeElapsed, int similarity, int corrects) {
					
			Criterias = criterias;
			for ( int i = 0; i < criterias.Count; i++ ) {
				CriteriasText.Add( criterias[i].Text );	
				GS_Item auxItem = ExerciseGiftsShopping.getInstance().itemManager.GetItem( criterias[i].Item );
				ItemsGoal.Add ( auxItem.Name );					
			}
			Shopping = shopping;
			for ( int i = 0; i < Shopping.Count; i++ ) {
				shoppingName.Add( Shopping[i].Name );	
			}			

			MaxBudget = maxBudget;
			UsedBudget = usedBudget;
			TimeElapsed = timeElapsed;
			Similarity = similarity;
			Corrects = corrects;			
			
		}
	}
	
	public class GiftsShoppingResults : ExerciseResults 
	{
		
		List <ExerciceExecutionResult<SingleResultGiftsShopping>> giftsShoppingExecutionResults;
		
		[XmlElement("GiftsShoppingExecutionResults")]
		public List<ExerciceExecutionResult<SingleResultGiftsShopping>> GiftsShoppingExecutionResults {
			get {return this.giftsShoppingExecutionResults;}
			set {giftsShoppingExecutionResults = value;}
		}
		
		public GiftsShoppingResults ()
		{
			giftsShoppingExecutionResults = new List<ExerciceExecutionResult<SingleResultGiftsShopping>>();
		}
		
		public void setResult( List <GS_Criteria> criterias, List <GS_Item> shopping, int maxBudget, int usedBudget, int timeUsed, int similarity, int corrects)
		{
			SingleResultGiftsShopping re = new SingleResultGiftsShopping( criterias, shopping, maxBudget, 
			                                                        usedBudget, timeUsed, similarity, corrects);
			GiftsShoppingExecutionResults[GiftsShoppingExecutionResults.Count-1].SingleResults.Add(re);
        }
		
		public void setResult(SingleResultGiftsShopping re)
		{
			GiftsShoppingExecutionResults[GiftsShoppingExecutionResults.Count-1].SingleResults.Add(re);
        }
		
		public override string ToString ()
		{
			return null;
		}
		
		[XmlIgnore]		
		public ExerciceExecutionResult<SingleResultGiftsShopping> CurrentExecution {		
			get { 
				if ( GiftsShoppingExecutionResults.Count > 0 ) {
					return GiftsShoppingExecutionResults[GiftsShoppingExecutionResults.Count - 1];
				} else {
					return null;	
				}
			}
			set {
				GiftsShoppingExecutionResults[GiftsShoppingExecutionResults.Count - 1] = value;	
			}			
		}
		 
	}
}

