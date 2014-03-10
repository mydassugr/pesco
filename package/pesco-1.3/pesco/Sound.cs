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
using System.Diagnostics;

namespace pesco
{

	// Sound to be played using music123 program
	public class Sound
	{
		// Proccess to manage sound
		private Process proc = new System.Diagnostics.Process();		
		
		public void Play(string path){
			// configure proccess to play sound
			//proc.EnableRaisingEvents=false; 			
			proc.StartInfo.FileName = "music123";
			proc.StartInfo.Arguments = path;	
			proc.Start();
			
			// Wait until the sound is played to avoid two sounds at the same time
			proc.WaitForExit();
			proc = null;			
		}
		
	}
}

