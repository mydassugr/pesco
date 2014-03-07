using System;
using System.IO;
using System.Collections.Generic;
using Gtk;


namespace pesco
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class ExercisePanelWordListLong : Gtk.Bin
    {
        const int WIDTH_SCREEN = 1000;
        const int HEIGHT_SCREEN = 600;
        WordListExercise wle= null;
    
        // Draw elements
        Gtk.DrawingArea drawingArea = new Gtk.DrawingArea();
        private Gdk.Pixbuf bgPixbuf;
        private Gdk.Pixbuf backgroundPixbuf;
        private Gdk.Pixbuf dialogPixbuf;
        private Gdk.Pixbuf pepe1Pixbuf;
        private Gdk.Pixbuf pepe2Pixbuf;
        int pepeStatus = 0;
        Gdk.GC auxGC;
        Gdk.Pixbuf auxPixbuf1;
        Gdk.Pixbuf auxPixbuf2;
        Gdk.Pixbuf auxPixbuf3;
        Gdk.Pixbuf auxPixbuf4;
        
        // Panel test
        int validChecked = 0;

        // Animations
        string stringToSay;
        int currentStep = 0;
        int currentCharacter = 0;
        int totalCharacters = 0;
      
  string [] animationsText={"<span color='black'>            Lista de Palabras Largo Plazo</span>\n"+
            "Ahora vamos a ver si recuerdas la LISTA AZUL que memorizaste hace un rato. Te la mostré tres veces.",
            "En primer lugar, tendrás que decir en voz alta las palabras que recuerdes."+
            " Necesitarás de nuevo el micrófono, si no lo tienes pídelo al dinamizador o tutor",
            "En segundo lugar, deberás seleccionar las palabras que recuerdes de la LISTA AZUL." +
            "\nCuando estés preparado para continuar con el ejercicio pulsa<span color='black'> ¡Continuar Ejercicio!</span>"};       
        bool firstFrameStep = true;
        
        // Timers
        private int auxAnimationTimer;
        private int auxAnimationTimer2;
        private uint auxTimer;
        private GLib.TimeoutHandler currentHandler;
        private uint currentInterval;
        private bool pausedExercise;
        private const int REPAINT_SPEED = 50;
        int pauseTimer = 100;

        private string DirExer =Configuration.DirExerImage+"wordlist"+System.IO.Path.DirectorySeparatorChar;
        // Layout 
        Pango.Layout auxLayout;

        public ExercisePanelWordListLong( WordListExercise ex)
        {
            this.Build ();
            this.wle = ex;
            GtkUtil.SetStyle( buttonGoBack, Configuration.Current.MediumFont );
            GtkUtil.SetStyle( buttonGoForward, Configuration.Current.MediumFont );
            GtkUtil.SetStyle( buttonGoLast, Configuration.Current.MediumFont );
            GtkUtil.SetStyle( buttonStartExercise, Configuration.Current.MediumFont );
            
        }

        private void InitTexts ()
        {

        }


        private void InitLayout ()
        {
            auxLayout = new Pango.Layout (this.CreatePangoContext ());
            auxLayout.Width = Pango.Units.FromPixels (750);
            auxLayout.Justify = false;
            auxLayout.Wrap = Pango.WrapMode.Word;
            auxLayout.Alignment = Pango.Alignment.Left;
            auxLayout.FontDescription = Pango.FontDescription.FromString ("Ahafoni CLM Bold 20");
            
            // gc.RgbFgColor = new Gdk.Color( 0, 255, 0 );          
        }

        public void InitPanel ()
        {
            
            InitTexts ();
            InitLayout ();

            imageBackground.ExposeEvent += Expose_Event;
            backgroundPixbuf = new Gdk.Pixbuf(DirExer+"backgroundwl1.png");
            dialogPixbuf = new Gdk.Pixbuf(DirExer+"dialog.png");
            pepe1Pixbuf = new Gdk.Pixbuf(DirExer+"pepewl1.png");
            pepe2Pixbuf = new Gdk.Pixbuf(DirExer+"pepewl2.png");
            
           
            imageBackground.Pixmap = new Gdk.Pixmap( hboxCentral.GdkWindow, WIDTH_SCREEN, HEIGHT_SCREEN, 24);
            auxGC = new Gdk.GC(imageBackground.Pixmap);
            
            if ( auxTimer != null && auxTimer > 0 ) 
                GLib.Source.Remove(auxTimer);
            
            currentHandler = new GLib.TimeoutHandler (Update);
            currentInterval = 25;
            auxTimer = GLib.Timeout.Add (currentInterval, currentHandler);
            
        }

        void Expose_Event (object obj, ExposeEventArgs args)
        {
            // Draw background
            DrawDialogBackground();
            
            // Hello
            if (currentStep == 0) {             
                if ( FirstFrameStep() ) {
                    buttonGoBack.Sensitive = false;
                    stringToSay = animationsText[currentStep];
                    totalCharacters = animationsText[currentStep].Length;
                    buttonStartExercise.HideAll();
                    buttonGoLast.HideAll();
                }
                PepeUtils.IncrementCharacterDialog(ref currentCharacter, ref pepeStatus, stringToSay );
                auxLayout.SetMarkup( "<span color=\"blue\">"+stringToSay.Substring(0, currentCharacter)+"</span>" );
            } 
            // Explanation 1
            else if ( currentStep == 1 ) {
                if ( FirstFrameStep() ) {
                    buttonGoBack.Sensitive = true;
                    stringToSay = animationsText[currentStep];
                    totalCharacters = animationsText[currentStep].Length;
                }
                PepeUtils.IncrementCharacterDialog(ref currentCharacter, ref pepeStatus, stringToSay );
                auxLayout.SetMarkup( "<span color=\"blue\">"+stringToSay.Substring(0, currentCharacter)+"</span>" );                
            }
            else if ( currentStep == 2 ) {
                if ( FirstFrameStep() ) {
                    stringToSay = animationsText[currentStep];
                    totalCharacters = animationsText[currentStep].Length;
                    buttonGoForward.Sensitive = false;
                    buttonStartExercise.ShowAll();
                    buttonGoBack.HideAll();
                    buttonGoForward.HideAll();                  
                }
                auxLayout.SetMarkup( "<span color=\"blue\">"+stringToSay.Substring(0, currentCharacter )+"</span>" );
                PepeUtils.IncrementCharacterDialog(ref currentCharacter, ref pepeStatus, stringToSay );
            }
                    
            // Draw text
            imageBackground.Pixmap.DrawLayout( auxGC,200, 40, auxLayout );
            
        }
        
        private void DrawDialogBackground() {
        
            imageBackground.Pixmap.DrawPixbuf( auxGC, backgroundPixbuf, 0, 0,0, 0, WIDTH_SCREEN, HEIGHT_SCREEN, 0, 0, 0);
            imageBackground.Pixmap.DrawPixbuf( auxGC, dialogPixbuf, 0, 0, 10, 0, WIDTH_SCREEN, HEIGHT_SCREEN, 0, 0, 0);
            if ( pepeStatus > 2 ) {
                imageBackground.Pixmap.DrawPixbuf( auxGC, pepe1Pixbuf, 0, 0, 0, HEIGHT_SCREEN - pepe1Pixbuf.Height, pepe1Pixbuf.Width, pepe1Pixbuf.Height, 0, 0, 0);    
            } else {
                imageBackground.Pixmap.DrawPixbuf( auxGC, pepe2Pixbuf, 0, 0, 0, HEIGHT_SCREEN - pepe2Pixbuf.Height, pepe2Pixbuf.Width, pepe2Pixbuf.Height, 0, 0, 0);        
            }
            
        }
                
      /*  public void SayGreat(string text) {     
            validChecked++;
            if ( validChecked < 3 ) {
                stringToSay = "¡Bien! Ya sólo faltan "+( 8 - validChecked)+" postales";
            } else if ( validChecked < 5 ) {
                stringToSay = "¡Genial! Ya sólo faltan "+( 8 - validChecked)+" postales";
            } else if ( validChecked < 7 ) {                
                stringToSay = "¡Ya casi las tienes! Sólo faltan "+( 8 - validChecked)+" postales";
            } else if ( validChecked < 8 ) {
                stringToSay = "¡Sólo falta una!";
            } else {
                NextStep();
            }
            totalCharacters = stringToSay.Length;
            currentCharacter = 0;   
        }
        */
        public void SayBad( string text ) {
            stringToSay = text;
            totalCharacters = stringToSay.Length;
            currentCharacter = 0;
        }
        
        private void IncrementCharacterDialog() {
            if ( currentCharacter < totalCharacters - 2 ) {
                if ( stringToSay[currentCharacter] == '<' && stringToSay[currentCharacter+1] == 's') {
                    while ( stringToSay[currentCharacter] != 'n' || stringToSay[currentCharacter+1] != '>' ) {
                        currentCharacter++;
                    }
                }
            }
            if (currentCharacter < totalCharacters) {
                currentCharacter++;
                pepeStatus = currentCharacter % 5;
            } else {
                pepeStatus = 0; 
            }
        }
        
        bool Update() {
        
            imageBackground.QueueDraw();
            return true;            
        }
        
        private bool FirstFrameStep ()
        {
            if (firstFrameStep == true) {
                firstFrameStep = false;
                return true;
            }
            
            return false;
        }
        
        private bool AfterLastFrameStep ()
        {
            
            if (currentCharacter >= animationsText[currentStep].Length) {
                return true;
            }
            
            return false;
        }
        
        private void NextStep() {
            firstFrameStep = true;
            currentStep++;
            currentCharacter = 0;
        }
        
        private void BackStep() {
            if ( currentStep != 0 ) {
                firstFrameStep = true;
                currentCharacter = 0;
                currentStep--;
            }
        }
        
        private void GoLast() {
            currentStep = animationsText.Length - 1;
            currentCharacter = 0;
            firstFrameStep = true;
        }
        
        protected virtual void OnButtonGoLastClicked (object sender, System.EventArgs e)
        {
            GoLast();
        }
        
        protected virtual void OnButtonGoBackClicked (object sender, System.EventArgs e)
        {
            BackStep();
        }
        
        protected virtual void OnButtonGoForwardClicked (object sender, System.EventArgs e)
        {
            NextStep();
        }
        
        protected virtual void OnButtonStartExerciseClicked (object sender, System.EventArgs e)
        {
          wle.iniciarExerciseLong();
            
        }
    }
}