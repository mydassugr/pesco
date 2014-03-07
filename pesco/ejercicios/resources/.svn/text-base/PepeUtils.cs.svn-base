using System;
using Gtk;
using System;
using System.IO;

namespace pesco
{
	public class PepeUtils
	{
		static Gdk.GC staticGC;
		static Gdk.Pixbuf staticPixbuf;
		static Gdk.Pixmap staticPixmap;
		static Pango.Layout staticLayout;
		static private Pango.Matrix staticPangoMatrix;
		static int offsetX;
		static int width, height;
		
		public PepeUtils ()
		{
		}
		
		static public void GenerateCartel( Pango.Context auxContext, Gdk.GC auxGC, Gdk.Pixmap auxPixmap, string text ) {
			staticPixmap = auxPixmap;
			staticGC = auxGC;
			
			staticPixbuf = new Gdk.Pixbuf( Configuration.CommandExercisesDirectory+"/resources/img/pepecartel.png" );
			
			staticPangoMatrix = Pango.Matrix.Identity;
			staticPangoMatrix.Rotate(3);
			
			staticGC.RgbFgColor = new Gdk.Color( 0, 0, 0);
			// Init pango layout
			staticLayout = new Pango.Layout( auxContext );
			staticLayout.Width = Pango.Units.FromPixels(560);
			staticLayout.Wrap = Pango.WrapMode.Word;
			staticLayout.Alignment = Pango.Alignment.Center;
			staticLayout.FontDescription = Pango.FontDescription.FromString("Ahafoni CLM Bold 32");
			staticLayout.Context.Matrix = staticPangoMatrix;
			staticLayout.SetText(text);
			staticLayout.ContextChanged();
			
			staticLayout.GetPixelSize( out width, out height );
			offsetX = (550-width)/2;
		}
		
		static public void DrawCartel() {
		
			staticPixmap.DrawPixbuf( staticGC, staticPixbuf, 0, 0, 0, 0, staticPixbuf.Width, staticPixbuf.Height, 0, 0, 0);
			staticPixmap.DrawLayout( staticGC, 100+offsetX, 60, staticLayout );

		}
		
		static public void IncrementCharacterDialog(ref int currentCharacter, ref int pepeStatus, string stringToSay ) {
			
			int totalCharacters = stringToSay.Length;
			
			// TTS of Pepe dialogs
			if ( currentCharacter == 0 ) {
				// GenerateSynthesizedSound( stringToSay, true, 1 );
			}
			
			if ( currentCharacter < totalCharacters - 1 ) {
				if ( stringToSay[currentCharacter] == '<' && stringToSay[currentCharacter+1] == 's') {
					while ( stringToSay[currentCharacter] != 'n' || stringToSay[currentCharacter+1] != '>' ) {
						currentCharacter++;
					}
				}
			}
			if (currentCharacter < totalCharacters) {
				while ( currentCharacter < totalCharacters ) {
					if ( stringToSay[currentCharacter] == ' ' )
						break;
					currentCharacter++;
				}
				if ( currentCharacter < totalCharacters ) {
					currentCharacter++;
				}
				pepeStatus = currentCharacter % 5;
			} else {
				pepeStatus = 0;	
			}
		}
	
		
		public static void GenerateSynthesizedSound (string text, bool gender, double volume){
		
			// text = "<?xml version='1.0'?><!DOCTYPE SABLE PUBLIC '-//SABLE//DTD SABLE speech mark up//EN' 'Sable.v0_2.dtd'[]><RATE SPEED='-40%'>" + text + "</RATE>";
			text = StripTagsCharArray(text);
	        double volumepositive= volume/50;
			string tempFilePath = Configuration.CommandExercisesDirectory + Path.DirectorySeparatorChar + "text2wave.temp";
		    // string TextVolume = volumepositive.ToString().Replace(",",".");
	         
			//create temp file necessary for calling text2wave
			FileInfo tempFile = new FileInfo(@tempFilePath);
			StreamWriter w = tempFile.CreateText();
			
			// write text and close file
			w.WriteLine(text);	
			w.Close();
			
			// generate sound file depending on the gender
			// if (gender == true)
			ExecuteCommandSync("festival",  "--tts "+ tempFilePath); 		
			// else
			//	ExecuteCommandSync("festival",  "-otype wav -o " + path + " " + tempFilePath + " -scale " +TextVolume  + " -eval \"(voice_JuntaDeAndalucia_es_sf_diphone)\""); 			
			
			// delete temp file
			// tempFile.Delete();
		
			
		}
		
		/// <summary>
		/// Executes a shell command synchronously.
		/// </summary>
		/// <param name="command">string command</param>
		/// <returns>string, as output of the command.</returns>
		private static void ExecuteCommandSync(object app, object command) {
			
			try {
				
				// create the ProcessStartInfo
				System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo((string)app, (string)command);
				
				// The following commands are needed to redirect the standard output. 
				// This means that it will be redirected to the Process.StandardOutput StreamReader
				procStartInfo.RedirectStandardOutput =true;
				procStartInfo.UseShellExecute =false;
				
				// Do not create the black window.
				procStartInfo.CreateNoWindow =true;
				
				// Now we create a process, assign its ProcessStartInfo and start it
				System.Diagnostics.Process proc =new System.Diagnostics.Process();
				proc.StartInfo = procStartInfo;
				proc.Start();
				
				// Get the output into a string
				// string result = proc.StandardOutput.ReadToEnd();
				
				// Display the command output.
				// Console.WriteLine(result);
				
			} catch(Exception objException) {
			
				// Propagate exception
				throw objException;
				
			}
		}
		
		public static string StripTagsCharArray(string source)
	    {
			char[] array = new char[source.Length];
			int arrayIndex = 0;
			bool inside = false;
		
			for (int i = 0; i < source.Length; i++)
			{
			    char let = source[i];
			    if (let == '<')
			    {
				inside = true;
				continue;
			    }
			    if (let == '>')
			    {
				inside = false;
				continue;
			    }
			    if (!inside)
			    {
				array[arrayIndex] = let;
				arrayIndex++;
			    }
			}
			return new string(array, 0, arrayIndex);
	    }
	}
}

