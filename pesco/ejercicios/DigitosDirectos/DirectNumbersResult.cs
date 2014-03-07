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


	public class DirectNumbersResult
	{
		int level;
		string showedvalue;
		string uservalue;
		string result;
		string time;
		
		[XmlElement("time")]
		public string Time {
	        get { return time; }
	        set { time = value; }
		}
		
		[XmlElement("level")]		
		public int Level {
			get {
				return level;
			}
			set {
				level = value;
			}
		}
		
		[XmlElement("uservalue")]		
		public string Uservalue {
			get {
				return uservalue;
			}
			set {
				uservalue = value;
			}
		}
		
		[XmlElement("showedvalue")]
		public string Showedvalue {
			get {
				return showedvalue;
			}
			set {
				showedvalue = value;
			}
		}
		
		[XmlElement("result")]		
		public string Result {
			get {
				return result;
			}
			set {
				result = value;
			}
		}
		
		public DirectNumbersResult ()
		{
		}
		
		public DirectNumbersResult ( int level, string showed, string user, string result, TimeSpan t)
		{
			this.level = level;
			this.Showedvalue = showed;
			this.Uservalue = user;
			this.Result = result;
			this.Time = t.ToString();
		}
	}
}

