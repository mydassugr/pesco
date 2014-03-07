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
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System;

namespace pesco
{


	public abstract class XmlSerializable
	{

		protected string xmlPath;
		
		public virtual void Serialize() {

			try
			{
				string fullPath = Configuration.CommandDirectory + xmlPath;
				
				XmlTextWriter escritor = new XmlTextWriter(fullPath, null);
				XmlSerializer s = new XmlSerializer(this.GetType());    			
				escritor.Formatting = Formatting.Indented;				
				escritor.WriteStartDocument();				
				escritor.WriteDocType(this.ToString(), null, null, null);
				s.Serialize(escritor, this);
				escritor.WriteEndDocument();
				escritor.Close();	
			}
			catch(Exception e)
			{
				Console.WriteLine("Error al serializar: " +  e.Message);
			}
		
		}
		
		public virtual void Deserialize() {
						
			string fullPath = Configuration.CommandDirectory + xmlPath;
		    if (File.Exists(fullPath))
		    {
		        StreamReader sr = new StreamReader(fullPath);
		        XmlTextReader xr = new XmlTextReader(sr);
		        XmlSerializer xs = new XmlSerializer(this.GetType());
		        object c;
		        if (xs.CanDeserialize(xr))
		        {
		            c = xs.Deserialize(xr);
		            Type t = this.GetType();
		            PropertyInfo[] properties = t.GetProperties();
		            foreach(PropertyInfo p in properties)
		            {
						Console.WriteLine( p.GetValue(c, null) );
		                p.SetValue(this, p.GetValue(c, null), null);
		            }
		        }
		        xr.Close();
		        sr.Close();
	    	} else {
				Console.WriteLine("File: "+fullPath+" not found.");	
			}
		}
	}
}
