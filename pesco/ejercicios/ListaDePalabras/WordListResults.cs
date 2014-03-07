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

    public class SingleResultsWordList: SingleResultElement{
        
		bool longTerm;
        int validTastks;
        int distractorTasks;
        int helpTasks;
        int userTasks;
        int helpcounter;
        int correts;
        int fails;
        int omissions;
        int level;
        int timeelapsed;
        
		[XmlElement("LongTerm")]
		public bool LongTerm {
			get {return this.longTerm;}
			set {longTerm = value;}
		}
		
        [XmlElement("Corrects")]
        public int Correts {
            get {return this.correts;}
            set {correts = value;}
        }

        [XmlElement("DistractorTasks")]
        public int DistractorTasks {
            get {return this.distractorTasks;}
            set {distractorTasks = value;}
        }

        [XmlElement("Fails")]
        public int Fails {
            get {return this.fails;}
            set {fails = value;}
        }

        [XmlElement("Helpcounter")]
        public int Helpcounter {
            get {return this.helpcounter;}
            set {helpcounter = value;}
        }

        [XmlElement("HelpTasks")]
        public int HelpTasks {
            get {return this.helpTasks;}
            set {helpTasks = value;}
        }

        [XmlElement("Level")]
        public int Level {
            get {return this.level;}
            set {level = value;}
        }

        [XmlElement("Omissions")]
        public int Omissions {
            get {return this.omissions;}
            set {omissions = value;}
        }

        [XmlElement("TimeElapsed")]
        public int Timeelapsed {
            get {return this.timeelapsed;}
            set {timeelapsed = value;}
        }

        [XmlElement("UserTasks")]
        public int UserTasks {
            get {return this.userTasks;}
            set {userTasks = value;}
        }
        
        [XmlElement("ValidTasks")]
        public int ValidTastks {
            get {return this.validTastks;}
            set {validTastks = value;}
        }

        public SingleResultsWordList(){}
        public SingleResultsWordList(bool longTermArg ,int validTastksArg, int distractorTasksArg, int helpTasksArg, int userTasksArg, int helpcounterArg, int corretsArg, int failsArg, int omissionsArg, int levelArg, int timeelapsedArg){
            
			LongTerm= longTermArg;
            ValidTastks= validTastksArg;
            DistractorTasks= distractorTasksArg;
            HelpTasks= helpTasksArg;
            UserTasks = userTasksArg;
            Helpcounter= helpcounterArg;
            Correts = corretsArg;
            Fails = failsArg;
            Omissions = omissionsArg;
            Level = levelArg;
            Timeelapsed = timeelapsedArg;
        }
        
    }
    public class WordListResults: Results
    {

        /*public class ResultsByLevel
        {
            int aciertos;
            int errores;
            int omisiones;
            TimeSpan tiempo;
            
            [XmlElement("tiempo")]
            public string XmlDuration
            {
                get { return tiempo.ToString(); }
                set { tiempo = TimeSpan.Parse(value); }
            }
                
            [XmlIgnore]
            public TimeSpan Time {
                get {
                    return tiempo;
                }
                set {
                    tiempo = value;
                }
            }
            
            [XmlElement("omissions")]
            public int Omissions {
                get {
                    return omisiones;
                }
                set {
                    omisiones = value;
                }
            }
            
            [XmlElement("errors")]
            public int Errors {
                get {
                    return errores;
                }
                set {
                    errores = value;
                }
            }
            
            [XmlElement("corrects")]
            public int CorrectAnswers {
                get {
                    return aciertos;
                }
                set {
                    aciertos = value;
                }
            }
            
            public ResultsByLevel(int a, int e, int o, TimeSpan t)
            {
                aciertos = a;
                Errors = e;
                omisiones = o;
                Time = t;
            }
            
            public ResultsByLevel()
            {
                
            }
        }*/
        
        
        List <ExerciceExecutionResult<SingleResultsWordList>> wordListExecutionResult   ;
        
        [XmlElement("WordListExecutionResult")]
        public List<ExerciceExecutionResult<SingleResultsWordList>> WordListExecutionResult {
            get {return this.wordListExecutionResult;}
            set {wordListExecutionResult = value;}
        }
        
        
        public WordListResults ()
        {
            WordListExecutionResult = new List<ExerciceExecutionResult<SingleResultsWordList>>();
        }
        
        public void setResult(bool longTerm, int validTastksArg, int distractorTasksArg, int helpTasksArg, int userTasksArg, int helpcounterArg, int corretsArg, int failsArg, int omissionsArg, int levelArg, int timeelapsedArg)
        {
            //TODO
            SingleResultsWordList re = new SingleResultsWordList(longTerm,validTastksArg, distractorTasksArg, helpTasksArg, userTasksArg, helpcounterArg, corretsArg, failsArg, omissionsArg,  levelArg,  timeelapsedArg);
            wordListExecutionResult[wordListExecutionResult.Count -1].SingleResults.Add(re);
            
        }
        
        public override string ToString ()
        {
            string cadena = "";
            /*
            for(int i=0; i<results.Length; ++i)
            {
                Console.WriteLine("Nivel" + i); 
                Console.WriteLine("Aciertos: " + results[i].CorrectAnswers + " Errores =" + results[i].Errors + " Omisiones = " + results[i].Omissions + " Tiempo=" + results[i].Time);
            }
            */
            return cadena;
        }

    }
}

