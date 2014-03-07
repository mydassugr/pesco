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
	public abstract class ReasoningExerciseElement
	{
		/// <summary>
		/// 
		/// </summary>
		public ReasoningExerciseElement ()
		{

		}
		[XmlElement("Position")]
		public abstract int Position
		{
			get; set;
		}
		/// <summary>
		/// 
		/// </summary>
		[XmlElement("Value")]
		public abstract string Value
		{
			get; set;
		}
		
		
		

		/// <summary>
		/// 
		/// </summary>
		/// <returns>
		/// A <see cref="Gtk.Widget"/>
		/// </returns>
		public abstract Gtk.Widget GetWidget();
	}
}


