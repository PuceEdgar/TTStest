
using System.Speech.Synthesis;
namespace TTStest
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello, World!");
			var powerTTS = new PowerTTS();
			powerTTS.Open();

			var tts = new SpeechSynthesizer();
			tts.SetOutputToDefaultAudioDevice();
			string message = Console.ReadLine();

			while (!message.Equals("exit")) 
			{
				//tts.Speak(message);
				powerTTS.Play(message);
				message = Console.ReadLine();
			}


			tts.Speak("bye");
			Console.WriteLine("bye!");
		}
	}
}
