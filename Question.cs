namespace CS_DHT;

public class Question
{
	private static readonly Random _random = new();

	private readonly string[] _answers;
	private readonly int _rightIdx;

	public IEnumerable<string> Answers => this._answers;

	public string Text { get; init; }

	public Question(string text, int rightIdx, string[] answers)
	{
		this.Text = text;
		var rightAnswer = answers[rightIdx];
		this._answers = [..answers.OrderBy(_ => _random)];

		for (int i = 0; i < this._answers.Length; i++) {
			if (rightAnswer == this._answers[i]) {
				this._rightIdx = i + 1;
				break;
			}
		}
	}

	bool IsRight(int index) => this._rightIdx == index;
	
}
