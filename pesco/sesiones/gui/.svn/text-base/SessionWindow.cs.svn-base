using Gtk;
using Gdk;
using System;
using System.Collections.Generic;

namespace pesco
{


	
	public partial class SessionWindow : Gtk.Window
	{
			
		private Gtk.Bin currentShownPanel;
		
		
		public Gtk.Button StartSessionButton {
			get {
				return startSessionButton;
			}
			set {
				startSessionButton = value;
			}
		}	

		public SessionWindow () : base(Gtk.WindowType.Toplevel)
		{
			this.Build ();		
			
			this.GdkWindow.Cursor=GtkUtil.ChangeCursor();			
			
			this.Maximize();
			SetIconFromFile( Configuration.CommandDirectory+"/gui/icon.png" );
			//GtkUtil.SetStyle( sessionLabel, Configuration.Current.MediumFont );
			GtkUtil.SetStyle( startSessionButton, Configuration.Current.ButtonFont );			
			//sessionLabel.Wrap = true;
			dialogPanel.InitPanel();
		}
		
		public void SetLabelText( string text ) {
			
			dialogPanel.SetText( text );

		}
		
		public void HideStartSessionButton() {
			startSessionButton.Hide();
		}
		public void ShowStartSessionButton() {
			startSessionButton.Show();
		}
		
		protected virtual void startExerciseClicked (object sender, System.EventArgs e)
		{
			// Hide();
			SessionManager.GetInstance().LaunchExercise();
		}
		
		protected virtual void OnDeleteEvent (object o, Gtk.DeleteEventArgs args)
		{
			if ( SessionManager.GetInstance().e != null ) {
				try {
					SessionManager.GetInstance().e.pausa();
				} catch ( Exception e ) {
					Console.WriteLine( e.ToString() );	
				}			
			}
			
			MessageDialog md = new MessageDialog ( this, 
										DialogFlags.DestroyWithParent,
										MessageType.Question, 
										ButtonsType.YesNo, "<span size=\"xx-large\">¿Desea realmente abandonar la aplicación?</span>");
			
      		GtkUtil.SetStyleRecursive( md, Configuration.Current.MediumFont );
			
			this.Sensitive = false;
			md.Show();
			md.GdkWindow.Cursor = GtkUtil.ChangeCursor();
			ResponseType result = (ResponseType) md.Run ();	
			
			if (result == ResponseType.Yes) {
				if ( SessionManager.GetInstance().e != null ) {				
					try { 
						SessionManager.GetInstance().e.finalizar();
					} catch ( Exception e ) {
						Console.WriteLine( e.ToString() );	
					}			
				}
				SessionManager.GetInstance().FinishApplication();
				args.RetVal = false; // <-- Destroy window
			} else {
				this.Sensitive = true;			
				if ( SessionManager.GetInstance().e != null ) {				
					try { 
						SessionManager.GetInstance().e.pausa();
					} catch ( Exception e ) {
						Console.WriteLine( e.ToString() );	
					}			
				}
				md.Destroy();
				args.RetVal = true; // <-- Don't destroy window
			}
			
			// SessionManager.GetInstance().FinishTimer();
			
			// Application.Quit ();
			// args.RetVal = false;
		}
		
		public void HideIntroSessionsShowExercise ( Gtk.Bin newPanel ) {
			
			// Hiding intro sessions
			vboxIntroSessions.Hide();
			
			// Removing existing panel ( if it exists )
			if ( currentShownPanel != null ) {
				vboxContainer.Remove( currentShownPanel );
				// currentShownPanel.Destroy();
			}
			// Adding new panel and saving reference
			vboxContainer.PackStart(newPanel,true,true,0);
			newPanel.Show();
			currentShownPanel = newPanel;
			
		}
		protected virtual void OnKeyPressEvent (object o, Gtk.KeyPressEventArgs args)
		{
			// Console.WriteLine( "Keyboard event: "+args.Event.Key.ToString() );
			if ( args.Event.Key == Gdk.Key.F10 ) {
				SessionManager.GetInstance().ExerciseFinished(-1);
				SessionManager.GetInstance().e.finalizar();
				SessionManager.GetInstance().TakeControl();
			}
		}
		
		
	}
}
