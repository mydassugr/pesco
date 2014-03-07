using System;
using Gtk;
using Gdk;


namespace pesco
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class PanelVocalesYNumerosDer : Gtk.Bin
	{
		string secuenciaIntroducida = "";
		
		public PanelVocalesYNumerosDer ()
		{
			this.Build ();
            ChangeButton();
			GtkUtil.SetStyle(this.botonListo, Configuration.Current.ButtonFont);
			GtkUtil.SetStyle(botonEjecutaDemo, Configuration.Current.ButtonFont);
			GtkUtil.SetStyle(this.BotonAEnsayo, Configuration.Current.ButtonFont);
          
            GtkUtil.SetStyle(this.explanation, Configuration.Current.MediumFont);
            GtkUtil.SetStyle(this.labelTitle,Configuration.Current.LargeFont);
			BotonAEnsayo.Visible=false;
       	}
		public Button BotonListo {
			get { return this.botonListo; }
		}
		
        
		public Button BotonAEnsayo{
			get{
				return this.botonAEnsayo;	
			}
		}
		/// 
		/// </summary>
		public string SecuenciaIntroducida {
			get { return secuenciaIntroducida; }
		}
		//Primer boton q se ejecuta en ensayo
		public Button BotonEjecutaDemo {
			get	{
				return 	this.botonEjecutaDemo;
			}
		}
        
        public void ChangeColor(Button iButton){
           iButton.ModifyBg(StateType.Active, new Gdk.Color(0x2d, 0x13, 0xea));
           iButton.ModifyBg(StateType.Prelight, new Gdk.Color(0xb6, 0x00, 0x00));
        }
        //Cambia el color de los botones
        public void ChangeButton(){
            ChangeColor(boton1);
            ChangeColor(boton2);
            ChangeColor(boton3);
            ChangeColor(boton4);
            ChangeColor(boton5);
            ChangeColor(boton6);
            ChangeColor(boton7);
            ChangeColor(boton8);
            ChangeColor(boton9);
            ChangeColor(botonA);
            ChangeColor(botonE);
            ChangeColor(botonI);
            ChangeColor(botonO);
            ChangeColor(botonU);
        }
        //Controla el hbox con las imágenes de las casillas
		//ibox=0 Crea un hbox de casillas con la longitud de la secuencia a introducir
        //ibox=1 Marca una casilla
        //ibox=2 Desmarca una casilla
		public void CompletaPanel(int iNumber, uint ibox){
            //Crea el hbox con casillas en blanco
            if (ibox==0){
               for (int i = 1; i <= iNumber; i++){
                 Gtk.Image iBox = new Gtk.Image(Gdk.Pixbuf.LoadFromResource("pesco.ejercicios.resources.img.numersvowels.casilla1.gif"));
                  hboxCompletado.PackStart(iBox,false,false,0);
                }
            }
            //Añade marca a la casilla
            if (ibox==1){
                int i=1;
                foreach (Gtk.Image iBox in hboxCompletado.Children){
                         if (i == iNumber){
                             Gdk.Pixbuf iBoxPix = Gdk.Pixbuf.LoadFromResource ("pesco.ejercicios.resources.img.numersvowels.casilla2.gif");
                             iBox.Pixbuf=iBoxPix;
                          }  
                    i++;
                }
               
             }
            //Quita la marca a casilla
             if (ibox==2){
                int i=1;
                foreach (Gtk.Image iBox in hboxCompletado.Children){
                         if (i==iNumber){
                             Gdk.Pixbuf iBoxPix = Gdk.Pixbuf.LoadFromResource ("pesco.ejercicios.resources.img.numersvowels.casilla1.gif");
                             iBox.Pixbuf=iBoxPix;
                          }  
                    i++;
                }
                
            }
        hboxCompletado.ShowAll();
        }
        public void HideExplanation(){
               explanation.Text="";
               explanation.Hide();
        }
        public void MuestraTitle(){
            explanation.Markup="        <b>Introduce los Números y las Vocales</b>";
        }
		public void MuestraExplanation(string[] iCadena, bool isCorrect){
             //explanation.Markup = "<span color=\"blue\">"+"<b>Pista:</b> " + iex+"</span>";
            string exn="";
            string exv="";
            if (iCadena[0].Length>2)
                     exn="los nùmeros";
                 else exn="el número";
            if (iCadena[1].Length>2)
                     exv="las vocales";
                 else exv="la vocal";
                 
            if (isCorrect){
                explanation.Markup="<span color=\"blue\">¡Muy bien! Has pulsado primero "+exn+"<b> " + iCadena[0]+ "</b> y luego "+exv+" <b>" + iCadena[1]+  "</b>.\n</span><span color='black'>Pulsa el botón <b>Ensayar</b> para continuar.</span>";              
		    }
            else {
                explanation.Markup = "<span color=\"blue\">Has pulsado <b>"+secuenciaIntroducida+ "</b> y tendrías que haber pulsado primero "+exn+" <b>" + iCadena[0]+ "</b> y luego "+exv+" <b>" + iCadena[1] + "</b>.\n</span><span color='black'>Pulsa el botón <b>Ensayar</b> para continuar.</span>";
            }
            bombilla.PixbufAnimation= new Gdk.PixbufAnimation (Configuration.CommandDirectory+"/ejercicios/resources/img/bombillamovil.gif");
            bombilla.Visible=true;
            imagepepe.PixbufAnimation= new Gdk.PixbufAnimation (Configuration.CommandDirectory+"/ejercicios/resources/img/numersvowels/pepehablanv.gif");
		//	explanation.Markup="<span size='xx-large'>"+iex+"</span>";
			explanation.Show();
		}
		
        public void TitleDemo(){
               
               labelTitle.SetAlignment(1/2,1/2);
               labelTitle.Markup="                       <big><big><big><big>Pantalla de demostración Números y Vocales</big></big></big></big>";
        }
      
		public void resetSecuencia ()
		{
			secuenciaIntroducida = "";
		}
		
		#region Eventos

		public void ActualizarSecuencia(char c)
		{
            int targetLenght = NumbersAndVowelsExercise.GetInstance().CadenaOrdenada.Length;
            
           	if (secuenciaIntroducida.Length <targetLenght)
			{  	secuenciaIntroducida += c;
                CompletaPanel(secuenciaIntroducida.Length,1);
			}
            Console.WriteLine("secuencia introducida:  "+secuenciaIntroducida);
			if (secuenciaIntroducida.Length >= targetLenght)
			{
                Console.WriteLine("Boton listo:  "+secuenciaIntroducida);
                table1.Sensitive=false;
                table2.Sensitive=false;
                //BotonRemove.Sensitive=false;
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
			ActualizarSecuencia('a');
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
			ActualizarSecuencia('e');
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
			ActualizarSecuencia('i');
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
			ActualizarSecuencia('o');
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
			ActualizarSecuencia('u');
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

	/*	protected virtual void OnClickBackspace (object sender, System.EventArgs e)
		{
			secuenciaIntroducida = secuenciaIntroducida.Remove (secuenciaIntroducida.Length - 1, 1);
		}
	*/	
	
  
        
  protected virtual void OnBotonRemoveClicked (object sender, System.EventArgs e)
        {
            if (secuenciaIntroducida.Length>0) {
                CompletaPanel(secuenciaIntroducida.Length,2);
                secuenciaIntroducida = secuenciaIntroducida.Remove (secuenciaIntroducida.Length - 1, 1);
            }
        }
        
        #endregion    
        
      		       
	}
}

