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


	public class GS_Item
	{

		private int id;
		private string name;
		private string img;
		private int shop;
		private List <string> tags;
		private int cheapPriceMin;
		private int cheapPriceMax;
		private int expensivePriceMin;
		private int expensivePriceMax;		
		private int finalPrice;
		private double similarity;
		
		[XmlElement("id")]		
		public int Id {
			get {
				return id;
			}
			set {
				id = value;
			}
		}
		
		[XmlElement("tags")]
		public List<string> Tags {
			get {
				return tags;
			}
			set {
				tags = value;
			}
		}
		
		[XmlElement("shop")]		
		public int Shop {
			get {
				return shop;
			}
			set {
				shop = value;
			}
		}
		
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
		
		[XmlElement("expensivepricemin")]
		public int ExpensivePriceMin {
			get {
				return expensivePriceMin;
			}
			set {
				expensivePriceMin = value;
			}
		}
		
		[XmlElement("expensivepricemax")]
		public int ExpensivePriceMax {
			get {
				return expensivePriceMax;
			}
			set {
				expensivePriceMax = value;
			}
		}
		
		[XmlElement("cheappricemin")]
		public int CheapPriceMin {
			get {
				return cheapPriceMin;
			}
			set {
				cheapPriceMin = value;
			}
		}
		
		[XmlElement("cheappricemax")]
		public int CheapPriceMax {
			get {
				return cheapPriceMax;
			}
			set {
				cheapPriceMax = value;
			}
		}						
		
		[XmlIgnore]
		public int FinalPrice {
			get {
				return finalPrice;
			}
			set {
				finalPrice = value;
			}
		}
		
		[XmlIgnore]
		public double Similarity {
			get {
				return similarity;
			}
			set {
				similarity = value;
			}
		}
		
		
		
		public GS_Item() {}
		public GS_Item( GS_Item copy ) {
			
			Id = copy.Id;
			Name = copy.Name;
			Tags = copy.Tags;
			Img = copy.Img;
			Shop = copy.Shop;
			CheapPriceMin = copy.CheapPriceMin;
			CheapPriceMax = copy.CheapPriceMax;
			ExpensivePriceMin = copy.ExpensivePriceMin;
			ExpensivePriceMax = copy.ExpensivePriceMax;
			FinalPrice = copy.FinalPrice;
			Similarity = copy.Similarity;
			
		}
		
		public GS_Item (int id, string name, string img, List <string> tags, int shop,
		                int cheappricemin, int cheappricemax, int expensivepricemin, int expensivepricemax, double similarity )
		{
			Id = id;
			Name = name;
			Tags = tags;
			Shop = shop;
			CheapPriceMin = cheappricemin;
			CheapPriceMax = cheappricemax;
			ExpensivePriceMin = expensivepricemin;
			ExpensivePriceMax = expensivepricemax;
			Similarity = similarity;
		}
		
		public double GetSimilarity( GS_Item itemToCompare ) {
		
			int maxTagsPossible = Math.Min( this.Tags.Count, itemToCompare.Tags.Count );
			double auxSimilarity = 0;
			
			for ( int i = 0; i < this.Tags.Count; i++ ) {
				for ( int j = 0; j < itemToCompare.Tags.Count; j++ ) {
					if ( this.Tags[i] == itemToCompare.Tags[j] ) {
						auxSimilarity += 1.0 / maxTagsPossible;
					}
				}				
			}
			
			this.Similarity = auxSimilarity;
			
			return auxSimilarity;
		}
	}
}

