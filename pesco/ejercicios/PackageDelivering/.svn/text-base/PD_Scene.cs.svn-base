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

namespace pesco
{
	
	public class PD_Scene
	{

		private int [] places= new int[16];
		private int [][] conections = new int[16][];
		private PD_BoxStatus [] boxesStatus = new PD_BoxStatus[4];
		
		public int[] Places {
			get {
				return places;
			}
			set {
				places = value;
			}
		}
		
		
		public int[][] Conections {
			get {
				return conections;
			}
			set {
				conections = value;
			}
		}
		
		
		public PD_BoxStatus[] BoxesStatus {
			get {
				return boxesStatus;
			}
			set {
				boxesStatus = value;
			}
		}		
		
		public PD_Scene ()
		{			
		}
		
		public PD_Scene ( int[] places, int [][] conections, PD_BoxStatus [] boxesStatus ) {
		
			Places = places;
			Conections = conections;
			BoxesStatus = boxesStatus;
			
		}
	}
}

