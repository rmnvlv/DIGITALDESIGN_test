class Program
{
    const string UnwantedSigns = "()<>;:|~.,?!-/*”“\"";
    static void Main()
    {
        Console.WriteLine("Please enter path to TXT file.");
  
        string path = Console.ReadLine();
        string FileContents = File.ReadAllText(path);

        Console.WriteLine("(+) Processing...");
       
        foreach (char c in UnwantedSigns)
        {
            FileContents = FileContents.Replace(c, ' ');
            GC.Collect();
        }

        FileContents = FileContents.Replace(Environment.NewLine, " ");
        string[] Words = FileContents.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        FileContents = string.Empty;
        GC.Collect();

        Dictionary<string, int> WordCounts = new Dictionary<string, int>();
        foreach (string w in Words.Select(x => x.ToLower()))
            if (WordCounts.TryGetValue(w, out int c))
                WordCounts[w] = c + 1;
            else
                WordCounts.Add(w, 1);
        Words = new string[1];
        GC.Collect();

        List<Tuple<int, string>> WordStats = WordCounts.Select(x => new Tuple<int, string>(x.Value, x.Key)).ToList();
        WordStats.Sort((x, y) => y.Item1.CompareTo(x.Item1));
        foreach (Tuple<int, string> t in WordStats)
            File.AppendAllText(path + ".CountedWords.txt", t.Item2 + " " + t.Item1 + Environment.NewLine);

        Console.WriteLine("(+) " + path + ".CountedWords.txt" + " ~~~~~~ Words counted");
    }
}