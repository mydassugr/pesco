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
	//TODO Variables a parametrizar:
	// 1. 

	[XmlRoot("task-list-exercise")]
	public class TaskListExerciseOld : Exercise
	{

		/// <summary>
		/// ventana que mostrará el ejercicio
		/// </summary>
		// protected Gtk.Window ventana;

		/// <summary>
		/// Panel
		/// </summary>
		protected TaskListPanel p1;

		/// <summary>
		/// 
		/// </summary>
		protected ListCheckingPanel p2;

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

		/// <summary>
		/// 
		/// </summary>
		protected TaskListResults res;
		protected TaskListResults finalRes = XmlUtil.DeserializeForUser<TaskListResults>(Configuration.Current.GetExerciseConfigurationFolderPath () + "/task-list2.xml");

		protected int initialListSize = 5;

		[XmlElement("Results")]
		public TaskListResults Res {
			get { return this.res; }
			set { res = value; }
		}

		#region Persistent values
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

		/// <summary>
		/// 
		/// </summary>
		protected string[] tasksPool = { "Sacar al perro", "Comprar el pan", "Recoger ropa de la tintorería", "Lavar el coche", "Limpiar la casa", "Llevar el regalo al nieto", "Ir al banco", "Sacar dinero", "Hacer la compra", "Ir a la farmacia",
		"Ir al médico", "Salir de paseo", "Llamar a la vecina", "Felicitar a la prima por su cumpleaños", "Salir a andar durante una hora", "Ir a comer a casa de la hija", "Ir a desayunar a casa de la vecina", "Regar las macetas", "Comprar en la pescadería", "Comprar en la frutería",
		"Pagar la factura del agua", "Comprar un libro", "Hacer sopa de letras o pasatiempos", "Ver el telediario", "Jugar a las cartas", "Hacer la comida", "Leer el periódico", "Comprar el periódico", "Arreglar el jardín", "Ir al taller de memoria",
		"Ir a la gimnasia", "Apuntarse a un viaje", "Visitar a mi mejor amiga", "Devolver dinero a la vecina" };

		[XmlElement("task-pool")]
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
		#endregion

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


		#region Singleton
		/// <summary>
		/// 
		/// </summary>
		protected static TaskListExerciseOld ejercicio = null;

		/// <summary>
		/// 
		/// </summary>
		/// <returns>
		/// A <see cref="EjercicioListaDeRecados"/>
		/// </returns>
		public static TaskListExerciseOld GetInstance ()
		{
			if (ejercicio == null)
				ejercicio = new TaskListExerciseOld ();
			return ejercicio;
		}

		public static TaskListExerciseOld GetInstance (bool longterm2)
		{
			
			if (ejercicio == null)
				ejercicio = new TaskListExerciseOld ();
			
			
			ejercicio.longTerm = longterm2;
			
			return ejercicio;
		}

		/// <summary>
		/// 
		/// </summary>
		protected TaskListExerciseOld ()
		{
			category = ExerciseCategory.Memory;
			chosen = new bool[tasksPool.Length];
			generaPoolTareas ();
			res = new TaskListResults ();
			nivel = 1;
			
			finalRes.CategoryId=Convert.ToInt16(ExerciseCategory.Memory);
			finalRes.CurrentLevel=nivel;
			finalRes.ExerciseId= this.idEjercicio();
			
			ExerciceExecutionResult <SingleResultsTaskList> exerciceExecution =  new ExerciceExecutionResult<SingleResultsTaskList>(SessionManager.GetInstance().CurSession.IdSession, SessionManager.GetInstance().CurExecInd);
			finalRes.TaskListExecutionResult.Add(exerciceExecution);
		}
		#endregion

		#region Interfaz Ejercicio
		public void ayuda ()
		{
			int contador = 0;
			
			int total = this.toMemorize.Count;
			
			while (contador < total && !p2.SeleccionarTarea (this.seleccionaTareaAleatoria ()))
				contador++;
		}

		public override void finalizar ()
		{
			
			if (!this.longTerm)
				p1.StopTimer ();
			this.Serialize ();
			/* Adapting exercise to single window */
			// ventana.Dispose();
			// ventana.Hide ();
			// ventana.Destroy ();
			
			// ventana = null;
			// ejercicio = null;
			
			longTerm = true;
			
			if (nivel < 3)
				medalValue = 0;
			else if (nivel >= 3 && nivel < 5)
				medalValue = 60;
			else
				medalValue = 100;
			
			SessionManager.GetInstance ().ExerciseFinished (medalValue);
			SessionManager.GetInstance ().TakeControl ();
		}

		public override bool inicializar ()
		{
			
			// Console.WriteLine("inicializando lista de tareas");
			
			// if we're not in the longterm department...
			if (!longTerm) {
				for (int i = 0; i < chosen.Length; ++i)
					chosen[i] = false;
				if (nivel > 1){
					nivel--;
					finalRes.CurrentLevel --;
					
				}
				
				
				//initialListSize = initialListSize + 2 * (nivel - 1);
				
				// Console.WriteLine("iniciando generación de lista de palabras");
				
				this.generaListaDeTareasAMemorizar ();
				
				// Console.WriteLine("iniciando generación de lista de palabras total");
				
				ejercicio.generaListaPalabras ();
				
				if (nivel > 1){
					nivel++;
					finalRes.CurrentLevel++;
					this.actualizarListas();
				}
				
				// Console.WriteLine("lista de palabras generalizada");
				p1 = new TaskListPanel (toMemorize, this.MemorizationTime);
				
				
				// Commented to adapt exercise to single panel behaviour
				/*ventana = new Gtk.Window ("Lista de la Tareas");				
				ventana.WidthRequest = 550;
				ventana.WindowPosition = WindowPosition.CenterAlways;
				ventana.DeleteEvent += this.OnDeleteWindow;*/				
				
				//res.Results = new TaskListResults.ResultsByLevel[100];
				
				return true;
			} else {
				// Commented to adapt exercise to single panel behaviour
				/* ventana = new Gtk.Window ("Recuerdo a largo plazo: Lista de la Tareas");
				ventana.WidthRequest = 550;
				ventana.WindowPosition = WindowPosition.CenterAlways;
				ventana.DeleteEvent += this.OnDeleteWindow;*/
				/* foreach(string s in toMemorize)
					Console.WriteLine(s);
				*/
				
				p1 = new TaskListPanel (toMemorize,0);

				for (int i = 0; i < chosen.Length; ++i)
					chosen[i] = false;
				
				ejercicio.generaListaPalabras ();
				p2 = new ListCheckingPanel (taskList);
				ejercicio.p2.BotonListo.Clicked += ejercicio.OnClickBotonListo;
	
				//res.Results = new TaskListResults.ResultsByLevel[100];
				this.maxAttempts = 1;
				return true;
			}
			
			
			// Console.WriteLine("fin de inicialización lista de tareas");
		}

		public override void iniciar ()
		{
			// Console.WriteLine("iniciando lista de tareas");
			if (!longTerm)
				this.textoIntro = "Lista de Recados\n\n " +
						"Este ejercicio te ayudará a mejorar tu memoria</span>";
			else {
				this.textoIntro = "Lista de Recados\n\nAhora vamos a ver si recuerdas la lista \nque memorizaste hace un rato.";
			}
			
			demoPanel = new DemoPanel (this.TextoIntro);
			// Commented to adapt exercise to single panel behaviour
			/* ventana.Maximize ();
			ventana.Add (demoPanel);*/
			SessionManager.GetInstance().ReplacePanel(demoPanel);
			demoPanel.Show();
			
			// mostramos la ventana
			// ventana.Show ();
			
			demoPanel.ButtonNext.Clicked += delegate {
				//demoPanel = new DemoPanel(this.TextoIntro);
				
				// Commented to adapt exercise to single panel behaviour
				// ventana.Remove(demoPanel);
				// ventana.Add (p1);
				SessionManager.GetInstance().ReplacePanel(p1);
				
				if (!longTerm)
				{	
					p1.SetInstructions ("<span color=\"blue\">Voy a mostrarte una lista con recados que tienes que memorizar.\n\nPuedes ver un ejemplo de lista en la imagen de abajo.</span>", Pixbuf.LoadFromResource ("ecng.tasklist1.png"));
					
					p1.ShowNextInstructionButton ();
					
					p1.ButtonNextInstruction.Clicked += delegate(object sender, EventArgs e) {
						
						p1.ShowStartExercisesButton ();
						
						p1.SetInstructions ("<span color=\"blue\">En la imagen de abajo puedes ver la pantalla desde la que podrás seleccionar " +
							"los recados que recuerdes. Te presentaré una lista con varios de ellos. Tienes que pulsar los " +
							"recados que recuerdes de la lista anterior.</span>", Pixbuf.LoadFromResource ("ecng.tasklist2.png"));
						
					};
					
					// ventana.Maximize ();
					// ventana.Show ();
					
					
					
					p1.ButtonStartExercises.Clicked += delegate {
						
						//ventana.Remove(demoPanel);
						
						if (!this.longTerm) {
							
							// ventana.Add (p1);
							SessionManager.GetInstance().ReplacePanel(p1);
							p1.SetInstructions ("<span color=\"blue\">Tienes un minuto para memorizar los recados, durante este tiempo no tienes que pulsar nada.\n\nCuando estés preparado " + "para comenzar el ejercicio, pulsa el botón <span color=\"black\">¡Empezar Ejercicio!</span></span>.", null);// Pixbuf.LoadFromResource ("ecng.tasklist1.png"));							
							p1.ShowNextExerciseButton ();							
							p1.ButtonNextExercise.Clicked += new EventHandler (OnClickNextExercise);
						} else {
							// ventana.Add (p2);
							SessionManager.GetInstance().ReplacePanel(p2);
							p2.Show();							
						}
						
						// ventana.Maximize ();
						// ventana.Show ();
					};
						
					
				}
				else {
					this.textoIntro = "<span color=\"blue\"><big><big>Intenta tachar todas las palabras de la última lista de recados que memorizaste. \n\nTe mostraré los recados en varias páginas, para pasar de una página a otra pulsa los botones " + 
						"<span color=\"black\"><b>Siguiente</b></span> y <span color=\"black\"><b>Anterior</b></span> (puedes verlos en la imagen de abajo). \n\nCuando creas que hayas marcado todas las palabras de la lista, pulsa el botón <span color=\"black\"><b>¡Listo!</b></span>. \n\n Para comenzar el ejercicio, pulsa el botón <span color=\"black\"><b>Siguiente</b></span></big></big></span>";
					
					p1.SetInstructionsSmall(this.textoIntro, Pixbuf.LoadFromResource ("ecng.pag-tareas.png"));
					
					// ventana.Maximize ();
					// ventana.Show ();
					p1.ShowStartExercisesButton ();
					p1.ButtonStartExercises.Clicked += delegate {
						
							// ventana.Remove(p1);
							// ventana.Add (p2);
							SessionManager.GetInstance().ReplacePanel(p2);
							p2.Show();
						
						
						// ventana.Maximize ();
						// ventana.Show ();
					};
					
				
				}
			};
			
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
			return 2;
		}
		#endregion

		#region Generación Tareas

		/// <summary>
		/// 
		/// </summary>
		/// <param name="longitud">
		/// A <see cref="System.Int32"/>
		/// </param>
		public virtual void generaListaDeTareasAMemorizar ()
		{
			int total = 0;
			
			Random r = new Random (DateTime.Now.Millisecond);
			
			// toMemorize.Clear();
			toMemorize= new List<string>();
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

		/// <summary>
		/// 
		/// </summary>
		public virtual void generaListaPalabras ()
		{
			
			this.taskList = new List<string> ();
			Random r = new Random (DateTime.Now.Millisecond);
			int total = 0;
			taskList.AddRange (toMemorize);
			total = toMemorize.Count;
			int objetivo = toMemorize.Count * 2;
			
			int pos;
			
			do {
				pos = r.Next (0, tasksPool.Length);
				string aux = tasksPool[pos];
				if (!toMemorize.Contains(aux) && !taskList.Contains(aux)) {
					taskList.Add (tasksPool[pos]);
					distractors[pos] = true;
					total++;
				}
			} while (total < objetivo);

			
			Shuffle<string> (taskList);
			Shuffle<string> (taskList);
			Shuffle<string> (taskList);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="list">
		/// A <see cref="IList<T>"/>
		/// </param>
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

		/// <summary>
		/// 
		/// </summary>
		protected void actualizarListas ()
		{
			int delta = 2;
			
			if (nivel > 3)
				delta = 3;
			
			// add the new tasks to the list, as much as the value of delta
			int total = 0;
			Random r = new Random (DateTime.Now.Millisecond);
			int pos;
			
			do {
				pos = r.Next (0, tasksPool.Length);
				
				if (!this.chosen[pos] && !this.distractors[pos]) {
					chosen[pos] = true;
					toMemorize.Add (tasksPool[pos]);
					taskList.Add (tasksPool[pos]);
					total++;
				}
			} while (total < delta);
			
			// Add distractors
			total = 0;
			
			do {
				pos = r.Next (0, tasksPool.Length);
				
				if (!this.chosen[pos] && !this.distractors[pos]) {
					taskList.Add (tasksPool[pos]);
					distractors[pos] = true;
					total++;
				}
			} while (total < delta);
			
		 	Shuffle<string> (taskList);
			Shuffle<string> (toMemorize);
			Shuffle<string> (taskList);
			Shuffle<string> (toMemorize);
			Shuffle<string> (taskList);
			Shuffle<string> (toMemorize);
			
		}

		/// <summary>
		/// 
		/// </summary>
		public void generaPoolTareas ()
		{
			
			distractors = new bool[tasksPool.Length];
			
			for (int i = 0; i < chosen.Length; ++i) {
				chosen[i] = false;
				distractors[i] = false;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/>
		/// </returns>
		protected string seleccionaTareaAleatoria ()
		{
			Random r = new Random (DateTime.Now.Millisecond);
			int pos = r.Next (0, toMemorize.Count);
			
			return toMemorize[pos];
		}
		#endregion

		#region Events

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender">
		/// A <see cref="System.Object"/>
		/// </param>
		/// <param name="e">
		/// A <see cref="System.EventArgs"/>
		/// </param>
		protected virtual void OnClickBotonListo (object sender, System.EventArgs e)
		{
			
			
			
			finishTime = DateTime.Now;
			
			numAttempts++;
			
			bool nivelSuperado = NivelSuperado ();
			
			if (nivelSuperado && nivel < MaxLevel) {
				nivel++;
				finalRes.CurrentLevel ++;
				numAttempts = 0;
				this.actualizarListas ();
			} else if (numAttempts >= this.maxAttempts || longTerm) {
				this.finalizar ();
				return;
			}
			
			// ventana.Remove (ventana.Child);
			p1 = new TaskListPanel (toMemorize, this.MemorizationTime);
			if (NivelSuperado ())
				p1.LabelPauseText.Markup = "<span color=\"blue\">Ahora voy a volver a presentar la misma lista pero con más recados." + " Intenta memorizarlos todos.\n\nCuando estés preparado para comenzar el ejercicio, pulsa el botón <span color=\"black\">¡Empezar Ejercicio!</span></span>.";
			else
				p1.LabelPauseText.Markup = "<span color=\"blue\">Ahora te voy a volver presentar la misma lista. Intenta memorizarlos todos. " + "\n\nCuando estés preparado para comenzar el ejercicio, pulsa el botón <span color=\"black\">¡Empezar Ejercicio!</span></span>.";
			
			p1.ShowNextExerciseButton ();
			p1.ButtonNextExercise.Clicked += new EventHandler (OnClickNextExercise);
			// ventana.Add (p1);
			// ventana.Show ();
			SessionManager.GetInstance().ReplacePanel(p1);
		}

		protected void OnClickNextExercise (object sender, System.EventArgs e)
		{
			// Console.WriteLine ("Button Start Exercise pressed");
			p1.StartTimer (TaskListExerciseOld.AccionTemporizador);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender">
		/// A <see cref="System.Object"/>
		/// </param>
		/// <param name="e">
		/// A <see cref="System.EventArgs"/>
		/// </param>
		/* protected virtual void OnDeleteWindow (object sender, DeleteEventArgs args)
		{
			MessageDialog md = new MessageDialog (ventana, DialogFlags.DestroyWithParent, MessageType.Question, ButtonsType.YesNo, "¿Desea realmente abandonar el ejercicio?");
			
			ResponseType result = (ResponseType)md.Run ();
			
			if (result == ResponseType.Yes) {
				this.Serialize ();
				Application.Quit ();
				args.RetVal = false;
				// <-- Destroy window
			} else {
				md.Destroy ();
				args.RetVal = true;
				// <-- Don't destroy window
			}
		}*/

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender">
		/// A <see cref="System.Object"/>
		/// </param>
		/// <param name="e">
		/// A <see cref="System.EventArgs"/>
		/// </param>
		protected virtual void OnClickAyuda (object sender, System.EventArgs e)
		{
			this.ayuda ();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns>
		/// A <see cref="System.Boolean"/>
		/// </returns>
		protected bool NivelSuperado ()
		{
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
			
			//TaskListResults tasklistresult=  new TaskListResults();
			//tasklistresult.setResultsForLevel (20, correct, errors, omissions, startTime - finishTime);
			//this.res.Add(tasklistresult);
			//this.res.setResultsForLevel ( correct, errors, omissions, startTime - finishTime);
			this.finalRes.setResult(LongTerm,totalToRemember, getDistractorNum(), 0, getUserTaskNum(),0, correct, errors, totalToRemember - correct,nivel,finishTime.Second -startTime.Second); 
			
			// Console.WriteLine (res.ToString ());
			
			if ((correct - errors) >= errorThreshold * totalToRemember)
				return true;
			else
				return false;
			
			return true;
		}
	
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
			return taskList.Count;
		}
		public int getUserTaskNum(){
			
			return 	p2.GetSelectedItems().Count;
		}
			                        
		
		protected void SetFinalResult(){
			
			finalRes.TaskListExecutionResult[finalRes.TaskListExecutionResult.Count -1].TotalCorrects =0;
			finalRes.TaskListExecutionResult[finalRes.TaskListExecutionResult.Count -1].TotalFails =0;
			finalRes.TaskListExecutionResult[finalRes.TaskListExecutionResult.Count -1].TotalTimeElapsed =0;
			
			foreach(SingleResultsTaskList sr in finalRes.TaskListExecutionResult[finalRes.TaskListExecutionResult.Count -1].SingleResults){
				
				finalRes.TaskListExecutionResult[finalRes.TaskListExecutionResult.Count -1].TotalCorrects += sr.Corrects;
				finalRes.TaskListExecutionResult[finalRes.TaskListExecutionResult.Count -1].TotalFails += sr.Fails;
				finalRes.TaskListExecutionResult[finalRes.TaskListExecutionResult.Count -1].TotalTimeElapsed += sr.TimeElapsed;
				
			}
		}	
		/// <summary>
		/// 
		/// </summary>
		public override void pausa ()
		{
			throw new System.NotImplementedException ();
		}
		#endregion

		#region Timers
		/// <summary>
		/// 
		/// </summary>
		public static void AccionTemporizador ()
		{
			
			ejercicio.p1.ShowLeftPanel ();
			string text = "<span color=\"blue\"><big><big>Voy a presentarte una lista con varios recados. Selecciona " + 
				"aquellos que recuerdes de la lista anterior. Si te equivocas al " + 
				"seleccionar un recado, vuelve a pulsar sobre él para desmarcarlo.\nPuedes pasar de una página de recados a otra pulsando los botones <span color='black'>Siguiente</span> y <span color='black'>Anterior</span>.\n\n Para continuar con el ejercicio pulsa el botón <span  color=\"black\">¡Recordar Lista!</span></big></big></span>";
			ejercicio.p1.SetInstructionsSmall (text, Pixbuf.LoadFromResource ("ecng.pag-tareas.png"));
			
			ejercicio.p1.ShowContinueExerciseButton ();
					
			ejercicio.p1.ButtonContinueExercise.Clicked += new EventHandler (ejercicio.OnClickStartCheckingPanel);
			
		}


		public void OnClickStartCheckingPanel (object sender, EventArgs e)
		{
			// ejercicio.ventana.Remove (ejercicio.ventana.Child);
			
			ejercicio.p2 = new ListCheckingPanel (ejercicio.taskList);
			
			// ejercicio.ventana.Add (ejercicio.p2);
			SessionManager.GetInstance().ReplacePanel(ejercicio.p2);
			
			ejercicio.p2.BotonListo.Clicked += new EventHandler (ejercicio.OnClickBotonListo);
	
			
			ejercicio.p2.Show ();
			// ejercicio.ventana.Show ();
			
			ejercicio.startTime = DateTime.Now;
		}
		#endregion

		#region implemented abstract members of ecng.Ejercicio
		/// <summary>
		/// 
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/>
		/// </returns>
		public override string NombreEjercicio ()
		{
			return "Lista de Recados";
		}

		#endregion

		#region XML serialization

		protected static string xmlUserFile = "task-list.xml";

		public virtual void Serialize ()
		{
			SetFinalResult();
			XmlUtil.SerializeForUser<TaskListResults>(finalRes,Configuration.Current.GetExerciseConfigurationFolderPath () + "/task-list2.xml");
			
			string fullPath = Configuration.Current.GetExerciseConfigurationFolderPath () + "/" + xmlUserFile;
			
			XmlTextWriter escritor = new XmlTextWriter (fullPath, null);
			
			try {
				escritor.Formatting = Formatting.Indented;
				
				escritor.WriteStartDocument ();
				
				escritor.WriteDocType ("task-list-exercise", null, null, null);
				
				// hoja de estilo para pode ver en un navegador el xml
				//escritor.WriteProcessingInstruction("xml-stylesheet", "type='text/xsl' href='cuestionario.xsl'");
				
				XmlSerializer serializer = new XmlSerializer (typeof(TaskListExercise));
				
				XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces ();
				
				namespaces.Add ("", "");
				
				serializer.Serialize (escritor, this, namespaces);
				
				escritor.WriteEndDocument ();
				escritor.Close ();
			} catch (Exception e) {
				escritor.Close ();
				// Console.WriteLine ("\n\nError al serializar" + e.Message + "\n\n");
			}
		}

		public static TaskListExerciseOld Deserialize ()
		{
			string fullPath = Configuration.Current.GetExerciseConfigurationFolderPath () + "/" + xmlUserFile;
			
			if (!File.Exists (fullPath)) {
				GetInstance ();
				ejercicio.Serialize ();
				return ejercicio;
			}
			
			XmlTextReader lector = new XmlTextReader (fullPath);
			
			
			try {
				
				
				ejercicio = new TaskListExerciseOld ();
				
				XmlSerializer serializer = new XmlSerializer (typeof(TaskListExerciseOld));
				ejercicio = (TaskListExerciseOld)serializer.Deserialize (lector);
			
				lector.Close ();
				
				/*foreach(string s in ejercicio.ToMemorize)
					Console.WriteLine(s);*/
				
				return (TaskListExerciseOld)ejercicio;
			} catch (Exception e) {
				lector.Close ();
				// Console.WriteLine ("Error al deserializar" + e.Message);
				Gtk.Application.Quit ();
				return null;
			}
		}
		#endregion
	}
}

