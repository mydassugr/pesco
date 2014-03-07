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
using System.IO;
using System;
using System.Collections.Generic;

namespace pesco
{


	public class GS_Shop
	{
		private int id;
		private string name;
		private string img;
		private int itemsInShop = 9;
		private List <GS_Item> items = new List<GS_Item>();
		
		[XmlElement("name")]
		public string Name {
			get {
				return name;
			}
			set {
				name = value;
			}
		}
		
		[XmlElement("img")]
		public string Img {
			get {
				return img;
			}
			set {
				img = value;
			}
		}
		
		[XmlElement("id")]
		public int Id {
			get {
				return id;
			}
			set {
				id = value;
			}
		}
		
		[XmlIgnore]
		public List<GS_Item> Items {
			get {
				return items;
			}
			set {
				items = value;
			}
		}
		
		
		public GS_Shop(){}
		
		public GS_Shop (int id, string name, string img)
		{
			
			Id = id;
			Name = name;
			Img = img;
		}
		
		public void FillShopBySimilarity( List <GS_Item> itemsAvalaible, GS_Item itemTarget ) {

			// Calculate the similarity for each items in the avalaible list
			for ( int i = 0; i < itemsAvalaible.Count; i++ ) {			
				itemsAvalaible[i].Similarity = itemsAvalaible[i].GetSimilarity( itemTarget );				
			}
			
			itemsAvalaible.Sort( delegate ( GS_Item it1, GS_Item it2 ) {
				return Comparer<double>.Default.Compare( it1.Similarity, it2.Similarity );
			});
			itemsAvalaible.Reverse();
			
			for ( int i = 0; i < itemsAvalaible.Count && Items.Count < itemsInShop; i++ ) {		
				
				if ( itemsAvalaible[i].GetSimilarity( itemTarget ) == 1 )
					continue;
				
				GS_Item auxItem = new GS_Item(itemsAvalaible[i]);

				Random r = new Random(DateTime.Now.Millisecond);
				if ( r.Next(0,2) == 0 ) {
					auxItem.FinalPrice = auxItem.CheapPriceMin + 
						r.Next( 0, (auxItem.CheapPriceMax - auxItem.CheapPriceMin) / 5 ) * 5;
				} else {
					auxItem.FinalPrice = auxItem.ExpensivePriceMin + 
						r.Next( 0, (auxItem.ExpensivePriceMax - auxItem.ExpensivePriceMin) / 5 ) * 5;
				}
				
				Items.Add( auxItem );
			}
			Items = ListUtils.Shuffle( Items );
		}
		
		public void FillShop( List <GS_Item> itemsAvalaible ) {
		
			for ( int i = 0; i < itemsAvalaible.Count && Items.Count < itemsInShop; i++ ) {
				GS_Item auxItem = itemsAvalaible[i];
				Random r = new Random(DateTime.Now.Millisecond);
				if ( r.Next(0,2) == 0 ) {
					auxItem.FinalPrice = auxItem.CheapPriceMin + 
						( r.Next( 0, (auxItem.CheapPriceMax - auxItem.CheapPriceMin) / 5 ) * 5 );
				} else {
					auxItem.FinalPrice = auxItem.ExpensivePriceMin + 
						( r.Next( 0, (auxItem.ExpensivePriceMax - auxItem.ExpensivePriceMin) / 5 ) * 5 );
				}
				Items.Add( auxItem );			
			}			
		}
	}
}

