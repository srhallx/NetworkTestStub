using System;
using SQLite;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace NetworkTest
{
	public class Settings
	{
		static string dbName = "settingsdb.db3";

		public Settings () {}
		public Settings (string property, string value) { Property = property; Value = value; }

		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string Property { get; set; }
		public string Value { get; set; }


		public static string GetURI(){

			try
			{
				using (SQLiteConnection db = new SQLiteConnection (GetDBPath (dbName))) {

					List<Settings> settings = new List<Settings> (from p in db.Table<Settings>() select p);

					db.Close();

					if (settings.Count > 0)
					{
						string val = settings.Where(x => x.Property.Equals("wsuri")).First().Value;

						return val;
					}
				}
			}
			catch (Exception ex) {
				Console.WriteLine (ex.Message);
			}

			return null;
		}

		public static void StoreURI(string uri) 
		{

			try 
			{
				SQLiteConnection db = new SQLiteConnection (GetDBPath (dbName));


				db.DropTable<Settings>();

				db.CreateTable<Settings> ();

				// declare vars
				List<Settings> settings = new List<Settings> ();

				settings.Add (new Settings ("wsuri", uri));

				int rows = db.InsertAll (settings);

				// close the connection
				db.Close ();

			}
			catch (Exception ex) {
				Console.WriteLine (ex.Message);
			}
		}

		protected static string GetDBPath (string dbName)
		{
			// get a reference to the documents folder
			var documents = Environment.GetFolderPath (Environment.SpecialFolder.Personal);

			// create the db path
			string db = Path.Combine (documents, dbName);

			return db;
		}
	}
}

