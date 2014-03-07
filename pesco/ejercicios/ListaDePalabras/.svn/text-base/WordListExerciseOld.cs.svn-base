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
namespace ecng
{


    [XmlRoot("word-list-exercise")]
    public class  WordListExerciseOld: TaskListExerciseOld
    {
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
        
        protected VerbalMerorizationPanel panel2;


        string[] poolPalabras = { "tomate", "pelota", "elemento", "dinosaurio", "autobús", "electricidad", "conejo", "factura", "compra", "farmacia",
        "médico", "paseo", "vecina", "cumpleaños", "horario", "hija", "caseta", "teclado", "macetas", "pescadería",
        "comprar", "aguador", "libro", "pasatiempos", "telediario", "carta", "taladro", "comida", "periódico", "botella",
        "jardines", "memoria", "gimnasia", "viajero", "lavadora", "dinero", "jirafa", "sábana", "pepino", "ternera" };

        [XmlElement("words-pool")]
        public string[] PoolPalabras {
            get { return this.poolPalabras; }
            set { poolPalabras = value; }
        }

        protected WordListExerciseOld () : base()
        {
            category = ExerciseCategory.Memory;
            chosen = new bool[poolPalabras.Length];
            this.tasksPool = this.poolPalabras;
            this.generaPoolTareas ();
            this.MemorizationTime = 60;
            initialListSize = 12;
        }

        public static TaskListExerciseOld GetInstance ()
        {
            if (ejercicio == null)
                ejercicio = new WordListExerciseOld ();
            
            return ejercicio;
        }

        public static TaskListExerciseOld GetInstance (bool lt)
        {
            if (ejercicio == null)
                ejercicio = new WordListExerciseOld ();
            
            ejercicio.LongTerm = lt;
            return ejercicio;
        }

        public override bool inicializar ()
        {
            // Check if we are in screening post ( session id = 1 ) or
            // in screening post ( session id = 11 ). For security we check if cur session
            // is greater than 6 to use one or other list
            // TODO: Check words for screening post
            if ( SessionManager.GetInstance().CurSession.IdSession > 6 ) {
                Console.WriteLine("Usando palabras de la lista Screening POST");
                listA = listPostA;
                listB = listPostB;
            }
            
            // if we're not in the longterm department...
            if (!longTerm) {
                toMemorize.Clear ();
                toMemorize.AddRange (listA);
                
                p1 = new TaskListPanel (toMemorize, this.MemorizationTime);                             
                
                /* Adapting exercise to single window */
                // ventana = new Gtk.Window ("Lista de la Palabras");               
                // ventana.WidthRequest = 550;
                // ventana.WindowPosition = WindowPosition.CenterAlways;
                // res.Results = new TaskListResults.ResultsByLevel[100];
                // ventana.DeleteEvent += this.OnDeleteWindow;
                
                return true;
            } else {
                // ventana = new Gtk.Window ("Recuerdo a largo plazo: Lista de la Palabras");
                
                // ventana.WidthRequest = 550;
                // ventana.WindowPosition = WindowPosition.CenterAlways;
                p1 = new TaskListPanel (toMemorize, this.MemorizationTime);
                // ventana.DeleteEvent += this.OnDeleteWindow;
                for (int i = 0; i < chosen.Length; ++i)
                    chosen[i] = false;
                this.generaListaPalabras ();
                p2 = new ListCheckingPanel (taskList);
                this.p2.BotonListo.Clicked += this.OnClickBotonListo;
                            
                
                this.maxAttempts = 1;
                return true;
            }
        }

        public override void generaListaPalabras ()
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

        public override void iniciar ()
        {
            
            
            if (!longTerm)
                this.textoIntro = "Lista de Palabras\n\nEste ejercicio te permitirá medir cómo está tu memoria.";
            else {
                this.textoIntro = "Lista de Palabras\n\nAhora vamos a ver si recuerdas la lista que memorizaste hace un rato.";
                }
                
            demoPanel = new DemoPanel (this.TextoIntro);
            // ventana.Maximize ();
            // ventana.Add (demoPanel);
            /* Adapting exercise to single window */
            SessionManager.GetInstance().ReplacePanel(demoPanel);
            demoPanel.Show();
            
            // mostramos la ventana
            // ventana.Show ();
            
            demoPanel.ButtonNext.Clicked += delegate {
                /* Adapting exercise to single window */
                // ventana.Remove(demoPanel);
                // ventana.Add (p1);
                SessionManager.GetInstance().ReplacePanel(p1);
                
                Pixbuf pb = null;
                // Pifbux instructions is not showed anymore
                /* 
                if (!longTerm)
                    pb = Pixbuf.LoadFromResource ("ecng.ejercicios.ListaDePalabras.lista_palabras.png");
                */
                if (!longTerm){
                    this.textoIntro = "<span color=\"blue\">Voy a mostrarte durante un minuto una lista con 12 palabras en color azul " +
                        "que tienes que memorizar. Después tendrás que decir en voz alta las " +
                        "palabras que recuerdes, el orden no importa. Para comenzar el ejercicio, pulsa el botón <span color=\"black\"><b>Siguiente</b></span></span>.";
                    
                    p1.SetInstructions (this.textoIntro, pb);
                }
                else {
                    this.textoIntro = "<span color=\"blue\"><big>Intenta pulsar todas las palabras de la primera lista, la que te mostré tres veces y era de color azul. " + 
                        "\n\nTen cuidado, también estarán las palabras de la segunda lista, que sólo te he mostrado una vez y era de color rojo y que no tienes que pulsar.\n\n"+
                            "Te mostraré las palabras en varias páginas, para pasar de una página a otra pulsa los botones " + 
                        "<span color=\"black\"><b>Siguiente</b></span> y <span color=\"black\"><b>Anterior</b></span>. \n\nCuando creas que hayas marcado todas las palabras de la lista, pulsa <span color=\"black\"><b>¡Listo!</b></span>. \n Para comenzar el ejercicio, pulsa <span color=\"black\"><b>Siguiente</b></span></big></span>";
                    
                    p1.SetInstructionsSmall(this.textoIntro, pb);
                }
                /* Adapting exercise to single window */
                // ventana.Maximize ();
                //ventana.Show ();
                
                p1.ShowStartExercisesButton ();
                
                p1.ButtonStartExercises.Clicked += delegate {
                    
                    if (!this.longTerm) {
                        //ventana.Add(p1);
                        p1.StartTimer (WordListExerciseOld.AccionTemporizador);
                    } else {
                        /* Adapting exercise to single window */
                        // ventana.Remove (p1);
                        // ventana.Add (p2);
                        SessionManager.GetInstance().ReplacePanel(p2);
                        p2.Show ();
                    }
                    /* Adapting exercise to single window */
                    // ventana.Maximize ();
                    // ventana.Show ();
                };
                
            };
            
        }



        protected override void OnClickBotonListo (object sender, System.EventArgs e)
        {
            AudioManager.StopAudioRecording ();
            
            if (this.longTerm) {
                
           //     List<string> selected = this.panel2.GetSelectedItems ();
              List<string> selected = toMemorize;
            
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
            
            //TaskListResults tasklistresult=  new TaskListResults();
            //tasklistresult.setResultsForLevel (20, correct, errors, omissions, startTime - finishTime);
            //this.res.Add(tasklistresult);
            //this.res.setResultsForLevel (correct, errors, omissions, startTime - finishTime);
            
            // Console.WriteLine (res.ToString ());
                
            
            if (correct > 9)
                nivel = 6;
            else if (correct > 6)
                nivel = 4;
            else
                nivel = 1;
            
                
                this.finalizar ();
                
                return;
            } else {
                
                finishTime = DateTime.Now;
                numAttempts++;
                
                string message;
                
                // mostramos la lista B, cuando ya se ha mostrado la A dos veces
                if (numAttempts == 3) {
                    toMemorize.Clear ();
                    toMemorize.AddRange (this.listB);
                    
                    
                    TaskListExercise.Shuffle<string>(toMemorize);
                    p1 = new TaskListPanel (toMemorize, this.MemorizationTime, true);
                    message = "<span color=\"blue\">Ahora te voy a presentar una lista de palabras totalmente diferente, intenta recordarlas. En esta ocasión la lista será de color rojo.\n\nPara comenzar, pulsa el botón <span color=\"black\"><b>Empezar ejercicio</b></span></span>.";
                    
                    // si ya hemos mostrado la lista B, c'est fini!
                } else if (numAttempts > 3) {
                    nivel = 4;
                    this.finalizar ();
                    return;
                } else {            
                    TaskListExercise.Shuffle<string>(toMemorize);
                    p1 = new TaskListPanel (toMemorize, this.MemorizationTime);
                    message = "<span color=\"blue\">Ahora volveré a presentarte las mismas palabras, intenta recordar las que puedas, aunque las hayas dicho antes.\n\nPara comenzar, pulsa el botón <span color=\"black\"><b>Empezar ejercicio</b></span></span>.";
                }
                
                p1.ShowLeftPanel ();
                p1.SetInstructions (message, null);
                p1.ShowNextExerciseButton ();
                p1.ButtonNextExercise.Clicked += delegate {
                    if (!this.longTerm) {
                        //ventana.Add(p1);
                        p1.StartTimer (WordListExerciseOld.AccionTemporizador);
                    } else {
                        /* Adapting exercise to single window */
                        // ventana.Remove (p1);
                        // ventana.Add (p2);
                        SessionManager.GetInstance().ReplacePanel(p2);
                        p2.Show ();
                    }
                    
                    // ventana.Maximize ();
                    // ventana.Show ();
                };
                
                /* Adapting exercise to single window */
                // ventana.Remove (ventana.Child);                              
                // ventana.Add (p1);
                // ventana.Show ();
                SessionManager.GetInstance().ReplacePanel(p1);
                
            }
        }

        public static void AccionTemporizador ()
        {
            WordListExerciseOld e = WordListExerciseOld.GetInstance () as WordListExerciseOld;
            
            if (e == null)
                return;
            /* Adapting exercise to single window */
            // if (e.ventana.Child != null) 
            //  e.ventana.Remove (e.ventana.Child);
            
            VerbalMerorizationPanel vmp = new VerbalMerorizationPanel ();
            
            /* Adapting exercise to single window */
            // e.ventana.Add (vmp);
            SessionManager.GetInstance().ReplacePanel(vmp);
            
            vmp.BotonListo.Clicked += e.OnClickBotonListo;
            
            /* Adapting exercise to single window */
            // e.ventana.ShowAll ();
            vmp.ShowAll();
            
            vmp.RecordButton.Clicked += delegate {
                AudioManager.Filename = Configuration.Current.GetConfigurationFolderPath () + "/lista_palabras-" + DateTime.Now.Millisecond + ".wav";
                AudioManager.StartAudioRecording ();
                vmp.BotonListo.Sensitive = true;
                vmp.RecordButton.Sensitive = false;
                e.startTime = DateTime.Now;
            };
        }

        public override int idEjercicio ()
        {
            return 8;
        }

        public override void generaListaDeTareasAMemorizar ()
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


        #region XML serialization

        protected static string xmlUserFile = "word-list.xml";

        public override void Serialize ()
        {
            string fullPath = Configuration.Current.GetExerciseConfigurationFolderPath () + "/" + xmlUserFile;
            
            XmlTextWriter escritor = new XmlTextWriter (fullPath, null);
            
            try {
                escritor.Formatting = Formatting.Indented;
                
                escritor.WriteStartDocument ();
                
                escritor.WriteDocType ("word-list-exercise", null, null, null);
                
                // hoja de estilo para pode ver en un navegador el xml
                //escritor.WriteProcessingInstruction("xml-stylesheet", "type='text/xsl' href='cuestionario.xsl'");
                
                XmlSerializer serializer = new XmlSerializer (typeof(WordListExerciseOld));
                
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

        public static WordListExerciseOld Deserialize ()
        {
            
            string fullPath = Configuration.Current.GetExerciseConfigurationFolderPath () + "/" + xmlUserFile;
            
            // Console.WriteLine ("Deserializando fichero");
            
            if (!File.Exists (fullPath)) {
                GetInstance ();
                ejercicio.Serialize ();
                Console.WriteLine ("Fichero no existe => creando fichero");
                
                return ejercicio as WordListExerciseOld;
            }
            
            XmlTextReader lector = new XmlTextReader (fullPath);
            
            try {
                ejercicio = new WordListExerciseOld ();
                
                XmlSerializer serializer = new XmlSerializer (typeof(WordListExerciseOld));
                ejercicio = (WordListExerciseOld)serializer.Deserialize (lector);
                
                lector.Close ();
                
                /*foreach (string s in (ejercicio as WordListExercise).listA)
                    Console.WriteLine (s);*/
                
                return (WordListExerciseOld)ejercicio;
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

