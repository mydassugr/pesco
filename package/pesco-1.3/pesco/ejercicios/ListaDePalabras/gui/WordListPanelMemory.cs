using System;
using Gtk;
using System.Collections.Generic;
using Gdk;

namespace pesco
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class WordListPanelMemory : Gtk.Bin
    {
        Timer t;
        protected WordListExercise padre;       
        
        int row = 0;
        int column = 0;

        int timeLimit;

        public WordListExercise Padre {
            get {return this.padre;}
            set {padre = value;}
        }
               
        public WordListPanelMemory (List<string> listaPalabras, int exposure) : this(listaPalabras, exposure, false)
        {
        }

        public WordListPanelMemory (List<string> listaPalabras, int exposure, bool rojo)
        {
            this.Build ();
            
            Label b;
            
            int x = 90;
            int y = 0;
            int delta = 36;
            
            for (int i = 0; i < listaPalabras.Count; ++i) {
                
                b = new Label ();
                b.ShowAll ();
				b.UseMarkup=true;
				b.Markup = "-<b>" + listaPalabras[i]+"</b>";
                GtkUtil.SetStyle (b, Configuration.Current.HandwrittingStyle);
                
                if (rojo)
                    b.ModifyFg (StateType.Normal, new Gdk.Color (255, 0, 0));
                
                y = y + delta;
                
                fixed5.Put (b, x, y);
                
            }
            timeLimit = exposure;
            t = new Timer(FinalizeTime, exposure);
            this.vbox6.Add(t);
            IniciaTemporizador();
            this.ShowAll();
        }
        
         public void IniciaTemporizador(){
                    t.StartClock();
                } 
         public void FinalizeTime(){
			PararTemporizador();
            Padre.CreatePanelSpeak();
         }
        
     /*
         public void StartTimer (TimerAction action)
                {
            Console.WriteLine("START TIMER!!!");
            
            if (t != null) {
                t.StopTimer();

            }
            
            t = new Timer (action, timeLimit);
            t.StartClock ();
        }
         */
        public void PararTemporizador ()
        {
            t.Stop = true;
            t.StopTimer();
        }

    }
}

