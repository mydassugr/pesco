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

	public class SingleResultObjetosClasificables: SingleResultElement
	{
		
			
		private int validObjects;
		private int distractorObjects;
		private int selectedObjects;
		private int objectsNumber;
		private int level;
		private int corrects;
		private int errors;
		private int omissions;
		private int timeElapsed;
		private int total;
		private List<string> correctsByCategory = new List<string> ();

		
		[XmlElement("ValidObjects")]
		public int ValidObjects {
			get {return this.validObjects;}
			set {validObjects = value;}
		}
		

		[XmlElement("DistractorObjects")]
		public int DistractorObjects {
			get {return this.distractorObjects;}
			set {distractorObjects = value;}
		}
		
		[XmlElement("SelectedObjects")]
		public int SelectedObjects {
			get {return this.selectedObjects;}
			set {selectedObjects = value;}
		}
		
		[XmlElement("ObjectsNumber")]
		public int ObjectsNumber {
			get {return this.objectsNumber;}
			set {objectsNumber = value;}
		}
		
		[XmlElement("Level")]
		public int Level {
			get {return this.level;}
			set {level = value;}
		}
		
		[XmlElement("Corrects")]
		public int Corrects {
			get {return this.corrects;}
			set {corrects = value;}
		}

		[XmlElement("Errors")]
		public int Errors {
			get {return this.errors;}
			set {errors = value;}
		}

		[XmlElement("Omissions")]
		public int Omissions {
			get {return this.omissions;}
			set {omissions = value;}
		}

		

		[XmlElement("TimeElapsed")]
		public int TimeElapsed {
			get {return this.timeElapsed;}
			set {timeElapsed = value;}
		}
		
		[XmlElement("CorrectsByCategory")]
		public List<string> CorrectsByCategory {
			get { return this.correctsByCategory; }
			set { correctsByCategory = value; }
		}

				

		public SingleResultObjetosClasificables (int validObjectsArg, int distractorObjectsArg, int selectedObjectsArg, int objectsNumberArg, int levelArg, int correctsArg, int errorsArg, int omissionsArg, int timeElapsedArg)
		{
			ValidObjects = validObjectsArg;
			DistractorObjects= distractorObjectsArg;
			SelectedObjects = selectedObjectsArg;
			ObjectsNumber = objectsNumberArg;
			Level = levelArg;
			Corrects = correctsArg;
			Errors = errorsArg;
			Omissions = omissionsArg;
			TimeElapsed = timeElapsedArg;
		}

		public SingleResultObjetosClasificables ()
		{
			
		}
	}

	public class ResultadosEjercicioObjetosClasificables: Results
	{
		
	/*	public  bool demoExecuted;
		
		[XmlElement("demoExecuted")]
		public bool DemoExecuted {
			get {return this.demoExecuted;}
			set {demoExecuted = value;}
		}*/	
		
		List <ExerciceExecutionResult<SingleResultObjetosClasificables>> objetosClasificablesExecutionResults	;
		
		[XmlElement("ObjetosClasificablesExecutionResults")]
		public List<ExerciceExecutionResult<SingleResultObjetosClasificables>> ObjetosClasificablesExecutionResults {
			get {return this.objetosClasificablesExecutionResults;}
			set {objetosClasificablesExecutionResults = value;}
		}
		


		public void AddResult (int validObjectsArg, int distractorObjectsArg, int selectedObjectsArg, int objectsNumberArg, int levelArg, int correctsArg, int errorsArg, int omissionsArg,int timeElapsedArg)

		{
			

			SingleResultObjetosClasificables auxResult = new SingleResultObjetosClasificables ( validObjectsArg, distractorObjectsArg, selectedObjectsArg, objectsNumberArg, levelArg, correctsArg, errorsArg, omissionsArg, timeElapsedArg);
			ObjetosClasificablesExecutionResults[ObjetosClasificablesExecutionResults.Count -1].SingleResults.Add(auxResult);

			//CO_Result auxResult = new CO_Result ( executionId, level, correctsByCategory, corrects, errors, omissions, total, totalElements, timeElapsed);
			//results.Add (auxResult);
		
			
		}

		public ResultadosEjercicioObjetosClasificables ()
		{
			ObjetosClasificablesExecutionResults = new List<ExerciceExecutionResult<SingleResultObjetosClasificables>>();
			
		}
	}
}

