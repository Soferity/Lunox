namespace Lunox.Library.Enum
{
    #region AudioTypeEnum

    /// <summary>
    /// 
    /// </summary>
    public enum AudioType
    {
        /// <summary>
        /// An MP3 file is an audio file saved in a compressed audio format developed by the
        /// Moving Picture Experts Group (MPEG) that uses "Layer 3" audio compression (MP3).
        /// MP3 files are commonly used to store audio tracks, podcasts, lectures, sermons,
        /// and audiobooks.
        /// </summary>
        MP3,
        /// <summary>
        /// An M4A file is an audio file that may store various types of audio content,
        /// such as songs, podcasts, and audiobooks. It is saved in the MPEG-4 format and
        /// encoded with either the Advanced Audio Coding (AAC) codec or the Apple Lossless
        /// Audio Codec (ALAC).
        /// </summary>
        M4A,
        /// <summary>
        /// An AAC file is an audio file similar to an .MP3 file but compressed with Advanced
        /// Audio Coding (AAC), a lossy digital audio compression standard. The lossy compression
        /// maintains quality nearly indistinguishable from the original audio source, making it
        /// optimal for compressing music data.
        /// </summary>
        AAC,
        /// <summary>
        /// A FLAC file is an audio file compressed in the Free Lossless Audio Codec (FLAC) format,
        /// which is an open-source lossless audio compression format. It is similar to an .MP3 file,
        /// but is compressed without any loss in quality or loss of any original audio data.
        /// </summary>
        FLAC,
        /// <summary>
        /// Audio file created with the Apple Lossless Audio Codec (ALAC); used for storing digital
        /// music losslessly without losing any sound quality from the original audio data; differs
        /// from lossy audio codecs such as AAC.
        /// </summary>
        ALAC,
        /// <summary>
        /// An AIFF file is an audio file saved in the Audio Interchange File Format (AIFF). It contains
        /// high-quality audio stored in an uncompressed lossless format. AIFF files may also be saved as
        /// .AIF files or .AIFC files (if they are compressed).
        /// </summary>
        AIFF,
        /// <summary>
        /// A WAV file is an audio file saved in the WAVE format, a standard digital audio file format
        /// utilized for storing waveform data. WAV files may contain audio recordings with different
        /// sampling rates and bitrates but are often saved in a 44.1 kHz, 16-bit, stereo format, which
        /// is the standard format used for CD audio.
        /// </summary>
        WAV,
        /// <summary>
        /// A WMA file is an audio file saved in the Advanced Systems Format (ASF) container format
        /// developed by Microsoft. It stores audio data encoded in the Windows Media Audio (WMA),
        /// WMA Pro, WMA Lossless, or WMA Voice codecs. WMA files also include metadata objects such
        /// as the title, artist, album, and genre of the track.
        /// </summary>
        WMA,
        /// <summary>
        /// An OGG file is an audio file, similar to an .MP3 file, that typically stores music. It saves
        /// audio data in the Ogg container format that is compressed with the free, unpatented Vorbis
        /// audio compression. It may also include song metadata, such as artist information and track data.
        /// </summary>
        OGG
    }

    #endregion
}