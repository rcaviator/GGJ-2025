using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GGJ2025.Managers
{
  [CreateAssetMenu(fileName = "AudioMappings", menuName = "Sounds/AudioMappings", order = 1)]
  public class AudioMappings : ScriptableObject
  {
    [Serializable]
    public class AudioMapping<T> where T : Enum
    {
      public T type;
      public AssetReference sound = null!;
    }

    public AudioMapping<MusicSoundEffect>[] musicMappings = Array.Empty<AudioMapping<MusicSoundEffect>>();

    public AudioMapping<UISoundEffect>[] uiMappings = Array.Empty<AudioMapping<UISoundEffect>>();

    public AudioMapping<GameSoundEffect>[] gameMappings = Array.Empty<AudioMapping<GameSoundEffect>>();
  }
}