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


	public class LI_Position
	{
		#region Variables
		private int id;	
		private int x;
		private int y;
		private int width;
		private int height;
		private List<string> tags;

		public int Id {
			get {
				return this.id;
			}
			set {
				id = value;
			}
		}
	
		public int X {
			get {
				return this.x;
			}
			set {
				x = value;
			}
		}

		public int Y {
			get {
				return this.y;
			}
			set {
				y = value;
			}
		}
		public int Width {
			get {
				return this.width;
			}
			set {
				width = value;
			}
		}

		public int Height {
			get {
				return this.height;
			}
			set {
				height = value;
			}
		}

		public List<string> Tags {
			get {
				return this.tags;
			}
			set {
				tags = value;
			}
		}
		
		#endregion
		
		public LI_Position ()
		{
		}
		
		public LI_Position ( int x, int y) {
			this.x = x;
			this.y = y;
		}
		
		public LI_Position ( int id, int x, int y) {
			this.id = id;
			this.x = x;
			this.y = y;
		}

		public LI_Position ( int id, int x, int y, int width, int height) {
			this.id = id;
			this.x = x;
			this.y = y;
			this.width = width;
			this.height = height;
		}
		
	}
}

