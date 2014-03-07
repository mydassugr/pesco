
using System;
using Gtk;
using Gdk;
using System.Threading;

namespace pesco
{

	/// <summary>
	/// 
	/// </summary>
	[System.ComponentModel.ToolboxItem(true)]
	public partial class PanelVocalesYNumeros : Gtk.Bin
	{
		bool rehersalMode = false;
		

		public bool RehersalMode {
			get {
				return this.rehersalMode;
			}
			set {
				rehersalMode = value;
			}
		}
		int previouscorrect;
		int previoustotal;
		
		string secuenciaIntroducida = "";
        bool IsDemo=true;

		/// <summary>
		/// 
		/// </summary>
		public PanelVocalesYNumeros ()
		{
			this.Build ();	
             Console.WriteLine("Panel nuevo");   
            
            labelPrevius.Markup="<span size='xx-large'>De "+previoustotal+"  has acertado  "+previouscorrect+"</span>";
            labelPrevius.Show();
           
	//		progressbar1.Fraction = 0.0;
			
			/*GtkUtil.SetStyle(boton1, Configuration.Current.HugeFont);
			GtkUtil.SetStyle(boton2, Configuration.Current.HugeFont);
			GtkUtil.SetStyle(boton3, Configuration.Current.HugeFont);
			GtkUtil.SetStyle(boton4, Configuration.Current.HugeFont);
			GtkUtil.SetStyle(boton5, Configuration.Current.HugeFont);
			GtkUtil.SetStyle(boton6, Configuration.Current.HugeFont);
			GtkUtil.SetStyle(boton7, Configuration.Current.HugeFont);
			GtkUtil.SetStyle(boton8, Configuration.Current.HugeFont);
			GtkUtil.SetStyle(boton9, Configuration.Current.HugeFont);
			
			GtkUtil.SetStyle(botonA, Configuration.Current.HugeFont);
			GtkUtil.SetStyle(botonE, Configuration.Current.HugeFont);
			GtkUtil.SetStyle(botonI, Configuration.Current.HugeFont);
			GtkUtil.SetStyle(botonO, Configuration.Current.HugeFont);
			GtkUtil.SetStyle(botonU, Configuration.Current.HugeFont);			*/
			GtkUtil.SetStyle(this.labelCaracter, Configuration.Current.ExtraHugeFont);	
			labelCaracter.ModifyFg(StateType.Normal, new Gdk.Color(255, 255, 255));
			GtkUtil.SetStyle(this.botonListo, Configuration.Current.ButtonFont);
			GtkUtil.SetStyle(this.botonListo1, Configuration.Current.ButtonFont);
		//	progressbar1.Fraction = 0.0;
			//GtkUtil.SetStyle(label5, Configuration.Current.BubbleStyle);
			label5.ModifyFont(Pango.FontDescription.FromString("Ahafoni CLM Bold 10"));
		}

		
        public void CompletaPanel(int iNumber, bool iAppend){
            if (iAppend){
                Console.WriteLine("Añadir");
                foreach (Widget wid in hboxCompletado.Children){
                          Remove(wid);
                           wid.Destroy();
                          }  
               for (int i = 1; i <= iNumber; i++){
                  DrawingArea dr = new DrawingArea();
                  dr.WidthRequest=40;
                  dr.HeightRequest=40;
                  dr.ModifyBg(StateType.Normal,new Gdk.Color(0xf8, 0xf5, 0x0b));
                  hboxCompletado.PackStart(dr,false,false,0);
                }
                hboxCompletado.Show();
            }
            else{
                Console.WriteLine("Borrar");
                 int i=0;
                foreach (Widget wid in hboxCompletado.Children){
                         if (i <= iNumber){
                             wid.ModifyBg(StateType.Normal,new Gdk.Color(0xfb, 0x00, 0x00));
                             i++;
                          }      
                }
                hboxCompletado.Show();
            }
       
        }
        public void HideExplanation(){
               explanation.Text="";
               explanation.Hide();
        }
		public void MuestraExplanation(char CadOne, char CadTwo, bool isCorrect){
            if (isCorrect){
			explanation.Markup="<span size='xx-large'>Lo has hecho bien, porque has pulsado primero el número <b>" + CadOne+ "</b> y luego la vocal <b>" + CadTwo+ "</b>.</span>";
          //  explanation.Markup = "<span color=\"blue\">"+"<b>Explicación:</b> " +iex+"</span>";
            }
            else {
                explanation.Markup = "<span size='xx-large'>Tendrías que haber pulsado primero el número <b>" + CadOne+ "</b> y luego la vocal <b>" + CadTwo + "</b>. Pulsa el botón <b>Ensayar</b> para repetir el ensayo.</span>";
            }
            imagepepe.PixbufAnimation= new Gdk.PixbufAnimation (Configuration.CommandDirectory+"/ejercicios/resources/img/reasoning/pepehabla.gif");
		//	explanation.Markup="<span size='xx-large'>"+iex+"</span>";
			explanation.Show();
		}
		
        public void TitleDemo(){
               
               labelTitle.SetAlignment(1/2,1/2);
               labelTitle.Markup="                       <big><big><big><big>Pantalla de demostración Números y Vocales</big></big></big></big>";
        }
            
		public void MuestraPanelDerecho()
		{
			 int targetLenght = NumbersAndVowelsExercise.GetInstance().CadenaOrdenada.Length;  
            Console.WriteLine ("Secuencia inicialaa:  "+ targetLenght);
            CompletaPanel(targetLenght,true);
			hbox8.Show();
			table2.Hide();
			vbox3.Hide();
			vbox4.ShowAll();
			BotonListo.Hide();
		}
		public void MuestraPanelCentral()
		{			
         	table2.ShowAll();
			vbox4.Hide();
			vbox3.Hide();
			hbox8.Hide();
		}
		
		public void MuestraPanelIzquierdo(){
            IsDemo=false;
            this.HeightRequest=660;
            this.WidthRequest=1250;
			hbox8.Hide();
			table2.Hide();
			vbox4.Hide();
			vbox3.ShowAll();
		}
		public void MuestraPanelIzquierdo(int Pc,int Pt){
		    previoustotal=Pt;
			previouscorrect=Pc;
			hbox8.Hide();
			table2.Hide();
			vbox4.Hide();
			vbox3.ShowAll();
		}
		public char SiguienteCaracter
		{
			set {
				this.labelCaracter.Text = "" + value;
				labelCaracter.ShowAll();
			}
		}
		
		public void SetNextExerciseMessage()
		{
			
		}

		public void EjecutaDemo (string cadena)
		{
			//Thread.Sleep(1500);
			
			char[] caracteres = cadena.ToCharArray ();
			
			foreach (char c in caracteres) {
				switch (c) {
				case '1':					
					this.simulaClick (boton1);										
					break;
				
				case '2':
					this.simulaClick (boton2);
					break;
				case '3':
					this.simulaClick (boton3);
					break;
				case '4':
					this.simulaClick (boton4);
					break;
				case '5':
					this.simulaClick (boton5);
					break;
				case '6':
					this.simulaClick (boton6);
					break;
				case '7':
					this.simulaClick (boton7);
					break;
				case '8':
					this.simulaClick (boton8);
					break;
				case '9':
					this.simulaClick (boton9);
					break;
				case 'A':
					this.simulaClick (botonA);
					break;
				case 'E':
					this.simulaClick (botonE);
					break;
				case 'I':
					this.simulaClick (botonI);
					break;
				case 'O':
					this.simulaClick (botonO);
					break;
				case 'U':
					this.simulaClick (botonU);
					break;
					
				}
				
				
			}
		}

		private void simulaClick (Button b)
		{
         	int x, y;
			this.ParentWindow.GetPosition (out x, out y);
			
			this.Display.WarpPointer (this.Screen, b.Allocation.X + x + 7, b.Allocation.Y + y + 7);
			Thread.Sleep (500);
			b.Click ();
			Thread.Sleep (500);
		}

		#region Properties
		/// <summary>
		/// 
		/// </summary>
		public string SecuenciaIntroducida {
			get { return secuenciaIntroducida; }
		}

		/// <summary>
		/// 
		/// </summary>
		public Button BotonListo {
			get { return this.botonListo; }
		}

		public Button BotonEmpezarEjercicio {
			get { return this.botonListo1; }
		}
		
		#endregion

		#region reset
		/// <summary>
		/// 
		/// </summary>
		public void clearSecuenciaIntroducida ()
		{
			resetSecuencia ();
		}

		/// <summary>
		/// 
		/// </summary>
		public void resetSecuencia ()
		{
			secuenciaIntroducida = "";
		}
		#endregion

		#region bloqueo teclados
		/// <summary>
		/// 
		/// </summary>
		public void bloquear ()
		{
			
			vbox4.Hide();
			
			this.boton1.Sensitive = false;
			this.boton2.Sensitive = false;
			this.boton3.Sensitive = false;
			this.boton4.Sensitive = false;
			this.boton5.Sensitive = false;
			this.boton6.Sensitive = false;
			this.boton7.Sensitive = false;
			this.boton8.Sensitive = false;
			this.boton9.Sensitive = false;
			this.botonA.Sensitive = false;
			this.botonE.Sensitive = false;
			this.botonI.Sensitive = false;
			this.botonO.Sensitive = false;
			this.botonU.Sensitive = false;
			this.botonListo.Sensitive = false;
		}

		/// <summary>
		/// 
		/// </summary>
		public void desbloquear ()
		{
			this.boton1.Sensitive = true;
			this.boton2.Sensitive = true;
			this.boton3.Sensitive = true;
			this.boton4.Sensitive = true;
			this.boton5.Sensitive = true;
			this.boton6.Sensitive = true;
			this.boton7.Sensitive = true;
			this.boton8.Sensitive = true;
			this.boton9.Sensitive = true;
			this.botonA.Sensitive = true;
			this.botonE.Sensitive = true;
			this.botonI.Sensitive = true;
			this.botonO.Sensitive = true;
			this.botonU.Sensitive = true;
			this.botonListo.Sensitive = true;
		}

		#endregion

		#region Eventos

		public void ActualizarSecuencia(char c)
		{
            int targetLenght = NumbersAndVowelsExercise.GetInstance().CadenaOrdenada.Length;
           	if (secuenciaIntroducida.Length <targetLenght)
			{
                CompletaPanel(secuenciaIntroducida.Length,false);
           		secuenciaIntroducida += c;
			}
			if (secuenciaIntroducida.Length >= targetLenght)
			{
				this.botonListo.Click();
			}
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
		protected virtual void OnClickOne (object sender, System.EventArgs e)
		{
			ActualizarSecuencia('1');
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
		protected virtual void OnClickTwo (object sender, System.EventArgs e)
		{
			ActualizarSecuencia('2');
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
		protected virtual void OnClickThree (object sender, System.EventArgs e)
		{
			ActualizarSecuencia('3');
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
		protected virtual void OnClickFour (object sender, System.EventArgs e)
		{
			ActualizarSecuencia('4');
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
		protected virtual void OnClickFive (object sender, System.EventArgs e)
		{
			ActualizarSecuencia('5');
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
		protected virtual void OnClickSix (object sender, System.EventArgs e)
		{
			ActualizarSecuencia('6');
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
		protected virtual void OnClickSeven (object sender, System.EventArgs e)
		{
			ActualizarSecuencia('7');
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
		protected virtual void OnClickEight (object sender, System.EventArgs e)
		{
			ActualizarSecuencia('8');
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
		protected virtual void OnClickNine (object sender, System.EventArgs e)
		{
			ActualizarSecuencia('9');
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
		protected virtual void OnClickA (object sender, System.EventArgs e)
		{
			ActualizarSecuencia('A');
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
		protected virtual void OnClickE (object sender, System.EventArgs e)
		{
			ActualizarSecuencia('E');
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
		protected virtual void OnClickI (object sender, System.EventArgs e)
		{
			ActualizarSecuencia('I');
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
		protected virtual void OnClickO (object sender, System.EventArgs e)
		{
			ActualizarSecuencia('O');
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
		protected virtual void OnClickU (object sender, System.EventArgs e)
		{
			ActualizarSecuencia('U');
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
		protected virtual void OnClickLimpiar (object sender, System.EventArgs e)
		{
			secuenciaIntroducida = "";
			//LabelIntroducido.Text = "";
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
		protected virtual void OnClickBackspace (object sender, System.EventArgs e)
		{
			secuenciaIntroducida = secuenciaIntroducida.Remove (secuenciaIntroducida.Length - 1, 1);
		}
		
		#endregion
                		
	}
}
