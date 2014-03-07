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
using System.Collections.Generic;

using System.Collections;

namespace pesco
{

	public class BO_Place
	{

		private string id;
		private string nombre;
		private string img;
		
		public Gdk.Pixbuf pixbufLugar;
	
		public List <BO_ItemActionWho> objetosAccionAgregar = new List<BO_ItemActionWho>();
		public List <BO_ItemActionWho> objetosAccionEliminar = new List<BO_ItemActionWho>();
	
		public string Nombre {
			get {
				return nombre;
			}
			set {
				nombre = value;
			}
		}
		
		
		public string Img {
			get {
				return img;
			}
			set {
				img = value;
			}
		}
		
		
		public string Id {
			get {
				return id;
			}
			set {
				id = value;
			}
		}
		
		
		
		public BO_Place ()
		{			
		}
		
		public BO_Place (string id, string nombre, string img) {
		
			this.id = nombre;
			this.Nombre = nombre;
			this.Img = img;
			
			this.pixbufLugar = Gdk.Pixbuf.LoadFromResource(img);
		}
		
		public void agregarAccion(BO_ItemActionWho auxObjetoAccionQuien) {
		
			if ( auxObjetoAccionQuien.Accion.Tipo == "add" )
				this.objetosAccionAgregar.Add( auxObjetoAccionQuien );
			else if ( auxObjetoAccionQuien.Accion.Tipo == "remove" )
				this.objetosAccionEliminar.Add( auxObjetoAccionQuien );
			
		}
		
		public BO_ItemActionWho getAccionAgregarAleatoria() {
		
			if ( objetosAccionAgregar.Count > 0 ) {
				Random r = new Random();			                      
				int auxRandom = r.Next( 0, objetosAccionAgregar.Count);
				// Console.WriteLine( "Hay "+ objetosAccionAgregar.Count+" para agregar. Escogemos "+auxRandom);
				return objetosAccionAgregar[auxRandom];
			} else {
				return null;
			}
			
		}
		
		public BO_ItemActionWho getAccionEliminar( Dictionary <BO_Item,int> objetos ) {
		
			foreach( KeyValuePair<BO_Item, int> objeto in objetos ) {
			
				for ( int i = 0; i < this.objetosAccionEliminar.Count; i++ ) {
					if ( objeto.Key.Id == this.objetosAccionEliminar[i].Objeto.Id 
					    && objeto.Value > 0 )
						return this.objetosAccionEliminar[i];
				}
			
			}
			return null;
			
		}
		
	}
}

