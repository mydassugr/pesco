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

using System.Collections.Generic;


namespace pesco
{
	/// <summary>
	/// Posibles valores para el estado civil
	/// </summary>
	public enum MaritalStatus
	{
		
		error,
		married,
		widow,
		divorced,
		single,
		other
	}

	/// <summary>
	/// Posibles Valores para los coresidentes del usuario
	/// </summary> 
	public enum LivesWith
	{
		error,
		spouse,
		alone,
		residency,
		child,
		otherFamilyMenbers,
		others
		
	}

	/// <summary>
	/// Posibles valores para el nivel de estudios del us
	/// </summary> uario
	public enum Education
	{
		error,
		writing_reading,
		school,
		middle_school,
		high_school,
		vocational_school,
		degree,
		bachelor,
		ph_d
	}
	
	/// <summary>
	/// 
	/// </summary>
	public enum ProfileValue {
		Normal,
		Altered,
		Undefined
	}

	/// <summary>
	/// 
	/// </summary>
	[XmlRoot("usuario")]
	public class User
	{
		private string name;
		private string firstlastname;
		private string secondlastname;
		private string city;
		private string phone;
		/*private string alias;*/
		private char sex;
		private DateTime birthDate;
		private MaritalStatus maritalStatus;
		private LivesWith livesWith;
		private Education education;
		
		private string dni;

		private int readingHours;
		private int workshopHours;
		private int exerciseHours;
		private int computerHours;
		
		private ProfileValue memoryProfileValue = ProfileValue.Undefined;
		private ProfileValue attentionProfileValue = ProfileValue.Undefined;
		private ProfileValue reasoningProfileValue = ProfileValue.Undefined;
		private ProfileValue planificationProfileValue = ProfileValue.Undefined;
		
		[XmlElement("attention-profile")]
		public ProfileValue AttentionProfileValue {
			get {
				return this.attentionProfileValue;
			}
			set {
				attentionProfileValue = value;
			}
		}

		[XmlElement("memory-profile")]
		public ProfileValue MemoryProfileValue {
			get {
				return this.memoryProfileValue;
			}
			set {
				memoryProfileValue = value;
			}
		}

		[XmlElement("planning-profile")]
		public ProfileValue PlanificationProfileValue {
			get {
				return this.planificationProfileValue;
			}
			set {
				planificationProfileValue = value;
			}
		}

		[XmlElement("reasoning-profile")]
		public ProfileValue ReasoningProfileValue {
			get {
				return this.reasoningProfileValue;
			}
			set {
				reasoningProfileValue = value;
			}
		}

		private bool mouseTestPassed = true;
		
		[XmlElement("prueba-raton")]
		public bool MouseTestPassed {
			get {
				return this.mouseTestPassed;
			}
			set {
				mouseTestPassed = value;
			}
		}

		bool ok = true;
		
		
		public bool Ok {
			get {
				return this.ok;
			}
			set {
				ok = value;
			}
		}

		
		
		#region Medals
		[XmlIgnore]
		private List<Medal> medals;
		
		[XmlElement("medals")]
		public List<Medal> Medals {
			get {
				return this.medals;
			}
		}

		
		public List<Medal> GetMedalsForSession(int sessionId)
		{
			List<Medal> medalsForSession = new List<Medal>();
			
			foreach(Medal m in medals)
				if (m.SessionId == sessionId)
					medalsForSession.Add(m);
			
			return medalsForSession;
					
		}
		
		public static ExerciseCategory GetExerciseCategory( int idExercise ) {
		
			switch (idExercise) {
				case 1: return ExerciseCategory.Memory;
						break;
				case 2: return ExerciseCategory.Memory;
						break;
				case 3: return ExerciseCategory.Memory;
						break;
				case 4: return ExerciseCategory.Attention;
						break;
				case 5: return ExerciseCategory.Memory;
						break;
				case 6: return ExerciseCategory.Attention;
						break;
				case 7: return ExerciseCategory.Memory;
						break;
				case 8: return ExerciseCategory.Memory;
						break;
				case 9: return ExerciseCategory.Attention;
						break;
				case 11: return ExerciseCategory.Reasoning;
						break;
				case 12: return ExerciseCategory.Reasoning;
						break;
				case 13: return ExerciseCategory.Reasoning;
						break;		
				case 14: return ExerciseCategory.Reasoning;
						break;
				case 15: return ExerciseCategory.Reasoning;
						break;
				case 16: return ExerciseCategory.Planification;
						break;
				case 17: return ExerciseCategory.Planification;
						break;
				case 18: return ExerciseCategory.Memory;
						break;
				case 19: return ExerciseCategory.Memory;
						break;				
			}
			
			return ExerciseCategory.Attention;
		}
		
		public static System.Collections.Generic.SortedDictionary<ExerciseCategory, List<Medal>> GetMedalsBySessionAndCategory(int sessionId)
		{
			User u = User.Deserialize();
			
			List<Medal> medals4Session = u.GetMedalsForSession(sessionId);
			
			// Attention
			List<Medal> attentionMedals = new List<Medal>();
			List<Medal> memoryMedals= new List<Medal>();
			List<Medal> reasoningMedals= new List<Medal>();
			List<Medal> planningMedals= new List<Medal>();
			
			foreach(Medal m in medals4Session){
				if (GetExerciseCategory(m.ExerciseId).Equals(ExerciseCategory.Attention))
				    attentionMedals.Add(m);
				else if (GetExerciseCategory(m.ExerciseId).Equals(ExerciseCategory.Memory))
					memoryMedals.Add(m);
				else if (GetExerciseCategory(m.ExerciseId).Equals(ExerciseCategory.Reasoning))
					reasoningMedals.Add(m);
				else if (GetExerciseCategory(m.ExerciseId).Equals(ExerciseCategory.Planification))
					planningMedals.Add(m);
			}
			
			SortedDictionary<ExerciseCategory, List<Medal>> res = new SortedDictionary<ExerciseCategory, List<Medal>>();
			
			res.Add(ExerciseCategory.Attention, attentionMedals);
			res.Add(ExerciseCategory.Reasoning, reasoningMedals);
			res.Add(ExerciseCategory.Planification, planningMedals);
			res.Add(ExerciseCategory.Memory, memoryMedals);
			
			//u.Serialize();
			
			return res;
		}
		
		public static System.Collections.Generic.SortedDictionary<MedalValue, int> GetMedalsBySessionAndValue(int sessionId)
		{
			User u = User.Deserialize();
			
			// If there isn't user data, return null
			if ( u == null ) {
				return null;	
			}
			
			List<Medal> medals4Session = u.GetMedalsForSession(sessionId);
			
			int goldCounter = 0;
			int silverCounter = 0;
			int bronzeCounter = 0;
			
			foreach(Medal m in medals4Session){
				if ( m.MedalValue == MedalValue.Gold )
					goldCounter++;
				else if ( m.MedalValue == MedalValue.Silver )
					silverCounter++;
				else if ( m.MedalValue == MedalValue.Bronze )
					bronzeCounter++;				
			}
			
			SortedDictionary<MedalValue, int> res = new SortedDictionary<MedalValue, int>();
			
			res.Add(MedalValue.Gold, goldCounter);
			res.Add(MedalValue.Silver, silverCounter);
			res.Add(MedalValue.Bronze, bronzeCounter);
			
			if ( goldCounter == 0 && silverCounter == 0 && bronzeCounter == 0 ) {
				return null;	
			} else {				
				return res;
			}
		}
		#endregion
		
		/// <summary>
		/// Crea un usuario con los datos vacios
		/// </summary>
		public User (){
			
			medals = new List<Medal>();
			readingHours = -1;
			workshopHours= -1;
			exerciseHours= -1;
			computerHours= -1;
			
			/*
			 * 
			Medal m = new Medal(MedalValue.Gold, DateTime.Now, 1, 1);
			Medal m2 = new Medal(MedalValue.Gold, DateTime.Now, 2,1);
			Medal m3 = new Medal(MedalValue.Gold, DateTime.Now, 12,1);
			Medal m4 = new Medal(MedalValue.Bronze, DateTime.Now, 7,1);
			
			this.medals.Add(m);
			this.medals.Add(m2);
			this.medals.Add(m3);
			this.medals.Add(m4);
			*/
		}

		public static bool IsRegistered(){
			return System.IO.File.Exists(Configuration.Current.GetUserFilePath());
		}
		
		#region Properties

		/// <summary>
		/// 
		/// </summary>
		[XmlElement("nombre")]
		public string Name {
			get { return name; }
			set { name = value; }
		}

		[XmlElement("primerApellido")]
		public string Firstlastname {
			get {
				return this.firstlastname;
			}
			set {
				firstlastname = value;
			}
		}
		
		[XmlElement("segundoApellido")]
		public string Secondlastname {
			get {
				return this.secondlastname;
			}
			set {
				secondlastname = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/*[XmlElement("alias")]
		public string Alias {
			get {
				return alias;
			}
			set {
				alias = value;
			}
		}*/

		/// <summary>
		/// 
		/// </summary>
		[XmlElement("poblacion")]
		public string City {
			get { return city; }
			set { city = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		[XmlElement("telefono")]
		public string Phone {
			get { return phone; }
			set { phone = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		[XmlElement("sexo")]
		public char Sex {
			
			get { return sex; }
			set { if (sex == 'h' || sex == 'm') sex = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		[XmlElement("fecha_nacimiento")]
		public DateTime BirthDate {
			get { return birthDate; }
			set { birthDate = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		[XmlElement("estado_civil")]
		public MaritalStatus MaritalStat {
			get { return maritalStatus; }
			set { maritalStatus = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		[XmlElement("con_quien_vive")]
		public LivesWith Lives {
			get { return livesWith; }
			set { livesWith = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		[XmlElement("nivel_estudios")]
		public Education EducationalLevel {
			get { return education; }
			set { education = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		[XmlElement("horas_lectura")]
		public int ReadingHours {
			get { return readingHours; }
			set { readingHours = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		[XmlElement("horas_talleres")]
		public int WorkshopHours {
			get { return workshopHours; }
			set { workshopHours = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		[XmlElement("horas_ejercicio")]
		public int ExerciseHours {
			get { return exerciseHours; }
			set { exerciseHours = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		[XmlElement("horas_ordenador")]
		public int ComputerHours {
			get { return computerHours; }
			set { computerHours = value; }
		}
		
		/// <summary>
		/// 
		/// </summary>
		[XmlElement("dni")]
		public string Dni {
			get {
				return dni;
			}
			set {
				dni = value;
			}
		}
		
		#endregion
		
		
		
		
		#region Serialization

		/// <summary>
		/// Serializa el usuario a un fichero xml
		/// </summary>
		/// <param name="filename">
		/// A <see cref="System.String"/> containing the full path to the file containing the serialization
		/// </param>
		public void Serialize ()
		{
			XmlTextWriter escritor = new XmlTextWriter (Configuration.Current.GetUserFilePath(), System.Text.Encoding.UTF8);
			
			try {
				escritor.Formatting = Formatting.Indented;
				
				escritor.WriteStartDocument ();
				
				escritor.WriteDocType ("usuario", null, null, null);
				
				// hoja de estilo para pode ver en un navegador el xml
				escritor.WriteProcessingInstruction ("xml-stylesheet", "type='text/xsl' href='./usuario.xsl'");
				
				XmlSerializer serializer = new XmlSerializer (typeof(User));
				
				XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces ();
				
				namespaces.Add ("", "");
				
				serializer.Serialize (escritor, this, namespaces);
				
				escritor.WriteEndDocument ();
				escritor.Close ();
			} catch (Exception e) {
				escritor.Close ();
				
				Console.WriteLine ("Error al serializar" + e.Message);
			}
		}

		/// <summary>
		/// Deserializa un usuario a partir de un fichero XML
		/// </summary>
		/// <param name="filename">
		/// A <see cref="System.String"/> con el nombre del fichero
		/// </param>
		/// <returns>
		/// A <see cref="Usuario"/> deserializado
		/// </returns>
		public static User Deserialize ()
		{
			try{
				XmlTextReader lector = new XmlTextReader (Configuration.Current.GetUserFilePath());
				
				try {
					User c = new User ();
					
					XmlSerializer serializer = new XmlSerializer (typeof(User));
					
					c = (User)serializer.Deserialize (lector);
					
					lector.Close ();
					
					return c;
				} catch (Exception e) {
					lector.Close ();
					Console.WriteLine ("Error deserializando " + e.Message);
					return null;
				}
			} catch (Exception e) {
				return null;
			}
		}
		#endregion
		
		
	}
}


