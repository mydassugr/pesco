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

using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace pesco
{

	[XmlRoot("lost-objects-exercise")]
	public class EjercicioObjetosClasificables : Exercise
	{
		#region XML
		protected static string xmlUserFile = "lost-objects.xml";
		

		public static EjercicioObjetosClasificables Deserialize ()
		{
			string fullPath = Configuration.Current.GetExerciseConfigurationFolderPath () + "/" + xmlUserFile;
			
			if (!File.Exists (fullPath)) {
				GetInstance ();
				ejercicio.Serialize ();
				return ejercicio;
				/*string s = Environment.CommandLine;			
				fullPath = Configuration.CommandDirectory + "/ejercicios/ObjetosClasificables/xml-templates/" + xmlUserFile;*/				
			}			
			
			XmlTextReader lector = new XmlTextReader (fullPath);
			
			try {
				ejercicio = new EjercicioObjetosClasificables ();
				
				XmlSerializer serializer = new XmlSerializer (typeof(EjercicioObjetosClasificables));
				ejercicio = (EjercicioObjetosClasificables)serializer.Deserialize (lector);
				
				lector.Close ();
				return (EjercicioObjetosClasificables)ejercicio;
			} catch (Exception e) {
				lector.Close ();
				return null;
			}
		}


		public virtual void Serialize ()
		{
			if(DemoExecuted){
				SetFinalResult();
				XmlUtil.SerializeForUser<ResultadosEjercicioObjetosClasificables>(finalRes,Configuration.Current.GetExerciseConfigurationFolderPath () + "/ClasificableObjects.xml");
			}
			/*string fullPath = Configuration.Current.GetExerciseConfigurationFolderPath () + "/" + xmlUserFile;
			
			XmlTextWriter escritor = new XmlTextWriter (fullPath, null);
			
			try {
				escritor.Formatting = Formatting.Indented;
				
				escritor.WriteStartDocument ();
				
				escritor.WriteDocType ("lost-objects-exercise", null, null, null);
							
				XmlSerializer serializer = new XmlSerializer (typeof(EjercicioObjetosClasificables));
				
				XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces ();
				
				namespaces.Add ("", "");
				
				serializer.Serialize (escritor, this, namespaces);
				
				escritor.WriteEndDocument ();
				escritor.Close ();
			} catch (Exception e) {
				escritor.Close ();
				Console.WriteLine ("Error al serializar" + e.Message);
			}*/
		}
		#endregion

		#region Objetos
		public static SortedDictionary<string, string[]> objetosPorCategoria = null;

		protected List<string> objetosEscogidos;
		protected List<string> distractoresEscogidos;
		protected List<string> objetosAMostrar;
		protected List<int> categoriasSeleccionadas;
		protected List<string> categoriasSeleccionadas2 = new List<string> ();
		private Dictionary<string, List<string>> selectedItemsByCategory;
		
		#endregion


		// protected uint maxRep = 2;
		protected uint rep = 0;

		#region GUI
	//	protected PanelObjetosClasificables panel = new PanelObjetosClasificables ();
		protected PanelObjetosClasificablesSelection panelSelection;
		protected PanelObjetosClasificablesMemory panelMemory;
		protected Gtk.Window ventana = new Gtk.Window ("Memoriza las Imágenes");
		protected TrialPanel tp;
        protected ExercisePanelObjetosClasificables ExercisePanel;
		#endregion

		#region Parámetros Nivel
		int numObjetos = 9;
        double auxscore;
        
        int[] TotalMedal={0,0,0};
        
		uint numColumnasD, numColumnasI, numFilasI, numFilasD;

	//	double tiempo;

		int numCategorias;
		int numObjetosPorCategoria;
		
		int numCorrectas = 0;

		#endregion

		bool demoExecuted = false;
		[XmlElement("demoExecuted")]
		public bool DemoExecuted {
			get {return this.demoExecuted;}
			set {demoExecuted = value;}
		}
		
		#region Resultados
		ResultadosEjercicioObjetosClasificables res = new ResultadosEjercicioObjetosClasificables();
		
		[XmlIgnoreAttribute]
		ResultadosEjercicioObjetosClasificables finalRes = XmlUtil.DeserializeForUser<ResultadosEjercicioObjetosClasificables>(Configuration.Current.GetExerciseConfigurationFolderPath () + "/ClasificableObjects.xml");

		[XmlElement("Results")]
		public ResultadosEjercicioObjetosClasificables Res {
			get { return this.res; }
			set { res = value; }
		}
        
		DateTime tiempoInicio;
		DateTime tiempoFin;
		#endregion

		#region Singleton

		protected static EjercicioObjetosClasificables ejercicio = null;

		private EjercicioObjetosClasificables ()
		{
			
			category = ExerciseCategory.Memory;
			
			objetosPorCategoria = new SortedDictionary<string, string[]> ();
			
			//1. Categoría comida
			string[] comidas = { "2316.png", "2494.png", "2838.png", "2865.png", "6646.png", "6918.png", "7000.png", "7130.png", "tomatefrito.png", "2446.png",
			"2594.png", "2839.png", "6244.png", "6911.png", "6976.png", "7001.png", "8600.png" };
			objetosPorCategoria.Add ("comida", comidas);
			
			//2. Categoría cuerpo
			string[] partesDelCuerpo = { "12218.png", "2663.png", "2669.png", "2841.png", "2851.png", "2871.png", "2887.png", "2928.png", "3298.png", "6573.png",
			"8666.png", "cuello.png" };
			objetosPorCategoria.Add ("cuerpo", partesDelCuerpo);
			
			//3. Categoría herramientas
			string[] herramientas = { "2706.png", "2788.png", "2837.png", "2937.png", "3392.png", "5586.png", "brocha.png", "sierra.png", "2736.png", "2809.png",
			"2922.png", "2938.png", "5499.png", "alicates.png", "clavo.png" };
			objetosPorCategoria.Add ("herramientas", herramientas);
			
			//4. Categoría transporte
			string[] transporte = { "2263.png", "2264.png", "2273.png", "2277.png", "2339.png", "2407.png", "2506.png", "2508.png", "monopatin.png", "parapente.png",
			"taxi.png", "tren.png" };
			objetosPorCategoria.Add ("transporte", transporte);
			
			//5. Categoría deportes
			string[] deportes = { "10159.png", "2889.png", "3097.png", "3260.png", "5917.png", "8544.png", "9189.png", "9699.png", "10167.png", "3090.png",
			"3199.png", "5398.png", "6513.png", "8591.png", "9190.png" };
			objetosPorCategoria.Add ("deportes", deportes);
			
			//6. Categoria fruta
			string[] frutas = { "13644.png", "2400.png", "2483.png", "2955.png", "ciruela.png", "uvas.png", "13645.png", "2462.png", "2525.png", "2983.png",
			"mandarina.png", "2329.png", "2468.png", "2561.png", "3022.png", "platano.png" };
			objetosPorCategoria.Add ("fruta", frutas);
			
			//7. Categoria instrumentos
			string[] instrumentos = { "11121.png", "17332.png", "2495.png", "2604.png", "5974.png", "8315.png", "8599.png", "12088.png", "2318.png", "2521.png",
			"2607.png", "6234.png", "8340.png", "12119.png", "2396.png", "2559.png", "2615.png", "6235.png", "8341.png", "12150.png",
			"2417.png", "2578.png", "5909.png", "6249.png", "8493.png" };
			objetosPorCategoria.Add ("instrumentos", instrumentos);
			
			//8. Categoria muebles
			string[] muebles = { "11376.png", "2304.png", "2570.png", "2917.png", "4960.png", "lampara.png", "silla.png", "2258.png", "2360.png", "2571.png",
			"4937.png", "escritorio.png", "pupitre.png" };
			objetosPorCategoria.Add ("muebles", muebles);
			
			//11. Categoria profesiones
			string[] profesiones = { "11165.png", "11342.png", "2636.png", "2740.png", "3008.png", "5603.png", "11265.png", "2457.png", "2642.png", "2921.png",
			"3321.png", "8631.png", "11341.png", "2467.png", "2733.png", "2969.png", "3358.png", "9196.png" };
			objetosPorCategoria.Add ("profesiones", profesiones);
			
			//10. Categoria ropa
			string[] ropa = { "11402.png", "2308.png", "2391.png", "2622.png", "4872.png", "12176.png", "2309.png", "2565.png", "2775.png", "7123.png",
			"16585.png", "2310.png", "2613.png", "3296.png", "8353.png" };
			objetosPorCategoria.Add ("ropa", ropa);
			
			objetosEscogidos = new List<string> ();
			distractoresEscogidos = new List<string> ();
			objetosAMostrar = new List<string> ();
			
			categoriasSeleccionadas = new List<int> ();
			nivel = 0;
			
			//final result inicialize
			finalRes.CategoryId=Convert.ToInt16(ExerciseCategory.Memory);
			finalRes.CurrentLevel=nivel;
			finalRes.ExerciseId= this.idEjercicio();
			
			ExerciceExecutionResult <SingleResultObjetosClasificables> exerciceExecution =  new ExerciceExecutionResult<SingleResultObjetosClasificables>(SessionManager.GetInstance().CurSession.IdSession, SessionManager.GetInstance().CurExecInd);
			finalRes.ObjetosClasificablesExecutionResults.Add(exerciceExecution);
         	
		}

		public static EjercicioObjetosClasificables GetInstance ()
		{
			if (ejercicio == null)
				ejercicio = new EjercicioObjetosClasificables ();
			return ejercicio;
		}
		#endregion

		#region Generación Objetos

		protected string GetCategoriaPorID (int id)
		{
			
			switch (id) {
			case 1:
				return "comida";
			
			case 2:
				return "cuerpo";
			
			case 3:
				return "herramientas";
			
			case 4:
				return "transporte";
			
			case 5:
				return "deportes";
			
			case 6:
				return "fruta";
			
			case 7:
				return "instrumentos";
			
			case 8:
				return "muebles";
			
			case 9:
				return "profesiones";
			
			case 10:
				return "ropa";
			default:
				
				return "";
			}
		}

		protected void GeneraCategorias (Random r)
		{
			int escogidas = 0;
			int generada;
			
			categoriasSeleccionadas.Clear ();
			categoriasSeleccionadas2.Clear ();
			do {
				generada = r.Next (1, 11);
				
				if (!categoriasSeleccionadas.Contains (generada)) {
					escogidas++;
					categoriasSeleccionadas.Add (generada);
					this.categoriasSeleccionadas2.Add (this.GetCategoriaPorID (generada));
				}
			} while (escogidas < this.numCategorias);
		
		}

		protected void GeneraObjetosAMemorizar ()
		{
			Random r = new Random (DateTime.Now.Millisecond);
			
			this.GeneraCategorias (r);
			string[] poolTareas;
			
			// Reset variables
			objetosEscogidos.Clear ();
			selectedItemsByCategory = new Dictionary<string, List<string>>();
			
			foreach (int i in this.categoriasSeleccionadas) {
				poolTareas = objetosPorCategoria[this.GetCategoriaPorID (i)];
				
				selectedItemsByCategory.Add( this.GetCategoriaPorID(i), new List<string>() );
				int total = 0;
				int pos;
				string cadena;
				
				do {
					pos = r.Next (0, poolTareas.Length);
					
					cadena = "pesco.ejercicios.ObjetosClasificables.figuras." + GetCategoriaPorID (i) + "." + poolTareas[pos];
					
					if (!objetosEscogidos.Contains (cadena)) {
						objetosEscogidos.Add (cadena);
						//panel.AddImage(cadena);
						total++;
						selectedItemsByCategory[ this.GetCategoriaPorID(i) ].Add( cadena );
					}
					
				} while (total < this.numObjetosPorCategoria);
			}
		}

		protected void GeneraDistractores ()
		{
			Random r = new Random (DateTime.Now.Millisecond);
			
			string[] poolTareas;
			
			distractoresEscogidos.Clear ();
			
			foreach (int i in this.categoriasSeleccionadas) {
				poolTareas = objetosPorCategoria[this.GetCategoriaPorID (i)];
				
				int total = 0, pos;
				string cadena;
				
				do {
					pos = r.Next (0, poolTareas.Length);
					cadena = "pesco.ejercicios.ObjetosClasificables.figuras." + GetCategoriaPorID (i) + "." + poolTareas[pos];
					
					if (!objetosEscogidos.Contains (cadena) && !distractoresEscogidos.Contains (cadena)) {
						this.distractoresEscogidos.Add (cadena);
						total++;
					}
				} while (total < this.numObjetosPorCategoria);
			}
			
			objetosAMostrar.Clear ();
			objetosAMostrar.AddRange (distractoresEscogidos);
			objetosAMostrar.AddRange (objetosEscogidos);
			
		    TaskListExercise.Shuffle<string> (objetosAMostrar);
		    TaskListExercise.Shuffle<string> (objetosEscogidos);
		}


		protected void AgregaObjetos ()
		{
			/*foreach (string s in objetosAMostrar)
				panel.AddImageDerecha (s);
			
			foreach (string s in objetosEscogidos)
				panel.AddImageIzquierda (s);*/
		}
		
		protected void SetFinalResult(){
			
			finalRes.ObjetosClasificablesExecutionResults[finalRes.ObjetosClasificablesExecutionResults.Count -1].TotalCorrects = 0;
			finalRes.ObjetosClasificablesExecutionResults[finalRes.ObjetosClasificablesExecutionResults.Count -1].TotalFails = 0;
			finalRes.ObjetosClasificablesExecutionResults[finalRes.ObjetosClasificablesExecutionResults.Count -1].TotalOmissions = 0;
			finalRes.ObjetosClasificablesExecutionResults[finalRes.ObjetosClasificablesExecutionResults.Count -1].TotalTimeElapsed = 0;
			
			foreach(SingleResultObjetosClasificables sr in finalRes.ObjetosClasificablesExecutionResults[finalRes.ObjetosClasificablesExecutionResults.Count -1].SingleResults){
			
				finalRes.ObjetosClasificablesExecutionResults[finalRes.ObjetosClasificablesExecutionResults.Count -1].TotalCorrects += sr.Corrects;
				finalRes.ObjetosClasificablesExecutionResults[finalRes.ObjetosClasificablesExecutionResults.Count -1].TotalFails += sr.Errors;
				finalRes.ObjetosClasificablesExecutionResults[finalRes.ObjetosClasificablesExecutionResults.Count -1].TotalOmissions += sr.Omissions;
				finalRes.ObjetosClasificablesExecutionResults[finalRes.ObjetosClasificablesExecutionResults.Count -1].TotalTimeElapsed += sr.TimeElapsed;
				
			}
		}

		#endregion
        
 public void LastMedalla ()
        {
            List<string> seleccionadas = panelSelection.getListaSeleccionadas ();
            
            int aciertos = 0;
            int errores = 0;
            int numSeleccionados = seleccionadas.Count;
            
            // Calculate corrects
            foreach (string s in this.objetosEscogidos) {
                if (seleccionadas.Contains (s))
                    aciertos++;
            }
            // Calculate errors
            foreach (string s in seleccionadas)
                if (!objetosEscogidos.Contains (s))
                    errores++;
            double auxScore = (double) (aciertos-errores) / (double) objetosEscogidos.Count;
            
            if ( auxScore < 0.6 ) {
                 TotalMedal[2]++;
            } else if ( auxScore >= 0.6 && auxScore < 0.8 ) {
                 TotalMedal[1]++;   
            } else if (auxScore>= 0.8) {
                 TotalMedal[0]++;   
            }
            
        }
                   
		protected bool CheckSelection()
		{
			List<string> seleccionadas = panelSelection.getListaSeleccionadas ();
			int aciertos = 0;
			int errores = 0;
			int numSeleccionados = seleccionadas.Count;
			int omisiones = 0;
			int numARecordar = objetosEscogidos.Count;
			bool CheckLevel= false;
            
            // Calculate corrects
			foreach (string s in this.objetosEscogidos) {
				if (seleccionadas.Contains (s)){
					aciertos++;}
            }
           
			// Calculate errors
			foreach (string s in seleccionadas)
				if (!objetosEscogidos.Contains (s))
					errores++;
           
        	// Calculate omissions
			omisiones = numARecordar - aciertos;
            Console.WriteLine("Omisiones: "+omisiones);
            
			// Calculate corrects by category
			Dictionary<string, int> auxCorrectsByCategory = new Dictionary<string, int>();
			// For each category we have to check correct items in that category
			foreach ( KeyValuePair<string, List<string>> entry in selectedItemsByCategory ) {
				int auxCorrectsCounterInCategory = 0;
				// For each selected item...
				foreach ( string s in seleccionadas ) {
					// If this category contains the item, category counter++
					if ( entry.Value.Contains( s ) ) {
						auxCorrectsCounterInCategory++;
					}
				}
				auxCorrectsByCategory.Add ( entry.Key, auxCorrectsCounterInCategory );
			}
			// In order to serialize, we have to save in a simple format. In this case "category:counter"
			List<string> resultsByCategory = new List<string>();
			foreach ( KeyValuePair<string,int> entry in auxCorrectsByCategory ) {
				resultsByCategory.Add( entry.Key+":"+entry.Value );
			}
			
			//the demo results are not saved
			if(demoExecuted){
				TimeSpan timespan = tiempoFin - tiempoInicio;
				this.finalRes.AddResult(objetosEscogidos.Count,distractoresEscogidos.Count,seleccionadas.Count, objetosAMostrar.Count,nivel,aciertos,errores,omisiones, Convert.ToInt16(timespan.TotalSeconds));
				Serialize();
			}
			
		//	double auxScore = (double) aciertos / (double) objetosEscogidos.Count;
            double auxScoreLevel = (double) (aciertos-errores) / (double) objetosEscogidos.Count;
		/*	
			if ( auxScore >= 0.8 )
			{
				numCorrectas++;
				if (numCorrectas == 1)
				{
					numCorrectas = 0;
					return true;
				}
				else
					return false;
			}
			else
	            		return false;*/
            
            if ( auxScoreLevel < 0.6 ) {
                 TotalMedal[2]++;
            } else if ( auxScoreLevel >= 0.6 && auxScoreLevel < 0.8 ) {
                 TotalMedal[1]++;   
            } else if (auxScoreLevel>= 0.8) {
                 TotalMedal[0]++;   
                 CheckLevel=true;
            }
            return CheckLevel;
            
		}
             
       	#region CreacionPaneles

		public void CreaPanelDerecha ()
		{
			if (ejercicio != null && ejercicio.panelMemory!= null) {
        
                panelSelection = new PanelObjetosClasificablesSelection();
				this.panelSelection.NumColumnas = this.numColumnasD;
				this.panelSelection.NumFilas = this.numFilasD;
			    TaskListExercise.Shuffle<string>(objetosAMostrar);
			    panelSelection.AddImageDerecha(objetosAMostrar);
										
				ejercicio.tiempoInicio = DateTime.Now;
				panelSelection.BotonComprobar.Clicked += OnClickComprobar;
				SessionManager.GetInstance().ReplacePanel( panelSelection );
				
			}
		}
		
		
        public void CreaPanelMemory(){
			
            this.GeneraObjetosAMemorizar ();
            this.GeneraDistractores ();
            panelMemory=new PanelObjetosClasificablesMemory(nivel);
            this.panelMemory.NumColumnasI = this.numColumnasI;
            this.panelMemory.NumFilasI = this.numFilasI;
			
            panelMemory.AddImageIzquierda(objetosEscogidos);
            panelMemory.Padre=this;
           
            SessionManager.GetInstance().ReplacePanel(panelMemory);
        }
        
        public void ScreenExplicationExercise(){
                SessionManager.GetInstance().ChangeExerciseStatus("game");
                ExercisePanelObjetosClasificables panelocl = new ExercisePanelObjetosClasificables(this);
                SessionManager.GetInstance().ReplacePanel(panelocl);
                panelocl.InitPanel();
        }
		
        //Panel para la medalla
		public void CreatePanelPodium(){
            
        //Siempre pasa de nivel con la medalla de oro
        PodiumPanel auxPodium = new PodiumPanel( 90 );
                auxPodium.BalloonText = "¡Enhorabuena! Has pasado de Nivel y has obtenido una medalla de...";
                auxPodium.ButtonOK.Label = "Continuar Ejercicio";
                GtkUtil.SetStyle( auxPodium.ButtonOK, Configuration.Current.MediumFont );
                auxPodium.ButtonOK.Clicked += delegate {
                    CreaPanelMemory();
                     };
        SessionManager.GetInstance().ReplacePanel( auxPodium );
        auxPodium.InitPanel();
        }
        
         public void CreatePanelChangeLevel(){
               
                LastMedalla();
                ExercisePanelObjectsClassify panellevel= new ExercisePanelObjectsClassify();
                panellevel.ShowLabel(nivel+1);
                panellevel.BotonEmpezarSecuencia.Clicked += delegate {
                    CreaPanelMemory();
                     };
                SessionManager.GetInstance().ReplacePanel(panellevel);
        }
     /*   
        public void CreatePanelPodiumTotal(){
                 
                // Generate a new Total Podium Panel
                LastMedalla();
                TotalPodiumPanel totalPodiumPanel = new TotalPodiumPanel( "Memoriza las imágenes", "¡Finalizaste el ejercicio!\n Y..., has conseguido las siguientes medallas.",TotalMedal);
                // Replace current panel
                SessionManager.GetInstance().ReplacePanel( totalPodiumPanel );
        }*/
        
            	#endregion

		#region Interfaz Ejercicio
		public void avanzarNivel ()
		{
			nivel++;
            Console.WriteLine("Cambio nivel: "+nivel);
       		finalRes.CurrentLevel ++;
			rep = 0;
	       	switch (nivel) {
			case 1:
				this.numColumnasD = 6;
				this.numFilasD = this.numFilasI = this.numColumnasI = 3;
				
				this.numCategorias = 3;
				this.numObjetosPorCategoria = 3;
				
				numObjetos = 9;
				break;
			case 2:
				this.numColumnasD = 8;
				this.numFilasD = 3;
				this.numFilasI = 3;
				this.numColumnasI = 4;
				
				this.numCategorias = 3;
				this.numObjetosPorCategoria = 4;
				
				numObjetos = 12;
				break;
			case 3:
				this.numColumnasD = 8;
				this.numFilasD = 4;
				this.numFilasI = 4;
				this.numColumnasI = 4;
				
				this.numCategorias = 4;
				this.numObjetosPorCategoria = 4;
				
				numObjetos = 16;
				break;
			case 4:
				this.numColumnasD = 10;
				this.numFilasD = 4;
				this.numFilasI = 4;
				this.numColumnasI = 5;
				
				this.numCategorias = 4;
				this.numObjetosPorCategoria = 5;
				
				numObjetos = 20;
				break;
			case 5:
                //Panel Selection
				this.numColumnasD = 12;
				this.numFilasD = 5;
                //Panel Memory
				this.numFilasI = 5;
				this.numColumnasI = 6;
			   
            	this.numCategorias = 5;
				this.numObjetosPorCategoria = 6;
				
				numObjetos = 30;
				break;
			default:
				this.FinishExercice ();
				break;
			}
			/*
			this.panelSelection.NumColumnas = this.numColumnasD;
			this.panelSelection.NumFilas = this.numFilasD;
			this.panelMemory.NumColumnasI = this.numColumnasI;
			this.panelMemory.NumFilasI = this.numFilasI;*/
		}

		public override void finalizar ()
		{
			ventana = null;
			ejercicio = null;
		}

		public void FinishExercice(){
			panelMemory.PararTemporizador();
			
			this.Serialize ();
			
			ventana = null;
			ejercicio = null;
			
			if (nivel < 3)
				medalValue = 0;
			else if (nivel >= 3 && nivel < 5)
				medalValue = 60;
			else
				medalValue = 100;
         
			//CreatePanelPodiumTotal();
            
			SessionManager.GetInstance ().ExerciseFinished (medalValue);
			SessionManager.GetInstance ().TakeControl ();
		}
		
		public override int idEjercicio ()
		{
			return 3;
		}

		public override bool inicializar ()
		{
			return true;
		}
        public override void iniciar(){
            
            if ( res == null ) { 
                res = new ResultadosEjercicioObjetosClasificables ();
            }
            
            string fullPath = Configuration.Current.GetExerciseConfigurationFolderPath () + "/" +"ClasificableObjects.xml";
            
            if (File.Exists (fullPath)) {
                DemoExecuted=true;
                Console.WriteLine("existe fichero  "+fullPath);
            }
              else Console.WriteLine(" NO existe fichero  "+fullPath);
           	if (!DemoExecuted){
            	// Changing mode to "demo" mode
				SessionManager.GetInstance().ChangeExerciseStatus("demo");
				// Creating panels Demo
            	ExerciseDemoObjetosClasificables demoocl= new ExerciseDemoObjetosClasificables(this);
            	SessionManager.GetInstance().ReplacePanel(demoocl);
            	demoocl.InitPanel();
			}
			else {
			    ScreenExplicationExercise();
			}
        }   
        //Inicia el ejercicio en modo ensayo
		public void iniciar2 ()
		{
			if(!DemoExecuted){
				SessionManager.GetInstance().ChangeExerciseStatus("demo");
				//this.avanzarNivel ();
          		this.numCategorias = 4;
				this.numObjetosPorCategoria = 4;
				GeneraObjetosAMemorizar ();
				tp = new TrialPanel (this.categoriasSeleccionadas2, this.objetosEscogidos);
				SessionManager.GetInstance().ReplacePanel( tp );
				tp.ButtonNext.Clicked += this.OnClickLevelPre;
			}
		}
		
   
    //Inicia el ejercicio    
	public void iniciarExercise ()
		{
			this.avanzarNivel ();
			CreaPanelMemory();
		}	
    
        
	public void OnClickLevelPre (object sender, System.EventArgs e)
		{
			SortedDictionary<string, List<string>> userClass = tp.GetUserClassification ();
			
			this.nivel = 0;
			finalRes.CurrentLevel =0;
			DemoExecuted=true;
			this.Serialize();
			if (EvaluateLevelPre (userClass))			/* check if the class is correct */ {
			//	this.avanzarNivel ();
				//GeneraObjetosAMemorizar ();
				//GeneraDistractores ();
		        //PanelObjetosClasificables1 Panel1 = new PanelObjetosClasificables1();
                //SessionManager.GetInstance().ReplacePanel( Panel1 );
                //Panel1.ButtonExercise.Clicked += delegate{
                this.ScreenExplicationExercise();
           // };
			
				
			} else {
				//this.avanzarNivel ();
				this.numCategorias = 4;
				this.numObjetosPorCategoria = 4;
				GeneraObjetosAMemorizar ();
				tp = new TrialPanel (this.categoriasSeleccionadas2, this.objetosEscogidos);
				SessionManager.GetInstance().ReplacePanel( tp );
				tp.ButtonNext.Clicked += this.OnClickLevelPre;
				tp.Show();
			}
		}

		bool EvaluateLevelPre (SortedDictionary<string, List<string>> userClassification)
		{
			bool res = false;
			
			int contador = 0;
			string cadena;
			foreach (string cat in objetosPorCategoria.Keys)
				if (userClassification.ContainsKey (cat))
					foreach (string s in objetosPorCategoria[cat]) {
						cadena = "pesco.ejercicios.ObjetosClasificables.figuras." +cat + "." + s;						
						if (userClassification[cat].Contains (cadena))
							contador++;
					}
			
			return true;// contador >= 12;
		}
		#endregion

		#region Eventos

		protected virtual void OnClickComprobar (object sender, System.EventArgs e)
		{
           	rep++;
		   // bool com=true;
      	    if ( SessionManager.GetInstance().HaveToFinishCurrentExercise() ) {
      			this.FinishExercice ();
				return;
			}
			this.tiempoFin = DateTime.Now;
			if (this.CheckSelection ()){
                CreatePanelChangeLevel();
               //CreatePanelPodium();
				avanzarNivel ();
               }
            else {
                Console.WriteLine("Repetir memoria"+ numColumnasD+"  "+numColumnasI);
                CreaPanelMemory();
            }
			}

		protected virtual void OnDeleteEjercicio (object sender, Gtk.DeleteEventArgs e)
		{
			MessageDialog md = new MessageDialog (ventana, DialogFlags.DestroyWithParent, MessageType.Question, ButtonsType.YesNo, "¿Desea realmente abandonar el ejercicio?");
			
			ResponseType result = (ResponseType)md.Run ();
			
			if (result == ResponseType.Yes) {
				this.Serialize ();
				Application.Quit ();
				e.RetVal = false;
				// <-- Destroy window
			} else {
				md.Destroy ();
				e.RetVal = true;
				// <-- Don't destroy window
			}
		}

		#endregion
		public override void pausa ()
		{
			throw new System.NotImplementedException ();
		}

		#region implemented abstract members of pesco.Ejercicio
		public override string NombreEjercicio ()
		{
			return "Memoriza las imágenes";
		}
		
		#endregion
		
	}
}

