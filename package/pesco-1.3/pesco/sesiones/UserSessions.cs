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
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using System;

namespace pesco
{

	public class UserSessions
	{
		
		protected static UserSessions current;
		
		public static string xmlUserFile = "user-sessions.xml";
		
		private List <Session> sessions = new List<Session>();
		
		[XmlElement("sessions")]
		public List<Session> Sessions {
			get {
				return sessions;
			}
			set {
				sessions = value;
			}
		}
		
		public static UserSessions GetUserSessions() {
			return current;
		}
		
		public static UserSessions GetInstance() {
			return current;
		}

		public void ShowInfo() {			
			// Console.WriteLine( "Sesiones en XML: "+sessions.Count );
		}
		
		public UserSessions ()
		{
			
			/*Session auxSession = new Session();
			
			auxSession.IdSession = 1;
			auxSession.Starttext = "En esta primera sesión...";
			auxSession.Exercises.Add( new ExerciseEntry(6, "repeat", 0, 2, -1, 0) );
			auxSession.Exercises.Add( new ExerciseEntry(6, "time", 15, 0, -1, 0) );
			auxSession.Exercises.Add( new ExerciseEntry(7, "notime", 0, 0, -1, 0) );
			
			sessions.Add( auxSession );
			
			Serialize();*/
			
		}
		
		
		public Session GetSession( int id ) {
			// Console.WriteLine("Hay "+sessions.Count+" sesiones en la lista");
			for ( int i = 0; i < Sessions.Count; i++ ) {
				// Console.WriteLine( Sessions[i].IdSession );
				if ( Sessions[i].IdSession == id ) {
					return Sessions[i];
				}
			}
			
			return null;
			
		}
		
		public int GetExerciseIdPosition ( int sessionid, int exerciseid ) {
			
			// Console.WriteLine("Looking for position of exercise: "+exerciseid+" in session "+sessionid);
			Session auxSession = GetSession(sessionid);
			for ( int i = 0; i < auxSession.Exercises.Count; i++ ) {		
				if ( auxSession.Exercises[i].Id == exerciseid ) {
					return i;	
				}
			}
			
			return -1;
		}
		
		public void Serialize(){

			string fullPath = Configuration.Current.GetConfigurationFolderPath()+ "/" + xmlUserFile;
			
			if(!Directory.Exists(Configuration.Current.GetConfigurationFolderPath()))
				Directory.CreateDirectory(Configuration.Current.GetConfigurationFolderPath());
	
			XmlTextWriter escritor = new XmlTextWriter(fullPath, null);
		
			try
			{
				XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
				
				namespaces.Add("","");
								
				escritor.Formatting = Formatting.Indented;				
				escritor.WriteStartDocument();				
				escritor.WriteDocType("user-sessions", null, null, null);
				
				XmlSerializer serializer = new XmlSerializer(typeof(UserSessions));
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
	
		public static UserSessions Deserialize()
		{
			// This has been replaced to overwrite user-sessions.xml file always
			
			// string fullPath = Configuration.Current.GetConfigurationFolderPath() + "/" + xmlUserFile;
			// Console.WriteLine( fullPath );
			
			// if (!File.Exists(fullPath))
			// {
			//	string s = Environment.CommandLine;
				string fullPath = Configuration.CommandDirectory + Path.DirectorySeparatorChar + "sesiones" + Path.DirectorySeparatorChar + "xml-templates"+ Path.DirectorySeparatorChar + xmlUserFile;
			  Console.WriteLine("Full path: " + fullPath);
			// }
			
			XmlTextReader lector = new XmlTextReader(fullPath);
			try
			{	
				current = new UserSessions();
				
				XmlSerializer serializer = new XmlSerializer(typeof(UserSessions));				
				current = (UserSessions) serializer.Deserialize(lector);
				current.ShowInfo();
				
				lector.Close();				
				return current;
			}
			catch( Exception e)
			{
				Console.WriteLine( e.ToString() );
				lector.Close();
				return null;
			}
		}		

		
	}
}

