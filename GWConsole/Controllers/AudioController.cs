using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GWConsole.Controllers {

    public static class AudioController {

        private static SoundPlayer _oneShotPlayer;
        private static SoundPlayer _loopingPlayer;

        public static void PlayOneShot(int soundId) {
            string soundLocationString = AudioController.GetSoundLocationString(soundId);
            if (!String.IsNullOrWhiteSpace(soundLocationString)) {
                _oneShotPlayer = new SoundPlayer(soundLocationString);

                if (_oneShotPlayer != null) {
                    _oneShotPlayer.Play();
                }
            }
        }

        public static void PlayLooping(int soundIndex) {
            throw new NotImplementedException();
        }

        public static void StopLooping() {
            throw new NotImplementedException();
        }

        private static string GetSoundLocationString(int soundId) {
            // PUT NEW SOUND LOCATIONS HERE!
            // Ensure all sound files are roughly placed in the same folders/structure
            switch (soundId) {
                case 0:
                    return Environment.CurrentDirectory + "\\Content\\Audio\\menu.wav";
            }
            return string.Empty;
        }
    }
}
