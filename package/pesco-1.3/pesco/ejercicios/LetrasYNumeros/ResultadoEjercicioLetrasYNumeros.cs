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

	public class SingleResultVowelsNumbers: SingleResultElement{
		string showedValue;
		string userValue;
		int level;
		string result;
		int timeElapsed;
		
		

		[XmlElement("ShowedValue")]
		public string ShowedValue {
			get {return this.showedValue;}
			set {showedValue = value;}
		}
		
		[XmlElement("UserValue")]			
		public string UserValue {
			get {return this.userValue;}
			set {userValue = value;}
		}
		
		[XmlElement("Level")]
		public int Level {
			get {return this.level;}
			set {level = value;}
		}

		[XmlElement("Result")]
		public string Result {
			get {return this.result;}
			set {result = value;}
		}

		[XmlElement("TimeElapsed")]
		public int TimeElapsed {
			get {return this.timeElapsed;}
			set {timeElapsed = value;}
		}
		
		
		
		public SingleResultVowelsNumbers(){}
		public SingleResultVowelsNumbers(string showedValueArg,string userValueArg,int levelArg,string resultArg,int timeElapsedArg){
			
			ShowedValue=showedValueArg;
			UserValue= userValueArg;
			Level= levelArg;
			Result= resultArg;
			TimeElapsed= timeElapsedArg;
			
		}
	}

	public class ResultadoEjercicioLetrasYNumeros : Results 
	{
		
		List <ExerciceExecutionResult<SingleResultVowelsNumbers>> vowelsNumberExecutionResults	;
		
		[XmlElement("VowelsNumberExecutionResults")]
		public List<ExerciceExecutionResult<SingleResultVowelsNumbers>> VowelsNumberExecutionResults {
			get {return this.vowelsNumberExecutionResults;}
			set {vowelsNumberExecutionResults = value;}
		}
		
		public ResultadoEjercicioLetrasYNumeros ()
		{
			VowelsNumberExecutionResults = new List<ExerciceExecutionResult<SingleResultVowelsNumbers>>();
		}
		
			
		
		public void setResultado(string showedValueArg,string userValueArg,int levelArg,string resultArg,int timeElapsedArg)
		{

			SingleResultVowelsNumbers re = new SingleResultVowelsNumbers(showedValueArg,userValueArg,levelArg,resultArg,timeElapsedArg);
			vowelsNumberExecutionResults[vowelsNumberExecutionResults.Count -1].SingleResults.Add(re);
			
        }
		
		public override string ToString ()
		{
			/*string s = "";
			
			for (int i=0; i<resultados.Count; ++i)
		    {
		       s += "Nivel = " + i  + "  Resultados = ";
				
				for (int j=0; j<3; ++j)
					s += resultados[i].Resultado[j] + ", ";
		    }

			s += "\n";
			return s;*/
			return null;
		}

		 
	}
}

