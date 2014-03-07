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
	[XmlRoot("cuestionario")]
	public class Questionary
	{
		protected List<Question> questions;
		protected QuestionaryResults questionsResults;
		protected int nextQuestion;
		protected string caption;
		public string questionaryFile;
		
		#region Constructors
		
		/// <summary>
		/// 
		/// </summary>
		private Questionary ()
		{
		}
		
		#endregion
		
		#region Properties
		
		/// <summary>
		/// 
		/// </summary>
		[XmlElement("titulo")]
		public string Caption{
			get{return caption;}
			set {caption = value;}
		}
		
		/// <summary>
		/// Lista de preguntas de un cuestionario
		/// </summary>
		[XmlElement("pregunta")]
		public List<Question> Questions{
			get {return questions;}	
			set {questions=value;}
		}
		
		/// <summary>
		/// Permite acceder y modificar la siguiente pregunta cuestionario
		/// </summary>
		[XmlElement("siguiente_pregunta")]
		public int NextQuestion{
			get {return nextQuestion;}
			set {nextQuestion = value;}
		}
		[XmlIgnore]
		public QuestionaryResults QuestionsResults {
			get {return this.questionsResults;}
			set {questionsResults = value;}
		}

#endregion
				
		/// <summary>
		/// Establece si una respuesta ha sido seleccionada o no. Mantiene la consistencia de respuestas (i.e., si se marca la respuesta como seleccionada se desmarcan como seleccionadas el resto de respuestas posibles para esa pregunta)
		/// </summary>
		/// <param name="numPregunta">
		/// Un <see cref="System.Int32"/> indicando el numero de la pregunta
		/// </param>
		/// <param name="numRespuesta">
		/// A <see cref="System.Int32"/> indicando el numero de la respuesta escogida
		/// </param>
		/// <param name="val">
		/// A <see cref="System.Boolean"/> Valor de la selección (i.e., si la pregunta a sido seleccionada o no)
		/// </param>
		public void SetAnswer (int numPregunta, int numRespuesta, bool val){
			
			// seleccionamos la pregunta
			Question p = questions[numPregunta];
			
			for (int i=0; i<p.Answers.Count; ++i)
				// actualizamos el valor
				if (i == numRespuesta)
					p.Answers[i].Selected = val;
			
				// si la ponemos a cierta todas las demás tiene que ponerse a false
				else if (val) {
					p.Answers[i].Selected = false;
				}
		}
		
		/// <summary>
		/// Resetea el cuestionario, elimina todas las respuestas y establece la siguiente pregunta como la primera
		/// </summary>
		public void Reset(){
			
			foreach( Question p in questions)
					foreach ( Answer r in p.Answers)
						r.Selected = false;
			
			NextQuestion = 0;
			
			this.Serialize();
			
			//Console.WriteLine("Resetenado!!!!");
		}		
		
		/// <summary>
		/// Calcula el resultado del test,una vez completado
		/// </summary>
		/// <returns>
		/// A <see cref="System.Int32"/> indicando la puntuación. Si el cuestionario no se ha completado, devuelve -1
		/// </returns>
		public int ComputeResults(){
			
			// si no hemos terminado el cuestionario devolvemos -1
			if (!IsFinished()){
				return -1;
			}
			else {
				int total = 0;
				
				foreach( Question p in questions)
					foreach ( Answer r in p.Answers)
						if (r.Selected)
							total += r.Score;
				
				return total;
			}		
		}		
		
		/// <summary>
		/// Comprueba si se ha terminado ya el cuestionario
		/// </summary>
		/// <returns>
		/// true si se ha completado el cuestionario y false en caso contrario
		/// </returns>
		public bool IsFinished(){			
			return NextQuestion == questions.Count;
		}
		
		#region Serialize
		
		/// <summary>
		/// Serializa el cuestionario a un fichero XML
		/// </summary>
		/// <param name="filename">
		/// A <see cref="System.String"/> containing the full path to the file containing the serialization
		/// </param>
		public void Serialize(){
			
			//this.questionsResults.Serialize();
			string path = Configuration.Current.GetQuestionaryConfigurationFolderPath() + Path.DirectorySeparatorChar + questionaryFile;
			//Console.WriteLine("guardando xml del cuestionario en  " + path);
									
			XmlTextWriter escritor = new XmlTextWriter(path, null);
		
			try
			{
				escritor.Formatting = Formatting.Indented;
				
				escritor.WriteStartDocument();
				
				escritor.WriteDocType("cuestionario", null, null, null);
				
				// hoja de estilo para pode ver en un navegador el xml
				escritor.WriteProcessingInstruction("xml-stylesheet", "type='text/xsl' href='cuestionario.xsl'");
				
				XmlSerializer serializer = new XmlSerializer(typeof(Questionary));
				
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
		
		/// <summary>
		/// Deserializa un cuestionario
		/// </summary>
		/// <param name="filename">
		/// A <see cref="System.String"/> con el nombre del fichero
		/// </param>
		/// <returns>
		/// A <see cref="Cuestionario"/> deserializado
		/// </returns>
		private static Questionary Deserialize(string filename)
		{ 
			
			string path = Configuration.Current.GetQuestionaryConfigurationFolderPath() + Path.DirectorySeparatorChar + filename;
			
			if (!File.Exists(path))
			{
				string s = Environment.CommandLine;				
				path = Configuration.CommandDirectory + Path.DirectorySeparatorChar + "cuestionario" + Path.DirectorySeparatorChar + "plantillas" + Path.DirectorySeparatorChar + filename;
			}
			
			// Console.WriteLine("Intentamos abrir el fichero " + path);
			
			XmlTextReader lector = new XmlTextReader(path);
			try
			{
				Questionary c = new Questionary();
				
				XmlSerializer serializer = new XmlSerializer(typeof(Questionary));				
				c = (Questionary) serializer.Deserialize(lector);
				
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
		
		#endregion
		
		#region Util
		
		/// <summary>
		/// Genera un cuesionario de prueba con cinco preguntas
		/// </summary>
		/// <returns>
		/// A <see cref="Cuestionario"/> de prueba
		/// </returns>
		public static Questionary GenerateQuestionary(){
			return GenerateQuestionary(5);			
		}
		
		
		/// <summary>
		/// Genera un cuestionario de prueba relleno con valores genéricos
		/// </summary>
		/// <param name="numPreguntas">
		/// Un <see cref="System.Int32"/> indicando el numero de preguntas que tendra el test
		/// </param>
		/// <returns>
		/// Un <see cref="Cuestionario"/> con numPreguntas genéricas
		/// </returns>
		public static Questionary GenerateQuestionary(int numPreguntas){
			Questionary c = new Questionary();
			
			c.Caption = "Un titulo";
			
			c.questions = new List<Question>();			
			
			for (int i=0; i<numPreguntas; ++i)				
				c.questions.Add(Question.GenerateQuestion("Pregunta " + i));
			
			return c;
			
		}
		#endregion
		
		#region 
		
		public static Questionary GetDailyLifeQuestionary()
		{
			Questionary dlq = Questionary.Deserialize(Configuration.Current.DailyLifeQuestionaryFileName);
			dlq.questionaryFile = Configuration.Current.DailyLifeQuestionaryFileName;
			dlq.questionsResults = QuestionaryResults.Deserialize(Configuration.Current.DailyLifeQuestionaryFileName);
			return dlq;
		}
		
		public static Questionary GetInstrumentalActivitiesQuestionary()
		{
			Questionary dlq = Questionary.Deserialize(Configuration.Current.InstrumentalActivitiesQuestionaryFileName);			
			dlq.questionaryFile = Configuration.Current.InstrumentalActivitiesQuestionaryFileName;
			dlq.questionsResults = QuestionaryResults.Deserialize(Configuration.Current.InstrumentalActivitiesQuestionaryFileName);
			return dlq;
		}
		
		#endregion
	}
	
}


