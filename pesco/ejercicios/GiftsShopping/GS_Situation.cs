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


	public class GS_Situation
	{

		private List <GS_Criteria> criterias = new List<GS_Criteria>();
		private List <GS_Shop> shops = new List<GS_Shop>();
		private List <GS_Item> shoppingcart = new List<GS_Item>();		
		private int budget;
		private int budgetused;
		private int timeUsed = 0;
		
		public List<GS_Shop> Shops {
			get {
				return shops;
			}
			set {
				shops = value;
			}
		}
		
		
		public List<GS_Item> Shoppingcart {
			get {
				return shoppingcart;
			}
			set {
				shoppingcart = value;
			}
		}
		
		
		public List<GS_Criteria> Criterias {
			get {
				return criterias;
			}
			set {
				criterias = value;
			}
		}
		
		
		public int Budgetused {
			get {
				return budgetused;
			}
			set {
				budgetused = value;
			}
		}
		
		
		public int Budget {
			get {
				return budget;
			}
			set {
				budget = value;
			}
		}
		
		public int TimeUsed {
			get {
				return timeUsed;
			}
			set {
				timeUsed = value;
			}
		}
		
		
		
		
		public GS_Situation ()
		{
		}
		
		public GS_Situation ( List<GS_Criteria> criterias, List<GS_Shop> shops, int budget) 
		{
			Criterias = criterias;
			Shops = shops;
			Budget = budget;			
		}
	}
}

