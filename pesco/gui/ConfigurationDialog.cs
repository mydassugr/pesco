using System;
using System.Collections.Generic;
using System.Reflection;


namespace pesco
{
	public partial class ConfigurationDialog : Gtk.Dialog
	{
		
		List <Gtk.Label> propertiesLabel = new List<Gtk.Label>();
		List <Gtk.Entry> valuesEntry = new List<Gtk.Entry>();
		
		protected virtual void OnButtonCancelButtonPressEvent (object o, Gtk.ButtonPressEventArgs args)
		{

		}
		
		
		public ConfigurationDialog ()
		{
			this.Build ();
			this.buildTable();
		}
		
		private void buildTable() {
		
						
			// Get the type.
			Type t = Configuration.Current.GetType();
			
			Gtk.Label propertyLabel;
			Gtk.Entry valueEntry;
			uint i = 0;
			// Cycle through the properties.
			foreach ( System.Reflection.PropertyInfo p in t.GetProperties(BindingFlags.Public | BindingFlags.Instance | 
			                                                              BindingFlags.GetProperty | BindingFlags.DeclaredOnly | BindingFlags.SetProperty ) ) {
			
				propertyLabel = new Gtk.Label( p.Name );
				propertyLabel.SetAlignment( (float) 0.0, (float) 0.5);
				valueEntry = new Gtk.Entry( p.GetValue(Configuration.Current, null).ToString() );
				// Avoid fonts or Current property
				if ( valueEntry.Text != "pesco.FontStyle" && propertyLabel.Text != "Current" ) {
					propertiesLabel.Add( propertyLabel );
					valuesEntry.Add( valueEntry );
					if ( propertyLabel.Text != "ServerURL" && propertyLabel.Text != "ShowExerciseButtons") {
						valueEntry.Sensitive = false;	
					}
					configTable.Attach( propertiesLabel[ (int) i], 0, 1, i, i+1 );
					configTable.Attach( valuesEntry[ (int) i], 1, 2, i, i+1 );
					i++;
				}
			}
			this.ShowAll();
			
		}
		
		private void saveConfig() {
			Type t = Configuration.Current.GetType();
			for ( int i = 0; i < propertiesLabel.Count; i++ ) {
        		var empNumberProperty = Configuration.Current.GetType().GetProperty(propertiesLabel[i].Text);
				empNumberProperty.SetValue( Configuration.Current, valuesEntry[i].Text, null );
			}
			// Configuration.CreateConfigurationFolder();
			// Configuration.CreateExerciseConfigurationFolder();
			// Configuration.CreateQuestionaryConfigurationFolder();
			Configuration.Current.Serialize();
 			Gtk.MessageDialog md = new Gtk.MessageDialog (null, Gtk.DialogFlags.Modal, Gtk.MessageType.Info, Gtk.ButtonsType.Ok, "Configuración guardada");
            md.Run ();
            md.Destroy();

		}
		
		protected virtual void OnButtonCancelClicked (object sender, System.EventArgs e)
		{
			this.Destroy();
		}
		
		protected virtual void OnButtonOkClicked (object sender, System.EventArgs e)
		{
			this.saveConfig();
		}
		
		protected virtual void OnSendDataButtonClicked (object sender, System.EventArgs e)
		{
			SessionManager.CompressAndUploadProfile();
		}
		
		
	
		
		
		
	}
}

