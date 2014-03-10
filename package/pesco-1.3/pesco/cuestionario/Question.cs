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
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace pesco
{
	/// <summary>
	/// 
	/// </summary>
	public class Question
	{
		protected string question;
		protected string imagen;
		protected List<Answer> answers;
		
		/// <summary>
		/// 
		/// </summary>
		public Question ()
		{
						
		}
	
		/// <summary>
		/// 
		/// </summary>
		[XmlElement("enunciado")]
		public string Question_{
			get {return question;}
			set {question = value;}
		}
		
		[XmlElement("imagen")]
		public string Imagen {
			get {return this.imagen;}
			set {imagen = value;}
		}

		/// <summary>
		/// 
		/// </summary>
		[XmlElement("respuesta")]
		public List<Answer> Answers{
			get {return answers;}	
			set {answers = value;}
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <returns>
		/// A <see cref="Pregunta"/>
		/// </returns>
		public static Question GenerateQuestion(string enun){
			return GenerateQuestion(enun, 4);
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="enun">
		/// A <see cref="System.String"/>
		/// </param>
		/// <param name="numRespuestas">
		/// A <see cref="System.Int32"/>
		/// </param>
		/// <returns>
		/// A <see cref="Pregunta"/>
		/// </returns>
		public static Question GenerateQuestion(string enun, int numRespuestas){
			
			Question p = new Question();
			
			p.question = enun;
			p.answers = new List<Answer>();
			Random random = new Random();

			for (int i=0; i<numRespuestas; ++i){
				Answer r = new Answer();
				r.Text = "respuesta " + i;
				r.Score =  random.Next(10);
				r.Selected = false;
				
				p.answers.Add(r);
			}
			
			return p;
			
		}
	}
}


