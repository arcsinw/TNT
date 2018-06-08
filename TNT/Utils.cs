using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.SpeechRecognition;
using Windows.Media.SpeechSynthesis;
using Windows.Storage.Streams;

namespace TNT
{
    public class Utils
    {
        private static SpeechRecognizer _recognizer = new SpeechRecognizer();
        private static SpeechSynthesizer _synthesizer = new SpeechSynthesizer();

        /// <summary>
        /// 语音转文字
        /// </summary>
        /// <returns></returns>
        public static async Task<string> SpeechToTextAsync()
        {
            var recognizedText = string.Empty;

            //Max time to wait for the speaker to speak
            _recognizer.Timeouts.InitialSilenceTimeout = TimeSpan.FromSeconds(3);

            await _recognizer.CompileConstraintsAsync();

            SpeechRecognitionResult result = await _recognizer.RecognizeWithUIAsync();
            if (result.Status == SpeechRecognitionResultStatus.Success)
            {
                recognizedText = result.Text;
            }
            return recognizedText;
        }

        /// <summary>
        /// 文字转语音
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static async Task<IRandomAccessStream> TextToSpeechAsync(string text)
        {
            IRandomAccessStream stream = await _synthesizer.SynthesizeTextToStreamAsync(text);
            return stream;
        }
    }
}
