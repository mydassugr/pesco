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
using Gtk;
using Gdk;

using System.Xml;
using System.Xml.Serialization;
using System.IO;
namespace pesco
{


	[XmlRoot("word-list-exercise")]
	public class WordListExercise: Exercise 
	{
        protected WordListPanelMemory p1;
        protected WordListCheckingPanel p2;
        protected VerbalMerorizationPanel panel2;

        /// <summary>
        /// 
        /// </summary>
        protected List<string> toMemorize = new List<string> ();

        [XmlElement("long-term")]
        public List<string> ToMemorize {
            get { return this.toMemorize; }
            //set { toMemorize = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        protected List<string> taskList;

        /// <summary>
        /// 
        /// </summary>
        protected int numAttempts = 0;

        protected bool longTerm = false;


        [XmlIgnore]
        public bool LongTerm {
            get { return this.longTerm; }
            set { longTerm = value; }
        }

        protected WordListResults res;
        protected WordListResults finalRes = XmlUtil.DeserializeForUser<WordListResults>(Configuration.Current.GetExerciseConfigurationFolderPath () + "/WordList.xml");

        
        protected int initialListSize = 5;

        [XmlElement("Results")]
        public WordListResults Res {
            get { return this.res; }
            set { res = value; }
        }
		// List for screening pre
		protected string[] listA = { "águila", "pavo", "buitre", "jazmín", "clavel", "orquídea", "licor", "anís", "coñac", "silla",
		"armario", "lámpara" };
		protected string[] listB = { "búho", "cuervo", "garza", "cómoda", "mesita", "vitrina", "arroz", "queso", "huevos", "falda",
		"pantalón", "calcetín" };
		
		// List for screening post
		protected string[] listPostA = { "gaviota", "loro", "pato", "azucena", "lirio", "tulipán", "zumo", "leche", "infusión", "sillón",
		"sofá", "cuna" };
		protected string[] listPostB = { "halcón", "cisne", "perdiz", "butaca", "librería", "aparador", "sartén", "tenedor", "taza", "piano",
		"violín", "guitarra" };	
		
     	protected uint maxAttempts = 2;
        protected double errorThreshold = 0.8;
        protected uint maxLevel = 5;

        protected int memorizationTime = 60;

        [XmlElement("memo-time")]
        public int MemorizationTime {
            get { return this.memorizationTime; }
            set { memorizationTime = value; }
        }

        [XmlElement("max-level")]
        public uint MaxLevel {
            get { return this.maxLevel; }
            set { maxLevel = value; }
        }

        [XmlElement("error-threshold")]
        public double ErrorThreshold {
            get { return this.errorThreshold; }
            set {
                if (value >= 0 && value <= 1)
                    errorThreshold = value;
            }
        }
        
	    protected string[] tasksPool = { "tomate", "pelota", "elemento", "dinosaurio", "autobús", "electricidad", "conejo", "factura", "compra", "farmacia",
		"médico", "paseo", "vecina", "cumpleaños", "horario", "hija", "caseta", "teclado", "macetas", "pescadería",
		"comprar", "aguador", "libro", "pasatiempos", "telediario", "carta", "taladro", "comida", "periódico", "botella",
		"jardines", "memoria", "gimnasia", "viajero", "lavadora", "dinero", "jirafa", "sábana", "pepino", "ternera" };

		[XmlElement("words-pool")]
		public string[] TasksPool {
			get { return this.tasksPool; }
			set { tasksPool = value; }
		}
       
        /// <summary>
        /// Level
        /// </summary>
        [XmlElement("current-level")]
        public int CurrentLevel {
            get { return this.nivel; }
            set { this.nivel = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        protected bool[] chosen;

        [XmlElement("chosen")]
        public bool[] Chosen {
            get { return this.chosen; }
            set { chosen = value; }
        } 
            /// <summary>
        /// 
        /// </summary>
        protected bool[] distractors;

        /// <summary>
        /// 
        /// </summary>
        protected DateTime startTime;

        /// <summary>
        /// 
        /// </summary>
        protected DateTime finishTime;

	    protected static WordListExercise ejercicio = null;

     
        public static WordListExercise GetInstance ()
        {
            if (ejercicio == null)
                ejercicio = new WordListExercise ();
            return ejercicio;
        }
    
        public static WordListExercise GetInstance (bool lt)
        {
            
            if (ejercicio == null)
                ejercicio = new WordListExercise ();
            ejercicio.longTerm = lt;
            
            return ejercicio;
        }
	    protected WordListExercise () 
		{	category = ExerciseCategory.Memory;
			chosen = new bool[tasksPool.Length];
			this.generaPoolTareas ();
			this.MemorizationTime = 60;
			initialListSize = 12;
            res = new WordListResults ();
            nivel = 1;
            finalRes.CategoryId=Convert.ToInt16(ExerciseCategory.Memory);
            finalRes.CurrentLevel=nivel;
            finalRes.ExerciseId= this.idEjercicio();
            
            ExerciceExecutionResult <SingleResultsWordList> exerciceExecution =  new ExerciceExecutionResult<SingleResultsWordList>(SessionManager.GetInstance().CurSession.IdSession, SessionManager.GetInstance().CurExecInd);
            finalRes.WordListExecutionResult.Add(exerciceExecution);
		}
	
#region Interfaz Ejercicio
		public override bool inicializar ()
		{
			return true;
		}
      
        
      public override void iniciar ()
        {
            
            if (!longTerm){            
                ExercisePanelWordList demowl = new ExercisePanelWordList(this);
                SessionManager.GetInstance().ReplacePanel(demowl);
                demowl.InitPanel();}
            else{
                ExercisePanelWordListLong demowll = new ExercisePanelWordListLong(this);
                SessionManager.GetInstance().ReplacePanel(demowll);
                demowll.InitPanel();}
          
            
        }
         public void iniciarCheckMicro() {
           
           CheckMicrofone checkMicro = new CheckMicrofone(this );
           SessionManager.GetInstance().ReplacePanel(checkMicro);
            
        }
         public void iniciarExercise() {
        // Check if we are in screening post ( session id = 1 ) or
            // in screening post ( session id = 11 ). For security we check if cur session
            // is greater than 6 to use one or other list
            // TODO: Check words for screening post
            if ( SessionManager.GetInstance().CurSession.IdSession > 6 ) {
                listA = listPostA;
                listB = listPostB;
				
            }
            
            // if we're not in the longterm department...
            if (!longTerm) {
                toMemorize.Clear ();
                toMemorize.AddRange (listA);
				WordListExercise.Shuffle<string>(toMemorize);
                CreatePanelMemory();
            }
        }
        public void iniciarExerciseLong () {
            // Check if we are in screening post ( session id = 1 ) or
            // in screening post ( session id = 11 ). For security we check if cur session
            // is greater than 6 to use one or other list
            // TODO: Check words for screening post
            if ( SessionManager.GetInstance().CurSession.IdSession > 6 ) {
                listA = listPostA;
                listB = listPostB;
            }
            for (int i = 0; i < chosen.Length; ++i)
                chosen[i] = false;
            this.generaListaPalabras ();
         
            //CreatePanelSelection();
            CreatePanelSpeakLong();
            this.maxAttempts = 1;
        }
       
  
        public bool tieneDemo ()
        {
            return true;
        }

        public bool tieneEnsayo ()
        {
            return false;
        }
        public override int idEjercicio ()
        {
            return 8;
        }
         
        public override void finalizar ()
        {
            
            //if (!this.longTerm)
               // p1.StopTimer ();
            
				
			/*longTerm = true;
            
            if (nivel < 3)
                medalValue = 0;
            else if (nivel >= 3 && nivel < 5)
                medalValue = 60;
            else
                medalValue = 100;
			
			this.Serialize (medalValue);
                      
            
            
            SessionManager.GetInstance ().ExerciseFinished (medalValue);
            SessionManager.GetInstance ().TakeControl ();*/
		
        }
		public void finishExercice(){
			longTerm = true;
            
            if (nivel < 3)
                medalValue = 0;
            else if (nivel >= 3 && nivel < 5)
                medalValue = 60;
            else
                medalValue = 100;
			
			this.Serialize (medalValue);
            SessionManager.GetInstance ().ExerciseFinished (medalValue);
            SessionManager.GetInstance ().TakeControl ();
		}

        public override void pausa ()
        {
            throw new System.NotImplementedException ();
        }
        #endregion
        
        #region Generación Tareas
        
		public  void generaListaPalabras ()
		{
			this.taskList = new List<string> ();
			Random r = new Random (DateTime.Now.Millisecond);
			int total = 0;
			taskList.AddRange (listA);
			taskList.AddRange (listB);
			toMemorize = new List<string> ();
			toMemorize.AddRange (listA);
			total = 0;
			int objetivo = toMemorize.Count;
			
			int pos;
			
			// TODO: We are adding listA+listB+12 random words... Why?
			do {
				pos = r.Next (0, tasksPool.Length);
				ListUtils.ShuffleArray<string>(listA);
				
				if (!this.chosen[pos] && !this.distractors[pos]) {
					taskList.Add (tasksPool[pos]);
					distractors[pos] = true;
					total++;
				}
			} while (total < objetivo);
			
			Shuffle<string> (taskList);
		}
        public static void Shuffle<T> (IList<T> list)
        {
            Random rng = new Random ();
            int n = list.Count;
            while (n > 1) {
                n--;
                int k = rng.Next (n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

	    public void generaPoolTareas ()
        {
            
            distractors = new bool[tasksPool.Length];
            
            for (int i = 0; i < chosen.Length; ++i) {
                chosen[i] = false;
                distractors[i] = false;
            }
        }
	
        public  void generaListaDeTareasAMemorizar ()
        {
            int total = 0;
            
            Random r = new Random (DateTime.Now.Millisecond);
            
            toMemorize = new List<string> ();
            int pos;
            
            do {
                pos = r.Next (0, tasksPool.Length);
                if (!this.chosen[pos]) {
                    chosen[pos] = true;
                    toMemorize.Add (tasksPool[pos]);
                    total++;
                }
            } while (total < initialListSize);
            
        }
        #endregion
        
        #region Open Panels

        //Panel repetir recados
        public void CreatePanelDemo1(string itext1, string itext2, bool icolor){
            ExercisePanelWordList1 demowl1 = new ExercisePanelWordList1();
            
            demowl1.BotonEmpezarSecuencia.Clicked += delegate{
                    if (!this.longTerm) {
                     this.CreatePanelMemory();
                        //p1.StartTimer (WordListExercise.AccionTemporizador);
                    } else {
                        CreatePanelSpeak();
                    //    SessionManager.GetInstance().ReplacePanel(p2);
                      //  p2.Show ();
                    }
               };
            demowl1.ShowLabel(itext1, itext2);
            if (icolor)
                demowl1.SetLabel1Blue();
            SessionManager.GetInstance().ReplacePanel(demowl1);
          
        }
       
        
        public void CreatePanelMemory(){
            // nummAtemmts==3, show the red list
            p1 = new WordListPanelMemory(toMemorize, this.MemorizationTime, (numAttempts==3)? true:false);
            p1.Padre=this;
            SessionManager.GetInstance().ReplacePanel(p1);
       }
        public void CreatePanelSpeak(){
             panel2 = new VerbalMerorizationPanel();
            //p2 = new ListCheckingPanel(ejercicio.taskList);
            ejercicio.panel2.BotonListo.Clicked += new EventHandler (ejercicio.OnClickBotonListo);
            ejercicio.panel2.RecordButton.Clicked += delegate {
                AudioManager.Filename = Configuration.Current.GetConfigurationFolderPath () + "/lista_palabras-" + DateTime.Now.Millisecond + ".wav";
                AudioManager.StartAudioRecording ();
                ejercicio.panel2.activateP1();
             };
            SessionManager.GetInstance().ReplacePanel(panel2);
        }
          public void CreatePanelSpeakLong(){
             panel2 = new VerbalMerorizationPanel();
            ejercicio.panel2.BotonListo.Clicked +=  delegate{ 
                AudioManager.StopAudioRecording();
                CreatePanelSelection();
            };
            ejercicio.panel2.RecordButton.Clicked += delegate {
                AudioManager.Filename = Configuration.Current.GetConfigurationFolderPath () + "/lista_palabras-" + DateTime.Now.Millisecond + ".wav";
                AudioManager.StartAudioRecording ();
                ejercicio.panel2.activateP1();
             };
            SessionManager.GetInstance().ReplacePanel(panel2);
        }
    
         public void CreatePanelSelection(){
            p2 = new WordListCheckingPanel(ejercicio.taskList);
            ejercicio.p2.BotonListo.Clicked += new EventHandler (ejercicio.OnClickBotonListo);
            ejercicio.startTime = DateTime.Now;
            SessionManager.GetInstance().ReplacePanel(p2);
        }
          
        #endregion
        
        #region Events
        protected void OnClickBotonListo (object sender, System.EventArgs e)
		{
			AudioManager.StopAudioRecording ();
			
			if (this.longTerm) {
			
				List<string> selected = this.p2.GetSelectedItems ();
                int correct = 0;
    			int errors = 0;
    			int omissions = 0;
    			int totalToRemember = toMemorize.Count;
    			
    			foreach (string s in toMemorize)
    				if (selected.Contains (s))
    					correct++;
    			
    			foreach (string s in selected)
    				if (!toMemorize.Contains (s))
    					errors++;
    			
				omissions = totalToRemember - correct;
				finishTime = DateTime.Now;
				TimeSpan timespan= finishTime -startTime;
				
				this.finalRes.setResult(LongTerm,totalToRemember, getDistractorNum(), 0, getUserTaskNum(),0, correct, errors, totalToRemember - correct,nivel,Convert.ToInt16( timespan.TotalSeconds)); 
				
    		
                if (correct > 9)
    				nivel = 6;
    			else if (correct > 6)
    				nivel = 4;
    			else
    				nivel = 1;
    				this.finishExercice ();
    				return;
                
			} else {
				
				
				numAttempts++;
                
				string message1;
				string message2;
                
				
				// mostramos la lista B, cuando ya se ha mostrado la A dos veces
				if (numAttempts == 3) {
					toMemorize.Clear ();
					toMemorize.AddRange (this.listB);
					WordListExercise.Shuffle<string>(toMemorize);
                   // message = "Ahora te voy a presentar una lista de palabras totalmente diferente, intenta recordarlas. En esta ocasión la lista será de color rojo.";
                    
                    message1 ="Muy bien, ahora te mostraré una lista nueva.";
                    message2 ="Será de color rojo. Memorizala y repite\n"+
                              "todas las palabras. El orden no importa.";
                    CreatePanelDemo1(message1,message2,false);   
                 //	p1 = new TaskListPanel (toMemorize, this.MemorizationTime, true);
					
					
					// si ya hemos mostrado la lista B, c'est fini!
				} else if (numAttempts > 3) {
					nivel = 4;
					this.finishExercice ();
					return;
				} else {			
					TaskListExercise.Shuffle<string>(toMemorize);
				//	p1 = new TaskListPanel (toMemorize, this.MemorizationTime);
					//message = "Ahora volveré a presentarte las mismas palabras, intenta recordar las que puedas, aunque las hayas dicho antes.";
                    message1 = "Te mostraré de nuevo la misma lista";
                    message2 = "Memorizala y vuelve a repetir todas las\npalabras. El orden no importa.";
                    CreatePanelDemo1(message1, message2,true);
				}
	//LO HE PUESTO EN CREATE PANEL DEMO1			
			/*	p1.ButtonNextExercise.Clicked += delegate {
					if (!this.longTerm) {
						p1.StartTimer (WordListExercise.AccionTemporizador);
					} else {
						SessionManager.GetInstance().ReplacePanel(p2);
						p2.Show ();
					}
					
					
				};*/
			
			//	SessionManager.GetInstance().ReplacePanel(p1);
				
			}
		}
        #endregion
        
        #region Timers
	/*	public static void AccionTemporizador ()
		{
			WordListExercise e = WordListExercise.GetInstance () as WordListExercise;
			
			if (e == null)
				return;
            
			VerbalMerorizationPanel vmp = new VerbalMerorizationPanel ();
			SessionManager.GetInstance().ReplacePanel(vmp);
			
			vmp.BotonListo.Clicked += e.OnClickBotonListo;
			
			vmp.ShowAll();
			
			vmp.RecordButton.Clicked += delegate {
				AudioManager.Filename = Configuration.Current.GetConfigurationFolderPath () + "/lista_palabras-" + DateTime.Now.Millisecond + ".wav";
				AudioManager.StartAudioRecording ();
				vmp.BotonListo.Sensitive = true;
				vmp.RecordButton.Sensitive = false;
				e.startTime = DateTime.Now;
			};
		}
      */ 
		public int getDistractorNum(){
			int cont =0;
			
			foreach ( bool isDistractor in distractors)
			{
				if (isDistractor)
					cont++;
			}
			
			return cont;
		}
		
		public int getValidTaskNum(){
			
			return toMemorize.Count;
		}
		public int getUserTaskNum(){
			
			return 	p2.GetSelectedItems().Count;
		}
		
		protected void SetFinalResult(int scoreArg){
			
			finalRes.WordListExecutionResult[finalRes.WordListExecutionResult.Count -1].TotalCorrects =0;
			finalRes.WordListExecutionResult[finalRes.WordListExecutionResult.Count -1].TotalFails =0;
			finalRes.WordListExecutionResult[finalRes.WordListExecutionResult.Count -1].TotalTimeElapsed =0;
			finalRes.WordListExecutionResult[finalRes.WordListExecutionResult.Count -1].Score=scoreArg;
			
			foreach(SingleResultsWordList sr in finalRes.WordListExecutionResult[finalRes.WordListExecutionResult.Count -1].SingleResults){
				
				finalRes.WordListExecutionResult[finalRes.WordListExecutionResult.Count -1].TotalCorrects += sr.Correts;
				finalRes.WordListExecutionResult[finalRes.WordListExecutionResult.Count -1].TotalFails += sr.Fails;
				finalRes.WordListExecutionResult[finalRes.WordListExecutionResult.Count -1].TotalTimeElapsed += sr.Timeelapsed;
				
			}
		}
        #endregion


		#region XML serialization

		protected static string xmlUserFile = "word-list.xml";

		public  void Serialize (int scoreArg)
		{
			SetFinalResult(scoreArg);
			XmlUtil.SerializeForUser<WordListResults>(finalRes,Configuration.Current.GetExerciseConfigurationFolderPath () + "/WordList.xml");
			
			string fullPath = Configuration.Current.GetExerciseConfigurationFolderPath () + "/" + xmlUserFile;
			
			XmlTextWriter escritor = new XmlTextWriter (fullPath, null);
			
			try {
				escritor.Formatting = Formatting.Indented;
				
				escritor.WriteStartDocument ();
				
				escritor.WriteDocType ("word-list-exercise", null, null, null);
				
				// hoja de estilo para pode ver en un navegador el xml
				//escritor.WriteProcessingInstruction("xml-stylesheet", "type='text/xsl' href='cuestionario.xsl'");
				
				XmlSerializer serializer = new XmlSerializer (typeof(WordListExercise));
				
				XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces ();
				
				namespaces.Add ("", "");
				
				serializer.Serialize (escritor, this, namespaces);
				
				escritor.WriteEndDocument ();
				escritor.Close ();
			} catch (Exception e) {
				escritor.Close ();
				Console.WriteLine ("\n\nError al serializar" + e.Message + "\n\n");
			}
		}
        
	public override string NombreEjercicio ()
        {
            return "Lista de Palabras";
        }
    
   
	public static WordListExercise Deserialize ()
		{
			
			string fullPath = Configuration.Current.GetExerciseConfigurationFolderPath () + "/" + xmlUserFile;
			
			// Console.WriteLine ("Deserializando fichero");
			
			if (!File.Exists (fullPath)) {
				GetInstance ();
				ejercicio.Serialize (0);
				// Console.WriteLine ("Fichero no existe => creando fichero");
				
				return ejercicio as WordListExercise;
			}
			
			XmlTextReader lector = new XmlTextReader (fullPath);
			
			try {
				ejercicio = new WordListExercise ();
				
				XmlSerializer serializer = new XmlSerializer (typeof(WordListExercise));
				ejercicio = (WordListExercise)serializer.Deserialize (lector);
				
				lector.Close ();
				
				/*foreach (string s in (ejercicio as WordListExercise).listA)
					Console.WriteLine (s);*/
				
				return (WordListExercise)ejercicio;
			} catch (Exception e) {
				lector.Close ();
				Console.WriteLine ("Error al deserializar" + e.Message);
				//Gtk.Application.Quit();
				return null;
			}
		}
		#endregion
		
	}
}

