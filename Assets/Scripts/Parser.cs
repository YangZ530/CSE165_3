using UnityEngine;
using System;
using System.Collections;


public class Parser{
	
	private string filepath;
	
	public Parser(string filepath)
	{
		this.filepath = filepath;
	}
	
	public Queue parse()
	{
		Queue checkPoints = new Queue ();
		string line;

		// Read the file and line by line and fill the graph.
		System.IO.StreamReader file = 
			new System.IO.StreamReader (filepath);	

		if (file == null) {
			Console.WriteLine ("file not found");
			return null;
		}
		
		while ((line = file.ReadLine()) != null) {
			float x, y, z;
			string[] coordinates;
			char[] separatingChars = { ' ' };
				
			file.ReadLine (); // [
				
			line = file.ReadLine (); // id ie. id 0
			coordinates = line.Split (separatingChars, System.StringSplitOptions.RemoveEmptyEntries);
			x = Int32.Parse (coordinates [0]);
			y = Int32.Parse (coordinates [1]);
			z = Int32.Parse (coordinates [2]);

				
			checkPoints.Enqueue (new Vector3 (x, y, z));
		}
		
		file.Close ();
		return checkPoints;
	}
}
