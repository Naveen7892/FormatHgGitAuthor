using System;
using System.IO;
using System.Text;

namespace FormatHgGitAuthor {
   class Program {
      static void Main (string[] args) {
         //foreach (String s in args) Console.WriteLine (s);
         try {
            if (args.Length == 0) {
               Console.WriteLine ("Provide authors file path!");
               Environment.Exit (0);
            } else {
               FormatHgGit (args[0]);
            }
         } catch (Exception ex) {
            Console.WriteLine ("SOMETHING WENT WRONG!!! CHECK DEBUG INFO BELOW:");
            Console.WriteLine (ex.ToString());
            Environment.Exit (1);
         }
      }

      static void FormatHgGit (string filepath) {
         if (!File.Exists (filepath)) {
            Console.WriteLine ("File path does not exist!");
            Environment.Exit (0);
         }
         StringBuilder sb = new StringBuilder ();
         int newlyFormatted = 0;
         int alreadyFormatted = 0;
         var authors = File.ReadAllLines (filepath);
         int total = authors.Length;
         if(total == 0) {
            Console.WriteLine ("No author info found!");
            Environment.Exit (0);
         }
         foreach (string s in authors) {
            if (!s.Contains ("\"=\"")) {
               string str = $"\"{s}\"=";
               if (str.Contains ("<") && str.Contains (">")) {
                  str += $"\"{s}\"";
               } else {
                  str += $"\"{s} <{s}@localhost>\"";
               }
               sb.AppendLine (str);
               newlyFormatted++;
            } else {
               // the line is already formatted "xxx"="yyy"
               // TODO: optionally check if the email address is provided for the already formatted author line
               alreadyFormatted++;
            }
         }
         if (newlyFormatted > 0) File.WriteAllText (filepath, sb.ToString ());
         // checks if all author line already formatted 
         Console.WriteLine (alreadyFormatted > 0 ? $"{alreadyFormatted}/{total} Author Info seems to be already formatted. Check manually to avoid potential issues." : $"{newlyFormatted}/{total} Author Info formatted successfully!");
      }
   }
}
