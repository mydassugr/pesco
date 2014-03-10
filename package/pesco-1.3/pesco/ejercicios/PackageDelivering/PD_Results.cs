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

	public class PD_ResultsBox
	{

		string boxname;
		string goal;
		string position;
		string achieved;

		public string Position {
			get { return position; }
			set { position = value; }
		}


		public string Goal {
			get { return goal; }
			set { goal = value; }
		}


		public string Boxname {
			get { return boxname; }
			set { boxname = value; }
		}


		public string Achieved {
			get { return achieved; }
			set { achieved = value; }
		}

		public PD_ResultsBox ()
		{
		}

		public PD_ResultsBox (string boxname, string goal, string position, string achieved)
		{
			
			this.Boxname = boxname;
			this.Goal = goal;
			this.Position = position;
			this.achieved = achieved;
			
		}
		
		
	}

	public class PD_ResultsAction
	{

		// Type can be: "MOVIMIENTO", "RECOGIDA" or "ENTREGA"
		string type;
		string allowed;
		string param1;
		string param2;

		public string Type {
			get { return type; }
			set { type = value; }
		}

		public string Allowed {
			get { return allowed; }
			set { allowed = value; }
		}

		public string Param1 {
			get { return param1; }
			set { param1 = value; }
		}

		public string Param2 {
			get { return param2; }
			set { param2 = value; }
		}

		public PD_ResultsAction ()
		{
		}

		public PD_ResultsAction (string type, string allowed, string param1, string param2)
		{
			
			this.Type = type;
			this.allowed = allowed;
			this.Param1 = param1;
			this.Param2 = param2;
			
		}
		
	}

	public class SingleResultPackageDelivering : SingleResultElement
	{
		
		private int scenarioId;
		private int executionId;
		List<PD_ResultsAction> actions = new List<PD_ResultsAction> ();
		List<PD_ResultsBox> boxes = new List<PD_ResultsBox> ();
		private int actionsCounter = 0;
		private DateTime startTime;
		private DateTime endTime;


		public int ExecutionId {
			get { return executionId; }
			set { executionId = value; }
		}

		public int ScenarioId {
			get { return scenarioId; }
			set { scenarioId = value; }
		}


		public int ActionsCounter {
			get { return actionsCounter; }
			set { actionsCounter = value; }
		}


		public List<PD_ResultsAction> Actions {
			get { return actions; }
			set { actions = value; }
		}

		public DateTime StartTime {
			get { return startTime; }
			set { startTime = value; }
		}

		public DateTime EndTime {
			get { return endTime; }
			set { endTime = value; }
		}

		public List<PD_ResultsBox> Boxes {
			get { return boxes; }
			set { boxes = value; }
		}

		public SingleResultPackageDelivering ()
		{
			
		}

		public SingleResultPackageDelivering (int scenario)
		{
			
			ExecutionId = SessionManager.GetInstance ().CurExecInd;
			ScenarioId = scenario;
			startTime = DateTime.Now;
			
		}

		public void AddAction (PD_ResultsAction action)
		{
			
			Actions.Add (action);
			ActionsCounter++;
			endTime = DateTime.Now;
			
		}

		public void FinishResult ()
		{
			endTime = DateTime.Now;
		}
		
		public int GetMovementsCounter() {
		
			int counterMovements = 0;
			for ( int i = 0; i < Actions.Count; i++ ) {
				if ( Actions[i].Type == "MOVIMIENTO" )	{
					counterMovements++;	
				}
			}
			return counterMovements;
		
		}
	}
	
	public class PackageDeliveringResults : ExerciseResults 
	{
		
		List <ExerciceExecutionResult<SingleResultPackageDelivering>> packageDeliveringExecutionResults;
		
		[XmlElement("DirectNumbersExecutionResults")]
		public List<ExerciceExecutionResult<SingleResultPackageDelivering>> PackageDeliveringExecutionResults {
			get {return this.packageDeliveringExecutionResults;}
			set {packageDeliveringExecutionResults = value;}
		}
		
		public PackageDeliveringResults ()
		{
			PackageDeliveringExecutionResults = new List<ExerciceExecutionResult<SingleResultPackageDelivering>>();
		}
		
		public void setResult(SingleResultPackageDelivering re)
		{
			PackageDeliveringExecutionResults[PackageDeliveringExecutionResults.Count-1].SingleResults.Add(re);
        }
		
		public void NewResults( int scenarioId ) {
			
			CurrentExecution.SingleResults.Add( new SingleResultPackageDelivering( scenarioId) );
		}
		
		public override string ToString ()
		{
			return null;
		}

		[XmlIgnore]		
		public ExerciceExecutionResult<SingleResultPackageDelivering> CurrentExecution {		
			get { 
				if ( PackageDeliveringExecutionResults.Count > 0 ) {
					return PackageDeliveringExecutionResults[PackageDeliveringExecutionResults.Count - 1];
				} else {
					return null;	
				}
			}
			set {
				PackageDeliveringExecutionResults[PackageDeliveringExecutionResults.Count - 1] = value;	
			}			
		}

		[XmlIgnore]		
		public SingleResultPackageDelivering CurrentSingleResult {		
			get { 
				if ( CurrentExecution != null ) {
					if ( CurrentExecution.SingleResults.Count > 0 );
					return CurrentExecution.SingleResults[CurrentExecution.SingleResults.Count - 1];
				} else {
					return null;	
				}
			}
			set {
				CurrentExecution.SingleResults[CurrentExecution.SingleResults.Count - 1] = value;	
			}			
		}
		public void AddAction( PD_ResultsAction action ) {
		
			CurrentSingleResult.AddAction( action );
			
		}
		
		public void SetFinalStatus( PD_BoxStatus [] boxstatus ) {
			
			for ( int i = 0; i < boxstatus.Length; i++ ) {
			
				PD_ResultsBox auxResultBox = new PD_ResultsBox();
				auxResultBox.Boxname = boxstatus[i].BoxName;
				if ( boxstatus[i].GoalPosition == -1 ) {
					auxResultBox.Goal = "van";	
				} else {
					auxResultBox.Goal = boxstatus[i].GoalPosition.ToString();	
				}
				
				if ( boxstatus[i].CurrentPosition == -1 ) {
					auxResultBox.Position = "van";	
				} else {
					auxResultBox.Position = boxstatus[i].CurrentPosition.ToString();	
				}
				
				if ( boxstatus[i].CurrentPosition == boxstatus[i].GoalPosition )
					auxResultBox.Achieved = "YES";
				else
					auxResultBox.Achieved = "NO";
				
				CurrentSingleResult.Boxes.Add( auxResultBox );
			}
			
		}
	}	
	
	
}

