/**

Copyright 2011 Grupo de Investigación GEDES
Lenguajes y sistemas informáticos
Universidad de Granada

Licensed under the EUPL, Version 1.1 or – as soon they 
will be approved by the European Commission - subsequent  
versions of the EUPL (the "Licence"); 
You may not use this work except in compliance with the 
Licence. 
You may obtain a copy of the Licence at: 

http://ec.europa.eu/idabc/eupl  

Unless required by applicable law or agreed to in 
writing, software distributed under the Licence is 
distributed on an "AS IS" basis, 
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either 
express or implied. 
See the Licence for the specific language governing 
permissions and limitations under the Licence. 



*/

using System;
using Gtk;

namespace pesco
{

	/// <summary>
	/// 
	/// </summary>
	[System.ComponentModel.ToolboxItem(true)]
	public partial class ContenedorEjercicio : Gtk.Bin
	{			
		
		/// <summary>
		/// 
		/// </summary>
		public ContenedorEjercicio ()
		{
			this.Build ();
		}
		
		
		public void agregarPanelEjercicio(Widget p){
			GtkUtil.Put(this.vbox2, p, 0);
			                               
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
		protected virtual void OnFinalizarClicked (object sender, System.EventArgs e)
		{
		}
		
		/// <summary>
		/// /
		/// </summary>
		/// <param name="sender">
		/// A <see cref="System.Object"/>
		/// </param>
		/// <param name="e">
		/// A <see cref="System.EventArgs"/>
		/// </param>
		protected virtual void OnHelpClicked (object sender, System.EventArgs e)
		{
		}
		
		protected virtual void OnDestroyEvent (object o, Gtk.DestroyEventArgs args)
		{
		}
		
		
	}
}

