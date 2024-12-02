using NAudio.Wave;

class Program
{
  static void Main(string[] args)
  {
    string filePath = @"C:\Users\tong\Music\audio_player_test\file1.mp3";
    Console.WriteLine($"audio file: {filePath}");
    

    using (var audioFile = new AudioFileReader(filePath))
    using (var outputDevice = new WaveOutEvent())
    {
      outputDevice.Init(audioFile);
      outputDevice.Play();

      int totalDuration = (int)audioFile.TotalTime.TotalSeconds;
      int width = 50;  // Width of the progress bar
      
      TimeSpan totalTime = audioFile.TotalTime;
      while (outputDevice.PlaybackState == PlaybackState.Playing)
      {
        TimeSpan currentTime = audioFile.CurrentTime;
        int currentPosition = (int)audioFile.CurrentTime.TotalSeconds;
        int progress = (int)((currentPosition / (double)totalDuration) * width);
        Console.SetCursorPosition(0, 1);
        Console.Write("[{0}{1}] {2:D2}:{3:D2}:{4:D2} / {5:D2}:{6:D2}:{7:D2}",
            new string('#', progress),
            new string('-', width - progress),
            currentTime.Hours, currentTime.Minutes, currentTime.Seconds,
            totalTime.Hours, totalTime.Minutes, totalTime.Seconds);
        Thread.Sleep(1000);
      }

      Console.WriteLine("\nAudio playback finished.");
    }

    Console.WriteLine("Audio playback stopped.");
  }
}