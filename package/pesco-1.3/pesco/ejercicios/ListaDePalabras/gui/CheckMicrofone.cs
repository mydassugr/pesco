using System;
using System.Media;

namespace pesco
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class CheckMicrofone : Gtk.Bin
    {
      private string DirExer = Configuration.DirExerImage+"wordlist"+System.IO.Path.DirectorySeparatorChar;
       
        WordListExercise wle= null;
        
        public CheckMicrofone (WordListExercise ex )
        {
            wle=ex;
            this.Build ();
            GtkUtil.SetStyle( buttonStartExercise, Configuration.Current.MediumFont );
            imagebtn1.Pixbuf = new Gdk.Pixbuf(DirExer+"btnRec.png");
            imagebtn2.Pixbuf = new Gdk.Pixbuf(DirExer+"btnStop.png");
            imagebtn3.Pixbuf = new Gdk.Pixbuf(DirExer+"btnPlay.png");
            imagepepe.Pixbuf = new Gdk.Pixbuf(DirExer+"pepehabla2.png");
            GtkUtil.SetStyle(labelP5, Configuration.Current.LargeFont);
            GtkUtil.SetStyle(togglebutton1,Configuration.Current.LargeFont);
            GtkUtil.SetStyle(togglebutton2,Configuration.Current.LargeFont);
            
        }
          public void NoActivate() {
            labelP1.Sensitive=false;
            labelP11.Sensitive=false;
            labelP2.Sensitive=false;
            labelP22.Sensitive=false;
            labelP3.Sensitive=false;
            labelP33.Sensitive=false;
            recordButton.Sensitive=false;
            stopButton.Sensitive=false;
            playButton.Sensitive=false;
        }
         public void ActivateP1() {
            labelP1.Sensitive=true;
            labelP11.Sensitive=true;
            labelP2.Sensitive=false;
            labelP22.Sensitive=false;
            labelP3.Sensitive=false;
            labelP33.Sensitive=false;
            recordButton.Sensitive=true;
            stopButton.Sensitive=false;
            playButton.Sensitive=false;
            //hboxP4.Visible=false;
            //hboxP5.Visible=false;
            
        }
        public void ActivateP2() {
            labelP1.Sensitive=false;
            labelP11.Sensitive=false;
            labelP2.Sensitive=true;
            labelP22.Sensitive=true;
            labelP3.Sensitive=false;
            labelP33.Sensitive=false;
            recordButton.Sensitive=false;
            stopButton.Sensitive=true;
            playButton.Sensitive=false;
            hboxP4.Visible=false;
            hboxP5.Visible=false;
           
        }
        public void ActivateP3() {
            labelP1.Sensitive=false;
            labelP11.Sensitive=false;
            labelP2.Sensitive=false;
            labelP22.Sensitive=false;
            labelP3.Sensitive=true;
            labelP33.Sensitive=true;
            recordButton.Sensitive=false;
            stopButton.Sensitive=false;
            playButton.Sensitive=true;
            hboxP4.Visible=false;
            hboxP5.Visible=false;
        }
          public void ActivateP4() {
          hboxP4.Visible=true;  
          hboxP5.Visible=false;
        }
       
        protected virtual void OnRecordButtonClicked (object sender, System.EventArgs e)
        {
            AudioManager.Filename = Configuration.Current.GetConfigurationFolderPath () + "/lista_palabras-Check.wav";
          // AudioManager.StartAudioRecording ();
            AudioManager.RecordAudio(Configuration.Current.GetConfigurationFolderPath () + "/lista_palabras-Check.wav",20);
            ActivateP2(); 
        }
        
        protected virtual void OnStopButtonClicked (object sender, System.EventArgs e)
        {
          AudioManager.StopAudioRecording(); 
          ActivateP3();
        }
        
        protected virtual void OnPlayButtonClicked (object sender, System.EventArgs e)
        {
         string fileSound=Configuration.Current.GetConfigurationFolderPath () + "/lista_palabras-Check.wav";
            Sound sound1 = new Sound();
            sound1.Play (fileSound);
            ActivateP4();
        }
        
        
        protected virtual void OnButtonStartExerciseClicked (object sender, System.EventArgs e)
        {
             wle.iniciarExercise();
        }
        
        protected virtual void OnTogglebutton1Clicked (object sender, System.EventArgs e)
        {
             hboxP4.Visible=false;
            buttonStartExercise.Sensitive=true;
            labelP5.UseMarkup=true;
            labelP5.Markup="<span color='blue'>¡Superada la prueba del micrófono!\n Cuando estés preparado para comenzar el ejercicio,\n pulsa</span><b> ¡Comenzar ejercicio!</b>";   
            labelP5.Show();
            hboxP5.Visible=true;
            NoActivate();
        }
        
        protected virtual void OnTogglebutton2Clicked (object sender, System.EventArgs e)
        {
             hboxP4.Visible=false;
            buttonStartExercise.Sensitive=false;
            labelP5.UseMarkup=true;
            labelP5.Markup="<span color='blue'>Consulte con el tutor y vuelva a realizar la prueba.</span>";
            hboxP5.Visible=true;
            labelP5.Show();
            ActivateP1();
        }
        
        
        
        
        
        
    }
}


