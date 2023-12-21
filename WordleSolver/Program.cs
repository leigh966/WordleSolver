using LeggySetLib;

string[] allWords = File.ReadAllLines("words.txt");

ISet<int> badCharacters = new BinarySet64(0,25);

