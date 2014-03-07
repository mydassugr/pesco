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
using System.IO;
using System.Xml;
using System.Xml.Serialization;


namespace pesco 
{	
	public class FontStyle
	{
		private string fontType = "Tahoma";
				
		[XmlElement("font-type")]
		public string FontType {
			get {
				return this.fontType;
			}
			set {
				fontType = value;
			}
		}

		private int size = 18;
				
		
		[XmlElement("size")]
		public int Size {
			get {
				return this.size;
			}
			set {
				size = value;
			}
		}

		
		private byte red = 0;
	
		
		[XmlElement("red")]
		public byte Red {
			get {
				return this.red;
			}
			set {
				red = value;
			}
		}
		
		private byte blue = 0;
	
		
		[XmlElement("blue")]
		public byte Blue {
			get {
				return this.blue;
			}
			set {
				blue = value;
			}
		}
		
		private byte green = 0;
	
		
		[XmlElement("green")]
		public byte Green {
			get {
				return this.green;
			}
			set {
				green = value;
			}
		}
}
	
	/// <summary>
	/// This class is responsible for the file configuration of the app (i.e., where are the config and data files the app uses)
	/// </summary>
	[XmlRoot("pesco-configuration")]
	public class Configuration
	{

        //Directory of pictures of the Exercises  
        public static string DirExerImage = ProgramDir() + System.IO.Path.DirectorySeparatorChar + "ejercicios" + 
			System.IO.Path.DirectorySeparatorChar+"resources"+
            System.IO.Path.DirectorySeparatorChar+"img"+System.IO.Path.DirectorySeparatorChar;
                
                
    	public static bool IsRunningOnMono ()
		{
			return Type.GetType ("Mono.Runtime") != null;
		}
		
		/// <summary>
		/// Current configuration
		/// </summary>
		private static Configuration current = new Configuration();
		
		/// <summary>
		/// Access to the current configuration file for the app
		/// </summary>
		public static Configuration Current {
			get {
				return current;
			}
		}
		
		/// <summary>
		/// The name of configuration file
		/// </summary>
		private static string configFile = "configuration.xml";
		
		#region URL
		private string serverURL = "http://asistic.ugr.es/pesco-guadalinfo";		
		#endregion
		
		[XmlElement("server-url")]
		public string ServerURL {
			get {
				return this.serverURL;
			}
			set {
				serverURL = value;
			}
		}
		
		#region Inferface
		
		private string showExerciseButtons = "yes";
		[XmlElement("show-exercise-buttons")]
		public string ShowExerciseButtons {
			get {
				return this.showExerciseButtons;
			}
			set {
				showExerciseButtons = value;
			}
		}

		private FontStyle largeFont = new FontStyle();
		private FontStyle mediumFont = new FontStyle();
		private FontStyle smallFont =  new FontStyle();
		private FontStyle buttonFont = new FontStyle();
		private FontStyle errorFont = new FontStyle();
		private FontStyle handwrittingStyle = new FontStyle();
		private FontStyle bubbleStyle = new FontStyle();
		
		public FontStyle BubbleStyle {
			get {
				return this.bubbleStyle;
			}
			set {
				bubbleStyle = value;
			}
		}

		[XmlElement("hand-writting-style")]
		public FontStyle HandwrittingStyle {
			get {
				return this.handwrittingStyle;
			}
			set {
				handwrittingStyle = value;
			}
		}

		[XmlElement("button-style")]
		public FontStyle ButtonFont {
			get {
				return this.buttonFont;
			}
			set {
				buttonFont = value;
			}
		}

		[XmlElement("error-style")]
		public FontStyle ErrorFont {
			get {
				return this.errorFont;
			}
			set {
				errorFont = value;
			}
		}

		[XmlElement("large-style")]
		public FontStyle LargeFont {
			get {
				return this.largeFont;
			}
			set {
				largeFont = value;
			}
		}

		[XmlElement("medium-style")]
		public FontStyle MediumFont {
			get {
				return this.mediumFont;
			}
			set {
				mediumFont = value;
			}
		}

		[XmlElement("small-style")]
		public FontStyle SmallFont {
			get {
				return this.smallFont;
			}
			set {
				smallFont = value;
			}
		}
	
		private FontStyle extraLargeFont= new FontStyle();
		private FontStyle hugeFont= new FontStyle();
		private FontStyle extraHugeFont= new FontStyle();
		
		[XmlElement("extra-huge-style")]
		public FontStyle ExtraHugeFont {
			get {
				return this.extraHugeFont;
			}
			set {
				extraHugeFont = value;
			}
		}

		[XmlElement("extra-large-style")]
		public FontStyle ExtraLargeFont {
			get {
				return this.extraLargeFont;
			}
			set {
				extraLargeFont = value;
			}
		}

		[XmlElement("huge-style")]
		public FontStyle HugeFont {
			get {
				return this.hugeFont;
			}
			set {
				hugeFont = value;
			}
		}

		#endregion
		
		#region Questionary
		
		private  string questionaryConfigurationFolder;
		private  string dailyLifeQuestionaryFileName;
		
		[XmlElement("daily-life")]
		public  string DailyLifeQuestionaryFileName {
			get {
				return dailyLifeQuestionaryFileName;
			}
			set {
				dailyLifeQuestionaryFileName = value;
			}
		}

		private  string instrumentalActivitiesQuestionaryFileName;

		[XmlElement("instrumental-activities")]
		public  string InstrumentalActivitiesQuestionaryFileName {
			get {
				return instrumentalActivitiesQuestionaryFileName;
			}
			set {
				instrumentalActivitiesQuestionaryFileName = value;
			}
		}
		
		/// <summary>
		///  Path to the <see cref="pesco.Questionary"/>  XML files
		/// </summary>
		[XmlElement("questionaries-folder")]
		public  string QuestionaryConfigurationFolder {
			get {
				return questionaryConfigurationFolder;
			}
			set {
				questionaryConfigurationFolder = value;
			}
		}
		
		public string GetQuestionaryConfigurationFolderPath()
		{
			return GetConfigurationFolderPath()  + Path.DirectorySeparatorChar + questionaryConfigurationFolder;
		}
		
		#endregion
		
		#region Exercises
		
		/// <summary>
		/// Name of the folder where the exercise configuration files will be saved
		/// </summary>
		private  string exerciseConfigurationFolder;
		
		/// <summary>
		///  Path to the <see cref="pesco.ExerciseMio"/>s  XML file
		/// </summary>
		[XmlElement("exercises-folder")]
		public  string ExerciseConfigurationFolder {
			get {
				return exerciseConfigurationFolder;
			}
			set {
				exerciseConfigurationFolder = value;
			}
		}
		
		/// <summary>
		/// Get the path to the exercice configuration folder
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/> containing the path
		/// </returns>
		public string GetExerciseConfigurationFolderPath()
		{
			return GetConfigurationFolderPath()  + Path.DirectorySeparatorChar + exerciseConfigurationFolder;
		}
		
		#endregion
		
		#region User
		
		/// <summary>
		/// Name of the xml file where the user data (name, addres, telephone, etc.) will be saved
		/// </summary>
		private string userFileName;
		
		/// <summary>
		/// Path to the <see cref="pesco.User"/>  XML file
		/// </summary>
		[XmlElement("user")]
		public  string UserXML{
			get{
				return userFileName;
			}
			set{
				userFileName = value;
			}
		}
		
		/// <summary>
		/// Get the path to the user configuration file
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/> string with the path
		/// </returns>
		public  string GetUserFilePath()
		{
			Console.WriteLine( GetConfigurationFolderPath() + Path.DirectorySeparatorChar + userFileName );
			return GetConfigurationFolderPath() + Path.DirectorySeparatorChar + userFileName;
		}
		#endregion
		
		#region Folders
		private string configurationFolder = ".pesco";
		
		/// <summary>
		/// The path to the user's folder
		/// </summary>
		[XmlIgnore]
		public static string PersonalFolder 
		{
			get{
				return Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			}
		}
		
		/// <summary>
		/// The path to the command's folder
		/// </summary>
		[XmlIgnore]
		public static string CommandDirectory
		{
			get{
				string s = Environment.CommandLine;
			
				s = s.Remove(s.LastIndexOf(Path.DirectorySeparatorChar));
				if ( !Configuration.IsRunningOnMono() ) {
					s = s.Substring(1);
				}
				if ( s.StartsWith("\"") ) {
					s = s.Substring(1);
				}
				return s;
			}
		}
		
		[XmlIgnore]
		public static string CommandExercisesDirectory
		{
			get{
				string s = CommandDirectory;
			
				s += Path.DirectorySeparatorChar + ExercisesFolderName;
				
				return s;
			}
		}
		[XmlIgnore]
		public static string ExercisesFolderName
		{
			get{				
				return "ejercicios";
			}
		}		
		
		/// <summary>
		/// Folder where the configuration file is stored
		/// </summary>
		[XmlElement("configuration-folder")]
		public string ConfigurationFolder
		{
			get{
				return configurationFolder;	
			}
			set {
				configurationFolder = value;	
			}
		}
		
		/// <summary>
		/// Get the path to de configuration folder
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/> strin
		/// </returns>
		public string GetConfigurationFolderPath(){
			return PersonalFolder + Path.DirectorySeparatorChar + configurationFolder;	
		}

		public static string ConfigurationFolderPath(){
			return PersonalFolder + Path.DirectorySeparatorChar + ".pesco";	
		}
		
		/// <summary>
		/// Creates the configuration folders, if they don't exist yet
		/// </summary>
		public static void CreateConfigurationFolder()
		{
			if (!Directory.Exists(current.GetConfigurationFolderPath()))
			{				
				Directory.CreateDirectory(current.GetConfigurationFolderPath());
				// Console.WriteLine("Creando el directorio de configuracion de ec-ng: " + current.GetConfigurationFolderPath());
			}
			
			// if the xml configuration file does not exist...
			if (!File.Exists(current.GetConfigurationFolderPath()+Path.DirectorySeparatorChar+configFile))
			{
				// ...copy it from the template to the hidden configuration folder
				// Console.WriteLine(Environment.CommandLine);
				// Console.WriteLine(CommandDirectory);
				// Console.WriteLine(CommandDirectory + Path.DirectorySeparatorChar + configFile, current.GetConfigurationFolderPath() + Path.DirectorySeparatorChar + configFile);
				File.Copy(CommandDirectory + Path.DirectorySeparatorChar + configFile, current.GetConfigurationFolderPath() + Path.DirectorySeparatorChar + configFile);
				// Console.WriteLine("Apparently the file doesn't exist: " + current.GetConfigurationFolderPath()+ "/" + configFile);
			}
		}
		
		public static void CreateQuestionaryConfigurationFolder()
		{
			// Console.WriteLine("Creando el directorio de configuracion de cuestionarios para ec-ng: " + current.GetQuestionaryConfigurationFolderPath());
			if (!Directory.Exists(current.GetQuestionaryConfigurationFolderPath()))
			{
				Directory.CreateDirectory(current.GetQuestionaryConfigurationFolderPath());
				
			}
		}
		
		public static void CreateExerciseConfigurationFolder()
		{
			// Console.WriteLine("Creando el directorio de configuracion de cuestionarios para ec-ng: " + current.GetExerciseConfigurationFolderPath());
			if (!Directory.Exists(current.GetExerciseConfigurationFolderPath()))
			{
				Directory.CreateDirectory(current.GetExerciseConfigurationFolderPath());
				
			}
		}
		        
           /// <summary>
        /// return the directory where  the program runs 
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/>
        /// </returns>
        public static string ProgramDir(){
            
			/*
            string mDirActual= (string)AppDomain.CurrentDomain.BaseDirectory;
            string [] mDirActual1;
            mDirActual1= mDirActual.Split(new string[] { "bin" }, StringSplitOptions.RemoveEmptyEntries);
                    
            return mDirActual1[0];*/
			
			return CommandDirectory;
        }
   
		#endregion
		
		#region XML Serialization
		
		/// <summary>
		/// Saves a configuration file to an XML file
		/// </summary>
		public void Serialize()
		{
			
		// Console.WriteLine(current.GetConfigurationFolderPath()+"/"+configFile);
			
			XmlTextWriter escritor = new XmlTextWriter(current.GetConfigurationFolderPath()+ Path.DirectorySeparatorChar + configFile, null);
		
			try
			{
				escritor.Formatting = Formatting.Indented;
				
				escritor.WriteStartDocument();
				
				escritor.WriteDocType("configuration", null, null, null);
				
				XmlSerializer serializer = new XmlSerializer(typeof(Configuration));
				
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
		/// Load the current configuration from an XML file
		/// </summary>
		private static void Deserialize()
		{
			Console.WriteLine("Cargando configuración desde: "+current.GetConfigurationFolderPath() + Path.DirectorySeparatorChar + configFile);
			XmlTextReader lector = new XmlTextReader(current.GetConfigurationFolderPath() + Path.DirectorySeparatorChar + configFile);
			try
			{
				current = new Configuration();
				
				XmlSerializer serializer = new XmlSerializer(typeof(Configuration));				
				current = (Configuration) serializer.Deserialize(lector);
				
				lector.Close();				
				return;
			}
			catch( Exception e)
			{
				lector.Close();
				return;
			}
		}
		#endregion
		
		#region Inicialization
		
		/// <summary>
		/// Creates a new configuration
		/// </summary>
		private Configuration ()
		{
			this.bubbleStyle.Blue = 255;
			this.bubbleStyle.Red = 0;
			this.bubbleStyle.Green = 0;			
			this.bubbleStyle.FontType = "Ahafoni CLM Bold";
			this.bubbleStyle.Size = 20;
		}
		
		/// <summary>
		/// Initializes the configuration loading it from the configuration XML file
		/// </summary>
		public static void Init()
		{ 
			// creates the configuration folder
			CreateConfigurationFolder();			
			
			// deserialize xml file
			Deserialize();
			
			// creates the directories for questionaries and exercises, if they don't exist
			CreateExerciseConfigurationFolder();
			CreateQuestionaryConfigurationFolder();
			
			//current.Serialize();

		}
		
		#endregion
	}
}


