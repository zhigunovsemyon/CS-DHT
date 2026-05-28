namespace CS_DHT;

public class Tests
{
	private static readonly Random _random = new();

	private readonly Question[] _questions = [
		new Question("Какова главная цель консистентного хеширования в DHT?", 1, [
			"Минимизировать количество ключей, перемещаемых при добавлении или удалении узла",
			"Обеспечить, чтобы каждый узел хранил строго одинаковое число ключей",
			"Гарантировать логарифмическую длину маршрута поиска",
			"Равномерно распределить IP-адреса узлов по пространству имён"
		]),
		new Question("Каким образом Chord отображает ключи на узлы?", 4, [
			"Через двумерное координатное пространство, в котором каждый узел отвечает за ближайший квадрат",
			"Через префиксное дерево, где ключ соответствует ближайшему префиксу узла",
			"Через случайный выбор k ближайших соседей по сетевой задержке",
			"Ключ назначается первому узлу, идентификатор которого равен или следует за " +
			"хешем ключа по часовой стрелке на кольце"
		])
	];

	public IEnumerable<Question> Questions { get; init; } 
	
	public Tests(int count)
	{ 
		this.Questions = this._questions.OrderBy(_ => _random.Next()).Take(count);
	}

	public void RunTests()
	{
		int rightAnswers = 0;

		foreach (var question in this.Questions) {
			if (Tests.AskQuestion(question)) {
				rightAnswers++;
			}
		}

		Console.WriteLine("Число правильных ответов: {0}/{1}", rightAnswers, this.Questions.Count());
		Console.WriteLine("Нажмите любую кнопку, чтобы продолжить");
		Console.ReadKey(true);
	}

	private static bool AskQuestion(Question question) 
	{
		for (; ; ) {
			Console.Clear();
			Console.WriteLine(question.Text);
			Console.WriteLine("Варианты ответа:");

			var answersCount = question.Answers.Count();
			for (int i = 0; i < answersCount; i++) {
				var answerText = question.Answers.ElementAt(i);
				Console.WriteLine(" {0} -- {1}\n", i + 1, answerText);
			}
			Console.WriteLine("Укажите номер ответа");
			var ch = Console.ReadKey(true).KeyChar;
			
			const char ESC = (char)27;
			if (ch == ESC) {
				return false;
			}
			if (ch < '0' || ch > '9') {
				continue;
			}
			return question.IsRight(ch - '0');
		}
	}
}
