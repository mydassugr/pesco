
using System;
using Gtk;

namespace pesco
{

	public partial class MouseTestDialog : Gtk.Bin
	{

		
		private int clicksCounter = 0;
		private int timeCounter=1;
		private bool keyPressed=false;
		//private int[] positionsX = new int[] { 50, 570, 50, 480, 300 };
		//private int[] positionsY = new int[] { 50, 50, 350, 350, 300 };
		
		private int[] positionsX = new int[] { 50, 700, 50, 700, 325 };
		private int[] positionsY = new int[] { 50, 50, 400, 400, 155 };

		private ResponseType response;
		
		public MouseTestDialog ()
		{
			this.Build ();
			//SetIconFromFile( Confuration.CommandDirectory+"/gui/icon.png" );
			
			button386.Hide();
			GtkUtil.SetStyle (labelInfo, Configuration.Current.MediumFont);
			labelInfo.Wrap = true;
			//labelInfo.SizeAllocated += delegate(object o, Gtk.SizeAllocatedArgs args) { ((Gtk.Label)o).SetSizeRequest (Allocation.Size.Width - 20, -1); };
			GtkUtil.SetStyle (buttonClickMe, Configuration.Current.ExtraLargeFont);
			fixedPane.Move (buttonClickMe, positionsX[0], positionsY[0]);
			buttonClickMe.Label = "¡Púlsame!";
			GtkUtil.SetStyle (buttonClickMe, Configuration.Current.ExtraLargeFont);
		}
		
		public new ResponseType Response {
			get {
				return response;
			}
		}
		protected virtual void buttonPulsameCallback (object sender, System.EventArgs e)
		{
			if(!keyPressed){ 
				
				clicksCounter++;
				
				if (clicksCounter == 5) {
					TimeSpan exTime= SessionManager.GetInstance().ExerciceTime();
					if( exTime.Minutes <1){
						this.response = ResponseType.Ok;
						//this.Hide();
						SessionManager.GetInstance().ExerciseFinished(-1);
						SessionManager.GetInstance().TakeControl();
					}
					else{
						if(timeCounter ==1){
							clicksCounter =0;
							timeCounter=2;
							SessionManager.GetInstance().RestartTimes();
							fixedPane.Move (buttonClickMe, positionsX[clicksCounter], positionsY[clicksCounter]);
							GtkUtil.SetStyle (buttonClickMe, Configuration.Current.ExtraLargeFont);
							
						}
						else{
							MyMessageDialod mmd= new MyMessageDialod("Pida ayuda al dinamizador","Aceptar",1000);
							mmd.Accept.Clicked += delegate{
								mmd.Destroy();
								SessionManager.GetInstance().FinishApplication();
							};
							
						}
						
					}
					//this.Destroy();
				} else {
					fixedPane.Move (buttonClickMe, positionsX[clicksCounter], positionsY[clicksCounter]);
					GtkUtil.SetStyle (buttonClickMe, Configuration.Current.ExtraLargeFont);
				}
			}
			keyPressed=false;
		}

		protected virtual void OnCloseEvent (object sender, System.EventArgs e)
		{
			this.response = ResponseType.Close;
		}
		
		protected virtual void OnButtonClickMeKeyPressEvent (object o, Gtk.KeyPressEventArgs args)
		{
			keyPressed=true;
			
		}
		
	}
}
