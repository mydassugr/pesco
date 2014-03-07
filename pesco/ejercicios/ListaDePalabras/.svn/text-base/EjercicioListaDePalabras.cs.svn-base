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
namespace ecng
{

	
	[XmlRoot("word-list-exercise")]
	public class EjercicioListaDePalabras : TaskListExercise
	{
		protected List<string> listA = new List<string>();
		protected List<string> listB = new List<string>();

		[XmlElement("list-a")]
		public List<string> ListA {
			get {
				return this.listA;
			}
			set {
				listA = value;
			}
		}

		[XmlElement("list-b")]
		public List<string> ListB {
			get {
				return this.listB;
			}
			set {
				listB = value;
			}
		}

		protected VerbalMerorizationPanel panel2;

		
		string[] poolPalabras = { "Tomate", "Pelota", "Elemento", "Dinosaurio", "Autobús", "Electricidad", "Conejo", "Factura", "Compra", "Farmacia",
		"Médico", "Paseo", "Vecina", "Cumpleaños", "Horario", "Hija", "Caseta", "Teclado", "Macetas", "Pescadería",
		"Comprar", "Aguador", "Libro", "Pasatiempos", "Telediario", "Carta", "Taladro", "Comida", "Periódico", "Botella",
		"Jardines", "Memoria", "Gimnasia", "Viajero", "Lavadora", "Dinero", "Jirafa", "Sábana", "Pepino", "Ternera" };

		[XmlElement("words-pool")]
		public string[] PoolPalabras {
			get {
				return this.poolPalabras;
			}
			set {
				poolPalabras = value;
			}
		}

		protected EjercicioListaDePalabras () : base()
		{
			chosen = new bool[poolPalabras.Length];
			this.tasksPool = this.poolPalabras;
			this.generaPoolTareas ();
			this.MemorizationTime = 5;
			initialListSize = 12;
			
		}

		public static TaskListExercise GetInstance ()
		{
			if (ejercicio == null)
				ejercicio = new EjercicioListaDePalabras();	
			
			return ejercicio;
		}
		
		public static TaskListExercise GetInstance (bool lt)
		{
			if (ejercicio == null)
				ejercicio = new EjercicioListaDePalabras();	
			
			ejercicio.LongTerm = lt;
			return ejercicio;
		}

		public override bool inicializar ()
		{
			
			
			if (!longTerm){
				base.inicializar ();
				listA = toMemorize;
				p1.StartTimer (EjercicioListaDePalabras.AccionTemporizador);
				return true;
			}
			else {
				toMemorize = listB;
				base.inicializar();
				return true;
			}
			
			
		}

		protected override void OnClickBotonListo (object sender, System.EventArgs e)
		{
			AudioManager.StopAudioRecording();
			
			if (this.longTerm) {
				this.finalizar ();				
				//TODO almacenar resultados
				return;
			} else {
				
				finishTime = DateTime.Now;
				numAttempts++;
				
				// mostramos la lista B, cuando ya se ha mostrado la A dos veces
				if (numAttempts == 2) {
					toMemorize.Clear ();
					taskList.Clear ();
					
					distractors = new bool[tasksPool.Length];
					
					for (int i = 0; i < chosen.Length; ++i) {
						distractors[i] = false;
					}
					
					// generar nueva lista a memorizar
					this.generaListaDeTareasAMemorizar ();
					this.generaListaPalabras ();
					
					listB = toMemorize;
					// si ya hemos mostrado la lista B, c'est fini!
				} else if (numAttempts > 2) {
					this.finalizar ();
					return;
				}
				
				ventana.Remove (ventana.Child);
				p1 = new TaskListPanel (toMemorize, this.MemorizationTime);
				p1.StartTimer (EjercicioListaDePalabras.AccionTemporizador);
				ventana.Add (p1);
				ventana.ShowAll ();
			}
			
		}

		public static void AccionTemporizador ()
		{
			EjercicioListaDePalabras e = EjercicioListaDePalabras.GetInstance () as EjercicioListaDePalabras;
			
			if (e == null)
				return;
			
			e.ventana.Remove (e.ventana.Child);
			
			//e.p2 = new ListCheckingPanel(e.taskList);
			
			VerbalMerorizationPanel vmp = new VerbalMerorizationPanel ();
			
			e.ventana.Add (vmp);
			
			vmp.BotonListo.Clicked += e.OnClickBotonListo;
			
			e.ventana.ShowAll ();
			
			vmp.RecordButton.Clicked += delegate{			
				AudioManager.Filename = Configuration.Current.GetConfigurationFolderPath() + "/lista_palabras-" + DateTime.Now.Millisecond + ".wav";				
				AudioManager.StartAudioRecording();	
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
			
			Console.WriteLine("TAm inicial " + initialListSize  );
			
		}
		
		
		#region XML serialization
		
		protected static string xmlUserFile = "word-list.xml";
		
		public override void Serialize(){
			string fullPath = Configuration.Current.GetExerciseConfigurationFolderPath() + "/" + xmlUserFile;
			
			XmlTextWriter escritor = new XmlTextWriter(fullPath, null);
		
			/*try
			{*/
				escritor.Formatting = Formatting.Indented;
				
				escritor.WriteStartDocument();
				
				escritor.WriteDocType("word-list-exercise", null, null, null);
				
				// hoja de estilo para pode ver en un navegador el xml
				//escritor.WriteProcessingInstruction("xml-stylesheet", "type='text/xsl' href='cuestionario.xsl'");
				
				XmlSerializer serializer = new XmlSerializer(typeof(EjercicioListaDePalabras));
				
				XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
				
				namespaces.Add("","");
				
				serializer.Serialize(escritor, this, namespaces);
				
				escritor.WriteEndDocument();
				escritor.Close();
			/*}
			catch(Exception e)
			{
				escritor.Close();
				Console.WriteLine("\n\nError al serializar" +  e.Message + "\n\n");
			}*/
		}	
		
		public static  EjercicioListaDePalabras Deserialize()
		{
			
			string fullPath = Configuration.Current.GetExerciseConfigurationFolderPath() + "/" + xmlUserFile;
			
			if (!File.Exists(fullPath))
			{
				GetInstance();
				ejercicio.Serialize();
				return ejercicio as EjercicioListaDePalabras;
				/*string s = Environment.CommandLine;			
				fullPath = Configuration.CommandDirectory + "/ejercicios/ListaDePalabras/xml-templates/" + xmlUserFile;*/
			}
		
			/*ejercicio = EjercicioListaDePalabras.GetInstance();
			
			ejercicio.Serialize();
			
			ejercicio.
			
			return ejercicio as EjercicioListaDePalabras;*/
			
			
			XmlTextReader lector = new XmlTextReader(fullPath);
			
			try
			{
			
				ejercicio = new EjercicioListaDePalabras();
				
				XmlSerializer serializer = new XmlSerializer(typeof(EjercicioListaDePalabras));				
				ejercicio = (EjercicioListaDePalabras) serializer.Deserialize(lector);
				
				lector.Close();		
				return (EjercicioListaDePalabras) ejercicio;
			}
			catch( Exception e)
			{
				lector.Close();
				Console.WriteLine("Error al deserializar" +  e.Message);
				Gtk.Application.Quit();
				return null;
			}
		}
		#endregion
		
	}
}

