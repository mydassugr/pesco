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
	public class AudioManager
	{
		protected static string filename;
		protected static Process proc;
		
		public static string Filename {
			get {
				return filename;
			}
			set {
				filename = value;
			}
		}

		private AudioManager ()
		{
			
		}
		
		public static bool RecordAudio(string filename, int seconds)
		{
			proc = new System.Diagnostics.Process();
			proc.EnableRaisingEvents=false; 
			proc.StartInfo.FileName = "arecord";
			proc.StartInfo.Arguments = "-d " + seconds + " -f cd -t wav " + filename;
			proc.Start();
			return true;
		}
		
		public static bool StartAudioRecording()
		{
			
			// Console.WriteLine("Ejecutando START audio recording");
			
			if (filename == null || filename == "")	
				return false;
			//else if (proc == null) {
				proc = new System.Diagnostics.Process();
				proc.EnableRaisingEvents=false; 
				proc.StartInfo.FileName = "arecord";
				proc.StartInfo.Arguments = "-f cd -t wav " + filename;
				proc.Start();
				return true;
			/*}
			else
				return false;*/
		}
		
		public static bool StopAudioRecording()
		{
			
			if (proc != null)
			{
				try{
					proc.Kill();
				}
				catch( System.InvalidOperationException e)
				{
					;	
				}
				return true;
			}
			else 
				return false;
		}
	}
}


