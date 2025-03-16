using Godot;
using System.IO;

public partial class DialogBox : Control
{
	private Label label;
	private ColorRect background;
	private AnimatedSprite2D man;
	private Button button;
	private string fullText = File.ReadAllText("assets/texts/text1.txt");
	private string currentText = "";
	private int currentIndex = 0;
	private float typingSpeed = 0.01f; 
	private float timer = 0f;
	private bool stop = false;
	private int shift = 30;
	private int iter = 0;
	private const int Padding = 10;
	private int spaces = 0;
	public override void _Ready()
	{
		button = GetNode<Button>("Button");
		button.Visible = false;
		button.Pressed += OnButtonPressed;
		label = GetNode<Label>("Label");
		background = GetNode<ColorRect>("ColorRect");
 		man = GetNode<AnimatedSprite2D>("man");
		label.Text = "";
	}

	public override void _Process(double delta)
	{
		if((iter == fullText.Length) && stop == false)
		{
			button.Visible = true;
			button.Text = "x";
			man.Play("default");
		}
		timer += (float)delta;
		if (timer >= typingSpeed && currentIndex < fullText.Length)
		{
			if (stop == false)
			{
				currentText += fullText[currentIndex];
				label.Text = currentText;
				currentIndex++;
				timer = 0f;
				iter++;
				if(fullText[iter] == ' ')
				{
					spaces++;
					if(spaces == 5)
					{
						currentText += "\n";
						spaces = 0;
					}
				}
				if(fullText[iter]=='\n') // здесь начинается новый абзац
				{
					stop = true;
					currentText += "\n";
					man.Play("default");
					button.Visible = true;
				}
			}
			UpdateBackgroundSize();
		}
		
			
		if (currentIndex >= fullText.Length)
		{
			label.Text += "  ";
			GD.Print("Текст полностью выведен!");
			stop = true;
			currentIndex = 0;
		}
	}

	private void UpdateBackgroundSize()
	{
		Vector2 textSize = label.GetMinimumSize();

		background.Size = new Vector2(textSize.X + Padding * 4, textSize.Y + Padding * 4);
		background.Position = new Vector2(-Padding, -Padding);
	}
	
	private void OnButtonPressed()
	{
		stop = false;
		label.Text = "";
		currentText = "";
		if(iter != fullText.Length)
			man.Play("talking");
		button.Visible = false;
		if(iter == fullText.Length)
		{
			stop = true;
			label.Visible = false;
			background.Visible = false;
		}
	}
	
	private string GetStrophe(int number)
	{
		return File.ReadAllText(@"assets//texts//text{number}.txt");
	}
}
