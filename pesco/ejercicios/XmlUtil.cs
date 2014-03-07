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


	public class XmlUtil
	{

		public XmlUtil ()
		{
		}
		
		public static XmlReader GetResource(string resourceCode)
		{
			XmlReader xml = null;
			try
			{
				string filePath = resourceCode;
				Stream fileStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(filePath);
				if (fileStream != null)
				{
					xml = XmlReader.Create(fileStream);
				}			
			}
			catch { }
			
			return xml;
		}
		
		public static string getAtributo(XmlReader xmlreader, int atributo) {
		
			xmlreader.MoveToAttribute(atributo);
			return xmlreader.Value;
		
		}
		
		public static void Serialize <T>(T auxClass, string xmlPath)
		{  
			try
			{
				string fullPath = Configuration.CommandDirectory + xmlPath;
				
				XmlTextWriter escritor = new XmlTextWriter( fullPath, null );
				XmlSerializer s = new XmlSerializer(auxClass.GetType());    			
				escritor.Formatting = Formatting.Indented;				
				escritor.WriteStartDocument();				
				escritor.WriteDocType(auxClass.ToString(), null, null, null);
				s.Serialize(escritor, auxClass);
				escritor.WriteEndDocument();
				escritor.Close();	
			}
			catch(Exception e)
			{
				Console.WriteLine("Error al serializar: " +  e.Message);
			}  
		}

		public static void SerializeForUser <T>(T auxClass, string xmlPath)
		{  
			try
			{
				string fullPath = xmlPath;
				
				XmlTextWriter escritor = new XmlTextWriter(fullPath, null);
				XmlSerializer s = new XmlSerializer(auxClass.GetType());
				escritor.Formatting = Formatting.Indented;				
				escritor.WriteStartDocument();				
				escritor.WriteDocType( auxClass.GetType().Name/*"numbers-and-vowels",*/, null, null, null);
				s.Serialize(escritor, auxClass);
				escritor.WriteEndDocument();
				escritor.Close();	
			}
			catch(Exception e)
			{
				Console.WriteLine("Error al serializar: " +  e.Message);
			}  
		}
		
		public static T Deserialize <T> ( string xmlPath ) where T : new() {
			
			T auxT = new T();
			
			string fullPath = Configuration.CommandDirectory + xmlPath;
		    if (File.Exists(fullPath))
		    {
		        StreamReader sr = new StreamReader(fullPath);
		        XmlTextReader xr = new XmlTextReader(sr);
		        XmlSerializer xs = new XmlSerializer( ((Object) auxT).GetType() );
		        object c;
		        if (xs.CanDeserialize(xr))
		        {
		            c = xs.Deserialize(xr);
		            Type t = ((Object) auxT).GetType();
		            PropertyInfo[] properties = t.GetProperties();
		            foreach(PropertyInfo p in properties)
		            {
		                p.SetValue(auxT, p.GetValue(c, null), null);
		            }
		        }
		        xr.Close();
		        sr.Close();
	    	} else {
				Console.WriteLine("File: "+fullPath+" not found.");	
			}
			
			return auxT;
		}

		public static T DeserializeForUser <T> ( string xmlPath ) where T : new() {
			
			T auxT = new T();
			
			string fullPath = xmlPath;
		    if (File.Exists(fullPath))
		    {
		        StreamReader sr = new StreamReader(fullPath);
		        XmlTextReader xr = new XmlTextReader(sr);
		        XmlSerializer xs = new XmlSerializer( ((Object) auxT).GetType() );
		        object c;
		        if (xs.CanDeserialize(xr))
		        {
		            c = xs.Deserialize(xr);
		            Type t = ((Object) auxT).GetType();
		            PropertyInfo[] properties = t.GetProperties();
		            foreach(PropertyInfo p in properties)
		            {
		                p.SetValue(auxT, p.GetValue(c, null), null);
		            }
		        }
		        xr.Close();
		        sr.Close();
	    	} else {
				Console.WriteLine("File: "+fullPath+" not found.");	
			}
			
			return auxT;
		}

	}
}

