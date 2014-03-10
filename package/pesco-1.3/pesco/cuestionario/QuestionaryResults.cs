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
using System.Collections.Generic;
using System.IO;

namespace pesco
{
	public class QuestionResult{
		
		public string enunciado;
		public string respuesta;
		public int puntuacion;
		public int puntuacionMax;
		
		[XmlElement("enunciado")]
		public string Enunciado {
			get {return this.enunciado;}
			set {enunciado = value;}
		}

		[XmlElement("respuesta")]
		public string Respuesta {
			get {return this.respuesta;}
			set {respuesta = value;}
		}
		
		[XmlElement("puntuacion")]
		public int Puntuacion {
			get {return this.puntuacion;}
			set {puntuacion = value;}
		}

		[XmlElement("puntuacionMax")]
		public int PuntuacionMax {
			get {return this.puntuacionMax;}
			set {puntuacionMax = value;}
		}
		
		public QuestionResult(){
		}
		public QuestionResult(string enunciadoArg, string respuestaArg, int puntuacionArg, int puntuacionMaxArg){
			enunciado= enunciadoArg;
			respuesta= respuestaArg;
			puntuacion= puntuacionArg;
			puntuacionMax= puntuacionMaxArg;
		}
	}
	[XmlRoot("QuestionaryResults")]
	public class QuestionaryResults
	{
		
		public string questionaryFile;
		protected List<QuestionResult> questionsResults;
		
		[XmlElement("QuestionaryFile")]
		public string QuestionaryFile {
			get {return this.questionaryFile;}
			set {questionaryFile = value;}
		}
		
		[XmlElement("QuestionsResults")]
		public List<QuestionResult> QuestionsResults {
			get {return this.questionsResults;}
			set {questionsResults = value;}
		}

		public QuestionaryResults ()
		{
		}
		
		public QuestionaryResults (string questionaryFileArg)
		{
			questionaryFile= questionaryFileArg;
			QuestionsResults = new List<QuestionResult>();
		}
		
		
		public void Serialize(){
			
			string path = Configuration.Current.GetQuestionaryConfigurationFolderPath() + Path.DirectorySeparatorChar + questionaryFile;
			//Console.WriteLine("guardando xml del cuestionario en  " + path);
									
			XmlTextWriter escritor = new XmlTextWriter(path, null);
		
			try
			{
				escritor.Formatting = Formatting.Indented;
				
				escritor.WriteStartDocument();
				
				escritor.WriteDocType("resultados-cuestionario", null, null, null);
				
				// hoja de estilo para pode ver en un navegador el xml
				escritor.WriteProcessingInstruction("xml-stylesheet", "type='text/xsl' href='cuestionario.xsl'");
				
				XmlSerializer serializer = new XmlSerializer(typeof(QuestionaryResults));
				
				XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
				
				namespaces.Add("","");
				
				serializer.Serialize(escritor, this, namespaces);
				
				escritor.WriteEndDocument();
				escritor.Close();
			}
			catch(Exception e)
			{
				escritor.Close();
				
				Console.WriteLine("Error al serializar" +  e.Message);
			}
		}
		
		public static QuestionaryResults Deserialize(string filename)
		{ 
			
			string path = Configuration.Current.GetQuestionaryConfigurationFolderPath() + Path.DirectorySeparatorChar + filename;
			
			if (!File.Exists(path))
			{
				return new QuestionaryResults(filename);
			}
			
			// Console.WriteLine("Intentamos abrir el fichero " + path);
			
			XmlTextReader lector = new XmlTextReader(path);
			try
			{
				QuestionaryResults c = new QuestionaryResults();
				
				XmlSerializer serializer = new XmlSerializer(typeof(QuestionaryResults));				
				c = (QuestionaryResults) serializer.Deserialize(lector);
				
				lector.Close();				
				return c;
			}
			catch( Exception e)
			{
				Console.WriteLine("Error al deserializar " +  e.Message);
				lector.Close();
				return null;
			}
		}
	}
}


