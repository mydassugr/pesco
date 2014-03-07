using System;
namespace pesco
{
	public partial class VentanaCreditos : Gtk.Window
	{
		protected virtual void projectReferenceHandler (object o, Gtk.ButtonPressEventArgs args)
		{
			
				SessionScreen st = new SessionScreen();
				st.Show();
			
		}
		
		
		public VentanaCreditos () : base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
			
			this.eventbox1.ButtonReleaseEvent += delegate {
				
				UserSessions userSessions = UserSessions.Deserialize ();				
				SessionManager sessionManager = SessionManager.Deserialize ();
				sessionManager.StartSessionManager ();
				
				SessionScreen st = new SessionScreen();
				st.Show();
				this.Destroy();
			};
		}
	}
}

