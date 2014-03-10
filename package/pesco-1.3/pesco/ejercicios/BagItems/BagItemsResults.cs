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

	public class SingleResultBagItems : SingleResultElement
	{
		private List <string> itemsSelected = new List<string>();
		private List <string> itemsInBag = new List<string>();
		private int corrects;
		private int semicorrects;
		private int omissions;
		private int fails;
		private int score;

		public List<string> ItemsSelected {
			get {
				return itemsSelected;
			}
			set {
				itemsSelected = value;
			}
		}
		
		public List<string> ItemsInBag {
			get {
				return itemsInBag;
			}
			set {
				itemsInBag = value;
			}
		}
				
		public int Corrects {
			get {
				return corrects;
			}
			set {
				corrects = value;
			}
		}		
		
		public int Semicorrects {
			get {
				return semicorrects;
			}
			set {
				semicorrects = value;
			}
		}
				
		public int Omissions {
			get {
				return omissions;
			}
			set {
				omissions = value;
			}
		}
				
		public int Fails {
			get {
				return fails;
			}
			set {
				fails = value;
			}
		}
		
		public int Score {
			get {
				return score;
			}
			set {
				score = value;
			}
		}
		
		public SingleResultBagItems ()
		{		
		}
		
		public SingleResultBagItems ( List <string> itemsinbag, List <string> itemsselected, int corrects,
		                   int semicorrects, int omissions, int fails, int score ) {

			ItemsInBag = itemsinbag;
			ItemsSelected = itemsselected;			
			Corrects = corrects;
			Semicorrects = semicorrects;
			Omissions = omissions;
			Fails = fails;
			Score = score;
			
		}
	}
	
	public class BagItemsResults : ExerciseResults 
	{
		
		List <ExerciceExecutionResult<SingleResultBagItems>> bagItemsExecutionResults;
		
		[XmlElement("BagItemsExecutionResults")]
		public List<ExerciceExecutionResult<SingleResultBagItems>> BagItemsExecutionResults {
			get {return this.bagItemsExecutionResults;}
			set {bagItemsExecutionResults = value;}
		}
		
		public BagItemsResults ()
		{
			bagItemsExecutionResults = new List<ExerciceExecutionResult<SingleResultBagItems>>();
		}
		
		public void setResult(string showedValueArg,string userValueArg,int levelArg,string resultArg,int timeElapsedArg)
		{
			/* SingleResultBagItems re = new SingleResultBagItems( itemsinbag, itemsselected, corrects,
		                   semicorrects, omissions, fails, score );*/
			// BagItemsExecutionResults[BagItemsExecutionResults.Count-1].SingleResults.Add(re);
        }
		
		public void setResult(SingleResultBagItems re)
		{
			if ( BagItemsExecutionResults.Count == 0 ) {
				bagItemsExecutionResults = new List<ExerciceExecutionResult<SingleResultBagItems>>();
				bagItemsExecutionResults.Add( new ExerciceExecutionResult<SingleResultBagItems>() );
			} else if ( SessionManager.GetInstance().CurExecInd != BagItemsExecutionResults[BagItemsExecutionResults.Count-1].SessionId ) {
				bagItemsExecutionResults.Add( new ExerciceExecutionResult<SingleResultBagItems>() );
			}
			BagItemsExecutionResults[BagItemsExecutionResults.Count-1].SingleResults.Add(re);
        }
		
		public override string ToString ()
		{
			return null;
		}

		 
	}
}

