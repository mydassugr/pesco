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
using System.Xml;
using System.Xml.Serialization;
using System.IO;


namespace pesco
{
	/// <summary>
	/// 
	/// </summary>
	public class Answer
	{
		/// <summary>
		/// 
		/// </summary>
		protected string text;
		
		/// <summary>
		/// 
		/// </summary>
		protected int score;
		
		/// <summary>
		/// 
		/// </summary>
		protected bool selected;

		/// <summary>
		/// 
		/// </summary>
		[XmlElement("texto")]
		public string Text {
			set { text = value; }
			get { return text; }
		}

		/// <summary>
		/// 
		/// </summary>
		[XmlElement("puntuacion")]
		public int Score {
			set { score = value; }
			get { return score; }
		}

		/// <summary>
		/// 
		/// </summary>
		[XmlElement("seleccionada")]
		public bool Selected {
			set { selected = value; }
			get { return selected; }
		}
	}
}


