using System;
using System.Security.Cryptography;
using Gtk;
using Gdk;
using System.Net;
using System.Collections.Generic;

namespace pesco
{
	/// <summary>
	/// Ventana de inicio de la aplicación
	/// </summary>
	public partial class MainWindow : Gtk.Window
	{

		/// <summary>
		/// Cadena conteniendo el tipo y 
		/// </summary>
		protected string font = "Tahoma 20";

		public static User user;
				
		public VBox VBoxMain {
			get {
				return vboxMain;	
			}			
		}
		
		/// <summary>
		/// 
		/// </summary>
		public MainWindow () : base(Gtk.WindowType.Toplevel)
		{
			Build ();	
			
			this.GdkWindow.Cursor= GtkUtil.ChangeCursor();		
			
			char auxSeparator = System.IO.Path.DirectorySeparatorChar;
			SetIconFromFile( Configuration.CommandDirectory+
			                auxSeparator+"gui"+auxSeparator+"icon.png" );
			
			this.Title = "PESCO";
			
			GtkUtil.SetStyle (button3, Configuration.Current.ButtonFont);
			GtkUtil.SetStyle (button2, Configuration.Current.ButtonFont);
			GtkUtil.SetStyle (this.completeTestButton, Configuration.Current.ButtonFont);
			GtkUtil.SetStyle (buttonShowMedals, Configuration.Current.MediumFont);
			GtkUtil.SetStyle (buttonAbout, Configuration.Current.MediumFont);
			
			label3.Markup = "<span foreground='#ff0000' size='large'>¡Error, nombre de usuario incorrecto!</span>";
			label3.Hide ();
			
			// buttonExercises.HideAll();
			
			button2.Clicked += this.OnClickEnter;
			completeTestButton.Clicked += OnClickCompleteTest;
			
			// Medals button
			if ( User.Deserialize() == null ) {
				buttonShowMedals.Sensitive = false;	
			}
			
			// Show exercises button
			if ( Configuration.Current.ShowExerciseButtons != "yes" ) {
				buttonExercises.Visible = false;	
			}
			
			//if (User.IsRegistered ()) {
				//TODO comprobar si los test se han terminado
				
				/*Questionary q1 = Questionary.GetDailyLifeQuestionary (), q2 = Questionary.GetInstrumentalActivitiesQuestionary ();
				
				if (!q1.IsFinished () || !q2.IsFinished ()) {
					hbox1.Hide ();
					button2.Hide ();
					button3.Hide ();
				} else {*/
					button3.Hide ();
					completeTestButton.Hide ();
					//user = User.Deserialize ();
				//}
		/*	} else {
				hbox1.Hide ();
				button3.GrabFocus ();
				completeTestButton.Hide ();
			}*/
			
			this.welcomePanel.InitPanel();
			
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender">
		/// A <see cref="System.Object"/>
		/// </param>
		/// <param name="a">
		/// A <see cref="DeleteEventArgs"/>
		/// </param>
		protected void OnDeleteEvent (object sender, Gtk.DeleteEventArgs a)
		{
			Application.Quit ();
			a.RetVal = true;
		}

		protected virtual void OnClickEnter (object sender, System.EventArgs e)
		{
						
				// Initialize user and sessions manager
				UserSessions userSessions = UserSessions.Deserialize ();

				userSessions.ShowInfo ();			

			
				SessionManager sessionManager = SessionManager.Deserialize();
				sessionManager.StartSessionManager ();
												
				this.Destroy ();
				this.Dispose ();
				
				// Exercise selector for debug. Comment for release version.

				// SessionScreen st = new SessionScreen();
				// st.Show();
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
		protected virtual void OnClickRegister (object sender, System.EventArgs e)
		{
			//this.Hide ();
			//RegisterUser();
		}
		
		private void RegisterUser()
		{
			//string message;
			//bool res = RegisterManager.RegisterUser ();
			// lanza la ventana modal para el registro del usuario
			/*if (res)
				message = "<span size='xx-large'>Muy bien. Has terminado la primera sesión. " +
					"En la siguiente sesión, te voy a proponer una serie de ejercicios que van a medir " +
					" tu nivel de memoria, atención, razonamiento y planificación.\n\n<b>¡Hasta pronto!</b></span>";
			else
				message = "<span size='large'>¡El registro <span color='black'>no ha terminado</span>!\n\nEl programa se " +
					"cerrará ahora. En la próxima sesión el resgistro seguirá por donde lo dejaste." +
					 "\n\n<b>¡Hasta pronto!</b></span>";
			*/
			/*PESCOGoodByeDialog pgbd = new PESCOGoodByeDialog(message);
			if (!res)
				pgbd.ShowBackButton();
		
				
			pgbd.Maximize();		
			
			Gtk.ResponseType result = (Gtk.ResponseType)pgbd.Run();
			pgbd.Destroy ();
			
			if (result.Equals(Gtk.ResponseType.Cancel) && !res)
				if (User.IsRegistered())
					this.OnClickCompleteTest(null, null);
				else
					this.RegisterUser();
			else 
				Application.Quit ();	*/
		}

		protected virtual void OnClickCompleteTest (object sender, System.EventArgs es)
		{
			this.Hide ();
			
			string message;
			
			bool res = RegisterManager.FinishTest ();
			
			FunctionalScaleDialog def = FunctionalScaleDialog.GetDailyLifeQuestionaryDialog ();
			FunctionalScaleDialog def2 = FunctionalScaleDialog.GetInstrumentalActivitiesQuestionaryDialog ();									
			
			res = !def.Quest.IsFinished() || !def2.Quest.IsFinished();
			
			/*
			// lanza la ventana modal para el registro del usuario
			if (!res)
				message = "<span size='xx-large'>Muy bien. Ya hemos terminado la primera sesión. " +
					"En la siguiente sesión, te voy a proponer una serie de ejercicios que van a medir " +
					" tu nivel de memoria, atención, razonamiento y planificación.\n\n<b>¡Hasta pronto!</b></span>";
			else
				message = "<span size='xx-large' color='black'>¡No has terminado de completar el cuestionario!</span>\n\n<span size='xx-large'>El programa se cerrará ahora. En la próxima sesión deberás continuar donde lo has dejado.</span>";
			PESCOGoodByeDialog pgbd = new PESCOGoodByeDialog(message);
			if (res)
				pgbd.ShowBackButton();
			*/

		}

		protected virtual void OnClickAbout(object sender, System.EventArgs es)
		{
			
			AboutDialog auxAboutDialog = new AboutDialog();
			string [] authors = {"I. Entidades Participantes:",
								"• Consorcio para el Desarrollo de la Sociedad de la Información y el Conocimiento \"Fernando de los Ríos\"",
								"• Fundación General Universidad de Granada-Empresa",
								"",
								"II. Investigadoras  Principales:",
								"Dña. María José Rodríguez Fortiz y Dña. María Visitación Hurtado Torres.",
								"",
								"III. Investigadores Colaboradores:",
								"Grupo de Investigación de Neuropsicología y Psiconeuroinmunología Clínicas",
								"• D. Miguel García Pérez",
								"• D. Alfonso Caracuel Romero",
								"• Dña. Sandra Santiago Ramajo",
								"",
								"Grupo de Informática",
								"• Dña. María Luisa Rodríguez Almendros",
								"• D. José Luis Garrido Bullejos",
								"• D. Miguel J. Hornos Barranco",
								"• D. Manuel Noguera García",
								"• Dña. Kawtar Benghazi Akhlaki",
								"• D. Álvaro Fernández López",
								"• D. Carlos Rodríguez Domínguez",
								"• Dña. Luz María Roldán Vílchez",
								"",
								"IV. Programadores",
								"• D. Álvaro López Martínez",
								"• Dña. Ana Belén Pelegrina Ortiz",
								"• Dña. Luz María Roldán Vílchez",
								"• Dña. Inmaculada Rubio Gil",
								"• Dña. Elsa Trigueros Sánchez",
								"",
								"Colaboración en el diseño del personaje guía:\nDña. Ainhoa Serena Enamorado"};

			auxAboutDialog.Authors = authors;
			auxAboutDialog.Copyright = "Copyright (C) 2010 Guadalinfo.es\n"+
										"Copyright (C) 2010 Consorcio Fernando de los Ríos\n"+
										"dentro del proyecto Guadalinfo Accesible (Expediente 7/2010)\n"+
										"Referencia Proyecto: C-3437-00";
			auxAboutDialog.License = "Licensed under the EUPL, Version 1.1 or – as soon they\n"+
									"will be approved by the European Commission - subsequent\n"+
									"versions of the EUPL (the \"Licence\"); You may not use\n"+
									"this work except in compliance with the Licence.\n"+
									"You may obtain a copy of the Licence at:\n "+
									"http://ec.europa.eu/idabc/eupl\n"+	
									"Unless required by applicable law or agreed to in\n"+
									"writing, software distributed under the Licence is\n"+
									"distributed on an \"AS IS\" basis,\n"+
									"WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either\n"+
									"express or implied.\n"+
									"See the Licence for the specific language governing\n"+
									"permissions and limitations under the Licence.\n";
			auxAboutDialog.ProgramName = "PESCO";
			auxAboutDialog.Comments = "Plataforma Software para la\n"+"Estimulación Cognitiva";
			auxAboutDialog.Version = "1.0";
			auxAboutDialog.Website = "Página de PESCO";
			auxAboutDialog.WebsiteLabel = "asistic.ugr.es/pesco";
			string [] auxArtists =  {"Los recursos gráficos usados en esta aplicación han sido obtenidos de",
									"",
									"ARASAAC",
									"Portal Aragonés de la Comunicación Aumentativa y Alternativa",
									"<http://www.catedu.es/arasaac>",
									"",
									"AUMENTATIVA 2.0",
									"Comunicación Aumentativa",
									"<http://www.aumentativa.net>"};
			auxAboutDialog.Artists = auxArtists;

			auxAboutDialog.Logo = Gdk.Pixbuf.LoadFromResource( "pesco.gui.abuelo3.png" ).ScaleSimple( 100, 103, Gdk.InterpType.Bilinear );
			auxAboutDialog.Run();
			auxAboutDialog.Destroy();
			
		}
		
		protected virtual void exercisesClicked (object sender, System.EventArgs e)
		{
			
				launchExercisesSelector();
		}
		
		private void launchExercisesSelector() {
				UserSessions userSessions = UserSessions.Deserialize ();				
				SessionManager sessionManager = SessionManager.Deserialize ();
				sessionManager.StartSessionManager ();
				
				SessionScreen st = new SessionScreen();
				st.Show();
				this.Destroy();
		}
		
		protected virtual void showMedalsClicked (object sender, System.EventArgs e)
		{
			
			SessionsPodiumPanel auxPanel = new SessionsPodiumPanel( this );

			fixedMain.HideAll();
			vboxMain.Add( auxPanel );
			auxPanel.ShowAll();
			auxPanel.InitPanel();
			auxPanel.ButtonClose.Clicked += delegate {
				vboxMain.Remove( auxPanel );
				fixedMain.ShowAll();
			};
						
		}
		
		protected virtual void OnKeyPressEvent (object o, Gtk.KeyPressEventArgs args)
		{
			if ( args.Event.Key == Gdk.Key.F12 ) {
			
				ConfigurationDialog configurationDialog = new ConfigurationDialog();
				configurationDialog.Run();
				
			} else if ( args.Event.Key == Gdk.Key.F11 ) {
				launchExercisesSelector();
			}
		}
		
		
		
		
	}
}
