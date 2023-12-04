var lines = GetInput();

CalculateSolution1(lines);
CalculateSolution2(lines);

static string[] GetInput()
{
  return File
    .ReadLines("input.txt")
    .ToArray();
}

static void CalculateSolution1(string[] lines)
{
  int sum = 0;
  foreach (var line in lines)
  {
    if (!string.IsNullOrEmpty(line))
    {
      var digits = line
              .Where(char.IsDigit)
              .ToArray();

      var number = digits.Length switch
      {
        0 => 0,
        1 => Convert.ToInt32(new string(digits[0], 2)),
        _ => Convert.ToInt32($"{digits[0]}{digits[^1]}")
      };

      sum += number;
    }
  }
  Console.WriteLine($"Solution 1: The calibration number is {sum}.");
}

static void CalculateSolution2(string[] lines)
{
  int sum = 0;
  foreach (var line in lines)
  {
    if (!string.IsNullOrEmpty(line))
    {
      var digits = SearchForDigitsExtended(line).
        OrderBy(entry => entry.pos).
        ToArray();

      var number = digits.Count() switch
      {
        0 => 0,
        1 => digits[0].value * 10 + digits[0].value,
        _ => digits[0].value * 10 + digits[^1].value
      };

      sum += number;
    }
  }
  Console.WriteLine($"Solution 2: The calibration number is {sum}.");
}

static IEnumerable<(int pos, int value)> SearchForDigitsExtended(ReadOnlySpan<char> text)
{
  List<(int pos, int value)> results = [];
  string[] words = ["one", "two", "three", "four", "five", "six", "seven", "eight", "nine"];

  for (int i = 0; i < text.Length; i++)
  {
    if (char.IsDigit(text[i]))
    {
      results.Add((i, int.Parse(text[i].ToString())));
      continue;
    }

    for (int wordIndex = 0; wordIndex < words.Length; wordIndex++)
    {
      string word = words[wordIndex];
      int iInner = i;

      bool? wordIsInText = null;
      for (int j = 0; j < word.Length && iInner < text.Length; iInner++, j++)
      {
        if (text[iInner] != word[j])
        {
          wordIsInText = false;
          break;
        }
        else if (iInner - i == word.Length - 1)
        {
          wordIsInText = true;
          break;
        }
      }

      if (wordIsInText == true)
      {
        results.Add((i, wordIndex + 1));
        continue;
      }
    }
  }

  return results;
}