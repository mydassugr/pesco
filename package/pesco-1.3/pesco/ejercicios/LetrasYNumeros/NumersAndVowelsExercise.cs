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
using Gdk;
using Gtk;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace pesco
{
       
	[XmlRoot("numbers-and-vowels-exercise")]
	public class NumbersAndVowelsExercise : Exercise
	{
		/* Ensayos: 6-I
                    U-9
                    8-A-5
                    7-E-U 
                    O-4-A
			Nivel 1:E-3
              		9-I
              		A-5
			Nivel 2:E-2-O
               		U-4-9
               		8-A-4
			Nivel 3:7-E-3-O
            		I-6-A-9
               		8-A-4-U
			Nivel 4:U-7-E-2-A
               		6-E-8-A-4
               		I-5-E-1-U
			Nivel 5:8-A-3-O-6-E
              		O-7-I-4-A-2
              		7-U-1-A-5-E
			Nivel 6:E-8-I-5-O-2-A
              		3-U-6-E-8-I-9
              		A-4-U-1-O-7-E
			Nivel 7:5-I-9-E-2-A-6-U
              		O-7-E-4-U-2-I-9
              		5-A-1-O-8-E-7-U */
		
		string cadena = "";
		string cadenaOrdenada = "";
        
        bool Isfixed=true;
        
        //Tablas con las secuencias de vocales y numeros por nivel
        //El nivel 0 corresponde a los ensayos
        List <string>[] TablaSequences= new List<string>[8]{
        /*0*/   new List<string>{"6i","u9","8a5","7eu","o4a"}, 
     	/*1*/   new List<string>{"e3","9i","a5"},                    
        /*2*/   new List<string>{"e2o","u49","8a4"}, 
        /*3*/   new List<string>{"7e3o","i6a9","8a4u"} ,    
        /*4*/   new List<string>{"u7e2a","6e8a4","i5e1u"},
        /*5*/   new List<string>{"8a3o6e","o7i4a2","7u1a5e"},
        /*6*/   new List<string>{"e8i5o2a","3u6e8i9","a4u1o7e"},
        /*7*/   new List<string>{"5i9e2a6u","o7e4u2i9","5a1o8e7u"}};
       
        List <string> CadenaLevel= null;
        
		public string CadenaOrdenada {
			get {
				return this.cadenaOrdenada;
			}
		}
        
		DateTime tiempoInicio;
		DateTime tiempoFin;
      
		int longitudCadena = 2;
		bool[] vocalesEscogidas;
		bool[] cifrasEscogidas;

		int intentosActuales;
		int numAciertosActuales;
        int numFallos=0;
		int lastOvercomeLevel=0;

		#region Persistent variables
		uint tiempoEspera = 1000;

		/*public uint TiempoEspera {
			get { return this.tiempoEspera; }
			set { tiempoEspera = value; }
		}*/
		#endregion

		bool ScreenDemo;
		bool pararTemporizador = false;

		int elementoActualSecuencia = 0;

		int currentLevel;
		/// <summary>
		/// Current level of the exercise 
		/// </summary>
		[XmlElement("current-level")]
		public int CurrentLevel {
			get { return this.currentLevel; }
			set { currentLevel = value; }
		}
		
		[XmlElement("lastOvercomeLevel")]
			public int LastOvercomeLevel {
				get {
					return this.lastOvercomeLevel;
				}
				set {
					lastOvercomeLevel = value;
				}
		}
		
		ResultadoEjercicioLetrasYNumeros res = new ResultadoEjercicioLetrasYNumeros ();
		
		[XmlIgnoreAttribute]

        ResultadoEjercicioLetrasYNumeros finalRes= XmlUtil.DeserializeForUser<ResultadoEjercicioLetrasYNumeros>(Configuration.Current.GetExerciseConfigurationFolderPath () + "/NumbersAndVowels.xml");
	[XmlElement("exercise-results")]
		public ResultadoEjercicioLetrasYNumeros Res {
			get { return this.res; }
			set { res = value; }
		}

		#region GUI
	//PanelVacio w;
		PanelVocalesYNumerosIzq panelIzq=null; //new PanelVocalesYNumerosIzq();
        PanelVocalesYNumerosIzq2 panelIzq2=null; 
		PanelVocalesYNumerosDer panelDer=null; // new PanelVocalesYNumerosDer();
		PanelVocalesYNumerosCen panelCen=null;// new PanelVocalesYNumerosCen();
		
		//PanelVocalesYNumeros panel = new PanelVocalesYNumeros ();
		//PanelDemoEjercicioLetrasYNumeros panelDemo = new PanelDemoEjercicioLetrasYNumeros ();

		#endregion

		#region Singleton
		protected static NumbersAndVowelsExercise ejercicio = null;

		/// <summary>
		/// 
		/// </summary>
		/// <returns>
		/// A <see cref="EjercicioLetrasYNumeros"/>
		/// </returns>
		public static NumbersAndVowelsExercise GetInstance ()
		{
			if (ejercicio == null)
				ejercicio = new NumbersAndVowelsExercise ();
			
			
			return ejercicio;
		}

		/// <summary>
		/// 
		/// </summary>
		private NumbersAndVowelsExercise ()
		{
			category = ExerciseCategory.Memory;
			cifrasEscogidas = new bool[9];
			vocalesEscogidas = new bool[5];
			nivel=0;
			finalRes.CategoryId=Convert.ToInt16(ExerciseCategory.Memory);
			finalRes.CurrentLevel=nivel;
			finalRes.ExerciseId= this.idEjercicio();
			
			ExerciceExecutionResult <SingleResultVowelsNumbers> exerciceExecution =  new ExerciceExecutionResult<SingleResultVowelsNumbers>(SessionManager.GetInstance().CurSession.IdSession, SessionManager.GetInstance().CurExecInd);
			finalRes.VowelsNumberExecutionResults.Add(exerciceExecution);
			
			
		}
		#endregion

		#region Temporizador

       
		/// <summary>
		/// Inicia el temporizador que controla el tiempo que se muestra la secuencia a memorizar
		/// </summary>
		private void iniciarPeriodoMemorizacion ()
		{
			OpenPanelCen();
		    panelCen.SiguienteCaracter = cadena[0];
			elementoActualSecuencia = 1;
			GLib.Timeout.Add (tiempoEspera, new GLib.TimeoutHandler (SiguienteElementoAMemorizar));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns>
		/// A <see cref="System.Boolean"/>
		/// </returns>
		private bool SiguienteElementoAMemorizar ()
		{
			
			if (this.pararTemporizador)
				return false;
			
			if (this.elementoActualSecuencia < cadena.Length) {
				panelCen.SiguienteCaracter = cadena[this.elementoActualSecuencia];
				elementoActualSecuencia++;
					return true;
			} else
				return ocultaSecuenciaAMemorizar ();
		}

	/*	private bool SiguienteElementoAMemorizarEnsayo ()
		{
			
			if (this.pararTemporizador)
				return false;
			    
			if (this.elementoActualSecuencia < cadena.Length) {
				panelCen.SiguienteCaracter = cadena[this.elementoActualSecuencia];
				elementoActualSecuencia++;
		  	    return true;
			} else
				return ocultaSecuenciaAMemorizar ();
		}
	*/	
		/// <summary>
		/// Oculta la secuencia a memorizar cuando se ha terminado el tiempo establecido
		/// </summary>
		/// <returns>
		/// A <see cref="System.Boolean"/> indicando que el temporizador se para
		///
		private bool ocultaSecuenciaAMemorizar ()
		{
            OpenPanelDer();
			return false;
		}
		#endregion

		#region Eventos

		/// </summary>
		/// <param name="sender">
		/// A <see cref="System.Object"/>
		/// </param>
		/// <param name="e">
		/// A <see cref="System.EventArgs"/>
		/// </param>
		protected virtual void OnDeleteEjercicio (object sender, DeleteEventArgs args)
		{
			/*MessageDialog md = new MessageDialog (w, DialogFlags.DestroyWithParent, MessageType.Question, ButtonsType.YesNo, "¿Desea realmente abandonar el ejercicio?");
			
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
			}*/
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
		protected virtual void OnClickDemo (object sender, System.EventArgs e)
		{
			this.demo ();
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
		protected virtual void OnClickBotonEnsayo (object sender, System.EventArgs e)
        {
		    OpenPanelIzq2();
            
		}
        
	    protected virtual void OnClickListo (object sender, System.EventArgs e)
        {
            // comparamos la cadena objetivo con la introducida por el usuario
            tiempoFin= DateTime.Now;
			TimeSpan timeSpan = tiempoFin- tiempoInicio;
			
            bool result = this.comparaCadena (panelDer.SecuenciaIntroducida);
            if (result)
                numAciertosActuales++;
            // guardamos el resultado
            finalRes.setResultado(cadenaOrdenada,panelDer.SecuenciaIntroducida,nivel,(result==true)?"Valid": "Fail",Convert.ToInt16(timeSpan.TotalSeconds));
            //res.setResultado (nivel, intentosActuales, result);
            //res.setResultado (cadenaOrdenada,panelDer.SecuenciaIntroducida,nivel,result);
            
            // incrementamos el numero de intentos
            intentosActuales++;
            
            // si hemos llegado al final del nivel...
            if (intentosActuales == 3 && numAciertosActuales == 0) {
                // si no lo hemos superado
                //this.mostrarResultados();
                OpenPanelIzq_last();
               // this.finishExercice ();
                return;
            } else if (intentosActuales == 3 && numAciertosActuales > 0) {
                // si lo hemos superado
                this.avanzarNivel ();
                intentosActuales = 0;
                numAciertosActuales = 0;
            }
        //  panel.MuestraPanelIzquierdo(numAciertosActuales,CadenaOrdenada.Length);
        //  this.generaCadena ();
            if(nivel < 8){
                OpenPanelIzq();
                panelIzq.MuestraResultado(cadena.Length,numeroAciertos(panelDer.SecuenciaIntroducida),(CadenaLevel==null)?nivel:0);
                panelDer.resetSecuencia ();
            }
            else
                this.finishExercice();
           
        }
        
		#endregion

		#region Interfaz de Ejercicio
		/// <summary>

		/// <summary>
		/// 
		/// </summary>
		/// <returns>
		/// A <see cref="System.Boolean"/>
		/// </returns>
		public bool tieneDemo ()
		{
			return true;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns>
		/// A <see cref="System.Boolean"/>
		/// </returns>
		public bool tieneEnsayo ()
		{
			return true;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/>
		/// </returns>
		public override string ToString ()
		{
			return "Vocales y Letras";
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns>
		/// A <see cref="System.Int32"/>
		/// </returns>
		public override int idEjercicio ()
		{
			return 1;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns>
		/// A <see cref="System.Boolean"/>
		/// </returns>
		public override bool inicializar ()
		{
			Console.WriteLine("inicializar NumersAndVowelsExercise");
			return true;
		}
    
		/// <summary>
		/// 
		/// </summary>
    public override void iniciar(){
            // Changing mode to "demo" mode
            SessionManager.GetInstance().ChangeExerciseStatus("demo");
            
            // Creating panels
            ExerciseDemoNumersAndVowels demonv= new ExerciseDemoNumersAndVowels(this);
            SessionManager.GetInstance().ReplacePanel( demonv );
            demonv.InitPanel();
        }    

        //Devuelve un array con los numeros en [0] y las letras en [1]
       public string[] SetNumberVocal (string isequence){
            string [] iChoose = new string[2];
            foreach (char ic in isequence){
                if (ic>'0' & ic <='9')
                    iChoose[0]=iChoose[0]+" "+ic;
                else     
                    iChoose[1]=iChoose[1]+" "+ic;
              }
            return iChoose;
            
            }
                              
            
		//Panel con el Teclado de Números y Letra        
		public void OpenPanelDer(){
            panelDer= new PanelVocalesYNumerosDer();
			SessionManager.GetInstance().ReplacePanel(panelDer);
            int targetLenght = NumbersAndVowelsExercise.GetInstance().CadenaOrdenada.Length;  
            panelDer.CompletaPanel(targetLenght,0);
			
			tiempoInicio = DateTime.Now;
            
            if (ScreenDemo){
                //Si es la pantalla de demo
                panelDer.BotonAEnsayo.Clicked += OnClickBotonEnsayo;
                panelDer.BotonEjecutaDemo.Clicked += OnClickDemo;
                panelDer.BotonListo.Clicked += delegate{
                
                    if (panelDer.SecuenciaIntroducida.Equals(this.CadenaOrdenada) & CadenaLevel.Count==0) {
                        //Se han terminado todas las secuencias de la demo    
                        panelDer.MuestraExplanation(SetNumberVocal(cadenaOrdenada),true );
                        panelDer.BotonEjecutaDemo.Visible=false;
                        panelDer.BotonAEnsayo.Visible=true;
                        ScreenDemo=false;
                        avanzarNivel();
                    }
                    else {
                        //Tenemos más secuencias pendientes en la demo
                       if (panelDer.SecuenciaIntroducida.Equals(this.CadenaOrdenada)){
                           //Hemos introducido la secuencia de la demo correctamente     
                           panelDer.MuestraExplanation(SetNumberVocal(cadenaOrdenada),true);
                           }
                       else {
                             //Nos hemos equivocado en la secuencia de la demo.
                             AppendSequence(cadena);     
                             panelDer.MuestraExplanation(SetNumberVocal(cadenaOrdenada),false);
                            }
                       panelDer.BotonEjecutaDemo.Visible = true;
                       panelDer.BotonAEnsayo.Visible = false;        
                     }
                }; //fin del delegate del BotonListo
            }
            else{
                //Estamos en el  ejercicio
                panelDer.MuestraTitle();
                panelDer.BotonListo.Clicked += OnClickListo;
                panelDer.BotonEjecutaDemo.Visible=false;
                panelDer.BotonAEnsayo.Visible=false;
                
               }
       
		}
		//Panel con la Pizarra
		public void OpenPanelCen(){
            this.generaCadena();
            panelCen=new PanelVocalesYNumerosCen();
			SessionManager.GetInstance().ReplacePanel(panelCen);
		}
		//Panel que pasa de la pantalla de demo a la de ejercicio
		public void OpenPanelIzq(){
              panelIzq=new PanelVocalesYNumerosIzq();
              SessionManager.GetInstance().ReplacePanel(panelIzq);
              panelIzq.BotonEmpezarSecuencia.Clicked += delegate{
              this.iniciarPeriodoMemorizacion ();
            };
            
		}
            //Panel que va a iniciar una nueva secuencia y muestra los resultados de la secuencia anterior
        public void OpenPanelIzq2(){
            nivel=1;
            finalRes.CurrentLevel=nivel;
            panelIzq2=new PanelVocalesYNumerosIzq2();
            SessionManager.GetInstance().ReplacePanel(panelIzq2);
            panelIzq2.BotonEmpezarSecuencia.Clicked += delegate{
                ScreenDemo=false;
                this.iniciarPeriodoMemorizacion ();
            };
         
        }
		//Panel que muestra los resultados de la secuencia anterior y finaliza ejercicio
        public void OpenPanelIzq_last(){
              panelIzq=new PanelVocalesYNumerosIzq();
              panelIzq.MuestraResultado(cadena.Length,numeroAciertos(panelDer.SecuenciaIntroducida),(CadenaLevel==null)?nivel:0);
              SessionManager.GetInstance().ReplacePanel(panelIzq);
              panelIzq.BotonEmpezarSecuencia.Clicked += delegate{
                finishExercice();
            };
        }
        //Iniciamos la demo
		public void iniciar2 ()
		{ 
            ScreenDemo=true;
			demo();
         }
    
		public void demo()
		{
            OpenPanelCen();
            panelCen.SiguienteCaracter = cadena[this.elementoActualSecuencia];
            GLib.Timeout.Add (tiempoEspera, new GLib.TimeoutHandler (SiguienteElementoAMemorizar));
   		}
        //Finalizamos la ejecución del ejercicio. Hemos fallado 3 intentos
		public override void finalizar ()
		{
			/*this.pararTemporizador = true;
			ejercicio.CurrentLevel=nivel;
			ejercicio.Serialize ();
			ejercicio = null;
			
			if (nivel < 3)
				medalValue = 0;
			else if (nivel >= 3 && nivel < 6)
				medalValue = 60;
			else
				medalValue = 100;
			
			SessionManager.GetInstance ().ExerciseFinished (medalValue);
			SessionManager.GetInstance ().TakeControl ();*/
			ejercicio =null;
		}
		
		public void finishExercice(){
			this.pararTemporizador = true;
			
			
			if (nivel < 3)
				medalValue = 0;
			else if (nivel >= 3 && nivel < 6)
				medalValue = 60;
			else
				medalValue = 100;
			
			ejercicio.CurrentLevel=nivel;
			ejercicio.Serialize (medalValue);
			ejercicio = null;
			
			SessionManager.GetInstance ().ExerciseFinished (medalValue);
			SessionManager.GetInstance ().TakeControl ();
		}

		public void avanzarNivel ()
		{
            if (Isfixed){
                   CadenaLevel=null;
            }
         	if (longitudCadena <= 8)
				longitudCadena++;
			
			lastOvercomeLevel=nivel;
			nivel++;
			finalRes.CurrentLevel++;
		}

		public void retrocederNivel ()
		{
			if (longitudCadena >= 3)
				longitudCadena--;
			nivel--;
			finalRes.CurrentLevel--;
		}
		#endregion

		#region Generación Cadena

		public static int GetPosVocal (char cc)
		{
			int i;
			
			switch (cc) {
			
			case 'a':
				i = 0;
				break;
			
			case 'e':
				i = 1;
				break;
			
			case 'i':
				i = 2;
				break;
			
			case 'o':
				i = 3;
				break;
			
			case 'u':
				i = 4;
				break;
			default:
				
				i = -1;
				break;
			}
			
			return i;
		}

		public static int CharToInt (char cc)
		{
			int i;
			
			switch (cc) {
			
			case '1':
				i = 1;
				break;
			
			case '2':
				i = 2;
				break;
			
			case '3':
				i = 3;
				break;
			
			case '4':
				i = 4;
				break;
			
			case '5':
				i = 5;
				break;
			
			case '6':
				i = 6;
				break;
			
			case '7':
				i = 7;
				break;
			
			case '8':
				i = 8;
				break;
			
			case '9':
				i = 9;
				break;
			default:
				
				i = -1;
				break;
			}
			
			return i;
		}

		/// <summary>
		/// Genera un digito aleatoriamente un digito entre 1 y 9
		/// </summary>
		/// <param name="r">
		/// A <see cref="Random"/>
		/// </param>
		/// <returns>
		/// A <see cref="System.Char"/>
		/// </returns>
		public static char RandomDigit (Random r)
		{
			
			char generado = '-';
			int valor = r.Next (1, 10);
			
			generado = (char)((int)'0' + valor);
			
			return generado;
		}

		/// <summary>
		/// Genera una vocal aleatoriamente
		/// </summary>
		/// <param name="r">
		/// A <see cref="Random"/>
		/// </param>
		/// <returns>
		/// A <see cref="System.Char"/>
		/// </returns>
		public static char RandomVowel (Random r)
		{
			int valor = r.Next (10, 15);
			char generado = '-';
			switch (valor) {
			
			case 10:
				generado = 'a';
				break;
			
			case 11:
				generado = 'e';
				break;
			
			case 12:
				generado = 'i';
				break;
			
			case 13:
				generado = 'o';
				break;
			
			case 14:
				generado = 'u';
				break;
			}
			
			return generado;
		}

		/// <summary>
		/// Baraja los caracteres que forma una cadena
		/// </summary>
		/// <param name="s">
		/// A <see cref="System.String"/>
		/// </param>
		/// <param name="r">
		/// A <see cref="Random"/>
		/// </param>
		public static void ShuffleString (ref string s, Random r)
		{
			char[] c = s.ToCharArray ();
			
			for (int i = 0; i < c.Length; i++) {
				int pos = i + r.Next (0, c.Length - i);
				// Random remaining position.
				char temp = c[i];
				c[i] = c[pos];
				c[pos] = temp;
			}
			
			s = new String (c);
		}

        protected void generaCadena(){
            if (Isfixed)
                generaCadenafixed();
            else
                generaCadenaRandow();
            
        }
        protected void AppendSequence(string isequence){
           
                CadenaLevel.Add(isequence);
         }
        
        protected void generaCadenafixed ()
            {
            cadena = "";
            this.elementoActualSecuencia = 0;
                if (CadenaLevel==null){
                    CadenaLevel=new List<string>();
                    CadenaLevel=TablaSequences[nivel];
                }
             cadena=CadenaLevel[0];
             CadenaLevel.Remove(cadena);
           
             // ordenamos la cadena y la guardamos para la posterior comparación
             char[] c = cadena.ToCharArray ();
             Array.Sort (c);
             cadenaOrdenada = new String (c);
                
            }
		/// <summary>
		/// Genera la cadena a memorizar
		/// </summary>
		protected void generaCadenaRandow ()
		{
			InicializarVectoresEscogidas ();
			
			int totalVocales = (int)Math.Floor (longitudCadena / 2.0);
			int totalCifras = (int)Math.Ceiling (longitudCadena / 2.0);
			int numVocales = 0;
			int numCifras = 0;
			
			cadena = "";
			this.elementoActualSecuencia = 0;
			Random r = new Random (DateTime.Now.Millisecond);
			
			char generado;
			int pos;
			
			// añadimos las cifras
			do {
				generado = RandomDigit (r);
				pos = CharToInt (generado) - 1;
				
				if (!cifrasEscogidas[pos]) {
					cadena += generado;
					cifrasEscogidas[pos] = true;
					numCifras++;
				}
			} while (numCifras < totalCifras);
			
			// añadimos las vocales
			do {
				generado = RandomVowel (r);
				pos = GetPosVocal (generado);
				
				if (!vocalesEscogidas[pos]) {
					cadena += generado;
					vocalesEscogidas[pos] = true;
					numVocales++;
				}
			} while (numVocales < totalVocales);
			
			// barajamos los elementos de la cadena
			ShuffleString (ref cadena, r);
			
			// ordenaos la cadena y la guardamos para la posterior comparación
			char[] c = cadena.ToCharArray ();
			Array.Sort (c);
			cadenaOrdenada = new String (c);
            
		}

		protected void InicializarVectoresEscogidas ()
		{
			// reinicializamos los valores para la proxima cadena
			for (int i = 0; i < 9; ++i)
				cifrasEscogidas[i] = false;
			
			for (int i = 0; i < 5; ++i)
				vocalesEscogidas[i] = false;
		}
		
		protected void SetFinalResult(int scoreArg){
			
			finalRes.VowelsNumberExecutionResults[finalRes.VowelsNumberExecutionResults.Count -1].TotalCorrects =0;
			finalRes.VowelsNumberExecutionResults[finalRes.VowelsNumberExecutionResults.Count -1].TotalFails =0;
			finalRes.VowelsNumberExecutionResults[finalRes.VowelsNumberExecutionResults.Count -1].TotalTimeElapsed =0;
			finalRes.VowelsNumberExecutionResults[finalRes.VowelsNumberExecutionResults.Count -1].Score =scoreArg;
			foreach(SingleResultVowelsNumbers sr in finalRes.VowelsNumberExecutionResults[finalRes.VowelsNumberExecutionResults.Count -1].SingleResults){
				if( sr.Result.Equals("Valid"))
					finalRes.VowelsNumberExecutionResults[finalRes.VowelsNumberExecutionResults.Count -1].TotalCorrects ++;
				
				if( sr.Result.Equals("Fail"))
					finalRes.VowelsNumberExecutionResults[finalRes.VowelsNumberExecutionResults.Count -1].TotalFails ++;
			
				finalRes.VowelsNumberExecutionResults[finalRes.VowelsNumberExecutionResults.Count -1].TotalTimeElapsed += sr.TimeElapsed;
				
			}
		}
		
		#endregion

		#region Comparación Cadena
		protected bool comparaCadena (string aComparar)
		{
			bool b = cadenaOrdenada.Equals (aComparar);
			
			return b;
		}
        protected int numeroAciertos(string aComparar){
            int i=aComparar.Length-1;
            int comp=0;
            while ( i>=0){
                 if (aComparar[i]==CadenaOrdenada[i])
                    comp++;
                 i--;
            }
            return comp;
            
        }
		#endregion
		public override void pausa ()
		{
			throw new System.NotImplementedException ();
		}

		#region implemented abstract members of pesco.Ejercicio
		public override string NombreEjercicio ()
		{
			return "Números y Vocales";
		}

		#endregion

		#region XML serialization

		protected static string xmlUserFile = "numbers-and-vowels.xml";

		public virtual void Serialize (int score)
		{
		
			SetFinalResult(score);
			XmlUtil.SerializeForUser<ResultadoEjercicioLetrasYNumeros>(finalRes,Configuration.Current.GetExerciseConfigurationFolderPath () + "/NumbersAndVowels.xml");
			string fullPath = Configuration.Current.GetExerciseConfigurationFolderPath () + "/" + xmlUserFile;
			
			/*XmlTextWriter escritor = new XmlTextWriter (fullPath, null);
			
			try {
				escritor.Formatting = Formatting.Indented;
				
				escritor.WriteStartDocument ();
				
				escritor.WriteDocType ("numbers-and-vowels-exercise", null, null, null);
				
				// hoja de estilo para pode ver en un navegador el xml
				//escritor.WriteProcessingInstruction("xml-stylesheet", "type='text/xsl' href='cuestionario.xsl'");
				
				XmlSerializer serializer = new XmlSerializer (typeof(NumbersAndVowelsExercise));
				
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

		public static NumbersAndVowelsExercise Deserialize ()
		{

			// De aqui para abajo hay que quitarlo porque el ejecicio no debe dependender de esto	
			string fullPath = Configuration.Current.GetExerciseConfigurationFolderPath () + "/" + xmlUserFile;
			
			if (!File.Exists (fullPath)) {
				GetInstance ();
				ejercicio.Serialize (0);
				return ejercicio;
				/*string s = Environment.CommandLine;			
				fullPath = Configuration.CommandDirectory + "/ejercicios/ObjetosClasificables/xml-templates/" + xmlUserFile;*/				
			}			
			
			XmlTextReader lector = new XmlTextReader (fullPath);
			
			//try {
				ejercicio = new NumbersAndVowelsExercise ();
				
				XmlSerializer serializer = new XmlSerializer (typeof(NumbersAndVowelsExercise));
				ejercicio = (NumbersAndVowelsExercise)serializer.Deserialize (lector);
				
				lector.Close ();
				return (NumbersAndVowelsExercise)ejercicio;
			/*} catch (Exception e) {
				lector.Close ();
				return null;
			}*/
		}
		#endregion
		
	}
}

