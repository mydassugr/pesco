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
using System.Text;
using System.Security.Cryptography;

namespace pesco
{


	public class Util
	{

		/// <summary>
		/// Generates a hash for the given plain text value and returns a
		/// base64-encoded result. Before the hash is computed, a random salt
		/// is generated and appended to the plain text. This salt is stored at
		/// the end of the hash value, so it can be used later for hash
		/// verification.
		/// </summary>
		/// <param name="plainText">
		/// Plaintext value to be hashed. The function does not check whether
		/// this parameter is null.
		/// </param>
		/// <param name="hashAlgorithm">
		/// Name of the hash algorithm. Allowed values are: "MD5", "SHA1",
		/// "SHA256", "SHA384", and "SHA512" (if any other value is specified
		/// MD5 hashing algorithm will be used). This value is case-insensitive.
		/// </param>
		/// <param name="saltBytes">
		/// Salt bytes. This parameter can be null, in which case a random salt
		/// value will be generated.
		/// </param>
		/// <returns>
		/// Hash value formatted as a base64-encoded string.
		/// </returns>
		public static string ComputeHash (string plainText)
		{
			// Convert plain text into a byte array.
			byte[] plainTextBytes = Encoding.UTF8.GetBytes (plainText.ToUpper());
			HashAlgorithm hash = new MD5CryptoServiceProvider ();
			// Convert result into a base64-encoded string.
			string hashValue = Convert.ToBase64String (hash.ComputeHash (plainTextBytes));
			
			string s;
			
			return hashValue;
		}

		public static string GetHomePath ()
		{
			return Environment.GetFolderPath (Environment.SpecialFolder.Personal);
		}

		public static void ExecuteCommandAsync (string command)
		{
			System.Diagnostics.Process proc = new System.Diagnostics.Process();
			proc.EnableRaisingEvents=false; 
			proc.StartInfo.FileName = "fdisk";
			proc.StartInfo.Arguments = "-l | grep NTFS";
			proc.Start();
			proc.WaitForExit();
		}
               
	private Util ()
		{
			
		}
	}
}

