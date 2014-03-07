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
using System.Xml;
using System.Xml.Serialization;
using System.IO;

using System.Collections.Generic;

namespace pesco
{
	
	public enum ExerciseCategory{
		Attention,
		Memory,
		Planification,
		Reasoning
	}

	/// <summary>
	/// Clase que representa un ejercicio
	/// </summary>
	public abstract class Exercise
	{	
		protected int nivel;
		protected int executionId = 0;
		
		protected string textoIntro = "";
		
		protected List<string> instrucciones = new List<string>();
		
		protected ExerciseCategory category;
		
		public int medalValue;
		
		[XmlElement("category")]
		public ExerciseCategory Category {
			get {
				return this.category;
			}
			set {
				category = value;
			}
		}
		
		public static string ExerciseCategoryToString(ExerciseCategory cat)
		{
			if (cat == ExerciseCategory.Attention)	
				return "Atención";
			else if (cat == ExerciseCategory.Memory)
				return "Memoria";
			else if (cat == ExerciseCategory.Planification)
				return "Planificación";
			else if (cat == ExerciseCategory.Reasoning)
				return "Razonamiento";
			else 
				return "";
		}

		[XmlElement("texto-intro")]
		public string TextoIntro {
			get {
				return this.textoIntro;
			}
			set {
				textoIntro = value;
			}
		}

		[XmlIgnore]
		protected ContenedorEjercicio contenedor = new ContenedorEjercicio();
		
		protected DemoPanel demoPanel;
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="id">
		/// A <see cref="System.String"/>
		/// </param>
		public static Exercise GetEjercicio(int id)
		{
			Exercise e = null;
			
			// Auxiliar list for help exercises
			List <string> auxTexts = new List<string>();
			
			switch(id)
			{
				
			// Memory
			case 1:
				e = NumbersAndVowelsExercise.GetInstance();
				break;
			case 2:
				e = TaskListExercise.Deserialize();
				break;
			case 3:
				e = EjercicioObjetosClasificables.Deserialize();
				break;
			case 18:
				e = TaskListExercise.Deserialize();
				(e as TaskListExercise).LongTerm = true;
				break;
			case 19:
				e = WordListExercise.Deserialize();
				(e as WordListExercise).LongTerm = true;
				break;
			case 7:
				ExerciseDirectNumbers.Deserialize();
				e = ExerciseDirectNumbers.getInstance();
				break;
			case 8:
				e = WordListExercise.Deserialize();
				break;
				
			// Atention
			case 4:
				e = BalloonsExercise.Deserialize();
				break;
			case 5:
				e = ExerciseBagItems.Deserialize();
				break;
			case 6:
				e = EjercicioPiramides.Deserialize();
				break;
			case 9:
				e = LIExercise.Deserialize();
				break;
				
			// Reasoning exercises
			case 11:
				e = SemanticSeriesExercise.Deserialize();

				SemanticSeriesExercise ce = e as SemanticSeriesExercise;
				
				if (ce != null) ce.Serialize(0);
				break;
			case 12:
				e = LogicalSeriesExercise.Deserialize();
				LogicalSeriesExercise ca = e as LogicalSeriesExercise;
				
				if (ca != null) ca.Serialize(0);
				break;				
			case 13: 
				e = SpatialReasoningExercise.Deserialize();
				break;				
			case 14:
				e = ClassifyExercise.Deserialize();
				ClassifyExercise cd = e as ClassifyExercise;				
				if (cd != null) cd.Serialize(0);
				break;				
			case 15:
				e = SemanticAnalogiesExercise.Deserialize();
				SemanticAnalogiesExercise cx = e as SemanticAnalogiesExercise;				
				if (cx != null) cx.Serialize(0);
				break;

			// Planification
			case 16:
				//compra de regalos
				e = ExerciseGiftsShopping.Deserialize();
				break;
			case 17: 
				// reparto de paquetes
				e = ExercisePackageDelivering.Deserialize();
				break;
			
			case 20: 
				// Lost Items
				e = LIExercise.Deserialize();
				break;
				
			// Tests
			case 30: 
				e = new UsersExercice();
				break;
				
			case 31: 
				e = new QuestionaryExercise();
				break;
				
			case 32: 
				e = new MouseDialogExercice();
				break;
				
			case 33: 
				e = new QuestinaryInstrumentalExercice();
				break;
				
			// Helps
			case 34: 
				e = new IntroductionExercise();
				break;
				
			case 101:
				auxTexts.Add("¡Gracias por completar los datos! Ahora vamos a hacer unos ejercicios de razonamiento. Al final de cada ejercicio conseguirás una medalla según como lo hayas hecho, ¡hazlo lo mejor posible!");
				e = new GenericHelpExercise(101, "Ayuda", auxTexts);
				break;

			case 102:
				auxTexts.Add("Has completado los ejercicios de razonamiento, ¡enhorabuena!. Ahora te haré unas cuantas preguntas más antes terminar la primera sesión.");
				e = new GenericHelpExercise(102, "Ayuda", auxTexts);
				break;

			// Debug tests
			case 40:

				List <Medal> auxList = new List<Medal>();
				auxList.Add( new Medal( MedalValue.Gold, DateTime.Now, 10, 2 ) );
				auxList.Add( new Medal( MedalValue.Bronze, DateTime.Now, 10, 2 ) );
				auxList.Add( new Medal( MedalValue.Silver, DateTime.Now, 10, 2 ) );
				int [] medals = {2, 2, 1};
				TotalPodiumPanel totalPodiumPanel = new TotalPodiumPanel( "Ejercicio de prueba", "Este es el texto del globo!", medals );
								
				totalPodiumPanel.ButtonOK.Clicked += delegate {
					
					// Action for the button of the panel
					
				};
				SessionManager.GetInstance().ReplacePanel( totalPodiumPanel );
				break;
				
			// Debug Sessions Podium Panel
			case 41: 
				SessionsPodiumPanel auxPodium = new SessionsPodiumPanel();
				SessionManager.GetInstance().ReplacePanel( auxPodium );
				auxPodium.InitPanel();
								
				break;
			
			case 42:
				string message = "¡Muy bien! Has terminado la primera sesión. " +
				"En la siguiente sesión, realizarás una serie de ejercicios que medirán " +
				" tu nivel de memoria, atención, razonamiento y planificación. ¡Hasta pronto!";
				PESCOGoodByeDialog pgbd = new PESCOGoodByeDialog();
				pgbd.SetText(message);
				SessionManager.GetInstance().ReplacePanel(pgbd);
				pgbd.Dialog.InitPanel();
				break;

			}
			
			return e;
		}
		
		/// <summary>
		/// 
		/// </summary>
		[XmlIgnore]
		 public ContenedorEjercicio Contenedor {
			get {
				return contenedor;
			}
			set {
				contenedor = value;
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		[XmlIgnore]
		public int Nivel {
			get {
				return nivel;
			}
			set {
				nivel = value;
			}
		}
		
		[XmlElement("executionid")]
		public int ExecutionId {
			get {
				return executionId;
			}
			set {
				executionId = value;
			}
		}
		
		
		/// <summary>
		/// 
		/// </summary>
		/// <returns>
		/// A <see cref="System.Int32"/>
		/// </returns>
		public abstract int idEjercicio();
		
		///
		public abstract string NombreEjercicio();
		
		/// <summary>
		/// Inicializa el ejercicio
		/// </summary>
		/// <returns>
		/// A <see cref="System.Boolean"/> indicando si el ejercicio ha sido inicializado correctamente
		/// </returns>
		public abstract bool inicializar();

		/// <summary>
		/// Inicia el ejercicio
		/// </summary>
		public abstract void iniciar();
				
		/// <summary>
		/// Finaliza el ejercicio
		/// </summary>
		public abstract void finalizar();
		
		/// <summary>
		/// Devuelve los resultados de la ejecución del ejercicio
		/// </summary>
		/// <returns>
		/// A <see cref="Resultados"/>
		/// </returns>
		// public abstract Resultados getResultados();
	
		/// <summary>
		/// Detiene el ejercicio
		/// </summary>
		public abstract void pausa();
	}
	
}

