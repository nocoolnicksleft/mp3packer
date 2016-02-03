using System;
using System.IO;
using System.Collections;
using HundredMilesSoftware.UltraID3Lib;


namespace mp3packer
{

	class MainThread
	{
		static long CurrentFileID = 1;
		static SoundfileCollection Soundfiles = new SoundfileCollection();
		static StringCollection Artists = new StringCollection();
		const int MP3_COLUMN_LENGTH = 21;

		static void ScanDirectory(string path)
		{
			DirectoryInfo dir = new DirectoryInfo(path);
			
			Console.WriteLine("DIRECTORY " + path);

			Soundfile myfile;

			foreach (FileInfo file in dir.GetFiles() ) 
			{
				Console.WriteLine(file.Name);
				myfile = new Soundfile(path + @"\" + file.Name);
				myfile.RetrieveTagInfo();
				myfile.ID = CurrentFileID;
				CurrentFileID++;

				myfile.FileName = @"target\A\A"+(myfile.ID -1).ToString("000000")+".mp3";
				file.CopyTo(myfile.FileName,true);
				File.SetAttributes(myfile.FileName, FileAttributes.Archive);

				if (myfile.Artist != null) 
				{
					long aid = Artists.IndexOf(myfile.Artist);
					if (aid != -1) myfile.ArtistID = aid;
					else 
					{
						myfile.ArtistID = Artists.Add(myfile.Artist);
					}
				}
				Console.WriteLine(" " + myfile.Artist+" ("+myfile.ArtistID+") / " + myfile.Title + " (" + myfile.Album + ")");
				Soundfiles.Add(myfile);

			}

			foreach (DirectoryInfo subdir in dir.GetDirectories())
			{
				ScanDirectory(path + @"\" + subdir.Name);	
			}
		}

		static void WritePackage()
		{
			using (StreamWriter sw = new StreamWriter(@"target\V\c.txt",false,System.Text.Encoding.ASCII)) 
			{
				sw.Write((CurrentFileID - 2).ToString("000000"));
				sw.Write((Artists.Count - 1).ToString("000000"));
			}

			using (StreamWriter sw = new StreamWriter(@"target\V\a.txt",false,System.Text.Encoding.ASCII)) 
			{
				foreach(String artist in Artists) 
				{
					sw.Write(artist.PadRight(MP3_COLUMN_LENGTH).Substring(0,MP3_COLUMN_LENGTH));
				}
			}


			foreach(String artist in Artists) 
			{
				using (StreamWriter sw = new StreamWriter(@"target\V\k"+Artists.IndexOf(artist).ToString("000000") +".txt",false,System.Text.Encoding.ASCII)) 
				{
					int i = 1;
					foreach (Soundfile file in Soundfiles) 
					{
						if (file.Artist == artist) 
						{
							file.ArtistTrackNo = i;
							sw.Write(file.Title.PadRight(MP3_COLUMN_LENGTH).Substring(0,MP3_COLUMN_LENGTH)+file.ID.ToString("000000"));
							i++;
						}
					}
				}			
			}

			using (StreamWriter sw = new StreamWriter(@"target\V\t.txt",false,System.Text.Encoding.ASCII)) 
			{
				foreach(Soundfile file in Soundfiles) 
				{
 				  sw.Write(file.Artist.PadRight(MP3_COLUMN_LENGTH).Substring(0,MP3_COLUMN_LENGTH) + file.Title.PadRight(MP3_COLUMN_LENGTH).Substring(0,MP3_COLUMN_LENGTH) + file.ArtistID.ToString("00000") + file.ArtistTrackNo.ToString("00000") + file.Length.ToString("00000"));
				}
			}


		}

		
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
            Console.WriteLine("Uses UltraID3Lib, Copyright (c) 2002-2010, Hundred Miles Software");

			ScanDirectory(@"data");
			WritePackage();
			Console.ReadLine();
		}
	}






}
