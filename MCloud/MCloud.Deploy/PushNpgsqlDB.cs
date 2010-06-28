
using System;
using System.IO;
using System.Diagnostics;

using ICSharpCode.SharpZipLib;
using ICSharpCode.SharpZipLib.GZip;

namespace MCloud.Deploy
{
	public class PushNpgsqlDB : SSHDeployment
	{
		private string database;
		private string dump_file;
		
		public PushNpgsqlDB (string db_name)
		{
			Database = db_name;
		}
		
		public string Database {
			get { return database; }
			set {
				if (database != value)
					SetDumpFile (null);
				database = value;
			}
		}
		
		protected override void RunImpl (Node node, NodeAuth auth)
		{
			string host = node.PublicIPs [0].ToString ();
			string file = GetDumpFile ();
			
			PutFile (host, auth, file, "/root/dump.sql.gz");
			RunCommand ("gunzip -d -f /root/dump.sql.gz", host, auth);
			RunCommand ("mv /root/dump.sql ~postgres/.", host, auth);
			RunCommand ("sudo -u postgres psql -f ~postgres/dump.sql", host, auth);
		}
		
		private string GetDumpFile ()
		{
			if (dump_file != null)
				return dump_file;
			
			string file = Path.GetTempFileName ();
		
			Process p = new Process ();
			// how the hell do i find this on windows?
			p.StartInfo.FileName = "pg_dump";
			p.StartInfo.Arguments = String.Format ("-f {0} -x {1}", file, Database);
			p.StartInfo.UseShellExecute = true;
			
			p.Start ();
			p.WaitForExit ();
			
			string zip = Path.GetTempFileName () + ".sql.gz";
			FileStream s = File.OpenWrite (zip);
			FileStream dump_stream = File.OpenRead (file);
			GZipOutputStream zstream = new GZipOutputStream (s);
			
			int size;
			byte [] buffer = new byte [1000];
    		do {
        		size = dump_stream.Read (buffer, 0, buffer.Length);
        		zstream.Write (buffer, 0, size);
    		} while (size > 0);
    
			zstream.Close();
    		dump_stream.Close();
			
			dump_file = zip;
			return zip;
		}
		
		private void SetDumpFile (string file)
		{
			if (file == null && dump_file != null)
				File.Delete (dump_file);	
			
			dump_file = file;
		}
	}
}
