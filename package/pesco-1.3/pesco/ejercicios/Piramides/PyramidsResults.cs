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


	public class SingleResultPyramids : SingleResultElement
	{

		private int corrects;
		private int fails;
		private int omissions;						
	
		public int Corrects {
			get {
				return corrects;
			}
			set {
				corrects = value;
			}
		}

		public int Fails {
			get {
				return fails;
			}
			set {
				fails = value;
			}
		}
		
		public int Omissions {
			get {
				return omissions;
			}
			set {
				omissions = value;
			}
		}
		
		public SingleResultPyramids ()
		{
		}
		
		public SingleResultPyramids ( int corrects, int fails, int omissions ) {
			
			Corrects = corrects;
			Fails = fails;
			Omissions = omissions;
			
		}
	}
	
	public class PyramidsResults : ExerciseResults 
	{
		
		List <ExerciceExecutionResult<SingleResultPyramids>> pyramidsExecutionResults;
		
		[XmlElement("PyramidsExecutionResults")]
		public List<ExerciceExecutionResult<SingleResultPyramids>> PyramidsExecutionResults {
			get {return this.pyramidsExecutionResults;}
			set {pyramidsExecutionResults = value;}
		}

		[XmlIgnore]
		public ExerciceExecutionResult<SingleResultPyramids> CurrentExecution {		
			get { 
				if ( pyramidsExecutionResults.Count > 0 ) {
					return pyramidsExecutionResults[pyramidsExecutionResults.Count - 1];
				} else {
					return null;	
				}
			}
			set {
				pyramidsExecutionResults[pyramidsExecutionResults.Count - 1] = value;	
			}			
		}
	
		public PyramidsResults ()
		{
			pyramidsExecutionResults = new List<ExerciceExecutionResult<SingleResultPyramids>>();
		}
		
		public void setResult( int corrects, int fails, int omissions )
		{
			SingleResultPyramids re = new SingleResultPyramids( corrects, fails, omissions );
			PyramidsExecutionResults[PyramidsExecutionResults.Count-1].SingleResults.Add(re);
        }
		
		public void setResult(SingleResultPyramids re)
		{
			PyramidsExecutionResults[PyramidsExecutionResults.Count-1].SingleResults.Add(re);
        }
		
		public override string ToString ()
		{
			return null;
		}

		public void ResetExecution() {
			
			this.CurrentExecution.TotalCorrects = 0;
			this.CurrentExecution.TotalFails = 0;
			this.CurrentExecution.TotalOmissions = 0;
			this.CurrentExecution.TotalTimeElapsed = 0;

		}
		 
	}
}

